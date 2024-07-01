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
    public class ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_QueryParameterId = "QueryParameterId";
		public const string Key_ConfigurationServiceGroupSelectorId = "ConfigurationServiceGroupSelectorId";
       
        #endregion

        #region Constructors

        public ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification()
            : base()
        {
        }

        public ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification(IQuerySpecification original)
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
		public int? ConfigurationServiceGroupSelectorId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceGroupSelectorId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupSelectorId, value); }
		}
        
        #endregion

        #endregion

        #endregion

    }
}