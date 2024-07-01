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
	/// The non-generated portion of the JumpstationGroup class.
	/// </summary>
	public partial class JumpstationGroup : IStatefulEntity<JumpstationGroupStateId>
	{

		#region Implementation of IStatefulEntity<JumpstationGroupStateId>

		/// <summary>
		/// The entity's current lifecycle state within the lifecycle defined for the entity.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="JumpstationGroupStateId"/> value is passed to the property.
		/// </exception>
		public JumpstationGroupStateId CurrentState
		{
			get
			{
				return (JumpstationGroupStateId)this.JumpstationGroupStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "CurrentState", typeof(JumpstationGroupStateId));
				this.JumpstationGroupStatusId = (int)value;
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
		/// Strongly-typed convenience wrapper for the JumpstationGroupStatusId property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="JumpstationGroupStateId"/> value is passed to the property.
		/// </exception>
		public JumpstationGroupStateId JumpstationGroupState
		{
			get
			{
				return (JumpstationGroupStateId)this.JumpstationGroupStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "Status", typeof(JumpstationGroupStateId));
				this.JumpstationGroupStatusId = (int)value;
			}
		}

		/// <summary>
		/// Returns a list of the names of all the Tags associated with this JumpstationGroup.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and JumpstationGroupTag data is used to create the list of Tag names.
		/// </remarks>
		public List<string> TagNames
		{
			get
			{
				return JumpstationGroupController.GetTagNameList(this.Id, RowStatus.RowStatusId.Active);
			}

		}

		/// <summary>
		/// Returns a list of the IDs of all the Tags associated with this JumpstationGroup.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and JumpstationGroupTag data is used to create the list of Tag names.
		/// </remarks>
		public List<int> TagIds
		{
			get
			{
				return VwMapJumpstationGroupTagTagController.CreateTagIdList(VwMapJumpstationGroupTagTagController.GetActiveTagsByJumpstationGroupId(this.Id));
			}
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? JumpstationGroupId = (this.IsNew ? null : (int?)this.Id);
			Log(JumpstationGroupId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? JumpstationGroupId = (this.IsNew ? null : (int?)this.Id);
			Log(JumpstationGroupId, severity, this, message, ex);
		}

		internal static void Log(int? JumpstationGroupId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup History: #{0}: {1}.", JumpstationGroupId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? JumpstationGroupId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup History: #{0}: {1}.", JumpstationGroupId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Creates a new <see cref="JumpstationGroup"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="JumpstationGroup"/>.</returns>
		public JumpstationGroup SaveAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Creates a new <see cref="JumpstationGroup"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="JumpstationGroup"/>.</returns>
		public JumpstationGroup SaveAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, copyElementsId);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public JumpstationGroup SaveAsNew()
		{
			return SaveAsNew(this, false, false);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="JumpstationGroup"/>.
		/// </summary>
		/// <param name="originalJumpstationGroup">The <see cref="JumpstationGroup"/> to copy/duplicate.</param>
		/// <param name="assignCurrentUserAsOwner">If <c>true</c>, then the new JumpstationGroup will be (re)assigned to the current user; else it will be assigned to the original JumpstationGroup's owner.</param>
		/// <param name="copyElementsId">If <c>true</c>, then new JumpstationGroup will maintain the existing ElementsIds (ValidationId and ProductionId)</param>
		private static JumpstationGroup SaveAsNew(JumpstationGroup originalJumpstationGroup, bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			JumpstationGroup newJumpstationGroup;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newJumpstationGroup = JumpstationGroup.Copy(originalJumpstationGroup);

				//mark the newJumpstationGroup instance as an unsaved instance
				newJumpstationGroup.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newJumpstationGroup.CreatedBy = createdBy;
				newJumpstationGroup.CreatedOn = createdOn;
				newJumpstationGroup.ModifiedBy = createdBy;
				newJumpstationGroup.ModifiedOn = createdOn;

				//modify the status fields' values appropriately
				newJumpstationGroup.JumpstationGroupState = JumpstationGroupStateId.Modified;

				if (!copyElementsId)
				{
					newJumpstationGroup.ValidationId = null;
					newJumpstationGroup.ProductionId = null;
				}

				newJumpstationGroup.RowStatusId = (int)RowStatus.RowStatusId.Active;

				if (assignCurrentUserAsOwner)
				{
					//assign the current user as the Owner of the newJumpstationGroup
					newJumpstationGroup.OwnerId = PersonController.GetCurrentUser().Id;
				}

				//save the newJumpstationGroup to the DB so that it is assigned an ID
				newJumpstationGroup.Save(SecurityManager.CurrentUserIdentityName);

				//copy all of the originalJumpstationGroup's Tags to the newJumpstationGroup
				newJumpstationGroup.SetTags(originalJumpstationGroup.TagIds);

				//save as new Configuration Service Group Selector
				JumpstationGroupSelectorCollection colJumpstationGroupSelector = new JumpstationGroupSelectorCollection();
				foreach (JumpstationGroupSelector gsOriginalRecord in originalJumpstationGroup.JumpstationGroupSelectorRecords())
				{
					JumpstationGroupSelector gsNewRecord = JumpstationGroupSelector.Copy(gsOriginalRecord);
					gsNewRecord.MarkNew();
					gsNewRecord.JumpstationGroupId = newJumpstationGroup.Id;
					gsNewRecord.CreatedBy = createdBy;
					gsNewRecord.CreatedOn = createdOn;
					gsNewRecord.ModifiedBy = createdBy;
					gsNewRecord.ModifiedOn = createdOn;
					gsNewRecord.Save(SecurityManager.CurrentUserIdentityName);

					//copy all of the originalJumpstationGroupSelector's JumpstationGroupSelectorQueryParameterValue data to the newJumpstationGroupSelector
					JumpstationGroupSelectorQueryParameterValueCollection colJumpstationGroupSelectorQueryParameterValue = new JumpstationGroupSelectorQueryParameterValueCollection();
					foreach (JumpstationGroupSelectorQueryParameterValue qpvOriginalRecord in gsOriginalRecord.JumpstationGroupSelectorQueryParameterValueRecords())
					{
						JumpstationGroupSelectorQueryParameterValue qpvNewRecord = JumpstationGroupSelectorQueryParameterValue.Copy(qpvOriginalRecord);
						qpvNewRecord.MarkNew();
						qpvNewRecord.JumpstationGroupSelectorId = gsNewRecord.Id;
						qpvNewRecord.CreatedBy = createdBy;
						qpvNewRecord.CreatedOn = createdOn;
						qpvNewRecord.ModifiedBy = createdBy;
						qpvNewRecord.ModifiedOn = createdOn;
						colJumpstationGroupSelectorQueryParameterValue.Add(qpvNewRecord);
					}
					colJumpstationGroupSelectorQueryParameterValue.SaveAll(SecurityManager.CurrentUserIdentityName);
				}

				originalJumpstationGroup.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newJumpstationGroup));
				newJumpstationGroup.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalJumpstationGroup));

				scope.Complete(); // transaction complete
			}

			return newJumpstationGroup;
		}

		#endregion

		#region Tag-related Methods

		/// <summary>
		/// Disassociates this JumpstationGroup from a specific Tag.
		/// </summary>
		/// <param name="tagId"></param>
		public void RemoveTag(int tagId)
		{
			JumpstationGroupTag.Destroy(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="JumpstationGroup"/> with a specific <see cref="Tag"/>.
		/// </summary>
		/// <param name="tagId">The ID of an existing <see cref="Tag"/>.</param>
		public JumpstationGroupTag AddTag(int tagId)
		{
			return JumpstationGroupTag.Insert(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="JumpstationGroup"/> with a specific <see cref="Tag"/> (creating a new <see cref="Tag"/> if needed).
		/// </summary>
		/// <param name="tagName">The Name of a <see cref="Tag"/> (either existing or not).</param>
		public JumpstationGroupTag AddTag(string tagName)
		{
			if (!Tag.IsValidName(tagName))
			{
				throw new ArgumentException("Invalid Tag name.", "tagName");
			}

			Tag tag = TagController.GetOrCreateByName(tagName);
			return this.AddTag(tag.Id);
		}

		/// <summary>
		/// Deletes/removes all <see cref="Tag"/>s associated with this <see cref="JumpstationGroup"/>.
		/// </summary>
		public void ClearTags()
		{
			JumpstationGroupTag.DestroyByJumpstationGroupId(this.Id);
			this.Log(Severity.Debug, "JumpstationGroup tag removed: All tags were deleted.");
		}

		/// <summary>
		/// Replaces all JumpstationGroupTags currently associated with this <see cref="JumpstationGroup"/> with the specified set of new <see cref="Tag"/>s.
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
		/// Replaces all JumpstationGroupTags currently associated with this <see cref="JumpstationGroup"/> with the specified set of new <see cref="Tag"/>s.
		/// </summary>
		/// <param name="tags">The Names of the new set of <see cref="Tag"/>s.</param>
		public void SetTags(IEnumerable<string> tags)
		{
			tags = JumpstationGroup.PreprocessTags(tags);

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
				Tag tag = TagController.GetByName(tagName.Trim());
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
				this.AddTag(tagName.Trim());
				//this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Configuration Service group tag added: '{0}'.", tagName));
			}
		}

		#endregion

		/// <summary>
		/// Adds additional <see cref="JumpstationGroupTag"/>s to this <see cref="JumpstationGroup"/> to associate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will not remove any existing <see cref="JumpstationGroupTag"/>s.
		/// This method will not create any duplicate <see cref="JumpstationGroupTag"/>s.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to add.</param>
		public void AddTags(IEnumerable<string> tags)
		{
			tags = JumpstationGroup.PreprocessTags(tags);

			if (!Tag.AreValidNames(tags))
			{
				throw new ArgumentException("Invalid Tag name(s).", "tags");
			}

			//determine which tags need to be added
			List<string> oldTags = this.TagNames;
			this.AddTagsHelper(tags.Except(oldTags, StringComparer.CurrentCultureIgnoreCase));
		}

		/// <summary>
		/// Removes existing <see cref="JumpstationGroupTag"/>s from this <see cref="JumpstationGroup"/> to disassociate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will only remove existing <see cref="JumpstationGroupTag"/>s.
		/// This method will ignore any items in <paramref name="tags"/> that are not currently associated with this <see cref="JumpstationGroup"/>.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to remove.</param>
		public void RemoveTags(IEnumerable<string> tags)
		{
			tags = JumpstationGroup.PreprocessTags(tags);

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
		/// Returns a user-friendly string representation of an JumpstationGroup.
		/// </summary>
		/// <param name="JumpstationGroup">The JumpstationGroup to format.</param>
		/// <returns></returns>
		private static string Format(JumpstationGroup JumpstationGroup)
		{
			return string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} ({1:G})", JumpstationGroup.Id, JumpstationGroup.JumpstationGroupState);
		}

		#endregion

		#region JumpstationGroup Lifecycle Methods

		#region Lifecycle-related Interrogative Methods

		#region IsDataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Configuration Service Group with a specific <see cref="JumpstationGroupState"/>.
		/// </summary>
		/// <param name="state">The state of the Configuration Service Group that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsDataModificationAllowed(JumpstationGroupStateId state, ICollection<UserRoleId> requestorRoles )
		{
			switch (state)
			{
				case JumpstationGroupStateId.ReadyForValidation:
				case JumpstationGroupStateId.Validated:
				case JumpstationGroupStateId.Published:
				case JumpstationGroupStateId.Cancelled:
					//Nobody can edit a Ready For Modification, Validated, Published or Cancelled Configuration Service Group
					return false;
			}
            
			//A user is allowed to "edit" an Configuration Service Group in this state if the user would be allowed to Abandon such an Configuration Service Group
			return IsStateTransitionAllowed(state, JumpstationGroupStateId.Abandoned, requestorRoles);
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify the Configuration Service Group.
		/// </summary>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> requestorRoles)
		{
			return IsDataModificationAllowed(this.JumpstationGroupState, requestorRoles);
		}

		#endregion

		#region IsMetadataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Configuration Service Group with a specific <see cref="JumpstationGroupState"/>.
		/// </summary>
		/// <param name="state">The state of the Configuration Service Group that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsMetadataModificationAllowed(JumpstationGroupStateId state, ICollection<UserRoleId> requestorRoles)
		{
			//NOTE: A specific user is allowed to "edit" an JumpstationGroup's "metadata" (e.g. tags) if the user is an Editor
			ICollection<UserRoleId> roles = SecurityManager.Current.GetEffectiveRoles(requestorRoles);
            return roles.Contains(UserRoleId.Editor) ;
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

			return IsMetadataModificationAllowed(this.JumpstationGroupState, requestorRoles);
		}

		#endregion

		#region IsStateTransitionAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition between two specified <see cref="JumpstationGroupState"/>s.
		/// </summary>
		/// <param name="fromState">The initial/starting state that the ProxyURL would transition from.</param>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if the specified transition is allowed.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if either <paramref name="fromState"/> or <paramref name="toState"/> is invalid.
		/// </exception>
		public static bool IsStateTransitionAllowed(JumpstationGroupStateId fromState, JumpstationGroupStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(fromState, "fromState", typeof(JumpstationGroupStateId));
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(JumpstationGroupStateId));

			//a transition must be between 2 distinct states
			if (toState == JumpstationGroupStateId.None)
			{
				return false;
				//throw new ArgumentException("toState cannot be None", "toState");
			}

			//a transition must be between 2 distinct states
			if ((fromState == toState) && (fromState != JumpstationGroupStateId.Published))
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
		/// Determines whether a user with a specified set of roles is authorized to execute a transition to a specified <see cref="JumpstationGroupState"/> 
		/// from the Configuration Service group's current <see cref="JumpstationGroupState"/>.
		/// </summary>
		/// <param name="toState">The new state that the Configuration Service group would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(JumpstationGroupStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			if (this.IsDirty)
			{
				return false;
			}

			if (!IsStateTransitionAllowed(this.JumpstationGroupState, toState, requestorRoles))
			{
				return false;
			}

			switch (toState)
			{
				case JumpstationGroupStateId.ReadyForValidation:
				case JumpstationGroupStateId.Validated:
				case JumpstationGroupStateId.Published:
					//moving to any of these states requires that the Configuration Service group be "publishable"
					if (!this.IsDataPublishable())
					{
						return false;
					}
					break;
				case JumpstationGroupStateId.Cancelled:
					if (!this.IsDataUnPublishable())
					{
						return false;
					}
					break;
				default:
					//case JumpstationGroupStateId.None:
					//case JumpstationGroupStateId.Modified:
					//case JumpstationGroupStateId.Abandoned:
					//case JumpstationGroupStateId.Deleted:
					break;
			}

			return true;
		}

		#region IsStateTransitionAllowed Helper Methods

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_StatesPaths(JumpstationGroupStateId fromState, JumpstationGroupStateId toState)
		{
			//verify that the current state can transition to the requested state
			switch (toState)
			{
				case SubSonicClient.JumpstationGroupStateId.Cancelled:
					if (fromState != JumpstationGroupStateId.Published)
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationGroupStateId.Validated:
					if (fromState != JumpstationGroupStateId.ReadyForValidation)
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationGroupStateId.Published:
					if (!((fromState == JumpstationGroupStateId.Validated) || (fromState == JumpstationGroupStateId.Published) || (fromState == JumpstationGroupStateId.Cancelled)))
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationGroupStateId.Modified:
					List<JumpstationGroupStateId> possibleFromStates_Modified = new List<JumpstationGroupStateId>(
						new[] { JumpstationGroupStateId.Abandoned, JumpstationGroupStateId.ReadyForValidation, JumpstationGroupStateId.Validated, JumpstationGroupStateId.Cancelled });
					if (!possibleFromStates_Modified.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationGroupStateId.ReadyForValidation:
					List<JumpstationGroupStateId> possibleFromStates_ReadyForValidation = new List<JumpstationGroupStateId>(
						new[] { JumpstationGroupStateId.Abandoned, JumpstationGroupStateId.Modified });
					if (!possibleFromStates_ReadyForValidation.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationGroupStateId.Abandoned:
					List<JumpstationGroupStateId> possibleFromStates_Abandoned = new List<JumpstationGroupStateId>(
						new[] { JumpstationGroupStateId.Modified, JumpstationGroupStateId.ReadyForValidation });
					if (!possibleFromStates_Abandoned.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.JumpstationGroupStateId.Deleted:
					if (fromState != JumpstationGroupStateId.Abandoned)
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
		private static bool IsStateTransitionAllowedHelper_SpecialStates(JumpstationGroupStateId fromState, JumpstationGroupStateId toState)
		{
			//the None state is a virtual start state, and may only proceed to the Modified state
			if (fromState == JumpstationGroupStateId.None)
			{
				return (toState == JumpstationGroupStateId.Modified);
			}
			return true;
		}

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_RoleBasedRules(JumpstationGroupStateId fromState, JumpstationGroupStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			//First, enforce the "simple" transition rules requiring knowledge of only roles and fromState
			switch (fromState)
			{
				case JumpstationGroupStateId.Validated:
				case JumpstationGroupStateId.Published:
				case JumpstationGroupStateId.Cancelled:
					//All transitions from Validated or Published (to any other state) require the Coordinator role
                   
                   if (!requestorRoles.Contains(UserRoleId.Coordinator))
                   {
                       return false;
                   }
					break;
				case JumpstationGroupStateId.ReadyForValidation:
					//All transitions from ReadyForValidation (to any other state) require the Validator role
                    if (!requestorRoles.Contains(UserRoleId.Validator) )
					{
						return false;
					}
                  
					break;
				case JumpstationGroupStateId.Modified:
					//All transitions from Modified (to any other state) require the Editor role
                    if (!requestorRoles.Contains(UserRoleId.Editor) )
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
				case JumpstationGroupStateId.Cancelled:
				case JumpstationGroupStateId.Published:
					//All transitions to Cancelled or Published (from any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case JumpstationGroupStateId.Validated:
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
		/// Determines whether a user with a specified set of roles would be authorized to execute a transition to a specified <see cref="JumpstationGroupState"/> 
		/// (from any other possible state).
		/// </summary>
		/// <param name="toState">The new state that the JumpstationGroup would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if any potential transition is possible.</returns>
		public static bool IsStateTransitionPossible(JumpstationGroupStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			Array allStates = Enum.GetValues(typeof(JumpstationGroupStateId));
			foreach (JumpstationGroupStateId fromState in allStates)
			{
				if (IsStateTransitionAllowed(fromState, toState, requestorRoles))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Indicates whether the JumpstationGroup's data is complete and valid (i.e. could be published "as is").
		/// </summary>
		/// <returns></returns>
		public bool IsDataPublishable()
		{
			// Validate that all "required" fields are valid
			// One group selector is required.
			JumpstationGroupSelectorQuerySpecification JumpstationGroupSelectorQuerySpecification = new JumpstationGroupSelectorQuerySpecification() { JumpstationGroupId = this.Id };
			if (JumpstationGroupSelectorController.FetchCount(JumpstationGroupSelectorQuerySpecification) < 1)
			{
				return false;
			}

			switch (this.JumpstationGroupState)
			{
				case JumpstationGroupStateId.Published:
					//Published JumpstationGroups can only be re-Published if they could also be UnPublished
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
		/// Indicates whether the JumpstationGroup's data is being replaced (Published JumpstationGroup has already been copied)
		/// </summary>
		/// <returns></returns>
		public bool IsOriginalJumpstationGroupReplacement()
		{
			if (this.ValidationId == null && this.ProductionId == null)
			{
				// not published so allow.
				return false;
			}
			else
			{
				if ((JumpstationGroupStateId)this.JumpstationGroupStatusId != JumpstationGroupStateId.Published)
				{
					// not original.
					return false;
				}
				else
				{
					// check to see if replacement
					JumpstationGroupQuerySpecification JumpstationGroupQuerySpecification = new JumpstationGroupQuerySpecification() { ValidationId = this.ValidationId };
					return JumpstationGroupController.FetchCount(JumpstationGroupQuerySpecification) > 1;
				}
			}
		}

		/// <summary>
		/// Indicates whether the JumpstationGroup's data is complete and valid enough to be UnPublished
		/// (i.e. could be unpublished "as is" ignoring the JumpstationGroup's State).
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
		/// Validates that the current user is allowed to transition the current Jumpstation Group to a specified <see cref="JumpstationGroupState"/>.
		/// </summary>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the state transition is not allowed or the current user is not authorized to initiate it.
		/// </exception>
		private void AuthorizeStateTransition(JumpstationGroupStateId toState)
		{
			Person currentUser = PersonController.GetCurrentUser();
			List<UserRoleId> currentUserRoles = currentUser.GetRoles();
			if (!this.IsStateTransitionAllowed(toState, currentUserRoles))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid Configuration Service Group State transition (from {0:G} to {1:G}) attempted on {2} by {3}.", this.JumpstationGroupState, toState, this, currentUser);
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
		private void PerformSimpleStateTransition(JumpstationGroupStateId toState)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(JumpstationGroupStateId));

			//validate that the toState does not require a "complex transition"
			switch (toState)
			{
				case JumpstationGroupStateId.None:
				case JumpstationGroupStateId.Published:
				case JumpstationGroupStateId.Cancelled:
					throw new InvalidOperationException(
						string.Format(CultureInfo.CurrentCulture,
							"The PerformSimpleStateTransition() method cannot be used to transition to state {0:G}", toState));
			}

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			JumpstationGroupStateId fromState = this.JumpstationGroupState;
			this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Configuration Service Group state changing: from '{0:G}' to '{1:G}'.", fromState, toState));
			this.JumpstationGroupState = toState;
			this.Save(SecurityManager.CurrentUserIdentityName);
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Configuration Service Group state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
		}

		#endregion

		#region GoToState... Methods

		/// <summary>
		/// Transitions the instance to a specified state.
		/// </summary>
		/// <param name="toState">The state to transition the instance to.</param>
		public void GoToState(JumpstationGroupStateId toState)
		{
			//TODO: Refactor: Refactor this implementation to a standard BaseLifecycle implementation
#warning Refactor: Refactor this implementation to a standard BaseLifecycle implementation

			//JumpstationGroupLifecycle.Current.GoToState(this, toState);
			GoToState(this, toState, false);
		}

		private static void GoToState(JumpstationGroup instance, JumpstationGroupStateId toState, bool isAutotransition)
		{
			switch (toState)
			{
				case JumpstationGroupStateId.Modified:
					instance.SubmitBackToEditor();
					break;
				case JumpstationGroupStateId.Validated:
					instance.Validate();
					break;
				case JumpstationGroupStateId.Published:
					instance.Publish();
					break;
				case JumpstationGroupStateId.Cancelled:
					instance.UnPublish();
					break;
				case JumpstationGroupStateId.Deleted:
					instance.Delete();
					break;
				case JumpstationGroupStateId.Abandoned:
				case JumpstationGroupStateId.ReadyForValidation:
					instance.PerformSimpleStateTransition(toState);
					break;
				case JumpstationGroupStateId.None:
				case JumpstationGroupStateId.PublishedProductionOnly:
					throw new InvalidOperationException(string.Format("Explicit transition to state {0:G} is not supported.", toState));
				default:
					throw new ArgumentOutOfRangeException("toState");
			}
		}

		#endregion

		/// <summary>
		/// Changes the JumpstationGroup's <see cref="JumpstationGroupState"/> to <see cref="JumpstationGroupStateId.Abandoned"/>.
		/// </summary>
		public void Abandon()
		{
			this.GoToState(JumpstationGroupStateId.Abandoned); //this.PerformSimpleStateTransition(JumpstationGroupStateId.Abandoned);
		}

		/// <summary>
		/// Delete the JumpstationGroup
		/// </summary>
		public void Delete()
		{
			// can only delete JumpstationGroups that are abandoned.
			if (this.JumpstationGroupState == JumpstationGroupStateId.Abandoned)
			{
				try
				{
					this.ClearTags();
					this.ClearJumpstationGroupSelector();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture,
												   "Unable to delete JumpstationGroupId #{0}.",
												   this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
			}
		}

		/// <summary>
		/// Deletes a specified JumpstationGroup record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = JumpstationGroup.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			JumpstationGroupController.DestroyByQuery(query);
		}

		/// <summary>
		/// Changes the JumpstationGroup's <see cref="JumpstationGroupState"/> to <see cref="JumpstationGroupStateId.Published"/>.
		/// </summary>
		public void Publish()
		{
			JumpstationGroupStateId toState = JumpstationGroupStateId.Published;
			int? ProductionId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				JumpstationGroupStateId fromState = this.JumpstationGroupState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationGroup state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing configuration service group on the Elements server
				if (this.ProductionId != null)
				{
					JumpstationGroupQuerySpecification JumpstationGroupQuery = new JumpstationGroupQuerySpecification() { ProductionId = this.ProductionId };
					JumpstationGroupCollection JumpstationGroupColl = JumpstationGroupController.Fetch(JumpstationGroupQuery);
					foreach (JumpstationGroup JumpstationGroup in JumpstationGroupColl)
					{
						// skip existing one
						if (JumpstationGroup.Id != this.Id)
						{
							JumpstationGroup.JumpstationGroupState = JumpstationGroupStateId.Abandoned;
							JumpstationGroup.ValidationId = null;
							JumpstationGroup.ProductionId = null;
							JumpstationGroup.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//  Publish to Elements/CMService here!
				this.ProductionId = ElementsPublisher.Instance.PublishJumpstationGroup(this, HP.ElementsCPS.Data.CmService.Environment.Publication);
				this.JumpstationGroupState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationGroup state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} was successfully published to Elements as Elements JumpstationGroup #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ProductionId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the JumpstationGroup's <see cref="JumpstationGroupState"/> to <see cref="JumpstationGroupStateId.Cancelled"/>.
		/// </summary>
		public void UnPublish()
		{
			const JumpstationGroupStateId toState = JumpstationGroupStateId.Cancelled;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			JumpstationGroupStateId fromState = this.JumpstationGroupState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationGroup state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				// GADSC unpublish
				ElementsPublisher.Instance.UnPublishJumpstationGroup(this, HP.ElementsCPS.Data.CmService.Environment.Publication);

				this.ProductionId = null;
				this.JumpstationGroupState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationGroup state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} was not un-published from Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} was successfully un-published from Elements, but the system was unable to complete the UnPublish operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the JumpstationGroup's <see cref="JumpstationGroupState"/> to <see cref="JumpstationGroupStateId.Modified"/>.
		/// </summary>
		public void SubmitBackToEditor()
		{
			const JumpstationGroupStateId toState = JumpstationGroupStateId.Modified;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			JumpstationGroupStateId fromState = this.JumpstationGroupState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationGroup state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				if ((fromState == JumpstationGroupStateId.Validated) || (fromState == JumpstationGroupStateId.Cancelled))
				{
					// Unpublish from validation server here.
					ElementsPublisher.Instance.UnPublishJumpstationGroup(this, HP.ElementsCPS.Data.CmService.Environment.Validation);

					// if this is a replacement, then re-validate original jumpstation group
					if ((this.ValidationId != null) && (this.ProductionId != null))
					{
						JumpstationGroupQuerySpecification JumpstationGroupQuery = new JumpstationGroupQuerySpecification()
																	{
																		ValidationId = this.ValidationId,
																		ProductionId = this.ProductionId,
																		JumpstationGroupStatusId = (int?)JumpstationGroupStateId.PublishedProductionOnly
																	};
						JumpstationGroupCollection JumpstationGroupColl = JumpstationGroupController.Fetch(JumpstationGroupQuery);
						foreach (JumpstationGroup JumpstationGroup in JumpstationGroupColl)
						{
							//
							//  GADSC -> You will re-validate to Elements/CMService here!
							//
                            JumpstationGroup.ValidationId = ElementsPublisher.Instance.PublishJumpstationGroup(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
							JumpstationGroup.JumpstationGroupStatusId = (int)JumpstationGroupStateId.Published;
							JumpstationGroup.Save(SecurityManager.CurrentUserIdentityName);
						}
						// replacement so remove production id too
						this.ProductionId = null;
					}
					this.ValidationId = null;
				}
				this.JumpstationGroupState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationGroup state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} was not un-published from the Validation Server.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} was successfully un-published from the Validation Server, but the system was unable to complete the operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the JumpstationGroup's <see cref="JumpstationGroupState"/> to <see cref="JumpstationGroupStateId.ReadyForValidation"/>.
		/// </summary>
		public void SubmitToValidator()
		{
			this.GoToState(JumpstationGroupStateId.ReadyForValidation); //this.PerformSimpleStateTransition(JumpstationGroupStateId.ReadyForValidation);
		}

		/// <summary>
		/// Changes the JumpstationGroup's <see cref="JumpstationGroupState"/> to <see cref="JumpstationGroupStateId.Validated"/>.
		/// </summary>
		public void Validate()
		{
			JumpstationGroupStateId toState = JumpstationGroupStateId.Validated;
			int? ValidationId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				JumpstationGroupStateId fromState = this.JumpstationGroupState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationGroup state changing: from '{0:G}' to '{1:G}'.", fromState, toState));


				// Remove this
				//if (this.ValidationId == null)
				//{
				//    PublishTemp publishTemp = new PublishTemp();
				//    publishTemp.Save(SecurityManager.CurrentUserIdentityName);
				//    ValidationId = publishTemp.Id;
				//}
				//else
				//{
				//    ValidationId = this.ValidationId;
				//}
				// Remove this	


				// remove any existing configuration service group on validation server
				if (this.ValidationId != null)
				{
					JumpstationGroupQuerySpecification JumpstationGroupQuery = new JumpstationGroupQuerySpecification() { ValidationId = this.ValidationId };
					JumpstationGroupCollection JumpstationGroupColl = JumpstationGroupController.Fetch(JumpstationGroupQuery);
					foreach (JumpstationGroup JumpstationGroup in JumpstationGroupColl)
					{
						// skip existing one
						if (JumpstationGroup.Id != this.Id)
						{
							switch (JumpstationGroup.JumpstationGroupStatusId)
							{
								case (int)JumpstationGroupStateId.Published:
									JumpstationGroup.JumpstationGroupStatusId = (int)JumpstationGroupStateId.PublishedProductionOnly;
									break;
								case (int)JumpstationGroupStateId.Validated:
									JumpstationGroup.JumpstationGroupStatusId = (int)JumpstationGroupStateId.Modified;
									break;
								default:
									break;
							}
							JumpstationGroup.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//
				//  GADSC -> You will publish to Elements/CMService validation here!
				//
				//  Note - If ValidationId exists, then publish using the same ValidationId (i.e. replace the existing one).
				//
				this.ValidationId = ElementsPublisher.Instance.PublishJumpstationGroup(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
				this.JumpstationGroupState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "JumpstationGroup state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "JumpstationGroup #{0} was successfully published to Elements as Elements JumpstationGroup #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ValidationId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		#endregion

		#endregion

		#region JumpstationGroupSelector-related convenience members

		/// <summary>
		/// Removes all associated configuration service Group selectors
		/// </summary>
		public void ClearJumpstationGroupSelector()
		{
			JumpstationGroupSelector.DestroyByJumpstationGroupId(this.Id);
		}

        /// <summary>
        /// Indicates whether the JumpstationGroup's is attached to any existing group selectors.
        /// </summary>
        /// <returns></returns>
        public bool HasGroupSelector()
        {
            return (VwMapJumpstationGroupSelectorController.FetchCountByJumpstationGroupId(this.Id) > 0);
        }

		#endregion

	}
}
