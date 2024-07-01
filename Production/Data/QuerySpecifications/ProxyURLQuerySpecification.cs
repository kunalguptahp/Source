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
    public class ProxyURLQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_ProxyURL = "ProxyURL";
		public const string Key_ProxyURLStatusId = "ProxyURLStatusId";
		public const string Key_ProxyURLTypeId = "ProxyURLTypeId";
		public const string Key_TouchpointParameterValueQueryParameterValueId = "TouchpointParameterValueQueryParameterValueId";
		public const string Key_LocaleParameterValueQueryParameterValueId = "LocaleParameterValueQueryParameterValueId";
		public const string Key_BrandParameterValueQueryParameterValueId = "BrandParameterValueQueryParameterValueId";
		public const string Key_CycleParameterValueQueryParameterValueId = "CycleParameterValueQueryParameterValueId";
		public const string Key_PlatformParameterValueQueryParameterValueId = "PlatformParameterValueQueryParameterValueId";
		public const string Key_PartnerCategoryParameterValueQueryParameterValueId = "PartnerCategoryParameterValueQueryParameterValueId";
    	public const string Key_ValidationId = "ValidationId";
		public const string Key_ProductionId = "ProductionId";
		public const string Key_OwnerId = "OwnerId";
    	public const string Key_Tags = "Tags";
 
        #endregion

        #region Constructors

        public ProxyURLQuerySpecification()
            : base()
        {
        }

        public ProxyURLQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public ProxyURLQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

		[XmlIgnore]
		public string ProxyURL
		{
			get { return this.Conditions.GetConditionAsString(Key_ProxyURL); }
			set { this.Conditions.SetCondition(Key_ProxyURL, value); }
		}

		[XmlIgnore]
		public int? ProxyURLStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ProxyURLStatusId); }
			set { this.Conditions.SetCondition(Key_ProxyURLStatusId, value); }
		}

		[XmlIgnore]
		public int? ProxyURLTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ProxyURLTypeId); }
			set { this.Conditions.SetCondition(Key_ProxyURLTypeId, value); }
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
		public int? TouchpointParameterValueQueryParameterValueId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_TouchpointParameterValueQueryParameterValueId); }
			set { this.Conditions.SetCondition(Key_TouchpointParameterValueQueryParameterValueId, value); }
		}

		[XmlIgnore]
		public int? BrandParameterValueQueryParameterValueId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_BrandParameterValueQueryParameterValueId); }
			set { this.Conditions.SetCondition(Key_BrandParameterValueQueryParameterValueId, value); }
		}

		[XmlIgnore]
		public int? LocaleParameterValueQueryParameterValueId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_LocaleParameterValueQueryParameterValueId); }
			set { this.Conditions.SetCondition(Key_LocaleParameterValueQueryParameterValueId, value); }
		}

		[XmlIgnore]
		public int? CycleParameterValueQueryParameterValueId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_CycleParameterValueQueryParameterValueId); }
			set { this.Conditions.SetCondition(Key_CycleParameterValueQueryParameterValueId, value); }
		}

		[XmlIgnore]
		public int? PlatformParameterValueQueryParameterValueId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_PlatformParameterValueQueryParameterValueId); }
			set { this.Conditions.SetCondition(Key_PlatformParameterValueQueryParameterValueId, value); }
		}

		[XmlIgnore]
		public int? PartnerCategoryParameterValueQueryParameterValueId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_PartnerCategoryParameterValueQueryParameterValueId); }
			set { this.Conditions.SetCondition(Key_PartnerCategoryParameterValueQueryParameterValueId, value); }
		}

		[XmlIgnore]
		public string Tags
		{
			get { return this.Conditions.GetConditionAsString(Key_Tags); }
			set { this.Conditions.SetCondition(Key_Tags, value); }
		}
		
		#region Conditions related to Entity-specific Fields

		//public int? CompatibleWithQueryParameterId { get; set; }

        #endregion

        #endregion

        #endregion

    }
}