using System;
using System.Xml.Serialization;
using HP.HPFx.Data.Query;
using System.Collections.Generic;

namespace HP.ElementsCPS.Data.QuerySpecifications
{

    /// <summary>
    /// An entity-specific "query specification" implementation that exposes a variety of members corresponding to 
    /// the various query options supported by the entity's corresponding DAL.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Query")]
    public class ConfigurationServiceGroupQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_ConfigurationServiceGroupDescription = "ConfigurationServiceGroupDescription";
		public const string Key_ConfigurationServiceApplicationId = "ConfigurationServiceApplicationId";
		public const string Key_ConfigurationServiceGroupStatusId = "ConfigurationServiceGroupStatusId";
		public const string Key_ConfigurationServiceGroupTypeId = "ConfigurationServiceGroupTypeId";
		public const string Key_ReleaseQueryParameterValue = "ReleaseQueryParameterValue";
		public const string Key_CountryQueryParameterValue = "CountryQueryParameterValue";
		public const string Key_BrandQueryParameterValue = "BrandQueryParameterValue";
		public const string Key_PlatformQueryParameterValue = "PlatformQueryParameterValue";
		public const string Key_PublisherLabelValue = "PublisherLabelValue";
		public const string Key_InstallerTypeLabelValue = "InstallerTypeLabelValue";
		public const string Key_ValidationId = "ValidationId";
		public const string Key_ProductionId = "ProductionId";
		public const string Key_OwnerId = "OwnerId";
        public const string Key_AppClientId = "AppClientId";
    	public const string Key_Tags = "Tags";
        public const string Key_AppClientIds = "AppClientIds";
        #endregion

        #region Constructors

        public ConfigurationServiceGroupQuerySpecification()
            : base()
        {
        }

        public ConfigurationServiceGroupQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public ConfigurationServiceGroupQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

		[XmlIgnore]
		public int? ConfigurationServiceApplicationId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceApplicationId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceApplicationId, value); }
		}

		[XmlIgnore]
		public int? ConfigurationServiceGroupStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceGroupStatusId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupStatusId, value); }
		}

		[XmlIgnore]
		public int? ConfigurationServiceGroupTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceGroupTypeId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupTypeId, value); }
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
		public string ReleaseQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_ReleaseQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_ReleaseQueryParameterValue, value); }
		}

		[XmlIgnore]
		public string CountryQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_CountryQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_CountryQueryParameterValue, value); }
		}

		[XmlIgnore]
		public string PlatformQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_PlatformQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_PlatformQueryParameterValue, value); }
		}

		[XmlIgnore]
		public string BrandQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_BrandQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_BrandQueryParameterValue, value); }
		}

        [XmlIgnore]
        public int? AppClientId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_AppClientId); }
            set { this.Conditions.SetCondition(Key_AppClientId, value); }
        }

		[XmlIgnore]
		public string Tags
		{
			get { return this.Conditions.GetConditionAsString(Key_Tags); }
			set { this.Conditions.SetCondition(Key_Tags, value); }
		}

		[XmlIgnore]
		public string PublisherLabelValue
		{
			get { return this.Conditions.GetConditionAsString(Key_PublisherLabelValue); }
			set { this.Conditions.SetCondition(Key_PublisherLabelValue, value); }
		}

		[XmlIgnore]
		public string InstallerTypeLabelValue
		{
			get { return this.Conditions.GetConditionAsString(Key_InstallerTypeLabelValue); }
			set { this.Conditions.SetCondition(Key_InstallerTypeLabelValue, value); }
		}

        [XmlIgnore]
        public List<int> AppClientIds
        {
            get { return this.Conditions.GetConditionAsListOfInt32(Key_AppClientIds); }
            set { this.Conditions.SetCondition(Key_AppClientIds, value); }
        }
        #endregion

        #endregion

    }
}