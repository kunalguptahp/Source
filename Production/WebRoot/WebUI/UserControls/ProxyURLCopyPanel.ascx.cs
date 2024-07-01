using System;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.SqlClient;
using HP.ElementsCPS.Core.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class ProxyURLCopyPanel : BaseQuerySpecificationEditDataUserControl
	{
		#region Constants

		private const string ViewStateKey_ProxyURLIds = "ProxyURLIds";

		#endregion

		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		public List<int> ProxyURLIds
		{
			get
			{
				return this.ViewState[ViewStateKey_ProxyURLIds] as List<int>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_ProxyURLIds);
				}
				else
				{
					this.ViewState[ViewStateKey_ProxyURLIds] = value;
				}
			}
		}

		#endregion

		#region PageEvents

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.HideControlsFromUnauthorizedUsers();
				//this.txtTagsToRemove.Enabled = !ucProxyURLDescriptionMultiReplaceUpdate.IsNew;
			}
		}

		/// <summary>
		/// Hides/shows controls based upon the user's roles and the controls' required permissions.
		/// </summary>
		private void HideControlsFromUnauthorizedUsers()
		{
			this.btnSave.Visible = SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
			this.btnCancel.Visible = SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
		}

		#endregion

		#region ControlEvents

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.OnInputSave(new EventArgs());
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.OnInputCancel(new EventArgs());
		}

		protected void cvTxtTagsToAddValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
		}

		protected void cvTxtTagsToRemoveValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
		}

		protected void cvTxtTagsToAddMaxTagCount_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (Tag.ParseTagNameList(args.Value, false).Count() <= 100);
		}

		#endregion

		#region Methods

		protected override void UnbindItem()
		{
			base.UnbindItem();

			this.PopulateListControls();
			this.ClearDataControls();
		}

		/// <summary>
		/// Re-populates the ListItems of all this control's ListControls.
		/// </summary>
		/// <remarks>
		/// This method should be called every time the control is Un-bound/Re-bound so that any changes made for a previous DataSource don't persist.
		/// </remarks>
		private void PopulateListControls()
		{
			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetSelectListText());
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.lblIdText.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
			this.ddlOwner.ClearSelection();
			this.txtTagsToAdd.Text = string.Empty;
			this.txtTagsToRemove.Text = string.Empty;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public ProxyURLQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as ProxyURLQuerySpecification ?? new ProxyURLQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			ProxyURLCollection bindItems = this.GetProxyURLs();
			try
			{
				List<int> proxyURLIds = bindItems.GetIds();
				proxyURLIds.Sort();
				this.lblIdText.Text = string.Join(", ", proxyURLIds.ToStrings().ToArray());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}

			if (this.ConvertToExpectedType(this.QuerySpecification).IdList.Count == 0)
				bindItems.Clear();

			//only Coordinators are allowed to set or change the ProxyURL Owner
			this.ddlOwner.Enabled = SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator);
			this.ddlOwner.ForceSelectedValue(PersonController.GetCurrentUser().Id);
		}

		protected override void SaveInput()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			ProxyURLCollection originalProxyURLs = this.GetProxyURLs();

			foreach (ProxyURL originalItem in originalProxyURLs)
			{
				ProxyURL newItem = originalItem.SaveAsNew(true, true);

				if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
				{
					newItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
				}

				if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
				{
					newItem.OwnerId = string.IsNullOrEmpty(this.ddlOwner.SelectedValue) ? PersonController.GetCurrentUser().Id : Convert.ToInt32(this.ddlOwner.SelectedValue);
				}

				newItem.ProxyURLStatusId = (int)ProxyURLStateId.Modified;
				newItem.ValidationId = null;
				newItem.ProductionId = null;
				newItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

				cpsLogList.Add(new CPSLog(newItem.Id, string.Format(CultureInfo.CurrentCulture, "Copied Id #{0} to #{1}", originalItem.Id, newItem.Id), newItem.Url, "Copy success"));

				if (this.pnlDataControls_TagsToAdd.Enabled)
				{
					if (!string.IsNullOrEmpty(this.txtTagsToAdd.Text))
					{
						newItem.AddTags(Tag.ParseTagNameList(this.txtTagsToAdd.Text, false));
					}
				}

				if (this.pnlDataControls_TagsToRemove.Enabled)
				{
					if (!string.IsNullOrEmpty(this.txtTagsToRemove.Text))
					{
						newItem.RemoveTags(Tag.ParseTagNameList(this.txtTagsToRemove.Text, false));
					}
				}
			}
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();

			// when copied do not allow to be copied again
			this.btnSave.Enabled = false;
			this.btnCancel.Enabled = false;
		}

		private VwMapProxyURLCollection GetDataSource()
		{
			ProxyURLQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapProxyURLCollection proxyURLCollection = VwMapProxyURLController.Fetch(query);
			return proxyURLCollection;
		}

		/// <summary>
		/// Gets the <see cref="ProxyURL"/>s corresponding to the items in the <see cref="VwMapProxyURLCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private ProxyURLCollection GetProxyURLs()
		{
			return this.GetDataSource().GetProxyURLs();
		}

		#endregion

	}
}