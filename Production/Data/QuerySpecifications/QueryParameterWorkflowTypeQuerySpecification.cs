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
    public class QueryParameterWorkflowTypeQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_WorkflowTypeId = "WorkflowTypeId";
		public const string Key_WorkflowId = "WorkflowId";
		public const string Key_RowStatusId = "RowStatusId";
       
        #endregion

        #region Constructors

        public QueryParameterWorkflowTypeQuerySpecification()
            : base()
        {
        }

        public QueryParameterWorkflowTypeQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public QueryParameterWorkflowTypeQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? WorkflowTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowTypeId); }
			set { this.Conditions.SetCondition(Key_WorkflowTypeId, value); }
		}

		[XmlIgnore]
		public int? WorkflowId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowId); }
			set { this.Conditions.SetCondition(Key_WorkflowId, value); }
		}

		[XmlIgnore]
		public int? RowStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_RowStatusId); }
			set { this.Conditions.SetCondition(Key_RowStatusId, value); }
		}

        #endregion

        #endregion

        #endregion

    }
}