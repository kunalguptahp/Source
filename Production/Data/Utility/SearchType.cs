using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ElementsCPS.Data.Utility
{
	/// <summary>
	/// LDAP search Type 
	/// </summary>
	public enum SearchType
	{
		ByNTID,
		ByPhone,
		ByCommonName,
		ByBusinessGroup,
		ByEmail,
		ByFirstName,
		ByLastName
	}

}
