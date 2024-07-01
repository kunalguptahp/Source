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
	public partial class ConfigurationServiceGroupEditUpdatePanel : BaseQuerySpecificationEditDataUserControl
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
				ConfigurationServiceGroup.IsStateTransitionPossible(ConfigurationServiceGroupStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Visible =
				ConfigurationServiceGroup.IsStateTransitionPossible(ConfigurationServiceGroupStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Visible =
				ConfigurationServiceGroup.IsStateTransitionPossible(ConfigurationServiceGroupStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Visible =
				ConfigurationServiceGroup.IsStateTransitionPossible(ConfigurationServiceGroupStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Visible =
				ConfigurationServiceGroup.IsStateTransitionPossible(ConfigurationServiceGroupStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Visible =
				ConfigurationServiceGroup.IsStateTransitionPossible(ConfigurationServiceGroupStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Visible =
				ConfigurationServiceGroup.IsStateTransitionPossible(ConfigurationServiceGroupStateId.Validated, currentUsersRoles);
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
				this.GetConfigurationServiceGroups().Abandon();
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

				//next, validate using the controls' PublishableConfigurationServiceGroup validators
				this.Page.Validate("PublishableConfigurationServiceGroup");
				if (!this.Page.IsValid)
				{
					return;
				}

				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the groups (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ConfigurationServiceGroupCollection configurationServiceGroups = this.GetConfigurationServiceGroups();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ConfigurationServiceGroup configurationServiceGroup in configurationServiceGroups)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					configurationServiceGroup.SubmitToValidator();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Ready to Validate Success"));
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

				//next, validate using the controls' PublishableConfigurationServiceGroup validators
				this.Page.Validate("PublishableConfigurationServiceGroup");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the groups (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ConfigurationServiceGroupCollection configurationServiceGroups = this.GetConfigurationServiceGroups();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ConfigurationServiceGroup configurationServiceGroup in configurationServiceGroups)
			{
					using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) 
					{
						configurationServiceGroup.Validate();
						scope.Complete(); // transaction complete
					}
					cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Validate Success"));
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

				//next, validate using the controls' PublishableConfigurationServiceGroup validators
				this.Page.Validate("PublishableConfigurationServiceGroup");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the groups (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ConfigurationServiceGroupCollection configurationServiceGroups = this.GetConfigurationServiceGroups();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ConfigurationServiceGroup configurationServiceGroup in configurationServiceGroups)
			{
				// check to see if replacement
				// can't published the group to be replaced on the copied group
				if (configurationServiceGroup.IsOriginalConfigurationServiceGroupReplacement())
				{
					cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Not Published: Group cannot be unpublished because another group is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					configurationServiceGroup.Publish();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Publish Success"));
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
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the groups (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ConfigurationServiceGroupCollection configurationServiceGroups = this.GetConfigurationServiceGroups();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ConfigurationServiceGroup configurationServiceGroup in configurationServiceGroups)
			{
				// check to see if replacement
				// can't published the group to be replaced on the copied group
				if (configurationServiceGroup.IsOriginalConfigurationServiceGroupReplacement())
				{
					cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Not Unpublished: Group cannot be unpublished because another group is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					configurationServiceGroup.UnPublish();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Unpublish Success"));
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
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the groups (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			ConfigurationServiceGroupCollection configurationServiceGroups = this.GetConfigurationServiceGroups();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (ConfigurationServiceGroup configurationServiceGroup in configurationServiceGroups)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					configurationServiceGroup.SubmitBackToEditor();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Rework Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlCPSLog.Visible = true;
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();		
		}

		protected void cvLabelValueRequired_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.ucConfigurationServiceLabelValue.IsValid();
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

		protected void cvConfigurationServiceGroupPublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			ConfigurationServiceGroupCollection configurationServiceGroups = this.GetConfigurationServiceGroups();

			foreach (ConfigurationServiceGroup configurationServiceGroup in configurationServiceGroups)
			{
				ConfigurationServiceGroupStateId toState;
				switch (configurationServiceGroup.ConfigurationServiceGroupState)
				{
					case ConfigurationServiceGroupStateId.Modified:
					case ConfigurationServiceGroupStateId.Abandoned:
						toState = ConfigurationServiceGroupStateId.ReadyForValidation;
						break;
					case ConfigurationServiceGroupStateId.ReadyForValidation:
						toState = ConfigurationServiceGroupStateId.Validated;
						break;
					default:
						toState = ConfigurationServiceGroupStateId.Published;
						break;
				}
				if (!configurationServiceGroup.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles()))
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
			Global.BindConfigurationServiceApplicationListControl(this.ddlConfigurationServiceApplication, RowStatus.RowStatusId.Active);
			this.ddlConfigurationServiceApplication.InsertItem(0, "", Global.GetSelectListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetSelectListText());

			Global.BindConfigurationServiceGroupTypeListControl(this.ddlConfigurationServiceGroupType, RowStatus.RowStatusId.Active);
			this.ddlConfigurationServiceGroupType.InsertItem(0, "", Global.GetSelectListText());

			Global.BindConfigurationServiceGroupStatusListControl(this.ddlConfigurationServiceGroupStatus, RowStatus.RowStatusId.Active);
			this.ddlConfigurationServiceGroupStatus.InsertItem(0, "", Global.GetSelectListText());
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
			this.ddlConfigurationServiceApplication.ClearSelection();
            this.ddlConfigurationServiceGroupType.ClearSelection();
			this.ddlConfigurationServiceGroupStatus.ClearSelection();
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
			this.txtTagsToAdd.Text = string.Empty;
			this.txtTagsToRemove.Text = string.Empty;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public ConfigurationServiceGroupQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as ConfigurationServiceGroupQuerySpecification ?? new ConfigurationServiceGroupQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			ConfigurationServiceGroupCollection bindItems = this.GetConfigurationServiceGroups();
			Person currentUser = PersonController.GetCurrentUser();
			try
			{
				List<int> configurationServiceGroupIds = bindItems.GetIds();
				configurationServiceGroupIds.Sort();
				this.lblIdText.Text = string.Join(", ", configurationServiceGroupIds.ToStrings().ToArray());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}

			if (bindItems.Count > 0)
			{
                ddlConfigurationServiceApplication.ForceSelectedValue(bindItems[0].ConfigurationServiceApplicationId);
                ddlConfigurationServiceGroupType.ForceSelectedValue(bindItems[0].ConfigurationServiceGroupTypeId);

                //this.pnlDataControls_ConfigurationServiceLabelValue.Visible = bindItems[0].ConfigurationServiceApplicationId != 0;
                this.ucConfigurationServiceLabelValue.ConfigurationServiceGroupTypeId = bindItems[0].ConfigurationServiceGroupTypeId;
                this.ucConfigurationServiceLabelValue.DataBind();
	
                int status = bindItems[0].ConfigurationServiceGroupStatusId;
				if (bindItems.All(x => x.ConfigurationServiceGroupStatusId == status))
				{
					ddlConfigurationServiceGroupStatus.ForceSelectedValue(status);
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the ConfigurationServiceGroup's current state
			List<UserRoleId> currentUsersRoles = PersonController.GetCurrentUser().GetRoles();
			bool isDataModificationAllowed = this.IsDataModificationAllowed();
			bool isMetadataModificationAllowed = this.IsMetadataModificationAllowed();

			this.pnlEditArea.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSave.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnSaveAsNew.Enabled =
				isDataModificationAllowed || isMetadataModificationAllowed;
			this.btnDelete.Enabled =
				bindItems.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Enabled =
				bindItems.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Enabled =
				 bindItems.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Enabled =
				bindItems.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Enabled =
				bindItems.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
				bindItems.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Enabled =
				bindItems.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the ConfigurationServiceGroup Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
		}

		protected bool IsDataModificationAllowed()
		{
			return this.GetConfigurationServiceGroups().IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			return this.GetConfigurationServiceGroups().IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
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
			bool configurationServiceGroupModified = false;

			List<CPSLog> cpsLogList = new List<CPSLog>();
			ConfigurationServiceGroupCollection configurationServiceGroups = this.GetConfigurationServiceGroups();
			foreach (ConfigurationServiceGroup saveItem in configurationServiceGroups)
			{
				configurationServiceGroupModified = false;
				if (dataModificationAllowed)
				{
					if (!string.IsNullOrEmpty(this.txtName.Text.TrimToNull()))
					{
						saveItem.Name = this.txtName.Text.TrimToNull();
						configurationServiceGroupModified = true;
					}

					if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
					{
						saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
						configurationServiceGroupModified = true;
					}

                    if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
					{
						saveItem.OwnerId = string.IsNullOrEmpty(this.ddlOwner.SelectedValue) ? PersonController.GetCurrentUser().Id : Convert.ToInt32(this.ddlOwner.SelectedValue);
						configurationServiceGroupModified = true;
					}

					if (configurationServiceGroupModified)
					{
						saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
						saveItem.Log(Severity.Info, "ConfigurationServiceGroup data saved.");
					}

					//save the label values
					this.ucConfigurationServiceLabelValue.MultipleSaveInput(saveItem.Id);
				}

				if (metadataModificationAllowed)
				{
					//update the Child Record metadata
					if (this.pnlDataControls_TagsToAdd.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToAdd.Text))
						{
							saveItem.AddTags(Tag.ParseTagNameList(this.txtTagsToAdd.Text, false));
							configurationServiceGroupModified = true;
						}
					}

					if (this.pnlDataControls_TagsToRemove.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToRemove.Text))
						{
							saveItem.RemoveTags(Tag.ParseTagNameList(this.txtTagsToRemove.Text, false));
							configurationServiceGroupModified = true;
						}
					}
				}

				if (configurationServiceGroupModified)
				{
					cpsLogList.Add(new CPSLog(saveItem.Id, saveItem.Description, saveItem.Name, "Success Saved"));
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
		/// Saves all UI input/changes to a newly-created copy of the ConfigurationServiceGroup being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope())
			{
				//first create the new (copied) ConfigurationServiceGroup based upon the existing (pre-save) DB data
				ConfigurationServiceGroupCollection originalConfigurationServiceGroup = this.GetConfigurationServiceGroups();
				ConfigurationServiceGroupCollection newConfigurationServiceGroups = originalConfigurationServiceGroup.SaveAllAsNew(true);

				//Apply change:
				foreach (var configurationServiceGroup in newConfigurationServiceGroups)
				{
					string name = txtName.Text.Trim();
					if (!string.IsNullOrEmpty(name))
						configurationServiceGroup.Name = name;

					string description = txtDescription.Text.Trim();
					if (!string.IsNullOrEmpty(description))
						configurationServiceGroup.Description = description;

                    if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
                    {
                        configurationServiceGroup.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
                    }

					configurationServiceGroup.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                    //save the label values
                    this.ucConfigurationServiceLabelValue.MultipleSaveInput(configurationServiceGroup.Id);
                }
				//TODO:Tags??

				scope.Complete(); // transaction complete

                //next, update the state of the UI controls whose values should not affect the state of the new ConfigurationServiceGroup
                this.ddlConfigurationServiceGroupStatus.ForceSelectedValue(ConfigurationServiceGroupStateId.Modified);

				ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification = new ConfigurationServiceGroupQuerySpecification() { IdList = newConfigurationServiceGroups.GetIds() };
				this.QuerySpecification = configurationServiceGroupQuerySpecification;
			}
		}

		/// <summary>
		/// Delete the ConfigurationServiceGroup being edited.
		/// </summary>
		private void Delete()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			using (TransactionScope scope = new TransactionScope())
			{
				//get configurationServiceGroup to delete
				ConfigurationServiceGroupCollection configurationServiceGroups = this.GetConfigurationServiceGroups();

				//Apply change:
				foreach (var configurationServiceGroup in configurationServiceGroups)
				{
					if (configurationServiceGroup.ConfigurationServiceGroupState == ConfigurationServiceGroupStateId.Abandoned)
					{
						configurationServiceGroup.Delete();
						cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Success Delete"));
					}
					else
					{
						cpsLogList.Add(new CPSLog(configurationServiceGroup.Id, configurationServiceGroup.Description, configurationServiceGroup.Name, "Unable to Delete: Group must be 'Abandoned'."));						
					}
				}
				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlConfigurationServiceGroupStatus.ForceSelectedValue(ConfigurationServiceGroupStateId.Deleted);
			}

			if (cpsLogList.Count > 0)
			{
				this.pnlCPSLog.Visible = true;
				this.ucCPSLog.CPSLogList = cpsLogList;
				this.ucCPSLog.DataBind();
			}
		}

		private VwMapConfigurationServiceGroupCollection GetDataSource()
		{
			ConfigurationServiceGroupQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapConfigurationServiceGroupCollection configurationServiceGroupCollection = VwMapConfigurationServiceGroupController.Fetch(query);
			if (configurationServiceGroupCollection.Count < 1)
			{
				if (this.pnlEditArea.Enabled)
				{
					this.SetErrorMessage("Invalid data. No data matches the specified search criteria.");
					this.pnlEditArea.Enabled = false;
				}
			}
			return configurationServiceGroupCollection;
		}

		/// <summary>
		/// Gets the <see cref="ConfigurationServiceGroup"/>s corresponding to the items in the <see cref="VwMapConfigurationServiceGroupCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private ConfigurationServiceGroupCollection GetConfigurationServiceGroups()
		{
			return this.GetDataSource().GetConfigurationServiceGroups();
		}

		#endregion

	}
}