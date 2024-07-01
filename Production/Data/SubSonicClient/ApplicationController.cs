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
    /// The non-generated portion of the GroupTypeController class.
    /// </summary>
    public partial class ApplicationController
    {

        #region ObjectDataSource Methods

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static ApplicationCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
            ApplicationQuerySpecification qs = new ApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
        {
            ApplicationQuerySpecification qs = new ApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
        }

        #endregion

        #region QuerySpecification-related Methods

        public static ApplicationCollection Fetch(ApplicationQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteAsCollection<ApplicationCollection>();
        }

        public static Application FetchByName(string name)
        {
            SqlQuery query = DB.Select().From(Application.Schema);
            ApplicationQuerySpecification qs = new ApplicationQuerySpecification();

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(ApplicationController));
            Application instance = query.ExecuteSingle<Application>();
            return instance;
        }

        public static int FetchCount(ApplicationQuerySpecification qs)
        {
            return CreateQuery(qs, true).GetRecordCount();
        }

        #region CreateQuery

        private static SqlQuery CreateQuery(ApplicationQuerySpecification qs, bool isCountQuery)
        {
            //TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
            return ApplicationController.CreateQueryHelper(qs, Application.Schema, isCountQuery);
        }

        #endregion

        #region CreateQueryHelper

        internal static SqlQuery CreateQueryHelper(ApplicationQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
        {
            SqlQuery query = DB.Select().From(schema);

            ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

            if (!isCountQuery)
            {
                SubSonicUtility.SetPaging(query, qs.Paging);
                SubSonicUtility.SetOrderBy(query, qs.SortBy);
            }

            ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ApplicationController));
            return query;
        }

        #endregion

        #region SmartFetch Method

        public static IRecordCollection SmartFetch(ApplicationQuerySpecification qs, bool includeRowStatusNameInOutput)
        {
            if (includeRowStatusNameInOutput)
            {
                return VwMapApplicationController.Fetch(qs);
            }
            else
            {
                return ApplicationController.Fetch(qs);
            }
        }

        #endregion

        #endregion

    }
}
