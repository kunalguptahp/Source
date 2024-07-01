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
    public class QueryParameterQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_ProxyURLTypeId = "ProxyURLTypeId";
		public const string Key_ConfigurationServiceGroupTypeId = "ConfigurationServiceGroupTypeId";
		public const string Key_WorkflowTypeId = "WorkflowTypeId";

        #endregion

        #region Constructors

        public QueryParameterQuerySpecification()
            : base()
        {
        }

        public QueryParameterQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public QueryParameterQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

		[XmlIgnore]
		public int? ProxyURLTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ProxyURLTypeId); }
			set { this.Conditions.SetCondition(Key_ProxyURLTypeId, value); }
		}

		[XmlIgnore]
		public int? ConfigurationServiceGroupTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ConfigurationServiceGroupTypeId); }
			set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupTypeId, value); }
		}

		[XmlIgnore]
		public int? WorkflowTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_WorkflowTypeId); }
			set { this.Conditions.SetCondition(Key_WorkflowTypeId, value); }
		}
		
		#region Conditions related to Entity-specific Fields

    	//public int? CompatibleWithProxyUrlId { get; set; }
                
        #endregion

        #endregion

        #endregion

    }
}