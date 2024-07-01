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
    public class JumpstationGroupSelectorQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

		public const string Key_JumpstationGroupId = "JumpstationGroupId";
 
        #endregion

        #region Constructors

        public JumpstationGroupSelectorQuerySpecification()
            : base()
        {
        }

        public JumpstationGroupSelectorQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public JumpstationGroupSelectorQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

		[XmlIgnore]
		public int? JumpstationGroupId
		{
			get { return this.Conditions.GetConditionAsInt32(Key_JumpstationGroupId); }
			set { this.Conditions.SetCondition(Key_JumpstationGroupId, value); }
		}

		#region Conditions related to Entity-specific Fields

		//public int? CompatibleWithQueryParameterId { get; set; }

        #endregion

        #endregion

        #endregion

    }
}