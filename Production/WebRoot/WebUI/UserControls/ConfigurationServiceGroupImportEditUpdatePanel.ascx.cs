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
	public partial class ConfigurationServiceGroupImportEditUpdatePanel : BaseQuerySpecificationEditDataUserControl
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
				SecurityManager.IsCurrentUserInRole(UserRoleId.DataAdmin);
			this.btnDelete.Visible =
				SecurityManager.IsCurrentUserInRole(UserRoleId.DataAdmin);
            this.btnImport.Visible =
                SecurityManager.IsCurrentUserInRole(UserRoleId.DataAdmin);
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

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.Delete();
		}

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            this.Import();
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
			Global.BindConfigurationServiceApplicationListControl(this.ddlConfigurationServiceApplication, RowStatus.RowStatusId.Active);
			this.ddlConfigurationServiceApplication.InsertItem(0, "", Global.GetSelectListText());

			Global.BindConfigurationServiceGroupTypeListControl(this.ddlConfigurationServiceGroupType, RowStatus.RowStatusId.Active);
			this.ddlConfigurationServiceGroupType.InsertItem(0, "", Global.GetSelectListText());
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
			this.ddlConfigurationServiceApplication.ClearSelection();
			this.ddlConfigurationServiceGroupType.ClearSelection();
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
            this.txtImportStatus.Text = string.Empty;
            this.txtImportMessage.Text = string.Empty;
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

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

            ConfigurationServiceGroupImportCollection bindItems = this.GetConfigurationServiceGroupImports();
			try
			{
                List<int> configurationServiceGroupImportIds = bindItems.GetIds();
                configurationServiceGroupImportIds.Sort();
                this.lblIdText.Text = string.Join(", ", configurationServiceGroupImportIds.ToStrings().ToArray());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}
		}

		protected override void SaveInput()
		{
			this.pnlCPSLog.Visible = false;
            bool configurationServiceGroupImportModified = false;

			List<CPSLog> cpsLogList = new List<CPSLog>();
            ConfigurationServiceGroupImportCollection configurationServiceGroupImports = this.GetConfigurationServiceGroupImports();
            foreach (ConfigurationServiceGroupImport saveItem in configurationServiceGroupImports)
			{
                configurationServiceGroupImportModified = false;
				if (!string.IsNullOrEmpty(this.txtName.Text.TrimToNull()))
				{
					saveItem.Name = this.txtName.Text.TrimToNull();
                    configurationServiceGroupImportModified = true;
				}

                if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
                {
                    saveItem.Description = this.txtDescription.Text.TrimToNull();
                    configurationServiceGroupImportModified = true;
                }

				if (!string.IsNullOrEmpty(this.ddlConfigurationServiceApplication.SelectedValue))
				{
					saveItem.ConfigurationServiceApplicationName= this.ddlConfigurationServiceApplication.SelectedValue;
                    configurationServiceGroupImportModified = true;
				}

				if (!string.IsNullOrEmpty(this.ddlConfigurationServiceGroupType.SelectedValue) && (this.ddlConfigurationServiceGroupType.Enabled == true))
				{
					saveItem.ConfigurationServiceGroupTypeName = this.ddlConfigurationServiceGroupType.SelectedValue;
                    configurationServiceGroupImportModified = true;
				}

                if (!string.IsNullOrEmpty(this.txtImportStatus.Text.TrimToNull()))
                {
                    saveItem.ImportStatus = this.txtImportStatus.Text.TrimToNull();
                    configurationServiceGroupImportModified = true;
                }

                if (!string.IsNullOrEmpty(this.txtImportMessage.Text.TrimToNull()))
                {
                    saveItem.ImportMessage = this.txtImportMessage.Text.TrimToNull();
                    configurationServiceGroupImportModified = true;
                }

                if (configurationServiceGroupImportModified)
				{
					saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                    saveItem.Log(Severity.Info, "ConfigurationServiceImportGroup data saved.");
				}

				if (configurationServiceGroupImportModified)
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
		/// Delete the ConfigurationServiceGroupImport being edited.
		/// </summary>
		private void Delete()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			using (TransactionScope scope = new TransactionScope())
			{
                //get configurationServiceGroupImport to delete
                ConfigurationServiceGroupImportCollection configurationServiceGroupImports = this.GetConfigurationServiceGroupImports();

				//Apply change:
                foreach (var configurationServiceGroupImport in configurationServiceGroupImports)
				{
                    configurationServiceGroupImport.Delete();
                    cpsLogList.Add(new CPSLog(configurationServiceGroupImport.Id, configurationServiceGroupImport.Description, configurationServiceGroupImport.Name, "Success Delete"));
				}
				scope.Complete(); // transaction complete

				this.pnlEditArea.Enabled = false;
				this.txtImportStatus.Text = "Deleted";
			}

			if (cpsLogList.Count > 0)
			{
				this.pnlCPSLog.Visible = true;
				this.ucCPSLog.CPSLogList = cpsLogList;
				this.ucCPSLog.DataBind();
			}
		}

        /// <summary>
        /// Import the ConfigurationServiceGroupImport being edited.
        /// </summary>
        private void Import()
        {
            List<CPSLog> cpsLogList = new List<CPSLog>();
            using (TransactionScope scope = new TransactionScope())
            {
                //get configurationServiceGroupImport to import
                ConfigurationServiceGroupImportCollection configurationServiceGroupImports = this.GetConfigurationServiceGroupImports();

                //import
                foreach (var configurationServiceGroupImport in configurationServiceGroupImports)
                {
                    if (configurationServiceGroupImport.ImportStatus == "Add")
                    {
                        VwMapConfigurationServiceApplication configurationServiceApplication = VwMapConfigurationServiceApplicationController.FetchByElementsKey(configurationServiceGroupImport.ConfigurationServiceApplicationName);
                        if (configurationServiceApplication == null)
                        {
                            cpsLogList.Add(new CPSLog(configurationServiceGroupImport.Id, configurationServiceGroupImport.Description, configurationServiceGroupImport.Name, "Failed Import - Invalid Application Name"));
                            continue;
                        }

                        VwMapConfigurationServiceGroupType configurationServiceGroupType = VwMapConfigurationServiceGroupTypeController.FetchByElementsKeyGroupTypeName(configurationServiceGroupImport.ConfigurationServiceApplicationName, configurationServiceGroupImport.ConfigurationServiceGroupTypeName);
                        if (configurationServiceGroupType == null)
                        {
                            cpsLogList.Add(new CPSLog(configurationServiceGroupImport.Id, configurationServiceGroupImport.Description, configurationServiceGroupImport.Name, "Failed Import - Invalid Group Type Name"));
                            continue;
                        }

                        configurationServiceGroupImport.Import();
                        cpsLogList.Add(new CPSLog(configurationServiceGroupImport.Id, configurationServiceGroupImport.Description, configurationServiceGroupImport.Name, "Success Import"));
                    }
                    else
                    {
                        cpsLogList.Add(new CPSLog(configurationServiceGroupImport.Id, configurationServiceGroupImport.Description, configurationServiceGroupImport.Name, "Failed Import - Import status must be 'Add'"));
                    }
                }
                scope.Complete(); // transaction complete

                this.pnlEditArea.Enabled = false;
                this.txtImportStatus.Text = "Imported";
            }

            if (cpsLogList.Count > 0)
            {
                this.pnlCPSLog.Visible = true;
                this.ucCPSLog.CPSLogList = cpsLogList;
                this.ucCPSLog.DataBind();
            }
        }

        private VwMapConfigurationServiceGroupImportCollection GetDataSource()
		{
            ConfigurationServiceGroupImportQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

            VwMapConfigurationServiceGroupImportCollection configurationServiceGroupImportCollection = VwMapConfigurationServiceGroupImportController.Fetch(query);
            if (configurationServiceGroupImportCollection.Count < 1)
			{
				if (this.pnlEditArea.Enabled)
				{
					this.SetErrorMessage("Invalid data. No data matches the specified search criteria.");
					this.pnlEditArea.Enabled = false;
				}
			}
            return configurationServiceGroupImportCollection;
		}

		/// <summary>
        /// Gets the <see cref="ConfigurationServiceGroupImport"/>s corresponding to the items in the <see cref="VwMapConfigurationServiceGroupCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
        private ConfigurationServiceGroupImportCollection GetConfigurationServiceGroupImports()
		{
            return this.GetDataSource().GetConfigurationServiceGroupImports();
		}

		#endregion

	}
}