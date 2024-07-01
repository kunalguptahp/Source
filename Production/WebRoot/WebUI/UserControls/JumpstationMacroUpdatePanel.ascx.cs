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
	public partial class JumpstationMacroUpdatePanel : RecordDetailUserControl
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
				JumpstationMacro.IsStateTransitionPossible(JumpstationMacroStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Visible =
				JumpstationMacro.IsStateTransitionPossible(JumpstationMacroStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Visible =
				JumpstationMacro.IsStateTransitionPossible(JumpstationMacroStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Visible =
				JumpstationMacro.IsStateTransitionPossible(JumpstationMacroStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Visible =
				JumpstationMacro.IsStateTransitionPossible(JumpstationMacroStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Visible =
				JumpstationMacro.IsStateTransitionPossible(JumpstationMacroStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Visible =
				JumpstationMacro.IsStateTransitionPossible(JumpstationMacroStateId.Validated, currentUsersRoles);
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

				//next, validate using the controls' PublishableJumpstationMacro validators
				this.Page.Validate("PublishableJumpstationMacro");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, validate using the controls' DuplicateJumpstationMacro validators
				this.Page.Validate("DuplicateJumpstationMacro");
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

				//next, validate using the controls' PublishableJumpstationMacro validators
				this.Page.Validate("PublishableJumpstationMacro");
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

				//next, validate using the controls' PublishableJumpstationMacro validators
				this.Page.Validate("PublishableJumpstationMacro");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, cannot publish a JumpstationMacro to be replaced.
				this.Page.Validate("ReplacementJumpstationMacro");
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
				//next, validate using the controls' PublishableJumpstationMacro validators
				this.Page.Validate("PublishableJumpstationMacro");
				if (!this.Page.IsValid)
				{
					return;
				}

				//next, cannot publish a JumpstationMacro to be replaced.
				this.Page.Validate("ReplacementJumpstationMacro");
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

		protected void cvJumpstationMacroPublishable_ServerValidate(object source, ServerValidateEventArgs args)
		{
			JumpstationMacro JumpstationMacro = this.GetDataSource();
			JumpstationMacroStateId toState;
			switch (JumpstationMacro.JumpstationMacroState)
			{
				case JumpstationMacroStateId.Modified:
				case JumpstationMacroStateId.Abandoned:
					toState = JumpstationMacroStateId.ReadyForValidation;
					break;
				case JumpstationMacroStateId.ReadyForValidation:
					toState = JumpstationMacroStateId.Validated;
					break;
				default:
					toState = JumpstationMacroStateId.Published;
					break;
			}
			args.IsValid = JumpstationMacro.IsStateTransitionAllowed(toState, PersonController.GetCurrentUser().GetRoles());
		}

		protected void cvJumpstationMacroDuplicate_ServerValidate(object source, ServerValidateEventArgs args)
	    {
			JumpstationMacro jumpstationMacro = this.GetDataSource();

			// check to see if replacement
			if (jumpstationMacro.ValidationId == null && jumpstationMacro.ProductionId == null)
			{
				args.IsValid = true;
			}
		}

		/// <summary>
		/// Do not allow JumpstationMacro to be replaced to be published or unpublished
		/// </summary>
		protected void cvJumpstationMacroReplacement_ServerValidate(object source, ServerValidateEventArgs args)
		{
			JumpstationMacro jumpstationMacro = this.GetDataSource();
			args.IsValid = !jumpstationMacro.IsOriginalJumpstationMacroReplacement();					
		}

        protected void cvNameUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            VwMapJumpstationMacroCollection jumpstationMacroColl = VwMapJumpstationMacroController.FetchByName(this.txtName.Text);
            switch (jumpstationMacroColl.Count)
            {
                case 0:
                    // unique name
                    args.IsValid = true;
                    break;
                case 1:
                    // unique if updating same macro
                    args.IsValid = (jumpstationMacroColl[0].Id == this.DataSourceId);
                    break;
                default:
                    // duplicates detected (macro names can't be the same)

                    // check for replacement
                    foreach (VwMapJumpstationMacro jsMacro in jumpstationMacroColl)
                    {
                        if (jsMacro.ValidationId == null)
                        {
                            args.IsValid = false;
                            break;
                        }
                    }
                    args.IsValid = true;
                    break;
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
			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			Global.BindJumpstationMacroStatusListControl(this.ddlJumpstationMacroStatus, RowStatus.RowStatusId.Active);
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
			this.ddlOwner.ClearSelection();
			this.ddlJumpstationMacroStatus.ClearSelection();
			this.txtDescription.Text = string.Empty;
		    this.txtDefaultResultValue.Text = string.Empty;
		}

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			//default the JumpstationMacro Status to Modified
			this.ddlJumpstationMacroStatus.ClearSelection();
			this.ddlJumpstationMacroStatus.ForceSelectedValue(JumpstationMacroStateId.Modified);

			//default the Proxy URL to current user
			Person currentUser = PersonController.GetCurrentUser();
			Global.ForceSelectedValue(this.ddlOwner, currentUser);
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public JumpstationMacroQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as JumpstationMacroQuerySpecification ?? new JumpstationMacroQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			JumpstationMacro bindItem = this.GetDataSource();

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
				    this.txtName.Text = bindItem.Name;
					this.ddlJumpstationMacroStatus.ForceSelectedValue(bindItem.JumpstationMacroStatusId);
					this.ddlOwner.ForceSelectedValue(bindItem.OwnerId);
					this.txtDescription.Text = bindItem.Description;
				    this.txtDefaultResultValue.Text = bindItem.DefaultResultValue;

				    // set the proxyULRId for the query parameter value user control
				    //this.ucQueryParameterValueEditUpdatePanel.JumpstationMacroId = bindItem.Id;

				}
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}

			//enable/disable and hide/show controls based upon the user's roles and the JumpstationMacro's current state
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
				bindItem.IsStateTransitionAllowed(JumpstationMacroStateId.Deleted, currentUsersRoles);
			this.btnAbandon.Enabled =
				bindItem.IsStateTransitionAllowed(JumpstationMacroStateId.Abandoned, currentUsersRoles);
			this.btnPublish.Enabled =
				 bindItem.IsStateTransitionAllowed(JumpstationMacroStateId.Published, currentUsersRoles);
			this.btnReadyForValidation.Enabled =
				bindItem.IsStateTransitionAllowed(JumpstationMacroStateId.ReadyForValidation, currentUsersRoles);
			this.btnRework.Enabled =
				bindItem.IsStateTransitionAllowed(JumpstationMacroStateId.Modified, currentUsersRoles);
			this.btnUnPublish.Enabled =
				bindItem.IsStateTransitionAllowed(JumpstationMacroStateId.Cancelled, currentUsersRoles);
			this.btnValidate.Enabled =
				bindItem.IsStateTransitionAllowed(JumpstationMacroStateId.Validated, currentUsersRoles);

			//only Coordinators are allowed to set or change the JumpstationMacro Owner
			this.ddlOwner.Enabled =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator);
		}

		protected bool IsDataModificationAllowed()
		{
			JumpstationMacro bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;
			else
				return bindItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

		protected bool IsMetadataModificationAllowed()
		{
			JumpstationMacro bindItem = this.GetDataSource();
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

			JumpstationMacro saveItem;
			if (this.DataSourceId != null)
			{
				saveItem = JumpstationMacro.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new JumpstationMacro(true);
			}

			if (dataModificationAllowed)
			{
			    saveItem.Name = this.txtName.Text.TrimToNull();
				saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
			    saveItem.DefaultResultValue = this.txtDefaultResultValue.Text.TrimToNull();
				saveItem.OwnerId = Convert.ToInt32(this.ddlOwner.SelectedValue);
				saveItem.JumpstationMacroStatusId = Convert.ToInt32(this.ddlJumpstationMacroStatus.SelectedValue);
				saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
			}

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}

		/// <summary>
		/// Saves all UI input/changes to a newly-created copy of the JumpstationMacro being edited.
		/// </summary>
		private void SaveAsNew()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//first create the new (copied) JumpstationMacro based upon the existing (pre-save) DB data
				JumpstationMacro originalJumpstationMacro = this.GetDataSource();
				JumpstationMacro newJumpstationMacro = originalJumpstationMacro.SaveAsNew(true);

				//next, change the control's DataSourceId to reference the new JumpstationMacro
				this.DataSourceId = newJumpstationMacro.Id;

				//next, update the state of the UI controls whose values should not affect the state of the new JumpstationMacro
				this.ddlJumpstationMacroStatus.ForceSelectedValue(newJumpstationMacro.JumpstationMacroState);
				this.ddlOwner.ForceSelectedValue(newJumpstationMacro.OwnerId);

				//next, call SaveItem() to apply any UI input/changes (except for those UI controls re-bound above) to the new JumpstationMacro (only)
				this.SaveInput();

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Delete the JumpstationMacro being edited.
		/// </summary>
		private void Delete()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//get JumpstationMacro to delete
				JumpstationMacro jumpstationMacro = this.GetDataSource();

				//delete JumpstationMacro and it's children
				jumpstationMacro.Delete();

				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.ddlJumpstationMacroStatus.ForceSelectedValue(JumpstationMacroStateId.Deleted);
			}
		}

		private JumpstationMacro GetDataSource()
		{
			JumpstationMacro saveItem;
			if (!this.IsNewRecord)
			{
				saveItem = JumpstationMacro.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new JumpstationMacro(true); 
				saveItem.JumpstationMacroState = JumpstationMacroStateId.Modified;
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
            this.UpdateJumpstationMacroValueListImmutableConditions();
        }

        private void UpdateJumpstationMacroValueListImmutableConditions()
        {
            this.pnlJumpstationMacroValueListPanel.Visible = this.DataSourceId != null;
            this.ucJumpstationMacroValueList.ImmutableQueryConditions = new JumpstationMacroValueQuerySpecification { JumpstationMacroId = (this.DataSourceId ?? -1) };
        }

		#endregion

	}
}