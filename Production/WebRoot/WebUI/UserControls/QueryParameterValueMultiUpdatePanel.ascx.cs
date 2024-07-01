using System;
using System.Collections.Generic;
using System.Drawing;
using System.Transactions;
using System.Text;
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
	public partial class QueryParameterValueMultiUpdatePanel : BaseListViewUserControl
	{
		#region Properties

		public bool IsNew
		{
			get
			{
				var q = this.ExtractQuerySpec();
				return q.Id == -1;
			}
		}

		#endregion

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

		private ProxyURLQuerySpecification ExtractQuerySpec()
		{
			string s = System.Web.HttpUtility.UrlDecode(Request["query"]);
			QuerySpecificationWrapper qs = null;
			if (!string.IsNullOrEmpty(s))
			{
				try
				{
					qs = ProxyURLQuerySpecification.FromXml(s);
				}
				catch (Exception)
				{
					//ignore the error
				}
			}
			return this.ConvertToExpectedType(qs ?? new ProxyURLQuerySpecification());
		}

		#region Method Overrides

		protected override void BindScreen(IQuerySpecification querySpecification)
		{
			var qs = ExtractQuerySpec();
			if (IsNew)//If in "New" status, no record should be fetch
				qs.Id = 0;
			gvList.DataSource = ProxyURLController.Fetch(qs);
			gvList.DataBind();
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			ProxyURLQuerySpecification query = new ProxyURLQuerySpecification();

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

		#endregion

		#region Properties

		#region Convenience Properties

		/// <summary>
		/// Returns the result of invoking <see cref="ConvertToExpectedType"/> on this instance's <see cref="QuerySpecification"/>.
		/// </summary>
		public ProxyURLQuerySpecification ConvertedQuerySpecification
		{
			get
			{
				return this.ConvertToExpectedType(this.QuerySpecification);
			}
		}

		#endregion

		#endregion

		#region PageEvents

		protected override void OnInit(EventArgs e)
		{
			int proxyTypeId;
			var query = this.ExtractQuerySpec();
			if (query.ProxyURLTypeId.HasValue)
				proxyTypeId = query.ProxyURLTypeId.Value;
			else
			{
				var c = ProxyURLController.Fetch(query);
				if (c.Count == 0)
				{
					return;
				}
				proxyTypeId = c[0].ProxyURLTypeId;
			}

			//Clear columns first
			GridView gv = this.gvList;
			gv.Columns.Clear();

			//Then add
			TemplateField tfRemoveFlag = new TemplateField();
			tfRemoveFlag.ItemTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.DataRow, QueryParameterValueMultiEditTemplate.REMOVE_FLAG, 0);
			tfRemoveFlag.HeaderTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.Header, "Remove", 0);
			gv.Columns.Add(tfRemoveFlag);

			TemplateField tfID = new TemplateField();
			tfID.ItemTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.DataRow, QueryParameterValueMultiEditTemplate.ID, 0);
			tfID.HeaderTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.Header, QueryParameterValueMultiEditTemplate.ID, 0);
			gv.Columns.Add(tfID);

			TemplateField tfTargetUrl = new TemplateField();
			tfTargetUrl.ItemTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.DataRow, QueryParameterValueMultiEditTemplate.TARGET_URL, 0);
			tfTargetUrl.HeaderTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.Header, QueryParameterValueMultiEditTemplate.TARGET_URL, 0);
			gv.Columns.Add(tfTargetUrl);

			QueryParameterProxyURLTypeQuerySpecification qs = new QueryParameterProxyURLTypeQuerySpecification();
			qs.ProxyURLTypeId = proxyTypeId;
			VwMapQueryParameterProxyURLTypeCollection coll = VwMapQueryParameterProxyURLTypeController.Fetch(qs);
			coll.Sort("QueryParameterName", true);
			foreach (var p in coll)
			{
				TemplateField tf = new TemplateField();
				tf.ItemTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.DataRow, p.QueryParameterName, p.QueryParameterId);
				tf.HeaderTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.Header, p.QueryParameterName, p.QueryParameterId);
				gv.Columns.Add(tf);
			}

			TemplateField tfHidden = new TemplateField();
			tfHidden.ItemTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.DataRow, QueryParameterValueMultiEditTemplate.HIDDEN, 0);
			tfHidden.HeaderTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.Header, QueryParameterValueMultiEditTemplate.HIDDEN, 0);			
			gv.Columns.Add(tfHidden);

			TemplateField tfError = new TemplateField();
			tfError.ItemTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.DataRow, QueryParameterValueMultiEditTemplate.ERROR, 0);
			tfError.HeaderTemplate = new QueryParameterValueMultiEditTemplate(DataControlRowType.Header, null, 0);
			gv.Columns.Add(tfError);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Page_LoadHelper();

			this.btnInsert.Visible = IsNew;
		}

		#endregion

		#region ControlEvents

		protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				StringBuilder sb = new StringBuilder();
				ProxyURL pu = e.Row.DataItem as ProxyURL;
				if (!pu.IsNew)
				{
					Label lblID = (Label)e.Row.FindControl(QueryParameterValueMultiEditTemplate.LBL_ID);
					lblID.Text = pu.Id.ToString();
				}

				TextBox txtTargetUrl = (TextBox)e.Row.FindControl(QueryParameterValueMultiEditTemplate.TXT_TARGET_URL);
				txtTargetUrl.Text = pu.Url;
				sb.Append(pu.Url).Append("?");

				foreach (var pv in pu.ProxyURLQueryParameterValueRecords())
				{
					string name = QueryParameterValueMultiEditTemplate.PREFIX_DDL + pv.QueryParameterValue.QueryParameter.Name;
					DropDownList ddl = (DropDownList)e.Row.FindControl(name);
					if (ddl != null)
					{
						ddl.ForceSelectedValue(pv.QueryParameterValue.Id);
						sb.AppendFormat("{0}={1}&", pv.QueryParameterValue.QueryParameter.Id, pv.QueryParameterValue.Id);
					}
				}

				Label lblHidden = (Label)e.Row.FindControl(QueryParameterValueMultiEditTemplate.LBL_HIDDEN);
				lblHidden.Text = sb.ToString().TrimEnd('&');
				lblHidden.Visible = false;
			}
		}

		#endregion

		#region Methods

		#region Helper Methods

		/// <summary>
		/// Validate all required input
		/// </summary>
		/// <returns></returns>
		private bool IsValid()
		{
			if (gvList.Rows.Count == 0)
				return false;

			bool validInput = true;

			QueryParameterProxyURLTypeQuerySpecification query = new QueryParameterProxyURLTypeQuerySpecification();
			var qs = this.ExtractQuerySpec();
			query.ProxyURLTypeId = qs.ProxyURLTypeId;

			VwMapQueryParameterProxyURLTypeCollection cols = VwMapQueryParameterProxyURLTypeController.Fetch(query);
			foreach (GridViewRow row in gvList.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					CheckBox chkRemoveFlag = row.FindControl(QueryParameterValueMultiEditTemplate.CHK_REMOVE_FLAG) as CheckBox;
					if (!chkRemoveFlag.Checked)
					{
						TextBox txt = row.FindControl(QueryParameterValueMultiEditTemplate.TXT_TARGET_URL) as TextBox;

						Label lblError = row.FindControl(QueryParameterValueMultiEditTemplate.LBL_ERROR) as Label;
						lblError.ForeColor = System.Drawing.Color.Red;
						lblError.Text = "Parameter not selected or target url is invalid.";
						lblError.Visible = false;

						if (txt.Text.Length == 0)
						{
							lblError.Visible = true;
							validInput = false;
						}
						else
						{
							Uri url;
							bool valid = (Uri.IsWellFormedUriString(txt.Text, UriKind.Absolute) && Uri.TryCreate(txt.Text, UriKind.Absolute, out url));
							if (!valid)
							{
								lblError.Visible = true;
								validInput = false;
							}
						}

						if (validInput == true)
						{
							foreach (var c in cols)
							{
								DropDownList ddl = row.FindControl(QueryParameterValueMultiEditTemplate.PREFIX_DDL + c.QueryParameterName) as DropDownList;
								if (ddl != null && !ddl.SelectedValue.TryParseInt32().HasValue)
								{
									lblError.Visible = true;
									validInput = false;
								}
							}													
						}

					}
				}
			}
			return validInput;
		}

		private Dictionary<ProxyURL, int> _map;
		public ProxyURLCollection GetProxyUrls()
		{
			_map = new Dictionary<ProxyURL, int>();
			ProxyURLCollection coll = new ProxyURLCollection();
			foreach (GridViewRow row in gvList.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					CheckBox chkRemoveFlag = row.FindControl(QueryParameterValueMultiEditTemplate.CHK_REMOVE_FLAG) as CheckBox;
					if (!chkRemoveFlag.Checked)
					{
						ProxyURL p;
						if (IsNew)
						{
							p = new ProxyURL(true);
						}
						else
						{
							int id = ((Label) row.FindControl(QueryParameterValueMultiEditTemplate.LBL_ID)).Text.TryParseInt32().Value;
							p = ProxyURL.FetchByID(id);
						}
						p.Url = ((TextBox) row.FindControl(QueryParameterValueMultiEditTemplate.TXT_TARGET_URL)).Text;
						coll.Add(p);

						_map.Add(p, row.RowIndex);
					}
				}
			}
			return coll;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public ProxyURLQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as ProxyURLQuerySpecification ?? new ProxyURLQuerySpecification(original);
		}

		#endregion

		protected void btnInsert_Click(object sender, EventArgs e)
		{
			ProxyURLCollection coll = new ProxyURLCollection();

			QueryParameterProxyURLTypeQuerySpecification qs = new QueryParameterProxyURLTypeQuerySpecification();
			qs.ProxyURLTypeId = this.ExtractQuerySpec().ProxyURLTypeId;
			VwMapQueryParameterProxyURLTypeCollection cols = VwMapQueryParameterProxyURLTypeController.Fetch(qs);

			Dictionary<ProxyURL, Dictionary<string, string>> urlQueryParameters = new Dictionary<ProxyURL, Dictionary<string, string>>();

			ProxyURL newUrl = ProxyURLController.NewRecord();
			for (int r = 0; r < gvList.Rows.Count; r++)
			{
				var row = gvList.Rows[r];
				ProxyURL pu = new ProxyURL(true);

				pu.Url = ((TextBox)(row.FindControl(QueryParameterValueMultiEditTemplate.TXT_TARGET_URL))).Text;

				Dictionary<string, string> queryParameters = new Dictionary<string, string>();
				urlQueryParameters.Add(pu, queryParameters);

				foreach (var col in cols)
				{
					string name = QueryParameterValueMultiEditTemplate.PREFIX_DDL + col.QueryParameterName;
					DropDownList ddl = (DropDownList)row.FindControl(name);
					queryParameters.Add(name, ddl.SelectedValue);
				}

				if (r == gvList.Rows.Count - 1)//This is the last record, we duplicate value to new record.
				{
					urlQueryParameters.Add(newUrl, queryParameters);
				}

				coll.Add(pu);
			}

			coll.Add(newUrl);
			this.gvList.DataSource = coll;
			this.gvList.DataBind();

			if (gvList.Rows.Count <= 1)
				return;

			for (int r = 0; r < gvList.Rows.Count; r++)
			{
				var row = gvList.Rows[r];
				ProxyURL pu = coll[r];

				((TextBox)(row.FindControl(QueryParameterValueMultiEditTemplate.TXT_TARGET_URL))).Text = pu.Url;
				foreach (var col in urlQueryParameters[pu].Keys)
				{
					DropDownList ddl = (DropDownList)row.FindControl(col);
					ddl.ForceSelectedValue(urlQueryParameters[pu][ddl.ID]);
				}
			}
		}

		#endregion

		public bool IsDataModificationAllowed
		{
			get
			{
				return true;
			}
		}

		public bool IsMetadataModificationAllowed
		{
			get
			{
				return true;
			}
		}

		private ProxyURLCollection GetProxyURLs()
		{
			var qs = this.ExtractQuerySpec();
			return ProxyURLController.Fetch(qs);
		}

		public bool IsTargetUrlModified(ProxyURL proxyUrl)
		{
			int rowIndex = _map[proxyUrl];
			GridViewRow row = gvList.Rows[rowIndex];
			Label lblHidden = (Label)row.FindControl(QueryParameterValueMultiEditTemplate.LBL_HIDDEN);
			string originalValue = lblHidden.Text;
			int idx = originalValue.LastIndexOf('?');
			string url = originalValue.Substring(0, idx);
			return url != proxyUrl.Url;
		}

		/// <summary>
		/// Return changed(edited) parameter only
		/// </summary>
		/// <param name="proxyUrl">the ProxyUrl item</param>
		/// <returns>Changed parameter put in a dictionary</returns>
		public Dictionary<int, int> GetParameterList(ProxyURL proxyUrl)
		{
			QueryParameterProxyURLTypeQuerySpecification qs = new QueryParameterProxyURLTypeQuerySpecification();
			qs.ProxyURLTypeId = proxyUrl.ProxyURLTypeId;
			VwMapQueryParameterProxyURLTypeCollection cols = VwMapQueryParameterProxyURLTypeController.Fetch(qs);

			int rowIndex = _map[proxyUrl];
			GridViewRow row = gvList.Rows[rowIndex];
			Dictionary<int, int> paramValues = new Dictionary<int, int>();

			Label lblHidden = (Label)row.FindControl(QueryParameterValueMultiEditTemplate.LBL_HIDDEN);
			string originalValue = lblHidden.Text;

			foreach (var c in cols)
			{
				DropDownList ddl = (DropDownList)row.FindControl(QueryParameterValueMultiEditTemplate.PREFIX_DDL + c.QueryParameterName);

				//Note:Comare with hidden to see if it is changed, if yes, put it into return map.
				if (IsNew)
				{
					paramValues.Add(c.QueryParameterId, ddl.SelectedValue.TryParseInt32() ?? 0);
				}
				else
				{
					int originId = GetValueFromString(originalValue, c.QueryParameterId);
					if (originId != (ddl.SelectedValue.TryParseInt32() ?? 0))
						paramValues.Add(c.QueryParameterId, ddl.SelectedValue.TryParseInt32() ?? 0);
				}
			}

			return paramValues;
		}

		private int GetValueFromString(string origin, int paramId)
		{
			int idx = origin.LastIndexOf('?');
			string query = origin.Substring(idx + 1);
			string[] paramPairs = query.Split('&');
			foreach (string p in paramPairs)
			{
				string[] pair = p.Split('=');
				if (int.Parse(pair[0].Trim()) == paramId)
					return int.Parse(pair[1].Trim());
			}
			return 0;
		}

		protected void cvQueryParameterValue_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = IsValid();
		}
	}
}