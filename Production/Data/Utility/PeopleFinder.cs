using System;
using System.Collections;
using System.Configuration;
using System.DirectoryServices;
using HP.HPFx.Utility;

namespace HP.ElementsCPS.Data.Utility
{
	public class PeopleFinder
	{

		#region Constants

		// Internal LDAP Search Base
		protected const string SearchBaseDN = "ou=People,o=hp.com";
		// External LDAP Search Base
		protected const string SearchExternalBaseDN = "ou=partners, o=hp.com";

		/// <summary>
		/// Returns the prefix used by all AppSettings keys "owned" by this <see cref="Type"/>.
		/// </summary>
		/// <remarks>
		/// A <see cref="Type"/>'s <see cref="AppSettingPrefix"/> is usually the <see cref="Type.FullName"/> followed by a period (">").
		/// E.g. "HP.ElementsCPS.Data.Utility.PeopleFinder.".
		/// </remarks>
		internal static string AppSettingPrefix
		{
			get { return typeof(PeopleFinder).FullName + "."; }
		}

		#endregion

		#region Fields

		// Result list 
		private readonly ArrayList _profilerList = new ArrayList();

		#endregion

		#region Methods

		#region AppSetting Utility Methods

		private static string GetAppSettingInternalLdapServer()
		{
			return ConfigurationManager.AppSettings[AppSettingPrefix + "InternalLdapServer"];
		}

		private static string GetAppSettingExternalLdapServer()
		{
			return ConfigurationManager.AppSettings[AppSettingPrefix + "ExternalLdapServer"];
		}

		private static string GetAppSettingExternalLdapLogin()
		{
			return ConfigurationManager.AppSettings[AppSettingPrefix + "ExternalLdapLogin"];
		}

		private static string GetAppSettingExternalLdapPassword()
		{
			return ConfigurationManager.AppSettings[AppSettingPrefix + "ExternalLdapPassword"];
		}

		#endregion

