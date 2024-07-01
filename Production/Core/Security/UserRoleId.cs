namespace HP.ElementsCPS.Core.Security
{
	/// <summary>
	/// Enumerates the numeric values of the application's user roles.
	/// </summary>
	/// <remarks>
	/// NOTE: The IDs in the DB's Role table MUST match the values defined in this enumeration.
	/// </remarks>
	public enum UserRoleId
	{
		None = 0,
		Administrator = 1,
		DataAdmin = 2,
		UserAdmin = 3,
		Viewer = 4,
		Editor = 5,
		Validator = 6,
		Coordinator = 7,
		AccountLocked = 8,
		RestrictedAccess = 9,
		Everyone = 10,
		TechSupport = 11,
        SysDataAdmin = 12
	}
}