using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.WebControlEnhancements;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class JumpstationGroupDescriptionMultiReplaceUpdatePanel : BaseListViewUserControl
	{

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
			var qs = ExtractQuerySpec();
			if (qs == null)
				return;

			gvList.DataSource = VwMapJumpstationGroupController.Fetch(qs);
			gvList.DataBind();
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			JumpstationGroupQuerySpecification query = new JumpstationGroupQuerySpecification();

			//NOTE: DO NOT update the sorting and paging here _from the GridView_, because that is done in the GridView's RowCommand handler
			//NOTE: However, DO preserve the existing sort order (if any) _from the previous query_ to preserve the sort order when the filter conditions are changed
			IQuerySpecification previousQuery = this.QuerySpecification;

			if ((previousQuery != null) && (previousQuery.SortBy != null) && (previousQuery.SortBy.Count > 0))
			{
				query.SortBy.ReplaceAllItems(previousQuery.SortBy); //copy the SortBy info from the previousQuery
			}

			return query;
		}

		protected override void EditItem(int index)
		{
			throw new System.NotSupportedException();
		}

		protected override void PopulateListControls()
		{
			// Do nothing
		}

		protected override void ClearDataControls()
		{
			// Do nothing
		}

		#endregion

		#region PageEvents

		protected override void OnInit(EventArgs e)
		{
			//Clear columns first
			GridView gv = this.gvList;
			gv.Columns.Clear();

			//Then add
			TemplateField tfID = new TemplateField();
			tfID.ItemTemplate = new JumpstationGroupMultiReplaceTemplate(DataControlRowType.DataRow, JumpstationGroupMultiReplaceTemplate.ID, 0);
			tfID.HeaderTemplate = new JumpstationGroupMultiReplaceTemplate(DataControlRowType.Header, JumpstationGroupMultiReplaceTemplate.ID, 0);
			gv.Columns.Add(tfID);

            TemplateField tfTargetUrl = new TemplateField();
            tfTargetUrl.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.TARGET_URL);
            tfTargetUrl.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.TARGET_URL);
            gv.Columns.Add(tfTargetUrl);

            TemplateField tfDesc = new TemplateField();
            tfDesc.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.DESCRIPTION);
            tfDesc.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.DESCRIPTION);
            gv.Columns.Add(tfDesc);

            TemplateField tfType = new TemplateField();
            tfType.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.REDIRECTOR_TYPE);
            tfType.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.REDIRECTOR_TYPE);
            gv.Columns.Add(tfType);

            TemplateField tfBrand = new TemplateField();
            tfBrand.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.BRAND_PARAMETER_VALUE);
            tfBrand.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.BRAND_PARAMETER_VALUE);
            gv.Columns.Add(tfBrand);

            TemplateField tfCycle = new TemplateField();
            tfCycle.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.CYCLE_PARAMETER_VALUE);
            tfCycle.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.CYCLE_PARAMETER_VALUE);
            gv.Columns.Add(tfCycle);

            TemplateField tfLocale = new TemplateField();
            tfLocale.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.LOCALE_PARAMETER_VALUE);
            tfLocale.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.LOCALE_PARAMETER_VALUE);
            gv.Columns.Add(tfLocale);

            TemplateField tfPartnerCategory = new TemplateField();
            tfPartnerCategory.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.PARTNER_CATEGORY_PARAMETER_VALUE);
            tfPartnerCategory.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.PARTNER_CATEGORY_PARAMETER_VALUE);
            gv.Columns.Add(tfPartnerCategory);

            TemplateField tfPlatform = new TemplateField();
            tfPlatform.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.PLATFORM_PARAMETER_VALUE);
            tfPlatform.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.PLATFORM_PARAMETER_VALUE);
            gv.Columns.Add(tfPlatform);

            TemplateField tfTouchpoint = new TemplateField();
            tfTouchpoint.ItemTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.DataRow, ProxyURLMultiReplaceTemplate.TOUCHPOINT_PARAMETER_VALUE);
            tfTouchpoint.HeaderTemplate = new ProxyURLMultiReplaceTemplate(DataControlRowType.Header, ProxyURLMultiReplaceTemplate.TOUCHPOINT_PARAMETER_VALUE);
            gv.Columns.Add(tfTouchpoint);
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Page_LoadHelper();
		}

        protected void cvQueryParameterValue_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = IsValid();
        }

		#endregion

		#region ControlEvents

		protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				VwMapJumpstationGroup pu = e.Row.DataItem as VwMapJumpstationGroup;
				if (!pu.IsNew)
				{
					Label lblID = (Label)e.Row.FindControl(JumpstationGroupMultiReplaceTemplate.LBL_ID);
					lblID.Text = pu.Id.ToString();
				}

                TextBox txtTargetUrl = (TextBox)e.Row.FindControl(ProxyURLMultiReplaceTemplate.TXT_TARGET_URL);
                txtTargetUrl.Text = pu.TargetURL;

                TextBox txtDesc = (TextBox)e.Row.FindControl(ProxyURLMultiReplaceTemplate.TXT_DESCRIPTION);
                txtDesc.Text = pu.Description;

                Label lblRedirectorType = (Label)e.Row.FindControl(ProxyURLMultiReplaceTemplate.LBL_REDIRECTOR_TYPE);
                lblRedirectorType.Text = pu.JumpstationGroupTypeName;

                Label lblBrandParameterValue = (Label)e.Row.FindControl(ProxyURLMultiReplaceTemplate.LBL_BRAND_PARAMETER_VALUE);
                lblBrandParameterValue.Text = pu.BrandQueryParameterValue;

                Label lblCycleParameterValue = (Label)e.Row.FindControl(ProxyURLMultiReplaceTemplate.LBL_CYCLE_PARAMETER_VALUE);
                lblCycleParameterValue.Text = pu.CycleQueryParameterValue;

                Label lblLocaleParameterValue = (Label)e.Row.FindControl(ProxyURLMultiReplaceTemplate.LBL_LOCALE_PARAMETER_VALUE);
                lblLocaleParameterValue.Text = pu.LocaleQueryParameterValue;

                Label lblPartnerCategoryParameterValue = (Label)e.Row.FindControl(ProxyURLMultiReplaceTemplate.LBL_PARTNER_CATEGORY_PARAMETER_VALUE);
                lblPartnerCategoryParameterValue.Text = pu.PartnerCategoryQueryParameterValue;

                Label lblPlatformParameterValue = (Label)e.Row.FindControl(ProxyURLMultiReplaceTemplate.LBL_PLATFORM_PARAMETER_VALUE);
                lblPlatformParameterValue.Text = pu.PlatformQueryParameterValue;

                Label lblTouchpointParameterValue = (Label)e.Row.FindControl(ProxyURLMultiReplaceTemplate.LBL_TOUCHPOINT_PARAMETER_VALUE);
                lblTouchpointParameterValue.Text = pu.TouchpointQueryParameterValue;
            }
		}

		#endregion

		#region Helper Methods

        /// <summary>
        /// Validate all required input
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            foreach (GridViewRow row in gvList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = row.FindControl(QueryParameterValueMultiEditTemplate.TXT_TARGET_URL) as TextBox;
                    Uri url;
                    bool valid = Uri.IsWellFormedUriString(txt.Text, UriKind.Absolute) && Uri.TryCreate(txt.Text, UriKind.Absolute, out url);
                    if (!valid)
                        return false;
                }
            }
            return true;
        }

		private Dictionary<JumpstationGroup, int> _map;
		public JumpstationGroupCollection GetJumpstationGroups()
		{
			_map = new Dictionary<JumpstationGroup, int>();
			JumpstationGroupCollection coll = new JumpstationGroupCollection();
			foreach (GridViewRow row in gvList.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					int id = ((Label)row.FindControl(JumpstationGroupMultiReplaceTemplate.LBL_ID)).Text.TryParseInt32().Value;

					JumpstationGroup jumpstationGroup = JumpstationGroup.FetchByID(id);
					jumpstationGroup.Description = ((TextBox)row.FindControl(JumpstationGroupMultiReplaceTemplate.TXT_DESCRIPTION)).Text;
					coll.Add(jumpstationGroup);

					_map.Add(jumpstationGroup, row.RowIndex);
				}
			}
			return coll;
		}

        public String GetTargetURL(JumpstationGroup saveItem)
        {
            int rowIndex = _map[saveItem];
            GridViewRow row = gvList.Rows[rowIndex];

            TextBox txtTargetURL = (TextBox)row.FindControl(ProxyURLMultiReplaceTemplate.TXT_TARGET_URL);
            return txtTargetURL.Text;
        }

		public String GetDescription(JumpstationGroup saveItem)
		{
			int rowIndex = _map[saveItem];
			GridViewRow row = gvList.Rows[rowIndex];
			TextBox txtDescription = (TextBox)row.FindControl(JumpstationGroupMultiReplaceTemplate.TXT_DESCRIPTION);
			return txtDescription.Text;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public JumpstationGroupQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as JumpstationGroupQuerySpecification ?? new JumpstationGroupQuerySpecification(original);
		}

		private JumpstationGroupQuerySpecification ExtractQuerySpec()
		{
			string s = System.Web.HttpUtility.UrlDecode(Request["query"]);
			if (string.IsNullOrEmpty(s))
				return null;
			return this.ConvertToExpectedType(JumpstationGroupQuerySpecification.FromXml(s));
		}
	
		#endregion

	}
}