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
using HP.HPFx.Web.Utility;
using Microsoft.Security.Application;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
    public partial class JumpstationGroupSelectorUpdatePanel : RecordDetailUserControl
    {
        #region Constants

        private const string MAXIMUM_SELECTOR_GROUP = "You may only create a maximum of 10 selector groups.";

        #endregion
	
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
    		JumpstationGroup csg = JumpstationGroup.FetchByID(this.hdnJumpstationGroupId.Value.TryParseInt32() ?? 0);
    		if (csg != null)
    		{
    			VwMapQueryParameterJumpstationGroupTypeCollection queryParameterCollection =
    				VwMapQueryParameterJumpstationGroupTypeController.FetchByJumpstationGroupTypeId(
    					csg.JumpstationGroupTypeId, (int?)RowStatus.RowStatusId.Active);
    			queryParameterCollection.Sort("SortOrder", true);
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
			this.btnCancel.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
			this.btnDelete.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
		}

        #endregion

        #region ControlEvents

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            JumpstationGroupSelectorQuerySpecification jumpstationGroupSelectorQuerySpecification =
                new JumpstationGroupSelectorQuerySpecification() { JumpstationGroupId = Convert.ToInt32(this.hdnJumpstationGroupId.Value) };
            // 10 is a maximum limit that can be published to Elements/DPS.
            if (JumpstationGroupSelectorController.FetchCount(jumpstationGroupSelectorQuerySpecification) >= 10)
            {
                WebUtility.ShowAlertBox(this, MAXIMUM_SELECTOR_GROUP);
            }
            else
            {
                this.OnInputSave(new EventArgs());

                this.DataSourceId = null;

                // need to keep the query parameter the same.
                int? jumpstationGroupId = Convert.ToInt32(this.hdnJumpstationGroupId.Value);
                this.DefaultValuesSpecification = new JumpstationGroupSelectorQuerySpecification { JumpstationGroupId = (jumpstationGroupId ?? -1) };
                this.Response.Redirect(Global.GetJumpstationGroupSelectorUpdatePageUri(null, this.DefaultValuesSpecification));
            }
        }

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}
			this.Delete();

			lblError.Text = string.Format("Jumpstation Group Selection deleted.");
			pnlEditArea.Enabled = false;
		}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }
            this.OnInputSave(new EventArgs());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.OnInputCancel(new EventArgs());
        }

		protected void cvNameUnique_ServerValidate(object source, ServerValidateEventArgs args)
		{
			VwMapJumpstationGroupSelectorCollection JumpstationGroupSelectorColl = VwMapJumpstationGroupSelectorController.FetchByJumpstationGroupIdName(this.hdnJumpstationGroupId.Value.TryParseInt32() ?? 0, this.txtName.Text);
			switch (JumpstationGroupSelectorColl.Count)
			{
				case 0:
					// unique name
					args.IsValid = true;
					break;
				case 1:
					// unique if updating same jumpstation group selector
					args.IsValid = (JumpstationGroupSelectorColl[0].Id == this.DataSourceId);
					break;
				default:
					// duplicates detected (jumpstation selector names can't be the same)
					args.IsValid = false;
					break;
			}
		}

        protected void cvSaveAsNameUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            VwMapJumpstationGroupSelectorCollection jumpstationSelectorColl = VwMapJumpstationGroupSelectorController.FetchByJumpstationGroupIdName(this.hdnJumpstationGroupId.Value.TryParseInt32() ?? 0, this.txtName.Text);
            switch (jumpstationSelectorColl.Count)
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
                JumpstationQueryParameterValueEditListUpdatePanel uc =
                    (JumpstationQueryParameterValueEditListUpdatePanel)curRow.FindControl("ucQueryParameterValue");
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
                JumpstationQueryParameterValueEditListUpdatePanel uc =
                    (JumpstationQueryParameterValueEditListUpdatePanel)e.Item.FindControl("ucQueryParameterValue");
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
			this.lblJumpstationGroupValue.Text = string.Empty;
			this.txtName.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
        }

		/// <summary>
        /// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
        /// is appropriate (for a new, non-existing record) for each such control.
        /// </summary>
        private void ApplyDataControlDefaultValues()
        {
			JumpstationGroupSelectorQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

            this.txtName.Text = defaultValuesSpecification.Name;

			if (defaultValuesSpecification.JumpstationGroupId != null)
			{
				this.lblJumpstationGroupValue.Text = ElementsCPSSqlUtility.GetName("JumpstationGroup", defaultValuesSpecification.JumpstationGroupId.Value);
				this.hdnJumpstationGroupId.Value = defaultValuesSpecification.JumpstationGroupId.Value.ToString();
			}
        }

        /// <summary>
        /// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
		public JumpstationGroupSelectorQuerySpecification ConvertToExpectedType(IQuerySpecification original)
        {
            if (original == null)
            {
                return null;
            }

			return original as JumpstationGroupSelectorQuerySpecification ?? new JumpstationGroupSelectorQuerySpecification(original);
        }

        protected override void BindItem()
        {
			this.UnbindItem();

			JumpstationGroupSelector bindItem = this.GetDataSource();

			if (this.IsNewRecord)
			{
			    this.ApplyDataControlDefaultValues();
			    this.btnCreate.Enabled = false;
			    this.btnDelete.Enabled = false;
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
					this.hdnJumpstationGroupId.Value = bindItem.JumpstationGroupId.ToString();
					this.lblJumpstationGroupValue.Text = ElementsCPSSqlUtility.GetName("JumpstationGroup", bindItem.JumpstationGroupId);
					this.txtName.Text = bindItem.Name;
					this.txtDescription.Text = bindItem.Description;
                    this.btnCreate.Enabled = true;
                    this.btnDelete.Enabled = true;
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
			this.btnCancel.Enabled = isDataModificationAllowed;
		}

		protected bool IsDataModificationAllowed()
		{
			JumpstationGroupSelector bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;

			// new jumpstation is allowed to be modified.
			JumpstationGroup csgItem = JumpstationGroup.FetchByID(this.hdnJumpstationGroupId.Value.TryParseInt32() ?? 0);
			if (csgItem == null)
				return false;
	
			return csgItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

        protected override void SaveInput()
        {
            JumpstationGroupSelector saveItem = this.GetDataSource();
            saveItem.Name = this.txtName.Text.TrimToNull();
            saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
            saveItem.JumpstationGroupId = Convert.ToInt32(this.hdnJumpstationGroupId.Value);
            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                foreach (RepeaterItem curRow in this.repQueryParameter.Items)
                {
                    JumpstationQueryParameterValueEditListUpdatePanel uc = (JumpstationQueryParameterValueEditListUpdatePanel)curRow.FindControl("ucQueryParameterValue");
                    HiddenField hdnQueryParameterId = (HiddenField)curRow.FindControl("hdnQueryParameterId");
                    int queryParameterId = hdnQueryParameterId.Value.TryParseInt32() ?? 0;

                     uc.SaveInput(saveItem.Id, queryParameterId);
                }
                //reload the control using the record's (possibly newly assigned) ID
                this.DataSourceId = saveItem.Id;

                JumpstationGroupPivot pivot = JumpstationGroupPivot.FetchByID(saveItem.JumpstationGroupId);
                VwMapJumpstationGroupCalcOnFly origial = VwMapJumpstationGroupCalcOnFlyController.FetchValue(saveItem.JumpstationGroupId);
                pivot.CopyFromCalcOnFly(origial);
                pivot.Save();




              
         
        }




		/// <summary>
		/// Delete the JumpstationGroupSelector being edited.
		/// </summary>
		private void Delete()
		{
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0))) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//get JumpstationGroupSelector to delete
				JumpstationGroupSelector jumpstationGroupSelector = this.GetDataSource();

				//delete JumpstationGroupSelector and it's children
				jumpstationGroupSelector.Delete();

                //delete from JumpstationGroupPivot by re-bind
                ElementsCPS.Data.SubSonicClient.JumpstationGroupPivot pivot =  JumpstationGroupPivot.FetchByID(Convert.ToInt32(this.hdnJumpstationGroupId.Value));
                VwMapJumpstationGroupCalcOnFly origial = ElementsCPS.Data.SubSonicClient.VwMapJumpstationGroupCalcOnFlyController.FetchByID( Convert.ToInt32(this.hdnJumpstationGroupId.Value))[0];
                pivot.CopyFromCalcOnFly(origial);
                pivot.Save();
				scope.Complete(); // transaction complete
			}
		}

		protected JumpstationGroupSelector GetDataSource()
		{
			JumpstationGroupSelector saveItem;
			if (!this.IsNewRecord)
			{
				saveItem = JumpstationGroupSelector.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new JumpstationGroupSelector(true);
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
			foreach (RepeaterItem curRow in this.repQueryParameter.Items)
			{
				JumpstationQueryParameterValueEditListUpdatePanel uc = (JumpstationQueryParameterValueEditListUpdatePanel)curRow.FindControl("ucQueryParameterValue");
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