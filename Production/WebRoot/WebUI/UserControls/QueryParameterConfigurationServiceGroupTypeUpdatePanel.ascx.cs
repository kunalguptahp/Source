using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Transactions;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Data.QuerySpecifications;
using Microsoft.Security.Application;
using System.Web.UI;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
    public partial class QueryParameterConfigurationServiceGroupTypeUpdatePanel : RecordDetailUserControl
	{
        List<string> selectedAppType = new List<string>();

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
//				this.UpdateChildListImmutableConditions();
			}
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

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.OnInputCancel(new EventArgs());
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            this.Delete();
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
            string windowsId = this.Page.User.Identity.Name.ToString();
            int tenantId = PersonController.GetCurrentUser().TenantGroupId;
            
            Global.BindQueryParameterListControl(this.ddlQueryParameter, RowStatus.RowStatusId.Active);
            this.ddlQueryParameter.InsertItem(0, "", Global.GetSelectListText());

            Global.BindConfigurationServiceGroupTypeListControl(this.ddlConfigurationServiceGroupType, RowStatus.RowStatusId.Active, tenantId);
            this.ddlConfigurationServiceGroupType.InsertItem(0, "", Global.GetSelectListText());
        }

        protected override void OnPreRender(EventArgs e)
        {
            Control control = null;
            string ctrlname = this.Page.Request.Params.Get("__EVENTTARGET");
            if (ctrlname != null && ctrlname != string.Empty)
            {
                control = this.Page.FindControl(ctrlname);
            }
            else
            {
                foreach (string ctl in this.Page.Request.Form)
                {
                    Control c = this.Page.FindControl(ctl);
                    if (c is System.Web.UI.WebControls.Button)
                    {
                       
                        if (selectedAppType.Count != 0)
                        {
                            this.ddlConfigurationServiceGroupType.SelectedIndex = this.ddlConfigurationServiceGroupType.Items.IndexOf(this.ddlConfigurationServiceGroupType.Items.FindByValue(selectedAppType[0]));
                        }
                        
                    }
                }
            }

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
            this.ddlConfigurationServiceGroupType.ClearSelection();
            this.ddlQueryParameter.ClearSelection();
            this.txtDescription.Text = string.Empty;
            this.chkWildcard.Checked = true;
            this.txtMaximumSelection.Text = "0";
		}

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
            QueryParameterConfigurationServiceGroupTypeQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

			this.txtName.Text = defaultValuesSpecification.Name;
        }

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
        public QueryParameterConfigurationServiceGroupTypeQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

            return original as QueryParameterConfigurationServiceGroupTypeQuerySpecification ?? new QueryParameterConfigurationServiceGroupTypeQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

            QueryParameterConfigurationServiceGroupType bindItem = this.GetDataSource();

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
					this.txtName.Text = bindItem.Name;
					this.txtDescription.Text = bindItem.Description;
                    this.ddlQueryParameter.ForceSelectedValue(bindItem.QueryParameterId);
                    this.ddlConfigurationServiceGroupType.ForceSelectedValue(bindItem.ConfigurationServiceGroupType.Name.ToString());
                    this.chkWildcard.Checked = bindItem.Wildcard;
                    this.txtMaximumSelection.Text = bindItem.MaximumSelection.ToString();
                    this.pnlEditArea.Enabled = true;
                }
				catch (SqlException ex)
				{
					LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
					throw;
				}
			}
		}
        
        protected bool IsDataModificationAllowed()
        {
            QueryParameterConfigurationServiceGroupType bindItem = this.GetDataSource();
            if (bindItem == null)
                return false;
            else
                return bindItem.IsDataModificationAllowed();
        }

		protected override void SaveInput()
		{
		    QueryParameterConfigurationServiceGroupType saveItem = this.GetDataSource();
			saveItem.Name = this.txtName.Text.TrimToNull();
			saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
            saveItem.QueryParameterId = Convert.ToInt32(this.ddlQueryParameter.SelectedValue);
            //saveItem.ConfigurationServiceGroupTypeId = Convert.ToInt32(this.ddlConfigurationServiceGroupType.SelectedValue);
            saveItem.ConfigurationServiceGroupTypeId = Convert.ToInt32(ConfigurationServiceGroupTypeController.FetchByName(this.ddlConfigurationServiceGroupType.SelectedValue).Id);
            saveItem.Wildcard = this.chkWildcard.Checked;
            saveItem.MaximumSelection =  Convert.ToInt32(this.txtMaximumSelection.Text);
            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}

        /// <summary>
        /// Delete the Query Parameter/ConfigurationServiceGroup Type being edited.
        /// </summary>
        private void Delete()
        {
            if (this.DataSourceId != null)
            {
                using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
                {
                    QueryParameterConfigurationServiceGroupType queryParameterConfigurationServiceGroupType = QueryParameterConfigurationServiceGroupType.FetchByID(this.DataSourceId);
                    queryParameterConfigurationServiceGroupType.Destroy();
                    scope.Complete(); // transaction complete

                    this.pnlEditArea.Enabled = false;
                }
            }
        }

        private QueryParameterConfigurationServiceGroupType GetDataSource()
        {
            QueryParameterConfigurationServiceGroupType saveItem;
            if (!this.IsNewRecord)
            {
                saveItem = QueryParameterConfigurationServiceGroupType.FetchByID(this.DataSourceId);
            }
            else
            {
                saveItem = new QueryParameterConfigurationServiceGroupType(true);
            }
            return saveItem;
        }

		#endregion

	}
}