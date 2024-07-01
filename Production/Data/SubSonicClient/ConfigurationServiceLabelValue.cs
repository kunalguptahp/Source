using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceLabelValue table.
	/// </summary>
	public partial class ConfigurationServiceLabelValue
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by ConfigurationServiceGroupId
		/// </summary>
		public static void DestroyByConfigurationServiceGroupId(int configurationServiceGroupId)
		{
			Query query = ConfigurationServiceLabelValue.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceGroupIdColumn.ColumnName, configurationServiceGroupId);
            ConfigurationServiceLabelValueController.DestroyByQuery(query);
		}

        /// <summary>
        /// Delete all records by ConfigurationServiceGroupId and ConfigurationServiceLabelId
        /// </summary>
        public static void DestroyByConfigurationServiceGroupIdLabelId(int configurationServiceGroupId, int configurationServiceLabelId)
        {
            Query query = ConfigurationServiceLabelValue.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(ConfigurationServiceGroupIdColumn.ColumnName, configurationServiceGroupId);
            query = query.WHERE(ConfigurationServiceLabelIdColumn.ColumnName, configurationServiceLabelId);
            ConfigurationServiceLabelValueController.DestroyByQuery(query);
        }

		#endregion

	}
}
