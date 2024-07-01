using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Utility;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The workflow engine and finite state machine for the <see cref="TEntity"/> entity.
	/// </summary>
	/// <typeparam name="TEntity">The type of the data entity whose lifecycle is being modeled.</typeparam>
	/// <typeparam name="TEntityStateEnum">The type of the enum whose values represent the possible lifecycle states of the lifecycle.</typeparam>
	public abstract class BaseLifecycle<TEntity, TEntityStateEnum>
		where TEntity : ActiveRecord<TEntity>, IActiveRecord, IRecord, IStatefulEntity<TEntityStateEnum>, new()
	{

		#region Methods to move into HPFx

		public static IList<TEnum> GetEnumValues<TEnum>(Type enumType)
		{
			//TODO: Refactoring: Move this into HPFx
#warning Refactoring: Move this into HPFx

			Array enumValues = Enum.GetValues(enumType);
			//NOTE: Per the .NET API Docs, the Array class can be safely cast to IEnumerable<T>, ICollection<T>, or IList<T> at runtime
			return enumValues as IList<TEnum>; //return (IList<TEnum>)enumValues;
		}

		#endregion

		#region Constructors and Factory Methods

		#endregion

		#region Interrogative Methods

		#region GetAllStates Method

		public IList<TEntityStateEnum> GetAllStates()
		{
			return GetEnumValues<TEntityStateEnum>(typeof(TEntityStateEnum));
		}

		#endregion

		#region IsStateTransitionAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition between two specified <see cref="TEntityStateEnum"/>s.
		/// </summary>
		/// <param name="fromState">The initial/starting state that the TEntity would transition from.</param>
		/// <param name="toState">The new state that the TEntity would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for an autotransition to <paramref name="state"/>.
		/// Else, validates whether the data is valid for an explicitly-initiated transition or a non-transition (depending on the value of <paramref name="isCurrentState"/>).
		/// </param>
		/// <returns><c>true</c> if the specified transition is allowed.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if either <paramref name="fromState"/> or <paramref name="toState"/> is invalid.
		/// </exception>
		public virtual bool IsStateTransitionAllowed(TEntityStateEnum fromState, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles, bool isAutotransition)
		{
			IEnumerable<string> reasons = this.GetReasonsWhyStateTransitionIsInvalid(fromState, toState, requestorRoles, isAutotransition);
			return reasons.Count() <= 0;
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition to a specified <see cref="TEntityStateEnum"/> 
		/// from a specified TEntity's current <see cref="TEntityStateEnum"/>.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="toState">The new state that the TEntity would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for an autotransition to <paramref name="state"/>.
		/// Else, validates whether the data is valid for an explicitly-initiated transition.
		/// </param>
		/// <returns></returns>
		public virtual bool IsStateTransitionAllowed(TEntity instance, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles, bool isAutotransition)
		{
			IEnumerable<string> reasons = this.GetReasonsWhyStateTransitionIsInvalid(instance, toState, requestorRoles, isAutotransition);
			return reasons.Count() <= 0;
		}

		public virtual bool IsStateTransitionAllowed(TEntity instance, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles)
		{
			return IsStateTransitionAllowed(instance, toState, requestorRoles, false);
		}

		#endregion

		#region GetReasonsWhyStateTransitionIsInvalid Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition between two specified <see cref="TEntityStateEnum"/>s.
		/// </summary>
		/// <param name="fromState">The initial/starting state that the TEntity would transition from.</param>
		/// <param name="toState">The new state that the TEntity would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for an autotransition to <paramref name="state"/>.
		/// Else, validates whether the data is valid for an explicitly-initiated transition.
		/// </param>
		/// <returns><c>true</c> if the specified transition is allowed.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if either <paramref name="fromState"/> or <paramref name="toState"/> is invalid.
		/// </exception>
		public virtual IEnumerable<string> GetReasonsWhyStateTransitionIsInvalid(TEntityStateEnum fromState, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles, bool isAutotransition)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(fromState, "fromState", typeof(TEntityStateEnum));
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(TEntityStateEnum));

			////a transition must be to a real state
			//if (toState == TEntityStateEnum.None)
			//{
			//   return false;
			//   //throw new ArgumentException("toState cannot be None", "toState");
			//}

			//a transition must be between 2 distinct states
			if (Object.Equals(fromState, toState) && !this.IsSameStateTransitionAllowed())
			{
				return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
			}

			//start with an empty list of reasons (indicating no reasons for invalidity, i.e. valid)
			List<string> reasons = new List<string>();

			//Next, enforce the state-based transition rules involving start states, end states, and virtual states
			reasons.AddRange(GetReasonsWhyStateTransitionIsInvalidHelper_SpecialStates(fromState, toState, isAutotransition));
			//Next, enforce the more complex state-based transition rules that require examination of both the fromState and the toState
			reasons.AddRange(GetReasonsWhyStateTransitionIsInvalidHelper_StatesPaths(fromState, toState, isAutotransition));

			//account for inherited roles
			requestorRoles = SecurityManager.Current.GetEffectiveRoles(requestorRoles);

			//Next, enforce the transition rules requiring knowledge of roles and state
			reasons.AddRange(GetReasonsWhyStateTransitionIsInvalidHelper_RoleBasedRules(fromState, toState, requestorRoles, isAutotransition));

			//Next, enforce other transition rules
			reasons.AddRange(this.GetReasonsWhyStateTransitionIsInvalidHelper_OtherRules(fromState, toState, requestorRoles, isAutotransition));

			//remove any null or empty values which may have accumulated
			reasons.RemoveAll(s => string.IsNullOrEmpty(s));

			//return the set of reasons
			return reasons;
		}

		public IEnumerable<string> GetReasonsWhyStateTransitionIsInvalid(TEntityStateEnum fromState, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles)
		{
			return GetReasonsWhyStateTransitionIsInvalid(fromState, toState, requestorRoles, false);
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition to a specified <see cref="TEntityStateEnum"/> 
		/// from a specified TEntity's current <see cref="TEntityStateEnum"/>.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="toState">The new state that the TEntity would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for an autotransition to <paramref name="toState"/>.
		/// Else, validates whether the data is valid for an explicitly-initiated transition.
		/// </param>
		/// <returns></returns>
		public virtual IEnumerable<string> GetReasonsWhyStateTransitionIsInvalid(TEntity instance, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles, bool isAutotransition)
		{
			//start with an empty list of reasons (indicating no reasons for invalidity, i.e. valid)
			List<string> reasons = new List<string>();

			if (instance.IsDirty)
			{
				//TODO: Review Needed: Should state transition be allowed for Dirty instances?
#warning Review Needed: Should state transition be allowed for Dirty instances?
				//reasons.Add("The instance has unsaved modifications pending.");
			}

			//Next, enforce standard non-instance-based transition rules
			reasons.AddRange(this.GetReasonsWhyStateTransitionIsInvalid(instance.CurrentState, toState, requestorRoles, isAutotransition));

			//Next, enforce data validity rules transition rules
			reasons.AddRange(this.GetReasonsWhyDataIsInvalidForState(instance, toState, false, isAutotransition));

			//remove any null or empty values which may have accumulated
			reasons.RemoveAll(s => string.IsNullOrEmpty(s));

			//return the set of reasons
			return reasons;
		}

		public virtual IEnumerable<string> GetReasonsWhyStateTransitionIsInvalid(TEntity instance, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles)
		{
			return GetReasonsWhyStateTransitionIsInvalid(instance, toState, requestorRoles, false);
		}

		#region GetReasonsWhyDataIsInvalidForState Helper Methods

		/// <summary>
		/// Indicates whether there are any "special" state transition restrictions that disallow a direct transition between a specified fromState and a specified toState.
		/// </summary>
		/// <remarks>
		/// This is a helper method, and only enforces a subset of the rules that may restrict certain state transitions.
		/// Therefore, this method should not be used in isolation 
		/// because the other related helper methods may enforce additional restrictions that are unknown to (or undeterminable by) this method.
		/// </remarks>
		/// <seealso cref="IsStateTransitionAllowed(HP.ElementsCPS.Data.SubSonicClient.TEntityStateEnum,HP.ElementsCPS.Data.SubSonicClient.TEntityStateEnum,System.Collections.Generic.ICollection{HP.ElementsCPS.Core.Security.UserRoleId})"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_RoleBasedRules"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_SpecialStates"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_StatesPaths"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_OtherRules"/>
		protected abstract IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_SpecialStates(TEntityStateEnum fromState, TEntityStateEnum toState, bool isAutotransition);

		/// <summary>
		/// Indicates whether any valid scenario exists that would allow a direct transition between a specified fromState and a specified toState.
		/// I.e. Indicates the existence or nonexistence of any valid path between the two states on a state transition diagram.
		/// </summary>
		/// <remarks>
		/// This is a helper method, and only enforces a subset of the rules that may restrict certain state transitions.
		/// Therefore, this method should not be used in isolation 
		/// because the other related helper methods may enforce additional restrictions that are unknown to (or undeterminable by) this method.
		/// </remarks>
		/// <seealso cref="IsStateTransitionAllowed(HP.ElementsCPS.Data.SubSonicClient.TEntityStateEnum,HP.ElementsCPS.Data.SubSonicClient.TEntityStateEnum,System.Collections.Generic.ICollection{HP.ElementsCPS.Core.Security.UserRoleId})"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_RoleBasedRules"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_SpecialStates"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_StatesPaths"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_OtherRules"/>
		protected abstract IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_StatesPaths(TEntityStateEnum fromState, TEntityStateEnum toState, bool isAutotransition);

		/// <summary>
		/// Indicates whether any valid scenario exists that would allow a user with a specific set of <see cref="UserRoleId"/>s 
		/// to successfully cause a direct transition between a specified fromState and a specified toState.
		/// </summary>
		/// <remarks>
		/// This is a helper method, and only enforces a subset of the rules that may restrict certain state transitions.
		/// Therefore, this method should not be used in isolation 
		/// because the other related helper methods may enforce additional restrictions that are unknown to (or undeterminable by) this method.
		/// </remarks>
		/// <seealso cref="IsStateTransitionAllowed(HP.ElementsCPS.Data.SubSonicClient.TEntityStateEnum,HP.ElementsCPS.Data.SubSonicClient.TEntityStateEnum,System.Collections.Generic.ICollection{HP.ElementsCPS.Core.Security.UserRoleId})"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_RoleBasedRules"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_SpecialStates"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_StatesPaths"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_OtherRules"/>
		protected abstract IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_RoleBasedRules(TEntityStateEnum fromState, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles, bool isAutotransition);

		/// <summary>
		/// </summary>
		/// <remarks>
		/// This is a helper method, and only enforces a subset of the rules that may restrict certain state transitions.
		/// Therefore, this method should not be used in isolation 
		/// because the other related helper methods may enforce additional restrictions that are unknown to (or undeterminable by) this method.
		/// </remarks>
		/// <seealso cref="IsStateTransitionAllowed(HP.ElementsCPS.Data.SubSonicClient.TEntityStateEnum,HP.ElementsCPS.Data.SubSonicClient.TEntityStateEnum,System.Collections.Generic.ICollection{HP.ElementsCPS.Core.Security.UserRoleId})"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_RoleBasedRules"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_SpecialStates"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_StatesPaths"/>
		/// <seealso cref="GetReasonsWhyStateTransitionIsInvalidHelper_OtherRules"/>
		protected virtual IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_OtherRules(TEntityStateEnum fromState, TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles, bool isAutotransition)
		{
			return new string[0];
		}

		#endregion

		#endregion

		#region IsDataValidForState Method

		/// <summary>
		/// Indicates whether the specified instance's current data is valid/compatible with a specified <see cref="TEntityStateEnum"/>.
		/// </summary>
		/// <param name="instance">The instance whose data and properties will be validated.</param>
		/// <param name="state">The state the data will be validated against.</param>
		/// <param name="isCurrentState">
		/// If <c>true</c>, then validates whether the data is still valid for <paramref name="state"/> assuming <paramref name="instance"/> is already in that state.
		/// Else, validates whether the data is valid for a state transition to <paramref name="state"/>.
		/// </param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for an autotransition to <paramref name="state"/>.
		/// Else, validates whether the data is valid for an explicitly-initiated transition or a non-transition (depending on the value of <paramref name="isCurrentState"/>).
		/// </param>
		/// <returns></returns>
		public bool IsDataValidForState(TEntity instance, TEntityStateEnum state, bool isCurrentState, bool isAutotransition)
		{
			IEnumerable<string> reasons = this.GetReasonsWhyDataIsInvalidForState(instance, state, isCurrentState, false);
			return reasons.Count() <= 0;
		}

		public bool IsDataValidForState(TEntity instance, TEntityStateEnum state, bool isCurrentState)
		{
			return IsDataValidForState(instance, state, isCurrentState, false);
		}

		#endregion

		#region GetReasonsWhyDataIsInvalidForState Method

		/// <summary>
		/// Indicates one or more reasons why the specified instance's current data is invalid/incompatible 
		/// with a specified <see cref="TEntityStateEnum"/>.
		/// </summary>
		/// <param name="instance">The instance whose data and properties will be validated.</param>
		/// <param name="state">The state the data will be validated against.</param>
		/// <param name="isCurrentState">
		/// If <c>true</c>, then validates whether the data is still valid for <paramref name="state"/> assuming <paramref name="instance"/> is already in that state.
		/// Else, validates whether the data is valid for a state transition to <paramref name="state"/>.
		/// </param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for an autotransition to <paramref name="state"/>.
		/// Else, validates whether the data is valid for an explicitly-initiated transition or a non-transition (depending on the value of <paramref name="isCurrentState"/>).
		/// </param>
		/// <returns></returns>
		public abstract IEnumerable<string> GetReasonsWhyDataIsInvalidForState(TEntity instance, TEntityStateEnum state, bool isCurrentState, bool isAutotransition);

		public IEnumerable<string> GetReasonsWhyDataIsInvalidForState(TEntity instance, TEntityStateEnum state, bool isCurrentState)
		{
			return GetReasonsWhyDataIsInvalidForState(instance, state, isCurrentState, false);
		}

		#endregion

		#region IsStateTransitionPossible Method

		/// <summary>
		/// Indicates whether a user with a specified set of roles could ever be authorized to execute a transition to a specified <see cref="TEntityStateEnum"/> 
		/// (from any other possible state and in any hypothetically possible scenario).
		/// </summary>
		/// <param name="toState">The new state that the TEntity would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if any potential transition is possible.</returns>
		public bool IsStateTransitionPossible(TEntityStateEnum toState, ICollection<UserRoleId> requestorRoles)
		{
			IList<TEntityStateEnum> allStates = this.GetAllStates();
			foreach (TEntityStateEnum fromState in allStates)
			{
				if (this.IsStateTransitionAllowed(fromState, toState, requestorRoles, false)
					|| this.IsStateTransitionAllowed(fromState, toState, requestorRoles, true))
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		#region GetValidTransitionStates Method

		/// <summary>
		/// Returns all of the lifecycle states that could possibly be transitioned to, 
		/// based upon a specified set of <see cref="UserRoleId"/>s.
		/// </summary>
		/// <param name="requestorRoles"></param>
		/// <returns></returns>
		public IEnumerable<TEntityStateEnum> GetValidTransitionStates(ICollection<UserRoleId> requestorRoles)
		{
			return this.GetAllStates().Where(toState => this.IsStateTransitionPossible(toState, requestorRoles));
		}

		/// <summary>
		/// Returns all of the lifecycle states that could possibly be transitioned to, 
		/// based upon a specified current state and a specified set of <see cref="UserRoleId"/>s.
		/// </summary>
		/// <param name="fromState"></param>
		/// <param name="requestorRoles"></param>
		/// <param name="includeFromState">
		/// If <c>true</c>, then the specified <paramref name="fromState"/> is always included in the returned set,
		/// regardless of whether a "circular transition" from that state to the same state is possible.
		/// This case represents the scenario where no state transition occurs, 
		/// and where the <paramref name="fromState"/> would simply remain the current state.
		/// </param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for autotransitions to each state.
		/// Else, validates whether the data is valid for explicitly-initiated transitions to each state.
		/// </param>
		/// <returns></returns>
		public IEnumerable<TEntityStateEnum> GetValidTransitionStates(TEntityStateEnum fromState, ICollection<UserRoleId> requestorRoles, bool includeFromState, bool isAutotransition)
		{
			IEnumerable<TEntityStateEnum> validStates = this.GetAllStates().Where(toState => this.IsStateTransitionAllowed(fromState, toState, requestorRoles, isAutotransition));
			if (includeFromState)
			{
				validStates = validStates.Concat(new[] { fromState }).Distinct();
			}
			return validStates;
		}

		public IEnumerable<TEntityStateEnum> GetValidTransitionStates(TEntityStateEnum fromState, ICollection<UserRoleId> requestorRoles, bool includeFromState)
		{
			IEnumerable<TEntityStateEnum> validExplicitTransitionStates = this.GetValidTransitionStates(fromState, requestorRoles, includeFromState, false);
			IEnumerable<TEntityStateEnum> validAutotransitionStates = this.GetValidTransitionStates(fromState, requestorRoles, includeFromState, true);
			return validExplicitTransitionStates.Concat(validAutotransitionStates).Distinct();
		}

		/// <summary>
		/// Returns all of the lifecycle states that could possibly be transitioned to, 
		/// based upon a specified entity instance and a specified set of <see cref="UserRoleId"/>s.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="requestorRoles"></param>
		/// <param name="includeCurrentStateIfStillValid"></param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the transition is valid for autotransitions.
		/// Else, validates whether the transition is valid for explicit transitions.
		/// </param>
		/// <returns></returns>
		public IEnumerable<TEntityStateEnum> GetValidTransitionStates(TEntity instance, ICollection<UserRoleId> requestorRoles, bool includeCurrentStateIfStillValid, bool isAutotransition)
		{
			IList<TEntityStateEnum> allStates = this.GetAllStates();
			IEnumerable<TEntityStateEnum> validStates = allStates.Where(toState => this.IsStateTransitionAllowed(instance, toState, requestorRoles, isAutotransition));
			if (includeCurrentStateIfStillValid)
			{
				TEntityStateEnum currentState = instance.CurrentState;
				bool currentStateIsValid = this.IsDataValidForState(instance, currentState, true, isAutotransition);
				if (currentStateIsValid)
				{
					validStates = validStates.Concat(new[] { currentState }).Distinct();
				}
			}
			return validStates;
		}

		public IEnumerable<TEntityStateEnum> GetValidTransitionStates(TEntity instance, ICollection<UserRoleId> requestorRoles, bool includeCurrentStateIfStillValid)
		{
			IEnumerable<TEntityStateEnum> validExplicitTransitionStates = this.GetValidTransitionStates(instance, requestorRoles, includeCurrentStateIfStillValid, false);
			IEnumerable<TEntityStateEnum> validAutotransitionStates = this.GetValidTransitionStates(instance, requestorRoles, includeCurrentStateIfStillValid, true);
			return validExplicitTransitionStates.Concat(validAutotransitionStates).Distinct();
		}

		#endregion

		#endregion

		#region State Transition Methods

		#region State Transition Helper Methods

		/// <summary>
		/// Validates that the current user is allowed to transition the current TEntity to a specified <see cref="TEntityStateEnum"/>.
		/// </summary>
		/// <param name="instance">The instance the state transition should be authorized for.</param>
		/// <param name="toState">The new state that the TEntity would transition to.</param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for an autotransition to <paramref name="toState"/>.
		/// Else, validates whether the data is valid for an explicitly-initiated transition.
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the state transition is not allowed or the current user is not authorized to initiate it.
		/// </exception>
		protected void AuthorizeStateTransition(TEntity instance, TEntityStateEnum toState, bool isAutotransition)
		{
			Person currentUser = PersonController.GetCurrentUser();
			List<UserRoleId> currentUserRoles = currentUser.GetRoles();
			TEntity instanceWithStateLockDisabled = instance;
			if (instanceWithStateLockDisabled.IsCurrentStateLocked)
			{
				//NOTE: Since the passed instance has the IsCurrentStateLocked flag set to true
				//(which will likely cause IsStateTransitionAllowed to return false),
				//and since we know that this method is protected and is only called once a transition is already "in progress"
				//(i.e. it should only be called from within the same thread that set the IsCurrentStateLocked flag),
				//we therefore create a 2nd instance (with the IsCurrentStateLocked flag set to false)
				//in order to validate whether there are any reasons to abort the transition OTHER THAN the IsCurrentStateLocked flag,
				//and then call IsStateTransitionAllowed using the 2nd instance.
				instanceWithStateLockDisabled = new TEntity();
				instance.CopyTo(instanceWithStateLockDisabled);
				//disable the state lock on the 2nd instance (only)
				instanceWithStateLockDisabled.IsCurrentStateLocked = false;
			}
			if (!this.IsStateTransitionAllowed(instanceWithStateLockDisabled, toState, currentUserRoles))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid state transition (from {0:G} to {1:G}) attempted on {2} by {3}.", instance.CurrentState, toState, instance, currentUser);
				LogManager.Current.Log(Severity.Info, instance, message);
				throw new InvalidOperationException(message);
			}
		}

		/// <summary>
		/// Helper method used by some of the Lifecycle-related methods.
		/// </summary>
		/// <param name="instance">The instance the state transition should be applied to.</param>
		/// <param name="toState">The new state that the TEntity would transition to.</param>
		/// <param name="throwIfComplexTransitionRequired"></param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then validates whether the data is valid for an autotransition to <paramref name="toState"/>.
		/// Else, validates whether the data is valid for an explicitly-initiated transition.
		/// </param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if <paramref name="toState"/> is invalid.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown if this method does not support state transition to the specified state.
		/// </exception>
		protected void PerformSimpleStateTransition(TEntity instance, TEntityStateEnum toState, bool isAutotransition)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(instance, "instance");
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(TEntityStateEnum));

			//validate the transition
			this.AuthorizeStateTransition(instance, toState, isAutotransition);

			TEntityStateEnum fromState = instance.CurrentState;
			string transitionType = (isAutotransition ? "Autotransition" : "Explicit transition");
			LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Pre-Transition: {3}: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance, transitionType));

			//perform the transition
			instance.CurrentState = toState;
			instance.Save(SecurityManager.CurrentUserIdentityName);

			LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Post-Transition: {3}: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance, transitionType));
		}

		/// <summary>
		/// Indicates whether this lifecycle allows transitions where the "from" and "to" states are the same state.
		/// </summary>
		/// <returns></returns>
		protected virtual bool IsSameStateTransitionAllowed()
		{
			return false;
		}

		#endregion

		#region GetDefaultStatePrecedenceSequence Method

		/// <summary>
		/// Returns an ordered sequence indicating the precedence of <see cref="TEntityStateEnum"/>s when auto-transitioning.
		/// </summary>
		/// <returns></returns>
		public virtual IEnumerable<TEntityStateEnum> GetDefaultStatePrecedenceSequence(TEntityStateEnum currentState)
		{
			return new TEntityStateEnum[] { };
		}

		#endregion

		#region AutotransitionState Method

		/// <summary>
		/// Initiates an automatic state transition to the highest-precedence state that this <see cref="TEntity"/> instance can be transitioned to.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="repeatAutomatically">If <c>true</c>, automatic transitions will continue until no more automatic transitions are possible.</param>
		/// <returns></returns>
		public bool AutotransitionState(TEntity instance, bool repeatAutomatically)
		{
			//NOTE: We use null to specify that the default sequence should be used. This may be critical when repeatAutomatically is true,
			//because the default sequence may be different for each state transition in a multi-transition autotransition.
			const IEnumerable<TEntityStateEnum> statePrecedenceSequence = null;
			return this.AutotransitionState(instance, repeatAutomatically, statePrecedenceSequence, true);
		}

		/// <summary>
		/// Initiates an automatic state transition to the highest-precedence state that this <see cref="TEntity"/> instance can be transitioned to.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="repeatAutomatically">If <c>true</c>, automatic transitions will continue until no more automatic transitions are possible.</param>
		/// <param name="statePrecedenceSequence">
		/// <c>null</c>, or an empty collection, or a specific precedence sequence (highest precedence first) of the states that automatic transitioning should be attempted for.
		/// <para>
		/// NOTE: A value of <c>null</c> indicates that the default precedence sequence(s) defined by the lifecycle should be used.
		/// Specifying a default (i.e. <c>null</c>) sequence will cause the sequence to be re-defaulted for each of the state transitions 
		/// in an auto-repeat autotransition.
		/// However, specifying a non-default (i.e. non<c>null</c>) sequence will cause the same precedence sequence to be used for each of the state transitions 
		/// in an auto-repeat autotransition.
		/// This difference could be critical factor in the behavior of the autotransition process when <paramref name="repeatAutomatically"/> is <c>true</c>,
		/// because there may be a separate default precedence sequence for each individual "current state".
		/// </para>
		/// <para>
		/// Specifying an empty collection will always result in no transition taking place.
		/// </para>
		/// </param>
		/// <param name="allowRemainInCurrentStateIfStillValid">
		/// If <c>true</c> and if the <paramref name="instance"/>'s current state is still valid, 
		/// then the instance will only autotransition to a valid state that has a higher precedence than it's current state 
		/// (meaning any states with a lower precedence than the instance's current state will be irrelevant).
		/// However if either of those conditions is untrue, 
		/// then automatic transitioning from the current state to a state with a lower precedence may occur 
		/// if all higher-precedence state transitions are invalid.
		/// </param>
		/// <returns><c>true</c> if at least one transition occurred; else <c>false</c>.</returns>
		public bool AutotransitionState(TEntity instance, bool repeatAutomatically, IEnumerable<TEntityStateEnum> statePrecedenceSequence, bool allowRemainInCurrentStateIfStillValid)
		{
			bool transitionOccurred = false;
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				if (repeatAutomatically)
				{
					int transitionCount = 0;
					while (AutotransitionState(instance, statePrecedenceSequence, allowRemainInCurrentStateIfStillValid, false))
					{
						transitionCount++;
					}
					transitionOccurred = transitionCount > 0;
				}
				else
				{
					transitionOccurred = AutotransitionState(instance, statePrecedenceSequence, allowRemainInCurrentStateIfStillValid, false);
				}
				scope.Complete(); // transaction complete
			}
			return transitionOccurred;
		}

		/// <summary>
		/// Initiates an automatic state transition to the highest-precedence state that this <see cref="TEntity"/> instance can be transitioned to.
		/// </summary>
		/// <param name="instance">The <see cref="TEntity"/> instance that automatic transitioning should be attempted for.</param>
		/// <param name="statePrecedenceSequence">
		/// <c>null</c>, or an empty collection, or a specific precedence sequence (highest precedence first) of the states that automatic transitioning should be attempted for.
		/// <para>
		/// NOTE: A value of <c>null</c> indicates that the default precedence sequence(s) defined by the lifecycle should be used.
		/// Specifying a default (i.e. <c>null</c>) sequence will cause the sequence to be re-defaulted for each of the state transitions 
		/// in an auto-repeat autotransition.
		/// However, specifying a non-default (i.e. non<c>null</c>) sequence will cause the same precedence sequence to be used for each of the state transitions 
		/// in an auto-repeat autotransition.
		/// This difference could be critical factor in the behavior of the autotransition process when <paramref name="repeatAutomatically"/> is <c>true</c>,
		/// because there may be a separate default precedence sequence for each individual "current state".
		/// </para>
		/// <para>
		/// Specifying an empty collection will always result in no transition taking place.
		/// </para>
		/// </param>
		/// <param name="allowRemainInCurrentStateIfStillValid">
		/// If <c>true</c> and if the <paramref name="instance"/>'s current state is still valid, 
		/// then the instance will only autotransition to a valid state that has a higher precedence than it's current state 
		/// (meaning any states with a lower precedence than the instance's current state will be irrelevant).
		/// However if either of those conditions is untrue, 
		/// then automatic transitioning from the current state to a state with a lower precedence may occur 
		/// if all higher-precedence state transitions are invalid.
		/// </param>
		/// <param name="allowRetransitionToCurrentState">
		/// If <c>true</c> and if a circular re-transition to (and from) the <paramref name="instance"/>'s current state is valid, 
		/// then the instance will re-transition to it's current state 
		/// unless there exists a valid autotransition to a state that has a higher precedence than it's current state.
		/// However if either of those conditions is untrue, 
		/// then automatic transitioning from the current state to a state with a lower precedence may occur 
		/// if all higher-precedence state transitions are invalid.
		/// </param>
		/// <returns><c>true</c> if at least one transition occurred; else <c>false</c>.</returns>
		protected bool AutotransitionState(
			TEntity instance,
			IEnumerable<TEntityStateEnum> statePrecedenceSequence,
			bool allowRemainInCurrentStateIfStillValid,
			bool allowRetransitionToCurrentState)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(instance, "instance");

			//default the statePrecedenceSequence if it was unspecified
			statePrecedenceSequence = statePrecedenceSequence ?? GetDefaultStatePrecedenceSequence(instance.CurrentState);

			ICollection<UserRoleId> requestorRoles = PersonController.GetCurrentUserRoles(true);

			TEntityStateEnum fromState = instance.CurrentState;
			LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Autotransition: Starting Autotransition: FROM {0:G}. FOR {2}.", fromState, null, instance));

			foreach (TEntityStateEnum toState in statePrecedenceSequence)
			{
				LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Autotransition: Examining Autotransition: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance));

				if (Object.Equals(toState, instance.CurrentState))
				{
					if (allowRemainInCurrentStateIfStillValid)
					{
						//if the current state is still a valid state for the instance 
						//(i.e. remaining in the current state won't violate any lifecycle state validation rules),
						//then don't transition
						bool currentStateIsStillValid = this.IsDataValidForState(instance, instance.CurrentState, true, true);
						if (currentStateIsStillValid)
						{
							LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Autotransition: Invalid Autotransition: Staying in current state based on precedence: {0:G}. FOR {2}.", fromState, toState, instance));
							return false;
						}
					}

					if (!allowRetransitionToCurrentState)
					{
						//if the code has reached this point then:
						// Fact 1: any/all transitions to all higher-precedence states are invalid
						// Fact 2: remaining in the current state (i.e. without a transition) is invalid
						// Fact 3: re-transitioning to the current state (i.e. FROM currentState TO currentState) is invalid
						// Therefore:.we should continue iterating to determine if we can find a vali transition to a lower-precedence state
						LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Autotransition: Invalid Autotransition: Skipping current state: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance));
						continue; //if we are neither remaining in (see above) nor re-transitioning to the current state, then skip the current state and continue with the next state in the precedence sequence
					}

					//if the code has reached this point then:
					// Fact 1: any/all transitions to all higher-precedence states are invalid
					// Fact 2: remaining in the current state (i.e. without a transition) is invalid
					// Fact 3: re-transitioning to the current state (i.e. FROM currentState TO currentState) is ALLOWED (but not necessarily valid)
					// Therefore:.since a re-transition is no different than any other transition, we simply allow execution to continue out of the "current state" if block
					//and allow the regular autotransition code below to execute normally.
					//NOTE: the code below will handle the rest of the circular re-transition logic
				}

				if (this.IsStateTransitionAllowed(instance, toState, requestorRoles, true))
				{
					LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Autotransition: Pre-Autotransition: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance));

					this.GoToState(instance, toState, true);

					LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Autotransition: Post-Autotransition: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance));
					return true;
				}
				else
				{
					LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Autotransition: Invalid Autotransition: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance));
				}
			}
			LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Autotransition: No valid autotransitions are possible. FROM {0:G}. FOR {2}.", fromState, null, instance));
			return false;
		}

		#endregion

		#region GoToState Method

		/// <summary>
		/// Helper method.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="toState"></param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then the transition should be handled as an autotransition to <paramref name="toState"/>.
		/// Else, it is handled as an explicitly-initiated transition.
		/// </param>
		protected void GoToState(TEntity instance, TEntityStateEnum toState, bool isAutotransition)
		{
			TEntityStateEnum fromState = instance.CurrentState;
			string transitionType = (isAutotransition ? "Autotransition" : "Explicit transition");
			LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Pre-Transition: {3}: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance, transitionType));

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				//prevent any other state transitions until we are done with this one
				instance.IsCurrentStateLocked = true;
				instance.Save(SecurityManager.CurrentUserIdentityName);

				this.PerformStateTransition(instance, toState, isAutotransition);

				//TODO: Refactoring: Extract the "reload instance" code below into a utility method
