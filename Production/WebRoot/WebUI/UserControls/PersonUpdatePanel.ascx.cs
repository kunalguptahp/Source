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
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;


namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class PersonUpdatePanel : RecordDetailUserControl
	{
		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		/// <summary>
		/// A strongly-typed shadow of the underlying property.
		/// </summary>
		protected new VwMapPerson DataItem
		{
			get
			{
				return base.DataItem as VwMapPerson;
			}
		}

		#endregion

		#region PageEvents

		//protected void Page_Load(object sender, EventArgs e)
		//{
		//    if (!Page.IsPostBack)
		//    {
		//    }
		//}

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
		protected void btnSaveAndDone_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.OnInputSave(new EventArgs());

			this.Response.Redirect(Global.GetPersonDetailPageUri(this.DataItem.Id, null));
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
            Global.BindTenantListControl(this.ddlTenantGroup, RowStatus.RowStatusId.Active);
            this.ddlTenantGroup.InsertItem(0, "", Global.GetSelectListText());

			Global.BindRoleList2Control(this.chkRoleList,this.chkApplicationList1,this.chkApplicationList2, this.chkApplicationList3,this.chkApplicationList4, RowStatus.RowStatusId.Active, true);

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
			this.lblNameText.Text = string.Empty;
			this.txtWindowsId.Text = string.Empty;
			this.txtFirstName.Text = string.Empty;
			this.txtMiddleName.Text = string.Empty;
			this.txtLastName.Text = string.Empty;
			this.txtEmail.Text = string.Empty;
			this.chkRoleList.ClearSelection();
			this.ddlStatus.ClearSelection();
		}

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			PersonQuerySpecification defaultValuesSpecification = this.ConvertToExpectedType(this.DefaultValuesSpecification);

			this.txtLastName.Text = defaultValuesSpecification.LastName;
			this.txtFirstName.Text = defaultValuesSpecification.FirstName;
			this.txtEmail.Text = defaultValuesSpecification.Email;
			this.txtWindowsId.Text = defaultValuesSpecification.WindowsId;

			if (defaultValuesSpecification.RowStateId != null) { this.ddlStatus.ForceSelectedValue(defaultValuesSpecification.RowStateId); }
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public PersonQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as PersonQuerySpecification ?? new PersonQuerySpecification(original);
		}

		protected override object LoadDataItem()
		{
			VwMapPerson dataItem = null;
			if (this.DataSourceId != null)
			{
				VwMapPersonCollection bindItemCollection = VwMapPersonController.FetchByID(this.DataSourceId);
				dataItem = (bindItemCollection.Count == 0) ? null : bindItemCollection[0];
			}
			return dataItem ?? new VwMapPerson();
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
					Person bindItem = Person.FetchByID(this.DataSourceId);
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
					this.txtWindowsId.Text = bindItem.WindowsId;
					this.lblNameText.Text = bindItem.Name;
					this.txtFirstName.Text = bindItem.FirstName;
					this.txtMiddleName.Text = bindItem.MiddleName;
					this.txtLastName.Text = bindItem.LastName;
					this.txtEmail.Text = bindItem.Email;

					this.chkRoleList.ClearSelection();
                   
                    this.chkApplicationList1.ClearSelection();
                    this.chkApplicationList2.ClearSelection();
                    this.chkApplicationList3.ClearSelection();
                    this.chkApplicationList4.ClearSelection();
                    RoleCollection userRoles = bindItem.GetRoleCollection();
                    RoleCollection userRoleDomain = new RoleCollection();
                    //RoleCollection userRoleAction = new RoleCollection();
                    ApplicationCollection appcol1 =new ApplicationCollection();
                    ApplicationCollection appcol2 = new ApplicationCollection(); 
                    ApplicationCollection appcol3 = new ApplicationCollection(); 
                    ApplicationCollection appcol4 = new ApplicationCollection();
                    int personId = bindItem.Id;
                    foreach (Role userRole in userRoles)
                   {//|| userRole.UserRoleId == UserRoleId.Editor || userRole.UserRoleId == UserRoleId.Validator || userRole.UserRoleId == UserRoleId.DataAdmin
                       if (userRole.UserRoleId == UserRoleId.Viewer )
                       {
                           //userRoleAction.Add(userRole);
                            //appcol1 = Role.GetApplicationCollection((int)UserRoleId.Viewer);
                           appcol1 = ApplicationRoleController.GetCurrentAppColByRole(personId, UserRoleId.Viewer);
                       }
                       else if (userRole.UserRoleId == UserRoleId.Editor)
                       {
                           appcol2 = ApplicationRoleController.GetCurrentAppColByRole(personId, UserRoleId.Editor);
                       }
                       else if (userRole.UserRoleId == UserRoleId.Validator)
                       {
                           appcol3 = ApplicationRoleController.GetCurrentAppColByRole(personId, UserRoleId.Validator);
                       }
                       else if (userRole.UserRoleId == UserRoleId.DataAdmin)
                       {
                           appcol4 = ApplicationRoleController.GetCurrentAppColByRole(personId, UserRoleId.DataAdmin);
                       }
                       else
                       {
                           userRoleDomain.Add(userRole);
                       }
                        
                   }

                    Global.SetRoleListControl(this.chkRoleList, userRoleDomain);
                   // Global.SetRoleListControl(this.chkRoleActionList, userRoleAction);
                    Global.SetApplicationListControl(this.chkApplicationList1, appcol1);
                    Global.SetApplicationListControl(this.chkApplicationList2, appcol2);
                    Global.SetApplicationListControl(this.chkApplicationList3, appcol3);
                    Global.SetApplicationListControl(this.chkApplicationList4, appcol4);

                    this.ddlTenantGroup.ForceSelectedValue(bindItem.TenantGroupId);
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
			Person saveItem;
            
			if (this.DataSourceId != null)
			{
				saveItem = Person.FetchByID(this.DataSourceId);
			}
			else
			{
				saveItem = new Person(true);
			}
           
            
			saveItem.WindowsId = this.txtWindowsId.Text.TrimToNull();
			saveItem.RowStatusId = string.IsNullOrEmpty(this.ddlStatus.SelectedValue) ? (int)RowStatus.RowStatusId.Active : Convert.ToInt32(this.ddlStatus.SelectedValue);
            saveItem.TenantGroupId =  Convert.ToInt32(this.ddlTenantGroup.SelectedValue);
			saveItem.FirstName = this.txtFirstName.Text.TrimToNull();
			saveItem.MiddleName = this.txtMiddleName.Text.TrimToNull();
			saveItem.LastName = this.txtLastName.Text.TrimToNull();
			saveItem.Email = this.txtEmail.Text.TrimToNull();
			saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

			// save roles

			//create a list of the selected roles
			List<UserRoleId> newRoles = new List<UserRoleId>();
            List<RoleApplicationId> newApplications1 = new List<RoleApplicationId>();
            List<RoleApplicationId> newApplications2 = new List<RoleApplicationId>();
            List<RoleApplicationId> newApplications3 = new List<RoleApplicationId>();
            List<RoleApplicationId> newApplications4 = new List<RoleApplicationId>();
			foreach (ListItem item in this.chkRoleList.Items)
			{
				if (item.Selected)
				{
					int roleId = Convert.ToInt32(item.Value);
					newRoles.Add((UserRoleId)roleId);
				}
			}

            foreach (ListItem item in this.chkApplicationList1.Items)
            {
                if (item.Selected)
                {
                    int applicationId = Convert.ToInt32(item.Value);
                    newApplications1.Add((RoleApplicationId)applicationId);
                    newRoles.Add(UserRoleId.Viewer);
                }
            }

            foreach (ListItem item in this.chkApplicationList2.Items)
            {
                if (item.Selected)
                {
                    int applicationId = Convert.ToInt32(item.Value);
                    newApplications2.Add((RoleApplicationId)applicationId);
                    newRoles.Add(UserRoleId.Editor);
                }
            }
            foreach (ListItem item in this.chkApplicationList3.Items)
            {
                if (item.Selected)
                {
                    int applicationId = Convert.ToInt32(item.Value);
                    newApplications3.Add((RoleApplicationId)applicationId);
                    newRoles.Add(UserRoleId.Validator);
                }
            }
            foreach (ListItem item in this.chkApplicationList4.Items)
            {
                if (item.Selected)
                {
                    int applicationId = Convert.ToInt32(item.Value);
                    newApplications4.Add((RoleApplicationId)applicationId);
                    newRoles.Add(UserRoleId.DataAdmin);
                }
            }

			saveItem.SetRoles(newRoles);
            saveItem.SetApplications(saveItem.Id, UserRoleId.Viewer, newApplications1);
            saveItem.SetApplications(saveItem.Id, UserRoleId.Editor, newApplications2);
            saveItem.SetApplications(saveItem.Id, UserRoleId.Validator, newApplications3);
            saveItem.SetApplications(saveItem.Id, UserRoleId.DataAdmin, newApplications4);
			//reload the control using the record's (possibly newly assigned) ID
			this.DataSourceId = saveItem.Id;
		}

		#endregion
	}
}