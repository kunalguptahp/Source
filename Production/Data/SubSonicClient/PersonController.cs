using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using HP.ElementsCPS.Core.Utility;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Utility.SubSonic;
using HP.HPFx.Web.Utility;
using SubSonic;
using HP.ElementsCPS.Data.Utility.ImportUtility;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    /// <summary>
    /// The non-generated portion of the PersonController class.
    /// </summary>
    public partial class PersonController
    {
        #region Constants

        private static readonly string[] _ValidQueryConditionKeys = new[]
			{
				GenericQuerySpecificationWrapper.Key_Comments,
				GenericQuerySpecificationWrapper.Key_CreatedAfter,
				GenericQuerySpecificationWrapper.Key_CreatedBefore,
				GenericQuerySpecificationWrapper.Key_CreatedBy,
				GenericQuerySpecificationWrapper.Key_Id,
				GenericQuerySpecificationWrapper.Key_IdList,
				GenericQuerySpecificationWrapper.Key_ModifiedAfter,
				GenericQuerySpecificationWrapper.Key_ModifiedBefore,
				GenericQuerySpecificationWrapper.Key_ModifiedBy,
				GenericQuerySpecificationWrapper.Key_Name,
				GenericQuerySpecificationWrapper.Key_RowStateId,
				PersonQuerySpecification.Key_LastName,
				PersonQuerySpecification.Key_Email,
				PersonQuerySpecification.Key_FirstName,
				PersonQuerySpecification.Key_WindowsId
			};

        #endregion

        #region ObjectDataSource Methods

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static PersonCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
            PersonQuerySpecification qs = new PersonQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
        {
            PersonQuerySpecification qs = new PersonQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
        }

        /// <summary>
        /// Returns a collection of all <see cref="Person"/> instances associated with a specified Windows ID.
        /// </summary>
        /// <param name="windowsId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static PersonCollection FetchByWindowsID(string windowsId)
        {
          
            return new PersonCollection().Where(Person.WindowsIdColumn.ColumnName, windowsId).Load();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static PersonCollection FetchByTenantGroupId(int tenantGroupId)
        {
            return new PersonCollection().Where(Person.TenantGroupIdColumn.ColumnName, tenantGroupId).Load();
        }
        /// <summary>
        /// Returns a collection of all <see cref="Person"/> instances associated with a specified email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static PersonCollection FetchByEmail(string email)
        {
            return new PersonCollection().Where(Person.EmailColumn.ColumnName, email).Load();
        }

        /// <summary>
        /// Convenience method.
        /// Returns the <see cref="Person"/> with the specified name (if one exists).
        /// </summary>
        /// <param name="name"></param>
        public static Person FetchByName(string name)
        {
            SqlQuery query = DB.Select().From(Person.Schema);
            PersonQuerySpecification qs = new PersonQuerySpecification();

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(PersonController));
            Person instance = query.ExecuteSingle<Person>();
            return instance;
        }

        #endregion

        #region QuerySpecification-related Methods

        public static PersonCollection Fetch(PersonQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteAsCollection<PersonCollection>();
        }

        public static int FetchCount(PersonQuerySpecification qs)
        {
            return CreateQuery(qs, true).GetRecordCount();
        }

        #region CreateQuery

        private static SqlQuery CreateQuery(PersonQuerySpecification qs, bool isCountQuery)
        {
            //TODO: Implement: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
#warning Not Implemented: Re-enable QuerySpecification validation (after it has been code reviewed and tested)
            //ElementsCPSDataUtility.ValidateQuerySpecificationConditions(qs, _ValidQueryConditionKeys);
            return PersonController.CreateQueryHelper(qs, Person.Schema, isCountQuery);
        }

        #endregion

        #region CreateQueryHelper

        internal static SqlQuery CreateQueryHelper(PersonQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
        {
            SqlQuery query = DB.Select().From(schema);

            ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "RoleId", qs.RoleId);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "TenantGroupId", qs.TenantGroupId);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereStartsWith(query, "LastName", qs.LastName);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereStartsWith(query, "Email", qs.Email);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereStartsWith(query, "FirstName", qs.FirstName);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereStartsWith(query, "WindowsId", qs.WindowsId);

            if (!isCountQuery)
            {
                SubSonicUtility.SetPaging(query, qs.Paging);
                SubSonicUtility.SetOrderBy(query, qs.SortBy);
            }

            ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(PersonController));
            return query;
        }

        #endregion

        #region SmartFetch Method

        public static IRecordCollection SmartFetch(PersonQuerySpecification qs, bool includeRowStatusNameInOutput)
        {
            if (qs.RoleId != null)
            {
                return VwMapPersonRoleController.Fetch(qs);
            }
            else if (includeRowStatusNameInOutput)
            {
                return VwMapPersonController.Fetch(qs);
            }
            else
            {
                return PersonController.Fetch(qs);
            }
        }

        #endregion

        #endregion

        #region Other Querying Methods

        /// <summary>
        /// Returns the <see cref="Person"/> instance associated with a specified Windows ID.
        /// </summary>
        /// <param name="windowsId"></param>
        /// <returns></returns>
        public static Person GetByWindowsID(string windowsId)
        {
            //NOTE: Performance: Cacheing: We cache this call at the HttpContext.Cache level for performance
            Action<string, Person, bool, Cache> cacheMutator =
                delegate(string key, Person item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { WebUtility.CacheInsert(cache, key, item, null, ElementsCPSCoreUtility.AppCacheAbsoluteExpirationShort, CacheItemPriority.Low); } };
            string cacheKey = string.Format("PersonController.GetByWindowsID(\"{0}\")", windowsId);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, () => GetByWindowsIDHelper(windowsId));
            //return GetByWindowsIDHelper(windowsId);
        }

        private static Person GetByWindowsIDHelper(string windowsId)
        {
            PersonCollection collection = FetchByWindowsID(windowsId);
            if (collection.Count == 1)
            {
                return collection[0];
            }
            if (collection.Count == 0)
            {
                return null;
            }

            //The following condition should never occur
            string message = string.Format(CultureInfo.CurrentCulture, "The specified WindowsId ({0}) matches more than one Person record.", windowsId);
            LogManager.Current.Log(Severity.Info, typeof(PersonController), message);
            throw new InvalidOperationException(message);
        }

        public static List<string> GetWindowsIdListByTenantGroupId(int tenantGroupId)
        {
            PersonCollection collection = FetchByTenantGroupId(tenantGroupId);
            List<string> windowsIds = new List<string>();
            if (collection.Count == 0)
            {
                return null;
            }
            foreach (Person person in collection)
            {
                windowsIds.Add(person.WindowsId);
            }

            return windowsIds;
        }
        /// <summary>
        /// Returns the <see cref="Person"/> instance associated with a specified Windows ID.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static Person GetByEmail(string emailAddress)
        {
            //NOTE: Performance: Cacheing: We cache this call at the HttpContext.Cache level for performance
            Action<string, Person, bool, Cache> cacheMutator =
                delegate(string key, Person item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { WebUtility.CacheInsert(cache, key, item, null, ElementsCPSCoreUtility.AppCacheAbsoluteExpirationVeryShort, CacheItemPriority.Low); } };
            string cacheKey = string.Format("PersonController.GetByEmail(\"{0}\")", emailAddress);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, () => GetByEmailHelper(emailAddress));
            //return GetByEmailHelper(emailAddress);
        }

        private static Person GetByEmailHelper(string emailAddress)
        {
            PersonCollection collection = FetchByEmail(emailAddress);
            if (collection.Count == 1)
            {
                return collection[0];
            }
            if (collection.Count == 0)
            {
                return null;
            }

            //The following condition should never occur
            string message = string.Format(CultureInfo.CurrentCulture, "The specified Email Address ({0}) matches more than one Person record.", emailAddress);
            LogManager.Current.Log(Severity.Info, typeof(PersonController), message);
            throw new InvalidOperationException(message);
        }

        /// <summary>
        /// Returns the <see cref="Person"/> instance associated with a specified Windows ID.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Person GetByName(string name)
        {
            PersonCollection collection = new PersonCollection().Where("Name", name).Load();
            if (collection.Count == 1)
            {
                return collection[0];
            }
            if (collection.Count == 0)
            {
                return null;
            }

            //The following condition should never occur
            string message = string.Format(CultureInfo.CurrentCulture, "The specified Name ({0}) matches more than one Person record.", name);
            LogManager.Current.Log(Severity.Warn, typeof(PersonController), message);
            throw new InvalidOperationException(message);
        }

        /// <summary>
        /// Returns the <see cref="Person"/> instance associated with a specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Person GetByID(int id)
        {
            //Person person = Person.FetchByID(id);
            PersonCollection collection = FetchByID(id);
            if (collection.Count == 1)
            {
                return collection[0];
            }
            if (collection.Count == 0)
            {
                return null;
            }

            //The following condition should never occur
            string message = string.Format(CultureInfo.CurrentCulture, "The specified Id ({0}) matches more than one Person record.", id);
            LogManager.Current.Log(Severity.Warn, typeof(PersonController), message);
            throw new InvalidOperationException(message);
        }

        #region Person.Name-related Query Methods

        /// <summary>
        /// Returns the <see cref="Person.Name"/> of the <see cref="Person"/> with a specified <see cref="Person.WindowsId"/>.
        /// </summary>
        /// <returns>the <see cref="Person.Name"/> or <c>null</c></returns>
        public static string GetPersonNameByWindowsId(string windowsId)
        {
            return GetPersonName(PersonController.GetByWindowsID(windowsId));
        }

        /// <summary>
        /// Returns the <see cref="Person.Name"/> of the <see cref="Person"/> with a specified <see cref="Person.Id"/>.
        /// </summary>
        /// <returns>the <see cref="Person.Name"/> or <c>null</c></returns>
        public static string GetPersonNameById(int id)
        {
            return GetPersonName(PersonController.GetByID(id));
        }

        /// <summary>
        /// Returns the <see cref="Person.Name"/> of a specified <see cref="Person"/>.
        /// </summary>
        /// <returns>the <see cref="Person.Name"/> or <c>null</c></returns>
        private static string GetPersonName(Person person)
        {
            return (person == null) ? null : person.Name;
        }

        #endregion

        #region CurrentUser-related Query Methods

        /// <summary>
        /// Returns the <see cref="Person"/> instance associated with the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        public static Person GetCurrentUser()
        {
            return PersonController.GetByWindowsID(SecurityManager.CurrentUserIdentityName);
        }


        /// <summary>
        /// Returns the <see cref="Person.Id"/> associated with the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        public static int? GetCurrentUserId()
        {
            //NOTE: Performance: Cacheing: We cache this call at the Session level for performance
            Action<string, int?, bool, HttpSessionState> cacheMutator =
                delegate(string key, int? item, bool wasCached, HttpSessionState session) { if ((!wasCached) && (item != null)) { session[key] = item; } };
            return WebUtility.GetCachedItemFromHttpContextSession("CurrentUser.Id", cacheMutator, GetCurrentUserIdHelper);
            //return GetCurrentUserIdHelper();
        }

        private static int? GetCurrentUserIdHelper()
        {
            Person currentUser = PersonController.GetCurrentUser();
            return (currentUser == null) ? (int?)null : currentUser.Id;
        }

        /// <summary>
        /// Returns the <see cref="UserRoleId"/>s associated with the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        public static List<UserRoleId> GetCurrentUserRoles(bool includeInheritedRoles)
        {
            //NOTE: Performance: Cacheing: We cache this call at the HttpContext level for performance
            Action<string, List<UserRoleId>, bool, HttpContext> cacheMutator =
                delegate(string key, List<UserRoleId> item, bool wasCached, HttpContext context) { if ((!wasCached) && (item != null)) { context.Items[key] = item; } };
            string cacheKey = string.Format("CurrentUser.UserRoleIds({0}", includeInheritedRoles);
            return WebUtility.GetCachedItemFromHttpContext(cacheKey, cacheMutator, () => GetCurrentUserRolesHelper(includeInheritedRoles));
            //return GetCurrentUserRolesHelper(includeInheritedRoles);
        }

        private static List<UserRoleId> GetCurrentUserRolesHelper(bool includeInheritedRoles)
        {
            int? currentUserId = PersonController.GetCurrentUserId();
            if (currentUserId == null)
            {
                return null;
            }
            return GetRoles(currentUserId.Value, includeInheritedRoles);
        }

        /// <summary>
        /// Returns the <see cref="Person.Name"/> associated with the currently authenticated user.
        /// </summary>
        /// <returns>the <see cref="Person.Name"/> or <c>null</c></returns>
        public static string GetCurrentUserName()
        {
            return GetPersonName(PersonController.GetCurrentUser());
        }

        #endregion

        #region Role-related Query Methods

        public static List<UserRoleId> GetRoles(int personId, bool includeInheritedRoles)
        {
            //NOTE: Performance: Cacheing: We cache this call at the HttpContext.Cache level for performance
            Action<string, List<UserRoleId>, bool, Cache> cacheMutator =
                delegate(string key, List<UserRoleId> item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { WebUtility.CacheInsert(cache, key, item, null, ElementsCPSCoreUtility.AppCacheAbsoluteExpirationShort, CacheItemPriority.Low); } };
            string cacheKey = string.Format("PersonController.GetRoles({0}, {1})", personId, includeInheritedRoles);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, () => GetRolesHelper(personId, includeInheritedRoles));
            //return GetRolesHelper(personId, includeInheritedRoles);
        }

        private static List<UserRoleId> GetRolesHelper(int personId, bool includeInheritedRoles)
        {
            List<UserRoleId> roles = Person.GetRoleCollection(personId).ToUserRoleIdList();
            if (includeInheritedRoles)
            {
                roles = SecurityManager.Current.GetEffectiveRoles(roles);
            }
            return roles;
        }

        #endregion

        #endregion

        #region Authentication-related Methods

        /// <summary>
        /// Authenticates as (i.e. sets the security principal to) the user and role info associated with a specific WindowsId.
        /// </summary>
        /// <param name="windowsId"></param>
        /// <returns></returns>
        public static bool AuthenticateAsUser(string windowsId)
        {
            Person person = PersonController.GetByWindowsID(windowsId);
            if (person != null)
            {
                SecurityManager.CurrentUser = person.ToPrincipal();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Authenticates as (i.e. sets the security principal to) the user and role info associated with a specific WindowsId.
        /// </summary>
        /// <param name="windowsId"></param>
        /// <returns></returns>
        public static bool AuthenticateAsSupplierPortalUser(HttpRequest request, bool autoregisterIfNotFound)
        {
            if (!SupplierPortalUtility.IsAuthenticated(request))
            {
                return false;
            }

            string externalEmailAddress = SupplierPortalUtility.GetUserEmail(request);

            Person person = PersonController.GetByEmail(externalEmailAddress);

            if (autoregisterIfNotFound && (person == null))
            {
                person = AutoregisterSupplierPortalUser(externalEmailAddress, false);
            }

            if (person != null)
            {
                SecurityManager.CurrentUser = person.ToPrincipal();
                return true;
            }
            return false;
        }

        private static Person AutoregisterSupplierPortalUser(string externalEmailAddress, bool throwOnError)
        {
            try
            {
                Person person = new Person(true);

                PeopleFinder peopleFinder = new PeopleFinder();
                peopleFinder.Search(externalEmailAddress);
                Profiler profiler = peopleFinder.GetProfiler();
                if (profiler == null)
                {
                    throw new Exception(string.Format("PeopleFinder search failed. Profiler could not be obtained. Email: {0}.", externalEmailAddress));
                }

                person.WindowsId = profiler.Email;
                person.FirstName = profiler.FirstName;
                person.LastName = profiler.LastName;
                person.Email = profiler.Email;
                //person.Comment = "External User automatically created by the system";
                person.Save(profiler.Email);

                //Assign the UserRoleId.RestrictedAccess role to the new Person
                person.AddRole(UserRoleId.RestrictedAccess);

                return person;
            }
            catch (Exception ex)
            {
                string message = string.Format("Autoregistration of Supplier Portal user failed. Email: {0}.", externalEmailAddress);
                LogManager.Current.Log(Severity.Info, typeof(PersonController), message, ex);
                if (throwOnError)
                {
                    throw new Exception(message, ex);
                }
            }
            return null;
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Returns the <see cref="Person.WindowsId"/> value embedded within a specified <see cref="Person.Name"/> value.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ParseWindowsIDFromName(string name)
        {
            //ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(name, "name");
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            string s = name;
            int startIndexOfSubstring = s.LastIndexOf('(');
            int endIndexOfSubstring = s.LastIndexOf(')'); //int endIndexOfSubstring = s.LastIndexOf(')', ((startIndexOfSubstring < 0) ? 0 : startIndexOfSubstring));
            if ((startIndexOfSubstring > -1) && (startIndexOfSubstring < endIndexOfSubstring))
            {
                //NOTE: Since we know the substring starts and ends with a parenthesis char, we exclude those, too
                s = s.Substring(startIndexOfSubstring + 1, (endIndexOfSubstring - (startIndexOfSubstring + 1)));
                return s;
            }
            return null;
        }

        /// <summary>
        /// Returns a collection of alert/reminder/notification messages pertaining to a specified <see cref="Person"/>.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<string> GetSystemAlertMessages(int? personId)
        {
            List<string> messages = new List<string>();
            if (personId != null)
            {
                //TODO: Implement: Dynamic system alerts
#warning Not Implemented: Dynamic system alerts

                //NOTE: No dynamic system alerts are currently implemented

                //int overdueTaskCount = TaskController.FetchCount(new TaskQuerySpecification { AssignedToId = personId, IsResolved = false, IsDueBeforeNow = true });
                //if (overdueTaskCount > 0)
                //{
                //   if (overdueTaskCount == 1)
                //   {
                //      messages.Add(string.Format(CultureInfo.CurrentCulture, "<span style='font-size:large;background-color:Yellow;'>There is {0} overdue task assigned to you.</span>", overdueTaskCount));
                //   }
                //   else
                //   {
                //      messages.Add(string.Format(CultureInfo.CurrentCulture, "<span style='font-size:large;background-color:Yellow;'>There are {0} overdue tasks assigned to you.</span>", overdueTaskCount));
                //   }
                //}
            }
            return messages;
        }

        #endregion


        #region Import Methods

        #endregion
    }
}
