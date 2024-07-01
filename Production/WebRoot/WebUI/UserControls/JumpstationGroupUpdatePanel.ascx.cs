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
    public partial class JumpstationGroupUpdatePanel : RecordDetailUserControl
    {
        List<string> selectedAppClient = new List<string>();
        List<string> selectedAppType = new List<string>();
        List<string> selectedApp = new List<string>();
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

        protected void ddlJumpstationApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlJumpstationApplication_SelectedIndexChanged();
        }

        private void ddlJumpstationApplication_SelectedIndexChanged()
        {
            Global.BindJumpstationGroupTypeListControl(this.ddlJumpstationGroupType, (this.ddlJumpstationApplication.SelectedValue == "" ? 0 : Convert.ToInt32(JumpstationApplicationController.FetchByName(this.ddlJumpstationApplication.SelectedValue).Id)), RowStatus.RowStatusId.Active);
            this.ddlJumpstationGroupType.InsertItem(0, "", Global.GetSelectListText());

            ddlJumpstationGroupType_SelectedIndexChanged();
        }

        protected void ddlJumpstationGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlJumpstationGroupType_SelectedIndexChanged();
        }

        
        private void ddlJumpstationGroupType_SelectedIndexChanged()
        {
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
            if (!this.Page.IsValid)
            {
                return;
            }

            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                //first, save the item like btnSave would do
                this.SaveInput();

                //next, validate using the controls' PublishableJumpstationGroup validators
                this.Page.Validate("PublishableJumpstationGroup");
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

                //next, validate using the controls' PublishableJumpstationGroup validators
                this.Page.Validate("PublishableJumpstationGroup");
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

                //next, validate using the controls' PublishableJumpstationGroup validators
                this.Page.Validate("PublishableJumpstationGroup");
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
                this.Page.Validate("JumpstationGroupReplacement");
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

        //protected void cvURL_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    string uriString = args.Value;
        //    Uri url;
        //    args.IsValid = Uri.IsWellFormedUriString(uriString, UriKind.Absolute) && Uri.TryCreate(uriString, UriKind.Absolute, out url);
        //}

        protected void cvTxtTagsValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
        }

        protected void cvTxtTagsMaxTagCount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (Tag.ParseTagNameList(args.Value, false).Count() <= 100);
        }

        protected void cvJumpstationGroupPublishable_ServerValidate(object source, ServerValidateEventArgs args)
        {
            JumpstationGroup JumpstationGroup = this.GetDataSource();
            JumpstationGroupStateId toState;
            switch (JumpstationGroup.JumpstationGroupState)
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
            args.IsValid = JumpstationGroup.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles());
        }

        protected void cvJumpstationGroupReplacement_ServerValidate(object source, ServerValidateEventArgs args)
        {
            JumpstationGroup jumpstationGroup = this.GetDataSource();
            args.IsValid = !(jumpstationGroup.IsOriginalJumpstationGroupReplacement());
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
                        if (selectedAppType.Count != 0)
                        {
                            this.ddlJumpstationGroupType.SelectedIndex = this.ddlJumpstationGroupType.Items.IndexOf(this.ddlJumpstationGroupType.Items.FindByValue(selectedAppType[0]));
                        }
                        if (selectedApp.Count != 0)
                        {
                            this.ddlJumpstationApplication.SelectedIndex = this.ddlJumpstationApplication.Items.IndexOf(this.ddlJumpstationApplication.Items.FindByValue(selectedApp[0]));
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
            
            Global.BindJumpstationApplicationListControl(this.ddlJumpstationApplication, RowStatus.RowStatusId.Active, tenantId);
            this.ddlJumpstationApplication.InsertItem(0, "", Global.GetSelectListText());

            
            Global.BindJumpstationGroupTypeListControl(this.ddlJumpstationGroupType, RowStatus.RowStatusId.Active,tenantId);
            this.ddlJumpstationGroupType.InsertItem(0, "", Global.GetSelectListText());
            this.ddlJumpstationApplication_SelectedIndexChanged();

            //string windowsId = this.Page.User.Identity.Name.ToString();
            Global.BindAppClientListControl(this.ddlAppClient);
            this.ddlAppClient.InsertItem(0, "", Global.GetSelectListText());

            Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
            Global.BindJumpstationGroupStatusListControl(this.ddlJumpstationGroupStatus, RowStatus.RowStatusId.Active);
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
            this.txtTargetURL.Text = string.Empty;
            this.ddlOwner.ClearSelection();
            this.txtOrder.Text = "1";
            this.ddlJumpstationApplication.ClearSelection();
            this.ddlJumpstationGroupType.ClearSelection();
            this.ddlJumpstationGroupType_SelectedIndexChanged();
            this.ddlAppClient.ClearSelection();
            this.ddlJumpstationGroupStatus.ClearSelection();
            this.txtTags.Text = string.Empty;
        }

        /// <summary>
        /// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
        /// is appropriate (for a new, non-existing record) for each such control.
        /// </summary>
        private void ApplyDataControlDefaultValues()
        {
            //default the Configuration Service Group Status to Modified
            this.ddlJumpstationGroupStatus.ClearSelection();
            this.ddlJumpstationGroupStatus.ForceSelectedValue(JumpstationGroupStateId.Modified);

            //default the Configuration Service Group to current user
            Person currentUser = PersonController.GetCurrentUser();
            Global.ForceSelectedValue(this.ddlOwner, currentUser);
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

            JumpstationGroup bindItem = this.GetDataSource();

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
                    this.ddlJumpstationGroupStatus.ForceSelectedValue(bindItem.JumpstationGroupStatusId);
                    this.ddlOwner.ForceSelectedValue(bindItem.OwnerId);
                    this.txtOrder.Text = (bindItem.Order.ToString());
                    this.ddlJumpstationApplication.ForceSelectedValue(bindItem.JumpstationApplication.Name.ToString());
                    this.ddlJumpstationGroupType.ForceSelectedValue(bindItem.JumpstationGroupType.Name.ToString());
                    this.txtName.Text = bindItem.Name;
                    this.txtDescription.Text = bindItem.Description;
                    this.txtTargetURL.Text = bindItem.TargetURL.TrimToNull();
                
                    this.ddlAppClient.ForceSelectedValue(bindItem.AppClient.Name.ToString());
                    

                    List<string> tagNames = bindItem.TagNames;
                    this.txtTags.Text = (tagNames == null) ? "" : string.Join(", ", tagNames.ToArray());
                }
                catch (SqlException ex)
                {
                    LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
                    throw;
                }
            }

            //enable/disable and hide/show controls based upon the user's roles and the JumpstationGroup's current state
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
                isDataModificationAllowed || isMetadataModificationAllowed;
            this.btnSave.Enabled =
                (isDataModificationAllowed || isMetadataModificationAllowed) && (app1.Contains(RoleApplicationId.Default) || app1.Contains(RoleApplicationId.Jumpstation)||app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Jumpstation));
            this.btnSaveAsNew.Enabled =
                !this.IsNewRecord;
            this.btnDelete.Enabled =
                bindItem.IsStateTransitionAllowed(JumpstationGroupStateId.Deleted, currentUsersRoles) ;
            this.btnAbandon.Enabled =
                bindItem.IsStateTransitionAllowed(JumpstationGroupStateId.Abandoned, currentUsersRoles) && (app1.Contains(RoleApplicationId.Default) || app1.Contains(RoleApplicationId.Jumpstation) || app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Jumpstation));
            this.btnPublish.Enabled =
                bindItem.IsStateTransitionAllowed(JumpstationGroupStateId.Published, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Jumpstation));
            this.btnReadyForValidation.Enabled =
                bindItem.IsStateTransitionAllowed(JumpstationGroupStateId.ReadyForValidation, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Jumpstation));
            this.btnRework.Enabled =
                bindItem.IsStateTransitionAllowed(JumpstationGroupStateId.Modified, currentUsersRoles);
            this.btnUnPublish.Enabled =
                bindItem.IsStateTransitionAllowed(JumpstationGroupStateId.Cancelled, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Jumpstation));
            this.btnValidate.Enabled =
                bindItem.IsStateTransitionAllowed(JumpstationGroupStateId.Validated, currentUsersRoles );

            //only Coordinators are allowed to set or change the JumpstationGroup Owner
            this.ddlOwner.Enabled =
                SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
        }

        protected bool IsDataModificationAllowed()
        {
            JumpstationGroup bindItem = this.GetDataSource();
            //PersonController.GetCurrentUser().GetRoles().
            if (bindItem == null)
                return false;
            else
                return bindItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles() );
        }

        protected bool IsMetadataModificationAllowed()
        {
            JumpstationGroup bindItem = this.GetDataSource();
            if (bindItem == null)
                return false;
            else
                return bindItem.IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
        }

        protected bool HasGroupSelector()
        {
            JumpstationGroup bindItem = this.GetDataSource();
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

            JumpstationGroup saveItem;
            if (this.DataSourceId != null)
            {
                saveItem = JumpstationGroup.FetchByID(this.DataSourceId);
            }
            else
            {
                saveItem = new JumpstationGroup(true);
            }

            if (dataModificationAllowed)
            {
                saveItem.Name = this.txtName.Text.TrimToNull();
                saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
                saveItem.TargetURL = this.txtTargetURL.Text.TrimToNull();
                saveItem.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
                saveItem.AppClientId = Convert.ToInt32(AppClientController.FetchByName(this.ddlAppClient.SelectedValue).Id);
                selectedAppClient.Add(this.ddlAppClient.SelectedValue);
                
                saveItem.Order = Convert.ToInt32(this.txtOrder.Text);
                //saveItem.JumpstationApplicationId = Convert.ToInt32(this.ddlJumpstationApplication.SelectedValue);
                saveItem.JumpstationApplicationId = Convert.ToInt32(JumpstationApplicationController.FetchByName(this.ddlJumpstationApplication.SelectedValue).Id);
                selectedApp.Add(this.ddlJumpstationApplication.SelectedValue);
                //saveItem.JumpstationGroupTypeId = Convert.ToInt32(this.ddlJumpstationGroupType.SelectedValue);
                saveItem.JumpstationGroupTypeId = Convert.ToInt32(JumpstationGroupTypeController.FetchByName(this.ddlJumpstationGroupType.SelectedValue).Id);

                selectedAppType.Add(this.ddlJumpstationGroupType.SelectedValue);
                saveItem.JumpstationGroupStatusId = Convert.ToInt32(this.ddlJumpstationGroupStatus.SelectedValue);
                saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
            }

            if (metadataModificationAllowed)
            {
                //update the JumpstationGroup's Child Record metadata
                if (this.pnlDataControls_Tags.Enabled)
                {

                    saveItem.SetTags(Tag.ParseTagNameList(this.txtTags.Text.Trim(), false));
                }
            }

            //reload the control using the record's (possibly newly assigned) ID
            this.DataSourceId = saveItem.Id;

            //save in JumpstationGroupPivot
            ElementsCPS.Data.SubSonicClient.JumpstationGroupPivot pivot = JumpstationGroupPivot.FetchByID(this.DataSourceId);
            if (pivot == null)
            {
                ElementsCPS.Data.SubSonicClient.JumpstationGroupPivot newpivot = new JumpstationGroupPivot();
                newpivot.JumpstationGroupId = Convert.ToInt32(this.DataSourceId);

                VwMapJumpstationGroupCalcOnFly origial = ElementsCPS.Data.SubSonicClient.VwMapJumpstationGroupCalcOnFlyController.FetchValue(newpivot.JumpstationGroupId);
                newpivot.CopyFromCalcOnFly(origial);
                newpivot.Save();
            }


        }



        /// <summary>
        /// Saves all UI input/changes to a newly-created copy of the JumpstationGroup being edited.
        /// </summary>
        private void SaveAsNew()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                //first create the new (copied) JumpstationGroup based upon the existing (pre-save) DB data
                JumpstationGroup originalJumpstationGroup = this.GetDataSource();
                JumpstationGroup newJumpstationGroup = originalJumpstationGroup.SaveAsNew(true);


                //next, change the control's DataSourceId to reference the new JumpstationGroup
                this.DataSourceId = newJumpstationGroup.Id;

                //next, update the state of the UI controls whose values should not affect the state of the new JumpstationGroup
                this.ddlJumpstationGroupStatus.ForceSelectedValue(newJumpstationGroup.JumpstationGroupState);
                this.ddlOwner.ForceSelectedValue(newJumpstationGroup.OwnerId);

                //next, call SaveItem() to apply any UI input/changes (except for   UI controls re-bound above) to the new JumpstationGroup (only)
                this.SaveInput();

                scope.Complete(); // transaction complete
            }

        }

        /// <summary>
        /// Delete the JumpstationGroup being edited.
        /// </summary>
        private void Delete()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                //get JumpstationGroup to delete
                JumpstationGroup jumpstationGroup = this.GetDataSource();

                //delete JumpstationGroup and it's children
                jumpstationGroup.Delete();

                //get JumpstationGroupPivot to delete
                ElementsCPS.Data.SubSonicClient.JumpstationGroupPivot.Delete(jumpstationGroup.Id);

                scope.Complete(); // transaction complete

                this.pnlEditArea.Enabled = false;
                this.ddlJumpstationGroupStatus.ForceSelectedValue(JumpstationGroupStateId.Deleted);
            }
        }

        /// <summary>
        /// Add the JumpstationGroupSelector wildcard default.
        /// </summary>
        private void AddJumpstationGroupSelectorDefault()
        {
            string defaultQueryParameterValueName = ConfigurationManager.AppSettings["HP.ElementsCPS.WebUI.DefaultValue.GroupSelector.QueryParameterValueName"];
            if (String.IsNullOrEmpty(defaultQueryParameterValueName))
            {
                // there are no query parameter value default configured ("*")
                return;
            }

            //get JumpstationGroup to add the JumpstationGroupSelector
            JumpstationGroup jumpstationGroup = this.GetDataSource();

            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                //Save Group Selector
                JumpstationGroupSelector saveJumpstationGroupItem = new JumpstationGroupSelector(true);
                string defaultName = ConfigurationManager.AppSettings["HP.ElementsCPS.WebUI.DefaultValue.GroupSelector.Name"];
                saveJumpstationGroupItem.Name = String.IsNullOrEmpty(defaultName) ? "Default" : defaultName;
                string defaultDescription = ConfigurationManager.AppSettings["HP.ElementsCPS.WebUI.DefaultValue.GroupSelector.Description"];
                saveJumpstationGroupItem.Description = String.IsNullOrEmpty(defaultDescription) ? "" : defaultDescription;
                saveJumpstationGroupItem.JumpstationGroupId = jumpstationGroup.Id;
                saveJumpstationGroupItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                //Save the default query parameter values (wildcard)
                VwMapQueryParameterJumpstationGroupTypeCollection queryParameterCollection = VwMapQueryParameterJumpstationGroupTypeController.FetchByJumpstationGroupTypeId(jumpstationGroup.JumpstationGroupTypeId, (int?)RowStatus.RowStatusId.Active);
                foreach (VwMapQueryParameterJumpstationGroupType queryParameter in queryParameterCollection)
                {
                    QueryParameterValueQuerySpecification queryParameterValueQuerySpecification =
                        new QueryParameterValueQuerySpecification() { QueryParameterId = queryParameter.QueryParameterId, QueryParameterValueName = defaultQueryParameterValueName };
                    VwMapQueryParameterValueCollection queryParameterValueCollection = VwMapQueryParameterValueController.Fetch(queryParameterValueQuerySpecification);

                    // you can have only one value with "*" for each query parameter id
                    if (queryParameterValueCollection.Count > 0)
                    {
                        //Save query parameter value for the group selector
                        JumpstationGroupSelectorQueryParameterValue saveQueryParameterValueItem = new JumpstationGroupSelectorQueryParameterValue(true);
                        saveQueryParameterValueItem.QueryParameterValueId = queryParameterValueCollection[0].Id;
                        saveQueryParameterValueItem.JumpstationGroupSelectorId = saveJumpstationGroupItem.Id;
                        saveQueryParameterValueItem.Negation = false;
                        saveQueryParameterValueItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                    }
                }
                scope.Complete(); // transaction complete
            }
        }

        private JumpstationGroup GetDataSource()
        {
            JumpstationGroup saveItem;
            if (!this.IsNewRecord)
            {
                saveItem = JumpstationGroup.FetchByID(this.DataSourceId);
            }
            else
            {
                saveItem = new JumpstationGroup(true);
                saveItem.JumpstationGroupState = JumpstationGroupStateId.Modified;
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
            this.UpdateJumpstationGroupSelectorListImmutableConditions();
        }

        private void UpdateJumpstationGroupSelectorListImmutableConditions()
        {
            this.pnlJumpstationGroupSelectorListPanel.Visible = this.DataSourceId != null;
            this.ucJumpstationGroupSelectorList.ImmutableQueryConditions = new JumpstationGroupSelectorQuerySpecification { JumpstationGroupId = (this.DataSourceId ?? -1) };
        }

        #endregion
    }
}