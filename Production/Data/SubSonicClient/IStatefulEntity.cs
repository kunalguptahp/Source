using System;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	public interface IStatefulEntity<TEntityStateEnum>
	{
		/// <summary>
		/// The entity's current lifecycle state within the lifecycle defined for the entity.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="TEntityStateEnum"/> value is passed to the property.
		/// </exception>
		TEntityStateEnum CurrentState { get; set; }

		/// <summary>
		/// Indicates whether a "transition lock" is currently preventing the entity's current lifecycle state from changing.
		/// This is most commonly used to prevent another state transition from occurring while a first transition is in process but incomplete.
		/// </summary>
		bool IsCurrentStateLocked { get; set; }

	}
}