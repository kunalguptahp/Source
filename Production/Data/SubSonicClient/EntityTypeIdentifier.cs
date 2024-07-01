namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Enumerates the primary keys of the records in the EntityType table.
	/// </summary>
	/// <remarks>
	/// NOTE: The IDs in the EntityType table MUST match the values defined in this enumeration.
	/// </remarks>
	public enum EntityTypeIdentifier
	{
		None = 0,
		//Aardvark = 1,
		//Addendum = 2,
		//AddendumType = 3,
		//DDLChangeLog = 4,
		EntityType = 5,
		Log = 6,
		Note = 7,
		NoteType = 8,
		Person = 9,
		PersonRole = 10,
		Role = 11,
		RowStatus = 12,
		Tag = 13,
		//Task = 14,
		//TaskType = 15,
		ConfigurationServiceApplication = 16,
		ConfigurationServiceGroup = 17,
		ConfigurationServiceGroupConfigurationServiceLabelValue = 18,
		ConfigurationServiceGroupTag = 19,
		ConfigurationServiceGroupSelector = 20,
		ConfigurationServiceGroupSelectorQueryParameterValue = 21,
		ConfigurationServiceGroupStatus = 22,
		ConfigurationServiceGroupType = 23,
		ConfigurationServiceItem = 24,
		ConfigurationServiceLabel = 25,
		ConfigurationServiceLabelValue = 26,
		ProxyURL = 27,
		ProxyURLQueryParameterValue = 28,
		ProxyURLTag = 29,
		ProxyURLDomain = 30,
		ProxyURLGroupType = 31,
		ProxyURLStatus = 32,
		ProxyURLType = 33,
		PublishTemp = 34,
		QueryParameter = 35,
		QueryParameterConfigurationServiceGroupType = 36,
		QueryParameterProxyURLType = 37,
		QueryParameterValue = 38,
        JumpstationApplication = 39,
		JumpstationGroup = 40,
		//JumpstationGroupJumpstationLabelValue = 41,
		JumpstationGroupTag = 42,
		JumpstationGroupSelector = 43,
		JumpstationGroupSelectorQueryParameterValue = 44,
		JumpstationGroupStatus = 45,
		JumpstationGroupType = 46,
        JumpstationMacro = 47,
	}
}