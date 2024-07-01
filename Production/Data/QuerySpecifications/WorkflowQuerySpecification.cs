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
    public class WorkflowQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

        public const string Key_WorkflowApplicationId = "WorkflowApplicationId";
       
		public const string Key_WorkflowStatusId = "WorkflowStatusId";
		public const string Key_WorkflowTypeId = "WorkflowTypeId";
		public const string Key_ReleaseQueryParameterValue = "ReleaseQueryParameterValue";
		public const string Key_CountryQueryParameterValue = "CountryQueryParameterValue";
		public const string Key_BrandQueryParameterValue = "BrandQueryParameterValue";
		public const string Key_PlatformQueryParameterValue = "PlatformQueryParameterValue";
		public const string Key_SubBrandQueryParameterValue = "SubBrandQueryParameterValue";
		public const string Key_ModelNumberQueryParameterValue = "ModelNumberQueryParameterValue";
        public const string Key_CompatibleWithWorkflowModuleId = "CompatibleWithWorkflowModuleId";
		public const string Key_ValidationId = "ValidationId";
        public const string Key_AppClientId = "AppClientId";
		public const string Key_ProductionId = "ProductionId";
        public const string Key_Filename = "Filename";
        public const string Key_AppClientIds = "AppClientIds";
        #endregion

        #region Constructors

        public WorkflowQuerySpecification()
            : base()
        {
        }

        public WorkflowQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public WorkflowQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        [XmlIgnore]
        public int? WorkflowApplicationId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_WorkflowApplicationId); }
            set { this.Conditions.SetCondition(Key_WorkflowApplicationId, value); }
        }

		
		[XmlIgnore]
		public int? WorkflowStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowStatusId); }
			set { this.Conditions.SetCondition(Key_WorkflowStatusId, value); }
		}

		[XmlIgnore]
		public int? WorkflowTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowTypeId); }
			set { this.Conditions.SetCondition(Key_WorkflowTypeId, value); }
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
		public string SubBrandQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_SubBrandQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_SubBrandQueryParameterValue, value); }
		}

		[XmlIgnore]
		public string ModelNumberQueryParameterValue
		{
			get { return this.Conditions.GetConditionAsString(Key_ModelNumberQueryParameterValue); }
			set { this.Conditions.SetCondition(Key_ModelNumberQueryParameterValue, value); }
		}

        [XmlIgnore]
        public int? CompatibleWithWorkflowModuleId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_CompatibleWithWorkflowModuleId); }
            set { this.Conditions.SetCondition(Key_CompatibleWithWorkflowModuleId, value); }
        }

        [XmlIgnore]
        public string Filename
        {
            get { return this.Conditions.GetConditionAsString(Key_Filename); }
            set { this.Conditions.SetCondition(Key_Filename, value); }
        }

        [XmlIgnore]
        public int? AppClientId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_AppClientId); }
            set { this.Conditions.SetCondition(Key_AppClientId, value); }
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