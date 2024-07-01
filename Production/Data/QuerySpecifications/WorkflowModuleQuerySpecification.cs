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
    public class WorkflowModuleQuerySpecification : GenericQuerySpecificationWrapper
    {
        #region Constants

		public const string Key_WorkflowModuleStatusId = "WorkflowModuleStatusId";
		public const string Key_ModuleCategoryId = "ModuleCategoryId";
		public const string Key_ModuleSubCategoryId = "ModuleSubCategoryId";
		public const string Key_VersionMajor = "VersionMajor";
		public const string Key_VersionMinor = "VersionMinor";
		public const string Key_ValidationId = "ValidationId";
		public const string Key_ProductionId = "ProductionId";
        public const string Key_Filename = "Filename";

        #endregion

        #region Constructors

        public WorkflowModuleQuerySpecification()
            : base()
        {
        }

        public WorkflowModuleQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

		public WorkflowModuleQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

		[XmlIgnore]
		public int? WorkflowModuleStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowModuleStatusId); }
			set { this.Conditions.SetCondition(Key_WorkflowModuleStatusId, value); }
		}

		[XmlIgnore]
		public int? ModuleCategoryId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ModuleCategoryId); }
			set { this.Conditions.SetCondition(Key_ModuleCategoryId, value); }
		}

		[XmlIgnore]
		public int? ModuleSubCategoryId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ModuleSubCategoryId); }
			set { this.Conditions.SetCondition(Key_ModuleSubCategoryId, value); }
		}

		[XmlIgnore]
		public int? VersionMajor
		{
			get { return this.Conditions.GetConditionAsInt32(Key_VersionMajor); }
			set { this.Conditions.SetCondition(Key_VersionMajor, value); }
		}

		[XmlIgnore]
		public int? VersionMinor
		{
			get { return this.Conditions.GetConditionAsInt32(Key_VersionMinor); }
			set { this.Conditions.SetCondition(Key_VersionMinor, value); }
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
        public string Filename
        {
            get { return this.Conditions.GetConditionAsString(Key_Filename); }
            set { this.Conditions.SetCondition(Key_Filename, value); }
        }

        #endregion

        #endregion

    }
}