using System;
using System.Data.SqlClient;
using System.Threading;
using System.Transactions;
using System.Web.UI;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Diagnostics.Logging;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class RecordDetailUserControl : BaseEditDataUserControl
	{

		#region Events

		public event EventHandler DataSourceIdChanged;

		protected void RaiseDataSourceIdChanged(EventArgs e)
		{
			if (this.DataSourceIdChanged != null)
			{
				this.DataSourceIdChanged(this, e);
			}
		}

		#endregion

		#region Constants

		private const string ViewStateKey_DataSourceId = "DataSourceId";

		#endregion

		#region Properties

		public int? DataSourceId
		{
			get
			{
				return this.ViewState[ViewStateKey_DataSourceId] as int?;
			}
			set {
				this.SetDataSourceId(value, true);
			}
		}

		private object _DataItem;

		public object DataItem
		{
			get
			{
				if (this._DataItem == null)
				{
					this._DataItem = this.LoadDataItem();
				}
				return this._DataItem;
			}
			private set
			{
				this._DataItem = value;
			}
		}

		protected void SetDataSourceId(int? value, bool raiseDataSourceIdChangeEvent)
		{
			int? currentId = this.DataSourceId;
			if (value == currentId)
			{
				return;
			}

			if (value == null)
			{
				this.ViewState.Remove(ViewStateKey_DataSourceId);
			}
			else
			{
				this.ViewState[ViewStateKey_DataSourceId] = value;
			}

			if (raiseDataSourceIdChangeEvent)
			{
				this.OnDataSourceIdChange(new EventArgs());
			}
		}

		public bool IsNewRecord
		{
			get
			{
				return (this.DataSourceId == null);
			}
		}

		#endregion

		#region Abstract Methods

		protected virtual object LoadDataItem()
		{
			//TODO: Convert LoadDataItem() to an abstract method.
#warning TODO: Convert LoadDataItem() to an abstract method.

			throw new NotSupportedException();
		}

		/// <summary>
		/// A helper method for <see cref="OnSaveAsNew"/> that must be overridden by inheritors that wish to use that method.
		/// </summary>
		/// <returns>The <see cref="DataSourceId"/> of the new DataSource.</returns>
		protected virtual int PrepareToSaveAsNew()
		{
			//TODO: Refactoring: Rename this method to something more explanatory of it's intended implementation?
#warning Refactoring: Rename this method to something more explanatory of it's intended implementation?

			throw new NotSupportedException("This control does not support Save As New functionality.");
		}

		#endregion

		#region Methods

		protected virtual void OnDataSourceIdChange(EventArgs e)
		{
			this.RaiseDataSourceIdChanged(e);
		}

		protected override void UnbindItem()
		{
			base.UnbindItem();

			//clear the cached DataItem instance so that it will be reinitialized the next time it is accessed
			this.ClearDataSource();
		}

		private void ClearDataSource()
		{
			this.DataItem = null;
		}

		/// <summary>
		/// Implements the generic "SaveAsNew" logic (such as error handling and raising control events) that is common to all inheritors' SaveAsNew functionality.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		protected bool OnSaveAsNew(EventArgs e)
		{
			bool success = false;

			try
			{
				int? originalDataSourceId = this.DataSourceId;

				using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					//First, we need to call PrepareToSaveAsNew to get the new DataSourceId (and to allow the inheritor to do any other preparation that may be required).
					int newDataSourceId = this.PrepareToSaveAsNew();

					//next, change the control's DataSourceId to reference the new DataSource
					//NOTE: Don't raise the DataSourceIdChanged event yet, or a redirect may abort the transaction!!!
					this.SetDataSourceId(newDataSourceId, false); //this.DataSourceId = newDataSourceId;

					//next, call SaveInput() to apply any UI input/changes (except for those UI controls re-bound above) to the new Project (only)
					success = this.OnInputSave(new EventArgs(), true);
					if (success)
					{
						scope.Complete(); // transaction complete

						//NOTE: Now we can safely raise the DataSourceIdChanged event that we prevented above.
						this.OnDataSourceIdChange(new EventArgs());
					}
				}

				if (!success)
				{
					//NOTE: Since the code above modified the DataSourceId, we need to revert the DataSourceId back to it's original value
					//NOTE: Don't raise the DataSourceIdChanged event, since this change is actually reverting the change above, so no permanent change actually occurred
					this.SetDataSourceId(originalDataSourceId, false); //this.DataSourceId = newProject.Id;

					//NOTE: Since the code above modified the state of some of the UI controls, we need to re-DataBind to revert the UI portion of the above changes
					this.BindItem();
				}
			}
			catch (ThreadAbortException ex)
			{
				//NOTE: we intentionally bypass the normal exception handling logic for ThreadAbortExceptions, because these are almost always the result of invoking Response.Redirect(), and aren't really an error
				throw;
			}
			catch (Exception ex)
			{
				string userFriendlyExceptionMessage = Global.GetUserFriendlyExceptionMessage(ex, true);
				if (!string.IsNullOrEmpty(userFriendlyExceptionMessage))
				{
					this.SetErrorMessage(userFriendlyExceptionMessage);
					LogManager.Current.Log(Severity.Info, this, userFriendlyExceptionMessage, ex);
				}
				else
				{
					LogManager.Current.Log(Severity.Warn, this, "Unrecognized Exception", ex);
					throw;
				}
			}
			return success;
		}

		#endregion

	}
}