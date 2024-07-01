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
	public partial class ConfigurationServiceGroupDescriptionMultiReplaceUpdatePanel : BaseListViewUserControl
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

			gvList.DataSource = ConfigurationServiceGroupController.Fetch(qs);
			gvList.DataBind();
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			ConfigurationServiceGroupQuerySpecification query = new ConfigurationServiceGroupQuerySpecification();

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
			tfID.ItemTemplate = new ConfigurationServiceGroupMultiReplaceTemplate(DataControlRowType.DataRow, ConfigurationServiceGroupMultiReplaceTemplate.ID, 0);
			tfID.HeaderTemplate = new ConfigurationServiceGroupMultiReplaceTemplate(DataControlRowType.Header, ConfigurationServiceGroupMultiReplaceTemplate.ID, 0);
			gv.Columns.Add(tfID);

            TemplateField tfName = new TemplateField();
            tfName.ItemTemplate = new ConfigurationServiceGroupMultiReplaceTemplate(DataControlRowType.DataRow, ConfigurationServiceGroupMultiReplaceTemplate.NAME, 0);
            tfName.HeaderTemplate = new ConfigurationServiceGroupMultiReplaceTemplate(DataControlRowType.Header, ConfigurationServiceGroupMultiReplaceTemplate.NAME, 0);
            gv.Columns.Add(tfName);

			TemplateField tfDesc = new TemplateField();
			tfDesc.ItemTemplate = new ConfigurationServiceGroupMultiReplaceTemplate(DataControlRowType.DataRow, ConfigurationServiceGroupMultiReplaceTemplate.DESCRIPTION, 0);
			tfDesc.HeaderTemplate = new ConfigurationServiceGroupMultiReplaceTemplate(DataControlRowType.Header, ConfigurationServiceGroupMultiReplaceTemplate.DESCRIPTION, 0);
			gv.Columns.Add(tfDesc);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Page_LoadHelper();
		}

		#endregion

		#region ControlEvents

		protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				ConfigurationServiceGroup pu = e.Row.DataItem as ConfigurationServiceGroup;
				if (!pu.IsNew)
				{
					Label lblID = (Label)e.Row.FindControl(ConfigurationServiceGroupMultiReplaceTemplate.LBL_ID);
					lblID.Text = pu.Id.ToString();
				}

                TextBox txtName = (TextBox)e.Row.FindControl(ConfigurationServiceGroupMultiReplaceTemplate.TXT_NAME);
                txtName.MaxLength = 256;
                txtName.Text = pu.Name;

				TextBox txtDesc = (TextBox)e.Row.FindControl(ConfigurationServiceGroupMultiReplaceTemplate.TXT_DESCRIPTION);
                txtDesc.MaxLength = 512;
				txtDesc.Text = pu.Description;
			}
		}

		#endregion

		#region Helper Methods

		private Dictionary<ConfigurationServiceGroup, int> _map;
		public ConfigurationServiceGroupCollection GetConfigurationServiceGroups()
		{
			_map = new Dictionary<ConfigurationServiceGroup, int>();
			ConfigurationServiceGroupCollection coll = new ConfigurationServiceGroupCollection();
			foreach (GridViewRow row in gvList.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					int id = ((Label)row.FindControl(ConfigurationServiceGroupMultiReplaceTemplate.LBL_ID)).Text.TryParseInt32().Value;

					ConfigurationServiceGroup configurationServiceGroup = ConfigurationServiceGroup.FetchByID(id);
					configurationServiceGroup.Name = ((TextBox)row.FindControl(ConfigurationServiceGroupMultiReplaceTemplate.TXT_NAME)).Text;
                    configurationServiceGroup.Description = ((TextBox)row.FindControl(ConfigurationServiceGroupMultiReplaceTemplate.TXT_DESCRIPTION)).Text;
                    coll.Add(configurationServiceGroup);

					_map.Add(configurationServiceGroup, row.RowIndex);
				}
			}
			return coll;
		}

        public String GetName(ConfigurationServiceGroup saveItem)
        {
            int rowIndex = _map[saveItem];
            GridViewRow row = gvList.Rows[rowIndex];
            TextBox txtName = (TextBox)row.FindControl(ConfigurationServiceGroupMultiReplaceTemplate.TXT_NAME);
            return txtName.Text;
        }

		public String GetDescription(ConfigurationServiceGroup saveItem)
		{
			int rowIndex = _map[saveItem];
			GridViewRow row = gvList.Rows[rowIndex];
			TextBox txtDescription = (TextBox)row.FindControl(ConfigurationServiceGroupMultiReplaceTemplate.TXT_DESCRIPTION);
			return txtDescription.Text;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public ConfigurationServiceGroupQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as ConfigurationServiceGroupQuerySpecification ?? new ConfigurationServiceGroupQuerySpecification(original);
		}

		private ConfigurationServiceGroupQuerySpecification ExtractQuerySpec()
		{
			string s = System.Web.HttpUtility.UrlDecode(Request["query"]);
			if (string.IsNullOrEmpty(s))
				return null;
			return this.ConvertToExpectedType(ConfigurationServiceGroupQuerySpecification.FromXml(s));
		}
	
		#endregion

	}
}