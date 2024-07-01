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
    public class ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification : GenericQuerySpecificationWrapper
    {
        #region Constants

		public const string Key_ConfigurationServiceGroupTypeId = "ConfigurationServiceGroupTypeId";
		public const string Key_ConfigurationServiceItemId = "ConfigurationServiceItemId";

        #endregion

        #region Constructors

        public ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification()
            : base()
        {
        }

        public ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

		public ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? ConfigurationServiceGroupTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceGroupTypeId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupTypeId, value); }
		}

        [XmlIgnore]
        public int? ConfigurationServiceItemId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceItemId); }
            set { this.Conditions.SetCondition(Key_ConfigurationServiceItemId, value); }
        }

        #endregion

        #endregion

        #endregion

    }
}