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
	public partial class ConfigurationServiceGroupReportPanel : BaseQuerySpecificationEditDataUserControl
	{

		#region Constants

		#endregion

		#region Properties

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		#endregion

		#region PageEvents

		#endregion

		#region Methods

		protected override void UnbindItem()
		{
			base.UnbindItem();
			this.ClearDataControls();
		}

		/// <summary>
		/// Clears/resets the value or selection of all of this control's DataBound controls that are associated with the "current" DataSource.
		/// </summary>
		private void ClearDataControls()
		{
			this.lblIdText.Text = string.Empty;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public ConfigurationServiceGroupQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as ConfigurationServiceGroupQuerySpecification ?? new ConfigurationServiceGroupQuerySpecification(original);
		}

		protected override void BindItem()
		{
			this.UnbindItem();

			ConfigurationServiceGroupCollection bindItems = this.GetConfigurationServiceGroups();
			try
			{
				List<int> configurationServiceGroupIds = bindItems.GetIds();
				configurationServiceGroupIds.Sort();
				this.lblIdText.Text = string.Join(", ", configurationServiceGroupIds.ToStrings().ToArray());

				this.repConfigurationServiceGroup.DataSource = bindItems;
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
			throw new NotSupportedException("Report does not support saving input.");
		}

		private VwMapConfigurationServiceGroupCollection GetDataSource()
		{
			ConfigurationServiceGroupQuerySpecification query = this.ConvertToExpectedType(this.QuerySpecification);

			VwMapConfigurationServiceGroupCollection configurationServiceGroupColl = VwMapConfigurationServiceGroupController.Fetch(query);
			return configurationServiceGroupColl;
		}

		/// <summary>
		/// Gets the <see cref="ConfigurationServiceGroup"/>s corresponding to the items in the <see cref="VwMapConfigurationServiceGroupCollection"/> returned by <see cref="GetDataSource"/>.
		/// </summary>
		/// <returns></returns>
		private ConfigurationServiceGroupCollection GetConfigurationServiceGroups()
		{
			return this.GetDataSource().GetConfigurationServiceGroups();
		}

		#endregion

	}
}