using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class LogDetailPanel : RecordDetailUserControl
	{
		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		/// <summary>
		/// A strongly-typed shadow of the underlying property.
		/// </summary>
		protected new Log DataItem
		{
			get
			{
				return base.DataItem as Log;
			}
		}

		#endregion

		#region Methods

		protected override object LoadDataItem()
		{
			Log dataItem = null;
			if (this.DataSourceId != null)
			{
				dataItem = Log.FetchByID(this.DataSourceId);
			}
			return dataItem ?? new Log(false);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//try
			//{
			//   Log bindItem = this.DataItem;
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

		protected override void SaveInput()
		{
			//Not Supported
			throw new NotSupportedException();
		}

		#endregion
	}
}