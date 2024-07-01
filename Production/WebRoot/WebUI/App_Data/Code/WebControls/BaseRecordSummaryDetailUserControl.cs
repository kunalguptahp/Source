using System;
using System.Web.UI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class BaseRecordSummaryDetailUserControl : RecordDetailUserControl
	{

		#region Abstract Methods

		/// <summary>
		/// Returns the entity name of the control's dataSource.
		/// </summary>
		public abstract string GetDataSourceEntityName();

		/// <summary>
		/// Returns the URL that can be used to access a (default) "Record Detail" page for this control's currently-bound data source.
		/// </summary>
		public abstract string GenerateDetailPageUrl(int? dataSourceId);

		#endregion

		#region Overridden Methods

		protected override void SaveInput()
		{
			throw new NotSupportedException();
		}

		#endregion

		#region Other Methods

		/// <summary>
		/// Updates the URL and visibility of the "open popup as page" link based upon the current DataBinding context
		/// </summary>
		/// <param name="directAccessLink"></param>
		protected void BindDirectAccessLink(HyperLink directAccessLink)
		{
			directAccessLink.NavigateUrl = this.GenerateDetailPageUrl(this.DataSourceId);
			directAccessLink.Visible = !this.IsNewRecord;
		}

		#endregion

	}
}