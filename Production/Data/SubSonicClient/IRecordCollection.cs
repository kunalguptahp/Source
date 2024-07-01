using System;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	public interface IRecordCollection
	{
		/// <summary>
		/// Returns the Type of the ActiveRecord the controller is associated with.
		/// </summary>
		Type GetRecordType();
	}
}