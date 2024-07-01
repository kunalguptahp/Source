using System.Collections.Generic;
using HP.ElementsCPS.Core.Security;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the RoleCollection class.
	/// </summary>
	public partial class RoleCollection
	{
		public List<UserRoleId> ToUserRoleIdList()
		{
			List<UserRoleId> roles = new List<UserRoleId>(this.Count);
			foreach (Role role in this)
			{
				roles.Add(role.UserRoleId);
			}
			return roles;
		}
	}
}
