using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Security;

namespace HP.ElementsCPS.Core.Security
{
	/// <summary>
	/// Provides a simple, unified API for the most commonly needed security-related functionality.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class is implemented as a wrapper around the .NET (and ASP.NET) Authorization and Role Based Security framework.
	/// </para>
	/// </remarks>
	public class SecurityManager : AbstractSecurityManager<UserRoleId>
	{

		#region Constructors

		/// <summary>
		/// Type constructor.
		/// </summary>
		static SecurityManager()
		{
			object logSource = typeof(SecurityManager);
			LogManager.Current.Log(Severity.Debug, logSource, "Type constructor invoked.");
		}

		/// <summary>
		/// Private constructor.
		/// </summary>
		/// <remarks>
		/// NOTE: All constructors must be private in order to implement the Singleton pattern.
		/// </remarks>
		private SecurityManager()
		{
		}

		#endregion

		#region Singleton Property

		private static readonly object _LockForCurrentProperty = new object();

		private static SecurityManager _Current;
		public static SecurityManager Current
		{
			get
			{
				if (SecurityManager._Current == null)
				{
					SecurityManager.InitializeCurrentInstance();
				}
				return SecurityManager._Current;
			}
			//private set { _Current = value; }
		}

		private static void InitializeCurrentInstance()
		{
			if (SecurityManager._Current == null)
			{
				lock (SecurityManager._LockForCurrentProperty)
				{
					if (SecurityManager._Current == null)
					{
						//initialize the backing field
						SecurityManager._Current = new SecurityManager();
					}
				}
			}
		}

		#endregion

		#region GetEffectiveRoles method

		/// <summary>
		/// Returns a list of "effective" roles (based on Role-inheritance and business logic rules) that are implied by a set of explicit roles.
		/// </summary>
		/// <param name="explicitRoles">The explicit (i.e. directly assigned/possessed) set of roles.</param>
		/// <returns>The "effective" set of roles implied by the <paramref name="explicitRoles"/> set of roles.</returns>
		public override List<UserRoleId> GetEffectiveRoles(IEnumerable<UserRoleId> explicitRoles)
		{
			if (explicitRoles == null)
			{
				return null;
			}
			if (explicitRoles.Count() < 1)
			{
				return new List<UserRoleId>(new[] { UserRoleId.None }); //anonymous users have only the "None" role
				//return new List<UserRoleId>(new[] { UserRoleId.AccountLocked }); //anonymous users are locked out
				//return new List<UserRoleId>(0); //anonymous users have no roles
			}
			if (explicitRoles.Contains(UserRoleId.AccountLocked))
			{
				//ignore all other roles if the AccountLocked role is found
				return new List<UserRoleId>(new[] { UserRoleId.AccountLocked });
			}
			if (explicitRoles.Contains(UserRoleId.None))
			{
				//ignore all other roles if the None role is found
				return new List<UserRoleId>(new[] { UserRoleId.None });
			}

			if (explicitRoles.Contains(UserRoleId.Administrator))
			{
				//return all roles (except limiting ones like the AccountLocked and RestrictedAccess roles) if the Administrator role is found
				IList<UserRoleId> administratorEffectiveRoles = (IList<UserRoleId>)new UserRoleId[]{
					UserRoleId.Administrator, 
					UserRoleId.DataAdmin, 
					UserRoleId.Coordinator, 
					UserRoleId.Editor, 
					UserRoleId.Everyone, 
					UserRoleId.TechSupport, 
					UserRoleId.Validator, 
					UserRoleId.Viewer, 
					UserRoleId.UserAdmin
					};

				return new List<UserRoleId>(administratorEffectiveRoles);
			}

			//start by defaulting the effective roles to the explicit roles
			List<UserRoleId> effectiveRoles = new List<UserRoleId>(explicitRoles);

			if (!effectiveRoles.Contains(UserRoleId.Validator))
			{
				//OfferCoordinators inherit the OfferValidator role
				if (effectiveRoles.Contains(UserRoleId.Coordinator))
				{
					effectiveRoles.Add(UserRoleId.Validator);
				}
			}

			if (!effectiveRoles.Contains(UserRoleId.Editor))
			{
				//OfferValidators and OfferCoordinators inherit the OfferEditor role
				if (effectiveRoles.Contains(UserRoleId.Validator) || effectiveRoles.Contains(UserRoleId.Coordinator))
				{
					effectiveRoles.Add(UserRoleId.Editor);
				}
			}

			if (!effectiveRoles.Contains(UserRoleId.Viewer))
			{
				//assign all users (except those cases already handled by the guard conditions above) the OfferViewer role
                if (effectiveRoles.Contains(UserRoleId.UserAdmin) || effectiveRoles.Contains(UserRoleId.TechSupport))
                {
                    effectiveRoles.Add(UserRoleId.Viewer);
                }
            }

			if (!effectiveRoles.Contains(UserRoleId.Everyone))
			{
				//assign all users (except those cases already handled by the guard conditions above) the Everyone role
				effectiveRoles.Add(UserRoleId.Everyone);
			}

			//recurse (if necessary)
			bool rolesWereAdded = (effectiveRoles.Except(explicitRoles).Count() > 0);
			bool rolesWereRemoved = (explicitRoles.Except(effectiveRoles).Count() > 0);
			if (rolesWereAdded || rolesWereRemoved)
			{
				//recursively invoke the method to make sure that any roles that are inherited from non-explicit roles are properly inherited
				effectiveRoles = GetEffectiveRoles(effectiveRoles);
			}

			return effectiveRoles;
		}

		#endregion

		public static bool IsCurrentUserRegistered
		{
			get
			{
				//if the user has a blank Identity, then they are unregistered
				if (string.IsNullOrEmpty(SecurityManager.CurrentUserIdentityName))
				{
					return false;
				}
				//else if the user possesses the None role, then they must be unregistered
				if (SecurityManager.IsCurrentUserInRole(UserRoleId.None))
				{
					return false;
				}
				//else if the user possesses the AccountLocked or Everyone role, then they must be registered
				if (SecurityManager.IsCurrentUserInRole(UserRoleId.AccountLocked) || SecurityManager.IsCurrentUserInRole(UserRoleId.Everyone))
				{
					return true;
				}
				//else they must be unregistered
				return false;
			}
		}

		public static void AuthenticateAsUnregisteredUser(string identityName)
		{
			SecurityManager.CurrentUser = CreateUnregisteredUserPrincipal(identityName);
		}

		public static IPrincipal CreateUnregisteredUserPrincipal(string identityName)
		{
			return SecurityManager.Current.CreatePrincipal(identityName, new[] { UserRoleId.None });
		}

	}
}