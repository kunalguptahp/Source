using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HP.HPFx.Data.Utility;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	public abstract class BaseRecordController<TRecord, TRecordCollection, TController> : IRecordController
		where TRecord : RecordBase<TRecord>, new()
		where TRecordCollection : AbstractList<TRecord, TRecordCollection>, new()
		where TController : BaseRecordController<TRecord, TRecordCollection, TController>, new()
	{
		public abstract TableSchema.Table GetRecordSchema();

		#region Informational Methods

		/// <summary>
		/// Convenience method that allows the caller to get the schema without the caller having to instantiate an instance.
		/// </summary>
		/// <returns></returns>
		public static TableSchema.Table GetTableSchema()
		{
			return ((IRecordController)NewController()).GetRecordSchema();
		}

		/// <summary>
		/// Convenience property.
		/// </summary>
		public static ISubSonicRepository Repository
		{
			get
			{
				return DB.Repository;
			}
		}

		public Type GetRecordType()
		{
			return typeof(TRecord);
		}

		public Type GetCollectionType()
		{
			return typeof(TRecordCollection);
		}

		public Type GetControllerType()
		{
			return typeof(TController);
		}

		#endregion

		#region Factory methods

		/// <summary>
		/// Instantiates a new <see cref="IRecordController"/> instance (of the correct type).
		/// </summary>
		/// <returns></returns>
		public static TController NewController()
		{
			return new TController();
		}

		/// <summary>
		/// Instantiates a new <see cref="IRecordCollection"/> instance (of the correct type).
		/// </summary>
		/// <returns></returns>
		public static TRecordCollection NewCollection()
		{
			return new TRecordCollection();
		}

		/// <summary>
		/// Instantiates a new <see cref="RecordBase{T}"/> instance (of the correct type).
		/// </summary>
		/// <returns></returns>
		public static TRecord NewRecord()
		{
			return new TRecord();
		}

		/// <summary>
		/// Convenience method.
		/// </summary>
		/// <returns></returns>
		public static SqlQuery NewQuery()
		{
			return NewSelectQuery();
		}

		#region NewSelectQuery Overloads

		/// <summary>
		/// Instantiates a new <see cref="SqlQuery"/> instance 
		/// pre-configured to perform a SELECT query (FROM the controller's associated table).
		/// </summary>
		/// <returns></returns>
		public static SqlQuery NewSelectQuery()
		{
			return Repository.Select().From(GetTableSchema());
		}

		/// <summary>
		/// Instantiates a new <see cref="SqlQuery"/> instance 
		/// pre-configured to perform a SELECT query (FROM the controller's associated table).
		/// </summary>
		/// <returns></returns>
		public static SqlQuery NewSelectQuery(params string[] columns)
		{
			return Repository.Select(columns).From(GetTableSchema());
		}

		/// <summary>
		/// Instantiates a new <see cref="SqlQuery"/> instance 
		/// pre-configured to perform a SELECT query (FROM the controller's associated table).
		/// </summary>
		/// <returns></returns>
		public static SqlQuery NewSelectQuery(params Aggregate[] aggregates)
		{
			return Repository.Select(aggregates).From(GetTableSchema());
		}

		#endregion

		/// <summary>
		/// Creates a <see cref="SqlQuery"/> that can be used to retrieve the distinct values within a specific table column.
		/// </summary>
		/// <param name="columnName">The name of the DB column.</param>
		/// <param name="columnValuePrefix">Optional. If not null, only column values that begin with the specified string will be returned.</param>
		/// <param name="rowStatusFilter">If not null, filters the results by the specified RowStatus.</param>
		[Obsolete("Not yet working correctly??? Need to retest since I made a change.", true)]
		public static SqlQuery NewDistinctColumnValuesQuery(string columnName, string columnValuePrefix, int? rowStatusFilter)
		{

			//SqlQuery query = NewSelectQuery(string.Format(CultureInfo.InvariantCulture, "DISTINCT {0}", columnName));
			SqlQuery query = NewSelectQuery(SubSonic.Aggregate.GroupBy(columnName));
			if (!string.IsNullOrEmpty(columnValuePrefix))
			{
				string likeOperand = SqlUtility.ValidateSqlLikeComparisonOperand(columnValuePrefix, false);
				likeOperand = SqlUtility.ValidateSqlLikeComparisonOperand(likeOperand + "%", true);
				if (!string.IsNullOrEmpty(likeOperand))
				{
					query.AndWhere(columnName).Like(likeOperand);
				}
			}
			if (rowStatusFilter != null)
			{
				query.AndWhere("RowStatusId").IsEqualTo(rowStatusFilter.Value);
			}
			query.OrderAsc(columnName);
			return query;
		}

		/// <summary>
		/// Instantiates a new <see cref="Query"/> instance (of the correct type).
		/// </summary>
		/// <returns></returns>
		//[Obsolete("The SubSonic.Query type is obsolete. Use the NewSelectQuery() method instead.", false)]
		public static Query NewQueryObject()
		{
			return new Query(GetTableSchema());
		}

		[Obsolete("The SubSonic.Query type is obsolete. Use the NewDistinctColumnValuesQuery() method instead.", false)]
		public static Query NewDistinctColumnValuesQuery2(string columnName, string columnValuePrefix, int? rowStatusFilter)
		{
			return SubSonicUtility.CreateQuery_DistinctColumnValues(GetTableSchema().TableName, columnName, columnValuePrefix, rowStatusFilter);
		}

		#endregion

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TRecordCollection FetchAll()
		{
			TRecordCollection recordCollection = NewCollection();
			Query qry = NewQueryObject();
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int FetchAllCount()
		{
			Query qry = NewQueryObject();
			return qry.GetCount("CreatedBy");
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TRecordCollection FetchAll(string sortExpression, int maximumRows, int startRowIndex)
		{
			TRecordCollection recordCollection = NewCollection();
			Query qry = NewQueryObject();
			SubSonicUtility.SetPaging(qry, maximumRows, startRowIndex);
			SubSonicUtility.SetOrderBy(qry, sortExpression);
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TRecordCollection FetchAll(string sortExpression)
		{
			TRecordCollection recordCollection = NewCollection();
			Query qry = NewQueryObject();
			SubSonicUtility.SetOrderBy(qry, sortExpression);
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TRecordCollection FetchByID(object id)
		{
			return NewCollection().Where("Id", id).Load();
		}

		public static TRecordCollection FetchByIDs(IEnumerable<int> ids)
		{
			if (ids != null)
			{
				ids = ids.Distinct(); //eliminate any duplicates
			}
			SqlQuery query = DB.Select().From(GetTableSchema()).Where(RowStatus.Columns.Id).In(ids);
			return query.ExecuteAsCollection<TRecordCollection>();
		}

        public static TRecordCollection FetchByAppClientIds(IEnumerable<int> ids)
        {
            if (ids != null)
            {
                ids = ids.Distinct(); //eliminate any duplicates
            }
            SqlQuery query = DB.Select().From(GetTableSchema()).Where(JumpstationGroup.Columns.AppClientId).In(ids);
            return query.ExecuteAsCollection<TRecordCollection>();
        }


        public static TRecordCollection FetchJumpstationGroupTypeByIDs(IEnumerable<int> ids)
        {
            if (ids != null)
            {
                ids = ids.Distinct(); //eliminate any duplicates
            }
            SqlQuery query = DB.Select().From(GetTableSchema()).Where(JumpstationGroupType.Columns.JumpstationApplicationId).In(ids);

            return query.ExecuteAsCollection<TRecordCollection>();
        }

        public static TRecordCollection FetchConfigurationServiceGroupTypeByIDs(IEnumerable<int> ids)
        {
            if (ids != null)
            {
                ids = ids.Distinct(); //eliminate any duplicates
            }
            SqlQuery query = DB.Select().From(GetTableSchema()).Where(ConfigurationServiceGroupType.Columns.ConfigurationServiceApplicationId).In(ids);

            return query.ExecuteAsCollection<TRecordCollection>();
        }

        public static TRecordCollection FetchWorkflowTypeByIDs(IEnumerable<int> ids)
        {
            if (ids != null)
            {
                ids = ids.Distinct(); //eliminate any duplicates
            }
            SqlQuery query = DB.Select().From(GetTableSchema()).Where(WorkflowApplication.Columns.WorkflowApplicationTypeId).In(ids);

            return query.ExecuteAsCollection<TRecordCollection>();
        }

		/// <summary>
		/// Convenience method.
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static TRecordCollection FetchByIDs(IEnumerable<int?> ids)
		{
			if (ids == null)
			{
				return new TRecordCollection();
			}
			return FetchByIDs(ids.Where(id => id != null).Select(id => id.Value));
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TRecordCollection FetchByStatus(string sortExpression, int maximumRows, int startRowIndex, int statusIdParameter)
		{
			TRecordCollection recordCollection = NewCollection();
			Query qry = NewQueryObject();
			// where clause
			if (Convert.ToInt32(statusIdParameter) != 0)
			{
				qry.WHERE("RowStatusId", statusIdParameter);
			}
			SubSonicUtility.SetPaging(qry, maximumRows, startRowIndex);
			SubSonicUtility.SetOrderBy(qry, sortExpression);

			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int FetchCountByStatus(string sortExpression, int maximumRows, int startRowIndex, int statusIdParameter)
		{
			//Note: Object Data Source requires that we pass in sortExpression, maximumRows and startRowIndex.
			return FetchCountByStatus(statusIdParameter);
		}

		public static int FetchCountByStatus(int statusIdParameter)
		{
			Query qry = NewQueryObject();
			// where clause
			if (Convert.ToInt32(statusIdParameter) != 0)
			{
				qry.WHERE("RowStatusId", statusIdParameter);
			}
			return qry.GetCount("CreatedBy");
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TRecordCollection FetchBySearchCriteria(string sortExpression, RowStatus.RowStatusId? rowStatus)
		{
			TRecordCollection recordCollection = NewCollection();
			Query qry = NewQueryObject();
			if (rowStatus != null)
			{
				qry.AddWhere("RowStatusId", (int)rowStatus.Value);
			}
			SubSonicUtility.SetOrderBy(qry, sortExpression);
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TRecordCollection FetchBySearchCriteria(string sortExpression, int? rowStatus)
		{
			return FetchBySearchCriteria(sortExpression, (RowStatus.RowStatusId?)rowStatus);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TRecordCollection FetchByQuery(Query qry)
		{
			TRecordCollection recordCollection = NewCollection();
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		//[DataObjectMethod(DataObjectMethodType.Delete, false)]
		public static void DestroyByQuery(Query query)
		{
			if (query.QueryType != QueryType.Delete)
			{
				throw new ArgumentException("Invalid QueryType.", "query");
			}
			query.Execute();
		}

	}
}