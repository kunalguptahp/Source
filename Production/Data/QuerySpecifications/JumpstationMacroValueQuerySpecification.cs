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
    public class JumpstationMacroValueQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_MatchName = "MatchName";
        public const String Key_ResultValue = "ResultValue";
        public const string Key_JumpstationMacroId = "JumpstationMacroId";
        public const string Key_RowStatusId = "RowStatusId";
 
        #endregion

        #region Constructors

        public JumpstationMacroValueQuerySpecification()
            : base()
        {
        }

        public JumpstationMacroValueQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public JumpstationMacroValueQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

		[XmlIgnore]
		public string MatchName
		{
			get { return this.Conditions.GetConditionAsString(Key_MatchName); }
			set { this.Conditions.SetCondition(Key_MatchName, value); }
		}

        [XmlIgnore]
        public string ResultValue
        {
            get { return this.Conditions.GetConditionAsString(Key_ResultValue); }
            set { this.Conditions.SetCondition(Key_ResultValue, value); }
        }

        [XmlIgnore]
        public int? JumpstationMacroId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_JumpstationMacroId); }
            set { this.Conditions.SetCondition(Key_JumpstationMacroId, value); }
        }

        [XmlIgnore]
        public int? RowStatusId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_RowStatusId); }
            set { this.Conditions.SetCondition(Key_RowStatusId, value); }
        }

        #endregion

        #endregion

    }
}