#warning Refactoring: Extract the "reload instance" code below into a utility method

				//NOTE: Need to reload the instance from the DB in order to take into account possible changes to the instance that occurred as a result of triggered side effects (above)
				instance.LoadByKey(instance.GetPrimaryKeyValue());
				bool instanceStillExists = !instance.IsNew;
				if (!instanceStillExists)
				{
					//NOTE: do not make any further changes to the instance, because it no longer exists
					instance = null;
				}
				else
				{
					//allow other state transitions now that we are done with this one
					instance.IsCurrentStateLocked = false;
					instance.Save(SecurityManager.CurrentUserIdentityName);

					//autotransition immediately if possible
					this.AutotransitionState(instance, true);
				}

				scope.Complete(); // transaction complete
			}

			LogManager.Current.Log(Severity.Debug, this, string.Format("Lifecycle: Post-Transition: {3}: FROM {0:G} TO {1:G}. FOR {2}.", fromState, toState, instance, transitionType));
		}

		public void GoToState(TEntity instance, TEntityStateEnum toState)
		{
			GoToState(instance, toState, false);
		}

		#endregion

		#region PerformStateTransition Method

		/// <summary>
		/// Helper method.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="toState"></param>
		/// <param name="isAutotransition">
		/// If <c>true</c>, then the transition should be handled as an autotransition to <paramref name="toState"/>.
		/// Else, it is handled as an explicitly-initiated transition.
		/// </param>
		protected abstract void PerformStateTransition(TEntity instance, TEntityStateEnum toState, bool isAutotransition);

		#endregion

		#endregion

	}
}
