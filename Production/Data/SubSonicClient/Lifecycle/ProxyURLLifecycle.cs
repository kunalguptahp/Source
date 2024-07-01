using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;

namespace HP.ElementsCPS.Data.SubSonicClient.Lifecycle
{
	/// <summary>
	/// The workflow engine and finite state machine for the <see cref="ProxyURL"/> entity.
	/// </summary>
	public sealed class ProxyURLLifecycle : BaseLifecycle<ProxyURL, ProxyURLStateId>
	{

		#region Constructors and Factory Methods

		private ProxyURLLifecycle()
		{
		}

		#region Singleton Instance Accessor

		private static readonly ProxyURLLifecycle _Current = new ProxyURLLifecycle();

		public static ProxyURLLifecycle Current
		{
			get { return ProxyURLLifecycle._Current; }
			//private set { ProxyURLLifecycle._Current = value; }
		}

		#endregion

		#endregion

		#region Interrogative Methods

		#region IsStateTransitionAllowed Helper Methods

		protected override IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_SpecialStates(ProxyURLStateId fromState, ProxyURLStateId toState, bool isAutotransition)
		{
			//the None state is a virtual start state, and may only proceed to certain specific non-virtual start states
			if (fromState == ProxyURLStateId.None)
			{
				switch (toState)
				{
					case ProxyURLStateId.Modified:
						break; //allowed
					case ProxyURLStateId.None:
					case ProxyURLStateId.ReadyForValidation:
					case ProxyURLStateId.Validated:
					case ProxyURLStateId.Published:
					case ProxyURLStateId.PublishedProductionOnly:
					case ProxyURLStateId.Cancelled:
					case ProxyURLStateId.Abandoned:
					case ProxyURLStateId.Deleted:
						return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					default:
						throw new ArgumentOutOfRangeException("toState");
				}
			}

			//NOTE: At this point, all transition rules enforced by this method have been evaluated, and we have found no reasons for this method to disallow the specified transition
			return new string[0];
		}

