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
    public partial class AppClientController
    {

        #region ObjectDataSource Methods

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static AppClientCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
            AppClientQuerySpecification qs = new AppClientQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
        {
            AppClientQuerySpecification qs = new AppClientQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
        }

        #endregion

        #region QuerySpecification-related Methods

        public static AppClientCollection Fetch(AppClientQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteAsCollection<AppClientCollection>();
        }

        public static AppClient FetchByName(string name)
        {
            SqlQuery query = DB.Select().From(AppClient.Schema);
            AppClientQuerySpecification qs = new AppClientQuerySpecification();

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(AppClientController));
            AppClient instance = query.ExecuteSingle<AppClient>();
            return instance;
        }

        public static int FetchCount(AppClientQuerySpecification qs)
        {
            return CreateQuery(qs, true).GetRecordCount();
        }

        #region CreateQuery

        private static SqlQuery CreateQuery(AppClientQuerySpecification qs, bool isCountQuery)
        {
            //TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
            return AppClientController.CreateQueryHelper(qs, AppClient.Schema, isCountQuery);
        }

        #endregion

        #region CreateQueryHelper

        internal static SqlQuery CreateQueryHelper(AppClientQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
        {
            SqlQuery query = DB.Select().From(schema);


            if (qs.Name != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", qs.Name);
            }
            ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

            if (!isCountQuery)
            {
                SubSonicUtility.SetPaging(query, qs.Paging);
                SubSonicUtility.SetOrderBy(query, qs.SortBy);
            }

            ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(AppClientController));
            return query;
        }

        #endregion

        #region SmartFetch Method

        public static IRecordCollection SmartFetch(AppClientQuerySpecification qs, bool includeRowStatusNameInOutput)
        {
            if (includeRowStatusNameInOutput)
            {
                return VwMapAppClientController.Fetch(qs);
            }
            else
            {
                return AppClientController.Fetch(qs);
            }
        }

        #endregion

        #endregion

    }
}
