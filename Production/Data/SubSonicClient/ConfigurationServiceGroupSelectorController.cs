using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceGroupSelectorController class.
	/// </summary>
	public partial class ConfigurationServiceGroupSelectorController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ConfigurationServiceGroupSelectorCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceGroupSelectorQuerySpecification qs = new ConfigurationServiceGroupSelectorQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceGroupSelectorQuerySpecification qs = new ConfigurationServiceGroupSelectorQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceGroupSelectorCollection Fetch(ConfigurationServiceGroupSelectorQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceGroupSelectorCollection>();
		}

		public static int FetchCount(ConfigurationServiceGroupSelectorQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceGroupSelectorQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceGroupSelectorController.CreateQueryHelper(qs, ConfigurationServiceGroupSelector.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceGroupSelectorQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			//if (qs.Name != null)
			//{
			//    ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", qs.Name);
			//}

			if (qs.ConfigurationServiceGroupId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ConfigurationServiceGroupId", qs.ConfigurationServiceGroupId);
			}

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceGroupSelectorController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceGroupSelectorQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceGroupSelectorController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceGroupSelectorController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
