using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Transactions;
using HP.ElementsCPS.Core.Net.Mail;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the NoteController class.
	/// </summary>
	public partial class NoteController
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
		public static NoteCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
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

		public static NoteCollection Fetch(NoteQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<NoteCollection>();
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
			return NoteController.CreateQueryHelper(qs, Note.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(NoteQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "EntityId", qs.EntityId);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "NoteTypeId", qs.NoteTypeId);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "EntityTypeId", qs.EntityTypeId);

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(NoteController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(NoteQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapNoteController.Fetch(qs);
			}
			else
			{
				return NoteController.Fetch(qs);
			}
		}

		#endregion

		#endregion

		#region Factory Methods

		public static string TruncateStringLength(string stringValue, int maxLength)
		{
			if (stringValue.Length > maxLength)
			{
				//Truncate the string and append an indication that the string was truncated
				const string truncationSuffix = "...";
				stringValue = stringValue.Substring(0, maxLength - truncationSuffix.Length) + truncationSuffix;
			}
			return stringValue;
		}

		public static Note CreateNew(EntityTypeIdentifier entityTypeId, int entityId, NoteTypeIdentifier noteTypeId, string name, string comment)
		{
			//validate that the entityTypeId is valid, and also not equal to EntityTypeIdentifier.None
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(entityTypeId, "entityTypeId", typeof(EntityTypeIdentifier));
			if (entityTypeId == EntityTypeIdentifier.None)
			{
				throw new ArgumentException("EntityTypeIdentifier.None is an invalid value.", "entityTypeId");
			}

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				Note note = new Note(true);
				note.EntityTypeIdentifierEnumValue = entityTypeId;
				note.NoteTypeIdentifierEnumValue = noteTypeId;
				note.EntityId = entityId;
				note.Comment = comment;
				//note.Name = name;
				note.Name = TruncateStringLength(name, Note.NameColumn.MaxLength);
				note.Save(SecurityManager.CurrentUserIdentityName);

				scope.Complete(); // transaction complete

				return note;
			}
		}

		internal static void ReplicateNoteHelper(EntityTypeIdentifier entityTypeId, int? entityId, NoteTypeIdentifier noteType, string message, string comment)
		{
			if (entityId != null)
			{
				NoteController.CreateNew(entityTypeId, entityId.Value, noteType, message, comment);
			}
		}

		//public static Note CreateNew(EntityTypeIdentifier entityTypeId, int entityId, int noteTypeId, string name, string comment)
		//{
		//   return CreateNew(entityTypeId, entityId, (NoteTypeIdentifier)noteTypeId, name, comment);
		//}

		public static Note CreateNew(EntityTypeIdentifier entityTypeId, int entityId, MailMessage message)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(message, "message");
			string noteComment = MailMessageSender.Format(message);
			string noteName = TruncateStringLength(message.Subject, Note.NameColumn.MaxLength);
			return CreateNew(entityTypeId, entityId, NoteTypeIdentifier.NotificationSent, noteName, noteComment);

		}

		#endregion

		#region GetMessage Methods

		const string MESSAGES_NULL_DISPLAY_STRING = "BLANK";

		#region ToMessageFormat Methods

		public static string ToMessageFormat(object value)
		{
			//NOTE: Comparing the value against null is intentional; and produces the desired result for both ValueType and reference Types
			if (value == null)
			{
				return MESSAGES_NULL_DISPLAY_STRING;
			}
			if (value is Enum)
			{
				const string EnumFormatSpecifier = "G"; //Should this be "F"?
				return Enum.Format(value.GetType(), value, EnumFormatSpecifier);
			}
			return value.ToString();
		}

		public static string ToMessageFormat(Enum value)
		{
			const string EnumFormatSpecifier = "G"; //Should this be "F"?
			return Enum.Format(value.GetType(), value, EnumFormatSpecifier);
		}

		#endregion

		public static string GetMessage_PropertyChanged(string propertyName, object oldValue, object newValue)
		{
			return string.Format("{0} changed from '{1}' to '{2}'.", propertyName, ToMessageFormat(oldValue), ToMessageFormat(newValue));
		}

		public static string GetMessage_StatusChanged(object oldValue, object newValue)
		{
			return string.Format("{0} changed from '{1}' to '{2}'.", "Status", ToMessageFormat(oldValue), ToMessageFormat(newValue));
		}

		#endregion

	}
}
