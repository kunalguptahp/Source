using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.WebControlEnhancements;
using HP.HPFx.Utility.SubSonic;
using HP.HPFx.Web.Utility;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using SubSonic;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class LogListPanel : BaseListViewUserControl
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
			//this.UnbindList();

			LogQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			LogQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
			Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
			querySpecificationParameter.DefaultValue = query.ToString();

			//read the query's Conditions info
			this.ddlSeverity.DisableIf(immutableConditions.MinSeverity != null);
			if (query.MinSeverity == null)
			{
				ddlSeverity.ClearSelection();
			}
			else
			{
				ddlSeverity.SelectedValue = query.MinSeverity.ToString();
			}

			this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
			this.txtIdList.Text = query.IdListAsString;

			this.txtUserIdentity.DisableIf(!string.IsNullOrEmpty(immutableConditions.UserIdentity));
			this.txtUserIdentity.Text = query.UserIdentity;

			this.txtLogger.DisableIf(!string.IsNullOrEmpty(immutableConditions.Logger));
			this.txtLogger.Text = query.Logger;

			this.txtWebSessionId.DisableIf(!string.IsNullOrEmpty(immutableConditions.WebSessionId));
			this.txtWebSessionId.Text = query.WebSessionId;

			this.txtCreatedAfter.DisableIf(immutableConditions.CreatedAfter != null);
			Global.InitializeFilter_DateOnly(this.txtCreatedAfter, query.CreatedAfter);

			this.txtCreatedBefore.DisableIf(immutableConditions.CreatedBefore != null);
			Global.InitializeFilter_DateOnly(this.txtCreatedBefore, query.CreatedBefore);

			this.txtDateAfter.DisableIf(immutableConditions.DateAfter != null);
			Global.InitializeFilter_DateOnly(this.txtDateAfter, query.DateAfter);

			this.txtDateBefore.DisableIf(immutableConditions.DateBefore != null);
			Global.InitializeFilter_DateOnly(this.txtDateBefore, query.DateBefore);

			this.txtUtcDateAfter.DisableIf(immutableConditions.UtcDateAfter != null);
			Global.InitializeFilter_DateOnly(this.txtUtcDateAfter, query.UtcDateAfter);

			this.txtUtcDateBefore.DisableIf(immutableConditions.UtcDateBefore != null);
			Global.InitializeFilter_DateOnly(this.txtUtcDateBefore, query.UtcDateBefore);

			this.txtUserName.DisableIf(!string.IsNullOrEmpty(immutableConditions.UserName));
			this.txtUserName.Text = query.UserName;

			this.txtUserWebIdentity.DisableIf(!string.IsNullOrEmpty(immutableConditions.UserWebIdentity));
			this.txtUserWebIdentity.Text = query.UserWebIdentity;

			this.txtProcessUser.DisableIf(!string.IsNullOrEmpty(immutableConditions.ProcessUser));
			this.txtProcessUser.Text = query.ProcessUser;

			this.txtLocation.DisableIf(!string.IsNullOrEmpty(immutableConditions.Location));
			this.txtLocation.Text = query.Location;

			this.txtMachineName.DisableIf(!string.IsNullOrEmpty(immutableConditions.MachineName));
			this.txtMachineName.Text = query.MachineName;

			this.cbOnlyExceptions.DisableIf(immutableConditions.OnlyExceptions != null);
			this.cbOnlyExceptions.Checked = query.OnlyExceptions??false;

			this.BindScreen_GridPaging(query);
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			LogQuerySpecification query = new LogQuerySpecification();
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
			query.MinSeverity = this.ddlSeverity.SelectedValue.TryParseInt32();
			query.UserIdentity = this.txtUserIdentity.Text.TrimToNull();
			query.Logger = this.txtLogger.Text.TrimToNull();
			query.WebSessionId = this.txtWebSessionId.Text.TrimToNull();
			query.CreatedAfter = Global.ReadFilterValue_DateOnly(this.txtCreatedAfter);
			query.CreatedBefore = Global.ReadFilterValue_DateOnly(this.txtCreatedBefore);
			query.DateAfter = Global.ReadFilterValue_DateOnly(this.txtDateAfter);
			query.DateBefore = Global.ReadFilterValue_DateOnly(this.txtDateBefore);
			query.UtcDateAfter = Global.ReadFilterValue_DateOnly(this.txtUtcDateAfter);
			query.UtcDateBefore = Global.ReadFilterValue_DateOnly(this.txtUtcDateBefore);
			query.UserName = this.txtUserName.Text.TrimToNull();
			query.UserWebIdentity = this.txtUserWebIdentity.Text.TrimToNull();
			query.ProcessUser = this.txtProcessUser.Text.TrimToNull();
			query.Location = this.txtLocation.Text.TrimToNull();
			query.MachineName = this.txtMachineName.Text.TrimToNull();
			query.OnlyExceptions = this.cbOnlyExceptions.Checked;
			if(!query.OnlyExceptions.Value)
			{
				query.OnlyExceptions = null; // Force a blank checkbox to null in the query spec
			}

			return query;
		}

		protected override void EditItem(int index)
		{
			this.EditLog(this.GetRowIdInt32(index));
		}

		protected override void PopulateListControls()
		{
			Global.BindLogSeverityListControl(ddlSeverity);
			ddlSeverity.InsertItem(0, "", Global.GetAllListText());
		}

		protected override void ClearDataControls()
		{
			this.txtIdList.Text = string.Empty;
			this.ddlSeverity.ClearSelection();
			this.txtUserIdentity.Text = string.Empty;
			this.txtLogger.Text = string.Empty;
			this.txtWebSessionId.Text = string.Empty;
			this.txtCreatedAfter.Text = string.Empty;
			this.txtCreatedBefore.Text = string.Empty;
			this.txtDateAfter.Text = string.Empty;
			this.txtDateBefore.Text = string.Empty;
			this.txtUtcDateAfter.Text = string.Empty;
			this.txtUtcDateBefore.Text = string.Empty;
			this.txtUserName.Text = string.Empty;
			this.txtUserWebIdentity.Text = string.Empty;
			this.txtProcessUser.Text = string.Empty;
			this.txtLocation.Text = string.Empty;
			this.txtMachineName.Text = string.Empty;
			this.cbOnlyExceptions.Checked = false;
		}

		protected override void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename)
		{
			//NOTE: DO NOT invoke the base class' implementation
			this.ExportData(querySpecification, exportFormat, filename, Global.YieldDataTablesForDataExport<Log, LogCollection, LogQuerySpecification>(querySpecification, LogController.Fetch));
		}

		#endregion

		#endregion

		#region Properties

		#region Convenience Properties

		/// <summary>
		/// Returns the result of invoking <see cref="ConvertToExpectedType"/> on this instance's <see cref="QuerySpecification"/>.
		/// </summary>
		public LogQuerySpecification ConvertedQuerySpecification
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
			this.RegisterExportControl("btnExportPage");
		}

		#endregion

		#region ControlEvents

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			this.EditLog(null);
		}

		protected void btnExport_Click(object sender, EventArgs e)
		{
			const string entityName = "Logs";
			string filename = Global.GenerateStandardExportFilename(entityName, this.CombinedQuerySpecification);
			this.ExportData("xls", filename, true);
		}

		protected void btnExportPage_Click(object sender, EventArgs e)
		{
			const string entityName = "Logs";
			string filename = Global.GenerateStandardExportFilename(entityName, this.CombinedQuerySpecification);
			bool withoutPagination = false; //NOTE: Due to the abnormally large size of Log data, we DON'T ignore the paging criteria when exporting
			this.ExportData("xls", filename, withoutPagination);
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

		#endregion

		#region Methods

		#region Helper Methods

		private void EditLog(int? recordId)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public LogQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as LogQuerySpecification ?? new LogQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}