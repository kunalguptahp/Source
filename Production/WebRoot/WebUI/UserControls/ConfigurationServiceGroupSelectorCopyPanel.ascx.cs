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
	public partial class ConfigurationServiceGroupSelectorCopyPanel : BaseQuerySpecificationEditDataUserControl
	{
		#region Constants

		private const string ViewStateKey_ConfigurationServiceGroupSelectorIds = "ConfigurationServiceGroupSelectorIds";

		#endregion

		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		public List<int> ConfigurationServiceGroupSelectorIds
		{
			get
			{
				return this.ViewState[ViewStateKey_ConfigurationServiceGroupSelectorIds] as List<int>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_ConfigurationServiceGroupSelectorIds);
				}
				else
				{
					this.ViewState[ViewStateKey_ConfigurationServiceGroupSelectorIds] = value;
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
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.lblIdText.Text = string.Empty;
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public ConfigurationServiceGroupSelectorQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as ConfigurationServiceGroupSelectorQuerySpecification ?? new ConfigurationServiceGroupSelectorQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			//NOTE: Need to DataBind all of the controls which use DataBinding expressions in the ASCX here
			this.pnlEditArea.DataBind();
			this.PopulateListControls();

			ConfigurationServiceGroupSelectorCollection bindItems = this.GetConfigurationServiceGroupSelectors();
			try
			{
				List<int> ConfigurationServiceGroupSelectorIds = bindItems.GetIds();
				ConfigurationServiceGroupSelectorIds.Sort();
				this.lblIdText.Text = string.Join(", ", ConfigurationServiceGroupSelectorIds.ToStrings().ToArray());
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}

			if (this.ConvertToExpectedType(this.QuerySpecification).IdList.Count == 0)
				bindItems.Clear();
		}

		protected override void SaveInput()
		{
			List<CPSLog> cpsLogList = new List<CPSLog>();
			ConfigurationServiceGroupSelectorCollection originalConfigurationServiceGroupSelectors = this.GetConfigurationServiceGroupSelectors();

			foreach (ConfigurationServiceGroupSelector originalItem in originalConfigurationServiceGroupSelectors)
			{
				ConfigurationServiceGroupSelector newItem = originalItem.SaveAsNew();

				if (!string.IsNullOrEmpty(this.txtName.Text.TrimToNull()))
				{
					newItem.Name = this.txtName.Text.TrimToNull();
				}

				if (!string.IsNullOrEmpty(this.txtDescription.Text.TrimToNull()))
				{
					newItem.Description = (string.IsNullOrEmpty(this.txtDescription.Text) || this.txtDescription.Text.TrimToNull().Length < 512 ? this.txtDescription.Text.TrimToNull() : this.txtDescription.Text.TrimToNull().Substring(0, 512));
				}

				newItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

				cpsLogList.Add(new CPSLog(newItem.Id, string.Format(CultureInfo.CurrentCulture, "Copied Id #{0} to #{1}", originalItem.Id, newItem.Id), newItem.Name, "Copy success"));
			}
			this.ucCPSLog.CPSLogList = cpsLogList;
			this.ucCPSLog.DataBind();

			// when copied do not allow to be copied again
			this.btnSave.Enabled = false;
			this.btnCancel.Enabled = false;
		}

		private VwMapConfigurationServiceGroupSelectorCollection GetDataSource()
		{
			ConfigurationServiceGroupSelectorQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapConfigurationServiceGroupSelectorCollection configurationServiceGroupColl = VwMapConfigurationServiceGroupSelectorController.Fetch(query);
			return configurationServiceGroupColl;
		}

		/// <summary>
		/// Gets the <see cref="ConfigurationServiceGroupSelector"/>s corresponding to the items in the <see cref="VwMapConfigurationServiceGroupSelectorCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private ConfigurationServiceGroupSelectorCollection GetConfigurationServiceGroupSelectors()
		{
			return this.GetDataSource().GetConfigurationServiceGroupSelectors();
		}

		#endregion

	}
}