using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using HP.ElementsCPS.Data.Utility;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapEntityType class.
	/// </summary>
	public partial class VwMapEntityTypeController
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
		public static VwMapEntityTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
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

		public static VwMapEntityTypeCollection Fetch(EntityTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapEntityTypeCollection>();
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
			return EntityTypeController.CreateQueryHelper(qs, VwMapEntityType.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}