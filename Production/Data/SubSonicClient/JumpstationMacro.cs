using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using System.Text;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.CmService;
using HP.ElementsCPS.Data.Utility;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the JumpstationMacro class.
	/// </summary>
	public partial class JumpstationMacro : IStatefulEntity<JumpstationMacroStateId>
	{

		#region Implementation of IStatefulEntity<JumpstationMacroStateId>

		/// <summary>
		/// The entity's current lifecycle state within the lifecycle defined for the entity.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="JumpstationMacroStateId"/> value is passed to the property.
		/// </exception>
		public JumpstationMacroStateId CurrentState
		{
			get
			{
				return (JumpstationMacroStateId)this.JumpstationMacroStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "CurrentState", typeof(JumpstationMacroStateId));
				this.JumpstationMacroStatusId = (int)value;
			}
		}

		/// <summary>
		/// Indicates whether a "transition lock" is currently preventing the entity's current lifecycle state from changing.
		/// This is most commonly used to prevent another state transition from occurring while a first transition is in process but incomplete.
		/// </summary>
		public bool IsCurrentStateLocked
		{
			get { return false; }
			set
			{
				throw new NotImplementedException("State locking is not yet implemented for this entity.");
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Strongly-typed convenience wrapper for the JumpstationMacroStatusId property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="JumpstationMacroStateId"/> value is passed to the property.
		/// </exception>
		public JumpstationMacroStateId JumpstationMacroState
		{
			get
			{
				return (JumpstationMacroStateId)this.JumpstationMacroStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "Status", typeof(JumpstationMacroStateId));
				this.JumpstationMacroStatusId = (int)value;
			}
		}


		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? JumpstationMacroId = (this.IsNew ? null : (int?)this.Id);
			Log(JumpstationMacroId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? JumpstationMacroId = (this.IsNew ? null : (int?)this.Id);
			Log(JumpstationMacroId, severity, this, message, ex);
		}

		internal static void Log(int? JumpstationMacroId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro History: #{0}: {1}.", JumpstationMacroId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? JumpstationMacroId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro History: #{0}: {1}.", JumpstationMacroId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Creates a new <see cref="JumpstationMacro"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="JumpstationMacro"/>.</returns>
		public JumpstationMacro SaveAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Creates a new <see cref="JumpstationMacro"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="JumpstationMacro"/>.</returns>
		public JumpstationMacro SaveAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, copyElementsId);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public JumpstationMacro SaveAsNew()
		{
			return SaveAsNew(this, false, false);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="JumpstationMacro"/>.
		/// </summary>
		/// <param name="originalJumpstationMacro">The <see cref="JumpstationMacro"/> to copy/duplicate.</param>
		/// <param name="assignCurrentUserAsOwner">If <c>true</c>, then the new JumpstationMacro will be (re)assigned to the current user; else it will be assigned to the original JumpstationMacro's owner.</param>
		/// <param name="copyElementsId">If <c>true</c>, then new JumpstationMacro will maintain the existing ElementsIds (ValidationId and ProductionId)</param>
		private static JumpstationMacro SaveAsNew(JumpstationMacro originalJumpstationMacro, bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			JumpstationMacro newJumpstationMacro;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newJumpstationMacro = JumpstationMacro.Copy(originalJumpstationMacro);

				//mark the newJumpstationMacro instance as an unsaved instance
				newJumpstationMacro.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newJumpstationMacro.CreatedBy = createdBy;
				newJumpstationMacro.CreatedOn = createdOn;
				newJumpstationMacro.ModifiedBy = createdBy;
				newJumpstationMacro.ModifiedOn = createdOn;

				//modify the status fields' values appropriately
				newJumpstationMacro.JumpstationMacroState = JumpstationMacroStateId.Modified;

				if (!copyElementsId)
				{
					newJumpstationMacro.ValidationId = null;
					newJumpstationMacro.ProductionId = null;
				}

				newJumpstationMacro.RowStatusId = (int)RowStatus.RowStatusId.Active;

				if (assignCurrentUserAsOwner)
				{
					//assign the current user as the Owner of the newJumpstationMacro
					newJumpstationMacro.OwnerId = PersonController.GetCurrentUser().Id;
				}

				//save the newJumpstationMacro to the DB so that it is assigned an ID
				newJumpstationMacro.Save(SecurityManager.CurrentUserIdentityName);

                //save as new jumpstation macro value
                JumpstationMacroValueCollection jumpstationMacroValueCollection = new JumpstationMacroValueCollection();
                foreach (JumpstationMacroValue originalRecord in originalJumpstationMacro.JumpstationMacroValueRecords())
                {
                    JumpstationMacroValue newRecord = JumpstationMacroValue.Copy(originalRecord);
                    newRecord.MarkNew();
                    newRecord.JumpstationMacroId = newJumpstationMacro.Id;
                    newRecord.CreatedBy = createdBy;
                    newRecord.CreatedOn = createdOn;
                    newRecord.ModifiedBy = createdBy;
                    newRecord.ModifiedOn = createdOn;
                    jumpstationMacroValueCollection.Add(newRecord);
                }
                jumpstationMacroValueCollection.SaveAll(SecurityManager.CurrentUserIdentityName);

				originalJumpstationMacro.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newJumpstationMacro));
				newJumpstationMacro.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalJumpstationMacro));

				scope.Complete(); // transaction complete
			}

			return newJumpstationMacro;
		}

		#endregion

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of an JumpstationMacro.
		/// </summary>
		/// <param name="JumpstationMacro">The JumpstationMacro to format.</param>
		/// <returns></returns>
		private static string Format(JumpstationMacro JumpstationMacro)
		{
			return string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} ({1:G})", JumpstationMacro.Id, JumpstationMacro.JumpstationMacroState);
		}

		#endregion

		#region JumpstationMacro Lifecycle Methods

		#region Lifecycle-related Interrogative Methods

		#region IsDataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Configuration Service Group with a specific <see cref="JumpstationMacroState"/>.
		/// </summary>
		/// <param name="state">The state of the Configuration Service Group that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsDataModificationAllowed(JumpstationMacroStateId state, ICollection<UserRoleId> requestorRoles)
		{
			switch (state)
			{
				case JumpstationMacroStateId.ReadyForValidation:
				case JumpstationMacroStateId.Validated:
				case JumpstationMacroStateId.Published:
				case JumpstationMacroStateId.Cancelled:
					//Nobody can edit a Ready For Modification, Validated, Published or Cancelled Configuration Service Group
					return false;
			}

			//A user is allowed to "edit" an Configuration Service Group in this state if the user would be allowed to Abandon such an Configuration Service Group
			return IsStateTransitionAllowed(state, JumpstationMacroStateId.Abandoned, requestorRoles);
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify the Configuration Service Group.
		/// </summary>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> requestorRoles)
		{
			return IsDataModificationAllowed(this.JumpstationMacroState, requestorRoles);
		}

		#endregion

		#region IsMetadataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Configuration Service Group with a specific <see cref="JumpstationMacroState"/>.
		/// </summary>
		/// <param name="state">The state of the Configuration Service Group that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsMetadataModificationAllowed(JumpstationMacroStateId state, ICollection<UserRoleId> requestorRoles)
		{
			//NOTE: A specific user is allowed to "edit" an JumpstationMacro's "metadata" (e.g. tags) if the user is an Editor
			ICollection<UserRoleId> roles = SecurityManager.Current.GetEffectiveRoles(requestorRoles);
			return roles.Contains(UserRoleId.Editor);
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify the ProxyURL.
		/// </summary>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public bool IsMetadataModificationAllowed(ICollection<UserRoleId> requestorRoles)
		{
			//TODO: Implement: Configuration Service group-instance-specific metadata modification permission logic (if any)
			//#warning Not Implemented: Configuration Service group-instance-specific metadata modification permission logic (if any)

			return IsMetadataModificationAllowed(this.JumpstationMacroState, requestorRoles);
		}

		#endregion

		#region IsStateTransitionAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition between two specified <see cref="JumpstationMacroState"/>s.
		/// </summary>
		/// <param name="fromState">The initial/starting state that the ProxyURL would transition from.</param>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if the specified transition is allowed.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if either <paramref name="fromState"/> or <paramref name="toState"/> is invalid.
		/// </exception>
		public static bool IsStateTransitionAllowed(JumpstationMacroStateId fromState, JumpstationMacroStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(fromState, "fromState", typeof(JumpstationMacroStateId));
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(JumpstationMacroStateId));

			//a transition must be between 2 distinct states
			if (toState == JumpstationMacroStateId.None)
			{
				return false;
				//throw new ArgumentException("toState cannot be None", "toState");
			}

			//a transition must be between 2 distinct states
			if ((fromState == toState) && (fromState != JumpstationMacroStateId.Published))
			{
				return false;
				//throw new ArgumentException(
				//    string.Format(CultureInfo.CurrentCulture, "'fromState' must be different than 'toState'. ({0:G})", toState), "toState");
			}

			//account for inherited roles
			requestorRoles = SecurityManager.Current.GetEffectiveRoles(requestorRoles);

			//First, enforce the transition rules requiring knowledge of roles and state
			if (!IsStateTransitionAllowedHelper_RoleBasedRules(fromState, toState, requestorRoles))
			{
				return false;
			}

			//Next, enforce the state-based transition rules involving start states, end states, and virtual states
			if (!IsStateTransitionAllowedHelper_SpecialStates(fromState, toState))
			{
				return false;
			}

			//Next, enforce the more complex state-based transition rules that require examination of both the fromState and the toState
			if (!IsStateTransitionAllowedHelper_StatesPaths(fromState, toState))
			{
				return false;
			}

			//if none of the above rules have caused the method to exit yet, then the transition is allowed
			return true;
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition to a specified <see cref="JumpstationMacroState"/> 
		/// from the Configuration Service group's current <see cref="JumpstationMacroState"/>.
		/// </summary>
		/// <param name="toState">The new state that the Configuration Service group would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(JumpstationMacroStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			if (this.IsDirty)
			{
				return false;
			}

			if (!IsStateTransitionAllowed(this.JumpstationMacroState, toState, requestorRoles))
			{
				return false;
			}

			switch (toState)
			{
				case JumpstationMacroStateId.ReadyForValidation:
				case JumpstationMacroStateId.Validated:
				case JumpstationMacroStateId.Published:
					//moving to any of these states requires that the Configuration Service group be "publishable"
					if (!this.IsDataPublishable())
					{
						return false;
					}
					break;
				case JumpstationMacroStateId.Cancelled:
					if (!this.IsDataUnPublishable())
					{
						return false;
					}
					break;
				default:
					//case JumpstationMacroStateId.None:
					//case JumpstationMacroStateId.Modified:
					//case JumpstationMacroStateId.Abandoned:
					//case JumpstationMacroStateId.Deleted:
					break;
			}

			return true;
		}

		#region IsStateTransitionAllowed Helper Methods

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_StatesPaths(JumpstationMacroStateId fromState, JumpstationMacroStateId toState)
		{
			//verify that the current state can transition to the requested state
			switch (toState)
			{
				case SubSonicClient.JumpstationMacroStateId.Cancelled:
					if (fromState != JumpstationMacroStateId.Published)
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationMacroStateId.Validated:
					if (fromState != JumpstationMacroStateId.ReadyForValidation)
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationMacroStateId.Published:
					if (!((fromState == JumpstationMacroStateId.Validated) || (fromState == JumpstationMacroStateId.Published) || (fromState == JumpstationMacroStateId.Cancelled)))
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationMacroStateId.Modified:
					List<JumpstationMacroStateId> possibleFromStates_Modified = new List<JumpstationMacroStateId>(
						new[] { JumpstationMacroStateId.Abandoned, JumpstationMacroStateId.ReadyForValidation, JumpstationMacroStateId.Validated, JumpstationMacroStateId.Cancelled });
					if (!possibleFromStates_Modified.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationMacroStateId.ReadyForValidation:
					List<JumpstationMacroStateId> possibleFromStates_ReadyForValidation = new List<JumpstationMacroStateId>(
						new[] { JumpstationMacroStateId.Abandoned, JumpstationMacroStateId.Modified });
					if (!possibleFromStates_ReadyForValidation.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationMacroStateId.Abandoned:
					List<JumpstationMacroStateId> possibleFromStates_Abandoned = new List<JumpstationMacroStateId>(
						new[] { JumpstationMacroStateId.Modified, JumpstationMacroStateId.ReadyForValidation });
					if (!possibleFromStates_Abandoned.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationMacroStateId.Deleted:
					if (fromState != JumpstationMacroStateId.Abandoned)
					{
						return false;
					}
					break;
			}
			return true;
		}

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_SpecialStates(JumpstationMacroStateId fromState, JumpstationMacroStateId toState)
		{
			//the None state is a virtual start state, and may only proceed to the Modified state
			if (fromState == JumpstationMacroStateId.None)
			{
				return (toState == JumpstationMacroStateId.Modified);
			}
			return true;
		}

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_RoleBasedRules(JumpstationMacroStateId fromState, JumpstationMacroStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			//First, enforce the "simple" transition rules requiring knowledge of only roles and fromState
			switch (fromState)
			{
				case JumpstationMacroStateId.Validated:
				case JumpstationMacroStateId.Published:
				case JumpstationMacroStateId.Cancelled:
					//All transitions from Validated or Published (to any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case JumpstationMacroStateId.ReadyForValidation:
					//All transitions from ReadyForValidation (to any other state) require the Validator role
					if (!requestorRoles.Contains(UserRoleId.Validator))
					{
						return false;
					}
					break;
				case JumpstationMacroStateId.Modified:
					//All transitions from Modified (to any other state) require the Editor role
					if (!requestorRoles.Contains(UserRoleId.Editor))
					{
						return false;
					}
					break;
				default:
					//All transitions from any state not handled above (to any other state) require the Editor role
					if (!requestorRoles.Contains(UserRoleId.Editor))
					{
						return false;
					}
					break;
			}

			//Next, enforce the "simple" transition rules requiring knowledge of only roles and toState
			switch (toState)
			{
				case JumpstationMacroStateId.Cancelled:
				case JumpstationMacroStateId.Published:
					//All transitions to Cancelled or Published (from any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case JumpstationMacroStateId.Validated:
					//All transitions to Validated (from any other state) require the Validator role
					if (!requestorRoles.Contains(UserRoleId.Validator))
					{
						return false;
					}
					break;
				default:
					//All transitions to any state not handled above (from any other state) require the Editor role
					if (!requestorRoles.Contains(UserRoleId.Editor))
					{
						return false;
					}
					break;
			}
			return true;
		}

		#endregion

		#endregion

		/// <summary>
		/// Determines whether a user with a specified set of roles would be authorized to execute a transition to a specified <see cref="JumpstationMacroState"/> 
		/// (from any other possible state).
		/// </summary>
		/// <param name="toState">The new state that the JumpstationMacro would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if any potential transition is possible.</returns>
		public static bool IsStateTransitionPossible(JumpstationMacroStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			Array allStates = Enum.GetValues(typeof(JumpstationMacroStateId));
			foreach (JumpstationMacroStateId fromState in allStates)
			{
				if (IsStateTransitionAllowed(fromState, toState, requestorRoles))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Indicates whether the JumpstationMacro's data is complete and valid (i.e. could be published "as is").
		/// </summary>
		/// <returns></returns>
		public bool IsDataPublishable()
		{
			// Validate that all "required" fields are valid
            // One group selector is required.
            JumpstationMacroValueQuerySpecification jumpstationMacroValueQuerySpecification = new JumpstationMacroValueQuerySpecification() {JumpstationMacroId=this.Id };
            if(JumpstationMacroValueController.FetchCount(jumpstationMacroValueQuerySpecification)<1)
            {
                return false;
            }
            //validate macro values whether it contains 'inactive or deleted' data
            JumpstationMacroValueCollection record = JumpstationMacroValueController.Fetch(jumpstationMacroValueQuerySpecification);
            int count = 0;
            foreach (JumpstationMacroValue jumpstationMacroValue in record)
            {
                if (jumpstationMacroValue.RowStatusId > 1)
                {
                    count++;
                    if (count == record.Count())
                    {
                        return false;
                    }
                }
            }
         
			switch (this.JumpstationMacroState)
			{
				case JumpstationMacroStateId.Published:
					//Published JumpstationMacros can only be re-Published if they could also be UnPublished
					if (!this.IsDataUnPublishable())
					{
						return false;
					}
					break;
				default:
					break;
			}

			return true;
		}

		/// <summary>
		/// Indicates whether the JumpstationMacro's data is being replaced (Published JumpstationMacro has already been copied)
		/// </summary>
		/// <returns></returns>
		public bool IsOriginalJumpstationMacroReplacement()
		{
			if (this.ValidationId == null && this.ProductionId == null)
			{
				// not published so allow.
				return false;
			}
			else
			{
				if ((JumpstationMacroStateId)this.JumpstationMacroStatusId != JumpstationMacroStateId.Published)
				{
					// not original.
					return false;
				}
				else
				{
					// check to see if replacement
					JumpstationMacroQuerySpecification JumpstationMacroQuerySpecification = new JumpstationMacroQuerySpecification() { ValidationId = this.ValidationId };
					return JumpstationMacroController.FetchCount(JumpstationMacroQuerySpecification) > 1;
				}
			}
		}

		/// <summary>
		/// Indicates whether the JumpstationMacro's data is complete and valid enough to be UnPublished
		/// (i.e. could be unpublished "as is" ignoring the JumpstationMacro's State).
		/// </summary>
		/// <returns></returns>
		private bool IsDataUnPublishable()
		{
			return (this.ProductionId != null);
		}

		#endregion

		#region State Transition Methods

		#region State Transition Helper Methods

		/// <summary>
		/// Validates that the current user is allowed to transition the current Jumpstation Group to a specified <see cref="JumpstationMacroState"/>.
		/// </summary>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the state transition is not allowed or the current user is not authorized to initiate it.
		/// </exception>
		private void AuthorizeStateTransition(JumpstationMacroStateId toState)
		{
			Person currentUser = PersonController.GetCurrentUser();
			List<UserRoleId> currentUserRoles = currentUser.GetRoles();
			if (!this.IsStateTransitionAllowed(toState, currentUserRoles))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid Configuration Service Group State transition (from {0:G} to {1:G}) attempted on {2} by {3}.", this.JumpstationMacroState, toState, this, currentUser);
				LogManager.Current.Log(Severity.Info, this, message);
				throw new InvalidOperationException(message);
			}
		}

		/// <summary>
		/// Helper method used by some of the Lifecycle-related methods.
		/// </summary>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if <paramref name="toState"/> is invalid.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown if this method does not support state transition to the specified state.
		/// </exception>
		private void PerformSimpleStateTransition(JumpstationMacroStateId toState)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(JumpstationMacroStateId));

			//validate that the toState does not require a "complex transition"
			switch (toState)
			{
				case JumpstationMacroStateId.None:
				case JumpstationMacroStateId.Published:
				case JumpstationMacroStateId.Cancelled:
					throw new InvalidOperationException(
						string.Format(CultureInfo.CurrentCulture,
							"The PerformSimpleStateTransition() method cannot be used to transition to state {0:G}", toState));
			}

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			JumpstationMacroStateId fromState = this.JumpstationMacroState;
			this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Configuration Service Group state changing: from '{0:G}' to '{1:G}'.", fromState, toState));
			this.JumpstationMacroState = toState;
			this.Save(SecurityManager.CurrentUserIdentityName);
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Configuration Service Group state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
		}

		#endregion

		#region GoToState... Methods

		/// <summary>
		/// Transitions the instance to a specified state.
		/// </summary>
		/// <param name="toState">The state to transition the instance to.</param>
		public void GoToState(JumpstationMacroStateId toState)
		{
			//TODO: Refactor: Refactor this implementation to a standard BaseLifecycle implementation
#warning Refactor: Refactor this implementation to a standard BaseLifecycle implementation

			//JumpstationMacroLifecycle.Current.GoToState(this, toState);
			GoToState(this, toState, false);
		}

		private static void GoToState(JumpstationMacro instance, JumpstationMacroStateId toState, bool isAutotransition)
		{
			switch (toState)
			{
				case JumpstationMacroStateId.Modified:
					instance.SubmitBackToEditor();
					break;
				case JumpstationMacroStateId.Validated:
					instance.Validate();
					break;
				case JumpstationMacroStateId.Published:
					instance.Publish();
					break;
				case JumpstationMacroStateId.Cancelled:
					instance.UnPublish();
					break;
				case JumpstationMacroStateId.Deleted:
					instance.Delete();
					break;
				case JumpstationMacroStateId.Abandoned:
				case JumpstationMacroStateId.ReadyForValidation:
					instance.PerformSimpleStateTransition(toState);
					break;
				case JumpstationMacroStateId.None:
				case JumpstationMacroStateId.PublishedProductionOnly:
					throw new InvalidOperationException(string.Format("Explicit transition to state {0:G} is not supported.", toState));
				default:
					throw new ArgumentOutOfRangeException("toState");
			}
		}

		#endregion

		/// <summary>
		/// Changes the JumpstationMacro's <see cref="JumpstationMacroState"/> to <see cref="JumpstationMacroStateId.Abandoned"/>.
		/// </summary>
		public void Abandon()
		{
			this.GoToState(JumpstationMacroStateId.Abandoned); //this.PerformSimpleStateTransition(JumpstationMacroStateId.Abandoned);
		}

		/// <summary>
		/// Delete the JumpstationMacro
		/// </summary>
		public void Delete()
		{
			// can only delete JumpstationMacros that are abandoned.
			if (this.JumpstationMacroState == JumpstationMacroStateId.Abandoned)
			{
				try
				{
                    this.ClearJumpstationMacroValue();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture,
												   "Unable to delete JumpstationMacroId #{0}.",
												   this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
			}
		}

		/// <summary>
		/// Deletes a specified JumpstationMacro record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = JumpstationMacro.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			JumpstationMacroController.DestroyByQuery(query);
		}

		/// <summary>
		/// Changes the JumpstationMacro's <see cref="JumpstationMacroState"/> to <see cref="JumpstationMacroStateId.Published"/>.
		/// </summary>
		public void Publish()
		{
			JumpstationMacroStateId toState = JumpstationMacroStateId.Published;
			int? ProductionId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				JumpstationMacroStateId fromState = this.JumpstationMacroState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationMacro state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing configuration service group on the Elements server
				if (this.ProductionId != null)
				{
					JumpstationMacroQuerySpecification JumpstationMacroQuery = new JumpstationMacroQuerySpecification() { ProductionId = this.ProductionId };
					JumpstationMacroCollection JumpstationMacroColl = JumpstationMacroController.Fetch(JumpstationMacroQuery);
					foreach (JumpstationMacro JumpstationMacro in JumpstationMacroColl)
					{
						// skip existing one
						if (JumpstationMacro.Id != this.Id)
						{
							JumpstationMacro.JumpstationMacroState = JumpstationMacroStateId.Abandoned;
							JumpstationMacro.ValidationId = null;
							JumpstationMacro.ProductionId = null;
							JumpstationMacro.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//  Publish to Elements/CMService here!
				this.ProductionId = ElementsPublisher.Instance.PublishJumpstationMacro(this, HP.ElementsCPS.Data.CmService.Environment.Publication);
				this.JumpstationMacroState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationMacro state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} was successfully published to Elements as Elements JumpstationMacro #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ProductionId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the JumpstationMacro's <see cref="JumpstationMacroState"/> to <see cref="JumpstationMacroStateId.Cancelled"/>.
		/// </summary>
		public void UnPublish()
		{
			const JumpstationMacroStateId toState = JumpstationMacroStateId.Cancelled;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			JumpstationMacroStateId fromState = this.JumpstationMacroState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationMacro state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				// GADSC unpublish
				ElementsPublisher.Instance.UnPublishJumpstationMacro(this, HP.ElementsCPS.Data.CmService.Environment.Publication);

				this.ProductionId = null;
				this.JumpstationMacroState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationMacro state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} was not un-published from Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} was successfully un-published from Elements, but the system was unable to complete the UnPublish operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the JumpstationMacro's <see cref="JumpstationMacroState"/> to <see cref="JumpstationMacroStateId.Modified"/>.
		/// </summary>
		public void SubmitBackToEditor()
		{
			const JumpstationMacroStateId toState = JumpstationMacroStateId.Modified;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			JumpstationMacroStateId fromState = this.JumpstationMacroState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationMacro state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				if ((fromState == JumpstationMacroStateId.Validated) || (fromState == JumpstationMacroStateId.Cancelled))
				{
					// Unpublish from validation server here.
					ElementsPublisher.Instance.UnPublishJumpstationMacro(this, HP.ElementsCPS.Data.CmService.Environment.Validation);

					// if this is a replacement, then re-validate original configuration service group
					if ((this.ValidationId != null) && (this.ProductionId != null))
					{
						JumpstationMacroQuerySpecification JumpstationMacroQuery = new JumpstationMacroQuerySpecification()
																	{
																		ValidationId = this.ValidationId,
																		ProductionId = this.ProductionId,
                                                                        JumpstationMacroStatusId = (int?)JumpstationMacroStateId.PublishedProductionOnly
																	};
						JumpstationMacroCollection JumpstationMacroColl = JumpstationMacroController.Fetch(JumpstationMacroQuery);
						foreach (JumpstationMacro JumpstationMacro in JumpstationMacroColl)
						{
							//
							//  GADSC -> You will re-validate to Elements/CMService here!
							//
                            ElementsPublisher.Instance.PublishJumpstationMacro(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
							JumpstationMacro.JumpstationMacroStatusId = (int)JumpstationMacroStateId.Published;
							JumpstationMacro.Save(SecurityManager.CurrentUserIdentityName);
						}
						// replacement so remove production id too
						this.ProductionId = null;
					}
					this.ValidationId = null;
				}
				this.JumpstationMacroState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationMacro state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} was not un-published from the Validation Server.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} was successfully un-published from the Validation Server, but the system was unable to complete the operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the JumpstationMacro's <see cref="JumpstationMacroState"/> to <see cref="JumpstationMacroStateId.ReadyForValidation"/>.
		/// </summary>
		public void SubmitToValidator()
		{
			this.GoToState(JumpstationMacroStateId.ReadyForValidation); //this.PerformSimpleStateTransition(JumpstationMacroStateId.ReadyForValidation);
		}

		/// <summary>
		/// Changes the JumpstationMacro's <see cref="JumpstationMacroState"/> to <see cref="JumpstationMacroStateId.Validated"/>.
		/// </summary>
		public void Validate()
		{
			JumpstationMacroStateId toState = JumpstationMacroStateId.Validated;
			int? ValidationId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				JumpstationMacroStateId fromState = this.JumpstationMacroState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationMacro state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing configuration service group on validation server
				if (this.ValidationId != null)
				{
					JumpstationMacroQuerySpecification JumpstationMacroQuery = new JumpstationMacroQuerySpecification() { ValidationId = this.ValidationId };
					JumpstationMacroCollection JumpstationMacroColl = JumpstationMacroController.Fetch(JumpstationMacroQuery);
					foreach (JumpstationMacro JumpstationMacro in JumpstationMacroColl)
					{
						// skip existing one
						if (JumpstationMacro.Id != this.Id)
						{
							switch (JumpstationMacro.JumpstationMacroStatusId)
							{
								case (int)JumpstationMacroStateId.Published:
									JumpstationMacro.JumpstationMacroStatusId = (int)JumpstationMacroStateId.PublishedProductionOnly;
									break;
								case (int)JumpstationMacroStateId.Validated:
									JumpstationMacro.JumpstationMacroStatusId = (int)JumpstationMacroStateId.Modified;
									break;
								default:
									break;
							}
							JumpstationMacro.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//
				//  GADSC -> You will publish to Elements/CMService validation here!
				//
				//  Note - If ValidationId exists, then publish using the same ValidationId (i.e. replace the existing one).
				//
				this.ValidationId = ElementsPublisher.Instance.PublishJumpstationMacro(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
				this.JumpstationMacroState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationMacro state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationMacro #{0} was successfully published to Elements as Elements JumpstationMacro #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ValidationId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		#endregion

		#endregion

        #region JumpstationMacroSelector-related convenience members

        ///// <summary>
        ///// Removes all associated configuration service Group selectors
        ///// </summary>
        //public void ClearJumpstationMacroSelector()
        //{
        //    JumpstationMacroSelector.DestroyByJumpstationMacroId(this.Id);
        //}

        #endregion

        #region JumpstationLabelValue-related convenience members

        ///// <summary>
        ///// Removes all associated configuration service Group selector label values
        ///// </summary>
        //public void ClearJumpstationLabelValue()
        //{
        //    JumpstationLabelValue.DestroyByJumpstationMacroId(this.Id);
        //}

        #endregion

        #region JumpstationMacroValue-related convenience members

        /// <summary>
        /// Removes all associated jumpstation macro values
        /// </summary>
        public void ClearJumpstationMacroValue()
        {
            JumpstationMacroValue.DestroyByJumpstationMacroId(this.Id);
        }

        #endregion

	}
}
