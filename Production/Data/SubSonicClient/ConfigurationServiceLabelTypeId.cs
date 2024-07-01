namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Enumerates the numeric values of the application's label type values.
	/// </summary>
	/// <remarks>
	/// NOTE: The IDs in the DB's ConfigurationServiceLabelType table MUST match the values defined in this enumeration.
	/// </remarks>
	public enum ConfigurationServiceLabelTypeId
	{
		LabelTypeTextSingle = 1,
		LabelTypeTextMultiple = 2,
		LabelTypeDropDownList = 3
	}
}