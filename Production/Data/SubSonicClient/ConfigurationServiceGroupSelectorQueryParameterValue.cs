using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceGroupSelector_QueryParameterValue table.
	/// </summary>
	public partial class ConfigurationServiceGroupSelectorQueryParameterValue
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by ConfigurationServiceGroupSelectorId and QueryParameterId
		/// </summary>
		public static void DestroyByConfigurationServiceGroupSelectorIdQueryParameterId(int configurationServiceGroupSelectorId, int queryParameterId)
		{
			SubSonic.StoredProcedure sp = StoredProcedures.DeleteConfigurationServiceGroupSelectorQueryParameterValueByConfigurationServiceGroupSelectorIdQueryParameterId(configurationServiceGroupSelectorId, queryParameterId);
			sp.ExecuteScalar();
		}

		/// <summary>
		/// Deletes a specified <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void DestroyByConfigurationServiceGroupSelectorId(int configurationServiceGroupSelectorId)
		{
			Query query = ConfigurationServiceGroupSelectorQueryParameterValue.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceGroupSelectorIdColumn.ColumnName, configurationServiceGroupSelectorId);
			ConfigurationServiceGroupSelectorQueryParameterValueController.DestroyByQuery(query);
		}

		#endregion

	}
}