		/// <summary>
		/// Search Internal LDAP based on the search type and search target string.
		/// </summary>
		public void Search(SearchType searchType, string searchTarget)
		{
			string appSetting_InternalLdapServer = GetAppSettingInternalLdapServer();
			string ldapPathSearchString = "LDAP://" + appSetting_InternalLdapServer + "/" + SearchBaseDN;
			DirectoryEntry objDirEntry = new DirectoryEntry(ldapPathSearchString) {AuthenticationType = AuthenticationTypes.None};
			DirectorySearcher searcher = new DirectorySearcher(objDirEntry);

			switch (searchType)
			{
				case SearchType.ByEmail:
					searcher.Filter = "(uid=" + searchTarget + ")";
					break;
				case SearchType.ByNTID:
					if (searchTarget.IndexOf("\\") > 0)
					{
						searchTarget = searchTarget.Replace("\\", ":");
						searcher.Filter = "(ntUserDomainId=" + searchTarget + ")";
					}
					else
					{
						searcher.Filter = "(ntUserDomainId=*" + searchTarget + ")";
					}
					break;
				case SearchType.ByCommonName:
					searcher.Filter = "(cn=" + searchTarget + ")";
					break;
				case SearchType.ByFirstName:
					searcher.Filter = "(givenName=" + searchTarget + ")";
					break;
				case SearchType.ByLastName:
					searcher.Filter = "(sn=" + searchTarget + ")";
					break;
				case SearchType.ByBusinessGroup:
					searcher.Filter = "(hpBusinessGroup=" + searchTarget + ")";
					break;
				case SearchType.ByPhone:
					searcher.Filter = "(telephoneNumber=" + searchTarget + ")";
					break;
				default:
					throw new ArgumentOutOfRangeException("searchType", searchType, "Unsupported SearchType.");
			}

			try
			{
				SetLDAPProperties(ref searcher);
				SearchResultCollection results = searcher.FindAll();

				foreach (SearchResult result in results)
				{
					Profiler profiler = new Profiler();

					profiler.Load(result);
					{
						this._profilerList.Add(profiler);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured in PeopleFinder internal user Search method - " + ex.Message, ex);
			}
		}

		/// <summary>
		/// Search External LDAP based on external user's email.
		/// </summary>
		public void Search(string targetExternalEmail)
		{
			string appSetting_ExternalLdapServer = GetAppSettingExternalLdapServer();
			string appSetting_ExternalLdapLogin = GetAppSettingExternalLdapLogin();
			string appSetting_ExternalLdapPassword = GetAppSettingExternalLdapPassword();
			string ldapPathSearchString = "LDAP://" + appSetting_ExternalLdapServer + "/" + SearchExternalBaseDN;
			DirectoryEntry objDirEntry =
				new DirectoryEntry(ldapPathSearchString, appSetting_ExternalLdapLogin, appSetting_ExternalLdapPassword)
					{AuthenticationType = AuthenticationTypes.SecureSocketsLayer};

			DirectorySearcher searcher = new DirectorySearcher(objDirEntry) {Filter = "(uid=" + targetExternalEmail + ")"};

			try
			{
				SetExternalLDAPProperties(ref searcher);
				SearchResultCollection results = searcher.FindAll();

				foreach (SearchResult result in results)
				{
					Profiler profiler = new Profiler();

					profiler.LoadExternal(result);
					{
						this._profilerList.Add(profiler);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured in PeopleFinder external user search method - " + ex.Message, ex);
			}
		}

		/// <summary>
		/// Result count from search 
		/// </summary>
		public int ProfilerCount()
		{
			return this._profilerList.Count;
		}


		/// <summary>
		/// return element in profiler list
		/// </summary>
		public Profiler GetProfiler(int index)
		{
			if (this._profilerList.Count == 0)
			{
				//throw new InvalidOperationException("There is no result found for a specific search, profile list is empty search");
				return null;
			}
			else if (index >= this._profilerList.Count)
			{
				throw new IndexOutOfRangeException("Invalid index, index is not in the range of the profiler list");
			}
			return (Profiler)this._profilerList[index];
		}

		/// <summary>
		/// return profiler
		/// </summary>
		public Profiler GetProfiler()
		{
			return this.GetProfiler(0);
		}


		/// <summary>
		/// Setting up LDAP properties 
		/// </summary>
		private static void SetLDAPProperties(ref DirectorySearcher searcher)
		{
			searcher.PropertiesToLoad.Add("ntUserDomainId");
			searcher.PropertiesToLoad.Add("telephoneNumber");
			searcher.PropertiesToLoad.Add("cn");
			searcher.PropertiesToLoad.Add("hpBusinessGroup");
			searcher.PropertiesToLoad.Add("uid");
			searcher.PropertiesToLoad.Add("co");
			searcher.PropertiesToLoad.Add("employeeType");
			searcher.PropertiesToLoad.Add("givenName");
			searcher.PropertiesToLoad.Add("sn");
			searcher.PropertiesToLoad.Add("HpStatus");
			searcher.PropertiesToLoad.Add("hpWorkLocation");
			searcher.PropertiesToLoad.Add("timeZone");
			searcher.PropertiesToLoad.Add("timeZoneName");
			searcher.PropertiesToLoad.Add("hpOrganizationChart");
			searcher.PropertiesToLoad.Add("hpBusinessUnit");
			searcher.PropertiesToLoad.Add("ou");
			searcher.PropertiesToLoad.Add("hpMRUCode");
			searcher.PropertiesToLoad.Add("hpBusinessRegionAcronym");
			searcher.PropertiesToLoad.Add("hpBusinessRegion");
			searcher.PropertiesToLoad.Add("manager");
		}


		/// <summary>
		/// Setting up LDAP properties 
		/// </summary>
		private static void SetExternalLDAPProperties(ref DirectorySearcher searcher)
		{
			searcher.PropertiesToLoad.Add("telephoneNumber");
			searcher.PropertiesToLoad.Add("cn");
			searcher.PropertiesToLoad.Add("c");
			searcher.PropertiesToLoad.Add("uid");
			searcher.PropertiesToLoad.Add("st");
			//NOTE: "hprole" is not currently supported because accessing it requires special LDAP permissions.
			//searcher.PropertiesToLoad.Add("hprole");
			searcher.PropertiesToLoad.Add("givenName");
			searcher.PropertiesToLoad.Add("sn");
			searcher.PropertiesToLoad.Add("hpBPNumber");
			searcher.PropertiesToLoad.Add("preferredgivenname");
			searcher.PropertiesToLoad.Add("hpcertificatenotafter");
			searcher.PropertiesToLoad.Add("hpduns");

		}

		#endregion
	}



}