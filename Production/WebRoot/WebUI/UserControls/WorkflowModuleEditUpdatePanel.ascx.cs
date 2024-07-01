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
	public partial class WorkflowModuleEditUpdatePanel : BaseQuerySpecificationEditDataUserControl
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

			using (TransactionScope scope = new TransactionScope())
			{
				//first, save the item like btnSave would do
				this.SaveInput();

				//second, perform the state transition
				this.GetWorkflowModules().Abandon();
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

				//next, validate using the controls' PublishableWorkflowModule validators
				this.Page.Validate("PublishableWorkflowModule");
				if (!this.Page.IsValid)
				{
					return;
				}

				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the Workflow modules (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			WorkflowModuleCollection modules = this.GetWorkflowModules();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (WorkflowModule module in modules)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					module.SubmitToValidator();
					scope.Complete(); // transaction complete
				}
                cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Ready to Validate Success"));
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

				//next, validate using the controls' PublishableWorkflowModule validators
				this.Page.Validate("PublishableWorkflowModule");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the workflow modules (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
            WorkflowModuleCollection modules = this.GetWorkflowModules();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (WorkflowModule module in modules)
			{
					using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) 
					{
                        module.Validate();
						scope.Complete(); // transaction complete
					}
                    cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Validate Success"));
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

				//next, validate using the controls' PublishableWorkflow validators
				this.Page.Validate("PublishableWorkflowModule");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the workflow modules (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
            WorkflowModuleCollection modules = this.GetWorkflowModules();
			List<CPSLog> cpsLogList = new List<CPSLog>();
            foreach (WorkflowModule module in modules)
			{
				// check to see if replacement
				// can't published the workflow to be replaced on the copied workflow
                if (module.IsOriginalWorkflowModuleReplacement())
				{
                    cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Not Published: Workflow module cannot be unpublished because another workflow module is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
                    module.Publish();
					scope.Complete(); // transaction complete
				}
                cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Publish Success"));
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
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the workflow modules (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
            WorkflowModuleCollection modules = this.GetWorkflowModules();
			List<CPSLog> cpsLogList = new List<CPSLog>();
            foreach (WorkflowModule module in modules)
			{
				// check to see if replacement
				// can't published the workflow to be replaced on the copied workflow
                if (module.IsOriginalWorkflowModuleReplacement())
				{
                    cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Not Unpublished: Workflow module cannot be unpublished because another workflow module is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
                    module.UnPublish();
					scope.Complete(); // transaction complete
				}
                cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Unpublish Success"));
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
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the workflows (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
            WorkflowModuleCollection modules = this.GetWorkflowModules();
			List<CPSLog> cpsLogList = new List<CPSLog>();
            foreach (WorkflowModule module in modules)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
                    module.SubmitBackToEditor();
					scope.Complete(); // transaction complete
				}
                cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Rework Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlCPSLog.Visible = true;
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();		
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

		protected void cvWorkflowModulePublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
            WorkflowModuleCollection modules = this.GetWorkflowModules();
            foreach (WorkflowModule module in modules)
			{
				WorkflowStateId toState;
				switch (module.WorkflowModuleState)
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
                if (!module.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles()))
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
            this.ddlModuleCategory.InsertItem(0, "", Global.GetSelectListText());
            Global.BindWorkflowModuleSubCategoryListControl(this.ddlModuleSubCategory, RowStatus.RowStatusId.Active);

            this.ddlModuleSubCategory.InsertItem(0, "", Global.GetSelectListText());
            Global.BindWorkflowStatusListControl(this.ddlWorkflowModuleStatus, RowStatus.RowStatusId.Active);

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetSelectListText());

			Global.BindWorkflowStatusListControl(this.ddlWorkflowModuleStatus, RowStatus.RowStatusId.Active);
			this.ddlWorkflowModuleStatus.InsertItem(0, "", Global.GetSelectListText());
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
			this.ddlModuleCategory.ClearSelection();
			this.ddlModuleSubCategory.ClearSelection();
			this.ddlWorkflowModuleStatus.ClearSelection();
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
            this.txtTitle.Text = string.Empty;
            this.txtFilename.Text = string.Empty;
            this.txtTagsToAdd.Text = string.Empty;
			this.txtTagsToRemove.Text = string.Empty;
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

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			WorkflowModuleCollection bindItems = this.GetWorkflowModules();
			Person currentUser = PersonController.GetCurrentUser();
			try
			{
				List<int> moduleIds = bindItems.GetIds();
                moduleIds.Sort();
                this.lblIdText.Text = string.Join(", ", moduleIds.ToStrings().ToArray());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}

			if (bindItems.Count > 0)
			{
				int status = bindItems[0].WorkflowModuleStatusId;
				if (bindItems.All(x => x.WorkflowModuleStatusId == status))
				{
					this.ddlWorkflowModuleStatus.ForceSelectedValue(status);
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the Workflow Module's current state
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
				bindItems.IsStateTransitionAllowed(WorkflowStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Enabled =
				bindItems.IsStateTransitionAllowed(WorkflowStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Enabled =
				 bindItems.IsStateTransitionAllowed(WorkflowStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Enabled =
				bindItems.IsStateTransitionAllowed(WorkflowStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Enabled =
				bindItems.IsStateTransitionAllowed(WorkflowStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
				bindItems.IsStateTransitionAllowed(WorkflowStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Enabled =
				bindItems.IsStateTransitionAllowed(WorkflowStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the Workflow Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
		}

		protected bool IsDataModificationAllowed()
		{
			return this.GetWorkflowModules().IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			return this.GetWorkflowModules().IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
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
			bool moduleModified = false;

			List<CPSLog> cpsLogList = new List<CPSLog>();
			WorkflowModuleCollection modules = this.GetWorkflowModules();
			foreach (WorkflowModule saveItem in modules)
			{
				moduleModified = false;
				if (dataModificationAllowed)
				{
					if (!string.IsNullOrEmpty(this.txtName.Text.TrimToNull()))
					{
						saveItem.Name = this.txtName.Text.TrimToNull();
						moduleModified = true;
					}

					if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
					{
						saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
                        moduleModified = true;
                    }

                    if (!string.IsNullOrEmpty(this.txtTitle.Text.TrimToNull()))
                    {
                        saveItem.Title = this.txtTitle.Text.TrimToNull();
                        moduleModified = true;
                    }

					if (!string.IsNullOrEmpty(this.ddlModuleCategory.SelectedValue))
					{
						saveItem.WorkflowModuleCategoryId = Convert.ToInt32(this.ddlModuleCategory.SelectedValue);
                        moduleModified = true;
                    }

					if (!string.IsNullOrEmpty(this.ddlModuleSubCategory.SelectedValue))
					{
						saveItem.WorkflowModuleSubCategoryId = Convert.ToInt32(this.ddlModuleSubCategory.SelectedValue);
                        moduleModified = true;
                    }

                    if (!string.IsNullOrEmpty(this.txtFilename.Text.TrimToNull()))
                    {
                        saveItem.Filename = this.txtFilename.Text.TrimToNull();
                        moduleModified = true;
                    }

					if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
					{
						saveItem.OwnerId = string.IsNullOrEmpty(this.ddlOwner.SelectedValue) ? PersonController.GetCurrentUser().Id : Convert.ToInt32(this.ddlOwner.SelectedValue);
                        moduleModified = true;
                    }

					if (moduleModified)
					{
						saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
						saveItem.Log(Severity.Info, "Module data saved.");
					}

					//save the modules if they were changed.
                    this.ucWorkflowConditionEditListUpdate.SaveInput(saveItem.Id, true);
                }

				if (metadataModificationAllowed)
				{
					//update the Child Record metadata
					if (this.pnlDataControls_TagsToAdd.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToAdd.Text))
						{
							saveItem.AddTags(Tag.ParseTagNameList(this.txtTagsToAdd.Text, false));
                            moduleModified = true;
                        }
					}

					if (this.pnlDataControls_TagsToRemove.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToRemove.Text))
						{
							saveItem.RemoveTags(Tag.ParseTagNameList(this.txtTagsToRemove.Text, false));
                            moduleModified = true;
						}
					}
				}

				if (moduleModified)
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
		/// Saves all UI input/changes to a newly-created copy of the Workflow being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope())
			{
				//first create the new (copied) Workflow based upon the existing (pre-save) DB data
				WorkflowModuleCollection originalModule = this.GetWorkflowModules();
				WorkflowModuleCollection newModules = originalModule.SaveAllAsNew(true);

				//Apply change:
                foreach (var module in newModules)
				{
					string name = this.txtName.Text.Trim();
					if (!string.IsNullOrEmpty(name))
                        module.Name = name;

                    string description = this.txtDescription.Text.Trim();
                    if (!string.IsNullOrEmpty(description))
                        module.Description = description;

                    string title = this.txtTitle.Text.Trim();
                    if (!string.IsNullOrEmpty(title))
                        module.Title = title;

                    string filename = this.txtFilename.Text.Trim();
                    if (!string.IsNullOrEmpty(filename))
                        module.Filename = filename;

                    if (!string.IsNullOrEmpty(this.ddlModuleCategory.SelectedValue))
                    {
                        module.WorkflowModuleCategoryId = Convert.ToInt32(this.ddlModuleCategory.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(this.ddlModuleSubCategory.SelectedValue))
                    {
                        module.WorkflowModuleSubCategoryId = Convert.ToInt32(this.ddlModuleSubCategory.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
                    {
                        module.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
                    }

                    module.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                    //save the modules if they were changed.
                    this.ucWorkflowConditionEditListUpdate.SaveInput(module.Id, true);
                }
				//TODO:Tags??

				scope.Complete(); // transaction complete

                //next, update the state of the UI controls whose values should not affect the state of the new Workflow
                this.ddlWorkflowModuleStatus.ForceSelectedValue(WorkflowStateId.Modified);

				WorkflowModuleQuerySpecification WorkflowModuleQuerySpecification = new WorkflowModuleQuerySpecification() { IdList = newModules.GetIds() };
				this.QuerySpecification = WorkflowModuleQuerySpecification;
			}
		}

		/// <summary>
		/// Delete the Workflow being edited.
		/// </summary>
		private void Delete()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			using (TransactionScope scope = new TransactionScope())
			{
				//get module to delete
				WorkflowModuleCollection modules = this.GetWorkflowModules();

				//Apply change:
				foreach (var module in modules)
				{
                    if (module.WorkflowModuleState == WorkflowStateId.Abandoned)
					{
                        module.Delete();
                        cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Success Delete"));
					}
					else
					{
                        cpsLogList.Add(new CPSLog(module.Id, module.Description, module.Name, "Unable to Delete: Workflow module must be 'Abandoned'."));						
					}
				}
				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlWorkflowModuleStatus.ForceSelectedValue(WorkflowStateId.Deleted);
			}

			if (cpsLogList.Count > 0)
			{
				this.pnlCPSLog.Visible = true;
				this.ucCPSLog.CPSLogList = cpsLogList;
				this.ucCPSLog.DataBind();
			}
		}

		private VwMapWorkflowModuleCollection GetDataSource()
		{
			WorkflowModuleQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapWorkflowModuleCollection moduleCollection = VwMapWorkflowModuleController.Fetch(query);
			if (moduleCollection.Count < 1)
			{
				if (this.pnlEditArea.Enabled)
				{
					this.SetErrorMessage("Invalid data. No data matches the specified search criteria.");
					this.pnlEditArea.Enabled = false;
				}
			}
			return moduleCollection;
		}

		/// <summary>
		/// Gets the <see cref="WorkflowModule"/>s corresponding to the items in the <see cref="VwMapWorkflowModuleCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private WorkflowModuleCollection GetWorkflowModules()
		{
			return this.GetDataSource().GetWorkflowModules();
		}

		#endregion

	}
}