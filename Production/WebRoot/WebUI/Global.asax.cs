using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using HP.ElementsCPS.Apps.WebUI.MasterPages;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Core.Utility;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringAnalysis;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.HPFx.Utility;
using HP.HPFx.Utility.SubSonic;
using HP.HPFx.Web.UI;
using HP.HPFx.Web.Utility;
using SubSonic;

namespace HP.ElementsCPS.Apps.WebUI
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para>
    /// As indicated here (http://sandblogaspnet.blogspot.com/2008/03/methods-in-globalasax.html), 
    /// other events of possible interest include:
    ///     PostAuthenticateRequest
    ///     AuthorizeRequest
    ///     PostAuthorizeRequest
    ///     ResolveRequestCache
    ///     PostResolveRequestCache
    ///     PostMapRequestHandler
    ///     AcquireRequestState
    ///     PostAcquireRequestState
    ///     PreRequestHandlerExecute
    ///     PostRequestHandlerExecute
    ///     ReleaseRequestState
    ///     PostReleaseRequestState
    ///     UpdateRequestCache
    ///     PostUpdateRequestCache
    ///     LogRequest. (Supported in IIS 7.0 only.)
    ///     PostLogRequest (Supported in IIS 7.0 only.)
    /// </para>
    /// </remarks>
    public class Global : System.Web.HttpApplication
    {

        #region App-wide Constants

        public const string UrlParam_DataSourceId = ElementsCPSDataUtility.UrlParam_DataSourceId;
        public const string UrlParam_QuerySpecification = "query";
        public const string UrlParam_DefaultValuesSpecification = "defaults";

        #endregion

        #region AppSettings Properties

        public static bool EnableClientAccessToExceptionDetails
        {
            get
            {
                //TODO: Refactoring: Code Smell: Inconsistently formatted AppSettings key
#warning Refactoring: Code Smell: Inconsistently formatted AppSettings key
                const string key = "EnableClientAccess.ExceptionDetails"; //"HP.ElementsCPS.WebUI.Security.EnableClientAccess.ExceptionDetails";
                return ConfigurationManager.AppSettings[key].TryParseBoolean() ?? false;
            }
        }

        public static int ExportFileMaxPageSize
        {
            get
            {
                const int DefaultExportFileMaxPageSize = 1000;
                int pageSize = ConfigurationManager.AppSettings["HP.ElementsCPS.WebUI.ExportFileMaxPageSize"].TryParseInt32() ?? DefaultExportFileMaxPageSize;
                return pageSize;
            }
        }

        public static int ExportFileMaxRowCount
        {
            get
            {
                const int DefaultExportFileMaxRowCount = 1000000; //int.MaxValue;
                int maxRowCount = ConfigurationManager.AppSettings["HP.ElementsCPS.WebUI.ExportFileMaxRowCount"].TryParseInt32() ?? DefaultExportFileMaxRowCount;
                return maxRowCount;
            }
        }

        #endregion

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Diagnostics.Debugger.Launch();
            //added 28_05_24
            LogManager.Current.Log(Severity.Info, this, "Application Event: Application_Start.");
          
            QueryPaginationSpecification.DefaultPageSize = 20;

            SqlQuery.DefaultQueryCommandTimeout = 300;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            LogManager.Current.Log(Severity.Info, this, "Application Event: Session_Start.");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            LogManager.Current.Log(Severity.Debug, this, "Application Event: Application_BeginRequest.");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            LogManager.Current.Log(Severity.Debug, this, "Application Event: Application_AuthenticateRequest.");

            string currentUserWindowsId =  SecurityManager.CurrentUserIdentityName;
            //string currentUserWindowsId = "AUTH\\MuniyaRa";
            bool wasAuthenticated = PersonController.AuthenticateAsUser(currentUserWindowsId)
            || PersonController.AuthenticateAsSupplierPortalUser(this.Request, true);
            if (!wasAuthenticated)
            {
                SecurityManager.AuthenticateAsUnregisteredUser(currentUserWindowsId);
                RedirectToRegistrationPage();
            }
        }

        //protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //   LogManager.Current.Log(Severity.Debug, this, "Application Event: Application_PostAuthenticateRequest.");
        //}

        //protected void Application_AuthorizeRequest(object sender, EventArgs e)
        //{
        //   LogManager.Current.Log(Severity.Debug, this, "Application Event: Application_AuthorizeRequest.");
        //}

        //protected void Application_PostAuthorizeRequest(object sender, EventArgs e)
        //{
        //   LogManager.Current.Log(Severity.Debug, this, "Application Event: Application_PostAuthorizeRequest.");
        //}

        /// <summary>
        /// Logs "unhandled" errors that occur anywhere in the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = this.Server.GetLastError();
            string absoluteUri = WebUtility.GetCurrentRequestAbsoluteUri();
            string logMessage = string.Format("Application Event: Application_Error.\nURL: '{0}'.", absoluteUri);
            LogManager.Current.Log(this.GetApplicationErrorExceptionSeverity(exc), this, logMessage, exc);

            if (ExceptionUtility.IsOrContainsSqlTimeoutException(exc))
            {
                this.Context.Response.Redirect("~/ApplicationError.Timeout.html");
            }
            else if (ExceptionUtility.IsOrContainsExceptionOfType(exc, typeof(TimeoutException)))
            {
                this.Context.Response.Redirect("~/ApplicationError.Timeout.html");
            }
        }

        /// <summary>
        /// Utility method that uses app-specific logic to determine the most appropriate <see cref="Severity"/> with which to log 
        /// a specified <see cref="Exception"/> that has bubbled up to the <see cref="Application_Error"/> handler.
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        private Severity GetApplicationErrorExceptionSeverity(Exception exc)
        {
            try
            {
                if (WebUtility.IsViewStateException(exc))
                {
                    return Severity.Info;
                }
                else if (Global.IsHarmlessScriptResourceAxdIeBugException(exc))
                {
                    //NOTE: This case downgrades the severity of a special set of "ScriptResource.axd"-related requests that are related to an MS IE bug.
                    return Severity.Info;
                }
                else if (Global.IsHttpException_PageDoesNotExist(exc))
                {
                    //NOTE: This case downgrades the severity of exceptions caused by users manually entering invalid URLs.
                    return Severity.Info;
                }
                else if (ExceptionUtility.IsOrContainsSqlTimeoutException(exc))
                {
                    //NOTE: This case downgrades the severity of "DB Timeout expired" exceptions.
                    return Severity.Info;
                }
                else if (ExceptionUtility.IsOrContainsExceptionOfType(exc, typeof(TimeoutException)))
                {
                    return Severity.Info;
                }
                else if (exc.Message.StartsWith("Autoregistration of Supplier Portal user failed."))
                {
                    return Severity.Warn;
                }
            }
            catch (Exception ex2)
            {
                LogManager.Current.Log(Severity.Warn, this, "Logging: An Unexpected exception was thrown while attempting to log a different exception.", ex2);
                //throw; //silently consume the exception (except for the log entry above)
            }
            return Severity.Error;
        }

        protected void Session_End(object sender, EventArgs e)
        {
            LogManager.Current.Log(Severity.Debug, this, "Application Event: Session_End.");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            LogManager.Current.Log(Severity.Info, this, "Application Event: Application_End.");
        }

        #region Cacheing Methods

        #region Cacheing Utility Methods

        /// <summary>
        /// Convenience method.
        /// Safe accessor for the app's <see cref="Cache"/> object which will never throw a <see cref="NullReferenceException"/>.
        /// </summary>
        public static Cache AppCache
        {
            get
            {
                return GetCacheObject(HttpContext.Current);
            }
        }

        private static Cache GetCacheObject(HttpContext context)
        {
            Cache cache = context == null ? null : context.Cache;
            ValidateCacheObject(cache);
            return cache;
        }

        private static void ValidateCacheObject(Cache cache)
        {
            if (cache == null)
            {
                const string msg = "Cache access error. The Cache object could not be retrieved from the current HttpContext.";
                LogManager.Current.Log(Severity.Error, typeof(Global), msg);
                throw new InvalidOperationException(msg);
            }
        }

        public static void CacheClearAll()
        {
            //TODO: Implement: Primary code path
#warning Not Implemented: Primary code path
            throw new NotImplementedException("The invoked code path is not yet implemented.");
        }

        public static bool CacheContains(string key)
        {
            return AppCache[key] != null;
        }

        public static object CacheGet(string key)
        {
            return AppCache[key];
        }

        public static TResult CacheGetAs<TResult>(string key)
        {
            return (TResult)AppCache[key];
        }

        public static void CacheRemove(string key)
        {
            AppCache.Remove(key); //AppCache[key] = null;
        }

        /// <summary>
        /// Convenience method.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        public static void CacheAdd(string key, object item)
        {
            AppCache[key] = item; //AppCache.Add(key, item, ...);
        }

        /// <summary>
        /// Convenience method.
        /// </summary>
        public static void CacheInsert(string key, object item)
        {
            AppCache.Insert(key, item); //AppCache[key] = item;
        }

        /// <summary>
        /// Convenience method.
        /// </summary>
        public static void CacheInsert(string key, object item, TimeSpan? slidingExpiration, DateTime? absoluteExpiration, CacheItemPriority priority)
        {
            AppCache.Insert(key, item, null, absoluteExpiration ?? Cache.NoAbsoluteExpiration, slidingExpiration ?? Cache.NoSlidingExpiration, priority, null);
        }

        /// <summary>
        /// Convenience method.
        /// </summary>
        public static void CacheInsert(string key, object item, TimeSpan? slidingExpiration, TimeSpan? absoluteExpiration, CacheItemPriority priority)
        {
            DateTime expirationDate = absoluteExpiration.HasValue ? DateTime.UtcNow.Add(absoluteExpiration.Value) : Cache.NoAbsoluteExpiration;
            CacheInsert(key, item, slidingExpiration, expirationDate, priority);
        }

        #endregion

        #region DataSource Cacheing Methods

        public static string GenerateCacheKey_ListControlDataSource(string fromTable, string set)
        {
            return string.Format("ListControlDataSource;{0};{1}", fromTable, set);
        }

        private static void CacheListControlDataSource(string cacheKey, object dataSource)
        {
            CacheInsert(cacheKey, dataSource, (TimeSpan?)null, ElementsCPSCoreUtility.AppCacheAbsoluteExpirationDefault, CacheItemPriority.Default);
        }

        public static object GetCachedListControlDataSource_Id_Name(string fromTable)
        {
            string cacheKey = GenerateCacheKey_ListControlDataSource(fromTable, "all");
            //object dataSource = CacheGet(cacheKey);
            //if (dataSource == null)
            //{
            //   dataSource = ElementsCPSSqlUtility.GetListControlDataSource_Id_Name(fromTable); //rebuild the dataSource
            //   CacheListControlDataSource(cacheKey, dataSource);
            //}
            //return dataSource;
            Action<string, object, bool, Cache> cacheMutator =
                delegate(string key, object item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { CacheListControlDataSource(key, item); } };
            Func<object> itemAccessor =
                () => ElementsCPSSqlUtility.GetListControlDataSource_Id_Name(fromTable);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, itemAccessor);
        }

        public static object GetCachedListControlDataSource_Id_Name(string fromTable, string orderByColumn)
        {
            string cacheKey = GenerateCacheKey_ListControlDataSource(fromTable, "all");
            //object dataSource = CacheGet(cacheKey);
            //if (dataSource == null)
            //{
            //   dataSource = ElementsCPSSqlUtility.GetListControlDataSource_Id_Name(fromTable, null, orderByColumn); //rebuild the dataSource
            //   CacheListControlDataSource(cacheKey, dataSource);
            //}
            //return dataSource;
            Action<string, object, bool, Cache> cacheMutator =
                delegate(string key, object item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { CacheListControlDataSource(key, item); } };
            Func<object> itemAccessor =
                () => ElementsCPSSqlUtility.GetListControlDataSource_Id_Name(fromTable, null, orderByColumn);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, itemAccessor);
        }

        public static object GetCachedListControlDataSource_Id_Name(string fromTable, RowStatus.RowStatusId? rowStatusId)
        {
            if (rowStatusId == null)
            {
                return GetCachedListControlDataSource_Id_Name(fromTable);
            }

            string cacheKey = GenerateCacheKey_ListControlDataSource(fromTable, rowStatusId.Value.ToString("G"));
            //object dataSource = CacheGet(cacheKey);
            //if (dataSource == null)
            //{
            //   dataSource = ElementsCPSSqlUtility.GetListControlDataSource_Id_Name(fromTable, rowStatusId); //rebuild the dataSource
            //   CacheListControlDataSource(cacheKey, dataSource);
            //}
            //return dataSource;
            Action<string, object, bool, Cache> cacheMutator =
                delegate(string key, object item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { CacheListControlDataSource(key, item); } };
            Func<object> itemAccessor =
                () => ElementsCPSSqlUtility.GetListControlDataSource_Id_Name(fromTable, rowStatusId);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, itemAccessor);
        }

        public static object GetCachedListControlDataSource_Id_Name(string fromTable, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            if (rowStatusId == null)
            {
                return GetCachedListControlDataSource_Id_Name(fromTable);
            }

            string cacheKey = GenerateCacheKey_ListControlDataSource(fromTable, rowStatusId.Value.ToString("G"));

            Action<string, object, bool, Cache> cacheMutator =
                delegate(string key, object item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { CacheListControlDataSource(key, item); } };
            Func<object> itemAccessor =
                () => ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name(fromTable, rowStatusId, tenantId);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, itemAccessor);
        }
        public static object GetCachedListControlDataSource_Id_UniqueName(string fromTable)
        {
            string cacheKey = GenerateCacheKey_ListControlDataSource(fromTable, "all");
            //object dataSource = CacheGet(cacheKey);
            //if (dataSource == null)
            //{
            //   dataSource = ElementsCPSSqlUtility.GetListControlDataSource_Id_UniqueName(fromTable); //rebuild the dataSource
            //   CacheListControlDataSource(cacheKey, dataSource);
            //}
            //return dataSource;
            Action<string, object, bool, Cache> cacheMutator =
                delegate(string key, object item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { CacheListControlDataSource(key, item); } };
            Func<object> itemAccessor =
                () => ElementsCPSSqlUtility.GetListControlDataSource_Id_UniqueName(fromTable);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, itemAccessor);
        }

        public static object GetCachedListControlDataSource_Id_ViewUniqueName(string fromTable)
        {
            string cacheKey = GenerateCacheKey_ListControlDataSource(fromTable, "all");
            //object dataSource = CacheGet(cacheKey);
            //if (dataSource == null)
            //{
            //   dataSource = ElementsCPSSqlUtility.GetListControlDataSource_Id_ViewUniqueName(fromTable); //rebuild the dataSource
            //   CacheListControlDataSource(cacheKey, dataSource);
            //}
            //return dataSource;
            Action<string, object, bool, Cache> cacheMutator =
                delegate(string key, object item, bool wasCached, Cache cache) { if ((!wasCached) && (item != null)) { CacheListControlDataSource(key, item); } };
            Func<object> itemAccessor =
                () => ElementsCPSSqlUtility.GetListControlDataSource_Id_ViewUniqueName(fromTable);
            return WebUtility.GetCachedItemFromHttpContextCache(cacheKey, cacheMutator, itemAccessor);
        }

        public static List<UserRoleId> GetCurrentUserRolesFromCache()
        {
            //TODO: Implement: Cacheing portion of primary code path
#warning Not Implemented: Cacheing portion of primary code path

            //throw new NotImplementedException("The invoked code path is not yet implemented.");

            return PersonController.GetCurrentUserRoles(true);
        }

        /// <summary>
        /// When adding/updating reference data, remove the cache list control so it reloads with the new/updated data.
        /// </summary>
        /// <param name="fromTable"></param>
        /// <param name="rowStatusId"></param>
        /// <returns></returns>
        public static void RemoveCachedListControlDataSource(string fromTable, RowStatus.RowStatusId? rowStatusId)
        {
            // remove "all" cache
            string cacheKey = GenerateCacheKey_ListControlDataSource(fromTable, "all");
            CacheRemove(cacheKey);

            // remove "rowStatus" cache
            cacheKey = GenerateCacheKey_ListControlDataSource(fromTable, rowStatusId.Value.ToString("G"));
            CacheRemove(cacheKey);
        }

        #endregion

        #endregion

        #region ListControl Methods

        #region ListItem Text Constants

        public static string GetSelectListText()
        {
            return "- Select -";
        }

        public static string GetAllListText()
        {
            return "- All -";
        }

        public static string GetNoneListText()
        {
            return "- None -";
        }

        public static string GetEverywhereListText()
        {
            return "- Everywhere -";
        }

        #endregion

        #region ListControl Utility Methods

        /// <summary>
        /// Convenience overload.
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public static bool ForceSelectedValue(ListControl lc, Person person)
        {
            return lc.ForceSelectedValue(person.Id.ToString(), person.Name);
        }

        /// <summary>
        /// An overload-like convenience method that performs the specific ListControl DataBinding operation that is common to many different ListControls.
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="dataSource"></param>
        private static void DataBindListControl_Id_Name(ListControl lc, object dataSource)
        {
            const string dataValueField = "Id";
            const string dataTextField = "Name";
            lc.BindToDataSource(dataSource, dataTextField, dataValueField, false, true);
        }

        /// <summary>
        /// An overload-like convenience method that performs the specific ListControl DataBinding operation that is common to many different ListControls.
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="dataSource"></param>
        private static void DataBindListControl_Name_Name(ListControl lc, object dataSource)
        {
            const string dataValueField = "Name";
            const string dataTextField = "Name";
            lc.BindToDataSource(dataSource, dataTextField, dataValueField, false, true);
        }

        /// <summary>
        /// An overload-like convenience method that performs the specific ListControl DataBinding operation that is common to many different ListControls.
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="dataSource"></param>
        private static void DataBindListControl_ElementsKey_ElementsKey(ListControl lc, object dataSource)
        {
            const string dataValueField = "ElementsKey";
            const string dataTextField = "ElementsKey";
            lc.BindToDataSource(dataSource, dataTextField, dataValueField, false, true);
        }

        /// <summary>
        /// An overload-like convenience method that performs the specific ListControl DataBinding operation that is common to many different ListControls.
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="dataSource"></param>
        private static void DataBindListControl_Id_UniqueName(ListControl lc, object dataSource)
        {
            const string dataValueField = "Id";
            const string dataTextField = "UniqueName";
            lc.BindToDataSource(dataSource, dataTextField, dataValueField, false, true);
        }

        /// <summary>
        /// An overload-like convenience method that performs the specific ListControl DataBinding operation that is common to many different ListControls.
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="dataSource"></param>
        private static void DataBindListControl_Id_ViewUniqueName(ListControl lc, object dataSource)
        {
            const string dataValueField = "Id";
            const string dataTextField = "ViewUniqueName";
            lc.BindToDataSource(dataSource, dataTextField, dataValueField, false, true);
        }

        //public static void BindListControl(ListControl lc, ElementsCPSSqlUtility.ListTable tableName, ElementsCPSSqlUtility.ListDisplay listDisplay)
        //{
        //   lc.BindToDataSource(ElementsCPSSqlUtility.GetDataSource(tableName, listDisplay), listDisplay.ToString(), "Id", false, true);
        //}

        public static ListItemCollection GetSelectedItems(ListControl listControl)
        {
            ListItemCollection selectedItems = new ListItemCollection();
            foreach (ListItem item in listControl.Items)
            {
                if (item.Selected)
                {
                    selectedItems.Add(item);
                }
            }
            return selectedItems;
        }

        public delegate TReturnType ConvertToType<TReturnType>(string value);

        public static List<TValueType> GetSelectedValues<TValueType>(ListControl listControl, ConvertToType<TValueType> convertMethod)
        {
            List<TValueType> selectValues = new List<TValueType>();
            foreach (ListItem item in listControl.Items)
            {
                if (item.Selected)
                {
                    selectValues.Add(convertMethod(item.Value));
                }
            }
            return selectValues;
        }

        public static void SetListControlSelectedValues(ListControl lc, IEnumerable<string> selectedValues)
        {
            foreach (string s in selectedValues)
            {
                for (int i = 0; i < lc.Items.Count; i++)
                {
                    if (lc.Items[i].Value == s)
                    {
                        lc.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }


        #endregion

        #region ListControl Binding Methods

        #region Specialized Binding Methods

        /// <summary>
        /// Bind Bool value on the List Control, display as Yes/No.
        /// </summary>
        /// <param name="lc"></param>
        public static void BindBooleanListControl(ListControl lc)
        {
            lc.ClearItems();
            lc.InsertItem(0, "False", "No");
            lc.InsertItem(1, "True", "Yes");
        }

        /// <summary>
        /// Bind a comma-separated values string to a list control
        /// </summary>
        /// <param name="lc"></param>   
        /// <param name="csValues"></param>
        public static void BindCommaDelimitedListControl(ListControl lc, string csValues)
        {
            lc.ClearItems();
            ArrayList list = new ArrayList();
            if (csValues!=null)
            {
                 list.AddRange(csValues.Split(new char[] { ',' }));
                 for (int i = 0; i < list.Count; i++)
                 {
                     lc.Items.Add(list[i].ToString());
                 }
            }
           
           
        }

        #region Lifecycle State Binding Methods

        #endregion

        #endregion

        #region Standard Binding Methods

        public static void BindRowStatusListControl(ListControl lc)
        {
            DataBindListControl_Id_Name(lc, ElementsCPSSqlUtility.GetListControlDataSource_Id_Name("RowStatus"));
        }

        public static void BindPersonListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("Person", rowStatusId));
        }

        public static void BindRoleList2Control(ListControl lc, ListControl lc1, ListControl lc2, ListControl lc3, ListControl lc4, RowStatus.RowStatusId? rowStatusId, bool disableAdminForNonAdmins)
        {
            RoleCollection roleCol = RoleController.FetchBySearchCriteria("Name", rowStatusId);
            RoleCollection userRoleDomain = new RoleCollection();
            ApplicationCollection appCol = ApplicationController.FetchBySearchCriteria("Name", rowStatusId);
            //RoleCollection userRoleAction = new RoleCollection();
            //ApplicationCollection appcol1 = Role.GetApplicationCollection((int)UserRoleId.Viewer);
            //ApplicationCollection appcol2 = Role.GetApplicationCollection((int)UserRoleId.Editor);
            //ApplicationCollection appcol3 = Role.GetApplicationCollection((int)UserRoleId.Validator);
            //ApplicationCollection appcol4 = Role.GetApplicationCollection((int)UserRoleId.DataAdmin);
            foreach (Role userRole in roleCol)
            {
                if (userRole.UserRoleId == UserRoleId.Viewer || userRole.UserRoleId == UserRoleId.Editor || userRole.UserRoleId == UserRoleId.Validator || userRole.UserRoleId == UserRoleId.DataAdmin)
                {
                    //userRoleAction.Add(userRole);
                }
                else
                {
                    userRoleDomain.Add(userRole);
                }

            }
            DataBindListControl_Id_Name(lc, userRoleDomain);
            //DataBindListControl_Id_Name(lc1, userRoleAction);
            DataBindListControl_Id_Name(lc1, appCol);
            DataBindListControl_Id_Name(lc2, appCol);
            DataBindListControl_Id_Name(lc3, appCol);
            DataBindListControl_Id_Name(lc4, appCol);
            if (disableAdminForNonAdmins)
            {
                if (!SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator))
                {
                    ListItem liAdministrator =
                         lc.Items.FindByValue(UserRoleId.Administrator.ToString("D"));
                    liAdministrator.Enabled = false;
                }
            }
        }

        public static void BindRoleListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, bool disableAdminForNonAdmins)
        {
            DataBindListControl_Id_Name(lc, RoleController.FetchBySearchCriteria("Name", rowStatusId));
            if (disableAdminForNonAdmins)
            {
                if (!SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator))
                {
                    ListItem liAdministrator =
                         lc.Items.FindByValue(UserRoleId.Administrator.ToString("D"));
                    liAdministrator.Enabled = false;
                }
            }
        }
        public static void BindAppClientListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            DataBindListControl_Id_Name(lc, AppClientController.FetchBySearchCriteria("Name", rowStatusId));
            //List<AppClientId> appClientids = TenantController.GetAppClients(tenantId);

            List<int> allSelectedClients = TenantAppClientController.GetDistinctColumnValueInts("AppClientId", null, null);
            foreach (int str in allSelectedClients)
            {
                ListItem li = lc.Items.FindByValue(str.ToString());
                li.Enabled = false;
            }

            List<int> thisSelectedClients = TenantController.GetAppClientsInt(tenantId);
            //IEnumerable<int> otherappclient = allSelectedClients.Except(appclients);
            foreach (int str in thisSelectedClients)
            {
                ListItem li = lc.Items.FindByValue(str.ToString());
                li.Enabled = true;

            }
        }

        public static void BindLogSeverityListControl(ListControl lc)
        {
            lc.BindToEnum(typeof(Severity));
        }

        public static void BindEntityTypeListControl(ListControl lc)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("EntityType"));
            //DataBindListControl_Id_Name(lc, EntityTypeController.FetchAll("Name"));
        }

        public static void BindNoteTypeListControl(ListControl lc)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("NoteType"));
            //DataBindListControl_Id_Name(lc, NoteTypeController.FetchAll("Name"));
        }

        public static void BindRoleListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("Role", rowStatusId));
        }


        #endregion

        #endregion

        #region ListControl Selection Initialization Methods

        public static void SetRoleListControl(ListControl lc, RoleCollection roles)
        {
            Global.SetListControlSelectedValues(lc, roles.Select(c => c.Id.ToString()));
        }

        public static void SetApplicationListControl(ListControl lc, ApplicationCollection Applications)
        {
            Global.SetListControlSelectedValues(lc, Applications.Select(c => c.Id.ToString()));
        }

        public static void SetAppClientListControl(ListControl lc, AppClientCollection clients)
        {
            Global.SetListControlSelectedValues(lc, clients.Select(c => c.Id.ToString()));
        }
        #endregion

        #endregion

        #region AJAX Toolkit CascadingDropDown Methods

        #region AJAX Toolkit CascadingDropDown Utility Methods

        /// <summary>
        /// Convenience method. Sets the specified control's  <see cref="CascadingDropDown.SelectedValue"/> to <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="cddl"></param>
        /// <seealso cref="ListControl.ClearSelection"/>
        public static void ClearSelection(CascadingDropDown cddl)
        {
            //TODO: Refactoring: Migrate to HPFx
#warning Refactoring: Migrate to HPFx

            //TODO: Refactoring: Convert this method into an extension method
#warning Refactoring: Convert this method into an extension method

            cddl.SelectedValue = ""; //cddl.ClearSelection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cddl"></param>
        /// <param name="itemValue"></param>
        /// <param name="itemText"></param>
        /// <returns></returns>
        [Obsolete("Untested.", true)]
        public static void ForceSelectedValue(CascadingDropDown cddl, string itemValue, string itemText)
        {
            //TODO: Refactoring: Migrate to HPFx
#warning Refactoring: Migrate to HPFx

            //TODO: Refactoring: Convert this method into an extension method
#warning Refactoring: Convert this method into an extension method

            string displayText = itemText.TrimToNull() ?? itemValue.TrimToNull() ?? ""; //string displayText = (string.IsNullOrEmpty(itemText) ? itemValue : itemText);

            //change the control's PromptValue and PromptText to match the specified Value and Text
            cddl.PromptValue = itemValue;
            cddl.PromptText = displayText;

            //set the control's SelectedValue
            cddl.SelectedValue = itemValue;
        }

        #endregion

        #endregion

        #region Error Handling Utility Methods

        /// <summary>
        /// Analyzes an exception to determine whether it is "recognized" and has a corresponding user-friendly error message that can be displayed to users.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="excludeGenericMessages"></param>
        /// <returns></returns>
        public static string GetUserFriendlyExceptionMessage(Exception ex, bool excludeGenericMessages)
        {
            if (ex == null)
            {
                return null;
            }

            if (ex is ThreadAbortException)
            {
                //NOTE: we intentionally treat a ThreadAbortException as unrecognized, because these are almost always the result of invoking Response.Redirect(), and aren't really an error
                return null;
            }

            string userFriendlyExceptionMessage = null;

            //delegate to the ElementsCPSDataUtility.GetUserFriendlyExceptionMessage method (excluding generic messages) first
            userFriendlyExceptionMessage = ElementsCPSDataUtility.GetUserFriendlyExceptionMessage(ex, true);
            if (!string.IsNullOrEmpty(userFriendlyExceptionMessage))
            {
                return userFriendlyExceptionMessage;
            }

            //if no message was found yet, then attempt to determine an appropriate "non-generic" UI message

            //TODO: Implement: UI-specific non-generic error message logic
#warning Not Implemented: UI-specific non-generic error message logic

            //List<HttpException> httpExceptions = ExceptionUtility.GetExceptionsOfType<HttpException>(ex).ToList();
            //if (httpExceptions.Count > 0)
            //{
            //   bool isHttpException_FileDoesNotExist =
            //      (httpExceptions.Where(sqlEx => sqlEx.Message.StartsWith("The file '")).Where(sqlEx => sqlEx.Message.EndsWith("' does not exist")).Count() > 0);
            //   if (isHttpException_FileDoesNotExist)
            //   {
            //      return "Invalid URL. The page specified in the HTTP Request does not exist.";
            //   }
            //}

            if (!excludeGenericMessages)
            {
                //if no message was found yet, then delegate to the ElementsCPSDataUtility.GetUserFriendlyExceptionMessage method (allowing generic messages)
                userFriendlyExceptionMessage = ElementsCPSDataUtility.GetUserFriendlyExceptionMessage(ex, false);
                if (!string.IsNullOrEmpty(userFriendlyExceptionMessage))
                {
                    return userFriendlyExceptionMessage;
                }

                //if no message was found yet, then attempt to determine an appropriate "generic" UI message

                //TODO: Implement: UI-specific generic error message logic
#warning Not Implemented: UI-specific generic error message logic

                return "The attempted action caused an error.";
            }

            //The exception was unrecognized. Therefore, no user friendly message is available.
            return null;
        }

        #endregion

        #region Dynamic HTML Methods

        public static string CreateHtml_ClearFiltersButton(string tagId)
        {
            //TODO: Review Needed: Review implementation to decide whether to fix or just remove completely
#warning Review Needed: Review implementation to decide whether to fix or just remove completely

            //HACK: Return an empty string for now since using a Refresh button as a Clear button won't work when ListPanels are updated without an auto-redirect (which happens for several reasons)
            return "";

            //return CreateHtml_RefreshButton(tagId, "Clear", "Clear/reset all search parameters");
        }

        /// <summary>
        /// Creates an HTML tag for an INPUT Button that will cause a client-side redirect to a specified URL.
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="buttonText"></param>
        /// <param name="buttonTooltip"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public static string CreateHtml_RedirectButton(string tagId, string buttonText, string buttonTooltip, string redirectUrl)
        {
            return string.Format(CultureInfo.InvariantCulture,
                 @"<input id='{0}' type='button' value='{1}' title='{2}' onclick='javascript:location=""{3}""; return false;' />",
                 tagId,
                 buttonText,
                 buttonTooltip,
                 redirectUrl);
        }

        /// <summary>
        /// Creates an HTML tag for an INPUT Button that will cause a client-side Refresh of the current page.
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="buttonText"></param>
        /// <param name="buttonTooltip"></param>
        /// <returns></returns>
        public static string CreateHtml_RefreshButton(string tagId, string buttonText, string buttonTooltip)
        {
            return string.Format(CultureInfo.InvariantCulture,
                 @"<input id='{0}' type='button' value='{1}' title='{2}' onclick='javascript:location.reload(true); return false;' />",
                 tagId,
                 buttonText,
                 buttonTooltip);
        }

        public static string CreateHtml_ResetFormButton(string tagId)
        {
#warning AJAX Problem: Using reset buttons causes "Invalid postback or callback argument exception" errors during partial page postbacks
            //HACK: Return an empty string for now since the reset button would allow users to create worse runtime problems than not having the button would.
            return "";
            //return string.Format(CultureInfo.InvariantCulture, 
            //    "<input id='{0}' type='reset' value='Undo Changes' title='Undo any (unsubmitted) changes to the search parameters' />", tagId);
        }

        public static string ConditionalHtml(bool condition, string valueIfTrue, string valueIfFalse)
        {
            return condition ? valueIfTrue : valueIfFalse;
        }

        public static string ConditionalHtml_IfUserHasRole(UserRoleId role, string valueIfTrue, string valueIfFalse)
        {
            bool currentUserHasRole = SecurityManager.IsCurrentUserInRole(role);
            return ConditionalHtml(currentUserHasRole, valueIfTrue, valueIfFalse);
        }

        public static string ConditionalHtml_IfUserHasRole(UserRoleId role, string valueIfTrue)
        {
            return ConditionalHtml_IfUserHasRole(role, valueIfTrue, "");
        }

        public static string ConditionalHtml_IfUserLacksRole(UserRoleId role, string valueIfTrue, string valueIfFalse)
        {
            return ConditionalHtml_IfUserHasRole(role, valueIfFalse, valueIfTrue);
        }

        public static string ConditionalHtml_IfUserLacksRole(UserRoleId role, string valueIfTrue)
        {
            return ConditionalHtml_IfUserLacksRole(role, valueIfTrue, "");
        }

        public static string ConditionalHtml_IfUserHasRole_StyleDisplayNone(UserRoleId role)
        {
            return ConditionalHtml_IfUserHasRole(role, " style='display:none;' ", "");
        }

        public static string ConditionalHtml_IfUserLacksRole_StyleDisplayNone(UserRoleId role)
        {
            return ConditionalHtml_IfUserLacksRole(role, " style='display:none;' ", "");
        }

        #endregion

        #region Dynamic URL Methods

        public static string GetRegistrationPageUri()
        {
            //return "~/Registration/Register.aspx";
            return "~/ElementsCPS/WEBUI";
        }

        public static string GetSessionStartLogListPageUri()
        {
            int? currentUserId = PersonController.GetCurrentUserId();
            LogQuerySpecification qs = new LogQuerySpecification()
                {
                    Logger = "HP.ElementsCPS.Apps.WebUI.Global.ASP.global_asax",
                    Location = "at System.Web.SessionState.SessionStateModule.RaiseOnStart(EventArgs e)"
                };
            return GetLogListPageUri(qs);
        }

        public static string GetUserProfilePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/UserProfile/MyProfile.aspx", recordId, defaultValuesSpecification);
        }

        #region URL Modification Methods

        /// <summary>
        /// Modifies a specified <see cref="Uri"/> so that the URI's QueryString contains instructions to enable the app-wide "Content Only" Display Mode.
        /// </summary>
        /// <remarks>
        /// NOTE: This method may cause unpredictable results if it is used to modify a URI already has the "Content Only" Display Mode enabled.
        /// </remarks>
        /// <param name="uri">The URI to be modified.</param>
        /// <returns>A new URI.</returns>
        public static string EnableContentOnlyDisplayMode(Uri uri)
        {
            return WebUtility.AddToQuery(uri, "co=y");
        }

        /// <summary>
        /// Convenience overload.
        /// </summary>
        public static string EnableContentOnlyDisplayMode(string url)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(url, "url");
            return EnableContentOnlyDisplayMode(new Uri(url));
        }

        #endregion

        #region Generate...SummariesOverlayPageUri Methods

        private static string GenerateStandardSummariesListOverlayPageUri(string pageUrl, IQuerySpecification query)
        {
            string listPageUri = pageUrl;
            if (query != null)
            {
                string filteredListPageUri = string.Format("{0}?{1}={2}", pageUrl, UrlParam_QuerySpecification, HttpUtility.UrlEncode(HttpUtility.UrlEncode(query.ToString())));
                listPageUri = filteredListPageUri;
            }

            //simply return null if a valid URI can't be generated
            if (!WebUtility.IsUriLengthValid(listPageUri))
            {
                listPageUri = null;
            }

            //convert the Uri to an absolute Uri
            listPageUri = WebUtility.ResolveApplicationRelativeUrl(listPageUri).PathAndQuery;

            return listPageUri;
        }

        public static string GetNoteSummariesOverlayPageUri(IQuerySpecification query)
        {
            return GenerateStandardSummariesListOverlayPageUri("~/DataAdmin/NoteSummariesOverlay.aspx", query);
        }

        //public static string GetPersonSummariesOverlayPageUri(IQuerySpecification query)
        //{
        //   return GenerateStandardSummariesListOverlayPageUri("~/UserAdmin/PersonSummariesOverlay.aspx", query);
        //}

        #endregion

        #region Generate...SummaryOverlayPageUri Methods

        private static string GenerateStandardSummaryOverlayRecordPageUri(string pageUrl, int? recordId)
        {
            string pageUri = GenerateStandardUpdateRecordPageUri(pageUrl, recordId, null);
            //convert the Uri to an absolute Uri
            pageUri = WebUtility.ResolveApplicationRelativeUrl(pageUri).PathAndQuery;
            return pageUri;
        }

        public static string GetNoteSummaryOverlayPageUri(int? recordId)
        {
            return GenerateStandardSummaryOverlayRecordPageUri("~/DataAdmin/NoteSummaryOverlay.aspx", recordId);
        }

        public static string GetPersonSummaryOverlayPageUri(int? recordId)
        {
            return GenerateStandardSummaryOverlayRecordPageUri("~/UserAdmin/PersonSummaryOverlay.aspx", recordId);
        }

        #endregion

        #region Generate...ListPageUri Methods

        private static string GenerateStandardListPageUri(string pageUrl, IQuerySpecification query)
        {
            string listPageUri = pageUrl;
            if (query != null)
            {
                string filteredListPageUri = string.Format("{0}?{1}={2}", pageUrl, UrlParam_QuerySpecification, HttpUtility.UrlEncode(HttpUtility.UrlEncode(query.ToString())));
                listPageUri = filteredListPageUri;
            }

            //simply return null if a valid URI can't be generated
            if (!WebUtility.IsUriLengthValid(listPageUri))
            {
                listPageUri = null;
            }

            return listPageUri;
        }

        #region Custom Generate...ListPageUri Methods

        #endregion

        #region Basic Generate...ListPageUri Methods

        public static string GetEntityTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/EntityTypeList.aspx", query);
        }

        public static string GetLogListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/SystemAdmin/LogList.aspx", query);
        }

        public static string GetNoteListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/NoteList.aspx", query);
        }

        public static string GetNoteTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/NoteTypeList.aspx", query);
        }

        public static string GetPersonListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/UserAdmin/PersonList.aspx", query);
        }

        public static string GetAppClientListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/AppClientList.aspx", query);
        }

        public static string GetApplicationListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ApplicationList.aspx", query);
        }
        public static string GetTenantGroupListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/TenantGroupList.aspx", query);
        }

        #endregion

        #endregion

        #region Generate...UpdatePageUri Methods

        #region Custom Generate...UpdatePageUri Methods

        public static string GetMyPersonalInfoUri()
        {
            //return Global.GetPersonDetailPageUri(PersonController.GetCurrentUserId(), null);
            return "~/MyInfo/PersonDetailForMyInfo.aspx";
        }

        #endregion

        #region Basic Generate...UpdatePageUri Methods

        /// <summary>
        /// Generates a complete Uri for an ElementsCPS "Record Update" (or "Record Detail") page.
        /// </summary>
        /// <param name="pageUrl">The URL of the page (excluding the QueryString).</param>
        /// <param name="recordId">The ID of the specific record the page should display when the URL is accessed.</param>
        /// <param name="defaultValuesSpecification">
        /// <c>null</c>, or an <see cref="IQuerySpecification"/> indicating the desired "default field values" 
        /// that should be used by the Update page (if the page determines that any default values are needed and appropriate).
        /// </param>
        /// <returns></returns>
        private static string GenerateStandardUpdateRecordPageUri(string pageUrl, int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            if ((defaultValuesSpecification != null) && (defaultValuesSpecification.Conditions.Count > 0))
            {
                //clear any unneeded data from the object to reduce it's serialized size
                defaultValuesSpecification = defaultValuesSpecification.InnerData.Copy();
                defaultValuesSpecification.Paging.Clear();
                defaultValuesSpecification.SortBy.Clear();
            }
            else
            {
                defaultValuesSpecification = null;
            }

            string updateRecordPageUri = pageUrl;
            List<string> queryParams = new List<string>(2);
            if (recordId != null)
            {
                queryParams.Add(string.Format("{0}={1}", UrlParam_DataSourceId, recordId.Value));
            }
            if ((defaultValuesSpecification != null) && (defaultValuesSpecification.Conditions.Count > 0))
            {
                queryParams.Add(string.Format("{0}={1}", UrlParam_DefaultValuesSpecification, HttpUtility.UrlEncode(HttpUtility.UrlEncode(defaultValuesSpecification.ToString()))));
            }
            if (queryParams.Count > 0)
            {
                //append the queryParams to the URL
                string newUri = string.Format("{0}?{1}", pageUrl, string.Join("&", queryParams.ToArray()));
                if ((!WebUtility.IsUriLengthValid(updateRecordPageUri)) && (defaultValuesSpecification != null))
                {
                    //NOTE: If the defaultValuesSpecification makes the URL too long, then ignore it
                    return GenerateStandardUpdateRecordPageUri(pageUrl, recordId, null);
                }
                updateRecordPageUri = newUri;
            }
            return updateRecordPageUri;
        }

        private static string GenerateStandardMultiUpdateRecordPageUri(string pageUrl, IQuerySpecification editSetSpecification)
        {
            if ((editSetSpecification != null) && (editSetSpecification.Conditions.Count > 0))
            {
                //clear any unneeded data from the object to reduce it's serialized size
                editSetSpecification = editSetSpecification.InnerData.Copy();
                editSetSpecification.Paging.Clear();
                editSetSpecification.SortBy.Clear();
            }
            else
            {
                editSetSpecification = null;
            }

            string updateRecordPageUri = pageUrl;
            List<string> queryParams = new List<string>(2);
            if ((editSetSpecification != null) && (editSetSpecification.Conditions.Count > 0))
            {
                queryParams.Add(string.Format("{0}={1}", UrlParam_QuerySpecification, HttpUtility.UrlEncode(HttpUtility.UrlEncode(editSetSpecification.ToString()))));
            }
            if (queryParams.Count > 0)
            {
                //append the queryParams to the URL
                string newUri = string.Format("{0}?{1}", pageUrl, string.Join("&", queryParams.ToArray()));
                if ((!WebUtility.IsUriLengthValid(updateRecordPageUri)) && (editSetSpecification != null))
                {
                    //NOTE: If the editSetSpecification makes the URL too long, then ignore it
#warning Not Implemented: Where do we go if the URL is too long
                    throw new NotImplementedException("No where to go for exta long URI.");
                    //return GenerateStandardMultiUpdateRecordPageUri(pageUrl, recordId, null);
                }
                updateRecordPageUri = newUri;
            }
            return updateRecordPageUri;
        }

        public static string GetEntityTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/EntityTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetLogDetailPageUri(int? recordId)
        {
            return GenerateStandardUpdateRecordPageUri("~/SystemAdmin/LogDetail.aspx", recordId, null);
        }

        public static string GetNoteUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/NoteUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetNoteDetailPageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/NoteDetail.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetNoteTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/NoteTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetPersonDetailPageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/UserAdmin/PersonDetail.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetPersonUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/UserAdmin/PersonUpdate.aspx", recordId, defaultValuesSpecification);
        }

        //public static string GetTenantGroupPageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        //{
        //    return GenerateStandardUpdateRecordPageUri("~/DataAdmin/TenantGroupUpdate.aspx", recordId, defaultValuesSpecification);
        //}


        #endregion

        #endregion

        #endregion

        #region Dynamic Title, Header, and Filename Methods

        public static string GenerateStandardListPageTitle(string entityName, IQuerySpecification qs)
        {
            string title = string.Format("List {0}", entityName);
            if (qs.Conditions.Count > 0)
            {
                title += " (Filtered)";
            }
            if (qs.SortBy.Count > 0)
            {
                string[] sortExpressions = qs.SortBy.Select(
                     qsd => (qsd.SortExpression + ((qsd.SortDescending != true) ? "" : " (Descending)"))).ToArray();
                string sortInfoSuffix = string.Join(", ", sortExpressions);
                title += string.Format(" by {0}", sortInfoSuffix);
            }
            if (qs.Paging.PageIndex > 0)
            {
                string pageInfoSuffix = string.Format("Page {0}", qs.Paging.PageIndex + 1);
                title += string.Format(" - {0}", pageInfoSuffix);
            }
            return title;
        }

        public static string GenerateStandardExportFilename(string entityName, IQuerySpecification qs, bool includeConditionsDetails, bool includeSortByDetails, bool includePaginationDetails, bool includeTimestamp)
        {
            string title = string.Format("ElementsCPSData.{0}", entityName);
            if (includeConditionsDetails && (qs.Conditions.Count > 0))
            {
                title += ".Filtered";
            }
            if (includeSortByDetails && (qs.SortBy.Count > 0))
            {
                string[] sortExpressions = qs.SortBy.Select(
                     qsd => (qsd.SortExpression + ((qsd.SortDescending != true) ? "" : " Desc"))).ToArray();
                string sortInfoSuffix = string.Join(" ", sortExpressions);
                title += string.Format(".Sorted By {0}", sortInfoSuffix);
            }
            if (includePaginationDetails && (qs.Paging.PageIndex > 0))
            {
                string pageInfoSuffix = string.Format("Page {0}", qs.Paging.PageIndex + 1);
                title += string.Format(".{0}", pageInfoSuffix);
            }
            if (includeTimestamp)
            {
                title += string.Format(".{0:yyyyMMdd_HHmmss}", DateTime.UtcNow);
            }
            return title;
        }

        /// <summary>
        /// Convenience overload.
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static string GenerateStandardExportFilename(string entityName, IQuerySpecification qs)
        {
            return GenerateStandardExportFilename(entityName, qs, true, true, false, true);
        }

        #endregion

        #region Filter Control Methods

        #region InitializeFilter_... Methods

        public static void InitializeFilter_DateOnly(ITextControl filterControl, DateTime? date)
        {
            //if ((date != null) && (!date.Value.TimeOfDay.Equals(date.Value.Date.TimeOfDay)))
            //{
            //    filterControl.Text = date.Value.ToString("u");
            //}
            //else
            //{
            filterControl.Text = (date == null) ? "" : date.Value.ToString("d");
            //}
        }

        public static void InitializeFilter_DateTime(ITextControl dateFilterControl, ITextControl timeFilterControl, DateTime? date)
        {
            //if ((date != null) && (!date.Value.TimeOfDay.Equals(date.Value.Date.TimeOfDay)))
            //{
            //    dateFilterControl.Text = date.Value.ToString("u");
            //}
            //else
            //{
            dateFilterControl.Text = (date == null) ? "" : date.Value.ToString("d");
            //timeFilterControl.Text = ((date == null) || (date.Value == date.Value.Date)) ? "" : date.Value.ToString("T");
            timeFilterControl.Text = ((date == null) || (date.Value == date.Value.Date)) ? "" : date.Value.ToString("hh:mm:ss.FFF tt %K");
            //}
        }

        public static void InitializeFilter_CreatedByFilter(ITextControl filterControl, string windowsId)
        {
            string filterText = null;
            if (!string.IsNullOrEmpty(windowsId))
            {
                filterText = PersonController.GetPersonNameByWindowsId(windowsId);
                if (string.IsNullOrEmpty(filterText))
                {
                    //could not convert the windowsId to a Person.Name, so use the windowsId value instead
                    filterText = windowsId;
                }
            }
            filterControl.Text = filterText;
        }

        public static void InitializeFilter_ModifiedByFilter(ITextControl filterControl, string windowsId)
        {
            //use the exact same logic as we would for a CreatedBy filter
            InitializeFilter_CreatedByFilter(filterControl, windowsId);
        }

        #endregion

        #region ReadFilterValue_... Methods

        public static DateTime? ReadFilterValue_DateOnly(ITextControl filterControl)
        {
            string dateString = filterControl.Text;
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime d;
                if (DateTime.TryParseExact(dateString, new[] { "MM/dd/yy", "M/d/yy", "MM/dd/yyyy", "M/d/yyyy" }, null, DateTimeStyles.None, out d))
                {
                    //handled below
                }
                else if (DateTime.TryParse(dateString, out d))
                {
                    //handled below
                }
                else
                {
                    return null;
                }
                d = DateTime.SpecifyKind(d, DateTimeKind.Utc);
                return d;
            }
            return null;
        }

        public static DateTime? ReadFilterValue_DateTime(ITextControl dateFilterControl, ITextControl timeFilterControl)
        {
            string dateString = dateFilterControl.Text;
            string timeString = timeFilterControl.Text;
            DateTime? dateOnly = null;
            DateTime? timeOnly = null;

            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime d;
                if (DateTime.TryParseExact(dateString, new[] { "MM/dd/yy", "M/d/yy", "MM/dd/yyyy", "M/d/yyyy" }, null, DateTimeStyles.None, out d))
                {
                    d = DateTime.SpecifyKind(d, DateTimeKind.Utc);
                    dateOnly = d;
                }
                else if (DateTime.TryParse(dateString, out d))
                {
                    d = DateTime.SpecifyKind(d, DateTimeKind.Utc);
                    dateOnly = d;
                }
                else
                {
                    dateOnly = null;
                }
                //d = DateTime.SpecifyKind(d, DateTimeKind.Utc);
                //return d;
            }
            //NOTE: We don't need to parse the time portion if the date portion is null, because the time won't matter in that case anyway
            if (!string.IsNullOrEmpty(timeString) && (dateOnly != null))
            {
                DateTime t;
                if (DateTime.TryParseExact(timeString, new[] { "hh:mm:ss.FFF tt K", "hh:mm:ss.FFF tt", "hh:mm:ss tt", "hh:mm:ss", "hh:mm tt", "hh.mm.ss.FFF tt K", "hh.mm.ss.FFF tt", "hh.mm.ss tt", "hh.mm.ss", "hh.mm tt" }, null, DateTimeStyles.None, out t))
                {
                    t = DateTime.SpecifyKind(t, DateTimeKind.Utc);
                    timeOnly = t;
                }
                else
                    if (DateTime.TryParse(timeString, out t))
                    {
                        t = DateTime.SpecifyKind(t, DateTimeKind.Utc);
                        timeOnly = t;
                    }
                    else
                    {
                        timeOnly = null;
                    }
                //t = DateTime.SpecifyKind(t, DateTimeKind.Utc);
                //return t;
            }

            DateTime? dateAndTime = dateOnly;
            if ((dateAndTime != null) && (timeOnly != null))
            {
                //add the time of day to the date
                dateAndTime = dateAndTime.Value.Add(timeOnly.Value.TimeOfDay);
            }

            return dateAndTime;
        }

        private static string ReadFilterValue_WindowsIdFromCreatedByFilter(string filterText, bool allowInvalidValues)
        {
            filterText = filterText.TrimToNull();
            if (!string.IsNullOrEmpty(filterText))
            {
                string windowsId = PersonController.ParseWindowsIDFromName(filterText);
                if (string.IsNullOrEmpty(windowsId))
                {
                    //PersonController.ParseWindowsIDFromName could not parse the text, so assume the text value is a simple WindowsID value (rather than a Person/Name value)
                    windowsId = filterText;
                }
                if (!string.IsNullOrEmpty(windowsId))
                {
                    if (!allowInvalidValues)
                    {
                        //TODO: Implement: Conditional code path: Implementation of behavior for when allowInvalidValues==false
#warning Not Implemented: Conditional code path: Implementation of behavior for when allowInvalidValues==false
                        throw new NotImplementedException("Support for the allowInvalidValues==false code path is not yet implemented.");
                        //if (!IsValid(windowsId))
                        //{
                        //   return null;
                        //}
                    }
                    return windowsId;
                }
            }
            return null;
        }

        public static string ReadFilterValue_WindowsIdFromCreatedByFilter(ITextControl filterControl, bool allowInvalidValues)
        {
            return ReadFilterValue_WindowsIdFromCreatedByFilter(filterControl.Text, allowInvalidValues);
        }

        public static string ReadFilterValue_WindowsIdFromModifiedByFilter(ITextControl filterControl, bool allowInvalidValues)
        {
            //use the exact same logic as we would for a CreatedBy filter
            return ReadFilterValue_WindowsIdFromCreatedByFilter(filterControl.Text, allowInvalidValues);
        }

        #endregion

        #endregion

        #region Request Introspection Methods

        /// <summary>
        /// Indicates whether a specified <see cref="HttpRequest"/> has the "Content Only" Display Mode enabled (i.e. should it show the header, footer, etc. or not).
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsContentOnlyDisplayModeEnabled(HttpRequest request)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(request, "request");
            string paramValue = request.Params["co"] ?? String.Empty;
            return paramValue.ToLowerInvariant() == "y";
        }

        #endregion

        #region Misc. Methods

        public static void TransmitBinaryFile(HttpResponse response, string mimeType, FileInfo file, byte[] buffer)
        {
            //TODO: Refactoring: Migrate to HPFx as an overload of WebUtility.TransmitBinaryFile
#warning Refactoring: Migrate to HPFx as an overload of WebUtility.TransmitBinaryFile

            using (Stream responseStream = WebUtility.TransmitBinaryFile(response, mimeType, Path.GetFileName(file.Name)))
            {
                using (FileStream fileStream = file.OpenRead())
                {
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        responseStream.Write(buffer, 0, bytesRead);
                    }
                }
                responseStream.Flush();
            }
        }

        public static void ValidateUriLength(string uri)
        {
            if (!WebUtility.IsUriLengthValid(uri))
            {
                //TODO: Implement: Better way to handle very long URL strings.
#warning Not Implemented: Better way to handle very long URL strings.
                throw new NotImplementedException("The invoked code path is not yet implemented.");
            }
        }

        /// <summary>
        /// validate comma-separated id list
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public static bool ValidateCommaSeparatedIdList(string idList)
        {
            if (String.IsNullOrEmpty(idList))
            {
                return true;
            }

            foreach (var s in idList.Split(','))
            {
                int num;
                if (!int.TryParse(s, out num))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Redirects the client to the app's user registration page.
        /// </summary>
        public static void RedirectToRegistrationPage()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext.Current is null.");
            }

            Uri currentUri = httpContext.Request.Url;
            if (IsAppRegistrationPage(currentUri))
            {
                return; //do nothing (to prevent redirecting to the page you are already at)
            }
            httpContext.Response.Redirect(GetRegistrationPageUri());
        }

        /// <summary>
        /// Indicates whether a specified <see cref="Uri"/> represents a request to the app's registration page.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool IsAppRegistrationPage(Uri uri)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(uri, "uri");
            return (uri.LocalPath.IndexOf(GetRegistrationPageUri().TrimStart('~'), StringComparison.CurrentCultureIgnoreCase) >= 0);
        }

        /// <summary>
        /// Indicates whether a specified <see cref="Uri"/> represents a request to the app's "ScriptResource.axd" page.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool IsScriptResourceAxdPage(Uri uri)
        {
            string requestedFileName = HpfxUtility.GetLocalPathFileName(uri);
            return requestedFileName.Equals("ScriptResource.axd", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Registers a specified <see cref="Control"/> as one that requires a full PostBack (i.e. not an asynchronous or partial-page PostBack).
        /// </summary>
        /// <remarks>
        /// It is sometimes necessary to call this method for certan controls because certain types of controls or operations will not work properly 
        /// if initiated as part of a partial page PostBack.
        /// See here for details:
        /// http://blog.encoresystems.net/articles/fileupload-in-an-ajax-updatepanel.aspx
        /// </remarks>
        /// <param name="control">The Control which requires full PostBacks.</param>
        public static void RegisterAsFullPostBackTrigger(Control control)
        {
            ExceptionUtility.ArgumentNullEx_ThrowIfNull(control, "control");

            ScriptManager scriptManager = ScriptManager.GetCurrent(control.Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterPostBackControl(control);
            }
            else
            {
                SiteMaster masterPage = WebUtility.GetMasterPage<SiteMaster>(control.Page);
                if (masterPage != null)
                {
                    masterPage.RegisterAsFullPostbackTrigger(control);
                }
                else
                {
                    //TODO: Implement: Guard clause for unhandled scenario
#warning Not Implemented: Guard clause for unhandled scenario
                    throw new NotImplementedException("The invoked code path is not yet implemented.");
                }
            }
        }

        [Obsolete("Not working correctly yet.", false)]
        public static PlaceHolder CreatePageWithPlaceHolderControl(bool addInsideRunatServerFormControl, bool addScriptManagerToTemporaryPage)
        {
            if (addScriptManagerToTemporaryPage && (!addInsideRunatServerFormControl))
            {
                throw new ArgumentException("addScriptManagerToTemporaryPage must be false if addInsideRunatServerFormControl is false.");
            }

            PlaceHolder placeHolderControl = new PlaceHolder();
            placeHolderControl.ID = "placeholder";

            Page tempPage = new Page();
            if (addInsideRunatServerFormControl)
            {
                HtmlForm formControl = new HtmlForm();
                formControl.ID = "form1";
                formControl.Attributes.Add("runat", "server");
                tempPage.Controls.Add(formControl);

                if (addScriptManagerToTemporaryPage)
                {
                    ScriptManager scriptManagerControl = new ScriptManager();
                    //scriptManagerControl.ID = "ajaxScriptManager";
                    //scriptManagerControl.EnablePartialRendering = false;
                    //scriptManagerControl.Scripts.Add(new ScriptReference("~/scripts/HP.ElementsCPS.Apps.WebUI.js"));
                    formControl.Controls.Add(scriptManagerControl);
                }

                formControl.Controls.Add(placeHolderControl);
            }
            else
            {
                tempPage.Controls.Add(placeHolderControl);
            }

            return placeHolderControl;
        }

        /// <summary>
        /// Convenience method.
        /// </summary>
        /// <param name="userControlFileName"></param>
        /// <returns></returns>
        public static UserControl LoadUserControlByName(string userControlFileName)
        {
            PlaceHolder placeHolderControl = CreatePageWithPlaceHolderControl(true, true);
            UserControl uc = LoadUserControlByName(userControlFileName, placeHolderControl.Page);
            placeHolderControl.Controls.Add(uc);
            return uc;
        }

        public static UserControl LoadUserControlByName(string userControlFileName, Page page)
        {
            string userControlPath = string.Format("~/UserControls/{0}.ascx", Path.GetFileNameWithoutExtension(userControlFileName));
            return (UserControl)page.LoadControl(userControlPath);
        }

        /// <summary>
        /// Renders a specified <see cref="Page"/> into an HTML string.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string ExecutePage(Page page)
        {
            //TODO: Review Needed: Review implementation to verify that the method works correctly
#warning Review Needed: Review implementation to verify that the method works correctly

            //TODO: Refactoring: Move this utility method to HPFx
#warning Refactoring: Move this utility method to HPFx

            StringWriter output = new StringWriter();
            HttpContext.Current.Server.Execute(page, output, false);

            return output.ToString();
        }

        //      /// <summary>
        //      /// Dynamically loads/creates an instance of a specified <see cref="UserControl"/>.
        //      /// </summary>
        //      /// <param name="path">The path to the ASCX file of the control.</param>
        //      /// <param name="addToTemporaryPage">If <c>true</c>, the returned instance will already have been added to the <see cref="Control.Controls"/> hierarchy of a newly instantiated <see cref="Page"/> instance.</param>
        //      /// <param name="addInsideRunatServerFormControl">If <c>true</c>, the returned instance will already have been added to the <see cref="Control.Controls"/> hierarchy of a newly instantiated <see cref="HtmlForm"/> instance (decorated with a <c>runat="server"</c> attribute).</param>
        //      /// <param name="addScriptManagerToTemporaryPage"></param>
        //      /// <returns></returns>
        //      public static UserControl LoadUserControl(string path, bool addToTemporaryPage, bool addInsideRunatServerFormControl, bool addScriptManagerToTemporaryPage)
        //      {
        //         //TODO: Refactoring: Move this utility method to HPFx???
        //#warning Refactoring: Move this utility method to HPFx???
        //         if (addInsideRunatServerFormControl && (!addToTemporaryPage))
        //         {
        //            throw new ArgumentException("addInsideRunatServerFormControl must be false if addToTemporaryPage is false.");
        //         }
        //         if (addScriptManagerToTemporaryPage && (!addToTemporaryPage))
        //         {
        //            throw new ArgumentException("addScriptManagerToTemporaryPage must be false if addToTemporaryPage is false.");
        //         }
        //         if (addScriptManagerToTemporaryPage && (!addInsideRunatServerFormControl))
        //         {
        //            throw new ArgumentException("addScriptManagerToTemporaryPage must be false if addInsideRunatServerFormControl is false.");
        //         }
        //         if (addToTemporaryPage)
        //         {
        //            PlaceHolder placeHolderControl = CreatePageWithPlaceHolderControl(addInsideRunatServerFormControl, addScriptManagerToTemporaryPage);
        //            UserControl uc = (UserControl)placeHolderControl.Page.LoadControl(path);
        //            placeHolderControl.Controls.Add(uc);
        //         }
        //         return uc;
        //      }

        [Obsolete("Implementation is incomplete.", true)]
        public static string LoadAndExecuteUserControl(string path, string fieldName, object fieldValue)
        {
            //TODO: Refactoring: Move this utility method to HPFx
#warning Refactoring: Move this utility method to HPFx

            //TODO: Implement: Primary code path
#warning Not Implemented: Primary code path
            throw new NotImplementedException("The invoked code path is not yet implemented.");

            //Page tempPage = new Page();
            //UserControl uc = (UserControl)tempPage.LoadControl(path);

            //if (fieldValue != null)
            //{
            //   Type viewControlType = uc.GetType();
            //   FieldInfo field = viewControlType.GetField(fieldName);

            //   if (field != null)
            //   {
            //      field.SetValue(uc, fieldValue);
            //   }
            //   else
            //   {
            //      throw new Exception("View file: " + path + " does not have a public " + fieldName + " property");
            //   }
            //}

            //tempPage.Controls.Add(uc);

            //StringWriter output = new StringWriter();
            //HttpContext.Current.Server.Execute(tempPage, output, false);

            //return output.ToString();
        }

        /// <summary>
        /// Indicates whether a specified exception is/was caused by or related to an IE bug 
        /// that results in several types of harmless runtime exceptions 
        /// that are only detectable on the server side but have no other observable effects.
        /// </summary>
        /// <remarks>
        /// See: https://connect.microsoft.com/IE/feedback/ViewFeedback.aspx?FeedbackID=467062
        /// See: https://connect.microsoft.com/VisualStudio/feedback/details/434997/invalid-webresource-axd-parameters-being-generated
        /// </remarks>
        /// <param name="exc"></param>
        /// <returns></returns>
        internal static bool IsHarmlessScriptResourceAxdIeBugException(Exception exc)
        {
            return ((exc is HttpException) && (exc.Message.Equals("This is an invalid script resource request.")))
                     || ((exc is FormatException) && (exc.Message.Equals("Invalid length for a Base-64 char array.")));
        }

        /// <summary>
        /// Indicates whether a specified exception is/was caused by or related to an IE bug 
        /// that results in several types of harmless runtime exceptions 
        /// that are only detectable on the server side but have no other observable effects.
        /// </summary>
        /// <remarks>
        /// See: https://connect.microsoft.com/IE/feedback/ViewFeedback.aspx?FeedbackID=467062
        /// See: https://connect.microsoft.com/VisualStudio/feedback/details/434997/invalid-webresource-axd-parameters-being-generated
        /// </remarks>
        /// <param name="ex"></param>
        /// <returns></returns>
        internal static bool IsHttpException_PageDoesNotExist(Exception ex)
        {
            List<HttpException> httpExceptions = ExceptionUtility.GetExceptionsOfType<HttpException>(ex).ToList();
            if (httpExceptions.Count > 0)
            {
                bool isHttpException_FileDoesNotExist =
                    (httpExceptions.Where(sqlEx => sqlEx.Message.StartsWith("The file '")).Where(sqlEx => sqlEx.Message.EndsWith("' does not exist")).Count() > 0);
                return isHttpException_FileDoesNotExist;
            }

            return false;
        }

        public static IEnumerable<DataTable> YieldDataTablesForDataExport<TRecord, TRecordCollection, TQuerySpecification>(
            IQuerySpecification querySpecification,
            Func<TQuerySpecification, TRecordCollection> fetchMethod
        )
            where TRecord : RecordBase<TRecord>, new()
            where TRecordCollection : AbstractList<TRecord, TRecordCollection>, new()
            where TQuerySpecification : QuerySpecificationWrapper, IQuerySpecification, new()
        {
            //determine the Export paging settings/threshholds
            int startRowIndex = querySpecification.Paging.PagingStartRowIndex ?? 0;
            int totalRowCount = Math.Min(Global.ExportFileMaxRowCount, querySpecification.Paging.PageSize ?? int.MaxValue);
            //merge the Export paging settings/threshholds into the Paging settings of (a copy of) the QuerySpecification
            TQuerySpecification qs = QuerySpecificationWrapper.CopyAs<TQuerySpecification>(querySpecification);
            qs.Paging.SetPageIndexByStartRowIndex(totalRowCount, startRowIndex);
            //use the modified QuerySpecification to fetch the DataTables
            return SubSonicUtility.YieldDataTables<TRecord, TRecordCollection, TQuerySpecification>(qs, fetchMethod, Global.ExportFileMaxPageSize);
        }

        #endregion

        #region Configuration Methods

        /// <summary>
        /// Prevents this page/response from being cached by the client/browser
        /// </summary>
        public static void DisableClientCacheing(HttpResponse response)
        {
            //TODO: Refactoring: Moved this utility method into HPFx
#warning Refactoring: Moved this utility method into HPFx

            WebUtility.DisableClientCacheing(response);
        }

        #endregion

        #region ElementsCPSDB-specific Methods

        #region ElementsCPSDB-specific ListControl Methods

        #region ElementsCPSDB-specific ListItem Text Constants

        #endregion

        #region ElementsCPSDB-specific ListControl Utility Methods

        #endregion

        #region ElementsCPSDB-specific ListControl Binding Methods

        #region ElementsCPSDB-specific Specialized Binding Methods

        #region ElementsCPSDB-specific Lifecycle State Binding Methods

        #endregion

        #endregion

        #region ElementsCPSDB-specific Standard Binding Methods

        public static void RebindJumpstationMacroListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            // rebind so remove any existing cache
            string cacheKey = GenerateCacheKey_ListControlDataSource("JumpstationMacro", rowStatusId.Value.ToString("G"));
            CacheRemove(cacheKey);

            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationMacro", rowStatusId));
        }

        public static void RebindQueryParameterListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            // rebind so remove any existing cache
            string cacheKey = GenerateCacheKey_ListControlDataSource("QueryParameter", rowStatusId.Value.ToString("G"));
            CacheRemove(cacheKey);

            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("QueryParameter", rowStatusId));
        }

        public static void BindQueryParameterListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("QueryParameter", rowStatusId));
        }

        public static void BindConfigurationServiceGroupTypeListControl(ListControl lc, int configurationServiceApplicationId, RowStatus.RowStatusId? rowStatusId)
        {
            string whereClause = rowStatusId == null
                ? " ConfigurationServiceApplicationId = " + configurationServiceApplicationId.ToString()
                : " ConfigurationServiceApplicationId = " + configurationServiceApplicationId.ToString() + string.Format(" AND RowStatusId = {0:D}", rowStatusId);
            DataBindListControl_Id_Name(lc, ElementsCPSSqlUtility.GetListControlDataSource_Id_Name("ConfigurationServiceGroupType", whereClause));
        }
        //
        public static void BindJumpstationGroupTypeListControl(ListControl lc, int jumpstationApplicationId, RowStatus.RowStatusId? rowStatusId)
        {

            string whereClause = rowStatusId == null
                ? " JumpstationApplicationId = " + jumpstationApplicationId.ToString()
                : " JumpstationApplicationId = " + jumpstationApplicationId.ToString() + string.Format(" AND RowStatusId = {0:D}", rowStatusId);
            DataBindListControl_Name_Name(lc, ElementsCPSSqlUtility.GetListControlDataSource_Id_Name("JumpstationGroupType", whereClause));
        }

        //JS
        public static bool IsJSDataModificationAllowed()
        {
            return IsDataModificationAllowed((int)RoleApplicationId.Jumpstation);
        }

        public static bool IsJSMetaDataModificationAllowed()
        {
            return IsMetaDataModificationAllowed((int)RoleApplicationId.Jumpstation);
        }

        //CFS
        public static bool IsCFSDataModificationAllowed()
        {
            return IsDataModificationAllowed((int)RoleApplicationId.ConfigureService);
        }

        public static bool IsCFSMetaDataModificationAllowed()
        {
            return IsMetaDataModificationAllowed((int)RoleApplicationId.ConfigureService);
        }

        //REA
        public static bool IsREADataModificationAllowed()
        {
            return IsDataModificationAllowed((int)RoleApplicationId.Reach);
        }

        public static bool IsREAMetaDataModificationAllowed()
        {
            return IsMetaDataModificationAllowed((int)RoleApplicationId.Reach);
        }

        public static bool IsSysDataModificationAllowed()
        {
            return IsDataModificationAllowed();
        }

        protected static bool IsDataModificationAllowed()
        {
            PersonController.GetCurrentUser().GetRoles();
            List<UserRoleId> userRoleIds = PersonController.GetCurrentUser().GetRoles();
            if (userRoleIds.Contains(UserRoleId.Administrator) || userRoleIds.Contains(UserRoleId.SysDataAdmin))
            {
                return true;
            }

            return false;
        }

        protected static bool IsDataModificationAllowed(int applicationId)
        {
            List<UserRoleId> userRoleIds = PersonController.GetCurrentUser().GetRoles();
            int personId = PersonController.GetCurrentUser().Id;
            if (userRoleIds.Contains(UserRoleId.Administrator))
            {
                return true;
            }

            if (userRoleIds.Contains(UserRoleId.DataAdmin))
            {
                List<RoleApplicationId> app = ApplicationRoleController.GetCurrentAppListByRole(personId, UserRoleId.DataAdmin);
                if (app.Contains((RoleApplicationId)applicationId) || app.Contains(RoleApplicationId.Default))
                {
                    return true;
                }
            }
            return false;
        }

        protected static bool IsMetaDataModificationAllowed(int applicationId)
        {
            List<UserRoleId> userRoleIds = PersonController.GetCurrentUser().GetRoles();
            int personId = PersonController.GetCurrentUser().Id;
            if (userRoleIds.Contains(UserRoleId.Administrator))
            {
                return true;
            }
            List<UserRoleId> roles = ApplicationRoleController.GetCurrentRoleListByApp(personId, (RoleApplicationId)applicationId);
            if (roles.Contains(UserRoleId.Viewer) || roles.Contains(UserRoleId.Validator) || roles.Contains(UserRoleId.Editor))
            {
                return true;
            }
            List<UserRoleId> rolesDef = ApplicationRoleController.GetCurrentRoleListByApp(personId, (RoleApplicationId.Default));
            if (rolesDef.Contains(UserRoleId.Viewer) || rolesDef.Contains(UserRoleId.Validator) || rolesDef.Contains(UserRoleId.Editor))
            {
                return true;
            }
           
            return false;
        }

        public static void BindAppClientListControl(ListControl lc)
        {
            AppClientCollection appClients = PersonController.GetCurrentUser().TenantToTenantGroupId.GetAppClientCollection();

            for (int i = 0; i < appClients.Count; i++)
            {
                if (appClients[i].RowStatusId != 1)
                {
                    appClients.Remove(appClients[i]);
                }
            }
            //foreach (AppClient appclient in appClients)
            //{
            //    if (appclient.RowStatusId != 1)
            //    {
            //        appClients.Remove(appclient);
            //    }
            //}
            DataBindListControl_Name_Name(lc, appClients);
        }

        public static void BindAppClientListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("AppClient", rowStatusId));
        }

        public static void RemoveConfigurationServiceGroupTypeListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("ConfigurationServiceGroupType", rowStatusId);
        }

        public static void BindQueryParameterValueListControl(ListControl lc, int queryParameterId, RowStatus.RowStatusId? rowStatusId)
        {
            string whereClause = rowStatusId == null
                ? " QueryParameterId = " + queryParameterId.ToString()
                : " QueryParameterId = " + queryParameterId.ToString() + string.Format(" AND RowStatusId = {0:D}", rowStatusId);
            DataBindListControl_Id_Name(lc, ElementsCPSSqlUtility.GetListControlDataSource_Id_Name("QueryParameterValue", whereClause));
        }

        public static void BindQueryParameterValueNameListControl(ListControl lc, int queryParameterId, RowStatus.RowStatusId? rowStatusId)
        {
            string whereClause = rowStatusId == null
                ? " QueryParameterId = " + queryParameterId.ToString()
                : " QueryParameterId = " + queryParameterId.ToString() + string.Format(" AND RowStatusId = {0:D}", rowStatusId);
            DataBindListControl_Name_Name(lc, ElementsCPSSqlUtility.GetListControlDataSource_Id_Name("QueryParameterValue", whereClause));
        }

        public static void BindProxyURLTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ProxyURLType", rowStatusId));
        }

        public static void RemoveProxyURLTypeListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("ProxyURLType", rowStatusId);
        }

        public static void RemoveProxyURLDomainListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("ProxyURLDomain", rowStatusId);
        }

        public static void BindDomainListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ProxyURLDomain", rowStatusId));
        }

        public static void BindGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ProxyURLGroupType", rowStatusId));
        }

        public static void BindProxyURLStatusListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ProxyURLStatus", rowStatusId));
        }

        public static void BindConfigurationServiceApplicationListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceApplication", rowStatusId));
        }

        public static void BindConfigurationServiceApplicationListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId,bool isNewRecord)
        {
            DataBindListControl_Id_Name(lc, isNewRecord ? ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("ConfigurationServiceApplication", rowStatusId, tenantId) : Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceApplication", rowStatusId, tenantId));
        }
        public static void BindConfigurationServiceApplicationListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            DataBindListControl_Name_Name(lc,  ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("ConfigurationServiceApplication", rowStatusId, tenantId) );
        }

        public static void BindConfigurationServiceApplicationElementsKeyListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_ElementsKey_ElementsKey(lc, ElementsCPSSqlUtility.GetListControlDataSource_Id_ElementsKey("ConfigurationServiceApplication", rowStatusId));
        }

        public static void RemoveConfigurationServiceApplicationListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("ConfigurationServiceApplication", rowStatusId);
        }

        public static void BindConfigurationServiceApplicationTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceApplicationType", rowStatusId));
        }
        public static void BindConfigurationServiceApplicationTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            DataBindListControl_Id_Name(lc, ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("ConfigurationServiceApplicationType", rowStatusId, tenantId));
        }

        public static void BindConfigurationServiceApplicationTypeNameListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            DataBindListControl_Name_Name(lc, ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("ConfigurationServiceApplicationType", rowStatusId, tenantId));
        }
        public static void RemoveConfigurationServiceApplicationTypeListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("ConfigurationServiceApplicationType", rowStatusId);
        }

        public static void BindConfigurationServiceGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceGroupType", rowStatusId));
        }

        public static void BindConfigurationServiceGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId, bool isNewRecord)
        {
            DataBindListControl_Id_Name(lc, isNewRecord ? ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("ConfigurationServiceGroupType", rowStatusId, tenantId) : Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceGroupType", rowStatusId));
        }

        public static void BindConfigurationServiceGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            DataBindListControl_Name_Name(lc, ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("ConfigurationServiceGroupType", rowStatusId, tenantId));
        }

        public static void BindConfigurationServiceGroupTypeNameListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Name_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceGroupType", rowStatusId));
        }

        public static void BindConfigurationServiceGroupStatusListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceGroupStatus", rowStatusId));
        }

        public static void BindConfigurationServiceItemListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceItem", rowStatusId));
        }

        public static void RemoveConfigurationServiceItemListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("ConfigurationServiceItem", rowStatusId);
        }

        public static void BindConfigurationServiceLabelTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceLabelType", rowStatusId));
        }

        public static void BindConfigurationServiceLabelListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ConfigurationServiceLabel", rowStatusId));
        }

        public static void RemoveConfigurationServiceLabelListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("ConfigurationServiceLabel", rowStatusId);
        }

        public static void BindConfigurationServiceLabelValueListControl(ListControl lc, int labelId, RowStatus.RowStatusId? rowStatusId)
        {
            ConfigurationServiceLabel labelItem =
                ConfigurationServiceLabel.FetchByID(labelId);

            if (labelItem != null)
            {
                switch (labelItem.ConfigurationServiceLabelTypeId)
                {
                    case (int)ConfigurationServiceLabelTypeId.LabelTypeDropDownList:
                        Global.BindCommaDelimitedListControl(lc, labelItem.ValueList);
                        break;
                    default:
                        // only support LabelTypeDropDownList
                        break;
                }
            }
        }

       
        public static void BindTenantListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("Tenant", rowStatusId));
        }

        public static void BindJumpstationApplicationListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId, bool isNewRecord)
        {
            DataBindListControl_Id_Name(lc, isNewRecord ? ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("JumpstationApplication", rowStatusId, tenantId) : Global.GetCachedListControlDataSource_Id_Name("JumpstationApplication", rowStatusId, tenantId));
        }

        public static void BindJumpstationApplicationListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            DataBindListControl_Name_Name(lc, ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("JumpstationApplication", rowStatusId, tenantId));
        }

        public static void BindJumpstationApplicationListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationApplication", rowStatusId));
        }

        public static void BindJumpstationDomainListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationDomain", rowStatusId));
        }

        public static void BindJumpstationGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId,int tenantId)
        {
            DataBindListControl_Name_Name(lc, ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("JumpstationGroupType", rowStatusId, tenantId));
        }

        public static void BindJumpstationGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId,bool isnew)
        {
            DataBindListControl_Id_Name(lc, ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("JumpstationGroupType", rowStatusId, tenantId));
        }

       
        public static void BindJumpstationGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationGroupType", rowStatusId));
        }

        public static void BindTenantJumpstationGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            //DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationGroupType", rowStatusId));
         
            //List<int> list = ElementsCPSSqlUtility.GetTenantListControlDataSource_Id("JumpstationGroupType", rowStatusId, tenantId);

            //JumpstationGroupTypeCollection col = JumpstationGroupTypeController.FetchJumpstationGroupTypeByIDs(list);

            //for (int i = 0; i < col.Count; i++ )
            //{
            //    if (col[i].RowStatusId != 1)
            //    {
            //        col.Remove(col[i]);
            //    }
            //}
            DataBindListControl_Id_Name(lc, ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("JumpstationGroupType", rowStatusId, tenantId));
        }


        public static void BindTenantConfigurationServiceGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        {
            //DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationGroupType", rowStatusId));

            //List<int> list = ElementsCPSSqlUtility.GetTenantListControlDataSource_Id("ConfigurationServiceApplication", rowStatusId, tenantId);
            //ConfigurationServiceGroupTypeCollection col = ConfigurationServiceGroupTypeController.FetchConfigurationServiceGroupTypeByIDs(list);
            //for (int i = 0; i < col.Count; i++)
            //{
            //    if (col[i].RowStatusId != 1)
            //    {
            //        col.Remove(col[i]);
            //    }
            //}
            DataBindListControl_Id_Name(lc, ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("ConfigurationServiceGroupType", rowStatusId, tenantId));

        }

        //public static void BindTenantWorkflowTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId)
        //{
        //    //DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationGroupType", rowStatusId));
        //    List<int> list = ElementsCPSSqlUtility.GetTenantListControlDataSource_Id("WorkflowApplication", rowStatusId, tenantId);
        //    WorkflowApplicationCollection col = WorkflowApplicationController.FetchWorkflowTypeByIDs(list);

        //    WorkflowTypeCollection typecol = new WorkflowTypeCollection();
        //    foreach(WorkflowApplication app in col)
        //    {
        //        typecol.Add(app.WorkflowApplicationType);
        //    }

        //    DataBindListControl_Id_Name(lc, col);
        //}

        public static void BindJumpstationGroupStatusListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationGroupStatus", rowStatusId));
        }

        public static void BindJumpstationMacroStatusListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("JumpstationMacroStatus", rowStatusId));
        }

        public static void BindProxyURLDomainListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ProxyURLDomain", rowStatusId));
        }

        public static void BindProxyURLGroupTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("ProxyURLGroupType", rowStatusId));
        }

        public static void RemoveProxyURLGroupTypeListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("ProxyURLGroupType", rowStatusId);
        }

        public static void RemoveAppClientListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("AppClient", rowStatusId);
        }

        public static void RemoveApplicationListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("Application", rowStatusId);
        }
        public static void RemoveTenantListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("Tenant", rowStatusId);
        }

        public static void BindWorkflowApplicationListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("WorkflowApplication", rowStatusId));
        }

       public static void BindWorkflowApplicationListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId, int tenantId,bool isNewRecord)
        {
            DataBindListControl_Id_Name(lc, isNewRecord ? ElementsCPSSqlUtility.GetTenantListControlDataSource_Id_Name("WorkflowApplication", rowStatusId, tenantId) : Global.GetCachedListControlDataSource_Id_Name("WorkflowApplication", rowStatusId, tenantId));
        }

        public static void RemoveWorkflowApplicationListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("WorkflowApplication", rowStatusId);
        }

        public static void BindWorkflowApplicationTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("WorkflowApplicationType", rowStatusId));
        }

        public static void RemoveWorkflowApplicationTypeListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("WorkflowApplicationType", rowStatusId);
        }

        public static void BindWorkflowModuleCategoryListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("WorkflowModuleCategory", rowStatusId));
        }

        public static void RemoveWorkflowModuleCategoryListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("WorkflowModuleCategory", rowStatusId);
        }

        public static void BindWorkflowModuleSubCategoryListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("WorkflowModuleSubCategory", rowStatusId));
        }

        public static void RemoveWorkflowModuleSubCategoryListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("WorkflowModuleSubCategory", rowStatusId);
        }

        public static void BindWorkflowTypeListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("WorkflowType", rowStatusId));
        }

        public static void RemoveWorkflowTypeListControl(RowStatus.RowStatusId? rowStatusId)
        {
            Global.RemoveCachedListControlDataSource("WorkflowType", rowStatusId);
        }

        public static void BindWorkflowStatusListControl(ListControl lc, RowStatus.RowStatusId? rowStatusId)
        {
            DataBindListControl_Id_Name(lc, Global.GetCachedListControlDataSource_Id_Name("WorkflowStatus", rowStatusId));
        }


        #endregion

        #endregion

        #region ElementsCPSDB-specific ListControl Selection Initialization Methods

        public static void SetProxyURLTypeListControl(ListControl lc, ProxyURLTypeCollection proxyUrlTypes)
        {
            Global.SetListControlSelectedValues(lc, proxyUrlTypes.Select(proxyUrlType => proxyUrlType.Id.ToString()));
        }

        public static void SetConfigurationServiceGroupTypeControl(ListControl lc, ConfigurationServiceGroupTypeCollection configurationServiceGroupTypes)
        {
            Global.SetListControlSelectedValues(lc, configurationServiceGroupTypes.Select(configurationServiceGroupType => configurationServiceGroupType.Id.ToString()));
        }

        public static void SetJumpstationGroupTypeControl(ListControl lc, JumpstationGroupTypeCollection jumpstationGroupTypes)
        {
            Global.SetListControlSelectedValues(lc, jumpstationGroupTypes.Select(jumpstationGroupType => jumpstationGroupType.Id.ToString()));
        }


        #endregion

        #endregion

        #region ElementsCPSDB-specific Dynamic URL Methods

        #region ElementsCPSDB-specific URL Modification Methods

        #endregion

        #region ElementsCPSDB-specific Generate...SummariesOverlayPageUri Methods

        #endregion

        #region ElementsCPSDB-specific Generate...SummaryOverlayPageUri Methods

        #endregion

        #region ElementsCPSDB-specific Generate...ListPageUri Methods

        #region ElementsCPSDB-specific Custom Generate...ListPageUri Methods

        #endregion

        #region ElementsCPSDB-specific Basic Generate...ListPageUri Methods

        public static string GetConfigurationServiceApplicationTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ConfigurationServiceApplicationTypeList.aspx", query);
        }

        public static string GetProxyURLDomainListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ProxyURLDomainList.aspx", query);
        }

        public static string GetProxyURLGroupTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ProxyURLGroupTypeList.aspx", query);
        }

        public static string GetConfigurationServiceGroupTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ConfigurationServiceGroupTypeList.aspx", query);
        }

        public static string GetConfigurationServiceApplicationListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ConfigurationServiceApplicationList.aspx", query);
        }

        public static string GetConfigurationServiceItemListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ConfigurationServiceItemList.aspx", query);
        }

        public static string GetConfigurationServiceLabelListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ConfigurationServiceLabelList.aspx", query);
        }

        public static string GetConfigurationServiceLabelValueListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ConfigurationServiceLabelValueList.aspx", query);
        }

        public static string GetJumpstationGroupTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/JumpstationGroupTypeList.aspx", query);
        }

        public static string GetJumpstationApplicationListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/JumpstationApplicationList.aspx", query);
        }

        public static string GetJumpstationDomainListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/JumpstationDomainList.aspx", query);
        }

        public static string GetJumpstationMacroListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/JumpstationMacroList.aspx", query);
        }

        public static string GetPlatformListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/SystemAdmin/PlatformList.aspx", query);
        }

        public static string GetProxyURLTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/ProxyURLTypeList.aspx", query);
        }

        public static string GetQueryParameterConfigurationServiceGroupTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/QueryParameterConfigurationServiceGroupTypeList.aspx", query);
        }

        public static string GetQueryParameterJumpstationGroupTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/QueryParameterJumpstationGroupTypeList.aspx", query);
        }

        public static string GetQueryParameterProxyURLTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/QueryParameterProxyTypeList.aspx", query);
        }

        public static string GetQueryParameterWorkflowTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/QueryParameterWorkflowTypeList.aspx", query);
        }

        public static string GetQueryParameterConfigurationServiceGroupTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/QueryParameterConfigurationServiceGroupTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetQueryParameterJumpstationGroupTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/QueryParameterJumpstationGroupTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetQueryParameterProxyURLTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/QueryParameterProxyURLTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetQueryParameterWorkflowTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/QueryParameterWorkflowTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetQueryParameterListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/QueryParameterList.aspx", query);
        }

        public static string GetQueryParameterValueListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/QueryParameterValueList.aspx", query);
        }

        public static string GetProxyURLListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/Redirect/ProxyURLList.aspx", query);
        }

        public static string GetConfigurationServiceGroupListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/ConfigurationService/ConfigurationServiceGroupList.aspx", query);
        }

        public static string GetJumpstationGroupListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/Jumpstation/JumpstationGroupList.aspx", query);
        }

        public static string GetJumpstationGroupByQueryParameterListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/Jumpstation/JumpstationGroupByQueryParameterList.aspx", query);
        }

        public static string GetConfigurationServiceGroupImportListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/ConfigurationService/ConfigurationServiceGroupImportList.aspx", query);
        }

        public static string GetWorkflowListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/ConfigurationService/WorkflowList.aspx", query);
        }

        public static string GetWorkflowApplicationListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/WorkflowApplicationList.aspx", query);
        }

        public static string GetWorkflowApplicationTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/WorkflowApplicationTypeList.aspx", query);
        }

        public static string GetWorkflowConditionListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/WorkflowConditionList.aspx", query);
        }

        public static string GetWorkflowModuleListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/WorkflowModuleList.aspx", query);
        }

        public static string GetWorkflowModuleCategoryListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/WorkflowModuleCategoryList.aspx", query);
        }

        public static string GetWorkflowModuleSubCategoryListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/WorkflowModuleSubCategoryList.aspx", query);
        }

        public static string GetWorkflowTypeListPageUri(IQuerySpecification query)
        {
            return GenerateStandardListPageUri("~/DataAdmin/WorkflowTypeList.aspx", query);
        }

        #endregion

        #endregion

        #region ElementsCPSDB-specific Generate...UpdatePageUri Methods

        #region ElementsCPSDB-specific Custom Generate...UpdatePageUri Methods

        public static string GetProxyURLDomainUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ProxyURLDomainUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetProxyURLGroupTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ProxyURLGroupTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }
        //add client redirecturi
        public static string GetAppClientUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/AppClientUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetApplicationUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ApplicationUpdate.aspx", recordId, defaultValuesSpecification);
        }
        public static string GetTenantGroupUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/TenantGroupUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceGroupSelectorUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupSelectorUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceGroupTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ConfigurationServiceGroupTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceApplicationUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ConfigurationServiceApplicationUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceApplicationTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ConfigurationServiceApplicationTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetJumpstationGroupSelectorUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/Jumpstation/JumpstationGroupSelectorUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetJumpstationGroupTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/JumpstationGroupTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetJumpstationApplicationUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/JumpstationApplicationUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetJumpstationDomainUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/JumpstationDomainUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetPlatformUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/SystemAdmin/PlatformUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetProxyURLTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ProxyURLTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetQueryParameterUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/QueryParameterUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetQueryParameterValueUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/QueryParameterValueUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetProxyURLUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/Redirect/ProxyURLUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceGroupUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceLabelValueImportUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceLabelValueImportUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceGroupImportUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupImportUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceItemUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ConfigurationServiceItemUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceLabelUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ConfigurationServiceLabelUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceLabelValueUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/ConfigurationServiceLabelValueUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetConfigurationServiceQueryParameterValueImportUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceQueryParameterValueImportUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetJumpstationGroupUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/Jumpstation/JumpstationGroupUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetJumpstationMacroUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/JumpstationMacroUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetJumpstationMacroValueUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/JumpstationMacroValueUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetProxyURLEditUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Redirect/ProxyURLEditUpdate.aspx", editSetSpecification);
        }

        public static string GetJumpstationMacroEditUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/DataAdmin/JumpstationMacroEditUpdate.aspx", editSetSpecification);
        }

        public static string GetConfigurationServiceGroupEditUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupEditUpdate.aspx", editSetSpecification);
        }

        public static string GetConfigurationServiceGroupImportEditUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupImportEditUpdate.aspx", editSetSpecification);
        }

        public static string GetJumpstationGroupEditUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Jumpstation/JumpstationGroupEditUpdate.aspx", editSetSpecification);
        }

        public static string GetProxyURLCopyPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Redirect/ProxyURLCopy.aspx", editSetSpecification);
        }

        public static string GetConfigurationServiceGroupCopyPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupCopy.aspx", editSetSpecification);
        }

        public static string GetConfigurationServiceGroupSelectorCopyPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupSelectorCopy.aspx", editSetSpecification);
        }

        public static string GetConfigurationServiceGroupMultiReplaceUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupMultiReplaceUpdate.aspx", editSetSpecification);
        }

        public static string GetConfigurationServiceGroupReportPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/ConfigurationServiceGroupReport.aspx", editSetSpecification);
        }

        public static string GetJumpstationGroupCopyPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Jumpstation/JumpstationGroupCopy.aspx", editSetSpecification);
        }

        public static string GetJumpstationMacroCopyPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Jumpstation/JumpstationMacroCopy.aspx", editSetSpecification);
        }

        public static string GetJumpstationGroupSelectorCopyPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Jumpstation/JumpstationGroupSelectorCopy.aspx", editSetSpecification);
        }

        public static string GetJumpstationGroupMultiReplaceUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Jumpstation/JumpstationGroupMultiReplaceUpdate.aspx", editSetSpecification);
        }

        public static string GetJumpstationGroupReportPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Jumpstation/JumpstationGroupReport.aspx", editSetSpecification);
        }

        public static string GetJumpstationMacroMultiReplaceUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/DataAdmin/JumpstationMacroMultiReplaceUpdate.aspx", editSetSpecification);
        }

        public static string GetJumpstationMacroReportPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/DataAdmin/JumpstationMacroReport.aspx", editSetSpecification);
        }

        public static string GetProxyURLMultiEditUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Redirect/ProxyURLMultiEditUpdate.aspx", editSetSpecification);
        }

        public static string GetProxyURLMultiReplaceUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/Redirect/ProxyURLMultiReplaceUpdate.aspx", editSetSpecification);
        }

        public static string GetProxyURLQueryParameterUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/Redirect/ProxyURLQueryParameterUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowApplicationUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/WorkflowApplicationUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowConditionUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/WorkflowConditionUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowModuleUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/WorkflowModuleUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowModuleEditUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/DataAdmin/WorkflowModuleEditUpdate.aspx", editSetSpecification);
        }

        public static string GetWorkflowModuleMultiReplaceUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/DataAdmin/WorkflowModuleMultiReplaceUpdate.aspx", editSetSpecification);
        }

        public static string GetWorkflowModuleCategoryUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/WorkflowModuleCategoryUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowModuleSubCategoryUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/WorkflowModuleSubCategoryUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowApplicationTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/WorkflowApplicationTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowTypeUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/DataAdmin/WorkflowTypeUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/ConfigurationService/WorkflowUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowEditUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/WorkflowEditUpdate.aspx", editSetSpecification);
        }

        public static string GetWorkflowMultiReplaceUpdatePageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/WorkflowMultiReplaceUpdate.aspx", editSetSpecification);
        }

        public static string GetWorkflowReportPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/WorkflowReport.aspx", editSetSpecification);
        }

        public static string GetWorkflowSelectorUpdatePageUri(int? recordId, IQuerySpecification defaultValuesSpecification)
        {
            return GenerateStandardUpdateRecordPageUri("~/ConfigurationService/WorkflowSelectorUpdate.aspx", recordId, defaultValuesSpecification);
        }

        public static string GetWorkflowSelectorCopyPageUri(IQuerySpecification editSetSpecification)
        {
            return GenerateStandardMultiUpdateRecordPageUri("~/ConfigurationService/WorkflowSelectorCopy.aspx", editSetSpecification);
        }

        #endregion

        #region ElementsCPSDB-specific Basic Generate...UpdatePageUri Methods

        #endregion

        #endregion

        #endregion

        #endregion

        #region Container Methods

        //TODO: Refactoring: Move this utility method to HPFx
#warning Refactoring: Move this utility method to HPFx
        public static RecordDetailUserControl GetParentRecordDetailUserControl(UserControl uc)
        {
            Control container = uc.NamingContainer;
            while ((container != null) && !(container is RecordDetailUserControl))
            {
                container = container.Parent;
            }
            return ((RecordDetailUserControl)container);
        }

        #endregion

    }
}