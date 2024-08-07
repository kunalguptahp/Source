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
	public partial class WorkflowEditUpdatePanel : BaseQuerySpecificationEditDataUserControl
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
				this.GetWorkflows().Abandon();
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

				//next, validate using the controls' PublishableWorkflow validators
				this.Page.Validate("PublishableWorkflow");
				if (!this.Page.IsValid)
				{
					return;
				}

				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the Workflows (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			WorkflowCollection Workflows = this.GetWorkflows();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (Workflow Workflow in Workflows)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					Workflow.SubmitToValidator();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Ready to Validate Success"));
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

				//next, validate using the controls' PublishableWorkflow validators
				this.Page.Validate("PublishableWorkflow");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the workflows (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			WorkflowCollection Workflows = this.GetWorkflows();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (Workflow Workflow in Workflows)
			{
					using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) 
					{
						Workflow.Validate();
						scope.Complete(); // transaction complete
					}
					cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Validate Success"));
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
				this.Page.Validate("PublishableWorkflow");
				if (!this.Page.IsValid)
				{
					return;
				}
				scope.Complete(); // transaction complete
			}

			//NOTE: The actual state transition is excluded from the above transaction, because the actual Elements ValidatePublish/UnPublish 
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the workflows (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			WorkflowCollection Workflows = this.GetWorkflows();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (Workflow Workflow in Workflows)
			{
				// check to see if replacement
				// can't published the workflow to be replaced on the copied workflow
				if (Workflow.IsOriginalWorkflowReplacement())
				{
					cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Not Published: Workflow cannot be unpublished because another workflow is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					Workflow.Publish();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Publish Success"));
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
			//operation cannot be rolled back and therefore it is preferable to complete the operation for only some of the workflows (and then stop) 
			//rather than rolling back the transaction (and potentially leaving CPS and Elements DBs in an inconsistent state).
			WorkflowCollection Workflows = this.GetWorkflows();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (Workflow Workflow in Workflows)
			{
				// check to see if replacement
				// can't published the workflow to be replaced on the copied workflow
				if (Workflow.IsOriginalWorkflowReplacement())
				{
					cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Not Unpublished: Workflow cannot be unpublished because another workflow is replacing this one."));
					continue;
				}

				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					Workflow.UnPublish();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Unpublish Success"));
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
			WorkflowCollection Workflows = this.GetWorkflows();
			List<CPSLog> cpsLogList = new List<CPSLog>();
			foreach (Workflow Workflow in Workflows)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
				{
					Workflow.SubmitBackToEditor();
					scope.Complete(); // transaction complete
				}
				cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Rework Success"));
			}
			this.RaiseInputSaved(e);

			//last, re-bind the item to update the data, buttons, etc.
			this.BindItem();

			this.pnlCPSLog.Visible = true;
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();		
		}

		protected void cvMinimumSelectionRequired_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.ucWorkflowModuleEditListUpdate.IsMinimumValueSelected();
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

		protected void cvWorkflowPublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			WorkflowCollection Workflows = this.GetWorkflows();

			foreach (Workflow Workflow in Workflows)
			{
				WorkflowStateId toState;
				switch (Workflow.WorkflowState)
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
				if (!Workflow.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles()))
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
			Global.BindWorkflowApplicationListControl(this.ddlWorkflowApplication, RowStatus.RowStatusId.Active);
			this.ddlWorkflowApplication.InsertItem(0, "", Global.GetSelectListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetSelectListText());

			Global.BindWorkflowTypeListControl(this.ddlWorkflowType, RowStatus.RowStatusId.Active);
			this.ddlWorkflowType.InsertItem(0, "", Global.GetSelectListText());

			Global.BindWorkflowStatusListControl(this.ddlWorkflowStatus, RowStatus.RowStatusId.Active);
			this.ddlWorkflowStatus.InsertItem(0, "", Global.GetSelectListText());
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
			this.ddlWorkflowApplication.ClearSelection();
			this.ddlWorkflowType.ClearSelection();
			this.ddlWorkflowStatus.ClearSelection();
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

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			WorkflowCollection bindItems = this.GetWorkflows();
			Person currentUser = PersonController.GetCurrentUser();
			try
			{
				List<int> WorkflowIds = bindItems.GetIds();
				WorkflowIds.Sort();
				this.lblIdText.Text = string.Join(", ", WorkflowIds.ToStrings().ToArray());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}

			if (bindItems.Count > 0)
			{
				int status = bindItems[0].WorkflowStatusId;
				if (bindItems.All(x => x.WorkflowStatusId == status))
				{
					ddlWorkflowStatus.ForceSelectedValue(status);
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the Workflow's current state
			List<UserRoleId> currentUsersRoles = PersonController.GetCurrentUser().GetRoles();
			bool isDataModificationAllowed = this.IsDataModificationAllowed();
			bool isMetadataModificationAllowed = this.IsMetadataModificationAllowed();

			// only allow change parameter if there is only a single workflowType
            //if ((bindItems.Count > 0) && bindItems.IsIdenticalWorkflowType())
            //{
            //    this.pnlDataControls_WorkflowModuleEditListUpdate.Enabled = isDataModificationAllowed;
            //}
            //else
            //{
            //    this.pnlDataControls_WorkflowModuleEditListUpdate.Enabled = false;
            //}

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
			return this.GetWorkflows().IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			return this.GetWorkflows().IsMetadataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
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
			bool WorkflowModified = false;

			List<CPSLog> cpsLogList = new List<CPSLog>();
			WorkflowCollection Workflows = this.GetWorkflows();
			foreach (Workflow saveItem in Workflows)
			{
				WorkflowModified = false;
				if (dataModificationAllowed)
				{
					if (!string.IsNullOrEmpty(this.txtName.Text.TrimToNull()))
					{
						saveItem.Name = this.txtName.Text.TrimToNull();
						WorkflowModified = true;
					}

					if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
					{
						saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
						WorkflowModified = true;
					}

					if (!string.IsNullOrEmpty(this.ddlWorkflowApplication.SelectedValue))
					{
						saveItem.WorkflowApplicationId = Convert.ToInt32(this.ddlWorkflowApplication.SelectedValue);
						WorkflowModified = true;
					}

					if (!string.IsNullOrEmpty(this.ddlWorkflowType.SelectedValue))
					{
						saveItem.WorkflowTypeId = Convert.ToInt32(this.ddlWorkflowType.SelectedValue);
						WorkflowModified = true;
					}

					if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
					{
						saveItem.OwnerId = string.IsNullOrEmpty(this.ddlOwner.SelectedValue) ? PersonController.GetCurrentUser().Id : Convert.ToInt32(this.ddlOwner.SelectedValue);
						WorkflowModified = true;
					}

					if (WorkflowModified)
					{
						saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
						saveItem.Log(Severity.Info, "Workflow data saved.");
					}

					//save the modules if they were changed.
                    if (this.ucWorkflowModuleEditListUpdate.SelectionCount() > 0)
                    {
                        this.ucWorkflowModuleEditListUpdate.SaveInput(saveItem.Id);
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
							WorkflowModified = true;
						}
					}

					if (this.pnlDataControls_TagsToRemove.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToRemove.Text))
						{
							saveItem.RemoveTags(Tag.ParseTagNameList(this.txtTagsToRemove.Text, false));
							WorkflowModified = true;
						}
					}
				}

				if (WorkflowModified)
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
				WorkflowCollection originalWorkflow = this.GetWorkflows();
				WorkflowCollection newWorkflows = originalWorkflow.SaveAllAsNew(true);

				//Apply change:
				foreach (var workflow in newWorkflows)
				{
					string name = txtName.Text.Trim();
					if (!string.IsNullOrEmpty(name))
						workflow.Name = name;

                    string description = txtDescription.Text.Trim();
                    if (!string.IsNullOrEmpty(description))
                        workflow.Description = description;

                    if (!string.IsNullOrEmpty(this.ddlWorkflowApplication.SelectedValue))
                    {
                        workflow.WorkflowApplicationId = Convert.ToInt32(this.ddlWorkflowApplication.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(this.ddlWorkflowType.SelectedValue))
                    {
                        workflow.WorkflowTypeId = Convert.ToInt32(this.ddlWorkflowType.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
                    {
                        workflow.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
                    }

					workflow.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                    //save the modules if they were changed.
                    if (this.ucWorkflowModuleEditListUpdate.SelectionCount() > 0)
                    {
                        this.ucWorkflowModuleEditListUpdate.SaveInput(workflow.Id);
                    }
                }
				//TODO:Tags??

				scope.Complete(); // transaction complete

                //next, update the state of the UI controls whose values should not affect the state of the new Workflow
                this.ddlWorkflowStatus.ForceSelectedValue(WorkflowStateId.Modified);

				WorkflowQuerySpecification WorkflowQuerySpecification = new WorkflowQuerySpecification() { IdList = newWorkflows.GetIds() };
				this.QuerySpecification = WorkflowQuerySpecification;
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
				//get Workflow to delete
				WorkflowCollection Workflows = this.GetWorkflows();

				//Apply change:
				foreach (var Workflow in Workflows)
				{
					if (Workflow.WorkflowState == WorkflowStateId.Abandoned)
					{
						Workflow.Delete();
						cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Success Delete"));
					}
					else
					{
						cpsLogList.Add(new CPSLog(Workflow.Id, Workflow.Description, Workflow.Name, "Unable to Delete: Workflow must be 'Abandoned'."));						
					}
				}
				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlWorkflowStatus.ForceSelectedValue(WorkflowStateId.Deleted);
			}

			if (cpsLogList.Count > 0)
			{
				this.pnlCPSLog.Visible = true;
				this.ucCPSLog.CPSLogList = cpsLogList;
				this.ucCPSLog.DataBind();
			}
		}

		private VwMapWorkflowCollection GetDataSource()
		{
			WorkflowQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapWorkflowCollection WorkflowCollection = VwMapWorkflowController.Fetch(query);
			if (WorkflowCollection.Count < 1)
			{
				if (this.pnlEditArea.Enabled)
				{
					this.SetErrorMessage("Invalid data. No data matches the specified search criteria.");
					this.pnlEditArea.Enabled = false;
				}
			}
			return WorkflowCollection;
		}

		/// <summary>
		/// Gets the <see cref="Workflow"/>s corresponding to the items in the <see cref="VwMapWorkflowCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private WorkflowCollection GetWorkflows()
		{
			return this.GetDataSource().GetWorkflows();
		}

		#endregion

	}
}