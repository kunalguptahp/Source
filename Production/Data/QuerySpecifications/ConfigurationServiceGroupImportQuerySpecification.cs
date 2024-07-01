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
    public class ConfigurationServiceGroupImportQuerySpecification : GenericQuerySpecificationWrapper
    {

        #region Constants

        public const string Key_ConfigurationServiceApplicationName = "ConfigurationServiceApplicationName";
        public const string Key_ConfigurationServiceGroupTypeName = "ConfigurationServiceGroupTypeName";
        public const string Key_ImportStatus = "ImportStatus";
        public const string Key_ImportMessage = "ImportMessage";
        public const string Key_LabelValue = "LabelValue";
        public const string Key_RowStatusId = "RowStatusId";

		#endregion

        #region Constructors

        public ConfigurationServiceGroupImportQuerySpecification()
            : base()
        {
        }

        public ConfigurationServiceGroupImportQuerySpecification(QuerySpecification innerQuery)
            : base(innerQuery)
        {
        }

		public ConfigurationServiceGroupImportQuerySpecification(IQuerySpecification original)
            : base(original)
        {
        }

        #endregion

        #region Properties

        #region Query Conditions Properties (Strongly-typed convenience/utility properties)

        #region Conditions related to Entity-specific Fields

        [XmlIgnore]
        public string ConfigurationServiceApplicationName
        {
            get { return this.Conditions.GetConditionAsString(Key_ConfigurationServiceApplicationName); }
            set { this.Conditions.SetCondition(Key_ConfigurationServiceApplicationName, value); }
        }

        [XmlIgnore]
        public string ConfigurationServiceGroupTypeName
        {
            get { return this.Conditions.GetConditionAsString(Key_ConfigurationServiceGroupTypeName); }
            set { this.Conditions.SetCondition(Key_ConfigurationServiceGroupTypeName, value); }
        }

        [XmlIgnore]
        public string LabelValue
        {
            get { return this.Conditions.GetConditionAsString(Key_LabelValue); }
            set { this.Conditions.SetCondition(Key_LabelValue, value); }
        }

        [XmlIgnore]
        public string ImportMessage
        {
            get { return this.Conditions.GetConditionAsString(Key_ImportMessage); }
            set { this.Conditions.SetCondition(Key_ImportMessage, value); }
        }

        [XmlIgnore]
        public string ImportStatus
        {
            get { return this.Conditions.GetConditionAsString(Key_ImportStatus); }
            set { this.Conditions.SetCondition(Key_ImportStatus, value); }
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