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
    public class ConfigurationServiceItemQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_RowStatusId = "RowStatusId";

		#endregion

        #region Constructors

        public ConfigurationServiceItemQuerySpecification()
            : base()
        {
        }

        public ConfigurationServiceItemQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

		public ConfigurationServiceItemQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

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