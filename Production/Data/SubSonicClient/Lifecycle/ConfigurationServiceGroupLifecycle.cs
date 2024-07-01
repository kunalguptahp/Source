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
	/// The workflow engine and finite state machine for the <see cref="ConfigurationServiceGroup"/> entity.
	/// </summary>
	public sealed class ConfigurationServiceGroupLifecycle : BaseLifecycle<ConfigurationServiceGroup, ConfigurationServiceGroupStateId>
	{

		#region Constructors and Factory Methods

		private ConfigurationServiceGroupLifecycle()
		{
		}

		#region Singleton Instance Accessor

		private static readonly ConfigurationServiceGroupLifecycle _Current = new ConfigurationServiceGroupLifecycle();

		public static ConfigurationServiceGroupLifecycle Current
		{
			get { return ConfigurationServiceGroupLifecycle._Current; }
			//private set { ConfigurationServiceGroupLifecycle._Current = value; }
		}

		#endregion

		#endregion

		#region Interrogative Methods

		#region IsStateTransitionAllowed Helper Methods

		protected override IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_SpecialStates(ConfigurationServiceGroupStateId fromState, ConfigurationServiceGroupStateId toState, bool isAutotransition)
		{
			//the None state is a virtual start state, and may only proceed to certain specific non-virtual start states
			if (fromState == ConfigurationServiceGroupStateId.None)
			{
				switch (toState)
				{
					case ConfigurationServiceGroupStateId.Modified:
						break; //allowed
					case ConfigurationServiceGroupStateId.None:
					case ConfigurationServiceGroupStateId.ReadyForValidation:
					case ConfigurationServiceGroupStateId.Validated:
					case ConfigurationServiceGroupStateId.Published:
					case ConfigurationServiceGroupStateId.PublishedProductionOnly:
					case ConfigurationServiceGroupStateId.Cancelled:
					case ConfigurationServiceGroupStateId.Abandoned:
					case ConfigurationServiceGroupStateId.Deleted:
						return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					default:
						throw new ArgumentOutOfRangeException("toState");
				}
			}

			//NOTE: At this point, all transition rules enforced by this method have been evaluated, and we have found no reasons for this method to disallow the specified transition
			return new string[0];
		}

		protected override IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_StatesPaths(ConfigurationServiceGroupStateId fromState, ConfigurationServiceGroupStateId toState, bool isAutotransition)
		{
			//verify that a valid transition path exists between the specified fromState and toState (from the perspective of the fromState's valid exit paths)
			switch (fromState)
			{
				case ConfigurationServiceGroupStateId.None:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ConfigurationServiceGroupStateId.Modified:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ConfigurationServiceGroupStateId.ReadyForValidation:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ConfigurationServiceGroupStateId.Validated:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ConfigurationServiceGroupStateId.Published:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ConfigurationServiceGroupStateId.PublishedProductionOnly:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ConfigurationServiceGroupStateId.Cancelled:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ConfigurationServiceGroupStateId.Abandoned:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break; //no (additional) restrictions
						default:
							throw new ArgumentOutOfRangeException("toState");
					}
					break; //no (additional) restrictions
				case ConfigurationServiceGroupStateId.Deleted:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
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
				case ConfigurationServiceGroupStateId.None:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.NewState:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Setup:
						case ConfigurationServiceGroupStateId.Sustaining:
						case ConfigurationServiceGroupStateId.Obsolete:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ConfigurationServiceGroupStateId.NewState:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.Setup:
						case ConfigurationServiceGroupStateId.Obsolete:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.NewState:
						case ConfigurationServiceGroupStateId.Sustaining:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ConfigurationServiceGroupStateId.Setup:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.Sustaining:
						case ConfigurationServiceGroupStateId.Obsolete:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.NewState:
						case ConfigurationServiceGroupStateId.Setup:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ConfigurationServiceGroupStateId.Sustaining:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.Setup:
						case ConfigurationServiceGroupStateId.Obsolete:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.NewState:
						case ConfigurationServiceGroupStateId.Sustaining:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ConfigurationServiceGroupStateId.Obsolete:
					switch (toState)
					{
						case ConfigurationServiceGroupStateId.NewState:
						case ConfigurationServiceGroupStateId.Setup:
						case ConfigurationServiceGroupStateId.Sustaining:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Obsolete:
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
				case ConfigurationServiceGroupStateId.None:
				case ConfigurationServiceGroupStateId.Modified:
				case ConfigurationServiceGroupStateId.ReadyForValidation:
				case ConfigurationServiceGroupStateId.Validated:
				case ConfigurationServiceGroupStateId.Published:
				case ConfigurationServiceGroupStateId.PublishedProductionOnly:
				case ConfigurationServiceGroupStateId.Cancelled:
				case ConfigurationServiceGroupStateId.Abandoned:
				case ConfigurationServiceGroupStateId.Deleted:
					break;
				default:
					throw new ArgumentOutOfRangeException("toState");
			}
			switch (toState)
			{
				case ConfigurationServiceGroupStateId.None:
					return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
				case ConfigurationServiceGroupStateId.NewState:
					switch (fromState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break;
						default:
							throw new ArgumentOutOfRangeException("fromState");
					}
					switch (fromState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Obsolete:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.NewState:
						case ConfigurationServiceGroupStateId.Setup:
						case ConfigurationServiceGroupStateId.Sustaining:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ConfigurationServiceGroupStateId.Setup:
					switch (fromState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break;
						default:
							throw new ArgumentOutOfRangeException("fromState");
					}
					switch (fromState)
					{
						case ConfigurationServiceGroupStateId.NewState:
						case ConfigurationServiceGroupStateId.Sustaining:
						case ConfigurationServiceGroupStateId.Obsolete:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Setup:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ConfigurationServiceGroupStateId.Sustaining:
					switch (fromState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break;
						default:
							throw new ArgumentOutOfRangeException("fromState");
					}
					switch (fromState)
					{
						case ConfigurationServiceGroupStateId.Setup:
						case ConfigurationServiceGroupStateId.Obsolete:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.NewState:
						case ConfigurationServiceGroupStateId.Sustaining:
						default:
							return new[] { string.Format("Impossible transition path: {0:G} to {1:G}.", fromState, toState) };
					}
					break;
				case ConfigurationServiceGroupStateId.Obsolete:
					switch (fromState)
					{
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Modified:
						case ConfigurationServiceGroupStateId.ReadyForValidation:
						case ConfigurationServiceGroupStateId.Validated:
						case ConfigurationServiceGroupStateId.Published:
						case ConfigurationServiceGroupStateId.PublishedProductionOnly:
						case ConfigurationServiceGroupStateId.Cancelled:
						case ConfigurationServiceGroupStateId.Abandoned:
						case ConfigurationServiceGroupStateId.Deleted:
							break;
						default:
							throw new ArgumentOutOfRangeException("fromState");
					}
					switch (fromState)
					{
						case ConfigurationServiceGroupStateId.NewState:
						case ConfigurationServiceGroupStateId.Setup:
						case ConfigurationServiceGroupStateId.Sustaining:
							break; //no (additional) restrictions
						case ConfigurationServiceGroupStateId.None:
						case ConfigurationServiceGroupStateId.Obsolete:
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

		protected override IEnumerable<string> GetReasonsWhyStateTransitionIsInvalidHelper_RoleBasedRules(ConfigurationServiceGroupStateId fromState, ConfigurationServiceGroupStateId toState, ICollection<UserRoleId> requestorRoles, bool isAutotransition)
		{
			//First, enforce the "simple" transition rules requiring knowledge of only roles and fromState

			//Next, enforce the "simple" transition rules requiring knowledge of only roles and toState

			//Next, enforce other transition rules requiring knowledge of roles

			//NOTE: At this point, all transition rules enforced by this method have been evaluated, and we have found no reasons for this method to disallow the specified transition
			return new string[0];
		}

		#endregion

		#region IsDataValidForState Method

		public override IEnumerable<string> GetReasonsWhyDataIsInvalidForState(ConfigurationServiceGroup instance, ConfigurationServiceGroupStateId state, bool isCurrentState, bool isAutotransition)
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

			//A ConfigurationServiceGroup should not be able to enter OR remain the SUSTAINING state if the ConfigurationServiceGroup External ID is blank.
			if (state == ConfigurationServiceGroupStateId.Sustaining)
			{
				if (string.IsNullOrEmpty(instance.ExternalId.TrimUnlessNull()))
				{
					reasons.AddRange(new[] { "Invalid value: ExternalId is required." });
				}
			}

			//A ConfigurationServiceGroup should not be able to remain the NEW state if the ConfigurationServiceGroup is linked to a Product which has state Setup
			if (isCurrentState && (state == ConfigurationServiceGroupStateId.NewState))
			{
				if ((product != null) && (product.CurrentState == ProductStateId.Setup))
				{
					reasons.AddRange(new[] { "This localization's Product is in Setup." });
				}
			}

			//A ConfigurationServiceGroup should not be able to enter OR remain in state OBSOLETE if the linked Product is NOT in OBSOLETE state.
			if (state == ConfigurationServiceGroupStateId.Obsolete)
			{
				if ((product != null) && (product.CurrentState != ProductStateId.Obsolete))
				{
					reasons.AddRange(new[] { "This localization's Product is not Obsolete." });
				}
			}

			//A ConfigurationServiceGroup should not be able to remain the NEW state if the ConfigurationServiceGroup is linked to any ConfigurationServiceGroupComponent that is in Setup state.
			if (isCurrentState && (state == ConfigurationServiceGroupStateId.NewState))
			{
				IEnumerable<ConfigurationServiceGroupComponent> relatedConfigurationServiceGroupComponentsInSetup =
					ConfigurationServiceGroupController.GetConfigurationServiceGroupComponents(instance.Id, ConfigurationServiceGroupComponentStateId.Setup);
				int relatedConfigurationServiceGroupComponentsInSetupCount = relatedConfigurationServiceGroupComponentsInSetup.Count();
				if (relatedConfigurationServiceGroupComponentsInSetupCount > 0)
				{
					reasons.AddRange(new[] { "This localization has some related Localized Product Components in Setup." });
				}
			}

			if (!isCurrentState)
			{
				//enforce task transition gate rules
				bool hasUnresolvedTransitionGateTasks =
					TaskController.GetUnresolvedConfigurationServiceGroupTransitionGateTaskCount(instance.Id) > 0;
				if (hasUnresolvedTransitionGateTasks)
				{
					reasons.AddRange(new[] { "There are tasks that must be resolved." });
				}
			}

			//A ConfigurationServiceGroup should not be able to remain the NEW OR SUSTAINING state if the LP has any unresolved LP Stage Gate tasks
			if (isCurrentState)
			{
				switch (state)
				{
					case ConfigurationServiceGroupStateId.NewState:
					case ConfigurationServiceGroupStateId.Sustaining:
						bool hasUnresolvedTransitionGateTasks =
							TaskController.GetUnresolvedConfigurationServiceGroupTransitionGateTaskCount(instance.Id) > 0;
						if (hasUnresolvedTransitionGateTasks)
						{
							reasons.AddRange(new[] { "There are Setup tasks that must be resolved." });
						}
						break;
				}
			}

			//A ConfigurationServiceGroup should not be able to enter OR remain in any state except Obsolete if the linked Product is Obsolete
			if ((product != null) && (product.CurrentState == ProductStateId.Obsolete))
			{
				if (state != ConfigurationServiceGroupStateId.Obsolete)
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
		/// Returns an ordered sequence indicating the precedence of <see cref="ConfigurationServiceGroupStateId"/>s when auto-transitioning.
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<ConfigurationServiceGroupStateId> GetDefaultStatePrecedenceSequence(ConfigurationServiceGroupStateId currentState)
		{
			switch (currentState)
			{
				case ConfigurationServiceGroupStateId.None:
				case ConfigurationServiceGroupStateId.Modified:
				case ConfigurationServiceGroupStateId.ReadyForValidation:
				case ConfigurationServiceGroupStateId.Validated:
				case ConfigurationServiceGroupStateId.Published:
				case ConfigurationServiceGroupStateId.PublishedProductionOnly:
				case ConfigurationServiceGroupStateId.Cancelled:
				case ConfigurationServiceGroupStateId.Abandoned:
				case ConfigurationServiceGroupStateId.Deleted:
					return new ConfigurationServiceGroupStateId[] { }; //no autotransitions allowed
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
		protected override void PerformStateTransition(ConfigurationServiceGroup instance, ConfigurationServiceGroupStateId toState, bool isAutotransition)
		{
			//perform custom pre-transition operations
			switch (toState)
			{
				case ConfigurationServiceGroupStateId.None:
				case ConfigurationServiceGroupStateId.Modified:
				case ConfigurationServiceGroupStateId.ReadyForValidation:
				case ConfigurationServiceGroupStateId.Validated:
				case ConfigurationServiceGroupStateId.Published:
				case ConfigurationServiceGroupStateId.PublishedProductionOnly:
				case ConfigurationServiceGroupStateId.Cancelled:
				case ConfigurationServiceGroupStateId.Abandoned:
				case ConfigurationServiceGroupStateId.Deleted:
					break;
				default:
					break;
			}

			this.PerformSimpleStateTransition(instance, toState, isAutotransition);

			//perform custom post-transition operations
			switch (toState)
			{
				case ConfigurationServiceGroupStateId.None:
				case ConfigurationServiceGroupStateId.Modified:
				case ConfigurationServiceGroupStateId.ReadyForValidation:
				case ConfigurationServiceGroupStateId.Validated:
				case ConfigurationServiceGroupStateId.Published:
				case ConfigurationServiceGroupStateId.PublishedProductionOnly:
				case ConfigurationServiceGroupStateId.Cancelled:
				case ConfigurationServiceGroupStateId.Abandoned:
				case ConfigurationServiceGroupStateId.Deleted:
					break;
				default:
					break;
			}
		}

		#endregion

		#endregion

	}
}
