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
    public class QueryParameterValueQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

    	public const string Key_QueryParameterValueName = "QueryParameterValueName";
        public const string Key_QueryParameterValueNameCont = "QueryParameterValueNameCont";
        public const string Key_QueryParameterId = "QueryParameterId";
        public const string Key_Wildcard = "Wildcard";
    	public const string Key_RowStatusId = "RowStatusId";
      
        #endregion

        #region Constructors

        public QueryParameterValueQuerySpecification()
            : base()
        {
        }

        public QueryParameterValueQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public QueryParameterValueQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public string QueryParameterValueName
		{
			get { return this.Conditions.GetConditionAsString(Key_QueryParameterValueName); }
			set { this.Conditions.SetCondition(Key_QueryParameterValueName, value); }
		}

        [XmlIgnore]
        public string QueryParameterValueNameCont
        {
            get { return this.Conditions.GetConditionAsString(Key_QueryParameterValueNameCont); }
            set { this.Conditions.SetCondition(Key_QueryParameterValueNameCont, value); }
        }

		[XmlIgnore]
		public int? QueryParameterId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_QueryParameterId); }
			set { this.Conditions.SetCondition(Key_QueryParameterId, value); }
		}

        [XmlIgnore]
        public bool Wildcard
        {
            get { return this.Conditions.GetConditionAsBoolean(Key_Wildcard) ?? false; }
            set { this.Conditions.SetCondition(Key_Wildcard, value); }
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