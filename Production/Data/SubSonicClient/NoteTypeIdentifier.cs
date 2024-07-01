namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Enumerates the primary keys of the records in the NoteType table.
	/// </summary>
	/// <remarks>
	/// NOTE: The IDs in the NoteType table MUST match the values defined in this enumeration.
	/// </remarks>
	public enum NoteTypeIdentifier
	{
		None = 0,
		Debug = 1,
		Info = 2,
		Warning = 3,
		Error = 4,
		Milestone = 5,
		StatusChange = 6,
		DataChange = 7,
		RelatedDataChange = 8,
		RelatedDataStatusChange = 9,
		TaskCompleted = 10,
		TaskCancelled = 11,
		ProjectEnded = 12,
		NotificationSent = 13
	}
}