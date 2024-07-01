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
    public class QueryParameterConfigurationServiceGroupTypeQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_ConfigurationServiceGroupTypeId = "ConfigurationServiceGroupTypeId";
		public const string Key_ConfigurationServiceGroupId = "ConfigurationServiceGroupId";
        public const string Key_Wildcard = "Wildcard";
        public const string Key_RowStatusId = "RowStatusId";
        public const string Key_QueryParameterName = "QueryParameterName";
        #endregion

        #region Constructors

        public QueryParameterConfigurationServiceGroupTypeQuerySpecification()
            : base()
        {
        }

        public QueryParameterConfigurationServiceGroupTypeQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public QueryParameterConfigurationServiceGroupTypeQuerySpecification(IQuerySpecification original)
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
		public int? ConfigurationServiceGroupId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceGroupId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupId, value); }
		}

        [XmlIgnore]
        public bool? Wildcard
        {
            get { return this.Conditions.GetConditionAsBoolean(Key_Wildcard); }
            set { this.Conditions.SetCondition(Key_Wildcard, value); }
        }

		[XmlIgnore]
		public int? RowStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_RowStatusId); }
			set { this.Conditions.SetCondition(Key_RowStatusId, value); }
		}

        [XmlIgnore]
        public string QueryParameterName
        {
            get { return this.Conditions.GetConditionAsString(Key_QueryParameterName); }
            set { this.Conditions.SetCondition(Key_QueryParameterName, value); }
        }
        #endregion

        #endregion

        #endregion

    }
}