using HP.ElementsCPS.Core.Security;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceGroupTag class.
	/// </summary>
	public partial class ConfigurationServiceGroupTag
	{
		/// <summary>
		/// Delete all records by ConfigurationServiceGroupId
		/// </summary>
		public static void DestroyByConfigurationServiceGroupId(int configurationServiceGroupId)
		{
			Query query = ConfigurationServiceGroupTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceGroupIdColumn.ColumnName, configurationServiceGroupId);
			ConfigurationServiceGroupTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified <see cref="ConfigurationServiceGroupTag"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int configurationServiceGroupId, int tagId)
		{
			Query query = ConfigurationServiceGroupTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceGroupIdColumn.ColumnName, configurationServiceGroupId);
			query = query.WHERE(TagIdColumn.ColumnName, tagId);
			ConfigurationServiceGroupTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Creates and returns a new <see cref="ConfigurationServiceGroupTag"/> instance with the properties.
		/// </summary>
		/// <param name="configurationServiceGroupId"></param>
		/// <param name="tagId"></param>
		/// <returns>An <see cref="ConfigurationServiceGroupTag"/> instance with the specified properties.</returns>
		public static ConfigurationServiceGroupTag Insert(int configurationServiceGroupId, int tagId)
		{
			ConfigurationServiceGroupTag ConfigurationServiceGroupTag = new ConfigurationServiceGroupTag(true);
			ConfigurationServiceGroupTag.ConfigurationServiceGroupId = configurationServiceGroupId;
			ConfigurationServiceGroupTag.TagId = tagId;
			ConfigurationServiceGroupTag.Save(SecurityManager.CurrentUserIdentityName);
			return ConfigurationServiceGroupTag;
		}
	}
}
