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
    public class WorkflowWorkflowModuleQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_WorkflowId = "WorkflowId";
		public const string Key_WorkflowModuleId = "WorkflowModuleId";
       
        #endregion

        #region Constructors

        public WorkflowWorkflowModuleQuerySpecification()
            : base()
        {
        }

        public WorkflowWorkflowModuleQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public WorkflowWorkflowModuleQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? WorkflowId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowId); }
			set { this.Conditions.SetCondition(Key_WorkflowId, value); }
		}

		[XmlIgnore]
		public int? WorkflowModuleId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowModuleId); }
			set { this.Conditions.SetCondition(Key_WorkflowModuleId, value); }
		}
        
        #endregion

        #endregion

        #endregion

    }
}