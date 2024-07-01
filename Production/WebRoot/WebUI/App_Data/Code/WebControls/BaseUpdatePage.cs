using System;
using System.Web;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class BaseUpdatePage : BaseRecordDetailPage
	{

		#region Properties

		private IQuerySpecification _DefaultValuesSpecification = null;

		public IQuerySpecification DefaultValuesSpecification
		{
			get
			{
				if (this._DefaultValuesSpecification == null)
				{
					//NOTE: We should never read the query from the QueryString during PostBacks
					if (!this.IsPostBack)
					{
						string s = this.Request.QueryString[Global.UrlParam_DefaultValuesSpecification];
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
							LogManager.Current.Log(Severity.Info, this, "An invalid QuerySpecification XML was specified in the QueryString for the BaseUpdatePage.DefaultValuesSpecification property.", ex);
						}
						this._DefaultValuesSpecification = qs ?? new QuerySpecificationWrapper();
					}
				}

				return this._DefaultValuesSpecification;
			}
			set
			{
				if (!Equals(value, this.DefaultValuesSpecification))
				{
					this._DefaultValuesSpecification = value;
				}
			}
		}

		#endregion

		#region ViewState Methods

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			this._DefaultValuesSpecification = (IQuerySpecification)this.ViewState["DefaultValuesSpecification"];
		}

		protected override object SaveViewState()
		{
			this.ViewState["DefaultValuesSpecification"] = this._DefaultValuesSpecification;
			return base.SaveViewState();
		}

		#endregion

		#region PageEvents

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.RecordDetailControl.DefaultValuesSpecification = this.DefaultValuesSpecification;
			}
			base.Page_Load(sender, e);
		}

		#endregion

	}
}