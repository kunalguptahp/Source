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
using Microsoft.Security.Application;
using System.Web.UI;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class JumpstationGroupTypeUpdatePanel : RecordDetailUserControl
	{
        List<string> selectedApp = new List<string>();

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
            bool isnew =this.IsNewRecord;
            Global.BindJumpstationApplicationListControl(this.ddlJumpstationApplication, RowStatus.RowStatusId.Active, tenantId);
            this.ddlJumpstationApplication.InsertItem(0, "", Global.GetSelectListText());

            Global.BindJumpstationDomainListControl(this.ddlValidationJumpstationDomain, RowStatus.RowStatusId.Active);
            this.ddlValidationJumpstationDomain.InsertItem(0, "", Global.GetSelectListText());

            Global.BindJumpstationDomainListControl(this.ddlPublicationJumpstationDomain, RowStatus.RowStatusId.Active);
            this.ddlPublicationJumpstationDomain.InsertItem(0, "", Global.GetSelectListText());

            Global.BindRowStatusListControl(this.ddlStatus);
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
                        
                        if (selectedApp.Count != 0)
                        {
                            this.ddlJumpstationApplication.SelectedIndex = this.ddlJumpstationApplication.Items.IndexOf(this.ddlJumpstationApplication.Items.FindByValue(selectedApp[0]));
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
            this.txtDescription.Text = string.Empty;
            this.ddlJumpstationApplication.ClearSelection();
            this.ddlValidationJumpstationDomain.ClearSelection();
            this.ddlPublicationJumpstationDomain.ClearSelection();
            this.ddlStatus.ClearSelection();
		}
		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			JumpstationGroupTypeQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

			this.txtName.Text = defaultValuesSpecification.Name;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public JumpstationGroupTypeQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as JumpstationGroupTypeQuerySpecification ?? new JumpstationGroupTypeQuerySpecification(original);
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
					JumpstationGroupType bindItem = JumpstationGroupType.FetchByID(this.DataSourceId);
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
                    this.ddlJumpstationApplication.ForceSelectedValue(bindItem.JumpstationApplication.Name.ToString());
                    this.ddlValidationJumpstationDomain.ForceSelectedValue(bindItem.ValidationJumpstationDomainId);
                    this.ddlPublicationJumpstationDomain.ForceSelectedValue(bindItem.PublicationJumpstationDomainId);
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
			JumpstationGroupType saveItem;
			if (this.DataSourceId != null)
			{
				saveItem = JumpstationGroupType.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new JumpstationGroupType(true);
			}


			saveItem.Name = this.txtName.Text.TrimToNull();
			saveItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
            saveItem.JumpstationApplicationId = Convert.ToInt32(JumpstationApplicationController.FetchByName(this.ddlJumpstationApplication.SelectedValue).Id);
            saveItem.ValidationJumpstationDomainId = Convert.ToInt32(this.ddlValidationJumpstationDomain.SelectedValue);
            saveItem.PublicationJumpstationDomainId = Convert.ToInt32(this.ddlPublicationJumpstationDomain.SelectedValue);
            saveItem.RowStatusId = string.IsNullOrEmpty(this.ddlStatus.SelectedValue) ? (int)RowStatus.RowStatusId.Active : Convert.ToInt32(this.ddlStatus.SelectedValue);
			saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}

		#endregion

	}
}