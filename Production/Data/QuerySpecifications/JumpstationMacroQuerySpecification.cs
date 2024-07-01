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
    public class JumpstationMacroQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_MatchName = "MatchName";
		public const string Key_JumpstationMacroStatusId = "JumpstationMacroStatusId";
		public const string Key_ValidationId = "ValidationId";
		public const string Key_ProductionId = "ProductionId";
		public const string Key_OwnerId = "OwnerId";
 
        #endregion

        #region Constructors

        public JumpstationMacroQuerySpecification()
            : base()
        {
        }

        public JumpstationMacroQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public JumpstationMacroQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

		[XmlIgnore]
		public int? JumpstationMacroStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_JumpstationMacroStatusId); }
			set { this.Conditions.SetCondition(Key_JumpstationMacroStatusId, value); }
		}

		[XmlIgnore]
		public int? OwnerId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_OwnerId); }
			set { this.Conditions.SetCondition(Key_OwnerId, value); }
		}

		[XmlIgnore]
		public int? ValidationId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ValidationId); }
			set { this.Conditions.SetCondition(Key_ValidationId, value); }
		}

		[XmlIgnore]
		public int? ProductionId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ProductionId); }
			set { this.Conditions.SetCondition(Key_ProductionId, value); }
		}

		[XmlIgnore]
		public string MatchName
		{
			get { return this.Conditions.GetConditionAsString(Key_MatchName); }
			set { this.Conditions.SetCondition(Key_MatchName, value); }
		}
        #endregion

        #endregion

    }
}