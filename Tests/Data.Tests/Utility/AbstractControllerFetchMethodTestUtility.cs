using System;
using System.Collections.Generic;
using HP.HPFx.Data.Query;
using MbUnit.Framework;
using SubSonic;
using HP.ElementsCPS.Data.SubSonicClient;
using System.Collections;
using System.Text;
using System.Globalization;
using System.Linq;

namespace HP.ElementsCPS.Data.Tests.Utility
{
	public abstract class AbstractControllerFetchMethodTestUtility
	{
		#region Public Members

		public enum ParamType { General, DateTime, Decimal }
		public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";

		public delegate IList<ResultItem> QueryMethod<QuerySpecType, ResultItem>(QuerySpecType qs)
			where QuerySpecType : IQuerySpecification, new()
			where ResultItem : RecordBase<ResultItem>, new()
			;


		public virtual void Test_QueryMethod_With_QuerySpecParams<QuerySpecType, ResultItemType, PKType>(ICollection<PKType> expectedPKs, string PKName, QueryMethod<QuerySpecType, ResultItemType> queryMethod, params object[] querySpecParameters)
			where QuerySpecType : QuerySpecificationWrapper, new()
			where ResultItemType : RecordBase<ResultItemType>, new()
		{
			Test_QueryMethod_With_QuerySpecParams(expectedPKs, new string[] { PKName }, queryMethod, querySpecParameters);
		}

		public virtual void Test_QueryMethod_With_QuerySpecParams<QuerySpecType, ResultItemType, PKType>(ICollection<PKType> expectedPKs, IList<string> PKNames, QueryMethod<QuerySpecType, ResultItemType> queryMethod, params object[] querySpecParameters)
			where QuerySpecType : QuerySpecificationWrapper, new()
			where ResultItemType : RecordBase<ResultItemType>, new()
		{
			QuerySpecType qs = Test_CreateQuerySpec<QuerySpecType>(querySpecParameters);

			Assert_QueryMethodReturnsExpectedPKsOnly<QuerySpecType, ResultItemType, PKType>(expectedPKs, PKNames, queryMethod, qs);
		}

		#endregion
		
		#region Protected Members

		protected virtual QuerySpecType Test_CreateQuerySpec<QuerySpecType>(params object[] querySpecParameters)
			where QuerySpecType : QuerySpecificationWrapper, new()
		{
			Assert.IsNotNull(querySpecParameters, "Parameter array is null.");
			Assert.IsTrue((querySpecParameters.Length % 3 == 0), "Parameter number invalid. {0} mod 3 != 0", querySpecParameters.Length);

			QuerySpecType qs = new QuerySpecType();
			for (int i = 0; i <= querySpecParameters.Length - 2; i += 3)
			{

				Assert.IsInstanceOfType(typeof(string), querySpecParameters[i], "Type Invalid. Parameter Key must be string! Parameter index: {0}, key value: {1}", i, querySpecParameters[i]);
				Assert.DoesNotContain(qs.Conditions.Keys, (string)querySpecParameters[i], string.Format("Paramter Key \"{0}\" has already been added to the query spec! Parameter index: {1}", querySpecParameters[i], i));
				string paramKey = (string)querySpecParameters[i];

				Assert.IsNotNull(querySpecParameters[i + 1], "Parameter value must not be null! Parameter Key: {0}", paramKey);
				object paramValue = querySpecParameters[i + 1];

				Assert.IsInstanceOfType(typeof(ParamType), querySpecParameters[i + 2], "Type Invalid. Parameter Type must be ParamType! Parameter Key: {0}", paramKey);
				ParamType paramType = (ParamType)querySpecParameters[i + 2];

				Test_AddCondition<QuerySpecType>(qs, paramKey, paramValue, paramType);
			}
			return qs;
		}

		protected virtual void Test_AddCondition<QuerySpecType>(QuerySpecType qs, string paramKey, object paramValue, ParamType paramType)
			where QuerySpecType : QuerySpecificationWrapper, new()
		{
			switch (paramType)
			{
				case ParamType.DateTime:
					qs.Conditions.Add(paramKey, DateTime.ParseExact(paramValue as string, DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None));
					break;
				case ParamType.Decimal:
					qs.Conditions.Add(paramKey, Decimal.Parse(paramValue as string, NumberStyles.Any, CultureInfo.InvariantCulture));
					break;
				case ParamType.General:
					qs.Conditions.Add(paramKey, paramValue);
					break;
			}
		}

		protected virtual void Assert_QueryMethodReturnsExpectedPKsOnly<QuerySpecType, ResultItemType, PKType>(ICollection<PKType> expectedResultPKs, IList<string> PKNames, QueryMethod<QuerySpecType, ResultItemType> queryMethod, QuerySpecType qs)
			where QuerySpecType : QuerySpecificationWrapper, new()
			where ResultItemType : RecordBase<ResultItemType>, new()
		{
			IList<ResultItemType> results = queryMethod(qs);
			string qsXml = qs.ToXml();

			IList<PKType> resultPKs = GetPKCollection<ResultItemType, PKType>(results, PKNames);

			Assert.AreEqual(expectedResultPKs.Count, resultPKs.Count, "Expected query PK count: {0} != Actual query PK count: {1}.  QuerySpecXML:{2}", expectedResultPKs.Count, results.Count, qsXml);

			foreach (PKType pk in resultPKs)
			{
				Assert.IsTrue(expectedResultPKs.Contains(pk), "ID {0} is not supposed to be in the results. QuerySpecXML:{1}", pk.ToString(), qsXml);
			}
		}

		protected abstract PKType GetPKValue<ResultItemType, PKType>(ResultItemType item, IList<string> PKNames)
			where ResultItemType : RecordBase<ResultItemType>, new();

		protected virtual IList<PKType> GetPKCollection<ResultItemType, PKType>(IEnumerable<ResultItemType> items, IList<string> PKNames)
			where ResultItemType : RecordBase<ResultItemType>, new()
		{
			List<PKType> resultPKs = new List<PKType>();
			foreach (ResultItemType item in items)
			{
				PKType pk = GetPKValue<ResultItemType, PKType>(item, PKNames);
				if (!resultPKs.Contains(pk))
				{
					resultPKs.Add(pk);
				}
			}
			return resultPKs;
		}
		#endregion

	}
}
