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
    public class JumpstationGroupSelectorQueryParameterValueQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_QueryParameterId = "QueryParameterId";
		public const string Key_JumpstationGroupSelectorId = "JumpstationGroupSelectorId";
       
        #endregion

        #region Constructors

        public JumpstationGroupSelectorQueryParameterValueQuerySpecification()
            : base()
        {
        }

        public JumpstationGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public JumpstationGroupSelectorQueryParameterValueQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? QueryParameterId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_QueryParameterId); }
			set { this.Conditions.SetCondition(Key_QueryParameterId, value); }
		}

		[XmlIgnore]
		public int? JumpstationGroupSelectorId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_JumpstationGroupSelectorId); }
			set { this.Conditions.SetCondition(Key_JumpstationGroupSelectorId, value); }
		}
        
        #endregion

        #endregion

        #endregion

    }
}