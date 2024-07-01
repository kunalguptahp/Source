using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class WorkflowModuleSubCategoryUpdate : BaseUpdatePage
	{
		#region BaseRecordUpdatePage Abstract Members

		protected override RecordDetailUserControl RecordDetailControl
		{
			get
			{
				return this.ucDetail;
			}
		}

		protected override void RedirectToUpdatePage(int? dataSourceId)
		{
			this.Response.Redirect(Global.GetWorkflowModuleSubCategoryUpdatePageUri(dataSourceId, null));
		}

		protected override string GeneratePageTitle()
		{
			int? dataSourceId = this.DataSourceId;
			return (dataSourceId == null) ? "New Module Sub-Category" : string.Format("Update Module Sub-Category #{0}", dataSourceId.Value);
		}

		#endregion
	}
}