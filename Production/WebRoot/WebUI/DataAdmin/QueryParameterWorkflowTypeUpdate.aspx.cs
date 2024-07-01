using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class QueryParameterWorkflowTypeUpdate : BaseUpdatePage
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
			this.Response.Redirect(Global.GetQueryParameterWorkflowTypeUpdatePageUri(dataSourceId, null));
		}

		protected override string GeneratePageTitle()
		{
			int? dataSourceId = this.DataSourceId;
			return (dataSourceId == null) ? "New Parameter/Workflow Type" : string.Format("Update Parameter/Workflow Type #{0}", dataSourceId.Value);
		}

		#endregion
	}
}