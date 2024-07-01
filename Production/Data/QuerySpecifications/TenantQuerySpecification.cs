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
    public class TenantQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

        #endregion

        #region Constructors

        public TenantQuerySpecification()
            : base()
        {
        }

        public TenantQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public TenantQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

        #endregion

        #endregion

        #endregion

    }
}