using System;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.SqlClient;
using HP.ElementsCPS.Core.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class WorkflowModuleConditionReportPanel : UserControl
	{

		#region Properties

		public int? WorkflowModuleId { get; set; }

		#endregion

		#region PageEvents

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				VwMapWorkflowModuleWorkflowConditionCollection workflowModuleConditionCollection = VwMapWorkflowModuleWorkflowConditionController.FetchByWorkflowModuleId(WorkflowModuleId ?? 0);
				this.repWorkflowModuleCondition.DataSource = workflowModuleConditionCollection;
				this.repWorkflowModuleCondition.DataBind();
			}
		}

		#endregion

	}
}