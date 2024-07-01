﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Utility;
using HP.HPFx.Diagnostics.Logging;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class CPSLogPanel : System.Web.UI.UserControl
	{

		#region Constants

		private const string ViewStateKey_CPSLogList = "CPSLogList";

		#endregion

		#region Properties

		public List<CPSLog> CPSLogList
		{
			get
			{
				return this.ViewState[ViewStateKey_CPSLogList] as List<CPSLog>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_CPSLogList);
				}
				else
				{
					this.ViewState[ViewStateKey_CPSLogList] = value;
				}
			}
		}

		#endregion

		#region PageEvents

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public override void DataBind()
		{
			base.DataBind();
			this.grvCPSLog.DataSource = CPSLogList;
			this.grvCPSLog.DataBind();
		}

		#endregion

		#region Methods

		/// <summary>
		/// A generic event handler for a <see cref="DataPager"/> control's <see cref="Control.Init"/> event.
		/// It is primarily intended to be used with <see cref="DataPager"/> controls that use the "StandardDataPager" <see cref="WebControl.SkinID"/>.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DataPager_Init(object sender, EventArgs e)
		{
			DataPager dataPager = sender as DataPager;
			BaseListViewUserControl.InitializeDataPager(dataPager);
		}

		#endregion

	}
}