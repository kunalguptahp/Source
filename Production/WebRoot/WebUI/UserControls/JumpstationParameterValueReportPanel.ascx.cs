using System;
using System.Collections.Generic;
using System.Data;
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
	public partial class JumpstationParameterValueReportPanel : UserControl
	{

		#region Properties

		public int? JumpstationGroupSelectorId { get; set; }

		#endregion

		#region PageEvents

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				ReportQueryParameterValue();
			}
		}

		private void ReportQueryParameterValue()
		{
			// fetch the group to get group type.
			JumpstationGroupSelector jumpstationGroupSelector = JumpstationGroupSelector.FetchByID(JumpstationGroupSelectorId ?? 0);
			if (jumpstationGroupSelector != null)
			{
				JumpstationGroup jumpstationGroup =
					JumpstationGroup.FetchByID(jumpstationGroupSelector.JumpstationGroupId);
				if (jumpstationGroup != null)
				{
					VwMapQueryParameterJumpstationGroupTypeCollection queryParameterCollection =
						VwMapQueryParameterJumpstationGroupTypeController.FetchByJumpstationGroupTypeId(
							jumpstationGroup.JumpstationGroupTypeId, null);
					queryParameterCollection.Sort("QueryParameterName", true);

					DataSet ds = new DataSet();
					ds.Tables.Add("queryParameterValues");
					ds.Tables[0].Columns.Add("queryParameterName");
					ds.Tables[0].Columns.Add("queryParameterValue");
					ds.Tables[0].Columns.Add("queryParameterValueNegation");

					foreach (VwMapQueryParameterJumpstationGroupType queryParameter in queryParameterCollection)
					{
						DataRow dr = ds.Tables[0].NewRow();
						dr["queryParameterName"] = queryParameter.QueryParameterName;

						List<string> queryParameterValueList = new List<string>();
						VwMapJumpstationGroupSelectorQueryParameterValueCollection queryParameterValueCollection = VwMapJumpstationGroupSelectorQueryParameterValueController.FetchByJumpstationGroupSelectorIdQueryParameterId(JumpstationGroupSelectorId ?? 0, queryParameter.QueryParameterId);
						foreach (VwMapJumpstationGroupSelectorQueryParameterValue queryParameterValue in queryParameterValueCollection)
						{
							dr["queryParameterValueNegation"] = (queryParameterValue.Negation) ? "Y" : "N";
							queryParameterValueList.Add(queryParameterValue.QueryParameterValueName);
						}

						queryParameterValueList.Sort();
						dr["queryParameterValue"] = string.Join(", ", queryParameterValueList.ToArray());
						ds.Tables[0].Rows.Add(dr);
					}

					this.repJumpstationParameterValue.DataSource = ds;
					this.repJumpstationParameterValue.DataBind();
						
				}
			}
		}

		#endregion

	}
}