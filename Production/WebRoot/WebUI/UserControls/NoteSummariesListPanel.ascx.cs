using System;
using System.Data;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
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
	public partial class NoteSummariesListPanel : BaseSummariesListUserControl
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

			NoteQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			NoteQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);

			//change the ODS's DAL target based upon the whether the RoleId condition is null or not (because if it is not we need to use a different DAL query)			

			//Configure the ObjectDataSource using the current QuerySpecification
			Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
			querySpecificationParameter.DefaultValue = query.ToString();

			//read the query's Conditions info

			this.BindScreen_GridPaging(query);

			//update the URL of the "open popup as page" link based upon the new DataBinding context
			this.lnkDirectAccessLink.NavigateUrl = Global.GetNoteListPageUri(querySpecification);
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			NoteQuerySpecification query = new NoteQuerySpecification();
			GridView gv = this.Grid;

			//NOTE: DO NOT update the sorting and paging here _from the GridView_, because that is done in the GridView's RowCommand handler
			//NOTE: However, DO preserve the existing sort order (if any) _from the previous query_ to preserve the sort order when the filter conditions are changed
			IQuerySpecification previousQuery = this.QuerySpecification;
			if ((previousQuery != null) && (previousQuery.SortBy != null) && (previousQuery.SortBy.Count > 0))
			{
				query.SortBy.ReplaceAllItems(previousQuery.SortBy); //copy the SortBy info from the previousQuery
			}

			//set the query's Conditions info
			
			return query;
		}

		protected override void EditItem(int index)
		{
			this.EditNote(this.GetRowIdInt32(index));
		}

		#endregion

		#endregion

		#region Properties

		#region Convenience Properties

		/// <summary>
		/// Returns the result of invoking <see cref="ConvertToExpectedType"/> on this instance's <see cref="QuerySpecification"/>.
		/// </summary>
		public NoteQuerySpecification ConvertedQuerySpecification
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
			this.EditNote(null);
		}

		protected void btnExport_Click(object sender, EventArgs e)
		{
			const string entityName = "Notes";
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

		#endregion

		#region Methods

		#region Helper Methods

		private void EditNote(int? recordId)
		{
			this.Response.Redirect(Global.GetNoteUpdatePageUri(recordId, this.QuerySpecification));
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public NoteQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as NoteQuerySpecification ?? new NoteQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}
