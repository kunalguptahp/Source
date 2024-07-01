using System;
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
	/// The non-generated portion of the ProductFamilyController class.
	/// </summary>
	public partial class NoteTypeController
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
		public static NoteTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
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

		public static NoteTypeCollection Fetch(NoteTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<NoteTypeCollection>();
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
			return NoteTypeController.CreateQueryHelper(qs, NoteType.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(NoteTypeQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(NoteTypeController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(NoteTypeQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapNoteTypeController.Fetch(qs);
			}
			else
			{
				return NoteTypeController.Fetch(qs);
			}
		}

		#endregion

		#endregion

		#region NoteTypeIdentifier Enum Helper Methods

		/// <summary>
		/// Safely converts a <see cref="Nullable{Int32}"/> value into an equivelent <see cref="NoteTypeIdentifier"/> value.
		/// </summary>
		public static NoteTypeIdentifier ConvertNoteTypeId(int? value)
		{
			const int nullValue = (int)SubSonicClient.NoteTypeIdentifier.None; //(int)default(NoteTypeIdentifier);
			return (NoteTypeIdentifier)(value ?? nullValue);
		}

		/// <summary>
		/// Safely converts a <see cref="NoteTypeIdentifier"/> value into an equivelent <see cref="Nullable{Int32}"/> value.
		/// </summary>
		public static int? ConvertNoteTypeId(NoteTypeIdentifier value)
		{
			const NoteTypeIdentifier nullValue = SubSonicClient.NoteTypeIdentifier.None; //default(NoteTypeIdentifier);
			//ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "NoteType", typeof(NoteTypeIdentifier));
			return (value == nullValue) ? (int?)null : (int)value;
		}

		#endregion

	}
}
