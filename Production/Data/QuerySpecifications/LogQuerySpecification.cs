using System;
using System.Xml.Serialization;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Data.QuerySpecifications
{
	/// <summary>
	/// An entity-specific "query specification" implementation that exposes a variety of members corresponding to 
	/// the various query options supported by the entity's corresponding DAL.
	/// </summary>
	[Serializable]
	[XmlRoot(ElementName = "Query")]
	public class LogQuerySpecification : GenericQuerySpecificationWrapper
	{

		#region Constants

		public const string Key_Severity = "Severity";
		public const string Key_MaxSeverity = "MaxSeverity";
		public const string Key_MinSeverity = "MinSeverity";

		public const string Key_UserIdentity = "UserIdentity";

		public const string Key_Logger = "Logger";

		public const string Key_DateAfter = "DateAfrer";

		public const string Key_DateBefore = "DateBefore";

		public const string Key_UtcDateAfter = "UtcDateAfrer";

		public const string Key_UtcDateBefore = "UtcDateBefore";

		public const string Key_WebSessionId = "WebSessionId";

		public const string Key_UserName = "UserName";

		public const string Key_UserWebIdentity = "UserWebIdentity";

		public const string Key_ProcessUser = "ProcessUser";

		public const string Key_Location = "Location";

		public const string Key_MachineName = "MachineName";

		public const string Key_OnlyExceptions = "OnlyExceptions";


		#endregion

		#region Constructors

		public LogQuerySpecification()
			: base()
		{
		}

		public LogQuerySpecification(QuerySpecification innerQuery)
			: base(innerQuery)
		{
		}

		public LogQuerySpecification(IQuerySpecification original)
			: base(original)
		{
		}

		#endregion

		#region Properties

		#region Query Conditions Properties (Strongly-typed convenience/utility properties)

		#region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? Severity
		{
			get { return this.Conditions.GetConditionAsInt32(Key_Severity); }
			set { this.Conditions.SetCondition(Key_Severity, value); }
		}

		[XmlIgnore]
		public int? MaxSeverity
		{
			get { return this.Conditions.GetConditionAsInt32(Key_MaxSeverity); }
			set { this.Conditions.SetCondition(Key_MaxSeverity, value); }
		}

		[XmlIgnore]
		public int? MinSeverity
		{
			get { return this.Conditions.GetConditionAsInt32(Key_MinSeverity); }
			set { this.Conditions.SetCondition(Key_MinSeverity, value); }
		}

		[XmlIgnore]
		public string UserIdentity
		{
			get { return this.Conditions.GetConditionAsString(Key_UserIdentity); }
			set { this.Conditions.SetCondition(Key_UserIdentity, value); }
		}

		[XmlIgnore]
		public string Logger
		{
			get { return this.Conditions.GetConditionAsString(Key_Logger); }
			set { this.Conditions.SetCondition(Key_Logger, value); }
		}

		[XmlIgnore]
		public DateTime? DateAfter
		{
			get { return this.Conditions.GetConditionAsDateTime(Key_DateAfter); }
			set { this.Conditions.SetCondition(Key_DateAfter, value); }
		}

		[XmlIgnore]
		public DateTime? DateBefore
		{
			get { return this.Conditions.GetConditionAsDateTime(Key_DateBefore); }
			set { this.Conditions.SetCondition(Key_DateBefore, value); }
		}

		[XmlIgnore]
		public DateTime? UtcDateAfter
		{
			get { return this.Conditions.GetConditionAsDateTime(Key_UtcDateAfter); }
			set { this.Conditions.SetCondition(Key_UtcDateAfter, value); }
		}

		[XmlIgnore]
		public DateTime? UtcDateBefore
		{
			get { return this.Conditions.GetConditionAsDateTime(Key_UtcDateBefore); }
			set { this.Conditions.SetCondition(Key_UtcDateBefore, value); }
		}

		[XmlIgnore]
		public string WebSessionId
		{
			get { return this.Conditions.GetConditionAsString(Key_WebSessionId); }
			set { this.Conditions.SetCondition(Key_WebSessionId, value); }
		}

		[XmlIgnore]
		public string UserName
		{
			get { return this.Conditions.GetConditionAsString(Key_UserName); }
			set { this.Conditions.SetCondition(Key_UserName, value); }
		}

		[XmlIgnore]
		public string UserWebIdentity
		{
			get { return this.Conditions.GetConditionAsString(Key_UserWebIdentity); }
			set { this.Conditions.SetCondition(Key_UserWebIdentity, value); }
		}

		[XmlIgnore]
		public string ProcessUser
		{
			get { return this.Conditions.GetConditionAsString(Key_ProcessUser); }
			set { this.Conditions.SetCondition(Key_ProcessUser, value); }
		}

		[XmlIgnore]
		public string Location
		{
			get { return this.Conditions.GetConditionAsString(Key_Location); }
			set { this.Conditions.SetCondition(Key_Location, value); }
		}

		[XmlIgnore]
		public string MachineName
		{
			get { return this.Conditions.GetConditionAsString(Key_MachineName); }
			set { this.Conditions.SetCondition(Key_MachineName, value); }
		}

		public bool? OnlyExceptions
		{
			get { return this.Conditions.GetConditionAsBoolean(Key_OnlyExceptions); }
			set { this.Conditions.SetCondition(Key_OnlyExceptions, value); }
		}

		#endregion

		#endregion

		#endregion

	}
}