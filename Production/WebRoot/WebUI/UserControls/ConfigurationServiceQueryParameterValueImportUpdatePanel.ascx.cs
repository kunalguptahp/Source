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
using HP.ElementsCPS.Data.Utility;
using System.Transactions;
using Microsoft.Security.Application;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class ConfigurationServiceQueryParameterValueImportUpdatePanel : RecordDetailUserControl
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

            lblError.Text = string.Format("Configuration Service Query Parameter Value Import deleted.");
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
            this.lblConfigurationServiceGroupImportValue.Text = string.Empty;
            this.txtQueryParameterName.Text = string.Empty;
            this.txtQueryParameterValue.Text = string.Empty;
            this.ddlStatus.ClearSelection();
		}

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			ConfigurationServiceQueryParameterValueImportQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

            if (defaultValuesSpecification.ConfigurationServiceGroupImportId != null)
            {
                this.lblConfigurationServiceGroupImportValue.Text = ElementsCPSSqlUtility.GetName("ConfigurationServiceGroupImport", defaultValuesSpecification.ConfigurationServiceGroupImportId.Value);
                this.hdnConfigurationServiceGroupImportId.Value = defaultValuesSpecification.ConfigurationServiceGroupImportId.Value.ToString();
            }
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
        public ConfigurationServiceQueryParameterValueImportQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

            return original as ConfigurationServiceQueryParameterValueImportQuerySpecification ?? new ConfigurationServiceQueryParameterValueImportQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

            ConfigurationServiceQueryParameterValueImport bindItem = this.GetDataSource();

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
                    this.hdnConfigurationServiceGroupImportId.Value = bindItem.ConfigurationServiceGroupImportId.ToString();
                    this.lblConfigurationServiceGroupImportValue.Text = ElementsCPSSqlUtility.GetName("ConfigurationServiceGroupImport", bindItem.ConfigurationServiceGroupImportId);
                    this.txtQueryParameterName.Text = bindItem.QueryParameterName;
                    this.txtQueryParameterValue.Text = bindItem.QueryParameterValue;
                    this.ddlStatus.ForceSelectedValue((RowStatus.RowStatusId)bindItem.RowStatusId);
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
            ConfigurationServiceQueryParameterValueImport saveItem = this.GetDataSource();
			saveItem.RowStatusId = string.IsNullOrEmpty(this.ddlStatus.SelectedValue) ? (int)RowStatus.RowStatusId.Active : Convert.ToInt32(this.ddlStatus.SelectedValue);
			saveItem.QueryParameterName = this.txtQueryParameterName.Text.TrimToNull();
            saveItem.QueryParameterValue = this.txtQueryParameterValue.Text.TrimToNull();
            saveItem.ConfigurationServiceGroupImportId = Convert.ToInt32(this.hdnConfigurationServiceGroupImportId.Value);
            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}

        /// <summary>
        /// Delete the item being edited.
        /// </summary>
        private void Delete()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                ConfigurationServiceQueryParameterValueImport deleteItem = this.GetDataSource();
                deleteItem.Delete();

                scope.Complete(); // transaction complete
            }
        }

        protected ConfigurationServiceQueryParameterValueImport GetDataSource()
        {
            ConfigurationServiceQueryParameterValueImport saveItem;
            if (!this.IsNewRecord)
            {
                saveItem = ConfigurationServiceQueryParameterValueImport.FetchByID(this.DataSourceId);
            }
            else
            {
                saveItem = new ConfigurationServiceQueryParameterValueImport(true);
            }
            return saveItem;
        }

		protected override void OnDataSourceIdChange(EventArgs e)
		{
			base.OnDataSourceIdChange(e);
		}

		#endregion

	}
}