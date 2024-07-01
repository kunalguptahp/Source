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
    public class ConfigurationServiceQueryParameterValueImportQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

        public const string Key_ConfigurationServiceGroupImportId = "ConfigurationServiceGroupImportId";
      
        #endregion

        #region Constructors

        public ConfigurationServiceQueryParameterValueImportQuerySpecification()
            : base()
        {
        }

        public ConfigurationServiceQueryParameterValueImportQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public ConfigurationServiceQueryParameterValueImportQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
        public int? ConfigurationServiceGroupImportId
		{
            get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceGroupImportId); }
            set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupImportId, value); }
		}

        #endregion

        #endregion

        #endregion

    }
}