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
	public partial class WorkflowMultiReplaceUpdatePanel : BaseQuerySpecificationEditDataUserControl
	{
		#region Constants

		private const string ViewStateKey_WorkflowIds = "WorkflowIds";

		#endregion

		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		public List<int> WorkflowIds
		{
			get
			{
				return this.ViewState[ViewStateKey_WorkflowIds] as List<int>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_WorkflowIds);
				}
				else
				{
					this.ViewState[ViewStateKey_WorkflowIds] = value;
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

		protected void btnEditWorkflow_Click(object sender, EventArgs e)
		{
			if (WorkflowIds.Count > 1)
			{
				WorkflowQuerySpecification WorkflowQuerySpecification = new WorkflowQuerySpecification() { IdList = WorkflowIds };
				this.Response.Redirect(Global.GetWorkflowEditUpdatePageUri(WorkflowQuerySpecification));				
			}
			else
			{
				this.Response.Redirect(Global.GetWorkflowUpdatePageUri(WorkflowIds[0], null));
			}
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
		public WorkflowQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as WorkflowQuerySpecification ?? new WorkflowQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			WorkflowCollection bindItems = this.GetWorkflows();
			if (this.ConvertToExpectedType(this.QuerySpecification).IdList.Count == 0)
				bindItems.Clear();

			//only Coordinators are allowed to set or change the Workflow Owner
			this.ddlOwner.Enabled = SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator);
			this.ddlOwner.ForceSelectedValue(PersonController.GetCurrentUser().Id);
		}

		protected override void SaveInput()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			WorkflowCollection originalWorkflows = ucWorkflowDescriptionMultiReplaceUpdate.GetWorkflows();
			List<int> newWorkflowId = new List<int>();

			foreach (Workflow originalItem in originalWorkflows)
			{
				// check to see if Workflow already has a replacement.
				WorkflowQuerySpecification WorkflowQuerySpecification = new WorkflowQuerySpecification() { ValidationId = originalItem.ValidationId };
				if (WorkflowController.FetchCount(WorkflowQuerySpecification) > 1)
				{
					cpsLogList.Add(new CPSLog(originalItem.Id, string.Format(CultureInfo.CurrentCulture, "Id #{0} already has a replacement (Validation Id #{1} and Publication Id #{2})", originalItem.Id, originalItem.ValidationId, originalItem.ProductionId), originalItem.Name, "Failed replace"));
				}
				else
				{
					Workflow saveItem = originalItem.SaveAsNew(true, true);
                    saveItem.Name = this.ucWorkflowDescriptionMultiReplaceUpdate.GetName(originalItem);            
					saveItem.Description = this.ucWorkflowDescriptionMultiReplaceUpdate.GetDescription(originalItem);
					saveItem.OwnerId = ddlOwner.SelectedValue.TryParseInt32() ?? 0;
					saveItem.WorkflowStatusId = (int)WorkflowStateId.Modified;
					saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
					newWorkflowId.Add(saveItem.Id);

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
			if (newWorkflowId.Count != 0)
			{
				this.btnEditWorkflow.Enabled = true;
				this.WorkflowIds = newWorkflowId;
			}

			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();

			// when replaced do not allow to be replaced again
			this.btnSave.Enabled = false;
			this.btnCancel.Enabled = false;
		}

		private VwMapWorkflowCollection GetDataSource()
		{
			WorkflowQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapWorkflowCollection WorkflowCollection = VwMapWorkflowController.Fetch(query);
			return WorkflowCollection;
		}

		/// <summary>
		/// Gets the <see cref="Workflow"/>s corresponding to the items in the <see cref="VwMapWorkflowCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private WorkflowCollection GetWorkflows()
		{
			return this.GetDataSource().GetWorkflows();
		}

		#endregion

	}
}