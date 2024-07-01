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
	public class PersonQuerySpecification : GenericQuerySpecificationWrapper
	{

		#region Constants

		public const string Key_Email											= "Email";
		public const string Key_LastName										= "LastName";
		public const string Key_RoleId										    = "RoleId";
        public const string Key_FirstName                                       = "FirstName";
        public const string Key_WindowsId                                       = "WindowsId";
        public const string Key_TenantGroupId = "TenantGroupId";
		#endregion

		#region Constructors

		public PersonQuerySpecification()
			: base()
		{
		}

		public PersonQuerySpecification(QuerySpecification innerQuery)
			: base(innerQuery)
		{
		}

		public PersonQuerySpecification(IQuerySpecification original)
			: base(original)
		{
		}

		#endregion

		#region Properties

		#region Query Conditions Properties (Strongly-typed convenience/utility properties)

		#region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public string Email
		{
			get { return this.Conditions.GetConditionAsString(Key_Email); }
			set { this.Conditions.SetCondition(Key_Email, value); }
		}

		[XmlIgnore]
		public string LastName
		{
			get { return this.Conditions.GetConditionAsString(Key_LastName); }
			set { this.Conditions.SetCondition(Key_LastName, value); }
		}

		[XmlIgnore]
		public int? RoleId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_RoleId); }
			set { this.Conditions.SetCondition(Key_RoleId, value); }
		}

        [XmlIgnore]
        public string FirstName
        {
            get { return this.Conditions.GetConditionAsString(Key_FirstName); }
            set { this.Conditions.SetCondition(Key_FirstName, value); }
        }

        [XmlIgnore]
        public string WindowsId
        {
            get { return this.Conditions.GetConditionAsString(Key_WindowsId); }
            set { this.Conditions.SetCondition(Key_WindowsId, value); }
        }

        [XmlIgnore]
        public int? TenantGroupId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_TenantGroupId); }
            set { this.Conditions.SetCondition(Key_TenantGroupId, value); }
        }

		#endregion

		#endregion

		#endregion

	}
}