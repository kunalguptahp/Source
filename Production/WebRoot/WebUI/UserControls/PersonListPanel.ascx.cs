using System;
using System.Data;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.WebControlEnhancements;
using HP.HPFx.Web.Utility;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class PersonListPanel : BaseListViewUserControl
	{

		#region Overrides

		#region Property Overrides

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		protected override GridView Grid
		{
			get { return this.gvList; }
		}

		#endregion

		#region Method Overrides

		protected override void BindScreen(IQuerySpecification querySpecification)
		{
			PersonQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			PersonQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			GridView gv = this.Grid;

			//change the ODS's DAL target based upon the whether the RoleId condition is null or not (because if it is not we need to use a different DAL query)
			this.odsDataSource.TypeName = "HP.ElementsCPS.Data.SubSonicClient." + ((query.RoleId == null) ? "VwMapPersonController" : "VwMapPersonRoleController");

			//Configure the ObjectDataSource using the current QuerySpecification
			Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
			querySpecificationParameter.DefaultValue = query.ToString();

			//read the query's Conditions info

			this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
			this.txtIdList.Text = query.IdListAsString;
			this.txtEmail.DisableIf(!string.IsNullOrEmpty(immutableConditions.Email));
			this.txtEmail.Text = query.Email;
			this.txtLastName.DisableIf(!string.IsNullOrEmpty(immutableConditions.LastName));
			this.txtLastName.Text = query.LastName;
			this.txtFirstName.DisableIf(!string.IsNullOrEmpty(immutableConditions.FirstName));
			this.txtFirstName.Text = query.FirstName;
			this.txtWindowsId.DisableIf(!string.IsNullOrEmpty(immutableConditions.WindowsId));
			this.txtWindowsId.Text = query.WindowsId;

            this.ddlTenant.DisableIf(immutableConditions.TenantGroupId != null);
            if (query.TenantGroupId == null)
            {
                this.ddlTenant.ClearSelection();
            }
            else
            {
                this.ddlTenant.ForceSelectedValue(query.TenantGroupId);
            }

			this.ddlRole.DisableIf(immutableConditions.RoleId != null);
			if (query.RoleId == null)
			{
				this.ddlRole.ClearSelection();
			}
			else
			{
				this.ddlRole.ForceSelectedValue((UserRoleId)query.RoleId);
			}

			this.ddlStatus.DisableIf(immutableConditions.RowStateId != null);
			if (query.RowStateId == null)
			{
				this.ddlStatus.ClearSelection();
			}
			else
			{
				this.ddlStatus.ForceSelectedValue((RowStatus.RowStatusId)query.RowStateId);
			}

			this.txtCreatedAfter.DisableIf(immutableConditions.CreatedAfter != null);
			Global.InitializeFilter_DateOnly(this.txtCreatedAfter, query.CreatedAfter);

			this.txtCreatedBefore.DisableIf(immutableConditions.CreatedBefore != null);
			Global.InitializeFilter_DateOnly(this.txtCreatedBefore, query.CreatedBefore);

			this.txtCreatedBy.DisableIf(immutableConditions.CreatedBy != null);
			Global.InitializeFilter_CreatedByFilter(this.txtCreatedBy, query.CreatedBy);

			this.txtModifiedAfter.DisableIf(immutableConditions.ModifiedAfter != null);
			Global.InitializeFilter_DateOnly(this.txtModifiedAfter, query.ModifiedAfter);

			this.txtModifiedBefore.DisableIf(immutableConditions.ModifiedBefore != null);
			Global.InitializeFilter_DateOnly(this.txtModifiedBefore, query.ModifiedBefore);

			this.txtModifiedBy.DisableIf(immutableConditions.ModifiedBy != null);
			Global.InitializeFilter_ModifiedByFilter(this.txtModifiedBy, query.ModifiedBy);

			this.BindScreen_GridPaging(query);
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			PersonQuerySpecification query = new PersonQuerySpecification();
			GridView gv = this.Grid;

			//NOTE: DO NOT update the sorting and paging here _from the GridView_, because that is done in the GridView's RowCommand handler
			//NOTE: However, DO preserve the existing sort order (if any) _from the previous query_ to preserve the sort order when the filter conditions are changed
			IQuerySpecification previousQuery = this.QuerySpecification;
			if ((previousQuery != null) && (previousQuery.SortBy != null) && (previousQuery.SortBy.Count > 0))
			{
				query.SortBy.ReplaceAllItems(previousQuery.SortBy); //copy the SortBy info from the previousQuery
			}

			//set the query's Conditions info
			query.IdListAsString = this.txtIdList.Text.TrimToNull();
			query.Email = this.txtEmail.Text.TrimToNull();
			query.LastName = this.txtLastName.Text.TrimToNull();
			query.RoleId = this.ddlRole.SelectedValue.TryParseInt32();
			query.RowStateId = this.ddlStatus.SelectedValue.TryParseInt32();
			query.FirstName = this.txtFirstName.Text.TrimToNull();
			query.WindowsId = this.txtWindowsId.Text.TrimToNull();
			query.CreatedAfter = Global.ReadFilterValue_DateOnly(this.txtCreatedAfter);
			query.CreatedBefore = Global.ReadFilterValue_DateOnly(this.txtCreatedBefore);
			query.CreatedBy = Global.ReadFilterValue_WindowsIdFromCreatedByFilter(this.txtCreatedBy, true);
			query.ModifiedAfter = Global.ReadFilterValue_DateOnly(this.txtModifiedAfter);
			query.ModifiedBefore = Global.ReadFilterValue_DateOnly(this.txtModifiedBefore);
			query.ModifiedBy = Global.ReadFilterValue_WindowsIdFromModifiedByFilter(this.txtModifiedBy, true);
            query.TenantGroupId = this.ddlTenant.SelectedValue.TryParseInt32();
			return query;
		}

		protected override void EditItem(int index)
		{
			this.EditPerson(this.GetRowIdInt32(index));
		}

		protected override void PopulateListControls()
		{
			Global.BindRowStatusListControl(this.ddlStatus);
			this.ddlStatus.InsertItem(0, "", Global.GetAllListText());

			Global.BindRoleListControl(this.ddlRole, null);
			this.ddlRole.InsertItem(0, "", Global.GetAllListText());

            Global.BindTenantListControl(this.ddlTenant, null);
            this.ddlTenant.InsertItem(0, "", Global.GetAllListText());

		}

		protected override void ClearDataControls()
		{
			this.txtIdList.Text = string.Empty;
			this.txtLastName.Text = string.Empty;
			this.txtEmail.Text = string.Empty;
			this.ddlRole.ClearSelection();
			this.ddlStatus.ClearSelection();
			this.txtFirstName.Text = string.Empty;
			this.txtWindowsId.Text = string.Empty;
			this.txtCreatedAfter.Text = string.Empty;
			this.txtCreatedBefore.Text = string.Empty;
			this.txtCreatedBy.Text = string.Empty;
			this.txtModifiedAfter.Text = string.Empty;
			this.txtModifiedBefore.Text = string.Empty;
			this.txtModifiedBy.Text = string.Empty;
		}

		protected override void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename)
		{
			//NOTE: DO NOT invoke the base class' implementation

			//NOTE: We must choose which Controller's Fetch method to use based upon the QuerySPecification's contents 
			//(specifically based upon the whether the RoleId condition is null or not because if it is not we need to use a different DAL query)
			//Also see the similar conditional code in the BindScreen method.
			//this.odsDataSource.TypeName = "HP.ElementsCPS.Data.SubSonicClient." + ((query.RoleId == null) ? "VwMapPersonController" : "VwMapPersonRoleController");
			PersonQuerySpecification qs = this.ConvertToExpectedType(querySpecification);
			DataTable dataTable = qs.RoleId == null ? VwMapPersonController.Fetch(qs).ToDataTable() : VwMapPersonRoleController.Fetch(qs).ToDataTable();

			this.ExportData(querySpecification, exportFormat, filename, dataTable);
		}

		protected override void ExportData(System.Collections.Generic.IEnumerable<DataTable> dataTables, HP.HPFx.Extensions.Data.Export.ExportDataExtensions.ExportOptions exportOptions, string mimeType, string filename)
		{
			exportOptions.ExcludedColumns.AddRange(new[] { "rowstatusid", "rowstatusname" });
			base.ExportData(dataTables, exportOptions, mimeType, filename);
		}

		#endregion

		#endregion

		#region Properties

		#region Convenience Properties

		/// <summary>
		/// Returns the result of invoking <see cref="ConvertToExpectedType"/> on this instance's <see cref="QuerySpecification"/>.
		/// </summary>
		public PersonQuerySpecification ConvertedQuerySpecification
		{
			get
			{
				return this.ConvertToExpectedType(this.QuerySpecification);
			}
		}

		#endregion

		#endregion

		#region PageEvents

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Page_LoadHelper();
		}

		#endregion

		#region ControlEvents

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			this.EditPerson(null);
		}

		protected void btnExport_Click(object sender, EventArgs e)
		{
			const string entityName = "Person";
			string filename = Global.GenerateStandardExportFilename(entityName, this.CombinedQuerySpecification);
			this.ExportData("xls", filename, true);
		}


		protected void btnFilter_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.Grid.PageIndex = 0;
			this.ApplyScreenInput();
		}

        protected void cvTxtIdList_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Global.ValidateCommaSeparatedIdList(this.txtIdList.Text.ToString());
        }

		#endregion

		#region Methods

		#region Helper Methods

		private void EditPerson(int? recordId)
		{
			this.Response.Redirect(Global.GetPersonUpdatePageUri(recordId, this.QuerySpecification));
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public PersonQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as PersonQuerySpecification ?? new PersonQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}