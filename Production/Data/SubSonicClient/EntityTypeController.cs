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
	/// The non-generated portion of the ProductFamilyController class.
	/// </summary>
	public partial class EntityTypeController
	{
		#region Constants

		private static readonly string[] _ValidQueryConditionKeys = new[]
			{
				GenericQuerySpecificationWrapper.Key_Comments,
				GenericQuerySpecificationWrapper.Key_CreatedAfter,
				GenericQuerySpecificationWrapper.Key_CreatedBefore,
				GenericQuerySpecificationWrapper.Key_CreatedBy,
				GenericQuerySpecificationWrapper.Key_Id,
				GenericQuerySpecificationWrapper.Key_IdList,
				GenericQuerySpecificationWrapper.Key_ModifiedAfter,
				GenericQuerySpecificationWrapper.Key_ModifiedBefore,
				GenericQuerySpecificationWrapper.Key_ModifiedBy,
				GenericQuerySpecificationWrapper.Key_Name,
				GenericQuerySpecificationWrapper.Key_RowStateId
			};

		#endregion

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static EntityTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			EntityTypeQuerySpecification qs = new EntityTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			EntityTypeQuerySpecification qs = new EntityTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static EntityTypeCollection Fetch(EntityTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<EntityTypeCollection>();
		}

		public static int FetchCount(EntityTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(EntityTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
#warning Not Implemented: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
			//ElementsCPSDataUtility.ValidateQuerySpecificationConditions(qs, _ValidQueryConditionKeys);
			return EntityTypeController.CreateQueryHelper(qs, EntityType.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(EntityTypeQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(EntityTypeController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(EntityTypeQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapEntityTypeController.Fetch(qs);
			}
			else
			{
				return EntityTypeController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}