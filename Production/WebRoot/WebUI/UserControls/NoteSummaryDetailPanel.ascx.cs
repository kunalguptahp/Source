using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class NoteSummaryDetailPanel : BaseRecordSummaryDetailUserControl
	{
		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		/// <summary>
		/// A strongly-typed shadow of the underlying property.
		/// </summary>
		protected new VwMapNote DataItem
		{
			get
			{
				return base.DataItem as VwMapNote;
			}
		}

		#endregion

		#region Methods

		public override string GetDataSourceEntityName()
		{
			return "Note";
		}

		public override string GenerateDetailPageUrl(int? dataSourceId)
		{
			return Global.GetNoteDetailPageUri(dataSourceId, null);
		}

		protected override object LoadDataItem()
		{
			VwMapNote dataItem = null;
			if (this.DataSourceId != null)
			{
				VwMapNoteCollection bindItemCollection = VwMapNoteController.FetchByID(this.DataSourceId);
				dataItem = (bindItemCollection.Count == 0) ? null : bindItemCollection[0];
			}
			return dataItem ?? new VwMapNote();
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//try
			//{
			//   VwMapNote bindItem = this.DataItem;
			//   if (bindItem.IsNew)
			//   {
			//      return;
			//   }
			//}
			//catch (SqlException ex)
			//{
			//   LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
			//   throw;
			//}
		}

		#endregion

	}
}
