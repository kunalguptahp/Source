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
	public partial class JumpstationGroupEditUpdatePanel : BaseQuerySpecificationEditDataUserControl
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
				JumpstationGroup.IsStateTransitionPossible(JumpstationGroupStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Visible =
				JumpstationGroup.IsStateTransitionPossible(JumpstationGroupStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Visible =
				JumpstationGroup.IsStateTransitionPossible(JumpstationGroupStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Visible =
				JumpstationGroup.IsStateTransitionPossible(JumpstationGroupStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Visible =
				JumpstationGroup.IsStateTransitionPossible(JumpstationGroupStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Visible =
				JumpstationGroup.IsStateTransitionPossible(JumpstationGroupStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Visible =
				JumpstationGroup.IsStateTransitionPossible(JumpstationGroupStateId.Validated, currentUsersRoles);
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

		protected void ddlJumpstationGroupType_SelectedIndexChanged(object sender, EventArgs e)
		{
			ddlJumpstationGroupType_SelectedIndexChanged();
		}

		private void ddlJumpstationGroupType_SelectedIndexChanged()
		{
			int JumpstationGroupTypeId = string.IsNullOrEmpty(this.ddlJumpstationGroupType.SelectedValue) ? 0 : Convert.ToInt32(this.ddlJumpstationGroupType.SelectedValue);
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
				this.GetJumpstationGroups().Abandon();
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

				//next, validate using the controls' PublishableJumpstationGroup validators
				this.Page.Validate("PublishableJumpstationGroup");
				if (!this.Page.IsValid)
				{
					return;
				}

				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the groups (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			JumpstationGroupCollection jumpstationGroups = this.GetJumpstationGroups();
			List<JumpstationLog> jumpstationLogList = new List<JumpstationLog>();
			foreach (JumpstationGroup jumpstationGroup in jumpstationGroups)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					jumpstationGroup.SubmitToValidator();
					scope.Complete(); // transaction complete
				}
				jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Ready to Validate Success"));
			}

			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlJumpstationLog.Visible = true;
			this.ucJumpstationLog.JumpstationLogList = jumpstationLogList;
			this.ucJumpstationLog.DataBind();
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

				//next, validate using the controls' Publishable Jumpstation validators
				this.Page.Validate("PublishableJumpstationGroup");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the jumpstations (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			JumpstationGroupCollection jumpstationGroups = this.GetJumpstationGroups();
			List<JumpstationLog> jumpstationLogList = new List<JumpstationLog>();
			foreach (JumpstationGroup jumpstationGroup in jumpstationGroups)
			{
					using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) 
					{
						jumpstationGroup.Validate();
						scope.Complete(); // transaction complete
					}
					jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Validate Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlJumpstationLog.Visible = true;
			this.ucJumpstationLog.JumpstationLogList = jumpstationLogList;
			this.ucJumpstationLog.DataBind();
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

				//next, validate using the controls' PublishableJumpstationGroup validators
				this.Page.Validate("PublishableJumpstationGroup");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the jumpstations (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			JumpstationGroupCollection jumpstationGroups = this.GetJumpstationGroups();
			List<JumpstationLog> jumpstationLogList = new List<JumpstationLog>();
			foreach (JumpstationGroup jumpstationGroup in jumpstationGroups)
			{
				// check to see if replacement
				// can't published the group to be replaced on the copied group
				if (jumpstationGroup.IsOriginalJumpstationGroupReplacement())
				{
					jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Not Published: Jumpstation cannot be unpublished because another one is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					jumpstationGroup.Publish();
					scope.Complete(); // transaction complete
				}
				jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Publish Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlJumpstationLog.Visible = true;
			this.ucJumpstationLog.JumpstationLogList = jumpstationLogList;
			this.ucJumpstationLog.DataBind();
		}

		protected void btnUnPublish_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

            //next, validate to make sure it's not already part of a replacement.
            this.Page.Validate("ReplacementJumpstationGroup");
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
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the jumpstations (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			JumpstationGroupCollection jumpstationGroups = this.GetJumpstationGroups();
			List<JumpstationLog> jumpstationLogList = new List<JumpstationLog>();
			foreach (JumpstationGroup jumpstationGroup in jumpstationGroups)
			{
				// check to see if replacement
				// can't published jumpstation to be replaced on the copied jumpstation
				if (jumpstationGroup.IsOriginalJumpstationGroupReplacement())
				{
					jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Not Unpublished: Jumpstation cannot be unpublished because another one is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					jumpstationGroup.UnPublish();
					scope.Complete(); // transaction complete
				}
				jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Unpublish Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlJumpstationLog.Visible = true;
			this.ucJumpstationLog.JumpstationLogList = jumpstationLogList;
			this.ucJumpstationLog.DataBind();
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
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the jumpstations (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			JumpstationGroupCollection jumpstationGroups = this.GetJumpstationGroups();
			List<JumpstationLog> jumpstationLogList = new List<JumpstationLog>();
			foreach (JumpstationGroup jumpstationGroup in jumpstationGroups)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					jumpstationGroup.SubmitBackToEditor();
					scope.Complete(); // transaction complete
				}
				jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Rework Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlJumpstationLog.Visible = true;
			this.ucJumpstationLog.JumpstationLogList = jumpstationLogList;
			this.ucJumpstationLog.DataBind();		
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

		protected void cvJumpstationGroupPublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			JumpstationGroupCollection jumpstationGroups = this.GetJumpstationGroups();
			foreach (JumpstationGroup jumpstationGroup in jumpstationGroups)
			{
				JumpstationGroupStateId toState;
				switch (jumpstationGroup.JumpstationGroupState)
				{
					case JumpstationGroupStateId.Modified:
					case JumpstationGroupStateId.Abandoned:
						toState = JumpstationGroupStateId.ReadyForValidation;
						break;
					case JumpstationGroupStateId.ReadyForValidation:
						toState = JumpstationGroupStateId.Validated;
						break;
					default:
						toState = JumpstationGroupStateId.Published;
						break;
				}
				if (!jumpstationGroup.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles()))
				{
					args.IsValid = false;
					return;
				}
			}
		}

        //protected void cvURL_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    string uriString = args.Value;
        //    Uri url;
        //    args.IsValid = Uri.IsWellFormedUriString(uriString, UriKind.Absolute) && Uri.TryCreate(uriString, UriKind.Absolute, out url);
        //}

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
			Global.BindJumpstationApplicationListControl(this.ddlJumpstationApplication, RowStatus.RowStatusId.Active);
			this.ddlJumpstationApplication.InsertItem(0, "", Global.GetSelectListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetSelectListText());

			Global.BindJumpstationGroupTypeListControl(this.ddlJumpstationGroupType, RowStatus.RowStatusId.Active);
			this.ddlJumpstationGroupType.InsertItem(0, "", Global.GetSelectListText());
			this.ddlJumpstationGroupType_SelectedIndexChanged();

			Global.BindJumpstationGroupStatusListControl(this.ddlJumpstationGroupStatus, RowStatus.RowStatusId.Active);
			this.ddlJumpstationGroupStatus.InsertItem(0, "", Global.GetSelectListText());
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
			this.ddlOwner.ClearSelection();
			this.ddlJumpstationApplication.ClearSelection();
			this.ddlJumpstationGroupType.ClearSelection();
			this.ddlJumpstationGroupType_SelectedIndexChanged();
			this.ddlJumpstationGroupStatus.ClearSelection();
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
		    this.txtURL.Text = string.Empty;
			this.txtTagsToAdd.Text = string.Empty;
			this.txtTagsToRemove.Text = string.Empty;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public JumpstationGroupQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as JumpstationGroupQuerySpecification ?? new JumpstationGroupQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			JumpstationGroupCollection bindItems = this.GetJumpstationGroups();
			Person currentUser = PersonController.GetCurrentUser();
			try
			{
				List<int> jumpstationGroupIds = bindItems.GetIds();
				jumpstationGroupIds.Sort();
				this.lblIdText.Text = string.Join(", ", jumpstationGroupIds.ToStrings().ToArray());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}

			if (bindItems.Count > 0)
			{
				int status = bindItems[0].JumpstationGroupStatusId;
				if (bindItems.All(x => x.JumpstationGroupStatusId == status))
				{
					ddlJumpstationGroupStatus.ForceSelectedValue(status);
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the JumpstationGroup's current state
			List<UserRoleId> currentUsersRoles = PersonController.GetCurrentUser().GetRoles();
			bool isDataModificationAllowed = this.IsDataModificationAllowed();
			bool isMetadataModificationAllowed = this.IsMetadataModificationAllowed();

			// only allow change parameter if there is only a single proxyURLType
			if ((bindItems.Count > 0) && bindItems.IsIdenticalJumpstationGroupType())
			{
				this.ddlJumpstationGroupType.ForceSelectedValue(bindItems[0].JumpstationGroupTypeId);
				this.ddlJumpstationGroupType_SelectedIndexChanged();
			}

			this.pnlEditArea.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSave.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSaveAsNew.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnDelete.Enabled =
				bindItems.IsStateTransitionAllowed(JumpstationGroupStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Enabled =
				bindItems.IsStateTransitionAllowed(JumpstationGroupStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Enabled =
				 bindItems.IsStateTransitionAllowed(JumpstationGroupStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Enabled =
				bindItems.IsStateTransitionAllowed(JumpstationGroupStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Enabled =
				bindItems.IsStateTransitionAllowed(JumpstationGroupStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
				bindItems.IsStateTransitionAllowed(JumpstationGroupStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Enabled =
				bindItems.IsStateTransitionAllowed(JumpstationGroupStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the Jumpstation Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
		}

		protected bool IsDataModificationAllowed()
		{
			return this.GetJumpstationGroups().IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			return this.GetJumpstationGroups().IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected override void SaveInput()
		{
			bool dataModificationAllowed = this.IsDataModificationAllowed();
			bool metadataModificationAllowed = this.IsMetadataModificationAllowed();

			if (!((dataModificationAllowed || metadataModificationAllowed)))
			{
				return; //don't allow any edits
			}

			this.pnlJumpstationLog.Visible = false;
			bool jumpstationGroupModified = false;

			List<JumpstationLog> jumpstationLogList = new List<JumpstationLog>();
			JumpstationGroupCollection jumpstationGroups = this.GetJumpstationGroups();
			foreach (JumpstationGroup saveItem in jumpstationGroups)
			{
				jumpstationGroupModified = false;
				if (dataModificationAllowed)
				{
					if (!string.IsNullOrEmpty(this.txtName.Text.TrimToNull()))
					{
						saveItem.Name = this.txtName.Text.TrimToNull();
						jumpstationGroupModified = true;
					}

					if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
					{
						saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
						jumpstationGroupModified = true;
					}

                    if (!string.IsNullOrEmpty(this.txtURL.Text.TrimToNull()))
                    {
                        saveItem.TargetURL = this.txtURL.Text.TrimToNull();
                        jumpstationGroupModified = true;
                    }

					if (!string.IsNullOrEmpty(this.ddlJumpstationGroupType.SelectedValue) && (this.ddlJumpstationGroupType.Enabled == true))
					{
						saveItem.JumpstationGroupTypeId = Convert.ToInt32(this.ddlJumpstationGroupType.SelectedValue);
						jumpstationGroupModified = true;
					}

					if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
					{
						saveItem.OwnerId = string.IsNullOrEmpty(this.ddlOwner.SelectedValue) ? PersonController.GetCurrentUser().Id : Convert.ToInt32(this.ddlOwner.SelectedValue);
						jumpstationGroupModified = true;
					}

					if (jumpstationGroupModified)
					{
						saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
						saveItem.Log(Severity.Info, "JumpstationGroup data saved.");
					}

				}

				if (metadataModificationAllowed)
				{
					//update the Child Record metadata
					if (this.pnlDataControls_TagsToAdd.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToAdd.Text))
						{
							saveItem.AddTags(Tag.ParseTagNameList(this.txtTagsToAdd.Text, false));
							jumpstationGroupModified = true;
						}
					}

					if (this.pnlDataControls_TagsToRemove.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToRemove.Text))
						{
							saveItem.RemoveTags(Tag.ParseTagNameList(this.txtTagsToRemove.Text, false));
							jumpstationGroupModified = true;
						}
					}
				}

				if (jumpstationGroupModified)
				{
					jumpstationLogList.Add(new JumpstationLog(saveItem.Id, saveItem.Description, saveItem.Name, "Success Saved"));
				}
			}

			if (jumpstationLogList.Count > 0)
			{
				this.pnlJumpstationLog.Visible = true;
				this.ucJumpstationLog.JumpstationLogList = jumpstationLogList;
				this.ucJumpstationLog.DataBind();
			}
		}

		/// <summary>
		/// Saves all UI input/changes to a newly-created copy of the Jumpstation being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope())
			{
				//first create the new (copied) Jumpstation based upon the existing (pre-save) DB data
				JumpstationGroupCollection originalJumpstationGroup = this.GetJumpstationGroups();
				JumpstationGroupCollection newJumpstationGroups = originalJumpstationGroup.SaveAllAsNew(true);

				//next, update the state of the UI controls whose values should not affect the state of the new JumpstationGroup
				this.ddlJumpstationGroupStatus.ForceSelectedValue(JumpstationGroupStateId.Modified);
				Global.ForceSelectedValue(this.ddlOwner, PersonController.GetCurrentUser());

				//Apply change:
                foreach (var jumpstationGroup in newJumpstationGroups)
                {
                    string name = txtName.Text.Trim();
                    if (!string.IsNullOrEmpty(name))
                        jumpstationGroup.Name = name;

                    string description = txtDescription.Text.Trim();
                    if (!string.IsNullOrEmpty(description))
                        jumpstationGroup.Description = description;

                    jumpstationGroup.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                    //save in JumpstationGroupPivot
                    ElementsCPS.Data.SubSonicClient.JumpstationGroupPivot pivot = JumpstationGroupPivot.FetchByID(jumpstationGroup.Id);
                    if (pivot == null)
                    {
                        ElementsCPS.Data.SubSonicClient.JumpstationGroupPivot newpivot = new JumpstationGroupPivot();
                        newpivot.JumpstationGroupId = Convert.ToInt32(jumpstationGroup.Id);

                        VwMapJumpstationGroupCalcOnFly origial = ElementsCPS.Data.SubSonicClient.VwMapJumpstationGroupCalcOnFlyController.FetchValue(newpivot.JumpstationGroupId);
                        newpivot.CopyFromCalcOnFly(origial);
                        newpivot.Save();
                    }
                }
                
				//TODO:Tags??

				scope.Complete(); // transaction complete
                
				JumpstationGroupQuerySpecification JumpstationGroupQuerySpecification = new JumpstationGroupQuerySpecification() { IdList = newJumpstationGroups.GetIds() };
				this.QuerySpecification = JumpstationGroupQuerySpecification;
			}
		}

		/// <summary>
		/// Delete the JumpstationGroup being edited.
		/// </summary>
		private void Delete()
		{
			List<JumpstationLog> jumpstationLogList = new List<JumpstationLog>();
			using (TransactionScope scope = new TransactionScope())
			{
				//get JumpstationGroup to delete
				JumpstationGroupCollection jumpstationGroups = this.GetJumpstationGroups();

				//Apply change:
				foreach (var jumpstationGroup in jumpstationGroups)
				{
					if (jumpstationGroup.JumpstationGroupState == JumpstationGroupStateId.Abandoned)
					{
						jumpstationGroup.Delete();

                        //get JumpstationGroupPivot to delete
                        ElementsCPS.Data.SubSonicClient.JumpstationGroupPivot.Delete(jumpstationGroup.Id);

						jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Success Delete"));
					}
					else
					{
						jumpstationLogList.Add(new JumpstationLog(jumpstationGroup.Id, jumpstationGroup.Description, jumpstationGroup.Name, "Unable to Delete: Group must be 'Abandoned'."));						
					}
				}
				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlJumpstationGroupStatus.ForceSelectedValue(JumpstationGroupStateId.Deleted);
			}

			if (jumpstationLogList.Count > 0)
			{
				this.pnlJumpstationLog.Visible = true;
				this.ucJumpstationLog.JumpstationLogList = jumpstationLogList;
				this.ucJumpstationLog.DataBind();
			}
		}

		private VwMapJumpstationGroupCollection GetDataSource()
		{
			JumpstationGroupQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapJumpstationGroupCollection jumpstationGroupCollection = VwMapJumpstationGroupController.Fetch(query);
			if (jumpstationGroupCollection.Count < 1)
			{
				if (this.pnlEditArea.Enabled)
				{
					this.SetErrorMessage("Invalid data. No data matches the specified search criteria.");
					this.pnlEditArea.Enabled = false;
				}
			}
			return jumpstationGroupCollection;
		}

		/// <summary>
		/// Gets the <see cref="JumpstationGroup"/>s corresponding to the items in the <see cref="VwMapJumpstationGroupCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private JumpstationGroupCollection GetJumpstationGroups()
		{
			return this.GetDataSource().GetJumpstationGroups();
		}

		#endregion

	}
}