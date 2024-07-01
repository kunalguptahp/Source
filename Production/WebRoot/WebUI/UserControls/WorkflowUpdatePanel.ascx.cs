using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
using HP.ElementsCPS.Data.CmService;
using Microsoft.Security.Application;
using System.Web.UI;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class WorkflowUpdatePanel : RecordDetailUserControl
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
				Workflow.IsStateTransitionPossible(WorkflowStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Visible =
				Workflow.IsStateTransitionPossible(WorkflowStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Visible =
				Workflow.IsStateTransitionPossible(WorkflowStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Visible =
				Workflow.IsStateTransitionPossible(WorkflowStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Visible =
				Workflow.IsStateTransitionPossible(WorkflowStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Visible =
				Workflow.IsStateTransitionPossible(WorkflowStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Visible =
				Workflow.IsStateTransitionPossible(WorkflowStateId.Validated, currentUsersRoles);
		}

		#endregion

		#region ControlEvents

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			//if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
			{
				return;
			}

			this.OnInputSave(new EventArgs());
		}

		protected void btnSaveAsNew_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			//if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
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
			//if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
			{
				return;
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//next, validate using the controls' PublishableWorkflow validators
				this.Page.Validate("PublishableWorkflow");
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

            try
            {

                using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
                {
                    //first, save the item like btnSave would do
                    this.SaveInput();

                    //next, validate using the controls' PublishableWorkflow validators
                    this.Page.Validate("PublishableWorkflow");
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

            catch (PublishException pex)
            {
                if (pex.Errors.Length > 0)
                {
                    switch (pex.Errors[0].ErrorCode)
                    {
                        // duplicate set of query parameter values.
                        case PublishException.PUBLISH_ERROR_DUPLICATE_MATCH_CRITERIA:
                        // end release date must be after start release date.
                        case PublishException.PUBLISH_ERROR_RELEASE_DATE:
                            this.ErrorLabel.Text = pex.Errors[0].ErrorDesc;
                            break;
                        default:
                            string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was not validated to Elements.", this.lblIdValue.Text);
                            LogManager.Current.Log(Severity.Error, this, message, pex);
                            throw;
                    }
                }
                else
                {
                    string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was not validated to Elements.", this.lblIdValue.Text);
                    LogManager.Current.Log(Severity.Error, this, message, pex);
                    throw;
                }
            }
		}

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            //if (!this.Page.IsValid || !this.ucConfigurationServiceLabelValue.IsValid())
            {
                return;
            }

            try
            {
                using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
                {
                    //first, save the item like btnSave would do
                    this.SaveInput();

                    //next, validate using the controls' PublishableWorkflow validators
                    this.Page.Validate("PublishableWorkflow");
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

            catch (PublishException pex)
            {
                switch (pex.Errors[0].ErrorCode)
                {
                    // duplicate set of query parameter values.
                    case PublishException.PUBLISH_ERROR_DUPLICATE_MATCH_CRITERIA:
                    // end release date must be after start release date.
                    case PublishException.PUBLISH_ERROR_RELEASE_DATE:
                        this.ErrorLabel.Text = pex.Errors[0].ErrorDesc;
                        break;
                    default:
                        string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was not published to Elements.", this.lblIdValue.Text);
                        LogManager.Current.Log(Severity.Error, this, message, pex);
                        throw;
                }
            }
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
                this.Page.Validate("WorkflowReplacement");
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

        protected void btnIncrementVersionMajor_Click(object sender, EventArgs e)
        {
            int newVersion = Convert.ToInt32(this.ddlVersionMajor.Items[this.ddlVersionMajor.Items.Count - 1].Value) + 1;
            this.ddlVersionMajor.InsertItem(this.ddlVersionMajor.Items.Count, string.Format("{0}", newVersion), string.Format("{0}", newVersion));
            this.ddlVersionMajor.ForceSelectedValue(newVersion);

            this.BindVersionMinorListControl(newVersion);
            this.ddlVersionMinor.ForceSelectedValue(this.ddlVersionMinor.Items[this.ddlVersionMinor.Items.Count - 1].Value);
        }

        protected void btnIncrementVersionMinor_Click(object sender, EventArgs e)
        {
            int newVersion = Convert.ToInt32(this.ddlVersionMinor.Items[this.ddlVersionMinor.Items.Count - 1].Value) + 1;
            this.ddlVersionMinor.InsertItem(this.ddlVersionMinor.Items.Count, string.Format("{0}", newVersion), string.Format("{0}", newVersion));
            this.ddlVersionMinor.ForceSelectedValue(newVersion);
        }

        protected void ddlVersionMajor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVersionMinorListControl(this.ddlVersionMajor.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlVersionMajor.SelectedValue));
        }

        protected void cvMinimumSelectionRequired_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.ucWorkflowModuleEditListUpdate.IsMinimumValueSelected();
		}

		protected void cvTxtTagsValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
		}

		protected void cvTxtTagsMaxTagCount_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (Tag.ParseTagNameList(args.Value, false).Count() <= 100);
		}

		protected void cvWorkflowPublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			Workflow workflow = this.GetDataSource();
			WorkflowStateId toState;
			switch (workflow.WorkflowState)
			{
				case WorkflowStateId.Modified:
				case WorkflowStateId.Abandoned:
					toState = WorkflowStateId.ReadyForValidation;
					break;
				case WorkflowStateId.ReadyForValidation:
					toState = WorkflowStateId.Validated;
					break;
				default:
					toState = WorkflowStateId.Published;
					break;
			}
			args.IsValid = workflow.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles());
		}

        protected void cvWorkflowReplacement_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Workflow workflow = this.GetDataSource();
            args.IsValid = !(workflow.IsOriginalWorkflowReplacement());
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
			Global.BindWorkflowApplicationListControl(this.ddlWorkflowApplication, RowStatus.RowStatusId.Active,tenantId ,this.IsNewRecord);
			this.ddlWorkflowApplication.InsertItem(0, "", Global.GetSelectListText());

			Global.BindWorkflowTypeListControl(this.ddlWorkflowType, RowStatus.RowStatusId.Active);
			this.ddlWorkflowType.InsertItem(0, "", Global.GetSelectListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			Global.BindWorkflowStatusListControl(this.ddlWorkflowStatus, RowStatus.RowStatusId.Active);

            Global.BindAppClientListControl(this.ddlAppClient);
            this.ddlAppClient.InsertItem(0, "", Global.GetSelectListText());

            BindVersionMajorListControl();
            BindVersionMinorListControl(0);
        }

        private int BindVersionMajorListControl()
        {
            this.ddlVersionMajor.ClearItems();
            this.ddlVersionMajor.InsertItem(0, "", Global.GetSelectListText());
            int versionMajorMaximum = WorkflowVersionController.FetchVersionMajorMaximum();
            for (int i = 1; i <= versionMajorMaximum; i++)
            {
                this.ddlVersionMajor.InsertItem(i, string.Format("{0}", i), string.Format("{0}", i));
            }
            return versionMajorMaximum;
        }

        private void BindVersionMinorListControl(int versionMajor)
        {
            this.ddlVersionMinor.ClearItems();
            this.ddlVersionMinor.InsertItem(0, "", Global.GetSelectListText());

            for (int i = 0; i <= WorkflowVersionController.FetchVersionMinorMaximum(versionMajor); i++)
            {
                this.ddlVersionMinor.InsertItem(i + 1, string.Format("{0}", i), string.Format("{0}", i));
            }
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
            this.txtFilename.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
			this.ddlOwner.ClearSelection();
            this.ddlAppClient.ClearSelection();
			this.ddlWorkflowApplication.ClearSelection();
			this.ddlWorkflowType.ClearSelection();
			this.ddlWorkflowStatus.ClearSelection();
            this.ddlVersionMajor.ClearSelection();
            this.ddlVersionMinor.ClearSelection();
            this.chkOffline.Checked = false;
            this.txtTags.Text = string.Empty;
        }

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			//default the Workflow Status to Modified
			this.ddlWorkflowStatus.ClearSelection();
			this.ddlWorkflowStatus.ForceSelectedValue(WorkflowStateId.Modified);

			//default the Workflow Status to current user
			Person currentUser = PersonController.GetCurrentUser();
			Global.ForceSelectedValue(this.ddlOwner, currentUser);
        }

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public WorkflowQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as WorkflowQuerySpecification ?? new WorkflowQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			Workflow bindItem = this.GetDataSource();

			if (this.IsNewRecord)
			{ 
                this.ApplyDataControlDefaultValues();
                this.UpdateChildListImmutableConditions();
            }
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
					this.ddlWorkflowStatus.ForceSelectedValue(bindItem.WorkflowStatusId);
					this.ddlOwner.ForceSelectedValue(bindItem.OwnerId);
					this.ddlWorkflowApplication.ForceSelectedValue(bindItem.WorkflowApplicationId);
					this.ddlWorkflowType.ForceSelectedValue(bindItem.WorkflowTypeId);
					this.txtName.Text = bindItem.Name;
					this.txtDescription.Text = bindItem.Description;
                    this.ddlVersionMajor.ForceSelectedValue(bindItem.VersionMajor);
                    this.BindVersionMinorListControl(bindItem.VersionMajor);
                    this.ddlVersionMinor.ForceSelectedValue(bindItem.VersionMinor);
                    this.txtFilename.Text = bindItem.Filename;
                    this.chkOffline.Checked = (bool)bindItem.Offline;

                    this.ddlAppClient.ForceSelectedValue(bindItem.AppClient.Name.ToString());

                    string moduleUrlValidation = ConfigurationManager.AppSettings["workflowMainUrlValidation"];
                    this.ucWorkflowURLReportValidationPanel.URLToParse = string.Format("{0}/{1}.{2}/{3}", moduleUrlValidation, bindItem.VersionMajor, bindItem.VersionMinor, bindItem.Filename);

                    string moduleUrlPublication = ConfigurationManager.AppSettings["workflowMainUrlPublication"];
                    this.ucWorkflowURLReportPublicationPanel.URLToParse = string.Format("{0}/{1}.{2}/{3}", moduleUrlPublication, bindItem.VersionMajor, bindItem.VersionMinor, bindItem.Filename);

					List<string> tagNames = bindItem.TagNames;
                    this.txtTags.Text = (tagNames == null) ? "" : string.Join(", ", tagNames.ToArray());

				}
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the Workflow's current state
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
                (isDataModificationAllowed || isMetadataModificationAllowed) && (app1.Contains(RoleApplicationId.Default) || app1.Contains(RoleApplicationId.Reach) || app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Reach));
			this.btnSaveAsNew.Enabled = 
                !this.IsNewRecord;
			this.btnDelete.Enabled =
                bindItem.IsStateTransitionAllowed(WorkflowStateId.Deleted, currentUsersRoles) ;
			this.btnAbandon.Enabled =
                bindItem.IsStateTransitionAllowed(WorkflowStateId.Abandoned, currentUsersRoles) && (app1.Contains(RoleApplicationId.Default) || app1.Contains(RoleApplicationId.Reach) || app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Reach));
			this.btnPublish.Enabled =
                bindItem.IsStateTransitionAllowed(WorkflowStateId.Published, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Reach));
			this.btnReadyForValidation.Enabled =
                bindItem.IsStateTransitionAllowed(WorkflowStateId.ReadyForValidation, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Reach));
			this.btnRework.Enabled =
				bindItem.IsStateTransitionAllowed(WorkflowStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
                bindItem.IsStateTransitionAllowed(WorkflowStateId.Cancelled, currentUsersRoles) && (app2.Contains(RoleApplicationId.Default) || app2.Contains(RoleApplicationId.Reach));
			this.btnValidate.Enabled =
				bindItem.IsStateTransitionAllowed(WorkflowStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the Workflow Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
		}

		protected bool IsDataModificationAllowed()
		{
			Workflow bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;
			else
				return bindItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			Workflow bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;
			else
				return bindItem.IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

        protected bool HasGroupSelector()
        {
            Workflow bindItem = this.GetDataSource();
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

			Workflow saveItem;
			if (this.DataSourceId != null)
			{
				saveItem = Workflow.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new Workflow(true);
                saveItem.VersionMinor = 0;
            }

			if (dataModificationAllowed)
			{
				saveItem.Name = this.txtName.Text.TrimToNull();
				saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
                saveItem.VersionMajor = Convert.ToInt32(this.ddlVersionMajor.SelectedValue);
                saveItem.VersionMinor = Convert.ToInt32(this.ddlVersionMinor.SelectedValue);
                saveItem.AppClientId = Convert.ToInt32(AppClientController.FetchByName(this.ddlAppClient.SelectedValue).Id);
                selectedAppClient.Add(this.ddlAppClient.SelectedValue);
                saveItem.Filename = this.txtFilename.Text.TrimToNull();
				saveItem.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
				saveItem.WorkflowApplicationId = Convert.ToInt32(this.ddlWorkflowApplication.SelectedValue);
				saveItem.WorkflowTypeId = Convert.ToInt32(this.ddlWorkflowType.SelectedValue);
				saveItem.WorkflowStatusId = Convert.ToInt32(this.ddlWorkflowStatus.SelectedValue);
                saveItem.Offline = this.chkOffline.Checked;
                saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                // save the highest workflow version.
                WorkflowVersionController.UpdateWorkflowVersionToHighest(saveItem.VersionMajor, saveItem.VersionMinor);

                string moduleUrlValidation = ConfigurationManager.AppSettings["workflowMainUrlValidation"];
                this.ucWorkflowURLReportValidationPanel.URLToParse = string.Format("{0}/{1}.{2}/{3}", moduleUrlValidation, saveItem.VersionMajor, saveItem.VersionMinor, saveItem.Filename);
                this.ucWorkflowURLReportValidationPanel.DataBind();
 
                string moduleUrlPublication = ConfigurationManager.AppSettings["workflowMainUrlPublication"];
                this.ucWorkflowURLReportPublicationPanel.URLToParse = string.Format("{0}/{1}.{2}/{3}", moduleUrlPublication, saveItem.VersionMajor, saveItem.VersionMinor, saveItem.Filename);
                this.ucWorkflowURLReportPublicationPanel.DataBind();
            }

			if (metadataModificationAllowed)
			{
				//update the Workflow's Child Record metadata
				if (this.pnlDataControls_Tags.Enabled)
				{
                    
					saveItem.SetTags(Tag.ParseTagNameList(this.txtTags.Text, false));
				}
			}

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;

			if (dataModificationAllowed)
			{
				//save workflow modules
				if (this.DataSourceId != null)
				{
					this.ucWorkflowModuleEditListUpdate.SaveInput(this.DataSourceId ?? 0);
				}
			}
		}

		/// <summary>
		/// Saves all UI input/changes to a newly-created copy of the Workflow being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first create the new (copied) Workflow based upon the existing (pre-save) DB data
				Workflow originalWorkflow = this.GetDataSource();
				Workflow newWorkflow = originalWorkflow.SaveAsNew(true);

				//next, change the control's DataSourceId to reference the new Workflow
				this.DataSourceId = newWorkflow.Id;

				//next, update the state of the UI controls whose values should not affect the state of the new Workflow
				this.ddlWorkflowStatus.ForceSelectedValue(newWorkflow.WorkflowState);
				this.ddlOwner.ForceSelectedValue(newWorkflow.OwnerId);

				//next, call SaveItem() to apply any UI input/changes (except for those UI controls re-bound above) to the new Workflow (only)
				this.SaveInput();

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Delete the Workflow being edited.
		/// </summary>
		private void Delete()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//get workflow to delete
				Workflow workflow = this.GetDataSource();

				//delete configurationServiceGroup and it's children
				workflow.Delete();

				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlWorkflowStatus.ForceSelectedValue(WorkflowStateId.Deleted);
			}
		}

		private Workflow GetDataSource()
		{
			Workflow saveItem;
			if (!this.IsNewRecord)
			{
				saveItem = Workflow.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new Workflow(true); 
				saveItem.WorkflowState = WorkflowStateId.Modified;
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
			this.UpdateWorkflowSelectorListImmutableConditions();
            this.UpdateWorkflowModuleEditListImmutableConditions();
        }

		private void UpdateWorkflowSelectorListImmutableConditions()
		{
			this.pnlWorkflowSelectorListPanel.Visible = this.DataSourceId != null;			
			this.ucWorkflowSelectorList.ImmutableQueryConditions = new WorkflowSelectorQuerySpecification { WorkflowId = (this.DataSourceId ?? -1) };
		}

        private void UpdateWorkflowModuleEditListImmutableConditions()
        {
            this.ucWorkflowModuleEditListUpdate.ImmutableQueryConditions = new WorkflowModuleQuerySpecification { WorkflowModuleStatusId = (int)WorkflowStateId.Published };
            this.ucWorkflowModuleEditListUpdate.DataBind();
        }

		#endregion
	}
}