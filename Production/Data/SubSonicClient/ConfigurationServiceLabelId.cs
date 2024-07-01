namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Enumerates the numeric values of the application's Configuration Service Label ids.
	/// </summary>
	/// <remarks>
	/// NOTE: The IDs in the DB's ConfigurationServiceLabel table MUST match the values defined in this enumeration.
	/// </remarks>
	public enum ConfigurationServiceLabelId
	{
		Category = 1,
		TrackingConfigFreq = 2,
		TrackingConfigURL = 3,
		CurrentState = 4,
		DisplayName = 5,
		Version = 6,
		Publisher = 7,
		InstallerType = 8,
		InstallPath32bit= 9,
		InstallPath64bit = 10
	}
}