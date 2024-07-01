using System;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Collections.Generic;
using HP.ElementsCPS.Core.Security;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the Person class.
	/// </summary>
	public partial class Person
	{

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of a Person.
		/// </summary>
		/// <param name="instance">The <see cref="Person"/> to format.</param>
		/// <returns></returns>
		private static string Format(Person instance)
		{
			return string.Format(CultureInfo.CurrentCulture, "Person #{0} ({1})", instance.Id, instance.Name);
		}

		#endregion

		/// <summary>
		/// Creates a new IPrincipal instance that represents this instance.
		/// </summary>
		/// <returns></returns>
		public IPrincipal ToPrincipal()
		{
			//return SecurityManager.Current.CreatePrincipal(@"AUTH\MuniyaRa", this.GetRoles());
			return SecurityManager.Current.CreatePrincipal(this.WindowsId, this.GetRoles());
		}

		//public string GetEmailAddress(bool includeDisplayName)
		//{
		//   MailAddress mailAddress = this.GetMailAddress();
		//   if (mailAddress == null)
		//   {
		//      return null;
		//   }
		//   return includeDisplayName ? mailAddress.ToString() : mailAddress.Address;
		//}

		public MailAddress GetMailAddress()
		{
			return new MailAddress(this.Email, this.GetName_FirstLast());
		}

		public string GetName_FirstLast()
		{
			return (this.FirstName + " " + this.LastName).Trim();
		}

		#region Role-related convenience members

		/// <summary>
		/// Returns the roles (including both explicit and inherited roles) possessed by this instance.
		/// </summary>
		/// <returns></returns>
		public List<UserRoleId> GetEffectiveRoles()
		{
			return PersonController.GetRoles(this.Id, true);
			//List<UserRoleId> explicitRoles = this.GetRoles();
			//List<UserRoleId> effectiveRoles = SecurityManager.Current.GetEffectiveRoles(explicitRoles);
			//return effectiveRoles;
		}

		/// <summary>
		/// Returns the roles (including both explicit and inherited roles) possessed by this instance as a comma-delimited string.
		/// </summary>
		/// <returns></returns>
		public string GetEffectiveRolesAsString()
		{
			return Role.Format(this.GetEffectiveRoles().OrderBy(r => r.ToString("G")));
		}

		/// <summary>
		/// Returns the roles possessed by this instance.
		/// </summary>
		/// <returns></returns>
        public List<UserRoleId> GetRoles()
        {
            return PersonController.GetRoles(this.Id, false);
            //return this.GetRoleCollection().ToUserRoleIdList();
        }

    

		/// <summary>
		/// Replaces all Roles currently associated with this Person with the specified set of new roles.
		/// </summary>
		public void SetRoles(List<UserRoleId> newRoles)
		{
			//determine which roles need to be removed and which ones need to be added
            List<UserRoleId> oldRoles = new List<UserRoleId>();
			RoleCollection oldRoleCols = Person.GetRoleCollection(this.Id);
            foreach (Role role in oldRoleCols)
            {
                oldRoles.Add((UserRoleId)role.Id);
            }
			IEnumerable<UserRoleId> enumRemovedRoles = oldRoles.Except(newRoles);
			IEnumerable<UserRoleId> enumAddedRoles = newRoles.Except(oldRoles);

			//delete the removed roles
			foreach (UserRoleId roleId in enumRemovedRoles)
			{
				this.RemoveRole(roleId);
			}

			//insert the added roles
			foreach (UserRoleId roleId in enumAddedRoles)
			{
				this.AddRole(roleId);
			}
		}

		/// <summary>
		/// Deletes/removes a specific Role associated with this Person.
		/// </summary>
		/// <param name="roleId"></param>
		public void RemoveRole(UserRoleId roleId)
		{
			PersonRole.Destroy(this.Id, (int)roleId);
		}

		/// <summary>
		/// Adds/inserts a specific Role associated with this Person.
		/// </summary>
		/// <param name="roleId"></param>
		public void AddRole(UserRoleId roleId)
		{
			PersonRole newRole = new PersonRole(true);
			newRole.PersonId = this.Id;
			newRole.RoleId = (int)roleId;
			newRole.Save(SecurityManager.CurrentUserIdentityName);
		}

		/// <summary>
		/// Deletes/removes all Roles associated with this Person.
		/// </summary>
		public void ClearRoles()
		{
			PersonRole.DestroyByPersonId(this.Id);
		}

		#endregion


         public void SetApplications(int  personId, UserRoleId roleId, List<RoleApplicationId> newApplications)
        {
            List<RoleApplicationId> oldApplications = ApplicationRoleController.GetCurrentAppListByRole(personId, roleId);
           
            IEnumerable<RoleApplicationId> enumRemovedApplications = oldApplications.Except(newApplications);
            IEnumerable<RoleApplicationId> enumAddedApplications = newApplications.Except(oldApplications);
            foreach (RoleApplicationId appId in enumRemovedApplications)
            {
                this.RemoveApplication(personId ,roleId, appId);
            }
            foreach (RoleApplicationId appId in enumAddedApplications)
            {
                this.AddApplication(personId,roleId, appId);
            }
        }

        public void RemoveApplication(int personId,UserRoleId roleId, RoleApplicationId appId)
        {
            ApplicationRole.Destroy(personId ,(int)roleId, (int)appId);
        }

        public void AddApplication(int personId ,UserRoleId roleId, RoleApplicationId appId)
        {
            ApplicationRole newApplication = new ApplicationRole(true);
            newApplication.RoleId =(int) roleId;
            newApplication.PersonId = personId;
            newApplication.ApplicationId = (int)appId;
            newApplication.Save(SecurityManager.CurrentUserIdentityName);
        }
       
	}
}
