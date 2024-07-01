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

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class WorkflowModuleUpdatePanel : RecordDetailUserControl
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
                this.UpdateChildListImmutableConditions();
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
			this.btnCancel.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
			this.btnDelete.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);

			List<UserRoleId> currentUsersRoles = PersonController.GetCurrentUser().GetRoles();
			this.btnDelete.Visible =
				WorkflowModule.IsStateTransitionPossible(WorkflowStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Visible =
				WorkflowModule.IsStateTransitionPossible(WorkflowStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Visible =
				WorkflowModule.IsStateTransitionPossible(WorkflowStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Visible =
				WorkflowModule.IsStateTransitionPossible(WorkflowStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Visible =
				WorkflowModule.IsStateTransitionPossible(WorkflowStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Visible =
				WorkflowModule.IsStateTransitionPossible(WorkflowStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Visible =
				WorkflowModule.IsStateTransitionPossible(WorkflowStateId.Validated, currentUsersRoles);
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

				//next, validate using the controls' Publishable module validators
				this.Page.Validate("PublishableWorkflowModule");
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

				//next, validate using the controls' Publishable Module validators
				this.Page.Validate("PublishableWorkflowModule");
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

				//next, validate using the controls' Publishable Module validators
				this.Page.Validate("PublishableWorkflowModule");
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
                this.Page.Validate("WorkflowModuleReplacement");
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

            this.BindVersionMinorListControl(newVersion, Convert.ToInt32(this.ddlModuleCategory.SelectedValue), Convert.ToInt32(this.ddlModuleSubCategory.SelectedValue));
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
            ddlVersionMinor_SelectedIndexChanged();
        }

        protected void ddlModuleCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlVersionMajor_SelectedIndexChanged();
            ddlVersionMinor_SelectedIndexChanged();
        }

        protected void ddlModuleSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlVersionMajor_SelectedIndexChanged();
            ddlVersionMinor_SelectedIndexChanged();
        }

        private void ddlVersionMajor_SelectedIndexChanged()
        {
            BindVersionMajorListControl(
                this.ddlModuleCategory.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlModuleCategory.SelectedValue),
                this.ddlModuleSubCategory.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlModuleSubCategory.SelectedValue));
        }

        private void ddlVersionMinor_SelectedIndexChanged()
        {
            BindVersionMinorListControl(
                this.ddlVersionMajor.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlVersionMajor.SelectedValue),
                this.ddlModuleCategory.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlModuleCategory.SelectedValue),
                this.ddlModuleSubCategory.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlModuleSubCategory.SelectedValue));
        }

		protected void cvTxtTagsValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
		}

		protected void cvTxtTagsMaxTagCount_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (Tag.ParseTagNameList(args.Value, false).Count() <= 100);
		}

		protected void cvWorkflowModulePublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			WorkflowModule workflowModule = this.GetDataSource();
			WorkflowStateId toState;
			switch (workflowModule.WorkflowModuleState)
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
			args.IsValid = workflowModule.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles());
		}

        protected void cvWorkflowModuleReplacement_ServerValidate(object source, ServerValidateEventArgs args)
        {
            WorkflowModule workflowModule = this.GetDataSource();
            args.IsValid = !(workflowModule.IsOriginalWorkflowModuleReplacement());
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
			Global.BindWorkflowModuleCategoryListControl(this.ddlModuleCategory, RowStatus.RowStatusId.Active);

            this.ddlModuleCategory.InsertItem(0, "", Global.GetSelectListText());
			Global.BindWorkflowModuleSubCategoryListControl(this.ddlModuleSubCategory, RowStatus.RowStatusId.Active);

			this.ddlModuleSubCategory.InsertItem(0, "", Global.GetSelectListText());
			Global.BindWorkflowStatusListControl(this.ddlWorkflowModuleStatus, RowStatus.RowStatusId.Active);

            BindVersionMajorListControl(0, 0);
            BindVersionMinorListControl(0, 0, 0);
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
            this.txtTitle.Text = string.Empty;
			this.txtFilename.Text = string.Empty;
			this.ddlVersionMajor.ClearSelection();
            this.ddlVersionMinor.ClearSelection();
			this.ddlOwner.ClearSelection();
			this.ddlModuleCategory.ClearSelection();
			this.ddlModuleSubCategory.ClearSelection();
			this.ddlWorkflowModuleStatus.ClearSelection();
			this.txtTags.Text = string.Empty;
        }

        private int BindVersionMajorListControl(int categoryId, int subCategoryId)
        {
            this.ddlVersionMajor.ClearItems();
            this.ddlVersionMajor.InsertItem(0, "", Global.GetSelectListText());

            int versionMajorMaximum = WorkflowModuleVersionController.FetchVersionMajorMaximum(categoryId, subCategoryId);
            for (int i = 1; i <= versionMajorMaximum; i++)
            {
                this.ddlVersionMajor.InsertItem(i, string.Format("{0}", i), string.Format("{0}", i));
            }
            return versionMajorMaximum;
        }

        private void BindVersionMinorListControl(int versionMajorMaximum, int categoryId, int subCategoryId)
        {
            this.ddlVersionMinor.ClearItems();
            this.ddlVersionMinor.InsertItem(0, "", Global.GetSelectListText());

            for (int i = 0; i <= WorkflowModuleVersionController.FetchVersionMinorMaximum(versionMajorMaximum, categoryId, subCategoryId); i++)
            {
                this.ddlVersionMinor.InsertItem(i + 1, string.Format("{0}", i), string.Format("{0}", i));
            }
        }

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			//default the Workflow Module Status to Modified
			this.ddlWorkflowModuleStatus.ClearSelection();
			this.ddlWorkflowModuleStatus.ForceSelectedValue(WorkflowStateId.Modified);

			//default the Workflow Module to current user
			Person currentUser = PersonController.GetCurrentUser();
			Global.ForceSelectedValue(this.ddlOwner, currentUser);
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public WorkflowModuleQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as WorkflowModuleQuerySpecification ?? new WorkflowModuleQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			WorkflowModule bindItem = this.GetDataSource();

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
					this.ddlModuleCategory.ForceSelectedValue(bindItem.WorkflowModuleCategoryId);
					this.ddlModuleSubCategory.ForceSelectedValue(bindItem.WorkflowModuleSubCategoryId);
					this.ddlWorkflowModuleStatus.ForceSelectedValue(bindItem.WorkflowModuleState);
					this.ddlOwner.ForceSelectedValue(bindItem.OwnerId);
					this.txtName.Text = bindItem.Name;
					this.txtDescription.Text = bindItem.Description;
                    this.txtTitle.Text = bindItem.Title; 
					this.txtFilename.Text = bindItem.Filename;
                    this.BindVersionMajorListControl(bindItem.WorkflowModuleCategoryId, bindItem.WorkflowModuleSubCategoryId);
                    this.ddlVersionMajor.ForceSelectedValue(bindItem.VersionMajor);
                    this.BindVersionMinorListControl(bindItem.VersionMajor, bindItem.WorkflowModuleCategoryId, bindItem.WorkflowModuleSubCategoryId);
                    this.ddlVersionMinor.ForceSelectedValue(bindItem.VersionMinor);

                    string moduleUrlValidation = ConfigurationManager.AppSettings["workflowModuleUrlValidation"];
                    this.ucWorkflowURLReportValidationPanel.URLToParse = string.Format("{0}/{1}/{2}/{3}.{4}/{5}", moduleUrlValidation, bindItem.WorkflowModuleCategory, bindItem.WorkflowModuleSubCategory, bindItem.VersionMajor, bindItem.VersionMinor, bindItem.Filename);

                    string moduleUrlPublication = ConfigurationManager.AppSettings["workflowModuleUrlPublication"];
                    this.ucWorkflowURLReportPublicationPanel.URLToParse = string.Format("{0}/{1}/{2}/{3}.{4}/{5}", moduleUrlPublication, bindItem.WorkflowModuleCategory, bindItem.WorkflowModuleSubCategory, bindItem.VersionMajor, bindItem.VersionMinor, bindItem.Filename);

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
				bindItem.IsStateTransitionAllowed(WorkflowStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Enabled =
				bindItem.IsStateTransitionAllowed(WorkflowStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Enabled =
				bindItem.IsStateTransitionAllowed(WorkflowStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Enabled =
				bindItem.IsStateTransitionAllowed(WorkflowStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Enabled =
				bindItem.IsStateTransitionAllowed(WorkflowStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
				bindItem.IsStateTransitionAllowed(WorkflowStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Enabled =
				bindItem.IsStateTransitionAllowed(WorkflowStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the Workflow Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
		}

		protected bool IsDataModificationAllowed()
		{
			WorkflowModule bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;
			else
				return bindItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			WorkflowModule bindItem = this.GetDataSource();
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

			bool newRecord = false;
			WorkflowModule saveItem;
			if (this.DataSourceId != null)
			{
				saveItem = WorkflowModule.FetchByID(this.DataSourceId);
			}
			else
			{
				newRecord = true;
				saveItem = new WorkflowModule(true);
			}

			if (dataModificationAllowed)
			{
				saveItem.Name = this.txtName.Text.TrimToNull();
				saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
                saveItem.Title = this.txtTitle.Text.TrimToNull();
                saveItem.Filename = this.txtFilename.Text.TrimToNull();
				saveItem.VersionMajor = Convert.ToInt32(this.ddlVersionMajor.SelectedValue);
                saveItem.VersionMinor = Convert.ToInt32(this.ddlVersionMinor.SelectedValue);
				saveItem.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
				saveItem.WorkflowModuleCategoryId = Convert.ToInt32(this.ddlModuleCategory.SelectedValue);
				saveItem.WorkflowModuleSubCategoryId = Convert.ToInt32(this.ddlModuleSubCategory.SelectedValue);
				saveItem.WorkflowModuleStatusId = Convert.ToInt32(this.ddlWorkflowModuleStatus.SelectedValue);
				saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                // save the highest workflow module version.
                WorkflowModuleVersionController.UpdateWorkflowModuleVersionToHighest(saveItem.WorkflowModuleCategoryId, saveItem.WorkflowModuleSubCategoryId, saveItem.VersionMajor, saveItem.VersionMinor);

                // save the module conditions
                ucWorkflowConditionEditListUpdate.SaveInput(saveItem.Id, false);

                string moduleUrlValidation = ConfigurationManager.AppSettings["workflowModuleUrlValidation"];
                this.ucWorkflowURLReportValidationPanel.URLToParse = string.Format("{0}/{1}/{2}/{3}.{4}/{5}", moduleUrlValidation, saveItem.WorkflowModuleCategory, saveItem.WorkflowModuleSubCategory, saveItem.VersionMajor, saveItem.VersionMinor, saveItem.Filename);
                this.ucWorkflowURLReportValidationPanel.DataBind();

                string moduleUrlPublication = ConfigurationManager.AppSettings["workflowModuleUrlPublication"];
                this.ucWorkflowURLReportPublicationPanel.URLToParse = string.Format("{0}/{1}/{2}/{3}.{4}/{5}", moduleUrlPublication, saveItem.WorkflowModuleCategory, saveItem.WorkflowModuleSubCategory, saveItem.VersionMajor, saveItem.VersionMinor, saveItem.Filename);
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

		}

		/// <summary>
		/// Saves all UI input/changes to a newly-created copy of the Workflow being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first create the new (copied) module based upon the existing (pre-save) DB data
				WorkflowModule originalWorkflowModule = this.GetDataSource();
				WorkflowModule newWorkflowModule = originalWorkflowModule.SaveAsNew(true);

				//next, change the control's DataSourceId to reference the new Workflow
				this.DataSourceId = newWorkflowModule.Id;

				//next, update the state of the UI controls whose values should not affect the state of the new Workflow
				this.ddlWorkflowModuleStatus.ForceSelectedValue(newWorkflowModule.WorkflowModuleState);
				this.ddlOwner.ForceSelectedValue(newWorkflowModule.OwnerId);

				//next, call SaveItem() to apply any UI input/changes (except for those UI controls re-bound above) to the new Workflow (only)
				this.SaveInput();

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Delete the module being edited.
		/// </summary>
		private void Delete()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//get module to delete
				WorkflowModule workflowModule = this.GetDataSource();
				workflowModule.Delete();

				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlWorkflowModuleStatus.ForceSelectedValue(WorkflowStateId.Deleted);
			}
		}

		private WorkflowModule GetDataSource()
		{
			WorkflowModule saveItem;
			if (!this.IsNewRecord)
			{
				saveItem = WorkflowModule.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new WorkflowModule(true); 
				saveItem.WorkflowModuleState = WorkflowStateId.Modified;
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
            this.UpdateWorkflowConditionListImmutableConditions();
            this.UpdateWorkflowListImmutableConditions();
        }

        private void UpdateWorkflowConditionListImmutableConditions()
        {
            this.ucWorkflowConditionEditListUpdate.ImmutableQueryConditions = new WorkflowConditionQuerySpecification {  RowStateId = (int)RowStatus.RowStatusId.Active };
        }

        private void UpdateWorkflowListImmutableConditions()
        {
            this.pnlWorkflowListPanel.Visible = this.DataSourceId != null;
            this.ucWorkflowList.ImmutableQueryConditions = new WorkflowQuerySpecification { CompatibleWithWorkflowModuleId = (this.DataSourceId ?? -1) };
        }

		#endregion
	}
}