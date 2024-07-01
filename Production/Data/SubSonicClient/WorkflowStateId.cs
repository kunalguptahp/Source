namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Enumerates the numeric values of the application's ConfigurationService state values.
	/// </summary>
	/// <remarks>
	/// NOTE: The IDs in the DB's WorkflowStatus table MUST match the values defined in this enumeration.
	/// </remarks>
	public enum WorkflowStateId
	{
		None = 0,
		Modified = 1,
		ReadyForValidation = 2,
		Validated = 3,
		Published = 4,
		PublishedProductionOnly = 5,
		Cancelled = 6,
		Abandoned = 7,
		Deleted = 8
	}
}