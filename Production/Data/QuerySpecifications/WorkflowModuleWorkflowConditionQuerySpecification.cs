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
    public class WorkflowModuleWorkflowConditionQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_WorkflowModuleId = "WorkflowModuleId";
		public const string Key_WorkflowConditionId = "WorkflowConditionId";
       
        #endregion

        #region Constructors

        public WorkflowModuleWorkflowConditionQuerySpecification()
            : base()
        {
        }

        public WorkflowModuleWorkflowConditionQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public WorkflowModuleWorkflowConditionQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? WorkflowModuleId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowModuleId); }
			set { this.Conditions.SetCondition(Key_WorkflowModuleId, value); }
		}

		[XmlIgnore]
		public int? WorkflowConditionId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowConditionId); }
			set { this.Conditions.SetCondition(Key_WorkflowConditionId, value); }
		}
        
        #endregion

        #endregion

        #endregion

    }
}