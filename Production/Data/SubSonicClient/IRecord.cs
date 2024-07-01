using System;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	public interface IRecord
	{

		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with this instance. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to this instance; otherwise, <c>false</c>.</returns>
		bool IdenticalTo(object obj);

	}
}