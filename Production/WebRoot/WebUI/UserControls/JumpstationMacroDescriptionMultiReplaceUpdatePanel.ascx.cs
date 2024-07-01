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
    public partial class JumpstationMacroDescriptionMultiReplaceUpdatePanel : BaseListViewUserControl
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

            gvList.DataSource = JumpstationMacroController.Fetch(qs);
			gvList.DataBind();
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
            JumpstationMacroQuerySpecification query = new JumpstationMacroQuerySpecification();

			//NOTE: DO NOT update the sorting and paging here _from the GridView_, because that is done in the GridView's RowCommand handler
			//NOTE: However, DO preserve the existing sort order (if any) _from the previous query_ to preserve the sort order when the filter
            //conditions are changed
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
            tfID.ItemTemplate = new JumpstationMacroMultiReplaceTemplate(DataControlRowType.DataRow, JumpstationMacroMultiReplaceTemplate.ID, 0);
            tfID.HeaderTemplate = new JumpstationMacroMultiReplaceTemplate(DataControlRowType.Header, JumpstationMacroMultiReplaceTemplate.ID, 0);
			gv.Columns.Add(tfID);

            TemplateField tfName = new TemplateField();
            tfName.ItemTemplate = new JumpstationMacroMultiReplaceTemplate(DataControlRowType.DataRow, JumpstationMacroMultiReplaceTemplate.NAME, 0);
            tfName.HeaderTemplate = new JumpstationMacroMultiReplaceTemplate(DataControlRowType.Header, JumpstationMacroMultiReplaceTemplate.NAME, 0);
            gv.Columns.Add(tfName);

			TemplateField tfDesc = new TemplateField();
            tfDesc.ItemTemplate = new JumpstationMacroMultiReplaceTemplate(DataControlRowType.DataRow, JumpstationMacroMultiReplaceTemplate.DESCRIPTION, 0);
            tfDesc.HeaderTemplate = new JumpstationMacroMultiReplaceTemplate(DataControlRowType.Header, JumpstationMacroMultiReplaceTemplate.DESCRIPTION, 0);
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
                JumpstationMacro pu = e.Row.DataItem as JumpstationMacro;
				if (!pu.IsNew)
				{
                    Label lblID = (Label)e.Row.FindControl(JumpstationMacroMultiReplaceTemplate.LBL_ID);
					lblID.Text = pu.Id.ToString();
				}

                Label lblName = (Label)e.Row.FindControl(JumpstationMacroMultiReplaceTemplate.LBL_NAME);
                lblName.Text = pu.Name;

                TextBox txtDesc = (TextBox)e.Row.FindControl(JumpstationMacroMultiReplaceTemplate.TXT_DESCRIPTION);
                txtDesc.MaxLength = 512;
                txtDesc.Text = pu.Description;
			}
		}

		#endregion

		#region Helper Methods

        private Dictionary<JumpstationMacro, int> _map;
        public JumpstationMacroCollection GetJumpstationMacros()
		{
            _map = new Dictionary<JumpstationMacro, int>();
            JumpstationMacroCollection coll = new JumpstationMacroCollection();
			foreach (GridViewRow row in gvList.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
                    int id = ((Label)row.FindControl(JumpstationMacroMultiReplaceTemplate.LBL_ID)).Text.TryParseInt32().Value;

                    JumpstationMacro macro = JumpstationMacro.FetchByID(id);
                    macro.Name = ((Label)row.FindControl(JumpstationMacroMultiReplaceTemplate.LBL_NAME)).Text;
                    macro.Description = ((TextBox)row.FindControl(JumpstationMacroMultiReplaceTemplate.TXT_DESCRIPTION)).Text;
					coll.Add(macro);

					_map.Add(macro, row.RowIndex);
				}
			}
			return coll;
		}

        public String GetDescription(JumpstationMacro saveItem)
		{
			int rowIndex = _map[saveItem];
			GridViewRow row = gvList.Rows[rowIndex];
            TextBox txtDescription = (TextBox)row.FindControl(JumpstationMacroMultiReplaceTemplate.TXT_DESCRIPTION);
			return txtDescription.Text;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
        public JumpstationMacroQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

            return original as JumpstationMacroQuerySpecification ?? new JumpstationMacroQuerySpecification(original);
		}

        private JumpstationMacroQuerySpecification ExtractQuerySpec()
		{
			string s = System.Web.HttpUtility.UrlDecode(Request["query"]);
			if (string.IsNullOrEmpty(s))
				return null;
            return this.ConvertToExpectedType(JumpstationMacroQuerySpecification.FromXml(s));
		}
	
		#endregion

	}
}