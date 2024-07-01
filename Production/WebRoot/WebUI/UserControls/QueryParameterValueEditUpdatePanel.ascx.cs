using System;
using System.Data.SqlClient;
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
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class QueryParameterValueEditUpdatePanel : BaseListViewUserControl
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
			QueryParameterProxyURLTypeQuerySpecification query = this.ConvertToExpectedType(querySpecification);

            // default sort column
            if (query.SortBy.Count == 0)
            {
                // default to QueryValueName.
                query.SortBy.PromoteToPrimary("QueryParameterName ASC");
            }

			GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
			Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
			querySpecificationParameter.DefaultValue = query.ToString();

			//read the query's Paging.PageSize info
			gv.PageSize = query.Paging.PageSize ?? QueryPaginationSpecification.DefaultPageSize;

			//read the query's Paging.PageIndex info last (since sorting resets the PageIndex to 0)
			int pageIndex = query.Paging.PageIndex ?? 0;
			if (gv.PageIndex != pageIndex)
			{
				gv.PageIndex = pageIndex;
			}
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			// Do nothing
			return null;
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

		#region PageEvents

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
				DropDownList ddlParameterValue = (DropDownList) e.Row.FindControl("ddlParameterValue");
				Global.BindQueryParameterValueListControl(ddlParameterValue, Convert.ToInt32(e.Row.Cells[0].Text), RowStatus.RowStatusId.Active);
				ddlParameterValue.InsertItem(0, "", Global.GetSelectListText());

				QueryParameterProxyURLTypeQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);
				int? proxyURLId = GetProxyURLIdOfParent();
				if (proxyURLId != null)
				{
					ProxyURLQueryParameterValueQuerySpecification proxyURLParameterValueQuery = new ProxyURLQueryParameterValueQuerySpecification() { QueryParameterId = (int?)Convert.ToInt32(e.Row.Cells[0].Text), ProxyURLId = proxyURLId};
					VwMapProxyURLQueryParameterValueCollection coll = VwMapProxyURLQueryParameterValueController.Fetch(proxyURLParameterValueQuery);
					if (coll.Count == 1)
					{
						ddlParameterValue.ForceSelectedValue(coll[0].QueryParameterValueId);
					}
				}
			}
		}

		private int? GetProxyURLIdOfParent()
		{
			RecordDetailUserControl parentRecordDetailUserControl = Global.GetParentRecordDetailUserControl(this);
			return (parentRecordDetailUserControl == null) ? null : parentRecordDetailUserControl.DataSourceId;
		}


		#endregion

		#region Methods

		#region Helper Methods

		/// <summary>
		/// Validate all required input
		/// </summary>
		/// <returns></returns>
		public Boolean IsValid()
		{
			foreach (GridViewRow row in gvList.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					DropDownList ddlParameterValue = (DropDownList)row.FindControl("ddlParameterValue");
					if (ddlParameterValue.SelectedValue == "")
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Save Input
		/// </summary>
		/// <returns></returns>
		public void SaveInput(int proxyURLId)
		{
			this.Page.Validate("QueryParameterValueRequired");
			if (this.Page.IsValid == true)
			{
				SaveQueryParameterValue(proxyURLId);
			}
		}

		/// <summary>
		/// Multiple Save Input
		/// </summary>
		/// <returns></returns>
		public bool MultipleSaveInput(int proxyURLId)
		{
			// There must be at least one selected value to save the query value parameters.
			bool updateFlag = false;
			foreach (GridViewRow row in gvList.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					DropDownList ddlParameterValue = (DropDownList)row.FindControl("ddlParameterValue");
					if (ddlParameterValue.SelectedValue != "")
					{
						SaveQueryParameterValue(proxyURLId);
						updateFlag = true;
					}
				}
			}
			return updateFlag;
		}

		/// <summary>
		/// Create query parameter value delimited list
		/// </summary>
		/// <returns></returns>
		public string CreateQueryParameterValueIdDelimitedList()
		{
			string delimitedList = null;

			// There must be at least one selected value to save the query value parameters.
			foreach (GridViewRow row in gvList.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					DropDownList ddlParameterValue = (DropDownList)row.FindControl("ddlParameterValue");
					if (ddlParameterValue.SelectedValue != "")
					{
						delimitedList += (delimitedList == null) ? ddlParameterValue.SelectedValue : "," + ddlParameterValue.SelectedValue;
					}
				}
			}
			return delimitedList;
		}

		/// <summary>
		/// Get number of query parameter value rows
		/// </summary>
		/// <returns></returns>
		public int GetCountQueryParameterValues()
		{
			return gvList.Rows.Count;
		}

		private void SaveQueryParameterValue(int proxyURLId)
		{
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					ProxyURLQueryParameterValue saveItem;
					foreach (GridViewRow row in gvList.Rows)
					{
						if (row.RowType == DataControlRowType.DataRow)
						{
							DropDownList ddlParameterValue = (DropDownList)row.FindControl("ddlParameterValue");
							if (ddlParameterValue.SelectedValue != "")
							{
								saveItem = new ProxyURLQueryParameterValue(true);

								saveItem.ProxyURLId = proxyURLId;
								saveItem.QueryParameterValueId = Convert.ToInt32(ddlParameterValue.SelectedValue);

								ProxyURL url = ProxyURL.FetchByID(proxyURLId);
								//Find the param have same id with the selected param, remove it
								foreach (var record in url.ProxyURLQueryParameterValueRecords())
								{									
									if (record.QueryParameterValue.QueryParameterId == saveItem.QueryParameterValue.QueryParameterId)
									{
										ProxyURLQueryParameterValue.Delete(record.Id);
										break;
									}
								}

								//And create the selected one
								saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
							}
						}
					}

					// transaction complete
					scope.Complete();
				}
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public QueryParameterProxyURLTypeQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as QueryParameterProxyURLTypeQuerySpecification ?? new QueryParameterProxyURLTypeQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}