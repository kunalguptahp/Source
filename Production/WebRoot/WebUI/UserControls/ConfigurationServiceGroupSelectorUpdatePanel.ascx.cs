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
    public partial class ConfigurationServiceGroupSelectorUpdatePanel : RecordDetailUserControl
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
    		ConfigurationServiceGroup csg = ConfigurationServiceGroup.FetchByID(this.hdnConfigurationServiceGroupId.Value.TryParseInt32() ?? 0);
    		if (csg != null)
    		{
    			VwMapQueryParameterConfigurationServiceGroupTypeCollection queryParameterCollection =
    				VwMapQueryParameterConfigurationServiceGroupTypeController.FetchByConfigurationServiceGroupTypeId(
    					csg.ConfigurationServiceGroupTypeId, (int?)RowStatus.RowStatusId.Active);
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

			lblError.Text = string.Format("Configuration Service Group Selection deleted.");
			pnlEditArea.Enabled = false;
		}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.Page.Validate("SaveUniqueNameConfigurationServiceGroupSelector");
            if (!this.Page.IsValid)
            {
                return;
            }
            this.OnInputSave(new EventArgs());
        }

        protected void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            this.Page.Validate("SaveAsUniqueNameConfigurationServiceGroupSelector");
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
			VwMapConfigurationServiceGroupSelectorCollection configurationServiceGroupSelectorColl = VwMapConfigurationServiceGroupSelectorController.FetchByConfigurationServiceGroupIdName(this.hdnConfigurationServiceGroupId.Value.TryParseInt32() ?? 0, this.txtName.Text);
			switch (configurationServiceGroupSelectorColl.Count)
			{
				case 0:
					// unique name
					args.IsValid = true;
					break;
				case 1:
					// unique if updating same configuration service group selector
                    args.IsValid = (configurationServiceGroupSelectorColl[0].Id == this.DataSourceId);
                    break;
				default:
					// duplicates detected (configuration service group selector names can't be the same)
					args.IsValid = false;
					break;
			}
		}

        protected void cvSaveAsNameUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            VwMapConfigurationServiceGroupSelectorCollection configurationServiceGroupSelectorColl = VwMapConfigurationServiceGroupSelectorController.FetchByConfigurationServiceGroupIdName(this.hdnConfigurationServiceGroupId.Value.TryParseInt32() ?? 0, this.txtName.Text);
            switch (configurationServiceGroupSelectorColl.Count)
            {
                case 0:
                    // unique name
                    args.IsValid = true;
                    break;
                default:
                    // duplicates detected (configuration service group selector names can't be the same)
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
                QueryParameterValueEditListUpdatePanel uc =
                    (QueryParameterValueEditListUpdatePanel)curRow.FindControl("ucQueryParameterValue");
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
                QueryParameterValueEditListUpdatePanel uc =
                    (QueryParameterValueEditListUpdatePanel)e.Item.FindControl("ucQueryParameterValue");
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
			this.lblConfigurationServiceGroupValue.Text = string.Empty;
			this.txtName.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
        }

		/// <summary>
        /// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
        /// is appropriate (for a new, non-existing record) for each such control.
        /// </summary>
        private void ApplyDataControlDefaultValues()
        {
			ConfigurationServiceGroupSelectorQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

            this.txtName.Text = defaultValuesSpecification.Name;

			if (defaultValuesSpecification.ConfigurationServiceGroupId != null)
			{
				this.lblConfigurationServiceGroupValue.Text = ElementsCPSSqlUtility.GetName("ConfigurationServiceGroup", defaultValuesSpecification.ConfigurationServiceGroupId.Value);
				this.hdnConfigurationServiceGroupId.Value = defaultValuesSpecification.ConfigurationServiceGroupId.Value.ToString();
			}
        }

        /// <summary>
        /// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
		public ConfigurationServiceGroupSelectorQuerySpecification ConvertToExpectedType(IQuerySpecification original)
        {
            if (original == null)
            {
                return null;
            }

			return original as ConfigurationServiceGroupSelectorQuerySpecification ?? new ConfigurationServiceGroupSelectorQuerySpecification(original);
        }

        protected override void BindItem()
        {
			this.UnbindItem();

			ConfigurationServiceGroupSelector bindItem = this.GetDataSource();

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
					this.hdnConfigurationServiceGroupId.Value = bindItem.ConfigurationServiceGroupId.ToString();
					this.lblConfigurationServiceGroupValue.Text = ElementsCPSSqlUtility.GetName("ConfigurationServiceGroup", bindItem.ConfigurationServiceGroupId);
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
			ConfigurationServiceGroupSelector bindItem = this.GetDataSource();
			if (bindItem == null)
				return false;

			// new configuration service group is allowed to be modified.
			ConfigurationServiceGroup csgItem = ConfigurationServiceGroup.FetchByID(this.hdnConfigurationServiceGroupId.Value.TryParseInt32() ?? 0);
			if (csgItem == null)
				return false;
	
			return csgItem.IsDataModificationAllowed(PersonController.GetCurrentUser().GetRoles());
		}

        protected override void SaveInput()
        {
            ConfigurationServiceGroupSelector saveItem = this.GetDataSource();
            saveItem.Name = this.txtName.Text.TrimToNull();
            saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
			saveItem.ConfigurationServiceGroupId = Convert.ToInt32(this.hdnConfigurationServiceGroupId.Value);
            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			foreach (RepeaterItem curRow in this.repQueryParameter.Items)
			{
				QueryParameterValueEditListUpdatePanel uc = (QueryParameterValueEditListUpdatePanel)curRow.FindControl("ucQueryParameterValue");
				HiddenField hdnQueryParameterId = (HiddenField)curRow.FindControl("hdnQueryParameterId");
				int queryParameterId = hdnQueryParameterId.Value.TryParseInt32() ?? 0;
				uc.SaveInput(saveItem.Id, queryParameterId);
			}

            //reload the control using the record's (possibly newly assigned) ID
            this.DataSourceId = saveItem.Id;
        }

        /// <summary>
        /// Saves all UI input/changes to a newly-created copy of the ConfigurationServiceGroupSelector being edited.
        /// </summary>
        private void SaveAsNew()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                //first create the new (copied) module based upon the existing (pre-save) DB data
                ConfigurationServiceGroupSelector oriConfigurationServiceGroupSelector = this.GetDataSource();
                ConfigurationServiceGroupSelector newConfigurationServiceGroupSelector = oriConfigurationServiceGroupSelector.SaveAsNew();

                //next, change the control's DataSourceId to reference the new Workflow
                this.DataSourceId = newConfigurationServiceGroupSelector.Id;

                //next, call SaveItem() to apply any UI input/changes (except for those UI controls re-bound above) to the new ConfigurationServiceGroupSelector (only)
                this.SaveInput();

                scope.Complete(); // transaction complete
            }
        }

		/// <summary>
		/// Delete the ConfigurationServiceGroupSelector being edited.
		/// </summary>
		private void Delete()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//get configurationServiceGroupSelector to delete
				ConfigurationServiceGroupSelector configurationServiceGroupSelector = this.GetDataSource();

				//delete configurationServiceGroupSelector and it's children
				configurationServiceGroupSelector.Delete();

				scope.Complete(); // transaction complete
			}
		}

		protected ConfigurationServiceGroupSelector GetDataSource()
		{
			ConfigurationServiceGroupSelector saveItem;
			if (!this.IsNewRecord)
			{
				saveItem = ConfigurationServiceGroupSelector.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new ConfigurationServiceGroupSelector(true);
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
			foreach (RepeaterItem curRow in this.repQueryParameter.Items)
			{
				QueryParameterValueEditListUpdatePanel uc = (QueryParameterValueEditListUpdatePanel)curRow.FindControl("ucQueryParameterValue");
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