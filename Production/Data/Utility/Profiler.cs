
using System;
using System.DirectoryServices;


namespace HP.ElementsCPS.Data.Utility
{
	public class Profiler
	{
		#region Properties

		/// <summary>
		/// User Phone 
		/// </summary>
		public string Phone { get; private set; }

		/// <summary>
		/// User email 
		/// </summary>
		public string Email { get; private set; }

		/// <summary>
		/// User NTID 
		/// </summary>
		public string NTID { get; private set; }

		/// <summary>
		/// User Manager 
		/// </summary>
		public string Manager { get; private set; }

		/// <summary>
		/// User Common Name 
		/// </summary>
		public string CommonName { get; private set; }

		/// <summary>
		/// User Business Group 
		/// </summary>
		public string BusinessGroup { get; private set; }

		/// <summary>
		/// User Country 
		/// </summary>
		public string Country { get; private set; }

		/// <summary>
		/// User Employee Type 
		/// </summary>
		public string EmployeeType { get; private set; }

		/// <summary>
		/// User First Name 
		/// </summary>
		public string FirstName { get; private set; }

		/// <summary>
		/// User Last Name 
		/// </summary>
		public string LastName { get; private set; }

		/// <summary>
		/// User Status 
		/// </summary>
		public string Status { get; private set; }

		/// <summary>
		/// User TimeZone 
		/// </summary>
		public string TimeZone { get; private set; }

		/// <summary>
		/// User TimeZone Name 
		/// </summary>
		public string TimeZoneName { get; private set; }

		/// <summary>
		/// User BusinessUnit
		/// </summary>
		public string BusinessUnit { get; private set; }

		/// <summary>
		/// User MRU 
		/// </summary>
		public string MRU { get; private set; }

		/// <summary>
		/// User MRUCode 
		/// </summary>
		public string MRUCode { get; private set; }

		/// <summary>
		/// User Business Region Code 
		/// </summary>
		public string BusinessRegionCode { get; private set; }

		/// <summary>
		/// User Business Region  
		/// </summary>
		public string BusinessRegion { get; private set; }

		/// <summary>
		/// User Business Region  
		/// </summary>
		public string WorkLocation { get; private set; }

		/// <summary>
		/// User Business Region  
		/// </summary>
		public string OrganizationChart { get; private set; }

		/// <summary>
		/// State 
		/// </summary>
		public string State { get; private set; }

		/// <summary>
		/// HP Role 
		/// </summary>
		[Obsolete("Not currently supported because accessing this data requires special LDAP permissions.", true)]
		public string HPRole { get; private set; }

		/// <summary>
		/// HP DUN 
		/// </summary>
		public string HPDUN { get; private set; }

		#endregion

		#region Methods

		/// <summary>
		/// Load personnal information from LDAP
		/// </summary>
		internal void Load(SearchResult result)
		{
			this.Phone = result.Properties.Contains("telephoneNumber") ? result.Properties["telephoneNumber"][0].ToString() : "";
			this.Email = result.Properties.Contains("uid") ? result.Properties["uid"][0].ToString() : "";
			this.NTID = result.Properties.Contains("ntUserDomainId") ? result.Properties["ntUserDomainId"][0].ToString() : "";
			this.CommonName = result.Properties.Contains("cn") ? result.Properties["cn"][0].ToString() : "";
			this.BusinessGroup = result.Properties.Contains("hpBusinessGroup")
									? result.Properties["hpBusinessGroup"][0].ToString()
									: "";
			this.Country = result.Properties.Contains("co") ? result.Properties["co"][0].ToString() : "";
			this.EmployeeType = result.Properties.Contains("employeeType") ? result.Properties["employeeType"][0].ToString() : "";
			this.FirstName = result.Properties.Contains("givenName") ? result.Properties["givenName"][0].ToString() : "";
			this.LastName = result.Properties.Contains("sn") ? result.Properties["sn"][0].ToString() : "";
			this.Status = result.Properties.Contains("HpStatus") ? result.Properties["HpStatus"][0].ToString() : "";
			this.WorkLocation = result.Properties.Contains("hpWorkLocation")
									? result.Properties["hpWorkLocation"][0].ToString()
									: "";
			this.TimeZone = result.Properties.Contains("timeZone") ? result.Properties["timeZone"][0].ToString() : "";
			this.TimeZoneName = result.Properties.Contains("timeZoneName") ? result.Properties["timeZoneName"][0].ToString() : "";
			this.OrganizationChart = result.Properties.Contains("hpOrganizationChart")
										? result.Properties["hpOrganizationChart"][0].ToString()
										: "";
			this.BusinessUnit = result.Properties.Contains("hpBusinessUnit")
									? result.Properties["hpBusinessUnit"][0].ToString()
									: "";
			this.MRU = result.Properties.Contains("ou") ? result.Properties["ou"][0].ToString() : "";
			this.MRUCode = result.Properties.Contains("hpMRUCode") ? result.Properties["hpMRUCode"][0].ToString() : "";
			this.BusinessRegionCode = result.Properties.Contains("hpBusinessRegionAcronym")
										? result.Properties["hpBusinessRegionAcronym"][0].ToString()
										: "";
			this.BusinessRegion = result.Properties.Contains("hpBusinessRegion")
									? result.Properties["hpBusinessRegion"][0].ToString()
									: "";
			this.Manager = result.Properties.Contains("manager") ? result.Properties["manager"][0].ToString() : "";
		}

		internal void LoadExternal(SearchResult result)
		{
			this.Phone = result.Properties.Contains("telephoneNumber") ? result.Properties["telephoneNumber"][0].ToString() : "";
			this.Email = result.Properties.Contains("uid") ? result.Properties["uid"][0].ToString() : "";
			this.CommonName = result.Properties.Contains("cn") ? result.Properties["cn"][0].ToString() : "";
			this.FirstName = result.Properties.Contains("givenName") ? result.Properties["givenName"][0].ToString() : "";
			this.LastName = result.Properties.Contains("sn") ? result.Properties["sn"][0].ToString() : "";
			this.Country = result.Properties.Contains("c") ? result.Properties["c"][0].ToString() : "";
			this.State = result.Properties.Contains("st") ? result.Properties["st"][0].ToString() : "";
			//this.HPRole = result.Properties.Contains("hprole") ? result.Properties["hprole"][0].ToString() : "";
			this.HPDUN = result.Properties.Contains("hpduns") ? result.Properties["hpduns"][0].ToString() : "";

		}

		#endregion
	}
}
