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
	/// The non-generated portion of the JumpstationMacroValueController class.
	/// </summary>
	public partial class JumpstationMacroValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static JumpstationMacroValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			JumpstationMacroValueQuerySpecification qs = new JumpstationMacroValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			JumpstationMacroValueQuerySpecification qs = new JumpstationMacroValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static JumpstationMacroValueCollection Fetch(JumpstationMacroValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<JumpstationMacroValueCollection>();
		}

		public static int FetchCount(JumpstationMacroValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationMacroValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationMacroValueController.CreateQueryHelper(qs, JumpstationMacroValue.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(JumpstationMacroValueQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			if (qs.MatchName != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "MatchName", qs.MatchName);				
			}

			if (qs.ResultValue != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ResultValue", qs.ResultValue);
			}

            if (qs.JumpstationMacroId != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "JumpstationMacroId", qs.JumpstationMacroId);
            }

            if (qs.RowStatusId != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "RowStatusId", qs.RowStatusId);
            }

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(JumpstationMacroValueController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(JumpstationMacroValueQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapJumpstationMacroValueController.Fetch(qs);
			}
			else
			{
				return JumpstationMacroValueController.Fetch(qs);
			}
		}

		#endregion


        #region Other Querying Methods

        /// <summary>
        /// Convenience method.
        /// Returns the <see cref="VwMapJumpstationMacroValue"/> s that are related to a specified <see cref="JumpstationMacro" />
        /// <param name="name"></param>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="macroId"></param>
        public static VwMapJumpstationMacroValueCollection FetchByNameMacroId(string name, int macroId)
        {
            SqlQuery query = DB.Select().From(VwMapJumpstationMacroValue.Schema);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "JumpstationMacroId", macroId);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(VwMapJumpstationMacroValueController));
            return query.ExecuteAsCollection<VwMapJumpstationMacroValueCollection>();
        }

        #endregion

		#endregion

	}
}
