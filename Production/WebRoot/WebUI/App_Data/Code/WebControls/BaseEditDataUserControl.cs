using System;
using System.Data.SqlClient;
using System.Threading;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class BaseEditDataUserControl : UserControl
	{

		#region Events

		public event EventHandler InputSaved;
		public event EventHandler InputCancelled;

		protected void RaiseInputSaved(EventArgs e)
		{
			if (this.InputSaved != null)
			{
				this.InputSaved(this, e);
			}
		}

		protected void RaiseInputCancelled(EventArgs e)
		{
			if (this.InputCancelled != null)
			{
				this.InputCancelled(this, e);
			}
		}

		#endregion

		#region Constants

		private const string ViewStateKey_DefaultValuesSpecification = "DefaultValuesSpecification";

		#endregion

		#region Abstract Members

		protected abstract Label ErrorLabel { get; }

		protected abstract void BindItem();

		/// <summary>
		/// Performs the specific data saving/serialization functionality
		/// appropriate for the implementing class.
		/// </summary>
		/// <remarks>
		/// Note to inheritors: If a specific implementation of this method results in a modification of the primary DataItem object's ID, 
		/// then this method should update the DataSourceId property to be the same as the DataItem object's new ID value.
		/// E.g. When saving a "new" record (due to DataSourceId being null) with a DB-generated ID, 
		/// then after the save operation is complete and the record's ID is assigned, the control's DataSourceId property 
		/// should automatically be set to the new record's ID.
		/// </remarks>
		protected abstract void SaveInput();

		#endregion

		#region Properties

		/// <summary>
		/// Stores any query conditions that are fixed or immutable except via this property (e.g. shouldn't be modifiable by filter controls, etc.).
		/// </summary>
		/// <remarks>
		/// <para>
		/// Note that, unlike the <see cref="QuerySpecification"/> property, any direct modifications of the value returned by this property WILL NOT be persisted.
		/// </para>
		/// </remarks>
		public IQuerySpecification DefaultValuesSpecification
		{
			get
			{
				string xml = this.ViewState[ViewStateKey_DefaultValuesSpecification] as string;
				return (string.IsNullOrEmpty(xml)) ? new QuerySpecificationWrapper() : QuerySpecificationWrapper.FromXml(xml);
			}
			set
			{
				IQuerySpecification newValue = value;

				newValue = CleanUpDefaultValuesSpecification(newValue);

				IQuerySpecification currentDefaultValuesSpecification = this.DefaultValuesSpecification;
				if (newValue == currentDefaultValuesSpecification)
				{
					return; //do nothing if the new value is the same as the current value
				}

				if (newValue == null)
				{
					this.ViewState.Remove(ViewStateKey_DefaultValuesSpecification);
				}
				else
				{
					this.ViewState[ViewStateKey_DefaultValuesSpecification] = newValue.ToString();
				}

				//this.OnDefaultValuesSpecificationChange(new EventArgs());
			}
		}

		/// <summary>
		/// Perform "clean up" conversion/standardization of a <see cref="DefaultValuesSpecification"/> value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static IQuerySpecification CleanUpDefaultValuesSpecification(IQuerySpecification value)
		{
			if (value != null)
			{
				if (value.Conditions.Count < 1)
				{
					//there are no conditions, so the value is equivelent to null
					value = null;
				}
				else
				{
					value = value.InnerData.Copy(); //copy the value before modifying it
					//ignore/clear any existing paging and sorting info in the value (since those are irrelevant to default values)
					value.Paging.Clear();
					value.SortBy.Clear();
				}
			}
			return value;
		}

		#endregion

		#region Lifecycle/Binding Methods

		protected virtual void UnbindItem()
		{
			LogManager.Current.Log(Severity.Debug, this, this.ID + ": Entering: BaseEditDataUserControl.UnbindItem()");
			this.ClearErrorMessage();
		}

		protected override void OnDataBinding(EventArgs e)
		{
			LogManager.Current.Log(Severity.Debug, this, this.ID + ": Entering: BaseEditDataUserControl.OnDataBinding(EventArgs e)");
			base.OnDataBinding(e);
			this.BindItem();
		}

		/// <summary>
		/// Implements the generic "CancelInputChanges" logic (such as error handling and raising control events) that is common to all inheritors' CancelInputChanges functionality.
		/// </summary>
		/// <param name="e"></param>
		protected void OnInputCancel(EventArgs e)
		{
			this.BindItem(); //reload/refresh the control's UI with the latest data

			this.RaiseInputCancelled(e);
		}

		/// <summary>
		/// Implements the generic "SaveInput" logic (such as error handling and raising control events) that is common to all inheritors' SaveInput functionality.
		/// </summary>
		/// <param name="e"></param>
		protected void OnInputSave(EventArgs e)
		{
			this.OnInputSave(e, false);
		}

		protected bool OnInputSave(EventArgs e, bool suppressInputSavedEvent)
		{
			try
			{
				using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					this.SaveInput();

					// transaction complete
					scope.Complete();
				}

				this.BindItem(); //reload/refresh the control's UI with the latest data

				if (!suppressInputSavedEvent)
				{
					this.RaiseInputSaved(e);
				}

				return true;
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

			return false;
		}

		/// <summary>
		/// Implements the generic "DeleteDataSource" logic (such as error handling and raising control events) that is common to all inheritors' DeleteDataSource functionality.
		/// </summary>
		/// <param name="e"></param>
		protected void OnDelete(EventArgs e)
		{
			try
			{
				using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					this.DeleteItem();

					// transaction complete
					scope.Complete();
				}

				this.BindItem(); //reload/refresh the control's UI with the latest data

				//this.RaiseInputSaved(e);
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
		}

		protected virtual void DeleteItem()
		{
			throw new NotSupportedException();
		}

		#endregion

		#region Other Methods

		/// <summary>
		/// Convenience method.
		/// </summary>
		protected void ClearErrorMessage()
		{
			this.SetErrorMessage(null);
		}

		/// <summary>
		/// Sets the error or warning message that should be displayed by the control to communicate a problem to the end user.
		/// </summary>
		/// <param name="message">The message for the user.</param>
		protected void SetErrorMessage(string message)
		{
			this.ErrorLabel.Text = message ?? "";

			if (!string.IsNullOrEmpty(message))
			{
				WebUtility.ShowAlertBox(this, message);
			}
		}

		/// <summary>
		/// Returns the current error or warning message that is (or will be) displayed by the control.
		/// </summary>
		/// <returns></returns>
		protected string GetErrorMessage()
		{
			return this.ErrorLabel.Text;
		}

		#region File Upload-related methods

		/// <summary>
		/// Registers a specified control as an "FileUpload" control, and configures the page appropriately.
		/// </summary>
		/// <remarks>
		/// It is necessary to call this method for file upload controls because file upload will not work when initiated as part of a partial page PostBack.
		/// See here for details (this describes a related issue, but the cause and solution are the same):
		/// http://blog.encoresystems.net/articles/fileupload-in-an-ajax-updatepanel.aspx
		/// </remarks>
		protected void RegisterFileUploadControl(Control fileUploadControl)
		{
			Global.RegisterAsFullPostBackTrigger(fileUploadControl);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <param name="controlId"></param>
		/// <returns></returns>
		protected bool RegisterFileUploadControl(string controlId)
		{
			Control fileUploadControl = this.FindControl(controlId);
			if (fileUploadControl != null)
			{
				this.RegisterFileUploadControl(fileUploadControl);
				return true;
			}
			return false;
		}

		#endregion

		#endregion

	}
}