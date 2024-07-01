using System.Globalization;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	partial class RowStatus
	{
		/// <summary>
		/// Enumerates the primary keys of the records in the RowStatus table.
		/// </summary>
		/// <remarks>
		/// NOTE: The IDs in the RowStatus table MUST match the values defined in this enumeration.
		/// </remarks>
		public enum RowStatusId
		{
			Active = 1,
			Inactive = 2,
			Deleted = 3
		}

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of a RowStatus.
		/// </summary>
		/// <param name="instance">The <see cref="RowStatus"/> to format.</param>
		/// <returns></returns>
		private static string Format(RowStatus instance)
		{
			return string.Format(CultureInfo.CurrentCulture, "RowStatus #{0} ({1})", instance.Id, instance.Name);
		}

		#endregion

	}
}