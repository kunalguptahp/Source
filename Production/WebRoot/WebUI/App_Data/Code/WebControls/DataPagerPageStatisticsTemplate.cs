using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Diagnostics.Logging;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	/// <summary>
	/// A <see cref="System.Web.UI.ITemplate"/> that can be used in a <see cref="DataPager"/> control to display row- and page-related statistics about the current page and result set.
	/// </summary>
	/// <remarks>
	/// <para>
	/// NOTE: The implementation of this class is based upon the article found here:
	/// http://msdn.microsoft.com/en-us/library/0e39s2ck.aspx?ppud=4
	/// </para>
	/// </remarks>
	public class DataPagerPageStatisticsTemplate : System.Web.UI.ITemplate
	{
		System.Web.UI.WebControls.ListItemType _TemplateType;

		public DataPagerPageStatisticsTemplate()
			: this(ListItemType.Pager)
		{
		}

		public DataPagerPageStatisticsTemplate(System.Web.UI.WebControls.ListItemType type)
		{
			this._TemplateType = type;
		}

		public void InstantiateIn(System.Web.UI.Control container)
		{
			PlaceHolder ph = new PlaceHolder();
			Label lblTotalRows = new Label { ID = "lblTotalRows" };
			Label lblStartRowNumber = new Label { ID = "lblStartRowNumber" };
			Label lblEndRowNumber = new Label { ID = "lblEndRowNumber" };
			//Label lblPageSize = new Label { ID = "lblPageSize" };
			//Label lblPage = new Label { ID = "lblPage" };
			Label lblCurrentPageRowCount = new Label { ID = "lblCurrentPageRowCount" };

			switch (this._TemplateType)
			{
				case ListItemType.Pager:
					ph.Controls.Add(new LiteralControl("<span></br>"));
					ph.Controls.Add(new LiteralControl("Displaying "));
					ph.Controls.Add(lblCurrentPageRowCount);
					ph.Controls.Add(new LiteralControl(" rows ("));
					ph.Controls.Add(lblStartRowNumber);
					ph.Controls.Add(new LiteralControl(" - "));
					ph.Controls.Add(lblEndRowNumber);
					ph.Controls.Add(new LiteralControl(" of "));
					ph.Controls.Add(lblTotalRows);
					ph.Controls.Add(new LiteralControl(")"));
					ph.Controls.Add(new LiteralControl("</span>"));
					ph.DataBinding += new EventHandler(Item_DataBinding);
					break;
				default:
					return; //Do nothing
					//break; //Do nothing
			}
			container.Controls.Add(ph);
		}


		public void Item_DataBinding(object sender, System.EventArgs e)
		{
			PlaceHolder ph = (PlaceHolder)sender;
			DataPagerFieldItem dataPagerFieldItem = (DataPagerFieldItem)ph.NamingContainer;
			DataPager pager = (DataPager)dataPagerFieldItem.NamingContainer;
			int totalRowCount = pager.TotalRowCount;
			int pageSize = pager.PageSize; //pager.MaximumRows;
			int startRowIndex = pager.StartRowIndex;
			int endRowIndex = Math.Max(0, -1 + Math.Min((startRowIndex + pageSize), totalRowCount));
			int currentPageRowCount = (totalRowCount == 0) ? 0 : (1 + endRowIndex - startRowIndex);
			//convert the 0-based indexes to 1-based numbers (unless the totalRowCount is 0)
			int startRowNumber = (totalRowCount == 0) ? 0 : 1 + startRowIndex;
			int endRowNumber = (totalRowCount == 0) ? 0 : 1 + endRowIndex;

			Label lblTotalRows = ph.FindControl("lblTotalRows") as Label;
			if (lblTotalRows != null)
			{
				lblTotalRows.Text = totalRowCount.ToString();
			}
			Label lblCurrentPageRowCount = ph.FindControl("lblCurrentPageRowCount") as Label;
			if (lblCurrentPageRowCount != null)
			{
				lblCurrentPageRowCount.Text = currentPageRowCount.ToString();
			}
			//Label lblStartRowIndex = ph.FindControl("lblStartRowIndex") as Label;
			//if (lblStartRowIndex != null)
			//{
			//   lblStartRowIndex.Text = startRowIndex.ToString();
			//}
			Label lblStartRowNumber = ph.FindControl("lblStartRowNumber") as Label;
			if (lblStartRowNumber != null)
			{
				lblStartRowNumber.Text = startRowNumber.ToString();
			}
			//Label lblEndRowIndex = ph.FindControl("lblEndRowIndex") as Label;
			//if (lblEndRowIndex != null)
			//{
			//   lblEndRowIndex.Text = endRowIndex.ToString();
			//}
			Label lblEndRowNumber = ph.FindControl("lblEndRowNumber") as Label;
			if (lblEndRowNumber != null)
			{
				lblEndRowNumber.Text = endRowNumber.ToString();
			}
			//Label lblPageSize = ph.FindControl("lblPageSize") as Label;
			//if (lblPageSize != null)
			//{
			//   lblPageSize.Text = pageSize.ToString();
			//}
			//Label lblPageCount = ph.FindControl("lblPageCount") as Label;
			//if (lblPageCount != null)
			//{
			//   lblPageCount.Text = pageCount.ToString();
			//}
			//Label lblPageIndex = ph.FindControl("lblPageIndex") as Label;
			//if (lblPageIndex != null)
			//{
			//   lblPageIndex.Text = pageIndex.ToString();
			//}
			//Label lblPageNumber = ph.FindControl("lblPageNumber") as Label;
			//if (lblPageNumber != null)
			//{
			//   lblPageNumber.Text = pageNumber.ToString();
			//}
		}

	}

}