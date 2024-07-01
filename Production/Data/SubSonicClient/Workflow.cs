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
	/// The non-generated portion of the Workflow class.
	/// </summary>
	public partial class Workflow : IStatefulEntity<WorkflowStateId>
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
				return (WorkflowStateId)this.WorkflowStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "CurrentState", typeof(WorkflowStateId));
				this.WorkflowStatusId = (int)value;
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
		/// Strongly-typed convenience wrapper for the WorkflowStatusId property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="WorkflowStateId"/> value is passed to the property.
		/// </exception>
		public WorkflowStateId WorkflowState
		{
			get
			{
				return (WorkflowStateId)this.WorkflowStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "Status", typeof(WorkflowStateId));
				this.WorkflowStatusId = (int)value;
			}
		}

		/// <summary>
		/// Returns a list of the names of all the Tags associated with this Workflow.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and WorkflowTag data is used to create the list of Tag names.
		/// </remarks>
		public List<string> TagNames
		{
			get
			{
				return WorkflowController.GetTagNameList(this.Id, RowStatus.RowStatusId.Active);
			}

		}

		/// <summary>
		/// Returns a list of the IDs of all the Tags associated with this Workflow.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and WorkflowTag data is used to create the list of Tag names.
		/// </remarks>
		public List<int> TagIds
		{
			get
			{
				return VwMapWorkflowTagTagController.CreateTagIdList(VwMapWorkflowTagTagController.GetActiveTagsByWorkflowId(this.Id));
			}
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? WorkflowId = (this.IsNew ? null : (int?)this.Id);
			Log(WorkflowId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? WorkflowId = (this.IsNew ? null : (int?)this.Id);
			Log(WorkflowId, severity, this, message, ex);
		}

		internal static void Log(int? WorkflowId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "Workflow History: #{0}: {1}.", WorkflowId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? WorkflowId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "Workflow History: #{0}: {1}.", WorkflowId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Creates a new <see cref="Workflow"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="Workflow"/>.</returns>
		public Workflow SaveAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Creates a new <see cref="Workflow"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="Workflow"/>.</returns>
		public Workflow SaveAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, copyElementsId);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public Workflow SaveAsNew()
		{
			return SaveAsNew(this, false, false);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="Workflow"/>.
		/// </summary>
		/// <param name="originalWorkflow">The <see cref="Workflow"/> to copy/duplicate.</param>
		/// <param name="assignCurrentUserAsOwner">If <c>true</c>, then the new Workflow will be (re)assigned to the current user; else it will be assigned to the original Workflow's owner.</param>
		/// <param name="copyElementsId">If <c>true</c>, then new Workflow will maintain the existing ElementsIds (ValidationId and ProductionId)</param>
		private static Workflow SaveAsNew(Workflow originalWorkflow, bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			Workflow newWorkflow;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newWorkflow = Workflow.Copy(originalWorkflow);

				//mark the newWorkflow instance as an unsaved instance
				newWorkflow.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newWorkflow.CreatedBy = createdBy;
				newWorkflow.CreatedOn = createdOn;
				newWorkflow.ModifiedBy = createdBy;
				newWorkflow.ModifiedOn = createdOn;

				//modify the status fields' values appropriately
				newWorkflow.WorkflowState = WorkflowStateId.Modified;

				if (!copyElementsId)
				{
					newWorkflow.ValidationId = null;
					newWorkflow.ProductionId = null;
				}

				newWorkflow.RowStatusId = (int)RowStatus.RowStatusId.Active;

				if (assignCurrentUserAsOwner)
				{
					//assign the current user as the Owner of the newWorkflow
					newWorkflow.OwnerId = PersonController.GetCurrentUser().Id;
				}

				//save the newWorkflow to the DB so that it is assigned an ID
				newWorkflow.Save(SecurityManager.CurrentUserIdentityName);

				//copy all of the originalWorkflow's Tags to the newWorkflow
				newWorkflow.SetTags(originalWorkflow.TagIds);

				//save as new workflow module
                WorkflowWorkflowModuleCollection workflowModuleCollection = new WorkflowWorkflowModuleCollection();
                foreach (WorkflowWorkflowModule originalRecord in originalWorkflow.WorkflowWorkflowModuleRecords())
                {
                    WorkflowWorkflowModule newRecord = WorkflowWorkflowModule.Copy(originalRecord);
                    newRecord.MarkNew();
                    newRecord.WorkflowId = newWorkflow.Id;
                    newRecord.CreatedBy = createdBy;
                    newRecord.CreatedOn = createdOn;
                    newRecord.ModifiedBy = createdBy;
                    newRecord.ModifiedOn = createdOn;
                    workflowModuleCollection.Add(newRecord);
                }
                workflowModuleCollection.SaveAll(SecurityManager.CurrentUserIdentityName);

				//save as new Configuration Service Group Selector
				WorkflowSelectorCollection colWorkflowSelector = new WorkflowSelectorCollection();
				foreach (WorkflowSelector gsOriginalRecord in originalWorkflow.WorkflowSelectorRecords())
				{
					WorkflowSelector gsNewRecord = WorkflowSelector.Copy(gsOriginalRecord);
					gsNewRecord.MarkNew();
					gsNewRecord.WorkflowId = newWorkflow.Id;
					gsNewRecord.CreatedBy = createdBy;
					gsNewRecord.CreatedOn = createdOn;
					gsNewRecord.ModifiedBy = createdBy;
					gsNewRecord.ModifiedOn = createdOn;
					gsNewRecord.Save(SecurityManager.CurrentUserIdentityName);

					//copy all of the originalWorkflowSelector's WorkflowSelectorQueryParameterValue data to the newWorkflowSelector
					WorkflowSelectorQueryParameterValueCollection colWorkflowSelectorQueryParameterValue = new WorkflowSelectorQueryParameterValueCollection();
					foreach (WorkflowSelectorQueryParameterValue qpvOriginalRecord in gsOriginalRecord.WorkflowSelectorQueryParameterValueRecords())
					{
						WorkflowSelectorQueryParameterValue qpvNewRecord = WorkflowSelectorQueryParameterValue.Copy(qpvOriginalRecord);
						qpvNewRecord.MarkNew();
						qpvNewRecord.WorkflowSelectorId = gsNewRecord.Id;
						qpvNewRecord.CreatedBy = createdBy;
						qpvNewRecord.CreatedOn = createdOn;
						qpvNewRecord.ModifiedBy = createdBy;
						qpvNewRecord.ModifiedOn = createdOn;
						colWorkflowSelectorQueryParameterValue.Add(qpvNewRecord);
					}
					colWorkflowSelectorQueryParameterValue.SaveAll(SecurityManager.CurrentUserIdentityName);
				}

				originalWorkflow.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newWorkflow));
				newWorkflow.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalWorkflow));

				scope.Complete(); // transaction complete
			}

			return newWorkflow;
		}

		#endregion

		#region Tag-related Methods

		/// <summary>
		/// Disassociates this Workflow from a specific Tag.
		/// </summary>
		/// <param name="tagId"></param>
		public void RemoveTag(int tagId)
		{
			WorkflowTag.Destroy(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="Workflow"/> with a specific <see cref="Tag"/>.
		/// </summary>
		/// <param name="tagId">The ID of an existing <see cref="Tag"/>.</param>
		public WorkflowTag AddTag(int tagId)
		{
			return WorkflowTag.Insert(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="Workflow"/> with a specific <see cref="Tag"/> (creating a new <see cref="Tag"/> if needed).
		/// </summary>
		/// <param name="tagName">The Name of a <see cref="Tag"/> (either existing or not).</param>
		public WorkflowTag AddTag(string tagName)
		{
			if (!Tag.IsValidName(tagName))
			{
				throw new ArgumentException("Invalid Tag name.", "tagName");
			}

			Tag tag = TagController.GetOrCreateByName(tagName);
			return this.AddTag(tag.Id);
		}

		/// <summary>
		/// Deletes/removes all <see cref="Tag"/>s associated with this <see cref="Workflow"/>.
		/// </summary>
		public void ClearTags()
		{
			WorkflowTag.DestroyByWorkflowId(this.Id);
			this.Log(Severity.Debug, "Workflow tag removed: All tags were deleted.");
		}

		/// <summary>
		/// Replaces all WorkflowTags currently associated with this <see cref="Workflow"/> with the specified set of new <see cref="Tag"/>s.
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
		/// Replaces all WorkflowTags currently associated with this <see cref="Workflow"/> with the specified set of new <see cref="Tag"/>s.
		/// </summary>
		/// <param name="tags">The Names of the new set of <see cref="Tag"/>s.</param>
		public void SetTags(IEnumerable<string> tags)
		{
			tags = Workflow.PreprocessTags(tags);

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
					this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Configuration Service group tag removed: '{0}'.", tagName));
				}
			}
		}

		private void AddTagsHelper(IEnumerable<string> tags)
		{
			foreach (string tagName in tags)
			{
				this.AddTag(tagName);
				//this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Configuration Service group tag added: '{0}'.", tagName));
			}
		}

		#endregion

		/// <summary>
		/// Adds additional <see cref="WorkflowTag"/>s to this <see cref="Workflow"/> to associate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will not remove any existing <see cref="WorkflowTag"/>s.
		/// This method will not create any duplicate <see cref="WorkflowTag"/>s.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to add.</param>
		public void AddTags(IEnumerable<string> tags)
		{
			tags = Workflow.PreprocessTags(tags);

			if (!Tag.AreValidNames(tags))
			{
				throw new ArgumentException("Invalid Tag name(s).", "tags");
			}

			//determine which tags need to be added
			List<string> oldTags = this.TagNames;
			this.AddTagsHelper(tags.Except(oldTags, StringComparer.CurrentCultureIgnoreCase));
		}

		/// <summary>
		/// Removes existing <see cref="WorkflowTag"/>s from this <see cref="Workflow"/> to disassociate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will only remove existing <see cref="WorkflowTag"/>s.
		/// This method will ignore any items in <paramref name="tags"/> that are not currently associated with this <see cref="Workflow"/>.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to remove.</param>
		public void RemoveTags(IEnumerable<string> tags)
		{
			tags = Workflow.PreprocessTags(tags);

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
		/// Returns a user-friendly string representation of an Workflow.
		/// </summary>
		/// <param name="Workflow">The Workflow to format.</param>
		/// <returns></returns>
		private static string Format(Workflow Workflow)
		{
			return string.Format(CultureInfo.CurrentCulture, "Workflow #{0} ({1:G})", Workflow.Id, Workflow.WorkflowState);
		}

		#endregion

		#region Workflow Lifecycle Methods

		#region Lifecycle-related Interrogative Methods

		#region IsDataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Configuration Service Group with a specific <see cref="WorkflowState"/>.
		/// </summary>
		/// <param name="state">The state of the Configuration Service Group that would be edited.</param>
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
					//Nobody can edit a Ready For Modification, Validated, Published or Cancelled Configuration Service Group
					return false;
			}

			//A user is allowed to "edit" an Configuration Service Group in this state if the user would be allowed to Abandon such an Configuration Service Group
			return IsStateTransitionAllowed(state, WorkflowStateId.Abandoned, requestorRoles);
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify the Configuration Service Group.
		/// </summary>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> requestorRoles)
		{
			return IsDataModificationAllowed(this.WorkflowState, requestorRoles);
		}

		#endregion

		#region IsMetadataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Configuration Service Group with a specific <see cref="WorkflowState"/>.
		/// </summary>
		/// <param name="state">The state of the Configuration Service Group that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsMetadataModificationAllowed(WorkflowStateId state, ICollection<UserRoleId> requestorRoles)
		{
			//NOTE: A specific user is allowed to "edit" an Workflow's "metadata" (e.g. tags) if the user is an Editor
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

			return IsMetadataModificationAllowed(this.WorkflowState, requestorRoles);
		}

		#endregion

		#region IsStateTransitionAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition between two specified <see cref="WorkflowState"/>s.
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
		/// Determines whether a user with a specified set of roles is authorized to execute a transition to a specified <see cref="WorkflowState"/> 
		/// from the Configuration Service group's current <see cref="WorkflowState"/>.
		/// </summary>
		/// <param name="toState">The new state that the Configuration Service group would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(WorkflowStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			if (this.IsDirty)
			{
				return false;
			}

           

			if (!IsStateTransitionAllowed(this.WorkflowState, toState, requestorRoles))
			{
				return false;
			}

			switch (toState)
			{
				case WorkflowStateId.ReadyForValidation:
				case WorkflowStateId.Validated:
				case WorkflowStateId.Published:
					//moving to any of these states requires that the Configuration Service group be "publishable"
					if (!this.IsDataPublishable())
					{
						return false;
					}
					break;
				case WorkflowStateId.Cancelled:
					if (!this.IsDataUnPublishable())
					{
						return false;
					}
					break;
				default:
					//case WorkflowStateId.None:
					//case WorkflowStateId.Modified:
					//case WorkflowStateId.Abandoned:
					//case WorkflowStateId.Deleted:
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
                    if (!requestorRoles.Contains(UserRoleId.Editor) )
					{
						return false;
					}
					break;
				default:
					//All transitions from any state not handled above (to any other state) require the Editor role
                    if (!requestorRoles.Contains(UserRoleId.Editor) )
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
                    if (!requestorRoles.Contains(UserRoleId.Validator) )
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
		/// <param name="toState">The new state that the Workflow would transition to.</param>
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
		/// Indicates whether the Workflow's data is complete and valid (i.e. could be published "as is").
		/// </summary>
		/// <returns></returns>
		public bool IsDataPublishable()
		{
			// Validate that all "required" fields are valid
			// One group selector is required.
			WorkflowSelectorQuerySpecification WorkflowSelectorQuerySpecification = new WorkflowSelectorQuerySpecification() { WorkflowId = this.Id };
			if (WorkflowSelectorController.FetchCount(WorkflowSelectorQuerySpecification) < 1)
			{
				return false;
			}

			switch (this.WorkflowState)
			{
				case WorkflowStateId.Published:
					//Published Workflows can only be re-Published if they could also be UnPublished
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
		/// Indicates whether the Workflow's data is being replaced (Published Workflow has already been copied)
		/// </summary>
		/// <returns></returns>
		public bool IsOriginalWorkflowReplacement()
		{
			if (this.ValidationId == null && this.ProductionId == null)
			{
				// not published so allow.
				return false;
			}
			else
			{
				if ((WorkflowStateId)this.WorkflowStatusId != WorkflowStateId.Published)
				{
					// not original.
					return false;
				}
				else
				{
					// check to see if replacement
					WorkflowQuerySpecification WorkflowQuerySpecification = new WorkflowQuerySpecification() { ValidationId = this.ValidationId };
					return WorkflowController.FetchCount(WorkflowQuerySpecification) > 1;
				}
			}
		}

		/// <summary>
		/// Indicates whether the Workflow's data is complete and valid enough to be UnPublished
		/// (i.e. could be unpublished "as is" ignoring the Workflow's State).
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
		/// Validates that the current user is allowed to transition the current ConfigurationService Group to a specified <see cref="WorkflowState"/>.
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
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid Workflow State transition (from {0:G} to {1:G}) attempted on {2} by {3}.", this.WorkflowState, toState, this, currentUser);
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
			WorkflowStateId fromState = this.WorkflowState;
			this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Workflow state changing: from '{0:G}' to '{1:G}'.", fromState, toState));
			this.WorkflowState = toState;
			this.Save(SecurityManager.CurrentUserIdentityName);
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
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

			//WorkflowLifecycle.Current.GoToState(this, toState);
			GoToState(this, toState, false);
		}

		private static void GoToState(Workflow instance, WorkflowStateId toState, bool isAutotransition)
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
		/// Changes the Workflow's <see cref="WorkflowState"/> to <see cref="WorkflowStateId.Abandoned"/>.
		/// </summary>
		public void Abandon()
		{
			this.GoToState(WorkflowStateId.Abandoned); //this.PerformSimpleStateTransition(WorkflowStateId.Abandoned);
		}

		/// <summary>
		/// Delete the Workflow
		/// </summary>
		public void Delete()
		{
			// can only delete Workflows that are abandoned.
			if (this.WorkflowState == WorkflowStateId.Abandoned)
			{
				try
				{
					this.ClearTags();
                    this.ClearWorkflowWorkflowModule();
					this.ClearWorkflowSelector();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture,
												   "Unable to delete WorkflowId #{0}.",
												   this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
			}
		}

		/// <summary>
		/// Deletes a specified Workflow record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = Workflow.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			WorkflowController.DestroyByQuery(query);
		}

		/// <summary>
		/// Changes the Workflow's <see cref="WorkflowState"/> to <see cref="WorkflowStateId.Published"/>.
		/// </summary>
		public void Publish()
		{
			WorkflowStateId toState = WorkflowStateId.Published;
			int? ProductionId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				WorkflowStateId fromState = this.WorkflowState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing workflow on the Elements server
				if (this.ProductionId != null)
				{
					WorkflowQuerySpecification WorkflowQuery = new WorkflowQuerySpecification() { ProductionId = this.ProductionId };
					WorkflowCollection WorkflowColl = WorkflowController.Fetch(WorkflowQuery);
					foreach (Workflow Workflow in WorkflowColl)
					{
						// skip existing one
						if (Workflow.Id != this.Id)
						{
							Workflow.WorkflowState = WorkflowStateId.Abandoned;
							Workflow.ValidationId = null;
							Workflow.ProductionId = null;
                            Workflow.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//  Publish to Elements/CMService here!
				this.ProductionId = ElementsPublisher.Instance.PublishWorkflow(this, HP.ElementsCPS.Data.CmService.Environment.Publication);
				this.WorkflowState = toState;
                this.VersionMinor = this.VersionMinor++;
                this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Info, this, message, pex);

                throw pex;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was successfully published to Elements as Elements Workflow #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ProductionId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the Workflow's <see cref="WorkflowState"/> to <see cref="WorkflowStateId.Cancelled"/>.
		/// </summary>
		public void UnPublish()
		{
			const WorkflowStateId toState = WorkflowStateId.Cancelled;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			WorkflowStateId fromState = this.WorkflowState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				// GADSC unpublish
				ElementsPublisher.Instance.UnPublishWorkflow(this, HP.ElementsCPS.Data.CmService.Environment.Publication);

				this.ProductionId = null;
				this.WorkflowState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was not un-published from Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was successfully un-published from Elements, but the system was unable to complete the UnPublish operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the Workflow's <see cref="WorkflowState"/> to <see cref="WorkflowStateId.Modified"/>.
		/// </summary>
		public void SubmitBackToEditor()
		{
			const WorkflowStateId toState = WorkflowStateId.Modified;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			WorkflowStateId fromState = this.WorkflowState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				if ((fromState == WorkflowStateId.Validated) || (fromState == WorkflowStateId.Cancelled))
				{
					// Unpublish from validation server here.
					ElementsPublisher.Instance.UnPublishWorkflow(this, HP.ElementsCPS.Data.CmService.Environment.Validation);

					// if this is a replacement, then re-validate original configuration service group
					if ((this.ValidationId != null) && (this.ProductionId != null))
					{
						WorkflowQuerySpecification workflowQuery = new WorkflowQuerySpecification()
						{
							ValidationId = this.ValidationId,
							ProductionId = this.ProductionId,
						    WorkflowStatusId = (int?)WorkflowStateId.PublishedProductionOnly
						};
						WorkflowCollection workflowColl = WorkflowController.Fetch(workflowQuery);
						foreach (Workflow workflow in workflowColl)
						{
							workflow.ValidationId = ElementsPublisher.Instance.PublishWorkflow(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
							workflow.WorkflowStatusId = (int)WorkflowStateId.Published;
							workflow.Save(SecurityManager.CurrentUserIdentityName);
						}
						// replacement so remove production id too
						this.ProductionId = null;
					}
					this.ValidationId = null;
				}
				this.WorkflowState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was not un-published from the Validation Server.", this.Id);
				LogManager.Current.Log(Severity.Info, this, message, pex);

				throw pex;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was successfully un-published from the Validation Server, but the system was unable to complete the operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the Workflow's <see cref="WorkflowState"/> to <see cref="WorkflowStateId.ReadyForValidation"/>.
		/// </summary>
		public void SubmitToValidator()
		{
			this.GoToState(WorkflowStateId.ReadyForValidation); //this.PerformSimpleStateTransition(WorkflowStateId.ReadyForValidation);
		}

		/// <summary>
		/// Changes the Workflow's <see cref="WorkflowState"/> to <see cref="WorkflowStateId.Validated"/>.
		/// </summary>
		public void Validate()
		{
			WorkflowStateId toState = WorkflowStateId.Validated;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				WorkflowStateId fromState = this.WorkflowState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

                // remove any existing configuration service group on validation server
				if (this.ValidationId != null)
				{
					WorkflowQuerySpecification workflowQuery = new WorkflowQuerySpecification() { ValidationId = this.ValidationId };
					WorkflowCollection workflowColl = WorkflowController.Fetch(workflowQuery);
					foreach (Workflow workflow in workflowColl)
					{
						// skip existing one
						if (workflow.Id != this.Id)
						{
							switch (workflow.WorkflowStatusId)
							{
								case (int)WorkflowStateId.Published:
									workflow.WorkflowStatusId = (int)WorkflowStateId.PublishedProductionOnly;
									break;
								case (int)WorkflowStateId.Validated:
									workflow.WorkflowStatusId = (int)WorkflowStateId.Modified;
									break;
								default:
									break;
							}
							workflow.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}
				this.ValidationId = ElementsPublisher.Instance.PublishWorkflow(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
				this.WorkflowState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Workflow state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Info, this, message, pex);

				throw pex;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Workflow #{0} was successfully published to Elements as Elements Workflow #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ValidationId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		#endregion

		#endregion

		#region WorkflowSelector-related convenience members

		/// <summary>
		/// Removes all associated configuration service Group selectors
		/// </summary>
		public void ClearWorkflowSelector()
		{
			WorkflowSelector.DestroyByWorkflowId(this.Id);
		}

        /// <summary>
        /// Indicates whether the Workflow's is attached to any existing group selectors.
        /// </summary>
        /// <returns></returns>
        public bool HasGroupSelector()
        {
            return (VwMapWorkflowSelectorController.FetchCountByWorkflowId(this.Id) > 0);
        }

		#endregion

        #region WorkflowWorkflowModule-related convenience members

        /// <summary>
        /// Removes all associated workflow workflow modules
        /// </summary>
        public void ClearWorkflowWorkflowModule()
        {
            WorkflowWorkflowModule.DestroyByWorkflowId(this.Id);
        }

        #endregion

	}
}
