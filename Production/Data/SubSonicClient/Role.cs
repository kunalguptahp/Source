using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HP.ElementsCPS.Core.Security;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the Role class.
	/// </summary>
	public partial class Role
	{
		public UserRoleId UserRoleId
		{
			get
			{
				return (UserRoleId)this.Id;
			}
		}

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of a Role.
		/// </summary>
		/// <param name="instance">The <see cref="Role"/> to format.</param>
		/// <returns></returns>
		private static string Format(Role instance)
		{
			return string.Format(CultureInfo.CurrentCulture, "Role #{0} ({1})", instance.Id, instance.Name);
		}

		#endregion

		#region UserRoleId-related Utility Methods

		/// <summary>
		/// Converts a specified set of roles to a set of role names.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<string> GetRoleNames(IEnumerable<UserRoleId> roles)
		{
			//TODO: Review Needed: Review implementation for correctness
#warning Review Needed: Review implementation for correctness
			return roles.Select(r => string.Format("{0:G}", (object)r));
		}

		/// <summary>
		/// Returns a delimited string representing a specified set of roles.
		/// </summary>
		/// <returns></returns>
		public static string Format(IEnumerable<UserRoleId> roles, string separator)
		{
			return string.Join(separator, Role.GetRoleNames(roles).ToArray());
		}

		public static string Format(IEnumerable<UserRoleId> roles)
		{
			const string separator = ", ";
			return Format(roles, separator);
		}

		#endregion


        
    
       
	}
}
