using System;
using System.Collections.Generic;
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
using System.Transactions;
using Microsoft.Security.Application;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class ConfigurationServiceGroupImportUpdatePanel : RecordDetailUserControl
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
                this.pnlEditArea.Enabled = 
                    SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
            }
		}
         
        /// <summary>
        /// Hides/shows controls based upon the user's roles and the controls' required permissions.
        /// </summary>
        private void HideControlsFromUnauthorizedUsers()
        {
            this.btnSave.Visible =
                SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
            this.btnImport.Visible =
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

            this.lblError.Text = string.Format("Configuration Service Group Import deleted.");
            this.pnlEditArea.Enabled = false;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            this.Import();

            BindItem();
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

        protected void cvConfigurationServiceApplicationName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (VwMapConfigurationServiceApplicationController.FetchByElementsKey(this.txtConfigurationServiceApplicationName.Text) != null);
        }

        protected void cvConfigurationServiceGroupTypeName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (VwMapConfigurationServiceGroupTypeController.FetchByElementsKeyGroupTypeName(this.txtConfigurationServiceApplicationName.Text, this.txtConfigurationServiceGroupTypeName.Text) != null); ;
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
			Global.BindRowStatusListControl(this.ddlStatus);
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
            this.lblCPSIdValue.Text = string.Empty;
            this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
            this.txtConfigurationServiceApplicationName.Text = string.Empty;
            this.txtConfigurationServiceGroupTypeName.Text = string.Empty;
            this.txtImportMessage.Text = string.Empty;
            this.txtImportStatus.Text = string.Empty;
            this.txtImportId.Text = string.Empty;
            this.ddlStatus.ClearSelection();
		}

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			ConfigurationServiceGroupImportQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

			this.txtName.Text = defaultValuesSpecification.Name;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
        public ConfigurationServiceGroupImportQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

            return original as ConfigurationServiceGroupImportQuerySpecification ?? new ConfigurationServiceGroupImportQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			if (this.IsNewRecord)
			{ this.ApplyDataControlDefaultValues(); }
			else
			{
				try
				{
                    ConfigurationServiceGroupImport bindItem = this.GetDataSource();
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
                    this.lblCPSIdValue.Text = bindItem.ConfigurationServiceGroupId.ToString();
                    this.txtName.Text = bindItem.Name;
					this.txtDescription.Text = bindItem.Description;
				    this.txtConfigurationServiceGroupTypeName.Text = bindItem.ConfigurationServiceGroupTypeName;
                    this.txtConfigurationServiceApplicationName.Text = bindItem.ConfigurationServiceApplicationName;
                    this.txtImportStatus.Text = bindItem.ImportStatus;
                    this.txtImportMessage.Text = bindItem.ImportMessage;
                    this.txtImportId.Text = bindItem.ImportId;
                    this.ddlStatus.ForceSelectedValue((RowStatus.RowStatusId)bindItem.RowStatusId);

                    this.btnImport.Enabled = (bindItem.ImportStatus == "Add") && (bindItem.RowStatusId == (int)RowStatus.RowStatusId.Active);
				}
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}
		}

		protected override void SaveInput()
		{
            ConfigurationServiceGroupImport saveItem = this.GetDataSource();
            saveItem.RowStatusId = string.IsNullOrEmpty(this.ddlStatus.SelectedValue) ? (int)RowStatus.RowStatusId.Active : Convert.ToInt32(this.ddlStatus.SelectedValue);
			saveItem.Name = this.txtName.Text.TrimToNull();
            saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
            saveItem.ConfigurationServiceGroupTypeName = this.txtConfigurationServiceGroupTypeName.Text.TrimToNull();
            saveItem.ConfigurationServiceApplicationName = this.txtConfigurationServiceApplicationName.Text.TrimToNull();
            saveItem.ImportStatus = this.txtImportStatus.Text.TrimToNull();
            saveItem.ImportMessage = this.txtImportMessage.Text.TrimToNull();
            saveItem.ImportId = this.txtImportId.Text.TrimToNull();
			saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}

        /// <summary>
        /// Saves all UI input/changes to a newly-created copy of the ConfigurationServiceGroup being edited.
        /// </summary>
        private void Import()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                // Import into configurationServiceGroup
                ConfigurationServiceGroupImport configurationServiceGroupImport = this.GetDataSource();
                configurationServiceGroupImport.Import();
                scope.Complete(); // transaction complete
            }
            BindItem();
        }

        /// <summary>
        /// Delete the ConfigurationServiceGroupImport being edited.
        /// </summary>
        private void Delete()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                //get configurationServiceGroupImport to delete
                ConfigurationServiceGroupImport configurationServiceGroupImport = this.GetDataSource();

                //delete configurationServiceGroupImport and it's children
                configurationServiceGroupImport.Delete();

                scope.Complete(); // transaction complete
            }
        }

        protected ConfigurationServiceGroupImport GetDataSource()
        {
            ConfigurationServiceGroupImport saveItem;
            if (!this.IsNewRecord)
            {
                saveItem = ConfigurationServiceGroupImport.FetchByID(this.DataSourceId);
            }
            else
            {
                saveItem = new ConfigurationServiceGroupImport(true);
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
			this.UpdateConfigurationServiceLabelValueImportListImmutableConditions();
            this.UpdateConfigurationServiceQueryParameterValueImportListImmutableConditions();
        }

        private void UpdateConfigurationServiceLabelValueImportListImmutableConditions()
        {
            this.pnlConfigurationServiceLabelValueImportListPanel.Visible = this.DataSourceId != null;

            this.ucConfigurationServiceLabelValueImportList.ImmutableQueryConditions = new ConfigurationServiceLabelValueImportQuerySpecification { ConfigurationServiceGroupImportId = (this.DataSourceId ?? -1) };
        }

        private void UpdateConfigurationServiceQueryParameterValueImportListImmutableConditions()
        {
            this.pnlConfigurationServiceQueryParameterValueImportListPanel.Visible = this.DataSourceId != null;

            this.ucConfigurationServiceQueryParameterValueImportList.ImmutableQueryConditions = new ConfigurationServiceQueryParameterValueImportQuerySpecification { ConfigurationServiceGroupImportId = (this.DataSourceId ?? -1) };
        }

		#endregion
	}
}