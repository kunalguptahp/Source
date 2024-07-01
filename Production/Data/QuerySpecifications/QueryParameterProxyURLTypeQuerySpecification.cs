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
    public class QueryParameterProxyURLTypeQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_ProxyURLTypeId = "ProxyURLTypeId";
       
        #endregion

        #region Constructors

        public QueryParameterProxyURLTypeQuerySpecification()
            : base()
        {
        }

        public QueryParameterProxyURLTypeQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public QueryParameterProxyURLTypeQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? ProxyURLTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_ProxyURLTypeId); }
			set { this.Conditions.SetCondition(Key_ProxyURLTypeId, value); }
		}
            
        #endregion

        #endregion

        #endregion

    }
}