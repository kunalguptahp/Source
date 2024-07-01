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
    public class ConfigurationServiceLabelValueQuerySpecification : GenericQuerySpecificationWrapper
    {
        #region Constants

        public const string Key_ConfigurationServiceLabelId = "ConfigurationServiceLabelId";
		public const string Key_ConfigurationServiceGroupId = "ConfigurationServiceGroupId";

		#endregion

        #region Constructors

        public ConfigurationServiceLabelValueQuerySpecification()
            : base()
        {
        }

        public ConfigurationServiceLabelValueQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

		public ConfigurationServiceLabelValueQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? ConfigurationServiceLabelId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceLabelId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceLabelId, value); }
		}

		[XmlIgnore]
		public int? ConfigurationServiceGroupId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceGroupId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupId, value); }
		}

		#endregion

        #endregion

        #endregion

    }
}