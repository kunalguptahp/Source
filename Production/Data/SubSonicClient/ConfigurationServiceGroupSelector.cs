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
	/// This is an ActiveRecord class which wraps the ConfigurationServiceGroupSelector table.
	/// </summary>
	public partial class ConfigurationServiceGroupSelector
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by ConfigurationServiceGroupId
		/// </summary>
		public static void DestroyByConfigurationServiceGroupId(int configurationServiceGroupId)
		{
			// Destroy GroupSelectorQueryParameterValues first
			ConfigurationServiceGroupSelectorQuerySpecification configurationServiceGroupSelectorQuerySpecification = 
				new ConfigurationServiceGroupSelectorQuerySpecification() { ConfigurationServiceGroupId = configurationServiceGroupId};
			ConfigurationServiceGroupSelectorCollection configurationServiceGroupSelectorColl =
				ConfigurationServiceGroupSelectorController.Fetch(configurationServiceGroupSelectorQuerySpecification);
			foreach (ConfigurationServiceGroupSelector cfgServiceGroupSelector in configurationServiceGroupSelectorColl)
			{
				ConfigurationServiceGroupSelectorQueryParameterValue.DestroyByConfigurationServiceGroupSelectorId(cfgServiceGroupSelector.Id);
			}
			
			// Destroy group selectors
			Query query = ConfigurationServiceGroupSelector.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceGroupIdColumn.ColumnName, configurationServiceGroupId);
			ConfigurationServiceGroupSelectorController.DestroyByQuery(query);
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? ConfigurationServiceGroupSelectorId = (this.IsNew ? null : (int?)this.Id);
			Log(ConfigurationServiceGroupSelectorId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? ConfigurationServiceGroupSelectorId = (this.IsNew ? null : (int?)this.Id);
			Log(ConfigurationServiceGroupSelectorId, severity, this, message, ex);
		}

		internal static void Log(int? ConfigurationServiceGroupSelectorId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroupSelector History: #{0}: {1}.", ConfigurationServiceGroupSelectorId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? ConfigurationServiceGroupSelectorId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroupSelector History: #{0}: {1}.", ConfigurationServiceGroupSelectorId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public ConfigurationServiceGroupSelector SaveAsNew()
		{
			return SaveAsNew(this);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="ConfigurationServiceGroup"/>.
		/// </summary>
		/// <param name="originalConfigurationServiceGroupSelector">The <see cref="ConfigurationServiceGroup"/> to copy/duplicate.</param>
		private static ConfigurationServiceGroupSelector SaveAsNew(ConfigurationServiceGroupSelector originalConfigurationServiceGroupSelector)
		{
			ConfigurationServiceGroupSelector newConfigurationServiceGroupSelector;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newConfigurationServiceGroupSelector = ConfigurationServiceGroupSelector.Copy(originalConfigurationServiceGroupSelector);

				//mark the newConfigurationServiceGroupSelector instance as an unsaved instance
				newConfigurationServiceGroupSelector.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newConfigurationServiceGroupSelector.CreatedBy = createdBy;
				newConfigurationServiceGroupSelector.CreatedOn = createdOn;
				newConfigurationServiceGroupSelector.ModifiedBy = createdBy;
				newConfigurationServiceGroupSelector.ModifiedOn = createdOn;
				newConfigurationServiceGroupSelector.RowStatusId = (int)RowStatus.RowStatusId.Active;

				//save the newConfigurationServiceGroup to the DB so that it is assigned an ID
				newConfigurationServiceGroupSelector.Save(SecurityManager.CurrentUserIdentityName);

				//copy all of the originalConfigurationServiceGroupSelector's ConfigurationServiceGroupSelectorQueryParameterValue data to the newConfigurationServiceGroupSelector
				ConfigurationServiceGroupSelectorQueryParameterValueCollection colConfigurationServiceGroupSelectorQueryParameterValue = new ConfigurationServiceGroupSelectorQueryParameterValueCollection();
				foreach (ConfigurationServiceGroupSelectorQueryParameterValue qpvOriginalRecord in originalConfigurationServiceGroupSelector.ConfigurationServiceGroupSelectorQueryParameterValueRecords())
				{
					ConfigurationServiceGroupSelectorQueryParameterValue qpvNewRecord = ConfigurationServiceGroupSelectorQueryParameterValue.Copy(qpvOriginalRecord);
					qpvNewRecord.MarkNew();
					qpvNewRecord.ConfigurationServiceGroupSelectorId = newConfigurationServiceGroupSelector.Id;
					qpvNewRecord.CreatedBy = createdBy;
					qpvNewRecord.CreatedOn = createdOn;
					qpvNewRecord.ModifiedBy = createdBy;
					qpvNewRecord.ModifiedOn = createdOn;
					colConfigurationServiceGroupSelectorQueryParameterValue.Add(qpvNewRecord);
				}
				colConfigurationServiceGroupSelectorQueryParameterValue.SaveAll(SecurityManager.CurrentUserIdentityName);

				originalConfigurationServiceGroupSelector.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newConfigurationServiceGroupSelector));
				newConfigurationServiceGroupSelector.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalConfigurationServiceGroupSelector));

				scope.Complete(); // transaction complete
			}
			return newConfigurationServiceGroupSelector;
		}

		/// <summary>
		/// Delete the ConfigurationServiceGroupSelector
		/// </summary>
		public void Delete()
		{
				try
				{
					this.ClearConfigurationServiceGroupSelectorQueryParameterValue();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture,
												   "Unable to delete ConfigurationServiceGroupSelectorId #{0}.",
												   this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
		}

		/// <summary>
		/// Deletes a specified ConfigurationServiceGroupSelector record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = ConfigurationServiceGroupSelector.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			ConfigurationServiceGroupSelectorController.DestroyByQuery(query);
		}

		#region ConfigurationServiceLabelValue-related convenience members

		/// <summary>
		/// Removes all associated configuration service Group selector label values
		/// </summary>
		public void ClearConfigurationServiceGroupSelectorQueryParameterValue()
		{
			ConfigurationServiceGroupSelectorQueryParameterValue.DestroyByConfigurationServiceGroupSelectorId(this.Id);
		}

		#endregion

		#endregion

	}
}
