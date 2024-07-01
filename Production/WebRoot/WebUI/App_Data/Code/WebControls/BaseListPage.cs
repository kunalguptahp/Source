using System;
using System.Web;
using HP.ElementsCPS.Apps.WebUI.MasterPages;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class BaseListPage : BasePage
	{

		#region Abstract Members

		/// <summary>
		/// Returns the page's <see cref="BaseListViewUserControl"/> control.
		/// </summary>
		/// <remarks>
		/// Note to Implementors: When overriding, simply return a reference to the page's actual <see cref="BaseListViewUserControl"/> control.
		/// This property is simply an alias to avoid the use of reflection and to avoid imposing artificial limitations on the naming, etc. of the page's actual control.
		/// </remarks>
		protected abstract BaseListViewUserControl ListPanel { get; }

		/// <summary>
		/// Redirects the current page to the appropriate URL for displaying the data/query specified by <paramref name="querySpecification"/>.
		/// </summary>
		/// <remarks>
		/// Note to Implementors: When overriding, simply do a Response.Redirect to the correct URL 
		/// (usually the same page but with a different QueryString).
		/// This method is abstract because this class does not know the concrete page's URL navigation conventions.
		/// </remarks>
		protected abstract string GeneratePageUrl(IQuerySpecification querySpecification);

		/// <summary>
		/// Generates a dynamic title for this page based upon the page's current state (e.g. the page's <see cref="QuerySpecification"/>).
		/// </summary>
		/// <remarks>
		/// Note to Implementors: When overriding, simply return a string which represents a good page title for the page (but excluding the "CPS - " prefix), 
		/// based upon (at least) the page's <see cref="BaseListPage.QuerySpecification"/>.
		/// This method is abstract because the page should use a record-specific title so that the client browser's History/Back functionality displays user-readable information.
		/// </remarks>
		/// <returns></returns>
		protected abstract string GeneratePageTitle();

		#endregion

		#region Properties

		private IQuerySpecification _QuerySpecification = null;

		public IQuerySpecification QuerySpecification
		{
			get
			{
				if (this._QuerySpecification == null)
				{
					//NOTE: We should never read the query from the QueryString during PostBacks
					if (!this.IsPostBack)
					{
						string s = this.Request.QueryString[Global.UrlParam_QuerySpecification];
						s = HttpUtility.UrlDecode(s); //Due to the double-encoding...
						QuerySpecificationWrapper qs;
						try
						{
							qs = (string.IsNullOrEmpty(s)) ? null : QuerySpecificationWrapper.FromXml(s);
						}
						catch (InvalidOperationException ex)
						{
							//if unable to successfully parse the string, we just ignore it and act like no value was specified
							qs = null;
							LogManager.Current.Log(Severity.Info, this, "An invalid QuerySpecification XML was specified in the QueryString for the BaseListPage.QuerySpecification property.", ex);
						}
						this._QuerySpecification = qs ?? this.GetDefaultQuerySpecification();
					}
				}

				return this._QuerySpecification;
			}
			set
			{
				if (!Equals(value, this.QuerySpecification))
				{
					this._QuerySpecification = value;
					this.OnQuerySpecificationChanged();
				}
			}
		}

		#endregion

		#region ViewState Methods

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			this._QuerySpecification = (IQuerySpecification)this.ViewState["QuerySpecification"];
		}

		protected override object SaveViewState()
		{
			this.ViewState["QuerySpecification"] = this._QuerySpecification;
			return base.SaveViewState();
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
			WebUtility.DisableClientCacheing(this.Response);

			if (!this.Page.IsPostBack)
			{
				this.BindList(this.QuerySpecification);
			}
		}

		/// <seealso cref="SyncQuerySpecificationWithListPanel"/>
		protected virtual void Page_PreRender(object sender, EventArgs e)
		{
			this.SyncQuerySpecificationWithListPanel();

			this.UpdatePageTitle();
		}

		private void AttachEventHandlers()
		{
			if (this.ListPanel != null)
			{
				// setup events
				this.ListPanel.QueryChanged += this.ListPanel_QueryChanged;
				this.PreRender += this.Page_PreRender;
			}
		}

		/// <summary>
		/// Invoked whenever a new value is assigned to the <see cref="BaseListPage.QuerySpecification"/> property.
		/// </summary>
		/// <remarks>
		/// NOTE: This method is not invoked automatically if the current value 
		/// (i.e. the object instance returned by the <see cref="BaseListPage.QuerySpecification"/> property) is directly modified.
		/// </remarks>
		protected virtual void OnQuerySpecificationChanged()
		{
			IQuerySpecification query = this.QuerySpecification;
			this.RedirectToListPage(query);

			//TODO: Review Needed: Review implementation for correctness
#warning Review Needed: Review implementation for correctness

			//if (!this.Page.IsPostBack)
			//{
			//   //We DON'T want to rebind the list (below), unless this is a PostBack, because the Page_Load handler will do that for non-PostBacks.
			//   return;
			//}

			////if a redirect hasn't occurred, then we must re-bind the list using the changed query
			//this.BindList(query);
		}

		#endregion

		#region ControlEvents

		/// <seealso cref="SyncQuerySpecificationWithListPanel"/>
		protected void ListPanel_QueryChanged(object sender, EventArgs e)
		{
			this.SyncQuerySpecificationWithListPanel();
		}

		#endregion

		#region Other Methods

		/// <summary>
		/// Redirects the current page to the appropriate URL for displaying the data/query specified by <paramref name="querySpecification"/>.
		/// </summary>
		protected void RedirectToListPage(IQuerySpecification querySpecification)
		{
			string pageUrl = this.GeneratePageUrl(querySpecification);
			if (WebUtility.IsUriLengthValid(pageUrl))
			{
				this.Response.Redirect(pageUrl);
			}
		}

		/// <summary>
		/// Synchronizes the page's <see cref="BaseListPage.QuerySpecification"/> with the <see cref="BaseListViewUserControl.QuerySpecification"/> of the page's ListPanel.
		/// </summary>
		private void SyncQuerySpecificationWithListPanel()
		{
			this.QuerySpecification = this.ListPanel.QuerySpecification;
		}

		protected virtual void BindList(IQuerySpecification querySpecification)
		{
			this.ApplyImmutableConditionsToList();
			this.ListPanel.QuerySpecification = querySpecification;
			this.ListPanel.DataBind();
		}

		/// <summary>
		/// If needed, inheritors can override this method so that it modifies the 
		/// <see cref="BaseListViewUserControl.ImmutableQueryConditions"/> of the page's <see cref="ListPanel"/>.
		/// </summary>
		protected virtual void ApplyImmutableConditionsToList()
		{
			//do nothing
		}

		/// <summary>
		/// If needed, inheritors can override this method to return a custom <see cref="IQuerySpecification"/> that should be used as the default 
		/// (i.e. instead of a standard "empty" Query Specification).
		/// </summary>
		protected virtual IQuerySpecification GetDefaultQuerySpecification()
		{
			return new QuerySpecificationWrapper();
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

		#endregion

	}
}