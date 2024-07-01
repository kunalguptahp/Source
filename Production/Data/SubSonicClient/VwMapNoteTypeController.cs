using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using HP.ElementsCPS.Data.Utility;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapNoteType class.
	/// </summary>
	public partial class VwMapNoteTypeController
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
		public static VwMapNoteTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			NoteTypeQuerySpecification qs = new NoteTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			NoteTypeQuerySpecification qs = new NoteTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapNoteTypeCollection Fetch(NoteTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapNoteTypeCollection>();
		}

		public static int FetchCount(NoteTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(NoteTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
#warning Not Implemented: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
			//ElementsCPSDataUtility.ValidateQuerySpecificationConditions(qs, _ValidQueryConditionKeys);
			return NoteTypeController.CreateQueryHelper(qs, VwMapNoteType.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
