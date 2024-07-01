using System;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;
using System.Web.UI.WebControls;
using System.Linq;
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
	public partial class WorkflowModuleMultiReplaceUpdatePanel : BaseQuerySpecificationEditDataUserControl
	{
		#region Constants

		private const string ViewStateKey_WorkflowModuleIds = "WorkflowModuleIds";

		#endregion

		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		public List<int> WorkflowModuleIds
		{
			get
			{
				return this.ViewState[ViewStateKey_WorkflowModuleIds] as List<int>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_WorkflowModuleIds);
				}
				else
				{
					this.ViewState[ViewStateKey_WorkflowModuleIds] = value;
				}
			}
		}

		#endregion

		#region PageEvents

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

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
			this.btnSave.Visible = SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
			this.btnCancel.Visible = SecurityManager.IsCurrentUserInRole(UserRoleId.Editor);
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

		protected void cvTxtTagsToAddValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
		}

		protected void cvTxtTagsToRemoveValidateTagNames_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Tag.AreValidNames(Tag.ParseTagNameList(args.Value, false));
		}

		protected void cvTxtTagsToAddMaxTagCount_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (Tag.ParseTagNameList(args.Value, false).Count() <= 100);
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
			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetSelectListText());
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.ddlOwner.ClearSelection();
			this.txtTagsToAdd.Text = string.Empty;
            this.txtTagsToRemove.Text = string.Empty;
        }

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public WorkflowModuleQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as WorkflowModuleQuerySpecification ?? new WorkflowModuleQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			WorkflowModuleCollection bindItems = this.GetWorkflowModules();
			if (this.ConvertToExpectedType(this.QuerySpecification).IdList.Count == 0)
				bindItems.Clear();

			//only Coordinators are allowed to set or change the Workflow Module Owner
			this.ddlOwner.Enabled = SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator);
			this.ddlOwner.ForceSelectedValue(PersonController.GetCurrentUser().Id);
		}

		protected override void SaveInput()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			WorkflowModuleCollection originalWorkflowModules = ucWorkflowModuleDescriptionMultiReplaceUpdate.GetWorkflowModules();
			List<int> newWorkflowModuleId = new List<int>();

			foreach (WorkflowModule originalItem in originalWorkflowModules)
			{
				// check to see if Workflow Module already has a replacement.
				WorkflowModuleQuerySpecification workflowModuleQuerySpecification = new WorkflowModuleQuerySpecification() { ValidationId = originalItem.ValidationId };
				if (WorkflowModuleController.FetchCount(workflowModuleQuerySpecification) > 1)
				{
					cpsLogList.Add(new CPSLog(originalItem.Id, string.Format(CultureInfo.CurrentCulture, "Id #{0} already has a replacement (Validation Id #{1} and Publication Id #{2})", originalItem.Id, originalItem.ValidationId, originalItem.ProductionId), originalItem.Name, "Failed replace"));
				}
				else
				{
					WorkflowModule saveItem = originalItem.SaveAsNew(true, true);
                    saveItem.Name = this.ucWorkflowModuleDescriptionMultiReplaceUpdate.GetName(originalItem);            
					saveItem.Description = this.ucWorkflowModuleDescriptionMultiReplaceUpdate.GetDescription(originalItem);
					saveItem.OwnerId = ddlOwner.SelectedValue.TryParseInt32() ?? 0;
					saveItem.WorkflowModuleStatusId = (int)WorkflowStateId.Modified;
					saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
					newWorkflowModuleId.Add(saveItem.Id);

					cpsLogList.Add(new CPSLog(saveItem.Id, string.Format(CultureInfo.CurrentCulture, "Copied Id #{0} to #{1}", originalItem.Id, saveItem.Id), saveItem.Name, "Success replace"));

					if (this.pnlDataControls_TagsToAdd.Enabled)
					{
						if (!string.IsNullOrEmpty(this.txtTagsToAdd.Text))
						{
							saveItem.AddTags(Tag.ParseTagNameList(this.txtTagsToAdd.Text, false));
						}
					}

                    if (this.pnlDataControls_TagsToRemove.Enabled)
                    {
                        if (!string.IsNullOrEmpty(this.txtTagsToRemove.Text))
                        {
                            saveItem.RemoveTags(Tag.ParseTagNameList(this.txtTagsToRemove.Text, false));
                        }
                    }
                }
			}
			if (newWorkflowModuleId.Count != 0)
			{
				this.WorkflowModuleIds = newWorkflowModuleId;
			}

			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();

			// when replaced do not allow to be replaced again
			this.btnSave.Enabled = false;
			this.btnCancel.Enabled = false;
		}

		private VwMapWorkflowModuleCollection GetDataSource()
		{
			WorkflowModuleQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapWorkflowModuleCollection WorkflowModuleCollection = VwMapWorkflowModuleController.Fetch(query);
			return WorkflowModuleCollection;
		}

		/// <summary>
		/// Gets the <see cref="WorkflowModule"/>s corresponding to the items in the <see cref="VwMapWorkflowModuleCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private WorkflowModuleCollection GetWorkflowModules()
		{
			return this.GetDataSource().GetWorkflowModules();
		}

		#endregion

	}
}