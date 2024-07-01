using System;
using System.Web;
using HP.ElementsCPS.Apps.WebUI.MasterPages;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class BaseRecordSummaryDetailPage : BaseRecordDetailPage
	{

		/// <summary>
		/// Returns the page's <see cref="BaseRecordSummaryDetailUserControl"/> control.
		/// </summary>
		/// <remarks>
		/// Note to Implementors: When overriding, simply return a reference to the page's actual <see cref="RecordDetailUserControl"/> control.
		/// This property is simply an alias to avoid the use of reflection and to avoid imposing artificial limitations on the naming, etc. of the page's actual control.
		/// </remarks>
		protected abstract BaseRecordSummaryDetailUserControl RecordSummaryDetailUserControl { get; }

		protected override RecordDetailUserControl RecordDetailControl
		{
			get
			{
				return this.RecordSummaryDetailUserControl;
			}
		}

		protected override void RedirectToUpdatePage(int? dataSourceId)
		{
			this.Response.Redirect(this.RecordSummaryDetailUserControl.GenerateDetailPageUrl(dataSourceId));
		}

		protected override string GeneratePageTitle()
		{
			string dataSourceEntityName = this.RecordSummaryDetailUserControl.GetDataSourceEntityName();
			int? dataSourceId = this.DataSourceId;
			const string invalidDataSourceTemplate = "Invalid {0}";
			const string validDataSourceTemplate = "Summary of {0} #{1}";
			return (dataSourceId == null) ? string.Format(invalidDataSourceTemplate, dataSourceEntityName) 
			       	: string.Format(validDataSourceTemplate, dataSourceEntityName, dataSourceId.Value);
		}
	}
}