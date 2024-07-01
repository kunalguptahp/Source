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
using System.Configuration;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceGroup class.
	/// </summary>
	public partial class ConfigurationServiceGroup : IStatefulEntity<ConfigurationServiceGroupStateId>
	{

		#region Implementation of IStatefulEntity<ConfigurationServiceGroupStateId>

		/// <summary>
		/// The entity's current lifecycle state within the lifecycle defined for the entity.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="ConfigurationServiceGroupStateId"/> value is passed to the property.
		/// </exception>
		public ConfigurationServiceGroupStateId CurrentState
		{
			get
			{
				return (ConfigurationServiceGroupStateId)this.ConfigurationServiceGroupStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "CurrentState", typeof(ConfigurationServiceGroupStateId));
				this.ConfigurationServiceGroupStatusId = (int)value;
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
		/// Strongly-typed convenience wrapper for the ConfigurationServiceGroupStatusId property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="ConfigurationServiceGroupStateId"/> value is passed to the property.
		/// </exception>
		public ConfigurationServiceGroupStateId ConfigurationServiceGroupState
		{
			get
			{
				return (ConfigurationServiceGroupStateId)this.ConfigurationServiceGroupStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "Status", typeof(ConfigurationServiceGroupStateId));
				this.ConfigurationServiceGroupStatusId = (int)value;
			}
		}

		/// <summary>
		/// Returns a list of the names of all the Tags associated with this ConfigurationServiceGroup.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and ConfigurationServiceGroupTag data is used to create the list of Tag names.
		/// </remarks>
		public List<string> TagNames
		{
			get
			{
				return ConfigurationServiceGroupController.GetTagNameList(this.Id, RowStatus.RowStatusId.Active);
			}

		}

		/// <summary>
		/// Returns a list of the IDs of all the Tags associated with this ConfigurationServiceGroup.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and ConfigurationServiceGroupTag data is used to create the list of Tag names.
		/// </remarks>
		public List<int> TagIds
		{
			get
			{
				return VwMapConfigurationServiceGroupTagTagController.CreateTagIdList(VwMapConfigurationServiceGroupTagTagController.GetActiveTagsByConfigurationServiceGroupId(this.Id));
			}
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? ConfigurationServiceGroupId = (this.IsNew ? null : (int?)this.Id);
			Log(ConfigurationServiceGroupId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? ConfigurationServiceGroupId = (this.IsNew ? null : (int?)this.Id);
			Log(ConfigurationServiceGroupId, severity, this, message, ex);
		}

		internal static void Log(int? ConfigurationServiceGroupId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup History: #{0}: {1}.", ConfigurationServiceGroupId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? ConfigurationServiceGroupId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup History: #{0}: {1}.", ConfigurationServiceGroupId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Creates a new <see cref="ConfigurationServiceGroup"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="ConfigurationServiceGroup"/>.</returns>
		public ConfigurationServiceGroup SaveAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Creates a new <see cref="ConfigurationServiceGroup"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="ConfigurationServiceGroup"/>.</returns>
		public ConfigurationServiceGroup SaveAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, copyElementsId);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public ConfigurationServiceGroup SaveAsNew()
		{
			return SaveAsNew(this, false, false);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="ConfigurationServiceGroup"/>.
		/// </summary>
		/// <param name="originalConfigurationServiceGroup">The <see cref="ConfigurationServiceGroup"/> to copy/duplicate.</param>
		/// <param name="assignCurrentUserAsOwner">If <c>true</c>, then the new ConfigurationServiceGroup will be (re)assigned to the current user; else it will be assigned to the original ConfigurationServiceGroup's owner.</param>
		/// <param name="copyElementsId">If <c>true</c>, then new ConfigurationServiceGroup will maintain the existing ElementsIds (ValidationId and ProductionId)</param>
		private static ConfigurationServiceGroup SaveAsNew(ConfigurationServiceGroup originalConfigurationServiceGroup, bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			ConfigurationServiceGroup newConfigurationServiceGroup;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newConfigurationServiceGroup = ConfigurationServiceGroup.Copy(originalConfigurationServiceGroup);

				//mark the newConfigurationServiceGroup instance as an unsaved instance
				newConfigurationServiceGroup.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newConfigurationServiceGroup.CreatedBy = createdBy;
				newConfigurationServiceGroup.CreatedOn = createdOn;
				newConfigurationServiceGroup.ModifiedBy = createdBy;
				newConfigurationServiceGroup.ModifiedOn = createdOn;

				//modify the status fields' values appropriately
				newConfigurationServiceGroup.ConfigurationServiceGroupState = ConfigurationServiceGroupStateId.Modified;

				if (!copyElementsId)
				{
					newConfigurationServiceGroup.ValidationId = null;
					newConfigurationServiceGroup.ProductionId = null;
				}

				newConfigurationServiceGroup.RowStatusId = (int)RowStatus.RowStatusId.Active;

				if (assignCurrentUserAsOwner)
				{
					//assign the current user as the Owner of the newConfigurationServiceGroup
					newConfigurationServiceGroup.OwnerId = PersonController.GetCurrentUser().Id;
				}

				//save the newConfigurationServiceGroup to the DB so that it is assigned an ID
				newConfigurationServiceGroup.Save(SecurityManager.CurrentUserIdentityName);

				//copy all of the originalConfigurationServiceGroup's Tags to the newConfigurationServiceGroup
				newConfigurationServiceGroup.SetTags(originalConfigurationServiceGroup.TagIds);

				//save as new Configuration Service Label Value
				ConfigurationServiceLabelValueCollection colConfigurationServiceLabelValue = new ConfigurationServiceLabelValueCollection();
				foreach (ConfigurationServiceLabelValue originalRecord in originalConfigurationServiceGroup.ConfigurationServiceLabelValueRecords())
				{
					ConfigurationServiceLabelValue newRecord = ConfigurationServiceLabelValue.Copy(originalRecord);
					newRecord.MarkNew();
					newRecord.ConfigurationServiceGroupId = newConfigurationServiceGroup.Id;
					newRecord.CreatedBy = createdBy;
					newRecord.CreatedOn = createdOn;
					newRecord.ModifiedBy = createdBy;
					newRecord.ModifiedOn = createdOn;
					colConfigurationServiceLabelValue.Add(newRecord);
				}
				colConfigurationServiceLabelValue.SaveAll(SecurityManager.CurrentUserIdentityName);

				//save as new Configuration Service Group Selector
				ConfigurationServiceGroupSelectorCollection colConfigurationServiceGroupSelector = new ConfigurationServiceGroupSelectorCollection();
				foreach (ConfigurationServiceGroupSelector gsOriginalRecord in originalConfigurationServiceGroup.ConfigurationServiceGroupSelectorRecords())
				{
					ConfigurationServiceGroupSelector gsNewRecord = ConfigurationServiceGroupSelector.Copy(gsOriginalRecord);
					gsNewRecord.MarkNew();
					gsNewRecord.ConfigurationServiceGroupId = newConfigurationServiceGroup.Id;
					gsNewRecord.CreatedBy = createdBy;
					gsNewRecord.CreatedOn = createdOn;
					gsNewRecord.ModifiedBy = createdBy;
					gsNewRecord.ModifiedOn = createdOn;
					gsNewRecord.Save(SecurityManager.CurrentUserIdentityName);

					//copy all of the originalConfigurationServiceGroupSelector's ConfigurationServiceGroupSelectorQueryParameterValue data to the newConfigurationServiceGroupSelector
					ConfigurationServiceGroupSelectorQueryParameterValueCollection colConfigurationServiceGroupSelectorQueryParameterValue = new ConfigurationServiceGroupSelectorQueryParameterValueCollection();
					foreach (ConfigurationServiceGroupSelectorQueryParameterValue qpvOriginalRecord in gsOriginalRecord.ConfigurationServiceGroupSelectorQueryParameterValueRecords())
					{
						ConfigurationServiceGroupSelectorQueryParameterValue qpvNewRecord = ConfigurationServiceGroupSelectorQueryParameterValue.Copy(qpvOriginalRecord);
						qpvNewRecord.MarkNew();
						qpvNewRecord.ConfigurationServiceGroupSelectorId = gsNewRecord.Id;
						qpvNewRecord.CreatedBy = createdBy;
						qpvNewRecord.CreatedOn = createdOn;
						qpvNewRecord.ModifiedBy = createdBy;
						qpvNewRecord.ModifiedOn = createdOn;
						colConfigurationServiceGroupSelectorQueryParameterValue.Add(qpvNewRecord);
					}
					colConfigurationServiceGroupSelectorQueryParameterValue.SaveAll(SecurityManager.CurrentUserIdentityName);
				}

				originalConfigurationServiceGroup.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newConfigurationServiceGroup));
				newConfigurationServiceGroup.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalConfigurationServiceGroup));

				scope.Complete(); // transaction complete
			}

			return newConfigurationServiceGroup;
		}

		#endregion

		#region Tag-related Methods

		/// <summary>
		/// Disassociates this ConfigurationServiceGroup from a specific Tag.
		/// </summary>
		/// <param name="tagId"></param>
		public void RemoveTag(int tagId)
		{
			ConfigurationServiceGroupTag.Destroy(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="ConfigurationServiceGroup"/> with a specific <see cref="Tag"/>.
		/// </summary>
		/// <param name="tagId">The ID of an existing <see cref="Tag"/>.</param>
		public ConfigurationServiceGroupTag AddTag(int tagId)
		{
			return ConfigurationServiceGroupTag.Insert(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="ConfigurationServiceGroup"/> with a specific <see cref="Tag"/> (creating a new <see cref="Tag"/> if needed).
		/// </summary>
		/// <param name="tagName">The Name of a <see cref="Tag"/> (either existing or not).</param>
		public ConfigurationServiceGroupTag AddTag(string tagName)
		{
			if (!Tag.IsValidName(tagName))
			{
				throw new ArgumentException("Invalid Tag name.", "tagName");
			}

			Tag tag = TagController.GetOrCreateByName(tagName);
			return this.AddTag(tag.Id);
		}

		/// <summary>
		/// Deletes/removes all <see cref="Tag"/>s associated with this <see cref="ConfigurationServiceGroup"/>.
		/// </summary>
		public void ClearTags()
		{
			ConfigurationServiceGroupTag.DestroyByConfigurationServiceGroupId(this.Id);
			this.Log(Severity.Debug, "ConfigurationServiceGroup tag removed: All tags were deleted.");
		}

		/// <summary>
		/// Replaces all ConfigurationServiceGroupTags currently associated with this <see cref="ConfigurationServiceGroup"/> with the specified set of new <see cref="Tag"/>s.
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
		/// Replaces all ConfigurationServiceGroupTags currently associated with this <see cref="ConfigurationServiceGroup"/> with the specified set of new <see cref="Tag"/>s.
		/// </summary>
		/// <param name="tags">The Names of the new set of <see cref="Tag"/>s.</param>
		public void SetTags(IEnumerable<string> tags)
		{
			tags = ConfigurationServiceGroup.PreprocessTags(tags);

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
		/// Adds additional <see cref="ConfigurationServiceGroupTag"/>s to this <see cref="ConfigurationServiceGroup"/> to associate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will not remove any existing <see cref="ConfigurationServiceGroupTag"/>s.
		/// This method will not create any duplicate <see cref="ConfigurationServiceGroupTag"/>s.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to add.</param>
		public void AddTags(IEnumerable<string> tags)
		{
			tags = ConfigurationServiceGroup.PreprocessTags(tags);

			if (!Tag.AreValidNames(tags))
			{
				throw new ArgumentException("Invalid Tag name(s).", "tags");
			}

			//determine which tags need to be added
			List<string> oldTags = this.TagNames;
			this.AddTagsHelper(tags.Except(oldTags, StringComparer.CurrentCultureIgnoreCase));
		}

		/// <summary>
		/// Removes existing <see cref="ConfigurationServiceGroupTag"/>s from this <see cref="ConfigurationServiceGroup"/> to disassociate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will only remove existing <see cref="ConfigurationServiceGroupTag"/>s.
		/// This method will ignore any items in <paramref name="tags"/> that are not currently associated with this <see cref="ConfigurationServiceGroup"/>.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to remove.</param>
		public void RemoveTags(IEnumerable<string> tags)
		{
			tags = ConfigurationServiceGroup.PreprocessTags(tags);

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
		/// Returns a user-friendly string representation of an ConfigurationServiceGroup.
		/// </summary>
		/// <param name="ConfigurationServiceGroup">The ConfigurationServiceGroup to format.</param>
		/// <returns></returns>
		private static string Format(ConfigurationServiceGroup ConfigurationServiceGroup)
		{
			return string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} ({1:G})", ConfigurationServiceGroup.Id, ConfigurationServiceGroup.ConfigurationServiceGroupState);
		}

		#endregion

		#region ConfigurationServiceGroup Lifecycle Methods

		#region Lifecycle-related Interrogative Methods

		#region IsDataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Configuration Service Group with a specific <see cref="ConfigurationServiceGroupState"/>.
		/// </summary>
		/// <param name="state">The state of the Configuration Service Group that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsDataModificationAllowed(ConfigurationServiceGroupStateId state, ICollection<UserRoleId> requestorRoles)
		{
			switch (state)
			{
				case ConfigurationServiceGroupStateId.ReadyForValidation:
				case ConfigurationServiceGroupStateId.Validated:
				case ConfigurationServiceGroupStateId.Published:
				case ConfigurationServiceGroupStateId.Cancelled:
					//Nobody can edit a Ready For Modification, Validated, Published or Cancelled Configuration Service Group
					return false;
			}

			//A user is allowed to "edit" an Configuration Service Group in this state if the user would be allowed to Abandon such an Configuration Service Group
			return IsStateTransitionAllowed(state, ConfigurationServiceGroupStateId.Abandoned, requestorRoles);
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify the Configuration Service Group.
		/// </summary>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> requestorRoles)
		{
			return IsDataModificationAllowed(this.ConfigurationServiceGroupState, requestorRoles);
		}

		#endregion

		#region IsMetadataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an Configuration Service Group with a specific <see cref="ConfigurationServiceGroupState"/>.
		/// </summary>
		/// <param name="state">The state of the Configuration Service Group that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsMetadataModificationAllowed(ConfigurationServiceGroupStateId state, ICollection<UserRoleId> requestorRoles)
		{
			//NOTE: A specific user is allowed to "edit" an ConfigurationServiceGroup's "metadata" (e.g. tags) if the user is an Editor
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

			return IsMetadataModificationAllowed(this.ConfigurationServiceGroupState, requestorRoles);
		}

		#endregion

		#region IsStateTransitionAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition between two specified <see cref="ConfigurationServiceGroupState"/>s.
		/// </summary>
		/// <param name="fromState">The initial/starting state that the ProxyURL would transition from.</param>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if the specified transition is allowed.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if either <paramref name="fromState"/> or <paramref name="toState"/> is invalid.
		/// </exception>
		public static bool IsStateTransitionAllowed(ConfigurationServiceGroupStateId fromState, ConfigurationServiceGroupStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(fromState, "fromState", typeof(ConfigurationServiceGroupStateId));
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(ConfigurationServiceGroupStateId));

			//a transition must be between 2 distinct states
			if (toState == ConfigurationServiceGroupStateId.None)
			{
				return false;
				//throw new ArgumentException("toState cannot be None", "toState");
			}

			//a transition must be between 2 distinct states
			if ((fromState == toState) && (fromState != ConfigurationServiceGroupStateId.Published))
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
		/// Determines whether a user with a specified set of roles is authorized to execute a transition to a specified <see cref="ConfigurationServiceGroupState"/> 
		/// from the Configuration Service group's current <see cref="ConfigurationServiceGroupState"/>.
		/// </summary>
		/// <param name="toState">The new state that the Configuration Service group would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(ConfigurationServiceGroupStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			if (this.IsDirty)
			{
				return false;
			}


			if (!IsStateTransitionAllowed(this.ConfigurationServiceGroupState, toState, requestorRoles))
			{
				return false;
			}

			switch (toState)
			{
				case ConfigurationServiceGroupStateId.ReadyForValidation:
				case ConfigurationServiceGroupStateId.Validated:
				case ConfigurationServiceGroupStateId.Published:
					//moving to any of these states requires that the Configuration Service group be "publishable"
					if (!this.IsDataPublishable())
					{
						return false;
					}
					break;
				case ConfigurationServiceGroupStateId.Cancelled:
					if (!this.IsDataUnPublishable())
					{
						return false;
					}
					break;
				default:
					//case ConfigurationServiceGroupStateId.None:
					//case ConfigurationServiceGroupStateId.Modified:
					//case ConfigurationServiceGroupStateId.Abandoned:
					//case ConfigurationServiceGroupStateId.Deleted:
					break;
			}

			return true;
		}

		#region IsStateTransitionAllowed Helper Methods

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_StatesPaths(ConfigurationServiceGroupStateId fromState, ConfigurationServiceGroupStateId toState)
		{
			//verify that the current state can transition to the requested state
			switch (toState)
			{
				case SubSonicClient.ConfigurationServiceGroupStateId.Cancelled:
					if (fromState != ConfigurationServiceGroupStateId.Published)
					{
						return false;
					}
					break;
				case SubSonicClient.ConfigurationServiceGroupStateId.Validated:
					if (fromState != ConfigurationServiceGroupStateId.ReadyForValidation)
					{
						return false;
					}
					break;
				case SubSonicClient.ConfigurationServiceGroupStateId.Published:
					if (!((fromState == ConfigurationServiceGroupStateId.Validated) || (fromState == ConfigurationServiceGroupStateId.Published) || (fromState == ConfigurationServiceGroupStateId.Cancelled)))
					{
						return false;
					}
					break;
				case SubSonicClient.ConfigurationServiceGroupStateId.Modified:
					List<ConfigurationServiceGroupStateId> possibleFromStates_Modified = new List<ConfigurationServiceGroupStateId>(
						new[] { ConfigurationServiceGroupStateId.Abandoned, ConfigurationServiceGroupStateId.ReadyForValidation, ConfigurationServiceGroupStateId.Validated, ConfigurationServiceGroupStateId.Cancelled });
					if (!possibleFromStates_Modified.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.ConfigurationServiceGroupStateId.ReadyForValidation:
					List<ConfigurationServiceGroupStateId> possibleFromStates_ReadyForValidation = new List<ConfigurationServiceGroupStateId>(
						new[] { ConfigurationServiceGroupStateId.Abandoned, ConfigurationServiceGroupStateId.Modified });
					if (!possibleFromStates_ReadyForValidation.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.ConfigurationServiceGroupStateId.Abandoned:
					List<ConfigurationServiceGroupStateId> possibleFromStates_Abandoned = new List<ConfigurationServiceGroupStateId>(
						new[] { ConfigurationServiceGroupStateId.Modified, ConfigurationServiceGroupStateId.ReadyForValidation });
					if (!possibleFromStates_Abandoned.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.ConfigurationServiceGroupStateId.Deleted:
					if (fromState != ConfigurationServiceGroupStateId.Abandoned)
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
		private static bool IsStateTransitionAllowedHelper_SpecialStates(ConfigurationServiceGroupStateId fromState, ConfigurationServiceGroupStateId toState)
		{
			//the None state is a virtual start state, and may only proceed to the Modified state
			if (fromState == ConfigurationServiceGroupStateId.None)
			{
				return (toState == ConfigurationServiceGroupStateId.Modified);
			}
			return true;
		}

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_RoleBasedRules(ConfigurationServiceGroupStateId fromState, ConfigurationServiceGroupStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			//First, enforce the "simple" transition rules requiring knowledge of only roles and fromState
			switch (fromState)
			{
				case ConfigurationServiceGroupStateId.Validated:
				case ConfigurationServiceGroupStateId.Published:
				case ConfigurationServiceGroupStateId.Cancelled:
					//All transitions from Validated or Published (to any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case ConfigurationServiceGroupStateId.ReadyForValidation:
					//All transitions from ReadyForValidation (to any other state) require the Validator role
                    if (!requestorRoles.Contains(UserRoleId.Validator))
					{
						return false;
					}
					break;
				case ConfigurationServiceGroupStateId.Modified:
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
				case ConfigurationServiceGroupStateId.Cancelled:
				case ConfigurationServiceGroupStateId.Published:
					//All transitions to Cancelled or Published (from any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case ConfigurationServiceGroupStateId.Validated:
					//All transitions to Validated (from any other state) require the Validator role
                    if (!requestorRoles.Contains(UserRoleId.Validator))
					{
						return false;
					}
					break;
				default:
					//All transitions to any state not handled above (from any other state) require the Editor role
                    if (!requestorRoles.Contains(UserRoleId.Editor) )
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
		/// Determines whether a user with a specified set of roles would be authorized to execute a transition to a specified <see cref="ConfigurationServiceGroupState"/> 
		/// (from any other possible state).
		/// </summary>
		/// <param name="toState">The new state that the ConfigurationServiceGroup would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if any potential transition is possible.</returns>
		public static bool IsStateTransitionPossible(ConfigurationServiceGroupStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			Array allStates = Enum.GetValues(typeof(ConfigurationServiceGroupStateId));
			foreach (ConfigurationServiceGroupStateId fromState in allStates)
			{
				if (IsStateTransitionAllowed(fromState, toState, requestorRoles))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Indicates whether the ConfigurationServiceGroup's data is complete and valid (i.e. could be published "as is").
		/// </summary>
		/// <returns></returns>
		public bool IsDataPublishable()
		{
			// Validate that all "required" fields are valid
			// One group selector is required.
			ConfigurationServiceGroupSelectorQuerySpecification configurationServiceGroupSelectorQuerySpecification = new ConfigurationServiceGroupSelectorQuerySpecification() { ConfigurationServiceGroupId = this.Id };
			if (ConfigurationServiceGroupSelectorController.FetchCount(configurationServiceGroupSelectorQuerySpecification) < 1)
			{
				return false;
			}

			switch (this.ConfigurationServiceGroupState)
			{
				case ConfigurationServiceGroupStateId.Published:
					//Published ConfigurationServiceGroups can only be re-Published if they could also be UnPublished
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
		/// Indicates whether the ConfigurationServiceGroup's data is being replaced (Published ConfigurationServiceGroup has already been copied)
		/// </summary>
		/// <returns></returns>
		public bool IsOriginalConfigurationServiceGroupReplacement()
		{
			if (this.ValidationId == null && this.ProductionId == null)
			{
				// not published so allow.
				return false;
			}
			else
			{
				if ((ConfigurationServiceGroupStateId)this.ConfigurationServiceGroupStatusId != ConfigurationServiceGroupStateId.Published)
				{
					// not original.
					return false;
				}
				else
				{
					// check to see if replacement
					ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification = new ConfigurationServiceGroupQuerySpecification() { ValidationId = this.ValidationId };
                    return ConfigurationServiceGroupController.FetchCount(configurationServiceGroupQuerySpecification) > 1;
				}
			}
		}

		/// <summary>
		/// Indicates whether the ConfigurationServiceGroup's data is complete and valid enough to be UnPublished
		/// (i.e. could be unpublished "as is" ignoring the ConfigurationServiceGroup's State).
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
		/// Validates that the current user is allowed to transition the current ConfigurationService Group to a specified <see cref="ConfigurationServiceGroupState"/>.
		/// </summary>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the state transition is not allowed or the current user is not authorized to initiate it.
		/// </exception>
		private void AuthorizeStateTransition(ConfigurationServiceGroupStateId toState)
		{
			Person currentUser = PersonController.GetCurrentUser();
			List<UserRoleId> currentUserRoles = currentUser.GetRoles();
			if (!this.IsStateTransitionAllowed(toState, currentUserRoles))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid Configuration Service Group State transition (from {0:G} to {1:G}) attempted on {2} by {3}.", this.ConfigurationServiceGroupState, toState, this, currentUser);
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
		private void PerformSimpleStateTransition(ConfigurationServiceGroupStateId toState)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(ConfigurationServiceGroupStateId));

			//validate that the toState does not require a "complex transition"
			switch (toState)
			{
				case ConfigurationServiceGroupStateId.None:
				case ConfigurationServiceGroupStateId.Published:
				case ConfigurationServiceGroupStateId.Cancelled:
					throw new InvalidOperationException(
						string.Format(CultureInfo.CurrentCulture,
							"The PerformSimpleStateTransition() method cannot be used to transition to state {0:G}", toState));
			}

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			ConfigurationServiceGroupStateId fromState = this.ConfigurationServiceGroupState;
			this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "Configuration Service Group state changing: from '{0:G}' to '{1:G}'.", fromState, toState));
			this.ConfigurationServiceGroupState = toState;
			this.Save(SecurityManager.CurrentUserIdentityName);
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Configuration Service Group state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
		}

		#endregion

		#region GoToState... Methods

		/// <summary>
		/// Transitions the instance to a specified state.
		/// </summary>
		/// <param name="toState">The state to transition the instance to.</param>
		public void GoToState(ConfigurationServiceGroupStateId toState)
		{
			//TODO: Refactor: Refactor this implementation to a standard BaseLifecycle implementation
#warning Refactor: Refactor this implementation to a standard BaseLifecycle implementation

			//ConfigurationServiceGroupLifecycle.Current.GoToState(this, toState);
			GoToState(this, toState, false);
		}

		private static void GoToState(ConfigurationServiceGroup instance, ConfigurationServiceGroupStateId toState, bool isAutotransition)
		{
			switch (toState)
			{
				case ConfigurationServiceGroupStateId.Modified:
					instance.SubmitBackToEditor();
					break;
				case ConfigurationServiceGroupStateId.Validated:
					instance.Validate();
					break;
				case ConfigurationServiceGroupStateId.Published:
					instance.Publish();
					break;
				case ConfigurationServiceGroupStateId.Cancelled:
					instance.UnPublish();
					break;
				case ConfigurationServiceGroupStateId.Deleted:
					instance.Delete();
					break;
				case ConfigurationServiceGroupStateId.Abandoned:
				case ConfigurationServiceGroupStateId.ReadyForValidation:
					instance.PerformSimpleStateTransition(toState);
					break;
				case ConfigurationServiceGroupStateId.None:
				case ConfigurationServiceGroupStateId.PublishedProductionOnly:
					throw new InvalidOperationException(string.Format("Explicit transition to state {0:G} is not supported.", toState));
				default:
					throw new ArgumentOutOfRangeException("toState");
			}
		}

		#endregion

		/// <summary>
		/// Changes the ConfigurationServiceGroup's <see cref="ConfigurationServiceGroupState"/> to <see cref="ConfigurationServiceGroupStateId.Abandoned"/>.
		/// </summary>
		public void Abandon()
		{
			this.GoToState(ConfigurationServiceGroupStateId.Abandoned); //this.PerformSimpleStateTransition(ConfigurationServiceGroupStateId.Abandoned);
		}

		/// <summary>
		/// Delete the ConfigurationServiceGroup
		/// </summary>
		public void Delete()
		{
			// can only delete ConfigurationServiceGroups that are abandoned.
			if (this.ConfigurationServiceGroupState == ConfigurationServiceGroupStateId.Abandoned)
			{
				try
				{
					this.ClearTags();
					this.ClearConfigurationServiceGroupSelector();
					this.ClearConfigurationServiceLabelValue();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture,
												   "Unable to delete ConfigurationServiceGroupId #{0}.",
												   this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
			}
		}

		/// <summary>
		/// Deletes a specified ConfigurationServiceGroup record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = ConfigurationServiceGroup.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			ConfigurationServiceGroupController.DestroyByQuery(query);
		}

		/// <summary>
		/// Changes the ConfigurationServiceGroup's <see cref="ConfigurationServiceGroupState"/> to <see cref="ConfigurationServiceGroupStateId.Published"/>.
		/// </summary>
		public void Publish()
		{
			ConfigurationServiceGroupStateId toState = ConfigurationServiceGroupStateId.Published;
			int? ProductionId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				ConfigurationServiceGroupStateId fromState = this.ConfigurationServiceGroupState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing configuration service group on the Elements server
				if (this.ProductionId != null)
				{
					ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuery = new ConfigurationServiceGroupQuerySpecification() { ProductionId = this.ProductionId };
					ConfigurationServiceGroupCollection configurationServiceGroupColl = ConfigurationServiceGroupController.Fetch(configurationServiceGroupQuery);
					foreach (ConfigurationServiceGroup configurationServiceGroup in configurationServiceGroupColl)
					{
						// skip existing one
						if (configurationServiceGroup.Id != this.Id)
						{
							configurationServiceGroup.ConfigurationServiceGroupState = ConfigurationServiceGroupStateId.Abandoned;
							configurationServiceGroup.ValidationId = null;
							configurationServiceGroup.ProductionId = null;
							configurationServiceGroup.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//  Publish to Elements/CMService here!
				this.ProductionId = ElementsPublisher.Instance.PublishConfigGroup(this, HP.ElementsCPS.Data.CmService.Environment.Publication);
				this.ConfigurationServiceGroupState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} was successfully published to Elements as Elements ConfigurationServiceGroup #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ProductionId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the ConfigurationServiceGroup's <see cref="ConfigurationServiceGroupState"/> to <see cref="ConfigurationServiceGroupStateId.Cancelled"/>.
		/// </summary>
		public void UnPublish()
		{
			const ConfigurationServiceGroupStateId toState = ConfigurationServiceGroupStateId.Cancelled;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			ConfigurationServiceGroupStateId fromState = this.ConfigurationServiceGroupState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				// GADSC unpublish
				ElementsPublisher.Instance.UnPublishConfigGroup(this, HP.ElementsCPS.Data.CmService.Environment.Publication);

				this.ProductionId = null;
				this.ConfigurationServiceGroupState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} was not un-published from Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} was successfully un-published from Elements, but the system was unable to complete the UnPublish operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the ConfigurationServiceGroup's <see cref="ConfigurationServiceGroupState"/> to <see cref="ConfigurationServiceGroupStateId.Modified"/>.
		/// </summary>
		public void SubmitBackToEditor()
		{
			const ConfigurationServiceGroupStateId toState = ConfigurationServiceGroupStateId.Modified;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			ConfigurationServiceGroupStateId fromState = this.ConfigurationServiceGroupState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				if ((fromState == ConfigurationServiceGroupStateId.Validated) || (fromState == ConfigurationServiceGroupStateId.Cancelled))
				{
					// Unpublish from validation server here.
					ElementsPublisher.Instance.UnPublishConfigGroup(this, HP.ElementsCPS.Data.CmService.Environment.Validation);

					// if this is a replacement, then re-validate original configuration service group
					if ((this.ValidationId != null) && (this.ProductionId != null))
					{
						ConfigurationServiceGroupQuerySpecification ConfigurationServiceGroupQuery = new ConfigurationServiceGroupQuerySpecification()
																	{
																		ValidationId = this.ValidationId,
																		ProductionId = this.ProductionId,
																		ConfigurationServiceGroupStatusId = (int?)ConfigurationServiceGroupStateId.PublishedProductionOnly
																	};
						ConfigurationServiceGroupCollection configurationServiceGroupColl = ConfigurationServiceGroupController.Fetch(ConfigurationServiceGroupQuery);
						foreach (ConfigurationServiceGroup configurationServiceGroup in configurationServiceGroupColl)
						{
							//
							//  GADSC -> You will re-validate to Elements/CMService here!
							//
							configurationServiceGroup.ValidationId = ElementsPublisher.Instance.PublishConfigGroup(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
							configurationServiceGroup.ConfigurationServiceGroupStatusId = (int)ConfigurationServiceGroupStateId.Published;
							configurationServiceGroup.Save(SecurityManager.CurrentUserIdentityName);
						}
						// replacement so remove production id too
						this.ProductionId = null;
					}
					this.ValidationId = null;
				}
				this.ConfigurationServiceGroupState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} was not un-published from the Validation Server.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} was successfully un-published from the Validation Server, but the system was unable to complete the operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the ConfigurationServiceGroup's <see cref="ConfigurationServiceGroupState"/> to <see cref="ConfigurationServiceGroupStateId.ReadyForValidation"/>.
		/// </summary>
		public void SubmitToValidator()
		{
			this.GoToState(ConfigurationServiceGroupStateId.ReadyForValidation); //this.PerformSimpleStateTransition(ConfigurationServiceGroupStateId.ReadyForValidation);
		}

		/// <summary>
		/// Changes the ConfigurationServiceGroup's <see cref="ConfigurationServiceGroupState"/> to <see cref="ConfigurationServiceGroupStateId.Validated"/>.
		/// </summary>
		public void Validate()
		{
			ConfigurationServiceGroupStateId toState = ConfigurationServiceGroupStateId.Validated;
			int? ValidationId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				ConfigurationServiceGroupStateId fromState = this.ConfigurationServiceGroupState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup state changing: from '{0:G}' to '{1:G}'.", fromState, toState));


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
					ConfigurationServiceGroupQuerySpecification ConfigurationServiceGroupQuery = new ConfigurationServiceGroupQuerySpecification() { ValidationId = this.ValidationId };
					ConfigurationServiceGroupCollection ConfigurationServiceGroupColl = ConfigurationServiceGroupController.Fetch(ConfigurationServiceGroupQuery);
					foreach (ConfigurationServiceGroup ConfigurationServiceGroup in ConfigurationServiceGroupColl)
					{
						// skip existing one
						if (ConfigurationServiceGroup.Id != this.Id)
						{
							switch (ConfigurationServiceGroup.ConfigurationServiceGroupStatusId)
							{
								case (int)ConfigurationServiceGroupStateId.Published:
									ConfigurationServiceGroup.ConfigurationServiceGroupStatusId = (int)ConfigurationServiceGroupStateId.PublishedProductionOnly;
									break;
								case (int)ConfigurationServiceGroupStateId.Validated:
									ConfigurationServiceGroup.ConfigurationServiceGroupStatusId = (int)ConfigurationServiceGroupStateId.Modified;
									break;
								default:
									break;
							}
							ConfigurationServiceGroup.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//
				//  GADSC -> You will publish to Elements/CMService validation here!
				//
				//  Note - If ValidationId exists, then publish using the same ValidationId (i.e. replace the existing one).
				//
				this.ValidationId = ElementsPublisher.Instance.PublishConfigGroup(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
				this.ConfigurationServiceGroupState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroup #{0} was successfully published to Elements as Elements ConfigurationServiceGroup #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ValidationId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		#endregion

		#endregion

		#region ConfigurationServiceGroupSelector-related convenience members

		/// <summary>
		/// Removes all associated configuration service Group selectors
		/// </summary>
		public void ClearConfigurationServiceGroupSelector()
		{
			ConfigurationServiceGroupSelector.DestroyByConfigurationServiceGroupId(this.Id);
		}

        /// <summary>
        /// Indicates whether the ConfigurationServiceGroup's is attached to any existing group selectors.
        /// </summary>
        /// <returns></returns>
        public bool HasGroupSelector()
        {
            return (VwMapConfigurationServiceGroupSelectorController.FetchCountByConfigurationServiceGroupId(this.Id) > 0);
        }

        /// <summary>
        /// Add the ConfigurationServiceGroupSelector wildcard default.
        /// </summary>
        public void AddConfigurationServiceGroupSelectorWildcardDefault()
        {
            string defaultQueryParameterValueName = ConfigurationManager.AppSettings["HP.ElementsCPS.WebUI.DefaultValue.GroupSelector.QueryParameterValueName"];
            if (String.IsNullOrEmpty(defaultQueryParameterValueName))
            {
                // there are no query parameter value default configured ("*")
                return;
            }

            // all query parameters must allow all wildcards to add default group selector 
            QueryParameterConfigurationServiceGroupTypeQuerySpecification queryParameterConfigurationServiceGroupTypeQuerySpecification =
                new QueryParameterConfigurationServiceGroupTypeQuerySpecification() { ConfigurationServiceGroupTypeId = this.ConfigurationServiceGroupTypeId, Wildcard = false };
            VwMapQueryParameterConfigurationServiceGroupTypeCollection queryParameterGroupTypeCollection = VwMapQueryParameterConfigurationServiceGroupTypeController.Fetch(queryParameterConfigurationServiceGroupTypeQuerySpecification); 
            if (queryParameterGroupTypeCollection.Count > 0)
            {
                // all query parameters must allow wildcards to create a default group selector.
                return;
            }

            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                //Save Group Selector
                ConfigurationServiceGroupSelector saveConfigurationServiceGroupItem = new ConfigurationServiceGroupSelector(true);
                string defaultName = ConfigurationManager.AppSettings["HP.ElementsCPS.WebUI.DefaultValue.GroupSelector.Name"];
                saveConfigurationServiceGroupItem.Name = String.IsNullOrEmpty(defaultName) ? "Default" : defaultName;
                string defaultDescription = ConfigurationManager.AppSettings["HP.ElementsCPS.WebUI.DefaultValue.GroupSelector.Description"];
                saveConfigurationServiceGroupItem.Description = String.IsNullOrEmpty(defaultDescription) ? "" : defaultDescription;
                saveConfigurationServiceGroupItem.ConfigurationServiceGroupId = this.Id;
                saveConfigurationServiceGroupItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                //Save the default query parameter values (wildcard)
                VwMapQueryParameterConfigurationServiceGroupTypeCollection queryParameterCollection = VwMapQueryParameterConfigurationServiceGroupTypeController.FetchByConfigurationServiceGroupTypeId(this.ConfigurationServiceGroupTypeId, (int?)RowStatus.RowStatusId.Active);
                foreach (VwMapQueryParameterConfigurationServiceGroupType queryParameter in queryParameterCollection)
                {
                    QueryParameterValueQuerySpecification queryParameterValueQuerySpecification =
                        new QueryParameterValueQuerySpecification() { QueryParameterId = queryParameter.QueryParameterId, QueryParameterValueName = defaultQueryParameterValueName, Wildcard = true };
                    VwMapQueryParameterValueCollection queryParameterValueCollection = VwMapQueryParameterValueController.Fetch(queryParameterValueQuerySpecification);

                    // you can have only one value with "*" for each query parameter id
                    if (queryParameterValueCollection.Count > 0)
                    {
                        //Save query parameter value for the group selector
                        ConfigurationServiceGroupSelectorQueryParameterValue saveQueryParameterValueItem = new ConfigurationServiceGroupSelectorQueryParameterValue(true);
                        saveQueryParameterValueItem.QueryParameterValueId = queryParameterValueCollection[0].Id;
                        saveQueryParameterValueItem.ConfigurationServiceGroupSelectorId = saveConfigurationServiceGroupItem.Id;
                        saveQueryParameterValueItem.Negation = false;
                        saveQueryParameterValueItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                    }
                }
                scope.Complete(); // transaction complete
            }
        }

		#endregion

		#region ConfigurationServiceLabelValue-related convenience members

		/// <summary>
		/// Removes all associated configuration service Group selector label values
		/// </summary>
		public void ClearConfigurationServiceLabelValue()
		{
			ConfigurationServiceLabelValue.DestroyByConfigurationServiceGroupId(this.Id);
		}

		#endregion

	}
}
