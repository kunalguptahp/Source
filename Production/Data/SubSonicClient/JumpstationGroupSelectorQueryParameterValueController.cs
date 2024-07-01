using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the JumpstationGroupSelectorQueryParameterValueController class.
	/// </summary>
	public partial class JumpstationGroupSelectorQueryParameterValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static JumpstationGroupSelectorQueryParameterValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			JumpstationGroupSelectorQueryParameterValueQuerySpecification qs = new JumpstationGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			JumpstationGroupSelectorQueryParameterValueQuerySpecification qs = new JumpstationGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static JumpstationGroupSelectorQueryParameterValueCollection Fetch(JumpstationGroupSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<JumpstationGroupSelectorQueryParameterValueCollection>();
		}

		public static int FetchCount(JumpstationGroupSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationGroupSelectorQueryParameterValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationGroupSelectorQueryParameterValueController.CreateQueryHelper(qs, JumpstationGroupSelectorQueryParameterValue.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(JumpstationGroupSelectorQueryParameterValueQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			if (qs.JumpstationGroupSelectorId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "JumpstationGroupSelectorId", qs.JumpstationGroupSelectorId);
			}

			if (qs.QueryParameterId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "QueryParameterId", qs.QueryParameterId);
			}

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(JumpstationGroupSelectorQueryParameterValueController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(JumpstationGroupSelectorQueryParameterValueQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapJumpstationGroupSelectorQueryParameterValueController.Fetch(qs);
			}
			else
			{
				return JumpstationGroupSelectorQueryParameterValueController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