		protected override IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_StatesPaths(ProxyURLStateId fromState, ProxyURLStateId toState, bool isAutotransition)
		{
			//verify that a valid transition path exists between the specified fromState and toState (from the perspective of the fromState's valid exit paths)
			switch (fromState)
			{
				case ProxyURLStateId.None:
					switch (toState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ProxyURLStateId.Modified:
					switch (toState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ProxyURLStateId.ReadyForValidation:
					switch (toState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ProxyURLStateId.Validated:
					switch (toState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ProxyURLStateId.Published:
					switch (toState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ProxyURLStateId.PublishedProductionOnly:
					switch (toState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ProxyURLStateId.Cancelled:
					switch (toState)
					{
						case ProxyURLStateId.Published:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ProxyURLStateId.Abandoned:
					switch (toState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ProxyURLStateId.Deleted:
					switch (toState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				default:
					throw new ArgumentOutOfRangeException("fromState");
			}
			switch (fromState)
			{
				case ProxyURLStateId.None:
					switch (toState)
					{
						case ProxyURLStateId.NewState:
							break; //no (additional) restrictions
						case ProxyURLStateId.None:
						case ProxyURLStateId.Setup:
						case ProxyURLStateId.Sustaining:
						case ProxyURLStateId.Obsolete:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ProxyURLStateId.NewState:
					switch (toState)
					{
						case ProxyURLStateId.Setup:
						case ProxyURLStateId.Obsolete:
							break; //no (additional) restrictions
						case ProxyURLStateId.None:
						case ProxyURLStateId.NewState:
						case ProxyURLStateId.Sustaining:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ProxyURLStateId.Setup:
					switch (toState)
					{
						case ProxyURLStateId.Sustaining:
						case ProxyURLStateId.Obsolete:
							break; //no (additional) restrictions
						case ProxyURLStateId.None:
						case ProxyURLStateId.NewState:
						case ProxyURLStateId.Setup:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ProxyURLStateId.Sustaining:
					switch (toState)
					{
						case ProxyURLStateId.Setup:
						case ProxyURLStateId.Obsolete:
							break; //no (additional) restrictions
						case ProxyURLStateId.None:
						case ProxyURLStateId.NewState:
						case ProxyURLStateId.Sustaining:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ProxyURLStateId.Obsolete:
					switch (toState)
					{
						case ProxyURLStateId.NewState:
						case ProxyURLStateId.Setup:
						case ProxyURLStateId.Sustaining:
							break; //no (additional) restrictions
						case ProxyURLStateId.None:
						case ProxyURLStateId.Obsolete:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("fromState");
			}

			//NOTE: If the above switch is logically complete, then we should be able to simply return true if we reach this point.
			//However, in case there are any omissions or errors in the above logic, we will enforce the logic from the reverse perspective before returning
			//return true;

			//verify that a valid transition path exists between the specified fromState and toState (from the perspective of the toState's valid entry paths)
			switch (toState)
			{
				case ProxyURLStateId.None:
				case ProxyURLStateId.Modified:
				case ProxyURLStateId.ReadyForValidation:
				case ProxyURLStateId.Validated:
				case ProxyURLStateId.Published:
				case ProxyURLStateId.PublishedProductionOnly:
				case ProxyURLStateId.Cancelled:
				case ProxyURLStateId.Abandoned:
				case ProxyURLStateId.Deleted:
					break;
				default:
					throw new ArgumentOutOfRangeException("toState");
			}
			switch (toState)
			{
				case ProxyURLStateId.None:
					return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
				case ProxyURLStateId.NewState:
					switch (fromState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break;
						default:
							throw new ArgumentOutOfRangeException("fromState");
					}
					switch (fromState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Obsolete:
							break; //no (additional) restrictions
						case ProxyURLStateId.NewState:
						case ProxyURLStateId.Setup:
						case ProxyURLStateId.Sustaining:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ProxyURLStateId.Setup:
					switch (fromState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break;
						default:
							throw new ArgumentOutOfRangeException("fromState");
					}
					switch (fromState)
					{
						case ProxyURLStateId.NewState:
						case ProxyURLStateId.Sustaining:
						case ProxyURLStateId.Obsolete:
							break; //no (additional) restrictions
						case ProxyURLStateId.None:
						case ProxyURLStateId.Setup:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ProxyURLStateId.Sustaining:
					switch (fromState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break;
						default:
							throw new ArgumentOutOfRangeException("fromState");
					}
					switch (fromState)
					{
						case ProxyURLStateId.Setup:
						case ProxyURLStateId.Obsolete:
							break; //no (additional) restrictions
						case ProxyURLStateId.None:
						case ProxyURLStateId.NewState:
						case ProxyURLStateId.Sustaining:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ProxyURLStateId.Obsolete:
					switch (fromState)
					{
						case ProxyURLStateId.None:
						case ProxyURLStateId.Modified:
						case ProxyURLStateId.ReadyForValidation:
						case ProxyURLStateId.Validated:
						case ProxyURLStateId.Published:
						case ProxyURLStateId.PublishedProductionOnly:
						case ProxyURLStateId.Cancelled:
						case ProxyURLStateId.Abandoned:
						case ProxyURLStateId.Deleted:
							break;
						default:
							throw new ArgumentOutOfRangeException("fromState");
					}
					switch (fromState)
					{
						case ProxyURLStateId.NewState:
						case ProxyURLStateId.Setup:
						case ProxyURLStateId.Sustaining:
							break; //no (additional) restrictions
						case ProxyURLStateId.None:
						case ProxyURLStateId.Obsolete:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("toState");
			}

			//NOTE: At this point, all transition rules enforced by this method have been evaluated, and we have found no reasons for this method to disallow the specified transition
			return new string[0];
		}

		protected override IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_RoleBasedRules(ProxyURLStateId fromState, ProxyURLStateId toState, ICollection<UserRoleId> requestorRoles, bool isAutotransition)
		{
			//First, enforce the "simple" transition rules requiring knowledge of only roles and fromState

			//Next, enforce the "simple" transition rules requiring knowledge of only roles and toState

			//Next, enforce other transition rules requiring knowledge of roles

			//NOTE: At this point, all transition rules enforced by this method have been evaluated, and we have found no reasons for this method to disallow the specified transition
			return new string[0];
		}

		#endregion

		#region IsDataValidForState Method

		public override IEnumerable<string> GetReasonsWhyDataIsInvalidForState(ProxyURL instance, ProxyURLStateId state, bool isCurrentState, bool isAutotransition)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(instance, "instance");

			Product product = instance.Product;

			//start with an empty list of reasons (indicating no reasons for invalidity, i.e. valid)
			List<string> reasons = new List<string>();

			//Validate that state transitions are not "locked"
			if ((!isCurrentState) && instance.IsCurrentStateLocked)
			{
				//NOTE: This special reason is returned immediately, without checking for any other reasons.
				return new[] { "State transitions are currently prohibited. (This instance's state transition lock is enabled.) Try again later." };
			}

			//Validate that all "required" fields are valid
			if (string.IsNullOrEmpty(instance.Name.TrimUnlessNull()))
			{
				reasons.AddRange(new[] { "Invalid value: Name is required." });
			}

			//A ProxyURL should not be able to enter OR remain the SUSTAINING state if the ProxyURL External ID is blank.
			if (state == ProxyURLStateId.Sustaining)
			{
				if (string.IsNullOrEmpty(instance.ExternalId.TrimUnlessNull()))
				{
					reasons.AddRange(new[] { "Invalid value: ExternalId is required." });
				}
			}

			//A ProxyURL should not be able to remain the NEW state if the ProxyURL is linked to a Product which has state Setup
			if (isCurrentState && (state == ProxyURLStateId.NewState))
			{
				if ((product != null) && (product.CurrentState == ProductStateId.Setup))
				{
					reasons.AddRange(new[] { "This localization's Product is in Setup." });
				}
			}

			//A ProxyURL should not be able to enter OR remain in state OBSOLETE if the linked Product is NOT in OBSOLETE state.
			if (state == ProxyURLStateId.Obsolete)
			{
				if ((product != null) && (product.CurrentState != ProductStateId.Obsolete))
				{
					reasons.AddRange(new[] { "This localization's Product is not Obsolete." });
				}
			}

			//A ProxyURL should not be able to remain the NEW state if the ProxyURL is linked to any ProxyURLComponent that is in Setup state.
			if (isCurrentState && (state == ProxyURLStateId.NewState))
			{
				IEnumerable<ProxyURLComponent> relatedProxyURLComponentsInSetup =
					ProxyURLController.GetProxyURLComponents(instance.Id, ProxyURLComponentStateId.Setup);
				int relatedProxyURLComponentsInSetupCount = relatedProxyURLComponentsInSetup.Count();
				if (relatedProxyURLComponentsInSetupCount > 0)
				{
					reasons.AddRange(new[] { "This localization has some related Localized Product Components in Setup." });
				}
			}

			if (!isCurrentState)
			{
				//enforce task transition gate rules
				bool hasUnresolvedTransitionGateTasks =
					TaskController.GetUnresolvedProxyURLTransitionGateTaskCount(instance.Id) > 0;
				if (hasUnresolvedTransitionGateTasks)
				{
					reasons.AddRange(new[] { "There are tasks that must be resolved." });
				}
			}

			//A ProxyURL should not be able to remain the NEW OR SUSTAINING state if the LP has any unresolved LP Stage Gate tasks
			if (isCurrentState)
			{
				switch (state)
				{
					case ProxyURLStateId.NewState:
					case ProxyURLStateId.Sustaining:
						bool hasUnresolvedTransitionGateTasks =
							TaskController.GetUnresolvedProxyURLTransitionGateTaskCount(instance.Id) > 0;
						if (hasUnresolvedTransitionGateTasks)
						{
							reasons.AddRange(new[] { "There are Setup tasks that must be resolved." });
						}
						break;
				}
			}

			//A ProxyURL should not be able to enter OR remain in any state except Obsolete if the linked Product is Obsolete
			if ((product != null) && (product.CurrentState == ProductStateId.Obsolete))
			{
				if (state != ProxyURLStateId.Obsolete)
				{
					reasons.AddRange(new[] { "This localization's Product is Obsolete." });
				}
			}

			//NOTE: At this point, all transition rules enforced by this method have been evaluated, 
			//and if the list is empty we have found no reasons for this method to disallow the specified transition

			//remove any null or empty values which may have accumulated
			reasons.RemoveAll(s => string.IsNullOrEmpty(s));

			//return the set of reasons
			return reasons;
		}

		#endregion

		#endregion

		#region State Transition Methods

		#region GetDefaultStatePrecedenceSequence Method

		/// <summary>
		/// Returns an ordered sequence indicating the precedence of <see cref="ProxyURLStateId"/>s when auto-transitioning.
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<ProxyURLStateId> GetDefaultStatePrecedenceSequence(ProxyURLStateId currentState)
		{
			switch (currentState)
			{
				case ProxyURLStateId.None:
				case ProxyURLStateId.Modified:
				case ProxyURLStateId.ReadyForValidation:
				case ProxyURLStateId.Validated:
				case ProxyURLStateId.Published:
				case ProxyURLStateId.PublishedProductionOnly:
				case ProxyURLStateId.Cancelled:
				case ProxyURLStateId.Abandoned:
				case ProxyURLStateId.Deleted:
					return new ProxyURLStateId[] { }; //no autotransitions allowed
				default:
					throw new ArgumentOutOfRangeException("currentState");
			}
		}

		#endregion

		#region GoToState... Methods

		/// <summary>
		/// Transitions the specified instance to a specified state.
		/// </summary>
		/// <param name="instance">The instance to transition.</param>
		/// <param name="toState">The state to transition the instance to.</param>
		protected override void PerformStateTransition(ProxyURL instance, ProxyURLStateId toState, bool isAutotransition)
		{
			//perform custom pre-transition operations
			switch (toState)
			{
				case ProxyURLStateId.None:
				case ProxyURLStateId.Modified:
				case ProxyURLStateId.ReadyForValidation:
				case ProxyURLStateId.Validated:
				case ProxyURLStateId.Published:
				case ProxyURLStateId.PublishedProductionOnly:
				case ProxyURLStateId.Cancelled:
				case ProxyURLStateId.Abandoned:
				case ProxyURLStateId.Deleted:
					break;
				default:
					break;
			}

			this.PerformSimpleStateTransition(instance, toState, isAutotransition);

			//perform custom post-transition operations
			switch (toState)
			{
				case ProxyURLStateId.None:
				case ProxyURLStateId.Modified:
				case ProxyURLStateId.ReadyForValidation:
				case ProxyURLStateId.Validated:
				case ProxyURLStateId.Published:
				case ProxyURLStateId.PublishedProductionOnly:
				case ProxyURLStateId.Cancelled:
				case ProxyURLStateId.Abandoned:
				case ProxyURLStateId.Deleted:
					break;
				default:
					break;
			}
		}

		#endregion

		#endregion

	}
}
