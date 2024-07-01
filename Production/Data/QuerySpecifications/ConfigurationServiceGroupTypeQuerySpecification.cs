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
    public class ConfigurationServiceGroupTypeQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

        public const string Key_ConfigurationServiceApplicationId = "ConfigurationServiceGroupApplicationId";

        #endregion

        #region Constructors

        public ConfigurationServiceGroupTypeQuerySpecification()
            : base()
        {
        }

        public ConfigurationServiceGroupTypeQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

		public ConfigurationServiceGroupTypeQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

        [XmlIgnore]
        public int? ConfigurationServiceApplicationId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceApplicationId); }
            set { this.Conditions.SetCondition(Key_ConfigurationServiceApplicationId, value); }
        }

        #endregion

        #endregion

        #endregion

    }
}