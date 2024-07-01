using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Web.UI;
using HP.HPFx.Web.Utility;
using Microsoft.Security.Application;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class ProxyURLUpdatePanel : RecordDetailUserControl
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
				this.UpdateChildListImmutableConditions();
			}
		}

		/// <summary>
		/// Hides/shows controls based upon the user's roles and the controls' required permissions.
		/// </summary>
		private void HideControlsFromUnauthorizedUsers()
		{
			this.btnSave.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
			this.btnSaveAsNew.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
			this.btnCancel.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
			this.btnDelete.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);

			List<UserRoleId> currentUsersRoles = PersonController.GetCurrentUser().GetRoles();
			this.btnDelete.Visible =
				ProxyURL.IsStateTransitionPossible(ProxyURLStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Visible =
				ProxyURL.IsStateTransitionPossible(ProxyURLStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Visible =
				ProxyURL.IsStateTransitionPossible(ProxyURLStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Visible =
				ProxyURL.IsStateTransitionPossible(ProxyURLStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Visible =
				ProxyURL.IsStateTransitionPossible(ProxyURLStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Visible =
				ProxyURL.IsStateTransitionPossible(ProxyURLStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Visible =
				ProxyURL.IsStateTransitionPossible(ProxyURLStateId.Validated, currentUsersRoles);
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

		protected void btnSaveAsNew_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.SaveAsNew();
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.Delete();
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.OnInputCancel(new EventArgs());
		}

		protected void ddlProxyURLType_SelectedIndexChanged(object sender, EventArgs e)
		{
			ddlProxyURLType_SelectedIndexChanged();
		}

		private void ddlProxyURLType_SelectedIndexChanged()
		{
			this.UpdateChildListImmutableConditions();
			this.ucQueryParameterValueEditUpdatePanel.DataBind();
		}

		protected void btnAbandon_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();
				// no warning for abandon.
				this.lblWarning.Text = String.Empty;

				//second, perform the state transition
				this.GetDataSource().Abandon();
				scope.Complete(); // transaction complete
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.DataBind();
		}

		protected void btnReadyForValidation_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableProxyURL validators
				this.Page.Validate("PublishableProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, validate using the controls' DuplicateProxyURL validators
				this.Page.Validate("DuplicateProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, perform the state transition
				this.GetDataSource().SubmitToValidator();
				scope.Complete(); // transaction complete
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.DataBind();
		}

		protected void btnValidate_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableProxyURL validators
				this.Page.Validate("PublishableProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, perform the state transition
				this.GetDataSource().Validate();
				scope.Complete(); // transaction complete
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.DataBind();
		}

		protected void btnPublish_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableProxyURL validators
				this.Page.Validate("PublishableProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, cannot publish a proxyURL to be replaced.
				this.Page.Validate("ReplacementProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, perform the state transition
				this.GetDataSource().Publish();

				scope.Complete(); // transaction complete
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.DataBind();
		}

		protected void btnUnPublish_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//next, validate using the controls' PublishableProxyURL validators
				this.Page.Validate("PublishableProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, cannot publish a proxyURL to be replaced.
				this.Page.Validate("ReplacementProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}
				//first, save the item like btnSave would do
				this.SaveInput();

				//second, perform the state transition
				this.GetDataSource().UnPublish();

				scope.Complete(); // transaction complete
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.DataBind();
		}

		protected void btnRework_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//second, perform the state transition
				this.GetDataSource().SubmitBackToEditor();
				scope.Complete(); // transaction complete
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.DataBind();
		}

		protected void cvURL_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string uriString = args.Value;
			Uri url;
			args.IsValid = Uri.IsWellFormedUriString(uriString, UriKind.Absolute) && Uri.TryCreate(uriString, UriKind.Absolute, out url);
		}

		protected void cvQueryParameterValueRequired_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.ucQueryParameterValueEditUpdatePanel.IsValid();
		}

		protected void cvTxtTagsValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
		}

		protected void cvTxtTagsMaxTagCount_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (Tag.ParseTagNameList(args.Value, false).Count() <= 100);
		}

		protected void cvProxyURLPublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			ProxyURL proxyURL = this.GetDataSource();
			ProxyURLStateId toState;
			switch (proxyURL.ProxyURLState)
			{
				case ProxyURLStateId.Modified:
				case ProxyURLStateId.Abandoned:
					toState = ProxyURLStateId.ReadyForValidation;
					break;
				case ProxyURLStateId.ReadyForValidation:
					toState = ProxyURLStateId.Validated;
					break;
				default:
					toState = ProxyURLStateId.Published;
					break;
			}
			args.IsValid = proxyURL.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles());
		}

		protected void cvProxyURLDuplicate_ServerValidate(object source, ServerValidateEventArgs args)
		{
			ProxyURL proxyURL = this.GetDataSource();

			// check to see if replacement
			if (proxyURL.ValidationId == null && proxyURL.ProductionId == null)
			{
				args.IsValid = !proxyURL.IsQueryParameterValuesDuplicated();
			}
			else
			{
				args.IsValid = true;
			}
		}

		/// <summary>
		/// Do not allow proxyURL to be replaced to be published or unpublished
		/// </summary>
		protected void cvProxyURLReplacement_ServerValidate(object source, ServerValidateEventArgs args)
		{
			ProxyURL proxyURL = this.GetDataSource();
			args.IsValid = !proxyURL.IsOriginalProxyURLReplacement();					
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
			this.ddlProxyURLType_SelectedIndexChanged();

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			Global.BindProxyURLStatusListControl(this.ddlProxyURLStatus, RowStatus.RowStatusId.Active);
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.lblIdValue.Text = string.Empty;
			this.lblCreatedByValue.Text = string.Empty;
			this.lblCreatedOnValue.Text = string.Empty;
			this.lblModifiedByValue.Text = string.Empty;
			this.lblModifiedOnValue.Text = string.Empty;
			this.txtURL.Text = string.Empty;
			this.ddlOwner.ClearSelection();
			this.ddlProxyURLType.ClearSelection();
			this.ddlProxyURLType_SelectedIndexChanged();
			this.ddlProxyURLStatus.ClearSelection();
			this.txtDescription.Text = string.Empty;
			this.txtTags.Text = string.Empty;
		}

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			//default the ProxyURL Status to Modified
			this.ddlProxyURLStatus.ClearSelection();
			this.ddlProxyURLStatus.ForceSelectedValue(ProxyURLStateId.Modified);

			//default the Proxy URL to current user
			Person currentUser = PersonController.GetCurrentUser();
			Global.ForceSelectedValue(this.ddlOwner, currentUser);
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

			ProxyURL bindItem = this.GetDataSource();

			if (this.IsNewRecord)
			{ this.ApplyDataControlDefaultValues(); }
			else
			{
				try
				{
					if (bindItem == null)
					{
						this.Visible = false;
						return;
					}
					this.lblIdValue.Text = bindItem.Id.ToString();
					this.lblCreatedByValue.Text = bindItem.CreatedBy;
					this.lblCreatedOnValue.Text = bindItem.CreatedOn.ToString();
					this.lblModifiedByValue.Text = bindItem.ModifiedBy;
					this.lblModifiedOnValue.Text = bindItem.ModifiedOn.ToString();
					this.lblValidationIdValue.Text = bindItem.ValidationId.ToString();
					this.lblPublicationIdValue.Text = bindItem.ProductionId.ToString();
					this.txtURL.Text = bindItem.Url;
					this.ddlProxyURLStatus.ForceSelectedValue(bindItem.ProxyURLStatusId);
					this.ddlOwner.ForceSelectedValue(bindItem.OwnerId);
					this.ddlProxyURLType.ForceSelectedValue(bindItem.ProxyURLTypeId);
					this.ddlProxyURLType_SelectedIndexChanged(); //this.ucQueryParameterValueEditUpdatePanel.DataBind();
					this.txtDescription.Text = bindItem.Description;

					// set the proxyULRId for the query parameter value user control
					//this.ucQueryParameterValueEditUpdatePanel.ProxyURLId = bindItem.Id;

					List<string> tagNames = bindItem.TagNames;
                    this.txtTags.Text = (tagNames == null) ? "" : string.Join(", ", tagNames.ToArray());

				}
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the ProxyURL's current state
			List<UserRoleId> currentUsersRoles = PersonController.GetCurrentUser().GetRoles();
			bool isDataModificationAllowed = this.IsDataModificationAllowed();
			bool isMetadataModificationAllowed = this.IsMetadataModificationAllowed();
			this.pnlEditArea.Enabled =
			    isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSave.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSaveAsNew.Enabled =
				!this.IsNewRecord;
			this.btnDelete.Enabled =
				bindItem.IsStateTransitionAllowed(ProxyURLStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Enabled =
				bindItem.IsStateTransitionAllowed(ProxyURLStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Enabled =
				 bindItem.IsStateTransitionAllowed(ProxyURLStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Enabled =
				bindItem.IsStateTransitionAllowed(ProxyURLStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Enabled =
				bindItem.IsStateTransitionAllowed(ProxyURLStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
				bindItem.IsStateTransitionAllowed(ProxyURLStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Enabled =
				bindItem.IsStateTransitionAllowed(ProxyURLStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the ProxyURL Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
		}

		protected bool IsDataModificationAllowed()
		{
			ProxyURL bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;
			else
				return bindItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			ProxyURL bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;
			else
				return bindItem.IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected override void SaveInput()
		{
			bool dataModificationAllowed = this.IsDataModificationAllowed();
			bool metadataModificationAllowed = this.IsMetadataModificationAllowed();

			if (!((dataModificationAllowed || metadataModificationAllowed)))
			{
				return; //don't allow any edits
			}

			ProxyURL saveItem;
			if (this.DataSourceId != null)
			{
				saveItem = ProxyURL.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new ProxyURL(true);
			}

			if (dataModificationAllowed)
			{
				if (!saveItem.IsQueryParameterValuesDuplicated(this.ucQueryParameterValueEditUpdatePanel.CreateQueryParameterValueIdDelimitedList()))
 				{
					this.lblWarning.Text = string.Empty;
				}
				else
				{
					this.lblWarning.Text =
						"Warning: You have saved a Redirector that is already ready to validate, validated or published with the same parameter values.<br>";
				}

				saveItem.Url = this.txtURL.Text.TrimToNull();
				saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
				saveItem.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
				saveItem.ProxyURLStatusId = Convert.ToInt32(this.ddlProxyURLStatus.SelectedValue);
				int? newProxyUrlTypeId = this.ddlProxyURLType.SelectedValue.TryParseInt32();
				if (saveItem.ProxyURLTypeId != newProxyUrlTypeId)
				{
					//delete current values if the value changed
					saveItem.ClearQueryParameterValues();
				}
				saveItem.ProxyURLTypeId = newProxyUrlTypeId ?? -1;
				saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
			}

			if (metadataModificationAllowed)
			{
				//update the ProxyURL's Child Record metadata
				if (this.pnlDataControls_Tags.Enabled)
				{
                    
					saveItem.SetTags(Tag.ParseTagNameList(this.txtTags.Text, false));
				}
			}

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;

			if (dataModificationAllowed)
			{
				//save the query parameter values
				//this.ucQueryParameterValueEditUpdatePanel.ProxyURLId = saveItem.Id;
				this.ucQueryParameterValueEditUpdatePanel.SaveInput(saveItem.Id);
			}
		}

		/// <summary>
		/// Saves all UI input/changes to a newly-created copy of the ProxyURL being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first create the new (copied) ProxyURL based upon the existing (pre-save) DB data
				ProxyURL originalProxyURL = this.GetDataSource();
				ProxyURL newProxyURL = originalProxyURL.SaveAsNew(true);

				//next, change the control's DataSourceId to reference the new ProxyURL
				this.DataSourceId = newProxyURL.Id;

				//next, update the state of the UI controls whose values should not affect the state of the new ProxyURL
				this.ddlProxyURLStatus.ForceSelectedValue(newProxyURL.ProxyURLState);
				this.ddlOwner.ForceSelectedValue(newProxyURL.OwnerId);

				//next, call SaveItem() to apply any UI input/changes (except for those UI controls re-bound above) to the new ProxyURL (only)
				this.SaveInput();

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Delete the ProxyURL being edited.
		/// </summary>
		private void Delete()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//get proxyURL to delete
				ProxyURL proxyURL = this.GetDataSource();

				//delete proxyUrl and it's children
				proxyURL.Delete();

				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlProxyURLStatus.ForceSelectedValue(ProxyURLStateId.Deleted);
			}
		}

		private ProxyURL GetDataSource()
		{
			ProxyURL saveItem;
			if (!this.IsNewRecord)
			{
				saveItem = ProxyURL.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new ProxyURL(true); 
				saveItem.ProxyURLState = ProxyURLStateId.Modified;
			}
			return saveItem;
		}

		protected override void OnDataSourceIdChange(EventArgs e)
		{
			base.OnDataSourceIdChange(e);
			this.UpdateChildListImmutableConditions();
		}

		private void UpdateChildListImmutableConditions()
		{
			this.UpdateQueryParameterValueEditUpdatePanelImmutableConditions();
		}

		private void UpdateQueryParameterValueEditUpdatePanelImmutableConditions()
		{
			int? proxyURLTypeId = this.ddlProxyURLType.SelectedValue.TryParseInt32();
			this.ucQueryParameterValueEditUpdatePanel.ImmutableQueryConditions =
				new QueryParameterProxyURLTypeQuerySpecification
					{
						ProxyURLTypeId = proxyURLTypeId ?? -1
					};
			this.pnlQueryParameterValueEditUpdate.Visible = proxyURLTypeId != null;
		}

		#endregion
	}
}