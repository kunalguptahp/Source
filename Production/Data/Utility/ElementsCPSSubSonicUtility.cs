using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility;
using SubSonic;
using System.Collections.Generic;

namespace HP.ElementsCPS.Data.Utility
{
    /// <summary>
    /// Contains utility methods related to SubSonic.
    /// </summary>
    public static class ElementsCPSSubSonicUtility
    {

        #region Query-related Utility Methods

        public static string GetCountSelect(SqlQuery query, bool useReflection)
        {
            //TODO: Refactoring: Migrate to HPFx
#warning Refactoring: Migrate to HPFx

            //NOTE: Performance: It is be better for performance if we avoid using Reflection here
            ISqlGenerator sqlGenerator;
            if (useReflection)
            {
                sqlGenerator = ReflectionUtility.InvokeMethod<ISqlGenerator>(
                    query, "GetGenerator", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            }
            else
            {
                sqlGenerator = DB._provider.GetSqlGenerator(query);
            }
            return sqlGenerator.GetCountSelect();
        }

        /// <summary>
        /// Helper method for appropriately configuring the specified <see cref="SqlQuery"/> 
        /// based upon the most common "generic" query conditions that many tables/queries would support.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="qs"></param>
        public static void AddGenericConditionsToQuery(SqlQuery query, GenericQuerySpecificationWrapper qs)
        {
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualToAny(query, "Id", qs.IdList);

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Id", qs.Id);

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "StatusId", qs.LifecycleStateId);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "RowStatusId", qs.RowStateId);

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsGreaterThanOrEqualTo(query, "CreatedOn", qs.CreatedAfter);

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsLessThan(query, "CreatedOn", qs.CreatedBefore);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsGreaterThanOrEqualTo(query, "ModifiedOn", qs.ModifiedAfter);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsLessThan(query, "ModifiedOn", qs.ModifiedBefore);

            
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

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "ModifiedBy", qs.ModifiedBy, true);

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereStartsWith(query, "Name", qs.Name);

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereContains(query, "Description", qs.Description);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereContains(query, "Comment", qs.Comments);
        }

        public static void AddStandardQueryCondition_AndWhereIsIn(SqlQuery query, string columnName, List<string> valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).In(valueToMatch);
            }
        }

        #region AddStandardQueryCondition... Methods

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsEqualTo(SqlQuery query, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).IsEqualTo(valueToMatch);
            }
        }

        public static void AddStandardQueryCondition_AndWhereIsIn(SqlQuery query, string columnName, List<int> valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).In(valueToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_WhereIsEqualTo(SqlQuery query, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).IsEqualTo(valueToMatch);
            }
        }


        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="schema"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_WhereIsEqualTo(SqlQuery query, TableSchema.Table schema, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.Where(schema.Columns.GetColumn(columnName)).IsEqualTo(valueToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="schema"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsEqualTo(SqlQuery query, TableSchema.Table schema, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(schema.Columns.GetColumn(columnName)).IsEqualTo(valueToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueEqualsNull"></param>
        public static void AddStandardQueryCondition_AndWhereIsEqualToNull(SqlQuery query, string columnName, bool? valueEqualsNull)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueEqualsNull != null)
            {
                if (valueEqualsNull.Value)
                {
                    query.AndWhere(columnName).IsNull();
                }
                else
                {
                    query.AndWhere(columnName).IsNotNull();
                }
            }
        }

        #region AddStandardQueryCondition_AndWhereIsEqualToAny Methods

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valuesToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsEqualToAny(SqlQuery query, string columnName, IList valuesToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if ((valuesToMatch != null) && (valuesToMatch.Count > 0))
            {
                query.AndWhere(columnName).In(valuesToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valuesToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsEqualToAny(SqlQuery query, string columnName, Array valuesToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if ((valuesToMatch != null) && (valuesToMatch.Length > 0))
            {
                query.AndWhere(columnName).In(valuesToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valuesToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsEqualToAny(SqlQuery query, string columnName, Select valuesToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valuesToMatch != null)
            {
                query.AndWhere(columnName).In(valuesToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valuesToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsEqualToAny(SqlQuery query, string columnName, params object[] valuesToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if ((valuesToMatch != null) && (valuesToMatch.Length > 0))
            {
                query.AndWhere(columnName).In(valuesToMatch);
            }
        }

        #endregion

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsGreaterThan(SqlQuery query, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).IsGreaterThan(valueToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsGreaterThanOrEqualTo(SqlQuery query, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).IsGreaterThanOrEqualTo(valueToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsLessThan(SqlQuery query, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).IsLessThan(valueToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsLessThanOrEqualTo(SqlQuery query, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).IsLessThanOrEqualTo(valueToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsNotEqualTo(SqlQuery query, string columnName, object valueToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valueToMatch != null)
            {
                query.AndWhere(columnName).IsNotEqualTo(valueToMatch);
            }
        }

        #region AddStandardQueryCondition_AndWhereIsNotEqualToAny Methods

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valuesToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsNotEqualToAny(SqlQuery query, string columnName, IList valuesToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if ((valuesToMatch != null) && (valuesToMatch.Count > 0))
            {
                query.AndWhere(columnName).NotIn(valuesToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valuesToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsNotEqualToAny(SqlQuery query, string columnName, Array valuesToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if ((valuesToMatch != null) && (valuesToMatch.Length > 0))
            {
                query.AndWhere(columnName).NotIn(valuesToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valuesToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsNotEqualToAny(SqlQuery query, string columnName, Select valuesToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if (valuesToMatch != null)
            {
                query.AndWhere(columnName).NotIn(valuesToMatch);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valuesToMatch"></param>
        public static void AddStandardQueryCondition_AndWhereIsNotEqualToAny(SqlQuery query, string columnName, params object[] valuesToMatch)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            if ((valuesToMatch != null) && (valuesToMatch.Length > 0))
            {
                query.AndWhere(columnName).NotIn(valuesToMatch);
            }
        }

        #endregion

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        /// <param name="allowWildcards"></param>
        public static void AddStandardQueryCondition_AndWhereLike(SqlQuery query, string columnName, string valueToMatch, bool allowWildcards)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            string likeExpression = SqlUtility.ValidateSqlLikeComparisonOperand(valueToMatch, allowWildcards);
			if (!string.IsNullOrEmpty(likeExpression))
			{
				query.AndWhere(columnName).Like(likeExpression);
			}
		}

        /// <summary>
        /// e.g. when filter "en_es" ,the "*" will be returned
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        /// <param name="valueToMatchOr"></param>
        /// <param name="allowWildcards"></param>
        public static void AddStandardQueryCondition_AndWhereLike(SqlQuery query, string columnName, string valueToMatch, string valueToMatchOr, bool allowWildcards)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            string likeExpression = SqlUtility.ValidateSqlLikeComparisonOperand(valueToMatch, allowWildcards);
            string likeExpressionOr = SqlUtility.ValidateSqlLikeComparisonOperand(valueToMatchOr, allowWildcards);
            if (!string.IsNullOrEmpty(likeExpression))
            {       
                    query.AndWhereExpression(columnName).Like(likeExpression).Or(columnName).IsEqualTo(likeExpressionOr);
            }
        }

		/// <summary>
		/// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="columnName"></param>
		/// <param name="valueToMatch"></param>
		/// <param name="allowWildcards"></param>
		public static void AddStandardQueryCondition_AndWhereLike(SqlQuery query, TableSchema.Table schema, string columnName, string valueToMatch, bool allowWildcards)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
			ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            string likeExpression = SqlUtility.ValidateSqlLikeComparisonOperand(valueToMatch, allowWildcards);
            if (!string.IsNullOrEmpty(likeExpression))
            {
                query.AndWhere(schema.Columns.GetColumn(columnName)).Like(likeExpression);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        /// <param name="allowWildcards"></param>
        public static void AddStandardQueryCondition_WhereLike(SqlQuery query, string columnName, string valueToMatch, bool allowWildcards)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            string likeExpression = SqlUtility.ValidateSqlLikeComparisonOperand(valueToMatch, allowWildcards);
            if (!string.IsNullOrEmpty(likeExpression))
            {
                query.Where(columnName).Like(likeExpression);
            }
        }

        /// <summary>
        /// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="schema"></param>
        /// <param name="columnName"></param>
        /// <param name="valueToMatch"></param>
        /// <param name="allowWildcards"></param>
        public static void AddStandardQueryCondition_WhereLike(SqlQuery query, TableSchema.Table schema, string columnName, string valueToMatch, bool allowWildcards)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(query, "query");
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(columnName, "columnName");

            string likeExpression = SqlUtility.ValidateSqlLikeComparisonOperand(valueToMatch, allowWildcards);
            if (!string.IsNullOrEmpty(likeExpression))
            {
                query.Where(schema.Columns.GetColumn(columnName)).Like(likeExpression);
				
			}
			
	}

		/// <summary>
		/// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="columnName"></param>
		/// <param name="valueToMatch"></param>
		public static void AddStandardQueryCondition_AndWhereContains(SqlQuery query, string columnName, string valueToMatch)
		{
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, columnName, "%" + valueToMatch + "%", true);
		}

		/// <summary>
		/// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="columnName"></param>
		/// <param name="valueToMatch"></param>
		public static void AddStandardQueryCondition_AndWhereEndsWith(SqlQuery query, string columnName, string valueToMatch)
		{
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, columnName, "%" + valueToMatch, true);
		}

		/// <summary>
		/// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="columnName"></param>
		/// <param name="valueToMatch"></param>
		public static void AddStandardQueryCondition_AndWhereStartsWith(SqlQuery query, string columnName, string valueToMatch)
		{
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, columnName, valueToMatch + "%", true);
		}

		/// <summary>
		/// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="columnName"></param>
		/// <param name="valueToMatch"></param>
		public static void AddStandardQueryCondition_AndWhereStartsWith(SqlQuery query, TableSchema.Table schema, string columnName, string valueToMatch)
		{
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, schema, columnName, valueToMatch + "%", true);
		}

		/// <summary>
		/// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="schema"></param>
		/// <param name="columnName"></param>
		/// <param name="valueToMatch"></param>
		public static void AddStandardQueryCondition_WhereStartsWith(SqlQuery query, TableSchema.Table schema, string columnName, string valueToMatch)
		{
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_WhereLike(query, schema, columnName, valueToMatch + "%", true);
		}


		/// <summary>
		/// Helper method for performing a very common query configuration operation that would otherwise result in significant code duplication.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="columnName"></param>
		/// <param name="valueToMatch"></param>
		public static void AddStandardQueryCondition_WhereStartsWith(SqlQuery query, string columnName, string valueToMatch)
		{
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_WhereLike(query, columnName, valueToMatch + "%", true);
		}

		#endregion

		/// <summary>
		/// Helper method for generating and logging the SQL for a specified <see cref="SqlQuery"/>.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="isCountQuery">Indicates whether the query is to be logged as a Count query or not.</param>
		/// <param name="source">The log event source.</param>
		public static void LogQuerySql(SqlQuery query, bool isCountQuery, object source)
		{
			//NOTE: This method is redundant to (and less useful than) the InvokeWithSubSonicQueryLogging logging functionality invoked from within ElementsCPSSqlDataProvider
			////NOTE: Performance: We defer message-string evaluation to prevent unnecessary construction of the message string (unless the message will actually be logged based on logging config settings)
			//Func<string> messageBuilder;
			//if (isCountQuery)
			//{
			//   messageBuilder = (() => ("SQL for SubSonic Query:\n" + query.BuildSqlStatement()));
			//}
			//else
			//{
			//   messageBuilder = (() => ("SQL for SubSonic Count Query:\n" + ElementsCPSSubSonicUtility.GetCountSelect(query, false)));
			//}
			//LogManager.Current.Log(Severity.Debug, source, messageBuilder);
		}

		#endregion

		#region Deadlock-related Utility Methods

		public static bool DeleteWithDeadlockRedundancy<TRecord>(object id)
			where TRecord : ActiveRecord<TRecord>, new()
		{
			return DeleteWithDeadlockRedundancy<TRecord>(id, 3, 10);
		}

		public static bool DeleteWithDeadlockRedundancy<TRecord>(object id, int maxAttempts, int delayIntervalMilliseconds)
			where TRecord : ActiveRecord<TRecord>, new()
		{
			//validate that maxAttempts is positive
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(maxAttempts, "maxAttempts", 1, false);

			//validate that delayIntervalMilliseconds is non-negative
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(delayIntervalMilliseconds, "delayIntervalMilliseconds", 0, false);

			return InvokeWithDeadlockRedundancy(() => (ActiveRecord<TRecord>.Delete(id) == 1), typeof (TRecord), maxAttempts, delayIntervalMilliseconds);
		}

		#region InvokeWithDeadlockRedundancy

		//TODO: Refactoring: Migrate to HPFx
#warning Refactoring: Migrate to HPFx

        /// <summary>
        /// Utility method that allows the caller to easily apply consistent deadlock-handling behavior to the invocation of an <see cref="Action"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be invoked.</param>
        /// <param name="logEventSource">The source (usually the caller) that should be listed as the event source in any log events created during the invocation(s).</param>
        /// <param name="maxAttempts">Th maximum number of times that the invocation should be attempted if deadlocks occur.</param>
        /// <param name="delayIntervalMilliseconds">The minimum number of milliseconds of the delay interval that will be used between invocation attempts.</param>
        /// <exception cref="Exception">Any <see cref="Exception"/> (other than one caused by a deadlock during a non-final attempt) that is thrown while invoking the action will be raised to the caller normally.</exception>
        public static void InvokeWithDeadlockRedundancy(Action action, object logEventSource, int maxAttempts, int delayIntervalMilliseconds)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(action, "action");
            //validate that maxAttempts is positive
            ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(maxAttempts, "maxAttempts", 1, false);
            //validate that delayIntervalMilliseconds is non-negative
            ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(delayIntervalMilliseconds, "delayIntervalMilliseconds", 0, false);

            for (int attemptCount = 1; attemptCount <= maxAttempts; attemptCount++)
            {
                try
                {
                    //invoke the specified action
                    action();
                    return; //return to indicate that the action was successfully invoked (within the specified number of allowed attempts) without a deadlock
                }
                catch (Exception ex)
                {
                    //if the exception is a SQL deadlock, then try again
                    if (ExceptionUtility.IsOrContainsSqlDeadlockException(ex))
                    {
                        bool isFinalAttempt = attemptCount >= maxAttempts;

                        //log the deadlocked attempt before proceeding
                        string logMessage = string.Format("A DB deadlock has occurred. Attempt #{0} of {1}.", attemptCount, maxAttempts);
                        Severity severity = isFinalAttempt ? Severity.Warn : Severity.Info;
                        LogManager.Current.Log(severity, logEventSource, logMessage, ex);

                        if (isFinalAttempt)
                        {
                            //this was the final attempt, so re-throw the current (i.e. final) exception
                            throw;
                        }
                        Thread.Sleep(delayIntervalMilliseconds); //pause before trying again
                    }
                    else
                    {
                        //LogManager.Current.Log(Severity.Info, logEventSource, "An unexpected exception occurred while attempting to perform a deadlock-safe operation.", ex);
                        throw;
                    }
                }
            }
            //NOTE: We should never reach this point, because both success and failure should have been handled above
            LogManager.Current.Log(Severity.Warn, typeof(ElementsCPSSubSonicUtility), "Execution has reached a point in the code that was never expected to be reached.");
            throw new Exception("Invocation failed.");
        }

        /// <summary>
        /// Convenience overload.
        /// </summary>
        public static void InvokeWithDeadlockRedundancy(Action action, object logEventSource)
        {
            InvokeWithDeadlockRedundancy(action, logEventSource, 3, 10);
        }

        /// <summary>
        /// Utility method that allows the caller to easily apply consistent deadlock-handling behavior to the invocation of a <see cref="Func{TResult}"/>.
        /// </summary>
        /// <typeparam name="TFuncResult"></typeparam>
        /// <param name="action">The <see cref="Func{TResult}"/> to be invoked.</param>
        /// <param name="logEventSource">The source (usually the caller) that should be listed as the event source in any log events created during the invocation(s).</param>
        /// <param name="maxAttempts">Th maximum number of times that the invocation should be attempted if deadlocks occur.</param>
        /// <param name="delayIntervalMilliseconds">The minimum number of milliseconds of the delay interval that will be used between invocation attempts.</param>
        /// <returns>The value returned by the successful invocation of the action.</returns>
        /// <exception cref="Exception">Any <see cref="Exception"/> (other than one caused by a deadlock during a non-final attempt) that is thrown while invoking the action will be raised to the caller normally.</exception>
        public static TFuncResult InvokeWithDeadlockRedundancy<TFuncResult>(Func<TFuncResult> action, object logEventSource, int maxAttempts, int delayIntervalMilliseconds)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(action, "action");
            //validate that maxAttempts is positive
            ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(maxAttempts, "maxAttempts", 1, false);
            //validate that delayIntervalMilliseconds is non-negative
            ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(delayIntervalMilliseconds, "delayIntervalMilliseconds", 0, false);

            for (int attemptCount = 1; attemptCount <= maxAttempts; attemptCount++)
            {
                try
                {
                    //invoke the specified action
                    TFuncResult actionReturnValue = action();
                    return actionReturnValue; //return the invoked Func's returned value (any return indicates success)
                }
                catch (Exception ex)
                {
                    //if the exception is a SQL deadlock, then try again
                    if (ExceptionUtility.IsOrContainsSqlDeadlockException(ex))
                    {
                        bool isFinalAttempt = attemptCount >= maxAttempts;

                        //log the deadlocked attempt before proceeding
                        string logMessage = string.Format("A DB deadlock (SqlException.Number={0}) has occurred. Attempt #{1} of {2}.", 1205, attemptCount, maxAttempts);
                        Severity severity = isFinalAttempt ? Severity.Warn : Severity.Info;
                        LogManager.Current.Log(severity, logEventSource, logMessage, ex);

                        if (isFinalAttempt)
                        {
                            //this was the final attempt, so re-throw the current (i.e. final) exception
                            throw;
                        }
                        Thread.Sleep(delayIntervalMilliseconds); //pause before trying again
                    }
                    else
                    {
                        //LogManager.Current.Log(Severity.Info, logEventSource, "An unexpected exception occurred while attempting to perform a deadlock-safe operation.", ex);
                        throw;
                    }
                }
            }
            //NOTE: We should never reach this point, because both success and failure should have been handled above
            LogManager.Current.Log(Severity.Warn, typeof(ElementsCPSSubSonicUtility), "Execution has reached a point in the code that was never expected to be reached.");
            throw new Exception("Invocation failed.");
        }

        /// <summary>
        /// Convenience overload.
        /// </summary>
        public static TFuncResult InvokeWithDeadlockRedundancy<TFuncResult>(Func<TFuncResult> action, object logEventSource)
        {
            return InvokeWithDeadlockRedundancy(action, logEventSource, 3, 10);
        }

        #endregion

        #region TryInvokeWithDeadlockRedundancy

        /// <summary>
        /// Utility method that allows the caller to easily apply consistent deadlock-handling behavior to the invocation of an <see cref="Action"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be invoked.</param>
        /// <param name="logEventSource">The source (usually the caller) that should be listed as the event source in any log events created during the invocation(s).</param>
        /// <param name="maxAttempts">Th maximum number of times that the invocation should be attempted if deadlocks occur.</param>
        /// <param name="delayIntervalMilliseconds">The minimum number of milliseconds of the delay interval that will be used between invocation attempts.</param>
        /// <returns><c>true</c> if the action was successfully invoked within the specified number of allowed attempts. <c>false</c> if every attempt resulted in a deadlock.</returns>
        /// <exception cref="Exception">Any <see cref="Exception"/> other than one caused by a deadlock that is thrown while invoking the action will be raised to the caller normally.</exception>
        public static bool TryInvokeWithDeadlockRedundancy(Action action, object logEventSource, int maxAttempts, int delayIntervalMilliseconds)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(action, "action");
            //validate that maxAttempts is positive
            ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(maxAttempts, "maxAttempts", 1, false);
            //validate that delayIntervalMilliseconds is non-negative
            ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(delayIntervalMilliseconds, "delayIntervalMilliseconds", 0, false);

            for (int attemptCount = 1; attemptCount <= maxAttempts; attemptCount++)
            {
                try
                {
                    //invoke the specified action
                    action();
                    return true; //return true to indicate that the action was successfully invoked (within the specified number of allowed attempts) without a deadlock
                }
                catch (Exception ex)
                {
                    //if the exception is a SQL deadlock, then try again
                    if (ExceptionUtility.IsOrContainsSqlDeadlockException(ex))
                    {
                        bool isFinalAttempt = attemptCount >= maxAttempts;

                        //log the deadlocked attempt before proceeding
                        string logMessage = string.Format("A DB deadlock (SqlException.Number={0}) has occurred. Attempt #{1} of {2}.", 1205, attemptCount, maxAttempts);
                        Severity severity = isFinalAttempt ? Severity.Warn : Severity.Info;
                        LogManager.Current.Log(severity, logEventSource, logMessage, ex);

                        if (isFinalAttempt)
                        {
                            //this was the final attempt, so we exit the loop and allow the post-loop code to handle the failure
                            break;
                        }
                        Thread.Sleep(delayIntervalMilliseconds); //pause before trying again
                    }
                    else
                    {
                        //LogManager.Current.Log(Severity.Info, logEventSource, "An unexpected exception occurred while attempting to perform a deadlock-safe operation.", ex);
                        throw;
                    }
                }
            }
            //if we reach this point, all attempts resulted in a deadlock
            return false; //return false to indicate that all attempts resulted in a deadlock
        }

        /// <summary>
        /// Utility method that allows the caller to easily apply consistent deadlock-handling behavior to the invocation of a <see cref="Func{TResult}"/>.
        /// </summary>
        /// <typeparam name="TFuncResult"></typeparam>
        /// <param name="action">The <see cref="Func{TResult}"/> to be invoked.</param>
        /// <param name="logEventSource">The source (usually the caller) that should be listed as the event source in any log events created during the invocation(s).</param>
        /// <param name="maxAttempts">Th maximum number of times that the invocation should be attempted if deadlocks occur.</param>
        /// <param name="delayIntervalMilliseconds">The minimum number of milliseconds of the delay interval that will be used between invocation attempts.</param>
        /// <param name="result">The value returned by the successful invocation of the action.</param>
        /// <returns><c>true</c> if the action was successfully invoked within the specified number of allowed attempts. <c>false</c> if every attempt resulted in a deadlock.</returns>
        /// <exception cref="Exception">Any <see cref="Exception"/> other than one caused by a deadlock that is thrown while invoking the action will be raised to the caller normally.</exception>
        public static bool TryInvokeWithDeadlockRedundancy<TFuncResult>(Func<TFuncResult> action, object logEventSource, int maxAttempts, int delayIntervalMilliseconds, out TFuncResult result)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(action, "action");
            //validate that maxAttempts is positive
            ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(maxAttempts, "maxAttempts", 1, false);
            //validate that delayIntervalMilliseconds is non-negative
            ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfLessThan(delayIntervalMilliseconds, "delayIntervalMilliseconds", 0, false);

            for (int attemptCount = 1; attemptCount <= maxAttempts; attemptCount++)
            {
                try
                {
                    //invoke the specified action
                    TFuncResult actionReturnValue = action();
                    result = actionReturnValue; //set the out param's value to the invoked Func's returned value
                    return true; //return true to indicate that the action was successfully invoked (within the specified number of allowed attempts) without a deadlock
                }
                catch (Exception ex)
                {
                    //if the exception is a SQL deadlock, then try again
                    if (ExceptionUtility.IsOrContainsSqlDeadlockException(ex))
                    {
                        bool isFinalAttempt = attemptCount >= maxAttempts;

                        //log the deadlocked attempt before proceeding
                        string logMessage = string.Format("A DB deadlock (SqlException.Number={0}) has occurred. Attempt #{1} of {2}.", 1205, attemptCount, maxAttempts);
                        Severity severity = isFinalAttempt ? Severity.Warn : Severity.Info;
                        LogManager.Current.Log(severity, logEventSource, logMessage, ex);

                        if (isFinalAttempt)
                        {
                            //this was the final attempt, so we exit the loop and allow the post-loop code to handle the failure
                            break;
                        }
                        Thread.Sleep(delayIntervalMilliseconds); //pause before trying again
                    }
                    else
                    {
                        //LogManager.Current.Log(Severity.Info, logEventSource, "An unexpected exception occurred while attempting to perform a deadlock-safe operation.", ex);
                        throw;
                    }
                }
            }
            //if we reach this point, all attempts resulted in a deadlock
            result = default(TFuncResult);
            return false;
        }

        #endregion

        #endregion

        #region InvokeWithSubSonicQueryLogging Method

        /// <seealso cref="ElementsCPSSqlUtility.InvokeWithSqlCommandLogging"/>
        internal static void InvokeWithSubSonicQueryLogging(Action action, object logEventSource, QueryCommand query)
        {
            HpfxUtility.InvokeWithCallback(action,
                (startTime, duration, ex) => LogManager.Current.Log(Severity.Debug, logEventSource, () => string.Format("SQL Executed: SubSonic QueryCommand: Executed in {0}. Query SQL:\n{1}", duration, query.CommandSql), ex)
                );
        }

        /// <seealso cref="ElementsCPSSqlUtility.InvokeWithSqlCommandLogging{TFuncResult}"/>
        internal static TFuncResult InvokeWithSubSonicQueryLogging<TFuncResult>(Func<TFuncResult> action, object logEventSource, QueryCommand query)
        {
            return HpfxUtility.InvokeWithCallback(action,
                (startTime, duration, ex, results) => LogManager.Current.Log(Severity.Debug, logEventSource, () => string.Format("SQL Executed: SubSonic QueryCommand: Executed in {0}. Query SQL:\n{1}", duration, query.CommandSql), ex)
                );
        }

        /// <seealso cref="InvokeWithSubSonicQueryLogging(System.Action,object,SubSonic.QueryCommand)"/>
        internal static void InvokeWithSubSonicQueryLogging(Action action, object logEventSource, QueryCommandCollection commands)
        {
            HpfxUtility.InvokeWithCallback(action,
                (startTime, duration, ex) => LogManager.Current.Log(Severity.Debug, logEventSource, () => string.Format("SQL Executed: SubSonic QueryCommandCollection: Executed in {0}. Query SQL:\n{1}", duration, string.Join("\n;\n", commands.Select(qc => qc.CommandSql).ToArray())), ex)
                );
        }

		#endregion

	}
}
