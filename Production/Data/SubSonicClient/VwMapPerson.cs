using System;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapPerson class.
	/// </summary>
	public partial class VwMapPerson
	{

		#region Properties

		public Person InnerPerson
		{
			get { return Person.FetchByID(this.Id); }
		}

		#endregion

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of a VwMapPerson.
		/// </summary>
		/// <param name="instance">The <see cref="VwMapPerson"/> to format.</param>
		/// <returns></returns>
		private static string Format(VwMapPerson instance)
		{
			return string.Format(CultureInfo.CurrentCulture, "VwMapPerson #{0} ({1})", instance.Id, instance.Name);
		}

		#endregion

	}
}
