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
    public class WorkflowSelectorQueryParameterValueQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_QueryParameterId = "QueryParameterId";
		public const string Key_WorkflowSelectorId = "WorkflowSelectorId";
       
        #endregion

        #region Constructors

        public WorkflowSelectorQueryParameterValueQuerySpecification()
            : base()
        {
        }

        public WorkflowSelectorQueryParameterValueQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public WorkflowSelectorQueryParameterValueQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? QueryParameterId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_QueryParameterId); }
			set { this.Conditions.SetCondition(Key_QueryParameterId, value); }
		}

		[XmlIgnore]
		public int? WorkflowSelectorId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowSelectorId); }
			set { this.Conditions.SetCondition(Key_WorkflowSelectorId, value); }
		}
        
        #endregion

        #endregion

        #endregion

    }
}