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
	public partial class JumpstationGroupReportPanel : BaseQuerySpecificationEditDataUserControl
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

			JumpstationGroupCollection bindItems = this.GetJumpstationGroups();
			try
			{
				List<int> jumpstationGroupIds = bindItems.GetIds();
				jumpstationGroupIds.Sort();
				this.lblIdText.Text = string.Join(", ", jumpstationGroupIds.ToStrings().ToArray());

				this.repJumpstationGroup.DataSource = bindItems;
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