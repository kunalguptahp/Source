using System;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class ViewLog : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				string logId = this.Request.Params["id"];
				using (IDisposable dataSource = Log.FetchByParameter(Log.IdColumn.ColumnName, logId))
				{
					this.dvLog.DataSource = dataSource;

					this.dvLog.DataBind();
				}
			}
		}
	}
}
