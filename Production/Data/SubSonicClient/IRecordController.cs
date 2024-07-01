using System;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	public interface IRecordController
	{
		/// <summary>
		/// Returns the Type of the ActiveRecord the controller is associated with.
		/// </summary>
		Type GetRecordType();

		/// <summary>
		/// Returns the TableSchema of the ActiveRecord the controller is associated with.
		/// </summary>
		TableSchema.Table GetRecordSchema();
	}
}