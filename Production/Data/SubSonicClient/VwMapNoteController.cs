using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using HP.ElementsCPS.Data.Utility;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    /// <summary>
    /// Strongly-typed collection for the VwMapNote class.
    /// </summary>
    public partial class VwMapNoteController
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
    			NoteQuerySpecification.Key_EntityTypeId,
    			NoteQuerySpecification.Key_EntityId,
    			NoteQuerySpecification.Key_NoteTypeId
    		};

    	#endregion


        #region ObjectDataSource Methods

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapNoteCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
            NoteQuerySpecification qs = new NoteQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
        {
            NoteQuerySpecification qs = new NoteQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
        }

        #endregion

        #region QuerySpecification-related Methods

        public static VwMapNoteCollection Fetch(NoteQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteAsCollection<VwMapNoteCollection>();
        }

        public static int FetchCount(NoteQuerySpecification qs)
        {
            return CreateQuery(qs, true).GetRecordCount();
        }

        #region CreateQuery

        private static SqlQuery CreateQuery(NoteQuerySpecification qs, bool isCountQuery)
        {
            //TODO: Implement: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
#warning Not Implemented: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
			//ElementsCPSDataUtility.ValidateQuerySpecificationConditions(qs, _ValidQueryConditionKeys);
            return NoteController.CreateQueryHelper(qs, VwMapNote.Schema, isCountQuery);
        }

        #endregion

        #endregion

    }
}
