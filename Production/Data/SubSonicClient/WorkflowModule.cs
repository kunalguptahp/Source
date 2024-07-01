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
	/// The non-generated portion of the WorkflowModule class.
	/// </summary>
	public partial class WorkflowModule : IStatefulEntity<WorkflowStateId>
	{

		#region Implementation of IStatefulEntity<WorkflowStateId>

		/// <summary>
		/// The entity's current lifecycle state within the lifecycle defined for the entity.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="WorkflowStateId"/> value is passed to the property.
		/// </exception>
		public WorkflowStateId CurrentState
		{
			get
			{
				return (WorkflowStateId)this.WorkflowModuleStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "CurrentState", typeof(WorkflowStateId));
				this.WorkflowModuleStatusId = (int)value;
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
		/// Strongly-typed convenience wrapper for the WorkflowModuleStatusId property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="WorkflowModuleState"/> value is passed to the property.
		/// </exception>
		public WorkflowStateId WorkflowModuleState
		{
			get
			{
				return (WorkflowStateId)this.WorkflowModuleStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "Status", typeof(WorkflowStateId));
				this.WorkflowModuleStatusId = (int)value;
			}
		}

		/// <summary>
		/// Returns a list of the names of all the Tags associated with this WorkflowModule.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and WorkflowModuleTag data is used to create the list of Tag names.
		/// </remarks>
		public List<string> TagNames
		{
			get
			{
				return WorkflowModuleController.GetTagNameList(this.Id, RowStatus.RowStatusId.Active);
			}

		}

		/// <summary>
		/// Returns a list of the IDs of all the Tags associated with this WorkflowModule.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and WorkflowModuleTag data is used to create the list of Tag names.
		/// </remarks>
		public List<int> TagIds
		{
			get
			{
				return VwMapWorkflowModuleTagTagController.CreateTagIdList(VwMapWorkflowModuleTagTagController.GetActiveTagsByWorkflowModuleId(this.Id));
			}
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? WorkflowModuleId = (this.IsNew ? null : (int?)this.Id);
			Log(WorkflowModuleId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? WorkflowModuleId = (this.IsNew ? null : (int?)this.Id);
			Log(WorkflowModuleId, severity, this, message, ex);
		}

		internal static void Log(int? WorkflowModuleId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "WorkflowModule History: #{0}: {1}.", WorkflowModuleId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? WorkflowModuleId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "WorkflowModule History: #{0}: {1}.", WorkflowModuleId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Creates a new <see cref="WorkflowModule"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="WorkflowModule"/>.</returns>
		public WorkflowModule SaveAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Creates a new <see cref="WorkflowModule"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="WorkflowModule"/>.</returns>
		public WorkflowModule SaveAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, copyElementsId);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public WorkflowModule SaveAsNew()
		{
			return SaveAsNew(this, false, false);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="WorkflowModule"/>.
		/// </summary>
		/// <param name="originalWorkflowModule">The <see cref="WorkflowModule"/> to copy/duplicate.</param>
		/// <param name="assignCurrentUserAsOwner">If <c>true</c>, then the new WorkflowModule will be (re)assigned to the current user; else it will be assigned to the original WorkflowModule's owner.</param>
		/// <param name="copyElementsId">If <c>true</c>, then new WorkflowModule will maintain the existing ElementsIds (ValidationId and ProductionId)</param>
		private static WorkflowModule SaveAsNew(WorkflowModule originalWorkflowModule, bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			WorkflowModule newWorkflowModule;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newWorkflowModule = WorkflowModule.Copy(originalWorkflowModule);

				//mark the newWorkflowModule instance as an unsaved instance
				newWorkflowModule.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newWorkflowModule.CreatedBy = createdBy;
				newWorkflowModule.CreatedOn = createdOn;
				newWorkflowModule.ModifiedBy = createdBy;
				newWorkflowModule.ModifiedOn = createdOn;

				//modify the status fields' values appropriately
				newWorkflowModule.WorkflowModuleState = WorkflowStateId.Modified;

				if (!copyElementsId)
				{
					newWorkflowModule.ValidationId = null;
					newWorkflowModule.ProductionId = null;
				}

				newWorkflowModule.RowStatusId = (int)RowStatus.RowStatusId.Active;

				if (assignCurrentUserAsOwner)
				{
					//assign the current user as the Owner of the newWorkflowModule
					newWorkflowModule.OwnerId = PersonController.GetCurrentUser().Id;
				}

				//save the newWorkflowModule to the DB so that it is assigned an ID
				newWorkflowModule.Save(SecurityManager.CurrentUserIdentityName);

				//copy all of the originalWorkflowModule's Tags to the newWorkflowModule
				newWorkflowModule.SetTags(originalWorkflowModule.TagIds);

                //save as new workflow condition
                WorkflowModuleWorkflowConditionCollection workflowModuleWorkflowConditionCollection = new WorkflowModuleWorkflowConditionCollection();
                foreach (WorkflowModuleWorkflowCondition originalRecord in originalWorkflowModule.WorkflowModuleWorkflowConditionRecords())
                {
                    WorkflowModuleWorkflowCondition newRecord = WorkflowModuleWorkflowCondition.Copy(originalRecord);
                    newRecord.MarkNew();
                    newRecord.WorkflowModuleId = newWorkflowModule.Id;
                    newRecord.CreatedBy = createdBy;
                    newRecord.CreatedOn = createdOn;
                    newRecord.ModifiedBy = createdBy;
                    newRecord.ModifiedOn = createdOn;
                    workflowModuleWorkflowConditionCollection.Add(newRecord);
                }
                workflowModuleWorkflowConditionCollection.SaveAll(SecurityManager.CurrentUserIdentityName);

				originalWorkflowModule.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newWorkflowModule));
				newWorkflowModule.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalWorkflowModule));

				scope.Complete(); // transaction complete
			}

			return newWorkflowModule;
		}

		#endregion

		#region Tag-related Methods

		/// <summary>
		/// Disassociates this WorkflowModule from a specific Tag.
		/// </summary>
		/// <param name="tagId"></param>
		public void RemoveTag(int tagId)
		{
			WorkflowModuleTag.Destroy(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="WorkflowModule"/> with a specific <see cref="Tag"/>.
		/// </summary>
		/// <param name="tagId">The ID of an existing <see cref="Tag"/>.</param>
		public WorkflowModuleTag AddTag(int tagId)
		{
			return WorkflowModuleTag.Insert(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="WorkflowModule"/> with a specific <see cref="Tag"/> (creating a new <see cref="Tag"/> if needed).
		/// </summary>
		/// <param name="tagName">The Name of a <see cref="Tag"/> (either existing or not).</param>
		public WorkflowModuleTag AddTag(string tagName)
		{
			if (!Tag.IsValidName(tagName))
			{
				throw new ArgumentException("Invalid Tag name.", "tagName");
			}

			Tag tag = TagController.GetOrCreateByName(tagName);
			return this.AddTag(tag.Id);
		}

		/// <summary>
		/// Deletes/removes all <see cref="Tag"/>s associated with this <see cref="WorkflowModule"/>.
		/// </summary>
		public void ClearTags()
		{
			WorkflowModuleTag.DestroyByWorkflowModuleId(this.Id);
			this.Log(Severity.Debug, "WorkflowModule tag removed: All tags were deleted.");
		}

		/// <summary>
		/// Replaces all WorkflowModuleTags currently associated with this <see cref="WorkflowModule"/> with the specified set of new <see cref="Tag"/>s.
		/// </summary>
		/// <param name="tagIds">The IDs of the new set of <see cref="Tag"/>s.</param>
		public void SetTags(IEnumerable<int> tagIds)
		{
			//determine which tags need to be removed and which ones need to be added
			List<int> oldTags = this.TagIds;
			IEnumerable<int> enumRemovedTags = oldTags.Except(tagIds);
			IEnumerable<int> enumAddedTags = tagIds.Except(oldTags);

			//delete the removed tags
			foreach (int tagId in enumRemovedTags)
			{
				this.RemoveTag(tagId);
			}

			//insert the added tags
			foreach (int tagId in enumAddedTags)
			{
				this.AddTag(tagId);
			}
		}

		/// <summary>
		/// Replaces all WorkflowModuleTags currently associated with this <see cref="WorkflowModule"/> with the specified set of new <see cref="Tag"/>s.
		/// </summary>
		/// <param name="tags">The Names of the new set of <see cref="Tag"/>s.</param>
		public void SetTags(IEnumerable<string> tags)
		{
			tags = WorkflowModule.PreprocessTags(tags);

			if (!Tag.AreValidNames(tags))
			{
				throw new ArgumentException("Invalid Tag name(s).", "tags");
			}

			//determine which tags need to be removed and which ones need to be added
			List<string> oldTags = this.TagNames;
			this.RemoveTagsHelper(oldTags.Except(tags, StringComparer.CurrentCultureIgnoreCase));
			this.AddTagsHelper(tags.Except(oldTags, StringComparer.CurrentCultureIgnoreCase));
		}

		#region Helper methods

		private static IEnumerable<string> PreprocessTags(IEnumerable<string> tags)
		{
			if ((tags != null) && (tags.Count() > 0))
			{
				//trim all of the strings
				tags = tags.YieldTrim();

				//remove any duplicates (case insensitive)
				tags = tags.RemoveDuplicates(StringComparer.CurrentCultureIgnoreCase);
			}
			return tags;
		}

		private void RemoveTagsHelper(IEnumerable<string> tags)
		{
			foreach (string tagName in tags)
			{
				Tag tag = TagController.GetByName(tagName);
				if (tag != null)
				{
					this.RemoveTag(tag.Id);
					this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Workflow module tag removed: '{0}'.", tagName));
				}
			}
		}

		private void AddTagsHelper(IEnumerable<string> tags)
		{
			foreach (string tagName in tags)
			{
				this.AddTag(tagName);
				this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Workflow module tag added: '{0}'.", tagName));
			}
		}

		#endregion

		/// <summary>
		/// Adds additional <see cref="WorkflowModuleTag"/>s to this <see cref="WorkflowModule"/> to associate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will not remove any existing <see cref="WorkflowModuleTag"/>s.
		/// This method will not create any duplicate <see cref="WorkflowModuleTag"/>s.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to add.</param>
		public void AddTags(IEnumerable<string> tags)
		{
			tags = WorkflowModule.PreprocessTags(tags);

			if (!Tag.AreValidNames(tags))
			{
				throw new ArgumentException("Invalid Tag name(s).", "tags");
			}

			//determine which tags need to be added
			List<string> oldTags = this.TagNames;
			this.AddTagsHelper(tags.Except(oldTags, StringComparer.CurrentCultureIgnoreCase));
		}

		/// <summary>
		/// Removes existing <see cref="WorkflowModuleTag"/>s from this <see cref="WorkflowModule"/> to disassociate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will only remove existing <see cref="WorkflowModuleTag"/>s.
		/// This method will ignore any items in <paramref name="tags"/> that are not currently associated with this <see cref="WorkflowModule"/>.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to remove.</param>
		public void RemoveTags(IEnumerable<string> tags)
		{
			tags = WorkflowModule.PreprocessTags(tags);

			//determine which tags need to be removed
			List<string> oldTags = this.TagNames;
			this.RemoveTagsHelper(tags.Intersect(oldTags, StringComparer.CurrentCultureIgnoreCase));
		}

		#endregion

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of an WorkflowModule.
		/// </summary>
		/// <param name="WorkflowModule">The WorkflowModule to format.</param>
		/// <returns></returns>
		private static string Format(WorkflowModule WorkflowModule)
		{
			return string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} ({1:G})", WorkflowModule.Id, WorkflowModule.WorkflowModuleState);
		}

		#endregion

		#region WorkflowModule Lifecycle Methods

		#region Lifecycle-related Interrogative Methods

		#region IsDataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Workflow Module with a specific <see cref="WorkflowModuleState"/>.
		/// </summary>
		/// <param name="state">The state of the Workflow Module that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsDataModificationAllowed(WorkflowStateId state, ICollection<UserRoleId> requestorRoles)
		{
			switch (state)
			{
				case WorkflowStateId.ReadyForValidation:
				case WorkflowStateId.Validated:
				case WorkflowStateId.Published:
				case WorkflowStateId.Cancelled:
					//Nobody can edit a Ready For Modification, Validated, Published or Cancelled Workflow Module
					return false;
			}

			//A user is allowed to "edit" an Workflow Module in this state if the user would be allowed to Abandon such an Workflow Module
			return IsStateTransitionAllowed(state, WorkflowStateId.Abandoned, requestorRoles);
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify the Workfow Module.
		/// </summary>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> requestorRoles)
		{
			return IsDataModificationAllowed(this.WorkflowModuleState, requestorRoles);
		}

		#endregion

		#region IsMetadataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Workflow Module with a specific <see cref="WorkflowState"/>.
		/// </summary>
		/// <param name="state">The state of the Workflow Module that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsMetadataModificationAllowed(WorkflowStateId state, ICollection<UserRoleId> requestorRoles)
		{
			//NOTE: A specific user is allowed to "edit" an WorkflowModule's "metadata" (e.g. tags) if the user is an Editor
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
			return IsMetadataModificationAllowed(this.WorkflowModuleState, requestorRoles);
		}

		#endregion

		#region IsStateTransitionAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition between two specified <see cref="WorkflowModuleState"/>s.
		/// </summary>
		/// <param name="fromState">The initial/starting state that the ProxyURL would transition from.</param>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if the specified transition is allowed.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if either <paramref name="fromState"/> or <paramref name="toState"/> is invalid.
		/// </exception>
		public static bool IsStateTransitionAllowed(WorkflowStateId fromState, WorkflowStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(fromState, "fromState", typeof(WorkflowStateId));
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(WorkflowStateId));

			//a transition must be between 2 distinct states
			if (toState == WorkflowStateId.None)
			{
				return false;
				//throw new ArgumentException("toState cannot be None", "toState");
			}

			//a transition must be between 2 distinct states
			if ((fromState == toState) && (fromState != WorkflowStateId.Published))
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
		/// Determines whether a user with a specified set of roles is authorized to execute a transition to a specified <see cref="WorkflowModuleState"/> 
		/// from the Workflow Module's current <see cref="WorkflowModuleState"/>.
		/// </summary>
		/// <param name="toState">The new state that the Workflow Module would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(WorkflowStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			if (this.IsDirty)
			{
				return false;
			}

			if (!IsStateTransitionAllowed(this.WorkflowModuleState, toState, requestorRoles))
			{
				return false;
			}

			switch (toState)
			{
				case WorkflowStateId.ReadyForValidation:
				case WorkflowStateId.Validated:
				case WorkflowStateId.Published:
					//moving to any of these states requires that the Workflow Module be "publishable"
					if (!this.IsDataPublishable())
					{
						return false;
					}
					break;
				case WorkflowStateId.Cancelled:
                    // do not allow unpublish if module is attached to workflow.
                    if (!this.IsDataUnPublishable() || this.HasValidatedWorkflows())
					{
						return false;
					}
					break;
                case WorkflowStateId.Deleted:
                case WorkflowStateId.Abandoned:
                    // do not allow delete or Abandon if module is attached to workflow.
                    if (this.HasWorkflows())
                    {
                        return false;
                    }
                    break;
                default:
					//case WorkflowStateId.None:
					//case WorkflowStateId.Modified:
					break;
			}

			return true;
		}

		#region IsStateTransitionAllowed Helper Methods

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_StatesPaths(WorkflowStateId fromState, WorkflowStateId toState)
		{
			//verify that the current state can transition to the requested state
			switch (toState)
			{
				case SubSonicClient.WorkflowStateId.Cancelled:
					if (fromState != WorkflowStateId.Published)
					{
						return false;
					}
					break;
				case SubSonicClient.WorkflowStateId.Validated:
					if (fromState != WorkflowStateId.ReadyForValidation)
					{
						return false;
					}
					break;
				case SubSonicClient.WorkflowStateId.Published:
					if (!((fromState == WorkflowStateId.Validated) || (fromState == WorkflowStateId.Published) || (fromState == WorkflowStateId.Cancelled)))
					{
						return false;
					}
					break;
				case SubSonicClient.WorkflowStateId.Modified:
					List<WorkflowStateId> possibleFromStates_Modified = new List<WorkflowStateId>(
						new[] { WorkflowStateId.Abandoned, WorkflowStateId.ReadyForValidation, WorkflowStateId.Validated, WorkflowStateId.Cancelled });
					if (!possibleFromStates_Modified.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.WorkflowStateId.ReadyForValidation:
					List<WorkflowStateId> possibleFromStates_ReadyForValidation = new List<WorkflowStateId>(
						new[] { WorkflowStateId.Abandoned, WorkflowStateId.Modified });
					if (!possibleFromStates_ReadyForValidation.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.WorkflowStateId.Abandoned:
					List<WorkflowStateId> possibleFromStates_Abandoned = new List<WorkflowStateId>(
						new[] { WorkflowStateId.Modified, WorkflowStateId.ReadyForValidation });
					if (!possibleFromStates_Abandoned.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.WorkflowStateId.Deleted:
					if (fromState != WorkflowStateId.Abandoned)
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
		private static bool IsStateTransitionAllowedHelper_SpecialStates(WorkflowStateId fromState, WorkflowStateId toState)
		{
			//the None state is a virtual start state, and may only proceed to the Modified state
			if (fromState == WorkflowStateId.None)
			{
				return (toState == WorkflowStateId.Modified);
			}
			return true;
		}

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_RoleBasedRules(WorkflowStateId fromState, WorkflowStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			//First, enforce the "simple" transition rules requiring knowledge of only roles and fromState
			switch (fromState)
			{
				case WorkflowStateId.Validated:
				case WorkflowStateId.Published:
				case WorkflowStateId.Cancelled:
					//All transitions from Validated or Published (to any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case WorkflowStateId.ReadyForValidation:
					//All transitions from ReadyForValidation (to any other state) require the Validator role
					if (!requestorRoles.Contains(UserRoleId.Validator))
					{
						return false;
					}
					break;
				case WorkflowStateId.Modified:
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
				case WorkflowStateId.Cancelled:
				case WorkflowStateId.Published:
					//All transitions to Cancelled or Published (from any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case WorkflowStateId.Validated:
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
		/// Determines whether a user with a specified set of roles would be authorized to execute a transition to a specified <see cref="WorkflowState"/> 
		/// (from any other possible state).
		/// </summary>
		/// <param name="toState">The new state that the WorkflowModule would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if any potential transition is possible.</returns>
		public static bool IsStateTransitionPossible(WorkflowStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			Array allStates = Enum.GetValues(typeof(WorkflowStateId));
			foreach (WorkflowStateId fromState in allStates)
			{
				if (IsStateTransitionAllowed(fromState, toState, requestorRoles))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Indicates whether the WorkflowModule's data is complete and valid (i.e. could be published "as is").
		/// </summary>
		/// <returns></returns>
		public bool IsDataPublishable()
		{
			switch (this.WorkflowModuleState)
			{
				case WorkflowStateId.Published:
					//Published WorkflowModules can only be re-Published if they could also be UnPublished
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
		/// Indicates whether the WorkflowModule's data is being replaced (Published WorkflowModule has already been copied)
		/// </summary>
		/// <returns></returns>
		public bool IsOriginalWorkflowModuleReplacement()
		{
			if (this.ValidationId == null && this.ProductionId == null)
			{
				// not published so allow.
				return false;
			}
			else
			{
				if ((WorkflowStateId)this.WorkflowModuleStatusId != WorkflowStateId.Published)
				{
					// not original.
					return false;
				}
				else
				{
					// check to see if replacement
					WorkflowModuleQuerySpecification WorkflowModuleQuerySpecification = new WorkflowModuleQuerySpecification() { ValidationId = this.ValidationId };
					return WorkflowModuleController.FetchCount(WorkflowModuleQuerySpecification) > 1;
				}
			}
		}

		/// <summary>
		/// Indicates whether the WorkflowModule's data is complete and valid enough to be UnPublished
		/// (i.e. could be unpublished "as is" ignoring the WorkflowModule's State).
		/// </summary>
		/// <returns></returns>
		private bool IsDataUnPublishable()
		{
			return (this.ProductionId != null);
		}

        /// <summary>
        /// Indicates whether the WorkflowModule's is attached to any existing validated or ready to validate workflows.
        /// </summary>
        /// <returns></returns>
        private bool HasValidatedWorkflows()
        {
            foreach (VwMapWorkflowWorkflowModule workflowModule in VwMapWorkflowWorkflowModuleController.FetchByWorkflowModuleId(this.Id))
            {
                if ((workflowModule.WorkflowValidationId != null) || (workflowModule.WorkflowStatusId == (int)WorkflowStateId.ReadyForValidation))
                    return true;
            }            
            return false;
        }

        /// <summary>
        /// Indicates whether the WorkflowModule's is attached to any existing workflow.
        /// </summary>
        /// <returns></returns>
        private bool HasWorkflows()
        {
            return (VwMapWorkflowWorkflowModuleController.FetchCountByWorkflowModuleId(this.Id) > 0);
        }

        #endregion

		#region State Transition Methods

		#region State Transition Helper Methods

		/// <summary>
		/// Validates that the current user is allowed to transition the current workflow module to a specified <see cref="WorkflowModuleState"/>.
		/// </summary>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the state transition is not allowed or the current user is not authorized to initiate it.
		/// </exception>
		private void AuthorizeStateTransition(WorkflowStateId toState)
		{
			Person currentUser = PersonController.GetCurrentUser();
			List<UserRoleId> currentUserRoles = currentUser.GetRoles();
			if (!this.IsStateTransitionAllowed(toState, currentUserRoles))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid Workflow Module State transition (from {0:G} to {1:G}) attempted on {2} by {3}.", this.WorkflowModuleState, toState, this, currentUser);
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
		private void PerformSimpleStateTransition(WorkflowStateId toState)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(WorkflowStateId));

			//validate that the toState does not require a "complex transition"
			switch (toState)
			{
				case WorkflowStateId.None:
				case WorkflowStateId.Published:
				case WorkflowStateId.Cancelled:
					throw new InvalidOperationException(
						string.Format(CultureInfo.CurrentCulture,
							"The PerformSimpleStateTransition() method cannot be used to transition to state {0:G}", toState));
			}

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			WorkflowStateId fromState = this.WorkflowModuleState;
			this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Workflow Module state changing: from '{0:G}' to '{1:G}'.", fromState, toState));
			this.WorkflowModuleState = toState;
			this.Save(SecurityManager.CurrentUserIdentityName);
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow Module state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
		}

		#endregion

		#region GoToState... Methods

		/// <summary>
		/// Transitions the instance to a specified state.
		/// </summary>
		/// <param name="toState">The state to transition the instance to.</param>
		public void GoToState(WorkflowStateId toState)
		{
			//TODO: Refactor: Refactor this implementation to a standard BaseLifecycle implementation
#warning Refactor: Refactor this implementation to a standard BaseLifecycle implementation

			//WorkflowModuleLifecycle.Current.GoToState(this, toState);
			GoToState(this, toState, false);
		}

		private static void GoToState(WorkflowModule instance, WorkflowStateId toState, bool isAutotransition)
		{
			switch (toState)
			{
				case WorkflowStateId.Modified:
					instance.SubmitBackToEditor();
					break;
				case WorkflowStateId.Validated:
					instance.Validate();
					break;
				case WorkflowStateId.Published:
					instance.Publish();
					break;
				case WorkflowStateId.Cancelled:
					instance.UnPublish();
					break;
				case WorkflowStateId.Deleted:
					instance.Delete();
					break;
				case WorkflowStateId.Abandoned:
				case WorkflowStateId.ReadyForValidation:
					instance.PerformSimpleStateTransition(toState);
					break;
				case WorkflowStateId.None:
				case WorkflowStateId.PublishedProductionOnly:
					throw new InvalidOperationException(string.Format("Explicit transition to state {0:G} is not supported.", toState));
				default:
					throw new ArgumentOutOfRangeException("toState");
			}
		}

		#endregion

		/// <summary>
		/// Changes the WorkflowModule's <see cref="WorkflowModuleState"/> to <see cref="WorkflowStateId.Abandoned"/>.
		/// </summary>
		public void Abandon()
		{
			this.GoToState(WorkflowStateId.Abandoned); //this.PerformSimpleStateTransition(WorkflowStateId.Abandoned);
		}

		/// <summary>
		/// Delete the WorkflowModule
		/// </summary>
		public void Delete()
		{
			// can only delete WorkflowModules that are abandoned.
			if (this.WorkflowModuleState == WorkflowStateId.Abandoned)
			{
				try
				{
					this.ClearTags();
                    this.ClearWorkflowModuleWorkflowCondition();
                    this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture, "Unable to delete WorkflowModuleId #{0}.", this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
			}
		}

		/// <summary>
		/// Deletes a specified WorkflowModule record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = WorkflowModule.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			WorkflowModuleController.DestroyByQuery(query);
		}

		/// <summary>
		/// Changes the WorkflowModule's <see cref="WorkflowModuleState"/> to <see cref="WorkflowStateId.Published"/>.
		/// </summary>
		public void Publish()
		{
			WorkflowStateId toState = WorkflowStateId.Published;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				WorkflowStateId fromState = this.WorkflowModuleState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "WorkflowModule state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing workflow module on the Elements server
				if (this.ProductionId != null)
				{
                    WorkflowModuleQuerySpecification workflowModuleQuery = new WorkflowModuleQuerySpecification() { ProductionId = this.ProductionId };
					WorkflowModuleCollection workflowModuleColl = WorkflowModuleController.Fetch(workflowModuleQuery);
					foreach (WorkflowModule workflowModule in workflowModuleColl)
					{
						// skip existing one
						if (workflowModule.Id != this.Id)
						{
							workflowModule.WorkflowModuleState = WorkflowStateId.Abandoned;
							workflowModule.ValidationId = null;
							workflowModule.ProductionId = null;
							workflowModule.Save(SecurityManager.CurrentUserIdentityName);

                            // Update Workflow_WorkflowModule mapping (replace new module for each workflow).
                            WorkflowWorkflowModuleQuerySpecification workflowWorkflowModuleQuery = new WorkflowWorkflowModuleQuerySpecification() { WorkflowModuleId = workflowModule.Id, Paging = { PageSize = int.MaxValue } };
                            WorkflowWorkflowModuleCollection workflowWorkflowModuleColl = WorkflowWorkflowModuleController.Fetch(workflowWorkflowModuleQuery);
                            foreach (WorkflowWorkflowModule workflowWorkflowModule in workflowWorkflowModuleColl)
                            {
                                workflowWorkflowModule.WorkflowModuleId = this.Id;
                                workflowWorkflowModule.Save(SecurityManager.CurrentUserIdentityName);
                            }
						}
					}
				}

				//  Publish to Elements/DPS here!
			    this.ProductionId = ElementsPublisher.Instance.PublishWorkflowModule(this, HP.ElementsCPS.Data.CmService.Environment.Publication);
				this.WorkflowModuleState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "WorkflowModule state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} was successfully published to Elements as Elements WorkflowModule #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ProductionId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the WorkflowModule's <see cref="WorkflowModuleState"/> to <see cref="WorkflowStateId.Cancelled"/>.
		/// </summary>
		public void UnPublish()
		{
			const WorkflowStateId toState = WorkflowStateId.Cancelled;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			WorkflowStateId fromState = this.WorkflowModuleState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "WorkflowModule state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				// GADSC unpublish
				ElementsPublisher.Instance.UnPublishWorkflowModule(this, HP.ElementsCPS.Data.CmService.Environment.Publication);

				this.ProductionId = null;
				this.WorkflowModuleState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "WorkflowModule state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} was not un-published from Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} was successfully un-published from Elements, but the system was unable to complete the UnPublish operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the WorkflowModule's <see cref="WorkflowModuleState"/> to <see cref="WorkflowStateId.Modified"/>.
		/// </summary>
		public void SubmitBackToEditor()
		{
			const WorkflowStateId toState = WorkflowStateId.Modified;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			WorkflowStateId fromState = this.WorkflowModuleState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "WorkflowModule state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				if ((fromState == WorkflowStateId.Validated) || (fromState == WorkflowStateId.Cancelled))
				{
					// Unpublish from validation server here.
					ElementsPublisher.Instance.UnPublishWorkflowModule(this, HP.ElementsCPS.Data.CmService.Environment.Validation);

					// if this is a replacement, then re-validate original workflow module
					if ((this.ValidationId != null) && (this.ProductionId != null))
					{
						WorkflowModuleQuerySpecification workflowModuleQuery = new WorkflowModuleQuerySpecification()
						{
							ValidationId = this.ValidationId,
							ProductionId = this.ProductionId,
							WorkflowModuleStatusId = (int?)WorkflowStateId.PublishedProductionOnly
						};
						WorkflowModuleCollection workflowModuleColl = WorkflowModuleController.Fetch(workflowModuleQuery);
						foreach (WorkflowModule workflowModule in workflowModuleColl)
						{
							workflowModule.ValidationId = ElementsPublisher.Instance.PublishWorkflowModule(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
							workflowModule.WorkflowModuleStatusId = (int)WorkflowStateId.Published;
							workflowModule.Save(SecurityManager.CurrentUserIdentityName);
						}
						// replacement so remove production id too
						this.ProductionId = null;
					}
					this.ValidationId = null;
				}
				this.WorkflowModuleState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "WorkflowModule state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} was not un-published from the Validation Server.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} was successfully un-published from the Validation Server, but the system was unable to complete the operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the WorkflowModule's <see cref="WorkflowModuleState"/> to <see cref="WorkflowStateId.ReadyForValidation"/>.
		/// </summary>
		public void SubmitToValidator()
		{
			this.GoToState(WorkflowStateId.ReadyForValidation); //this.PerformSimpleStateTransition(WorkflowStateId.ReadyForValidation);
		}

		/// <summary>
		/// Changes the WorkflowModule's <see cref="WorkflowModuleState"/> to <see cref="WorkflowStateId.Validated"/>.
		/// </summary>
		public void Validate()
		{
			WorkflowStateId toState = WorkflowStateId.Validated;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				WorkflowStateId fromState = this.WorkflowModuleState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "WorkflowModule state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing workflow module on validation server
				if (this.ValidationId != null)
				{
					WorkflowModuleQuerySpecification workflowModuleQuery = new WorkflowModuleQuerySpecification() { ValidationId = this.ValidationId };
					WorkflowModuleCollection workflowModuleColl = WorkflowModuleController.Fetch(workflowModuleQuery);
					foreach (WorkflowModule workflowModule in workflowModuleColl)
					{
						// skip existing one
						if (workflowModule.Id != this.Id)
						{
							switch (workflowModule.WorkflowModuleStatusId)
							{
								case (int)WorkflowStateId.Published:
									workflowModule.WorkflowModuleStatusId = (int)WorkflowStateId.PublishedProductionOnly;
									break;
								case (int)WorkflowStateId.Validated:
									workflowModule.WorkflowModuleStatusId = (int)WorkflowStateId.Modified;
									break;
								default:
									break;
							}
							workflowModule.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}
			    this.ValidationId = ElementsPublisher.Instance.PublishWorkflowModule(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
				this.WorkflowModuleState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "WorkflowModule state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "WorkflowModule #{0} was successfully published to Elements as Elements WorkflowModule #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ValidationId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		#endregion

		#endregion

        #region WorkflowCondition-related convenience members

        /// <summary>
        /// Removes all associated WorkflowModuleCondtions
        /// </summary>
        public void ClearWorkflowModuleWorkflowCondition()
        {
            WorkflowModuleWorkflowCondition.DestroyByWorkflowModuleId(this.Id);
        }

        #endregion

	}
}
