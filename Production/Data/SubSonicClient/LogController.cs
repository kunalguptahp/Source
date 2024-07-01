using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	partial class LogController
	{
		#region Constants

		private static readonly string[] _ValidQueryConditionKeys = new[]
			{
				GenericQuerySpecificationWrapper.Key_Id,
				GenericQuerySpecificationWrapper.Key_IdList,
				GenericQuerySpecificationWrapper.Key_CreatedAfter,
				GenericQuerySpecificationWrapper.Key_CreatedBefore,
				LogQuerySpecification.Key_Severity,
				LogQuerySpecification.Key_Logger,
				LogQuerySpecification.Key_UserIdentity,
				LogQuerySpecification.Key_DateAfter,
				LogQuerySpecification.Key_DateBefore,
				LogQuerySpecification.Key_UtcDateAfter,
				LogQuerySpecification.Key_UtcDateBefore,
				LogQuerySpecification.Key_WebSessionId,
				LogQuerySpecification.Key_UserName,
				LogQuerySpecification.Key_UserWebIdentity,
				LogQuerySpecification.Key_ProcessUser,
				LogQuerySpecification.Key_Location,
				LogQuerySpecification.Key_MachineName,
				LogQuerySpecification.Key_OnlyExceptions,
			};

		#endregion

		internal static SqlQuery CreateQueryHelper(LogQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			//Since the Log table doesn't have CreatedOn but CreatedAt, we have to clear the CreatedAfter & CreatedBefore conditions before we call the AddStandardQueryCondition_AndWhereIsEqualTo method.
			DateTime? createdAfter = qs.CreatedAfter;
			DateTime? createdBefore = qs.CreatedBefore;
			qs.CreatedAfter = null;
			qs.CreatedBefore = null;

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			//Restore those two conditions and add them to the query.
			qs.CreatedAfter = createdAfter;
			qs.CreatedBefore = createdBefore;
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsGreaterThanOrEqualTo(query, "CreatedAt", qs.CreatedAfter);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsLessThan(query, "CreatedAt", qs.CreatedBefore);

			if (qs.Severity != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Severity", ((HPFx.Diagnostics.Logging.Severity)qs.Severity.Value).ToString());
			}
			if (qs.MaxSeverity != null)
			{
				IEnumerable<Severity> severities = ((IList<Severity>)Enum.GetValues(typeof(Severity))).Where(s => ((int)s) <= qs.MaxSeverity.Value);
				List<string> values = severities.Select(s => ((HPFx.Diagnostics.Logging.Severity)s).ToString()).ToList();
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualToAny(query, "Severity", values);
			}
			if (qs.MinSeverity != null)
			{
				IEnumerable<Severity> severities = ((IList<Severity>)Enum.GetValues(typeof(Severity))).Where(s => ((int)s) >= qs.MinSeverity.Value);
				List<string> values = severities.Select(s => ((HPFx.Diagnostics.Logging.Severity)s).ToString()).ToList();
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualToAny(query, "Severity", values);
			}

			if (qs.OnlyExceptions!= null)
			{
				//TODO: Review Needed: Look at changing the Exception column to contain true NULL instead of empty string. May have performance implications.
				if (qs.OnlyExceptions.Value)
				{
					query.AndExpression("Exception").IsNotNull().And("Exception").IsNotEqualTo("");
				}
				else
				{
					query.AndExpression("Exception").IsNull().Or("Exception").IsEqualTo("");
				}
			}

			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Logger", qs.Logger);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "UserIdentity", qs.UserIdentity);

			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsGreaterThanOrEqualTo(query, "Date", qs.DateAfter);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsLessThan(query, "Date", qs.DateBefore);

			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsGreaterThanOrEqualTo(query, "UtcDate", qs.UtcDateAfter);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsLessThan(query, "UtcDate", qs.UtcDateBefore);

			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WebSessionId", qs.WebSessionId);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "UserName", qs.UserName, true);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "UserWebIdentity", qs.UserWebIdentity, true);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "ProcessUser", qs.ProcessUser, true);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "Location", qs.Location, true);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "MachineName", qs.MachineName, true);


			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}
			
			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(LogController));
			return query;
		}


		public static LogCollection Fetch(LogQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<LogCollection>();
		}

		public static int FetchCount(LogQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		private static SqlQuery CreateQuery(LogQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
#warning Not Implemented: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
			//ElementsCPSDataUtility.ValidateQuerySpecificationConditions(qs, _ValidQueryConditionKeys);
			return LogController.CreateQueryHelper(qs, Log.Schema, isCountQuery);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LogCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			LogQuerySpecification qs = new LogQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			LogQuerySpecification qs = new LogQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}
	}
}
