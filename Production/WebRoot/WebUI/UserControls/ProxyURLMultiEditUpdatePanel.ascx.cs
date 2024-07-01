using System;
using System.Collections.Generic;
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
	public partial class ProxyURLMultiEditUpdatePanel : BaseQuerySpecificationEditDataUserControl
	{

		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		#endregion

		#region PageEvents

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.HideControlsFromUnauthorizedUsers();
				this.pnlTagsToRemove.Visible = !ucQueryParameterValueMultiUpdate.IsNew;
			}
		}

		/// <summary>
		/// Hides/shows controls based upon the user's roles and the controls' required permissions.
		/// </summary>
		private void HideControlsFromUnauthorizedUsers()
		{
			this.btnSave.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
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

		protected void ddlProxyURLType_SelectedIndexChanged(object sender, EventArgs e)
		{
			ProxyURLQuerySpecification qs = ConvertToExpectedType(this.QuerySpecification);
			qs.ProxyURLTypeId = ddlProxyURLType.SelectedValue.TryParseInt32();
			qs.Description = this.txtDescription.Text;
			qs.Tags = this.txtTagsToAdd.Text;
			Response.Redirect(Global.GetProxyURLMultiEditUpdatePageUri(qs));
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
			Global.BindProxyURLTypeListControl(this.ddlProxyURLType, RowStatus.RowStatusId.Active);
			this.ddlProxyURLType.InsertItem(0, "", Global.GetSelectListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetSelectListText());
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.ddlOwner.ClearSelection();
			this.ddlProxyURLType.ClearSelection();
			this.txtDescription.Text = string.Empty;
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
			ProxyURLQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);
			if (query.IdList.Count == 0)
				bindItems.Clear();
			this.txtDescription.Text = query.Description;
			this.txtTagsToAdd.Text = query.Tags;

			bool isDataModificationAllowed = this.IsDataModificationAllowed();
			bool isMetadataModificationAllowed = this.IsMetadataModificationAllowed();

			// only allow change proxyURLType if there are no parameters
			this.ddlProxyURLType.Enabled = ((bindItems.Count == 0 || bindItems.IsChildlessQueryParameterValue()) && isDataModificationAllowed);
			int? proxyTypeId = this.ConvertToExpectedType(this.QuerySpecification).ProxyURLTypeId;
			if (proxyTypeId.HasValue)
				this.ddlProxyURLType.ForceSelectedValue(proxyTypeId);
			if (bindItems.Count > 0)
				this.ddlProxyURLType.ForceSelectedValue(bindItems[0].ProxyURLTypeId);

			// only allow change parameter if there is only a single proxyURLType
			this.pnlDataControls_QueryParameterValueMultiUpdatePanel.Enabled = (bindItems.IsIdenticalProxyURLType() && isDataModificationAllowed);

			this.pnlEditArea.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSave.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;

			//only Coordinators are allowed to set or change the ProxyURL Owner
			this.ddlOwner.Enabled = SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator);
			if (ucQueryParameterValueMultiUpdate.IsNew)
				this.ddlOwner.ForceSelectedValue(PersonController.GetCurrentUser().Id);
		}

		protected bool IsDataModificationAllowed()
		{
			return this.GetProxyURLs().IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			return this.GetProxyURLs().IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected override void SaveInput()
		{
			bool dataModificationAllowed = this.IsDataModificationAllowed();
			bool metadataModificationAllowed = this.IsMetadataModificationAllowed();

			if (!((dataModificationAllowed || metadataModificationAllowed)))
			{
				return; //don't allow any edits
			}

			List<CPSLog> cpsLogList = new List<CPSLog>();
			bool proxyURLModified = false;

			ProxyURLCollection proxyURLs = ucQueryParameterValueMultiUpdate.GetProxyUrls();
			foreach (ProxyURL saveItem in proxyURLs)
			{
				proxyURLModified = false;
				if (dataModificationAllowed)
				{
					proxyURLModified = ucQueryParameterValueMultiUpdate.IsTargetUrlModified(saveItem);

					if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
					{
						saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
						proxyURLModified = true;
					}

					if (!string.IsNullOrEmpty(this.ddlProxyURLType.SelectedValue) && (this.ddlProxyURLType.Enabled == true))
					{
						saveItem.ProxyURLTypeId = Convert.ToInt32(this.ddlProxyURLType.SelectedValue);
						proxyURLModified = true;
					}

					if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
					{
						saveItem.OwnerId = string.IsNullOrEmpty(this.ddlOwner.SelectedValue) ? PersonController.GetCurrentUser().Id : Convert.ToInt32(this.ddlOwner.SelectedValue);
						proxyURLModified = true;
					}

					if (proxyURLModified)
					{
						saveItem.ProxyURLStatusId = (int)ProxyURLStateId.Modified;
						saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
					}

					Dictionary<int, int> dict = this.ucQueryParameterValueMultiUpdate.GetParameterList(saveItem);
					foreach (int key in dict.Keys)
					{
						ProxyURLQueryParameterValueQuerySpecification qs = new ProxyURLQueryParameterValueQuerySpecification();
						qs.ProxyURLId = saveItem.Id;
						qs.QueryParameterId = key;
						var oldValues = VwMapProxyURLQueryParameterValueController.Fetch(qs);
						foreach (var oldValue in oldValues)
							ProxyURLQueryParameterValue.Delete(oldValue.Id);

						ProxyURLQueryParameterValue v = new ProxyURLQueryParameterValue(true);
						v.ProxyURLId = saveItem.Id;
						v.QueryParameterValueId = dict[key];
						v.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
					}
					if (dict.Count > 0)
						proxyURLModified = true;
				}

				saveItem.Log(Severity.Info, "ProxyURL data saved.");

				if (metadataModificationAllowed)
				{
					if (this.pnlDataControls_TagsToAdd.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToAdd.Text))
						{
							saveItem.AddTags(Tag.ParseTagNameList(this.txtTagsToAdd.Text, false));
							proxyURLModified = true;
						}
					}

					if (this.pnlDataControls_TagsToRemove.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToRemove.Text))
						{
							saveItem.RemoveTags(Tag.ParseTagNameList(this.txtTagsToRemove.Text, false));
							proxyURLModified = true;
						}
					}
				}

				if (proxyURLModified)
				{
					cpsLogList.Add(new CPSLog(saveItem.Id, saveItem.Description, saveItem.Url, "Success Saved"));
				}
			}

			if (cpsLogList.Count > 0)
			{
				this.ucCPSLog.CPSLogList = cpsLogList;
				this.ucCPSLog.DataBind();
			}
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