using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.ElementsCPS.Data.Utility.ImportUtility;
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
	/// The non-generated portion of the JumpstationMacroController class.
	/// </summary>
	public partial class JumpstationMacroController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static JumpstationMacroCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			JumpstationMacroQuerySpecification qs = new JumpstationMacroQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			JumpstationMacroQuerySpecification qs = new JumpstationMacroQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static JumpstationMacroCollection Fetch(JumpstationMacroQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<JumpstationMacroCollection>();
		}

		public static int FetchCount(JumpstationMacroQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationMacroQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationMacroController.CreateQueryHelper(qs, JumpstationMacro.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(JumpstationMacroQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

            if (qs.CreatedBy != null)
            {
                string[] strs = qs.CreatedBy.Split(',');
                List<string> strlist = new List<string>();
                foreach (string str in strs)
                {
                    strlist.Add(str);
                }
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsIn(query, "CreatedBy", strlist);
            }

			if (qs.OwnerId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "OwnerId", qs.OwnerId);
			}

			if (qs.ValidationId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ValidationId", qs.ValidationId);
			}

			if (qs.ProductionId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ProductionId", qs.ProductionId);
			}

            if (qs.Name != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query,"Name", qs.Name);
            }

            if (qs.MatchName != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query,"MatchName",qs.MatchName);
            }
		  if (qs.JumpstationMacroStatusId != null)
		  {
			 ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "JumpstationMacroStatusId", qs.JumpstationMacroStatusId);
		  }

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(JumpstationMacroController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(JumpstationMacroQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapJumpstationMacroController.Fetch(qs);
			}
			else
			{
				return JumpstationMacroController.Fetch(qs);
			}
		}

		#endregion

		#endregion

		#region Tag Accessor Methods

		/// <summary>
		/// Returns a list of the names of the Tags associated with a specified JumpstationMacro.
		/// </summary>
		/// <param name="JumpstationMacroId"></param>
		/// <param name="rowStatusId"></param>
		/// <returns></returns>
		public static List<string> GetTagNameList(int JumpstationMacroId, RowStatus.RowStatusId rowStatusId)
		{
			return new List<string>(JumpstationMacroController.GetTagNames(JumpstationMacroId, rowStatusId));
		}

		public static IEnumerable<string> GetTagNames(int JumpstationMacroId, RowStatus.RowStatusId rowStatusId)
		{
			//TODO: Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar
#warning Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar

			const string sql = "SELECT [dbo].[JumpstationMacro_GetTagList](@JumpstationMacroId, ', ', @TagRowStatusId)";

			string tagNameList = SqlUtility.ExecuteAsScalar<string>(
				SqlUtility.CreateSqlCommandForSql(
					ElementsCPSSqlUtility.CreateDefaultConnection(),
					sql,
					SqlUtility.CreateSqlParameter("@JumpstationMacroId", SqlDbType.Int, JumpstationMacroId),
					SqlUtility.CreateSqlParameter("@TagRowStatusId", SqlDbType.Int, RowStatus.RowStatusId.Active)));
			return Tag.ParseTagNameList(tagNameList, false);
		}
		
		#endregion


		#region Publish Validation

		public static bool IsQueryParameterValuesDuplicated(int JumpstationMacroId)
		{
			//TODO: Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar
			#warning Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar

			const string sql = "SELECT [dbo].[JumpstationMacro_HasDuplicateQueryParameterValues](@JumpstationMacroId)";

			int JumpstationMacroDuplicateCount = SqlUtility.ExecuteAsScalar<int>(
				SqlUtility.CreateSqlCommandForSql(
					ElementsCPSSqlUtility.CreateDefaultConnection(),
					sql,
					SqlUtility.CreateSqlParameter("@JumpstationMacroId", SqlDbType.Int, JumpstationMacroId)));
			return (JumpstationMacroDuplicateCount > 0);
		}

		public static bool IsQueryParameterValuesDuplicated(int JumpstationMacroId, string queryParameterValueIdDelimitedList)
		{
			//TODO: Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar
			#warning Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar

			if (queryParameterValueIdDelimitedList == null)
			{
				return false;
			}

			const string sql = "SELECT [dbo].[JumpstationMacro_HasDuplicateQueryParameterValuesDelimitedList](@JumpstationMacroId, @JumpstationMacroQueryParameterValueIdList)";

			int JumpstationMacroDuplicateCount = SqlUtility.ExecuteAsScalar<int>(
				SqlUtility.CreateSqlCommandForSql(
					ElementsCPSSqlUtility.CreateDefaultConnection(),
					sql,
					SqlUtility.CreateSqlParameter("@JumpstationMacroId", SqlDbType.Int, JumpstationMacroId),
					SqlUtility.CreateSqlParameter("@JumpstationMacroQueryParameterValueIdList", SqlDbType.VarChar,
					                              queryParameterValueIdDelimitedList)));

			return (JumpstationMacroDuplicateCount > 0);
		}

		#endregion


		#region Other Methods

		/// <summary>
		/// Convenience method.
		/// Returns the <see cref="JumpstationMacro"/> with the specified name (if one exists).
		/// </summary>
		/// <param name="name"></param>
		public static JumpstationMacro FetchByName(string name)
		{
			SqlQuery query = DB.Select().From(JumpstationMacro.Schema);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
			ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(JumpstationMacroController));
			JumpstationMacro instance = query.ExecuteSingle<JumpstationMacro>();
			return instance;
		}

		public static string GenerateDefaultName(int? JumpstationMacroId)
		{
			return string.Format("New Product for JumpstationMacro #{0} created {1:U}", JumpstationMacroId, DateTime.UtcNow);
		}

		#endregion


		#region Data Import Methods

		public static ImportUtilityForStatefulEntity<JumpstationMacro> CreateImportUtility()
		{
			ImportUtilityForStatefulEntity<JumpstationMacro> importUtilityForStatefulEntity =
				new ImportUtilityForStatefulEntity<JumpstationMacro>(JumpstationMacro.Columns.JumpstationMacroStatusId)
					{
						NewInstanceFactory = () => new JumpstationMacro(true)
						, ModifyLifecycleStateCallback = importUtilityForStatefulEntity_SetState
					};
			return importUtilityForStatefulEntity;
		}

		private static void importUtilityForStatefulEntity_SetState(JumpstationMacro instance, object valueToSet)
		{
			if (valueToSet != null)
			{
				JumpstationMacroStateId stateId = ((JumpstationMacroStateId)ElementsCPSDataUtility.ParseImportValueAsInt32(valueToSet));
				if (instance.CurrentState != stateId)
				{
					instance.GoToState(stateId);
				}
			}
		}

		#endregion

	}
}
