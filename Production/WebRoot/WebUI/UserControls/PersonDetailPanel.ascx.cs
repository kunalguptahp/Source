using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Data.Query;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class PersonDetailPanel : RecordDetailUserControl
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

		#region ControlEvents

		protected void OnUpdateChildListVisibility(object sender, EventArgs e)
		{
			this.UpdateChildListVisibility();
		}

		protected void chkAllChildListToggle_CheckedChanged(object sender, EventArgs e)
		{
			this.DisplayAllChildLists(this.chkAllChildListToggle.Checked);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Re-populates the ListItems of all this control's ListControls.
		/// </summary>
		/// <remarks>
		/// This method should be called every time the control is Un-bound/Re-bound so that any changes made for a previous DataSource don't persist.
		/// </remarks>
		private void PopulateListControls()
		{
			//Global.BindRoleListControl(this.chkRoleList, RowStatus.RowStatusId.Active, true);
            Global.BindRoleList2Control(this.chkRoleList, this.chkApplicationList1, this.chkApplicationList2, this.chkApplicationList3, this.chkApplicationList4, RowStatus.RowStatusId.Active, true);
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.chkRoleList.ClearSelection();
			//this.chkTeamList.ClearSelection();
            
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

		protected override void UnbindItem()
		{
			base.UnbindItem();
			this.PopulateListControls();
			this.ClearDataControls();
			this.UpdateChildListVisibility();
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			try
			{
				VwMapPerson bindItem = this.DataItem;
				if (bindItem.IsNew)
				{
					return;
				}

				this.chkRoleList.ClearSelection();
                this.chkApplicationList1.ClearSelection();
                this.chkApplicationList2.ClearSelection();
                this.chkApplicationList3.ClearSelection();
                this.chkApplicationList4.ClearSelection();
				Person bindItemTable = Person.FetchByID(this.DataSourceId);

                RoleCollection userRoles = bindItemTable.GetRoleCollection();
                RoleCollection userRoleDomain = new RoleCollection();
                ApplicationCollection appcol1 = new ApplicationCollection();
                ApplicationCollection appcol2 = new ApplicationCollection();
                ApplicationCollection appcol3 = new ApplicationCollection();
                ApplicationCollection appcol4 = new ApplicationCollection();
                int personId = bindItemTable.Id; 
                foreach (Role userRole in userRoles)
                {
                    if (userRole.UserRoleId == UserRoleId.Viewer)
                    {
                        //userRoleAction.Add(userRole);
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

				//Global.SetRoleListControl(this.chkRoleList, bindItemTable.GetRoleCollection());
                //this.ddlTenant.SelectedValue=bindItem.TenantGroupName.ToString();
                //this.Tenant.Text =bindItemTable.TenantToTenantGroupId.ToString();
				//Update child grid toggle buttons
				//this.chkNoteChildListToggle.Enabled = (bindItem.NoteCount > 0);
				//this.UpdateChildListVisibility();
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}
		}

		protected override void SaveInput()
		{
			throw new NotSupportedException();
		}

		private void UpdateChildListVisibility()
		{
			//this.pnlNoteList.Visible = (this.DataSourceId != null) && this.chkNoteChildListToggle.Checked;
		}

		private void DisplayAllChildLists(bool showAll)
		{
			//Update all child grid toggle buttons
			//this.chkNoteChildListToggle.Checked = showAll && this.chkNoteChildListToggle.Enabled;

			this.UpdateChildListVisibility();
		}

		#endregion
	}
}