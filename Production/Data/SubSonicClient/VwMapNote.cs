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
	/// The non-generated portion of the VwMapNote class.
	/// </summary>
	public partial class VwMapNote
	{

		#region Properties

		/// <summary>
		/// Strongly-typed convenience wrapper for the EntityTypeIdentifier property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="EntityTypeId"/> value is passed to the property.
		/// </exception>
		public EntityTypeIdentifier EntityTypeIdentifierEnumValue
		{
			get
			{
				return (EntityTypeIdentifier)this.EntityTypeId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "EntityType", typeof(EntityTypeIdentifier));
				this.EntityTypeId = (int)value;
			}
		}

		#endregion

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of a VwMapNote.
		/// </summary>
		/// <param name="instance">The <see cref="VwMapNote"/> to format.</param>
		/// <returns></returns>
		private static string Format(VwMapNote instance)
		{
			return string.Format(CultureInfo.CurrentCulture, "VwMapNote #{0} (EntityType={1},Entity={2}) ({3})", instance.Id, instance.EntityTypeIdentifierEnumValue, instance.EntityId, instance.Name);
		}

		#endregion

	}
}
