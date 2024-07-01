using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.UI.WebControls;
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
	public partial class ProxyURLEditUpdatePanel : BaseQuerySpecificationEditDataUserControl
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

			using (TransactionScope scope = new TransactionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//second, perform the state transition
				this.GetProxyURLs().Abandon();
				this.RaiseInputSaved(e);

				//last, re-bind the item to update the data, buttons, etc.
				this.BindItem();

				scope.Complete(); // transaction complete
			}
		}

		protected void btnReadyForValidation_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableProxyURL validators
				this.Page.Validate("PublishableProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}

				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the redirectors (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ProxyURLCollection proxyURLs = this.GetProxyURLs();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ProxyURL proxyURL in proxyURLs)
			{
				// NOTE: Removing check here to increase multi-validate performance.  Now, this check is done in order to get redirector in to "ready to publish" state.
				if (proxyURL.IsQueryParameterValuesDuplicated())
				{
					cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Ready to Validate Failed: There is another Redirector that is ready to validate, validated or published with the same parameter values."));
				}
				else
				{
					using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
					{
						proxyURL.SubmitToValidator();
						scope.Complete(); // transaction complete
					}
					cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Ready to Validate Success"));
				}
			}

			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlCPSLog.Visible = true;
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();
		}

		protected void btnValidate_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableProxyURL validators
				this.Page.Validate("PublishableProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the redirectors (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ProxyURLCollection proxyURLs = this.GetProxyURLs();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ProxyURL proxyURL in proxyURLs)
			{
				// NOTE: Removing check here to increase multi-validate performance.  Now, this check is done in order to get redirector in to "ready to publish" state.
				//if (proxyURL.IsQueryParameterValuesDuplicated())
				//{
				//    cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Not Validated: There is a Redirector that is already published with the same parameter values."));
				//}
				//else
				//{
					using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) 
					{
						proxyURL.Validate();
						scope.Complete(); // transaction complete
					}
					cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Validate Success"));
				//}
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlCPSLog.Visible = true;
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();
		}

		protected void btnPublish_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableProxyURL validators
				this.Page.Validate("PublishableProxyURL");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the redirectors (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ProxyURLCollection proxyURLs = this.GetProxyURLs();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ProxyURL proxyURL in proxyURLs)
			{
				// check to see if replacement
				// can't published the redirector to be replaced on the copied redirector
				if (proxyURL.IsOriginalProxyURLReplacement())
				{
					cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Not Published: Redirector cannot be unpublished because another redirector is replacing this one."));
					continue;
				}

				// NOTE: Removing check here to increase multi-publish performance.  Now, this check is done in order to get redirector in to "ready to publish" state.
				//if (proxyURL.IsQueryParameterValuesDuplicated())
				//{
				//    cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Not Published: There is a Redirector that is already published with the same parameter values."));
				//    continue;
				//}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					proxyURL.Publish();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Publish Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlCPSLog.Visible = true;
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();
		}

		protected void btnUnPublish_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the redirectors (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ProxyURLCollection proxyURLs = this.GetProxyURLs();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ProxyURL proxyURL in proxyURLs)
			{
				// check to see if replacement
				// can't published the redirector to be replaced on the copied redirector
				if (proxyURL.IsOriginalProxyURLReplacement())
				{
					cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Not Unpublished: Redirector cannot be unpublished because another redirector is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					proxyURL.UnPublish();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Unpublish Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlCPSLog.Visible = true;
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();
		}

		protected void btnRework_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the redirectors (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ProxyURLCollection proxyURLs = this.GetProxyURLs();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ProxyURL proxyURL in proxyURLs)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					proxyURL.SubmitBackToEditor();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Rework Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlCPSLog.Visible = true;
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();		
		}

		protected void cvURL_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string uriString = args.Value;
			Uri url;
			args.IsValid = Uri.IsWellFormedUriString(uriString, UriKind.Absolute) && Uri.TryCreate(uriString, UriKind.Absolute, out url);
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

		protected void cvProxyURLPublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			ProxyURLCollection proxyURLs = this.GetProxyURLs();

			foreach (ProxyURL proxyURL in proxyURLs)
			{
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
				if (!proxyURL.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles()))
				{
					args.IsValid = false;
					return;
				}
			}
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
			this.ddlOwner.InsertItem(0, "", Global.GetSelectListText());

			Global.BindProxyURLStatusListControl(this.ddlProxyURLStatus, RowStatus.RowStatusId.Active);
			this.ddlProxyURLStatus.InsertItem(0, "", Global.GetSelectListText());
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.lblIdText.Text = string.Empty;
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
			Person currentUser = PersonController.GetCurrentUser();
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

			if (bindItems.Count > 0)
			{
				int status = bindItems[0].ProxyURLStatusId;
				if (bindItems.All(x => x.ProxyURLStatusId == status))
				{
					ddlProxyURLStatus.ForceSelectedValue(status);
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the ProxyURL's current state
			List<UserRoleId> currentUsersRoles = PersonController.GetCurrentUser().GetRoles();
			bool isDataModificationAllowed = this.IsDataModificationAllowed();
			bool isMetadataModificationAllowed = this.IsMetadataModificationAllowed();

			// only allow change proxyURLType if there are no parameters
			this.ddlProxyURLType.Enabled = (bindItems.IsChildlessQueryParameterValue() && isDataModificationAllowed);

			// only allow change parameter if there is only a single proxyURLType
			if ((bindItems.Count > 0) && bindItems.IsIdenticalProxyURLType())
			{
				this.pnlDataControls_QueryParameterValueEditUpdate.Enabled = isDataModificationAllowed;
				this.ddlProxyURLType.ForceSelectedValue(bindItems[0].ProxyURLTypeId);
				this.ddlProxyURLType_SelectedIndexChanged();
			}
			else
			{
				this.pnlDataControls_QueryParameterValueEditUpdate.Enabled = false;
			}

			this.pnlEditArea.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSave.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSaveAsNew.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnDelete.Enabled =
				bindItems.IsStateTransitionAllowed(ProxyURLStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Enabled =
				bindItems.IsStateTransitionAllowed(ProxyURLStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Enabled =
				 bindItems.IsStateTransitionAllowed(ProxyURLStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Enabled =
				bindItems.IsStateTransitionAllowed(ProxyURLStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Enabled =
				bindItems.IsStateTransitionAllowed(ProxyURLStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
				bindItems.IsStateTransitionAllowed(ProxyURLStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Enabled =
				bindItems.IsStateTransitionAllowed(ProxyURLStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the ProxyURL Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
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

			this.pnlCPSLog.Visible = false;
			bool proxyURLModified = false;

			List<CPSLog> cpsLogList = new List<CPSLog>();
			ProxyURLCollection proxyURLs = this.GetProxyURLs();
			foreach (ProxyURL saveItem in proxyURLs)
			{
				proxyURLModified = false;
				if (dataModificationAllowed)
				{
					if (!string.IsNullOrEmpty(this.txtURL.Text.TrimToNull()))
					{
						saveItem.Url = this.txtURL.Text.TrimToNull();
						proxyURLModified = true;
					}

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

					//save the query parameter values
					int queryValuesCount = this.ucQueryParameterValueEditUpdatePanel.GetCountQueryParameterValues();
					if (queryValuesCount > 0)
					{
						if (!saveItem.IsQueryParameterValuesDuplicated(this.ucQueryParameterValueEditUpdatePanel.CreateQueryParameterValueIdDelimitedList()))
						{
							if (ucQueryParameterValueEditUpdatePanel.MultipleSaveInput(saveItem.Id) == true)
							{
								proxyURLModified = true;
							}
						}
						else
						{
							// just a warning but save it anyway
							cpsLogList.Add(new CPSLog(saveItem.Id, saveItem.Description, saveItem.Url, "Warning: Duplicate parameter values."));
							proxyURLModified = true;
						}
					}
					if (proxyURLModified)
					{
						saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
						saveItem.Log(Severity.Info, "ProxyURL data saved.");
					}
				}

				if (metadataModificationAllowed)
				{
					//update the Offer's Child Record metadata

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
				this.pnlCPSLog.Visible = true;
				this.ucCPSLog.CPSLogList = cpsLogList;
				this.ucCPSLog.DataBind();
			}
		}

		/// <summary>
		/// Saves all UI input/changes to a newly-created copy of the ProxyURL being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope())
			{
				//first create the new (copied) ProxyURL based upon the existing (pre-save) DB data
				ProxyURLCollection originalProxyURL = this.GetProxyURLs();
				ProxyURLCollection newProxyURLs = originalProxyURL.SaveAllAsNew(true);

				//next, update the state of the UI controls whose values should not affect the state of the new ProxyURL
				this.ddlProxyURLStatus.ForceSelectedValue(ProxyURLStateId.Modified);
				Global.ForceSelectedValue(this.ddlOwner, PersonController.GetCurrentUser());

				//Apply change:
				foreach (var proxy in newProxyURLs)
				{
					string description = txtDescription.Text.Trim();
					if (!string.IsNullOrEmpty(description))					
						proxy.Description = description;

					string url = txtURL.Text.Trim();
					if (!string.IsNullOrEmpty(url))
						proxy.Url = url;

					proxy.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
					ucQueryParameterValueEditUpdatePanel.MultipleSaveInput(proxy.Id);
				}
				//TODO:Tags??

				scope.Complete(); // transaction complete

				ProxyURLQuerySpecification proxyURLQuerySpecification = new ProxyURLQuerySpecification() { IdList = newProxyURLs.GetIds() };
				this.QuerySpecification = proxyURLQuerySpecification;
			}
		}

		/// <summary>
		/// Delete the ProxyURL being edited.
		/// </summary>
		private void Delete()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			using (TransactionScope scope = new TransactionScope())
			{
				//get proxyURL to delete
				ProxyURLCollection proxyURLs = this.GetProxyURLs();

				//Apply change:
				foreach (var proxyURL in proxyURLs)
				{
					if (proxyURL.ProxyURLState == ProxyURLStateId.Abandoned)
					{
						//delete proxyUrl and it's children
						proxyURL.Delete();
						cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Success Delete"));
					}
					else
					{
						cpsLogList.Add(new CPSLog(proxyURL.Id, proxyURL.Description, proxyURL.Url, "Unable to Delete: Redirector must be 'Abandoned'."));						
					}
				}
				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlProxyURLStatus.ForceSelectedValue(ProxyURLStateId.Deleted);
			}

			if (cpsLogList.Count > 0)
			{
				this.pnlCPSLog.Visible = true;
				this.ucCPSLog.CPSLogList = cpsLogList;
				this.ucCPSLog.DataBind();
			}
		}

		private VwMapProxyURLCollection GetDataSource()
		{
			ProxyURLQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapProxyURLCollection proxyURLCollection = VwMapProxyURLController.Fetch(query);
			if (proxyURLCollection.Count < 1)
			{
				//throw new InvalidOperationException("DataSource is invalid.");
				if (this.pnlEditArea.Enabled)
				{
					this.SetErrorMessage("Invalid data. No data matches the specified search criteria.");
					this.pnlEditArea.Enabled = false;
				}
			}
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
			this.pnlDataControls_QueryParameterValueEditUpdate.Visible = proxyURLTypeId != null;
		}

		#endregion

	}
}