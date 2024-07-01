using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class PersonSummaryDetailPanel : BaseRecordSummaryDetailUserControl
	{
		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		/// <summary>
		/// A strongly-typed shadow of the underlying property.
		/// </summary>
		protected new VwMapPerson DataItem
		{
			get
			{
				return base.DataItem as VwMapPerson;
			}
		}

		#endregion

		#region Methods

		public override string GetDataSourceEntityName()
		{
			return "Person";
		}

		public override string GenerateDetailPageUrl(int? dataSourceId)
		{
			return Global.GetPersonDetailPageUri(dataSourceId, null);
		}

		protected override void UnbindItem()
		{
			base.UnbindItem();

			this.PopulateListControls();
			this.ClearDataControls();
		}

		/// <summary>
		/// Re-populates the ListItems of all this control's ListControls.
		/// </summary>
		/// <remarks>
		/// This method should be called every time the control is Un-bound/Re-bound so that any changes made for a previous DataSource don't persist.
		/// </remarks>
		private void PopulateListControls()
		{
			Global.BindRoleListControl(this.chkRoleList, RowStatus.RowStatusId.Active, true);
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.chkRoleList.ClearSelection();
		}

		protected override object LoadDataItem()
		{
			VwMapPerson dataItem = null;
			if (this.DataSourceId != null)
			{
				VwMapPersonCollection bindItemCollection = VwMapPersonController.FetchByID(this.DataSourceId);
				dataItem = (bindItemCollection.Count == 0) ? null : bindItemCollection[0];
			}
			return dataItem ?? new VwMapPerson();
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			try
			{
				VwMapPerson bindItem = this.DataItem;
				if (bindItem.IsNew)
				{
					return;
				}

				this.chkRoleList.ClearSelection();

				Person bindItemTable = Person.FetchByID(this.DataSourceId);

				Global.SetRoleListControl(this.chkRoleList, bindItemTable.GetRoleCollection());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}
		}

		#endregion
	}
}