using System;
using System.Globalization;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the JumpstationGroupSelector table.
	/// </summary>
	public partial class JumpstationGroupSelector
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by JumpstationGroupId
		/// </summary>
		public static void DestroyByJumpstationGroupId(int jumpstationGroupId)
		{
			// Destroy GroupSelectorQueryParameterValues first
			JumpstationGroupSelectorQuerySpecification jumpstationGroupSelectorQuerySpecification = 
				new JumpstationGroupSelectorQuerySpecification() { JumpstationGroupId = jumpstationGroupId};
			JumpstationGroupSelectorCollection jumpstationGroupSelectorColl =
				JumpstationGroupSelectorController.Fetch(jumpstationGroupSelectorQuerySpecification);
			foreach (JumpstationGroupSelector jumpstationGroupSelector in jumpstationGroupSelectorColl)
			{
				JumpstationGroupSelectorQueryParameterValue.DestroyByJumpstationGroupSelectorId(jumpstationGroupSelector.Id);
			}
			
			// Destroy group selectors
			Query query = JumpstationGroupSelector.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(JumpstationGroupIdColumn.ColumnName, jumpstationGroupId);
			JumpstationGroupSelectorController.DestroyByQuery(query);
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? JumpstationGroupSelectorId = (this.IsNew ? null : (int?)this.Id);
			Log(JumpstationGroupSelectorId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? JumpstationGroupSelectorId = (this.IsNew ? null : (int?)this.Id);
			Log(JumpstationGroupSelectorId, severity, this, message, ex);
		}

		internal static void Log(int? JumpstationGroupSelectorId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "JumpstationGroupSelector History: #{0}: {1}.", JumpstationGroupSelectorId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? JumpstationGroupSelectorId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "JumpstationGroupSelector History: #{0}: {1}.", JumpstationGroupSelectorId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public JumpstationGroupSelector SaveAsNew()
		{
			return SaveAsNew(this);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="JumpstationGroup"/>.
		/// </summary>
		/// <param name="originalJumpstationGroupSelector">The <see cref="JumpstationGroup"/> to copy/duplicate.</param>
		private static JumpstationGroupSelector SaveAsNew(JumpstationGroupSelector originalJumpstationGroupSelector)
		{
			JumpstationGroupSelector newJumpstationGroupSelector;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newJumpstationGroupSelector = JumpstationGroupSelector.Copy(originalJumpstationGroupSelector);

				//mark the newJumpstationGroupSelector instance as an unsaved instance
				newJumpstationGroupSelector.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newJumpstationGroupSelector.CreatedBy = createdBy;
				newJumpstationGroupSelector.CreatedOn = createdOn;
				newJumpstationGroupSelector.ModifiedBy = createdBy;
				newJumpstationGroupSelector.ModifiedOn = createdOn;
				newJumpstationGroupSelector.RowStatusId = (int)RowStatus.RowStatusId.Active;

				//save the newJumpstationGroup to the DB so that it is assigned an ID
				newJumpstationGroupSelector.Save(SecurityManager.CurrentUserIdentityName);

				//copy all of the originalJumpstationGroupSelector's JumpstationGroupSelectorQueryParameterValue data to the newJumpstationGroupSelector
				JumpstationGroupSelectorQueryParameterValueCollection colJumpstationGroupSelectorQueryParameterValue = new JumpstationGroupSelectorQueryParameterValueCollection();
				foreach (JumpstationGroupSelectorQueryParameterValue qpvOriginalRecord in originalJumpstationGroupSelector.JumpstationGroupSelectorQueryParameterValueRecords())
				{
					JumpstationGroupSelectorQueryParameterValue qpvNewRecord = JumpstationGroupSelectorQueryParameterValue.Copy(qpvOriginalRecord);
					qpvNewRecord.MarkNew();
					qpvNewRecord.JumpstationGroupSelectorId = newJumpstationGroupSelector.Id;
					qpvNewRecord.CreatedBy = createdBy;
					qpvNewRecord.CreatedOn = createdOn;
					qpvNewRecord.ModifiedBy = createdBy;
					qpvNewRecord.ModifiedOn = createdOn;
					colJumpstationGroupSelectorQueryParameterValue.Add(qpvNewRecord);
				}
				colJumpstationGroupSelectorQueryParameterValue.SaveAll(SecurityManager.CurrentUserIdentityName);

				originalJumpstationGroupSelector.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newJumpstationGroupSelector));
				newJumpstationGroupSelector.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalJumpstationGroupSelector));

				scope.Complete(); // transaction complete
			}
			return newJumpstationGroupSelector;
		}

		/// <summary>
		/// Delete the JumpstationGroupSelector
		/// </summary>
		public void Delete()
		{
				try
				{
					this.ClearJumpstationGroupSelectorQueryParameterValue();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture,
												   "Unable to delete JumpstationGroupSelectorId #{0}.",
												   this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
		}

		/// <summary>
		/// Deletes a specified JumpstationGroupSelector record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = JumpstationGroupSelector.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			JumpstationGroupSelectorController.DestroyByQuery(query);
		}

		#region JumpstationGroupSelector-related convenience members

		/// <summary>
		/// Removes all associated configuration service Group selectors
		/// </summary>
		public void ClearJumpstationGroupSelectorQueryParameterValue()
		{
			JumpstationGroupSelectorQueryParameterValue.DestroyByJumpstationGroupSelectorId(this.Id);
		}

		#endregion

		#endregion

	}
}
