using System;
using System.Web;
using HP.ElementsCPS.Apps.WebUI.MasterPages;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class BaseRecordDetailPage : BasePage
	{

		#region Abstract Members

		/// <summary>
		/// Returns the page's <see cref="RecordDetailUserControl"/> control.
		/// </summary>
		/// <remarks>
		/// Note to Implementors: When overriding, simply return a reference to the page's actual <see cref="RecordDetailUserControl"/> control.
		/// This property is simply an alias to avoid the use of reflection and to avoid imposing artificial limitations on the naming, etc. of the page's actual control.
		/// </remarks>
		protected abstract RecordDetailUserControl RecordDetailControl { get; }

		/// <summary>
		/// Redirects the current page to the appropriate URL for updating the specified <see cref="DataSourceId"/>.
		/// </summary>
		/// <remarks>
		/// Note to Implementors: When overriding, simply do a Response.Redirect to the correct URL 
		/// (usually the same page but with a different QueryString).
		/// This method is abstract because this class does not know the concrete page's URL navigation conventions.
		/// </remarks>
		protected abstract void RedirectToUpdatePage(int? dataSourceId);

		/// <summary>
		/// Generates a dynamic title for this page based upon the page's current state (e.g. the page's <see cref="DataSourceId"/>).
		/// </summary>
		/// <remarks>
		/// Note to Implementors: When overriding, simply return a string which represents a good page title for the page (but excluding the "CPS - " prefix), 
		/// based upon (at least) the page's <see cref="DataSourceId"/>.
		/// This method is abstract because the page should use a record-specific title so that the client browser's History/Back functionality displays user-readable information.
		/// </remarks>
		/// <returns></returns>
		protected abstract string GeneratePageTitle();

		#endregion

		#region Properties

		public int? DataSourceId
		{
			get
			{
				string s = this.Request.QueryString[Global.UrlParam_DataSourceId];
				return s.TryParseInt32();
			}
			set
			{
				if (value != this.DataSourceId)
				{
					this.RedirectToUpdatePage(value);
				}
			}
		}

		#endregion

		#region PageEvents

		protected override void OnInit(EventArgs e)
		{
			this.AttachEventHandlers();
			base.OnInit(e);
		}

		protected virtual void Page_Load(object sender, EventArgs e)
		{
			LogManager.Current.Log(Severity.Debug, this, this.ID + ": Entering: BaseRecordDetailPage.Page_Load(object sender, EventArgs e)");
			WebUtility.DisableClientCacheing(this.Response);

			if (!this.Page.IsPostBack)
			{
				this.EditRecord(this.DataSourceId);
			}
		}

		/// <seealso cref="SyncDataSourceIdWithEditPanel"/>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.SyncDataSourceIdWithEditPanel();

			this.UpdatePageTitle();
		}

		private void AttachEventHandlers()
		{
			if (this.RecordDetailControl != null)
			{
				// setup events
				this.RecordDetailControl.InputSaved += this.RecordDetailControl_InputSaved;
				this.RecordDetailControl.InputCancelled += this.RecordDetailControl_InputCancelled;
				//this.RecordDetailControl.DataSourceIdChanged += this.RecordDetailControl_DataSourceIdChanged;
				this.PreRender += this.Page_PreRender;
			}
		}

		#endregion

		#region ControlEvents

		/// <summary>
		/// Refreshes the page when the user cancels an update.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RecordDetailControl_InputCancelled(object sender, EventArgs e)
		{
			WebUtility.RefreshClient(this);
		}

		/// <seealso cref="SyncDataSourceIdWithEditPanel"/>
		protected void RecordDetailControl_DataSourceIdChanged(object sender, EventArgs e)
		{
			//NOTE: Do nothing (in this method). See the SyncDataSourceIdWithEditPanel method comments for details
		}

		/// <seealso cref="SyncDataSourceIdWithEditPanel"/>
		protected void RecordDetailControl_InputSaved(object sender, EventArgs e)
		{
			this.SyncDataSourceIdWithEditPanel();
		}

		#endregion


		/// <summary>
		/// Synchronizes the page's <see cref="BaseRecordDetailPage.DataSourceId"/> with the <see cref="RecordDetailUserControl.DataSourceId"/> of the page's RecordDetailControl.
		/// </summary>
		/// <remarks>
		/// Although the obvious place to call this method would be in the <see cref="RecordDetailUserControl.DataSourceIdChanged"/> handler, 
		/// doing so would cause the transaction that caused the DataSourceId to change to abort, 
		/// because this method would cause a redirect before the transaction was committed.
		/// Therefore, this method is instead invoked by the <see cref="BaseEditDataUserControl.InputSaved"/> handler
		/// and by the <see cref="System.Web.UI.Control.PreRender"/> handler, which together should ensure that any change to the DetailControl's DataSourceId is synched 
		/// before the page is fully rendered.
		/// </remarks>
		private void SyncDataSourceIdWithEditPanel()
		{
			this.DataSourceId = this.RecordDetailControl.DataSourceId;
		}

		private void EditRecord(int? dataSourceId)
		{
			this.RecordDetailControl.DataSourceId = dataSourceId;
			this.RecordDetailControl.DataBind();
		}

		/// <seealso cref="GeneratePageTitle"/>
		private void UpdatePageTitle()
		{
			string title = this.GeneratePageTitle();
			this.Title = "CPS - " + title;
			this.SetContentAreaTitle(title);
		}

		private void SetContentAreaTitle(string title)
		{
			DefaultPortalMaster masterPage = WebUtility.GetMasterPage<DefaultPortalMaster>(this);
			if (masterPage != null)
			{
				masterPage.ContentAreaTitle = title;
			}
		}

	}
}