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
	public partial class JumpstationGroupCopyPanel : BaseQuerySpecificationEditDataUserControl
	{

		#region Constants

		private const string ViewStateKey_JumpstationGroupIds = "JumpstationGroupIds";

		#endregion

		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		public List<int> JumpstationGroupIds
		{
			get
			{
				return this.ViewState[ViewStateKey_JumpstationGroupIds] as List<int>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_JumpstationGroupIds);
				}
				else
				{
					this.ViewState[ViewStateKey_JumpstationGroupIds] = value;
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
			this.lblIdText.Text = string.Empty;
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
			this.ddlOwner.ClearSelection();
			this.txtTagsToAdd.Text = string.Empty;
			this.txtTagsToRemove.Text = string.Empty;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public JumpstationGroupQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as JumpstationGroupQuerySpecification ?? new JumpstationGroupQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			JumpstationGroupCollection bindItems = this.GetJumpstationGroups();
			try
			{
				List<int> jumpstationGroupIds = bindItems.GetIds();
				jumpstationGroupIds.Sort();
				this.lblIdText.Text = string.Join(", ", jumpstationGroupIds.ToStrings().ToArray());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}

			if (this.ConvertToExpectedType(this.QuerySpecification).IdList.Count == 0)
				bindItems.Clear();

			//only Coordinators are allowed to set or change the Configuration Service Group Owner
			this.ddlOwner.Enabled = SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator);
			this.ddlOwner.ForceSelectedValue(PersonController.GetCurrentUser().Id);
		}

		protected override void SaveInput()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			JumpstationGroupCollection originalJumpstationGroups = this.GetJumpstationGroups();

			foreach (JumpstationGroup originalItem in originalJumpstationGroups)
			{
				JumpstationGroup newItem = originalItem.SaveAsNew(true, true);

				if (!string.IsNullOrEmpty(this.txtName.Text.TrimToNull()))
				{
					newItem.Name = this.txtName.Text.TrimToNull();
				}

				if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
				{
					newItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
				}

				if (!string.IsNullOrEmpty(this.ddlOwner.SelectedValue))
				{
					newItem.OwnerId = string.IsNullOrEmpty(this.ddlOwner.SelectedValue) ? PersonController.GetCurrentUser().Id : Convert.ToInt32(this.ddlOwner.SelectedValue);
				}

				newItem.JumpstationGroupStatusId = (int)JumpstationGroupStateId.Modified;
				newItem.ValidationId = null;
				newItem.ProductionId = null;
				newItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

				cpsLogList.Add(new CPSLog(newItem.Id, string.Format(CultureInfo.CurrentCulture, "Copied Id #{0} to #{1}", originalItem.Id, newItem.Id), newItem.Name, "Copy success"));

				if (this.pnlDataControls_TagsToAdd.Enabled)
				{
					if (!string.IsNullOrEmpty(this.txtTagsToAdd.Text))
					{
						newItem.AddTags(Tag.ParseTagNameList(this.txtTagsToAdd.Text, false));
					}
				}

				if (this.pnlDataControls_TagsToRemove.Enabled)
				{
					if (!string.IsNullOrEmpty(this.txtTagsToRemove.Text))
					{
						newItem.RemoveTags(Tag.ParseTagNameList(this.txtTagsToRemove.Text, false));
					}
				}
			}
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();

			// when copied do not allow to be copied again
			this.btnSave.Enabled = false;
			this.btnCancel.Enabled = false;
		}

		private VwMapJumpstationGroupCollection GetDataSource()
		{
			JumpstationGroupQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapJumpstationGroupCollection jumpstationGroupColl = VwMapJumpstationGroupController.Fetch(query);
			return jumpstationGroupColl;
		}

		/// <summary>
		/// Gets the <see cref="JumpstationGroup"/>s corresponding to the items in the <see cref="VwMapJumpstationGroupCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private JumpstationGroupCollection GetJumpstationGroups()
		{
			return this.GetDataSource().GetJumpstationGroups();
		}

		#endregion

	}
}