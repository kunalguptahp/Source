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
    public class NoteQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

        public const string Key_EntityTypeId = "EntityTypeId";
        public const string Key_EntityId = "EntityId";
        public const string Key_NoteTypeId = "NoteTypeId";

        #endregion

        #region Constructors

        public NoteQuerySpecification()
            : base()
        {
        }

        public NoteQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

        public NoteQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

        [XmlIgnore]
        public int? EntityTypeId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_EntityTypeId); }
            set { this.Conditions.SetCondition(Key_EntityTypeId, value); }
        }

        [XmlIgnore]
        public int? EntityId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_EntityId); }
            set { this.Conditions.SetCondition(Key_EntityId, value); }
        }

        [XmlIgnore]
        public int? NoteTypeId
        {
            get { return this.Conditions.GetConditionAsInt32(Key_NoteTypeId); }
            set { this.Conditions.SetCondition(Key_NoteTypeId, value); }
        }

        #endregion

        #endregion

        #endregion

    }
}