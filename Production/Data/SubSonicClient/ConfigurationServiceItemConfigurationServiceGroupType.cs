using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceItemConfigurationServiceGroupType table.
	/// </summary>
	public partial class ConfigurationServiceItemConfigurationServiceGroupType
	{
		/// <summary>
		/// Delete all records by configurationServiceItemId
		/// </summary>
		public static void DestroyByConfigurationServiceItemId(int configurationServiceItemId)
		{
			Query query = ConfigurationServiceItemConfigurationServiceGroupType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceItemIdColumn.ColumnName, configurationServiceItemId);
			ConfigurationServiceItemConfigurationServiceGroupTypeController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified ConfigurationServiceItemConfigurationServiceGroupType record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int configurationServiceItemId, int configurationServiceGroupTypeId)
		{
			Query query = ConfigurationServiceItemConfigurationServiceGroupType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceItemIdColumn.ColumnName, configurationServiceItemId);
			query = query.WHERE(ConfigurationServiceGroupTypeIdColumn.ColumnName, configurationServiceGroupTypeId);
			ConfigurationServiceItemConfigurationServiceGroupTypeController.DestroyByQuery(query);
		}
	}
}
