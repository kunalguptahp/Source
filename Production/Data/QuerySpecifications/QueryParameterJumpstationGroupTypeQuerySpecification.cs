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
    public class QueryParameterJumpstationGroupTypeQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_JumpstationGroupTypeId = "JumpstationGroupTypeId";
		public const string Key_JumpstationGroupId = "JumpstationGroupId";
		public const string Key_RowStatusId = "RowStatusId";

      
       
        public const string Key_QueryParameterName = "QueryParameterName";

        #endregion

        #region Constructors

        public QueryParameterJumpstationGroupTypeQuerySpecification()
            : base()
        {
        }

        public QueryParameterJumpstationGroupTypeQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public QueryParameterJumpstationGroupTypeQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

		[XmlIgnore]
		public int? JumpstationGroupTypeId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_JumpstationGroupTypeId); }
			set { this.Conditions.SetCondition(Key_JumpstationGroupTypeId, value); }
		}

		[XmlIgnore]
		public int? JumpstationGroupId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_JumpstationGroupId); }
			set { this.Conditions.SetCondition(Key_JumpstationGroupId, value); }
		}

		[XmlIgnore]
		public int? RowStatusId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_RowStatusId); }
			set { this.Conditions.SetCondition(Key_RowStatusId, value); }
		}

        

        [XmlIgnore]
        public string QueryParameterName
        {
            get { return this.Conditions.GetConditionAsString(Key_QueryParameterName); }
            set { this.Conditions.SetCondition(Key_QueryParameterName, value); }
        }

        #endregion

        #endregion

        #endregion

    }
}