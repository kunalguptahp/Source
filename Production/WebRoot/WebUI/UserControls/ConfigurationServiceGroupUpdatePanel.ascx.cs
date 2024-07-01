using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Web.UI;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class ConfigurationServiceGroupUpdatePanel : RecordDetailUserControl
	{
        List<string> selectedAppClient = new List<string>();

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
			if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
			{
				return;
			}

			this.OnInputSave(new EventArgs());
		}

		protected void btnSaveAsNew_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
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

        protected void ddlConfigurationServiceApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlConfigurationServiceApplication_SelectedIndexChanged();
        }

        private void ddlConfigurationServiceApplication_SelectedIndexChanged()
        {
           
            Global.BindConfigurationServiceGroupTypeListControl(this.ddlConfigurationServiceGroupType, (this.ddlConfigurationServiceApplication.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlConfigurationServiceApplication.SelectedValue)), RowStatus.RowStatusId.Active);
            this.ddlConfigurationServiceGroupType.InsertItem(0, "", Global.GetSelectListText());

            ddlConfigurationServiceGroupType_SelectedIndexChanged();
        }

		protected void ddlConfigurationServiceGroupType_SelectedIndexChanged(object sender, EventArgs e)
		{
			ddlConfigurationServiceGroupType_SelectedIndexChanged();
		}

		private void ddlConfigurationServiceGroupType_SelectedIndexChanged()
		{
			int configurationServiceGroupTypeId = string.IsNullOrEmpty(this.ddlConfigurationServiceGroupType.SelectedValue) ? 0 : Convert.ToInt32(this.ddlConfigurationServiceGroupType.SelectedValue);
			this.pnlDataControls_ConfigurationServiceLabelValue.Visible = configurationServiceGroupTypeId != 0;
			this.ucConfigurationServiceLabelValue.ConfigurationServiceGroupTypeId = configurationServiceGroupTypeId;
            this.ucConfigurationServiceLabelValue.ConfigurationServiceApplicationId = string.IsNullOrEmpty(this.ddlConfigurationServiceApplication.SelectedValue) ? 0 : Convert.ToInt32(this.ddlConfigurationServiceApplication.SelectedValue);
			this.ucConfigurationServiceLabelValue.DataBind();
			this.UpdateChildListImmutableConditions();
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
			if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableConfigurationServiceGroup validators
				this.Page.Validate("PublishableConfigurationServiceGroup");
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
			if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableConfigurationServiceGroup validators
				this.Page.Validate("PublishableConfigurationServiceGroup");
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
			if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableConfigurationServiceGroup validators
				this.Page.Validate("PublishableConfigurationServiceGroup");
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
                //first, save the item like btnSave would do
				this.SaveInput();

                //next, validate to make sure it's not already part of a replacement.
                this.Page.Validate("ConfigurationServiceGroupReplacement");
                if (!this.Page.IsValid)
                {
                    return;
                }
                
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

		protected void cvLabelValueRequired_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.ucConfigurationServiceLabelValue.IsValid();
		}

		protected void cvTxtTagsValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
		}

		protected void cvTxtTagsMaxTagCount_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (Tag.ParseTagNameList(args.Value, false).Count() <= 100);
		}

		protected void cvConfigurationServiceGroupPublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			ConfigurationServiceGroup configurationServiceGroup = this.GetDataSource();
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
			args.IsValid = configurationServiceGroup.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles());
		}

        protected void cvConfigurationServiceGroupReplacement_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ConfigurationServiceGroup configurationServiceGroup = this.GetDataSource();
            args.IsValid = !(configurationServiceGroup.IsOriginalConfigurationServiceGroupReplacement());
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
        /// rebind ddlAppClient dropdownlist for selected value
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            Control control = null;
            string ctrlname = this.Page.Request.Params.Get("__EVENTTARGET");
            if (ctrlname != null && ctrlname != string.Empty)
            {
                control = this.Page.FindControl(ctrlname);
            }
            else
            {
                foreach (string ctl in this.Page.Request.Form)
                {
                    Control c = this.Page.FindControl(ctl);
                    if (c is System.Web.UI.WebControls.Button)
                    {
                        if (selectedAppClient.Count != 0)
                        {
                        this.ddlAppClient.SelectedIndex = this.ddlAppClient.Items.IndexOf(this.ddlAppClient.Items.FindByValue(selectedAppClient[0]));
                        }
                    }
                }
            }

        }

		/// <summary>
		/// Re-populates the ListItems of all this control's ListControls.
		/// </summary>
		/// <remarks>
		/// This method should be called every time the control is Un-bound/Re-bound so that any changes made for a previous DataSource don't persist.
		/// </remarks>
		private void PopulateListControls()
		{
            string windowsId = this.Page.User.Identity.Name.ToString();
            int tenantId = PersonController.GetCurrentUser().TenantGroupId;
            bool isNewRecord = this.IsNewRecord;
            Global.BindConfigurationServiceApplicationListControl(this.ddlConfigurationServiceApplication, RowStatus.RowStatusId.Active, tenantId, isNewRecord);
			this.ddlConfigurationServiceApplication.InsertItem(0, "", Global.GetSelectListText());

			Global.BindConfigurationServiceGroupTypeListControl(this.ddlConfigurationServiceGroupType, RowStatus.RowStatusId.Active);
			this.ddlConfigurationServiceGroupType.InsertItem(0, "", Global.GetSelectListText());
			this.ddlConfigurationServiceGroupType_SelectedIndexChanged();

            Global.BindAppClientListControl(this.ddlAppClient);
            this.ddlAppClient.InsertItem(0, "", Global.GetSelectListText());


			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			Global.BindConfigurationServiceGroupStatusListControl(this.ddlConfigurationServiceGroupStatus, RowStatus.RowStatusId.Active);
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
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
			this.ddlOwner.ClearSelection();
			this.ddlConfigurationServiceApplication.ClearSelection();
            ddlConfigurationServiceApplication_SelectedIndexChanged();
            this.ddlConfigurationServiceGroupType.ClearSelection();
			ddlConfigurationServiceGroupType_SelectedIndexChanged();
            this.ddlAppClient.ClearSelection();
			this.ddlConfigurationServiceGroupStatus.ClearSelection();
			this.txtTags.Text = string.Empty;
		}

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			//default the Configuration Service Group Status to Modified
			this.ddlConfigurationServiceGroupStatus.ClearSelection();
			this.ddlConfigurationServiceGroupStatus.ForceSelectedValue(ConfigurationServiceGroupStateId.Modified);

			//default the Configuration Service Group to current user
			Person currentUser = PersonController.GetCurrentUser();
			Global.ForceSelectedValue(this.ddlOwner, currentUser);
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

			ConfigurationServiceGroup bindItem = this.GetDataSource();

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
					this.ddlConfigurationServiceGroupStatus.ForceSelectedValue(bindItem.ConfigurationServiceGroupStatusId);
					this.ddlOwner.ForceSelectedValue(bindItem.OwnerId);
                    this.ddlConfigurationServiceApplication.ForceSelectedValue(bindItem.ConfigurationServiceApplicationId);
                    this.ddlConfigurationServiceApplication_SelectedIndexChanged();
                    this.ddlConfigurationServiceGroupType.ForceSelectedValue(bindItem.ConfigurationServiceGroupTypeId);
					this.txtName.Text = bindItem.Name;
					this.txtDescription.Text = bindItem.Description;

                    this.ddlAppClient.ForceSelectedValue(bindItem.AppClient.Name.ToString());

					List<string> tagNames = bindItem.TagNames;
                    this.txtTags.Text = (tagNames == null) ? "" : string.Join(", ", tagNames.ToArray());

					this.pnlDataControls_ConfigurationServiceLabelValue.Visible = true;
					this.ucConfigurationServiceLabelValue.ConfigurationServiceGroupTypeId = bindItem.ConfigurationServiceGroupTypeId;
                    this.ucConfigurationServiceLabelValue.ConfigurationServiceApplicationId = bindItem.ConfigurationServiceApplicationId;
                    this.ucConfigurationServiceLabelValue.DataBind();
				}
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the ConfigurationServiceGroup's current state
            int personId = PersonController.GetCurrentUser().Id;
			List<UserRoleId> currentUsersRoles = PersonController.GetCurrentUser().GetRoles();
			bool isDataModificationAllowed = this.IsDataModificationAllowed();
			bool isMetadataModificationAllowed = this.IsMetadataModificationAllowed();

            List<RoleApplicationId> app1 = new List<RoleApplicationId>();
            List<RoleApplicationId> app2 = new List<RoleApplicationId>();
            if (currentUsersRoles.Contains(UserRoleId.Editor))
            {
                app1 = ApplicationRoleController.GetCurrentAppListByRole(personId, UserRoleId.Editor);
            }

            if (currentUsersRoles.Contains(UserRoleId.Validator))
            {
                app2 = ApplicationRoleController.GetCurrentAppListByRole(personId, UserRoleId.Validator);
            }

			this.pnlEditArea.Enabled =
                isDataModificationAllowed || isMetadataModificationAllowed ;
			this.btnSave.Enabled =
                (isDataModificationAllowed || isMetadataModificationAllowed) && (app1.Contains(RoleApplicationId.Default) || app1.Contains(RoleApplicationId.ConfigureService)||app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.ConfigureService));
			this.btnSaveAsNew.Enabled =
				!this.IsNewRecord;
			this.btnDelete.Enabled =
                bindItem.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Deleted, currentUsersRoles) ;
			this.btnAbandon.Enabled =
                bindItem.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Abandoned, currentUsersRoles) && (app1.Contains(RoleApplicationId.Default) || app1.Contains(RoleApplicationId.ConfigureService) || app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.ConfigureService));
			this.btnPublish.Enabled =
                bindItem.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Published, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.ConfigureService));
			this.btnReadyForValidation.Enabled =
                bindItem.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.ReadyForValidation, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.ConfigureService));
			this.btnRework.Enabled =
				bindItem.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
                bindItem.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Cancelled, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.ConfigureService));
			this.btnValidate.Enabled =
				bindItem.IsStateTransitionAllowed(ConfigurationServiceGroupStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the ConfigurationServiceGroup Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
		}

		protected bool IsDataModificationAllowed()
		{
			ConfigurationServiceGroup bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;
			else
				return bindItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			ConfigurationServiceGroup bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;
			else
				return bindItem.IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

        protected bool HasGroupSelector()
        {
            ConfigurationServiceGroup bindItem = this.GetDataSource();
            if (bindItem == null)
                return false;
            else
                return bindItem.HasGroupSelector();
        }

		protected override void SaveInput()
		{
			bool dataModificationAllowed = this.IsDataModificationAllowed();
			bool metadataModificationAllowed = this.IsMetadataModificationAllowed();

			if (!((dataModificationAllowed || metadataModificationAllowed)))
			{
				return; //don't allow any edits
			}

			bool newRecord = false;
			ConfigurationServiceGroup saveItem;
			if (this.DataSourceId != null)
			{
				saveItem = ConfigurationServiceGroup.FetchByID(this.DataSourceId);
			}
			else
			{
				newRecord = true;
				saveItem = new ConfigurationServiceGroup(true);
			}

			if (dataModificationAllowed)
			{
				saveItem.Name = this.txtName.Text.TrimToNull();
                saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
				saveItem.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
                saveItem.AppClientId = Convert.ToInt32(AppClientController.FetchByName(this.ddlAppClient.SelectedValue).Id);
                selectedAppClient.Add(this.ddlAppClient.SelectedValue);
				saveItem.ConfigurationServiceApplicationId = Convert.ToInt32(this.ddlConfigurationServiceApplication.SelectedValue);
				saveItem.ConfigurationServiceGroupTypeId = Convert.ToInt32(this.ddlConfigurationServiceGroupType.SelectedValue);
				saveItem.ConfigurationServiceGroupStatusId = Convert.ToInt32(this.ddlConfigurationServiceGroupStatus.SelectedValue);
				saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
			}

			if (metadataModificationAllowed)
			{
				//update the ConfigurationServiceGroup's Child Record metadata
				if (this.pnlDataControls_Tags.Enabled)
				{
                    
					saveItem.SetTags(Tag.ParseTagNameList(this.txtTags.Text, false));
				}
			}

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;

			if (dataModificationAllowed)
			{
				//save configuration service label values
				this.ucConfigurationServiceLabelValue.SaveUpdates();

				//add configuration service group selector wildcard default for any new group selector
				if (newRecord)
				{
                    saveItem.AddConfigurationServiceGroupSelectorWildcardDefault();
				}
			}
		}

		/// <summary>
		/// Saves all UI input/changes to a newly-created copy of the ConfigurationServiceGroup being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first create the new (copied) ConfigurationServiceGroup based upon the existing (pre-save) DB data
				ConfigurationServiceGroup originalConfigurationServiceGroup = this.GetDataSource();
				ConfigurationServiceGroup newConfigurationServiceGroup = originalConfigurationServiceGroup.SaveAsNew(true);

				//next, change the control's DataSourceId to reference the new ConfigurationServiceGroup
				this.DataSourceId = newConfigurationServiceGroup.Id;

				//next, update the state of the UI controls whose values should not affect the state of the new ConfigurationServiceGroup
				this.ddlConfigurationServiceGroupStatus.ForceSelectedValue(newConfigurationServiceGroup.ConfigurationServiceGroupState);
				this.ddlOwner.ForceSelectedValue(newConfigurationServiceGroup.OwnerId);

				//next, call SaveItem() to apply any UI input/changes (except for those UI controls re-bound above) to the new ConfigurationServiceGroup (only)
				this.SaveInput();

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Delete the ConfigurationServiceGroup being edited.
		/// </summary>
		private void Delete()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//get configurationServiceGroup to delete
				ConfigurationServiceGroup configurationServiceGroup = this.GetDataSource();

				//delete configurationServiceGroup and it's children
				configurationServiceGroup.Delete();

				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlConfigurationServiceGroupStatus.ForceSelectedValue(ConfigurationServiceGroupStateId.Deleted);
			}
		}

		private ConfigurationServiceGroup GetDataSource()
		{
			ConfigurationServiceGroup saveItem;
			if (!this.IsNewRecord)
			{
				saveItem = ConfigurationServiceGroup.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new ConfigurationServiceGroup(true); 
				saveItem.ConfigurationServiceGroupState = ConfigurationServiceGroupStateId.Modified;
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
			this.UpdateConfigurationServiceGroupSelectorListImmutableConditions();
		}

		private void UpdateConfigurationServiceGroupSelectorListImmutableConditions()
		{
			this.pnlConfigurationServiceGroupSelectorListPanel.Visible = this.DataSourceId != null;
			
			this.ucConfigurationServiceGroupSelectorList.ImmutableQueryConditions = new ConfigurationServiceGroupSelectorQuerySpecification {ConfigurationServiceGroupId = (this.DataSourceId ?? -1) };
		}

		#endregion
	}
}