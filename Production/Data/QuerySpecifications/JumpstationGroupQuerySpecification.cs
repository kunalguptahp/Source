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
    public class JumpstationGroupQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

        public const string Key_TargetURL = "TargetURL";
		public const string Key_JumpstationGroupDescription = "JumpstationGroupDescription";
		public const string Key_JumpstationApplicationId = "JumpstationApplicationId";
		public const string Key_JumpstationGroupStatusId = "JumpstationGroupStatusId";
		public const string Key_JumpstationGroupTypeId = "JumpstationGroupTypeId";
        public const string Key_TouchpointQueryParameterValue = "TouchpointQueryParameterValue";
        public const string Key_LocaleQueryParameterValue = "LocaleQueryParameterValue";
        public const string Key_BrandQueryParameterValue = "BrandParameterValue";
        public const string Key_CycleQueryParameterValue = "CycleQueryParameterValue";
        public const string Key_PlatformQueryParameterValue = "PlatformQueryParameterValue";
        public const string Key_PartnerCategoryQueryParameterValue = "PartnerCategoryQueryParameterValue";
		public const string Key_ValidationId = "ValidationId";
		public const string Key_ProductionId = "ProductionId";
		public const string Key_OwnerId = "OwnerId";
        public const string Key_AppClientId = "AppClientId";
    	public const string Key_Tags = "Tags";
        public const string Key_QueryParameterValueIdDelimitedList = "QueryParameterValueDelimintedList";
        public const string Key_AppClientIds = "AppClientIds";
        #endregion

        #region Constructors

        public JumpstationGroupQuerySpecification()
            : base()
        {
        }

        public JumpstationGroupQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public JumpstationGroupQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        [XmlIgnore]
        public string TargetURL
        {
            get { return this.Conditions.GetConditionAsString(Key_TargetURL); }
            set { this.Conditions.SetCondition(Key_TargetURL, value); }
        }

		[XmlIgnore]
		public int? JumpstationApplicationId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_JumpstationApplicationId); }
			set { this.Conditions.SetCondition(Key_JumpstationApplicationId, value); }
		}

		[XmlIgnore]
		public int? JumpstationGroupStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_JumpstationGroupStatusId); }
			set { this.Conditions.SetCondition(Key_JumpstationGroupStatusId, value); }
		}

		[XmlIgnore]
		public int? JumpstationGroupTypeId 
		{
			get { return this.Conditions.GetConditionAsInt32(Key_JumpstationGroupTypeId); }
			set { this.Conditions.SetCondition(Key_JumpstationGroupTypeId, value); }
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
		public string TouchpointQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_TouchpointQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_TouchpointQueryParameterValue, value); }
		}

		[XmlIgnore]
		public string LocaleQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_LocaleQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_LocaleQueryParameterValue, value); }
		}

		[XmlIgnore]
		public string BrandQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_BrandQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_BrandQueryParameterValue, value); }
		}

		[XmlIgnore]
		public string CycleQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_CycleQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_CycleQueryParameterValue, value); }
		}

        [XmlIgnore]
        public string PlatformQueryParameterValue
        {
            get { return this.Conditions.GetConditionAsString(Key_PlatformQueryParameterValue); }
            set { this.Conditions.SetCondition(Key_PlatformQueryParameterValue, value); }
        }

        [XmlIgnore]
        public string PartnerCategoryQueryParameterValue
        {
            get { return this.Conditions.GetConditionAsString(Key_PartnerCategoryQueryParameterValue); }
            set { this.Conditions.SetCondition(Key_PartnerCategoryQueryParameterValue, value); }
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
        public string QueryParameterValueIdDelimitedList 
        {
            get { return this.Conditions.GetConditionAsString(Key_QueryParameterValueIdDelimitedList); }
            set { this.Conditions.SetCondition(Key_QueryParameterValueIdDelimitedList, value); }
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