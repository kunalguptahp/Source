using System;
using System.Collections.Generic;
using HP.HPFx.Data.Query;
using MbUnit.Framework;
using SubSonic;
using HP.ElementsCPS.Data.SubSonicClient;
using System.Collections;
using System.Text;
using System.Globalization;

namespace HP.ElementsCPS.Data.Tests.Utility
{
	public class SinglePKControllerFetchMethodTestUtility : AbstractControllerFetchMethodTestUtility
	{
		public override void Test_QueryMethod_With_QuerySpecParams<QuerySpecType, ResultItemType, PKType>(ICollection<PKType> expectedPKs, IList<string> PKNames, QueryMethod<QuerySpecType, ResultItemType> queryMethod, params object[] querySpecParameters)
		{
			CheckPKNames(PKNames);
			base.Test_QueryMethod_With_QuerySpecParams<QuerySpecType, ResultItemType, PKType>(expectedPKs, PKNames, queryMethod, querySpecParameters);
		}

		protected override PKType GetPKValue<ResultItemType, PKType>(ResultItemType item, IList<string> PKNames)
		{
			CheckPKNames(PKNames);
			return item.GetColumnValue<PKType>(PKNames[0]);
		}

		protected virtual void CheckPKNames(IList<string> PKNames)
		{
			if (PKNames.Count >= 2)
			{
				throw new ArgumentException(string.Format("Too many PK columns! Count = {0}!", PKNames.Count));
			}
		}	
	}
}
