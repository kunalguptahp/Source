using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using Microsoft.Security.Application;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
    public partial class WorkflowSelectorUpdatePanel : RecordDetailUserControl
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
				this.LoadQueryParameters();
				this.UpdateChildListImmutableConditions();
            }
        }

		/// <summary>
		/// Databinds the Query Parameter Repeater.  The controls must exist prior to setting the values.
		/// </summary>
		private void LoadQueryParameters()
    	{
    		Workflow wrk = Workflow.FetchByID(this.hdnWorkflowId.Value.TryParseInt32() ?? 0);
    		if (wrk != null)
    		{
    			VwMapQueryParameterWorkflowTypeCollection queryParameterCollection =
    				VwMapQueryParameterWorkflowTypeController.FetchByWorkflowTypeId(
    					wrk.WorkflowTypeId, (int?)RowStatus.RowStatusId.Active);
    			queryParameterCollection.Sort("QueryParameterName", true);
    			this.repQueryParameter.DataSource = queryParameterCollection;
    			this.repQueryParameter.DataBind();
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
		}

        #endregion

        #region ControlEvents

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}
			this.Delete();

			lblError.Text = string.Format("Workflow Selection deleted.");
			pnlEditArea.Enabled = false;
		}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.Page.Validate("SaveUniqueNameWorkflowSelector");
            if (!this.Page.IsValid)
            {
                return;
            }
            this.OnInputSave(new EventArgs());
        }

        protected void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            this.Page.Validate("SaveAsUniqueNameWorkflowSelector");
            if (!this.Page.IsValid)
            {
                return;
            }
            this.SaveAsNew();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.OnInputCancel(new EventArgs());
        }

		protected void cvSaveNameUnique_ServerValidate(object source, ServerValidateEventArgs args)
		{
			VwMapWorkflowSelectorCollection workflowSelectorColl = VwMapWorkflowSelectorController.FetchByWorkflowIdName(this.hdnWorkflowId.Value.TryParseInt32() ?? 0, this.txtName.Text);
			switch (workflowSelectorColl.Count)
			{
				case 0:
					// unique name
					args.IsValid = true;
					break;
				case 1:
					// unique if updating same workflow selector
                    args.IsValid = (workflowSelectorColl[0].Id == this.DataSourceId);
                    break;
				default:
					// duplicates detected (workflow selector names can't be the same)
					args.IsValid = false;
					break;
			}
		}

        protected void cvSaveAsNameUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            VwMapWorkflowSelectorCollection workflowSelectorColl = VwMapWorkflowSelectorController.FetchByWorkflowIdName(this.hdnWorkflowId.Value.TryParseInt32() ?? 0, this.txtName.Text);
            switch (workflowSelectorColl.Count)
            {
                case 0:
                    // unique name
                    args.IsValid = true;
                    break;
                default:
                    // duplicates detected (workflow selector names can't be the same)
                    args.IsValid = false;
                    break;
            }
        }

		protected void cvQueryParameterValue_ServerValidate(object source, ServerValidateEventArgs args)
		{
            bool parameterValueValid = true;
            
            // One query parameter value must be selected.
			foreach (RepeaterItem curRow in this.repQueryParameter.Items)
			{
				WorkflowQueryParameterValueEditListUpdatePanel uc =
					(WorkflowQueryParameterValueEditListUpdatePanel) curRow.FindControl("ucQueryParameterValue");
				if (!uc.IsMinimumValueSelected())
				{
					parameterValueValid = false;
				}

                if (uc.IsMaximumValueExceeded())
                {
                    parameterValueValid = false;
                }
            }

            if (parameterValueValid)
                args.IsValid = true;
            else
                args.IsValid = false;

            return;
        }

        protected void repQueryParameter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
                WorkflowQueryParameterValueEditListUpdatePanel uc =
                    (WorkflowQueryParameterValueEditListUpdatePanel)e.Item.FindControl("ucQueryParameterValue");
                HiddenField hdnMaximumSelection = (HiddenField)e.Item.FindControl("hdnMaximumSelection");
                uc.MaximumSelection = hdnMaximumSelection.Value.TryParseInt32() ?? 0;
            }
        }

		#endregion

        #region Methods

        protected override void UnbindItem()
        {
            base.UnbindItem();

            this.ClearDataControls();
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
			this.lblWorkflowValue.Text = string.Empty;
			this.txtName.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
        }

		/// <summary>
        /// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
        /// is appropriate (for a new, non-existing record) for each such control.
        /// </summary>
        private void ApplyDataControlDefaultValues()
        {
			WorkflowSelectorQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

            this.txtName.Text = defaultValuesSpecification.Name;

			if (defaultValuesSpecification.WorkflowId != null)
			{
				this.lblWorkflowValue.Text = ElementsCPSSqlUtility.GetName("Workflow", defaultValuesSpecification.WorkflowId.Value);
				this.hdnWorkflowId.Value = defaultValuesSpecification.WorkflowId.Value.ToString();
			}
        }

        /// <summary>
        /// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
		public WorkflowSelectorQuerySpecification ConvertToExpectedType(IQuerySpecification original)
        {
            if (original == null)
            {
                return null;
            }

			return original as WorkflowSelectorQuerySpecification ?? new WorkflowSelectorQuerySpecification(original);
        }

        protected override void BindItem()
        {
			this.UnbindItem();

			WorkflowSelector bindItem = this.GetDataSource();

			if (this.IsNewRecord)
			{
				this.ApplyDataControlDefaultValues();
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
					this.hdnWorkflowId.Value = bindItem.WorkflowId.ToString();
					this.lblWorkflowValue.Text = ElementsCPSSqlUtility.GetName("Workflow", bindItem.WorkflowId);
					this.txtName.Text = bindItem.Name;
					this.txtDescription.Text = bindItem.Description;

				}
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}

			bool isDataModificationAllowed = this.IsDataModificationAllowed();
			this.btnDelete.Enabled = isDataModificationAllowed;
			this.btnSave.Enabled = isDataModificationAllowed;
            this.btnSaveAsNew.Enabled = !this.IsNewRecord;
            this.btnCancel.Enabled = isDataModificationAllowed;
		}

		protected bool IsDataModificationAllowed()
		{
			WorkflowSelector bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;

			// new workflow is allowed to be modified.
			Workflow wrkItem = Workflow.FetchByID(this.hdnWorkflowId.Value.TryParseInt32() ?? 0);
			if (wrkItem == null)
				return false;
	
			return wrkItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

        protected override void SaveInput()
        {
            WorkflowSelector saveItem = this.GetDataSource();
            saveItem.Name = this.txtName.Text.TrimToNull();
            saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
			saveItem.WorkflowId = Convert.ToInt32(this.hdnWorkflowId.Value);
            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			foreach (RepeaterItem curRow in this.repQueryParameter.Items)
			{
				WorkflowQueryParameterValueEditListUpdatePanel uc = (WorkflowQueryParameterValueEditListUpdatePanel)curRow.FindControl("ucQueryParameterValue");
				HiddenField hdnQueryParameterId = (HiddenField)curRow.FindControl("hdnQueryParameterId");
				int queryParameterId = hdnQueryParameterId.Value.TryParseInt32() ?? 0;
				uc.SaveInput(saveItem.Id, queryParameterId);
			}

            //reload the control using the record's (possibly newly assigned) ID
            this.DataSourceId = saveItem.Id;
        }

        /// <summary>
        /// Saves all UI input/changes to a newly-created copy of the WorkflowSelector being edited.
        /// </summary>
        private void SaveAsNew()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                //first create the new (copied) module based upon the existing (pre-save) DB data
                WorkflowSelector oriWorkflowSelector = this.GetDataSource();
                WorkflowSelector newWorkflowSelector = oriWorkflowSelector.SaveAsNew();

                //next, change the control's DataSourceId to reference the new Workflow
                this.DataSourceId = newWorkflowSelector.Id;

                //next, call SaveItem() to apply any UI input/changes (except for those UI controls re-bound above) to the new WorkflowSelector (only)
                this.SaveInput();

                scope.Complete(); // transaction complete
            }
        }

		/// <summary>
		/// Delete the WorkflowSelector being edited.
		/// </summary>
		private void Delete()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//get workflowSelector to delete
				WorkflowSelector workflowSelector = this.GetDataSource();

				//delete workflowSelector and it's children
				workflowSelector.Delete();

				scope.Complete(); // transaction complete
			}
		}

		protected WorkflowSelector GetDataSource()
		{
			WorkflowSelector saveItem;
			if (!this.IsNewRecord)
			{
				saveItem = WorkflowSelector.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new WorkflowSelector(true);
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
		}

		private void UpdateWorkflowSelectorListImmutableConditions()
		{
			foreach (RepeaterItem curRow in this.repQueryParameter.Items)
			{
				WorkflowQueryParameterValueEditListUpdatePanel uc = (WorkflowQueryParameterValueEditListUpdatePanel)curRow.FindControl("ucQueryParameterValue");
				HiddenField hdnQueryParameterId = (HiddenField)curRow.FindControl("hdnQueryParameterId");
				int queryParameterId = hdnQueryParameterId.Value.TryParseInt32() ?? 0;
                HiddenField hdnWildcard = (HiddenField)curRow.FindControl("hdnWildcard");
                bool wildcard = hdnWildcard.Value.TryParseBoolean() ?? false;
                uc.ImmutableQueryConditions =
					new QueryParameterValueQuerySpecification
					{
						QueryParameterId = queryParameterId,
                        Wildcard = wildcard,
						RowStatusId = (int?)RowStatus.RowStatusId.Active
					};

				uc.DataBind();
			}
		}

		#endregion

    }
}