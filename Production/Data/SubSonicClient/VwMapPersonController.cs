using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using HP.ElementsCPS.Data.Utility;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapPerson class.
	/// </summary>
	public partial class VwMapPersonController
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
				GenericQuerySpecificationWrapper.Key_RowStateId,
				PersonQuerySpecification.Key_LastName,
				PersonQuerySpecification.Key_Email,
				PersonQuerySpecification.Key_FirstName,
				PersonQuerySpecification.Key_WindowsId
			};

		#endregion


		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapPersonCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			PersonQuerySpecification qs = new PersonQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			PersonQuerySpecification qs = new PersonQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapPersonCollection Fetch(PersonQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapPersonCollection>();
		}

		public static int FetchCount(PersonQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(PersonQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
#warning Not Implemented: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
			//ElementsCPSDataUtility.ValidateQuerySpecificationConditions(qs, _ValidQueryConditionKeys);
			return PersonController.CreateQueryHelper(qs, VwMapPerson.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
