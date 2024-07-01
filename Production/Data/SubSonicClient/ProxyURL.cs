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
	/// The non-generated portion of the ProxyURL class.
	/// </summary>
	public partial class ProxyURL : IStatefulEntity<ProxyURLStateId>
	{

		#region Implementation of IStatefulEntity<ProxyURLStateId>

		/// <summary>
		/// The entity's current lifecycle state within the lifecycle defined for the entity.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="ProxyURLStateId"/> value is passed to the property.
		/// </exception>
		public ProxyURLStateId CurrentState
		{
			get
			{
				return (ProxyURLStateId)this.ProxyURLStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "CurrentState", typeof(ProxyURLStateId));
				this.ProxyURLStatusId = (int)value;
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
		/// Strongly-typed convenience wrapper for the proxyURLStatusId property.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if an invalid <see cref="ProxyURLStateId"/> value is passed to the property.
		/// </exception>
		public ProxyURLStateId ProxyURLState
		{
			get
			{
				return (ProxyURLStateId)this.ProxyURLStatusId;
			}
			set
			{
				ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(value, "Status", typeof(ProxyURLStateId));
				this.ProxyURLStatusId = (int)value;
			}
		}

		/// <summary>
		/// Returns a list of the names of all the Tags associated with this proxyURL.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and proxyURLTag data is used to create the list of Tag names.
		/// </remarks>
		public List<string> TagNames
		{
			get
			{
				return ProxyURLController.GetTagNameList(this.Id, RowStatus.RowStatusId.Active);
			}
		}

		/// <summary>
		/// Returns a list of the IDs of all the Tags associated with this proxyURL.
		/// </summary>
		/// <remarks>
		/// NOTE: Only Active Tag and proxyURLTag data is used to create the list of Tag names.
		/// </remarks>
		public List<int> TagIds
		{
			get
			{
				return VwMapProxyURLTagTagController.CreateTagIdList(VwMapProxyURLTagTagController.GetActiveTagsByProxyURLId(this.Id));
			}
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? proxyURLId = (this.IsNew ? null : (int?)this.Id);
			Log(proxyURLId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? proxyURLId = (this.IsNew ? null : (int?)this.Id);
			Log(proxyURLId, severity, this, message, ex);
		}

		internal static void Log(int? proxyURLId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "proxyURL History: #{0}: {1}.", proxyURLId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? proxyURLId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "proxyURL History: #{0}: {1}.", proxyURLId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Creates a new <see cref="ProxyURL"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="ProxyURL"/>.</returns>
		public ProxyURL SaveAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Creates a new <see cref="ProxyURL"/> based upon this instance's 
		/// </summary>
		/// <returns>A new (not quite identical) copy of the original <see cref="ProxyURL"/>.</returns>
		public ProxyURL SaveAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			return SaveAsNew(this, assignCurrentUserAsOwner, copyElementsId);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public ProxyURL SaveAsNew()
		{
			return SaveAsNew(this, false, false);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="ProxyURL"/>.
		/// </summary>
		/// <param name="originalProxyURL">The <see cref="ProxyURL"/> to copy/duplicate.</param>
		/// <param name="assignCurrentUserAsOwner">If <c>true</c>, then the new proxyURL will be (re)assigned to the current user; else it will be assigned to the original proxyURL's owner.</param>
		/// <param name="copyElementsId">If <c>true</c>, then new proxyURL will maintain the existing ElementsIds (ValidationId and ProductionId)</param>
		private static ProxyURL SaveAsNew(ProxyURL originalProxyURL, bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			ProxyURL newProxyURL;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newProxyURL = ProxyURL.Copy(originalProxyURL);

				//mark the newProxyURL instance as an unsaved instance
				newProxyURL.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newProxyURL.CreatedBy = createdBy;
				newProxyURL.CreatedOn = createdOn;
				newProxyURL.ModifiedBy = createdBy;
				newProxyURL.ModifiedOn = createdOn;

				//modify the status fields' values appropriately
				newProxyURL.ProxyURLState = ProxyURLStateId.Modified;

				if (!copyElementsId)
				{
					newProxyURL.ValidationId = null;
					newProxyURL.ProductionId = null;
				}

				newProxyURL.RowStatusId = (int)RowStatus.RowStatusId.Active;

				if (assignCurrentUserAsOwner)
				{
					//assign the current user as the Owner of the newProxyURL
					newProxyURL.OwnerId = PersonController.GetCurrentUser().Id;
				}

				//save the newProxyURL to the DB so that it is assigned an ID
				newProxyURL.Save(SecurityManager.CurrentUserIdentityName);

				//copy all of the originalProxyURL's Tags to the newProxyURL
				newProxyURL.SetTags(originalProxyURL.TagIds);

				//copy all of the originalProxyURL's ProxyURLQueryParameterValue data to the newProxyURL
				ProxyURLQueryParameterValueCollection colProxyURLQueryParameterValue = new ProxyURLQueryParameterValueCollection();
				foreach (ProxyURLQueryParameterValue originalRecord in originalProxyURL.ProxyURLQueryParameterValueRecords())
				{
					ProxyURLQueryParameterValue newRecord = ProxyURLQueryParameterValue.Copy(originalRecord);
					newRecord.MarkNew();
					newRecord.ProxyURLId = newProxyURL.Id;
					newRecord.CreatedBy = createdBy;
					newRecord.CreatedOn = createdOn;
					newRecord.ModifiedBy = createdBy;
					newRecord.ModifiedOn = createdOn;
					colProxyURLQueryParameterValue.Add(newRecord);
				}
				colProxyURLQueryParameterValue.SaveAll(SecurityManager.CurrentUserIdentityName);

				originalProxyURL.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newProxyURL));
				newProxyURL.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalProxyURL));

				scope.Complete(); // transaction complete
			}

			return newProxyURL;
		}

		#endregion

		#region Tag-related Methods

		/// <summary>
		/// Disassociates this ProxyURL from a specific Tag.
		/// </summary>
		/// <param name="tagId"></param>
		public void RemoveTag(int tagId)
		{
			ProxyURLTag.Destroy(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="ProxyURL"/> with a specific <see cref="Tag"/>.
		/// </summary>
		/// <param name="tagId">The ID of an existing <see cref="Tag"/>.</param>
		public ProxyURLTag AddTag(int tagId)
		{
			return ProxyURLTag.Insert(this.Id, tagId);
		}

		/// <summary>
		/// Associates this <see cref="ProxyURL"/> with a specific <see cref="Tag"/> (creating a new <see cref="Tag"/> if needed).
		/// </summary>
		/// <param name="tagName">The Name of a <see cref="Tag"/> (either existing or not).</param>
		public ProxyURLTag AddTag(string tagName)
		{
			if (!Tag.IsValidName(tagName))
			{
				throw new ArgumentException("Invalid Tag name.", "tagName");
			}

			Tag tag = TagController.GetOrCreateByName(tagName);
			return this.AddTag(tag.Id);
		}

		/// <summary>
		/// Deletes/removes all <see cref="Tag"/>s associated with this <see cref="ProxyURL"/>.
		/// </summary>
		public void ClearTags()
		{
			ProxyURLTag.DestroyByProxyURLId(this.Id);
			this.Log(Severity.Debug, "ProxyURL tag removed: All tags were deleted.");
		}

		/// <summary>
		/// Replaces all ProxyURLTags currently associated with this <see cref="ProxyURL"/> with the specified set of new <see cref="Tag"/>s.
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
		/// Replaces all ProxyURLTags currently associated with this <see cref="ProxyURL"/> with the specified set of new <see cref="Tag"/>s.
		/// </summary>
		/// <param name="tags">The Names of the new set of <see cref="Tag"/>s.</param>
		public void SetTags(IEnumerable<string> tags)
		{
			tags = ProxyURL.PreprocessTags(tags);

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
					this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "ProxyURL tag removed: '{0}'.", tagName));
				}
			}
		}

		private void AddTagsHelper(IEnumerable<string> tags)
		{
			foreach (string tagName in tags)
			{
				this.AddTag(tagName);
				//this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "ProxyURL tag added: '{0}'.", tagName));
			}
		}

		#endregion

		/// <summary>
		/// Adds additional <see cref="ProxyURLTag"/>s to this <see cref="ProxyURL"/> to associate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will not remove any existing <see cref="ProxyURLTag"/>s.
		/// This method will not create any duplicate <see cref="ProxyURLTag"/>s.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to add.</param>
		public void AddTags(IEnumerable<string> tags)
		{
			tags = ProxyURL.PreprocessTags(tags);

			if (!Tag.AreValidNames(tags))
			{
				throw new ArgumentException("Invalid Tag name(s).", "tags");
			}

			//determine which tags need to be added
			List<string> oldTags = this.TagNames;
			this.AddTagsHelper(tags.Except(oldTags, StringComparer.CurrentCultureIgnoreCase));
		}

		/// <summary>
		/// Removes existing <see cref="ProxyURLTag"/>s from this <see cref="ProxyURL"/> to disassociate it wth the specified set of <see cref="Tag"/>s.
		/// </summary>
		/// <remarks>
		/// This method will only remove existing <see cref="ProxyURLTag"/>s.
		/// This method will ignore any items in <paramref name="tags"/> that are not currently associated with this <see cref="ProxyURL"/>.
		/// </remarks>
		/// <param name="tags">The Names of the set of <see cref="Tag"/>s to remove.</param>
		public void RemoveTags(IEnumerable<string> tags)
		{
			tags = ProxyURL.PreprocessTags(tags);

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
		/// Returns a user-friendly string representation of an ProxyURL.
		/// </summary>
		/// <param name="proxyURL">The ProxyURL to format.</param>
		/// <returns></returns>
		private static string Format(ProxyURL proxyURL)
		{
			return string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} ({1:G})", proxyURL.Id, proxyURL.ProxyURLState);
		}

		#endregion

		#region ProxyURL Lifecycle Methods

		#region Lifecycle-related Interrogative Methods

		#region IsDataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an ProxyURL with a specific <see cref="ProxyURLState"/>.
		/// </summary>
		/// <param name="state">The state of the ProxyURL that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsDataModificationAllowed(ProxyURLStateId state, ICollection<UserRoleId> requestorRoles)
		{
			switch (state)
			{
				case ProxyURLStateId.ReadyForValidation:
				case ProxyURLStateId.Validated:
				case ProxyURLStateId.Published:
				case ProxyURLStateId.PublishedProductionOnly:
				case ProxyURLStateId.Cancelled:
					//Nobody can edit a ReadyForValidation, Validated, Published, Published Production Only or Cancelled proxyURL
					return false;
			}

			//A user is allowed to "edit" an ProxyURL this state if the user would be allowed to Abandon such an ProxyURL
			return IsStateTransitionAllowed(state, ProxyURLStateId.Abandoned, requestorRoles);
		}

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify the ProxyURL.
		/// </summary>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> requestorRoles)
		{
			return IsDataModificationAllowed(this.ProxyURLState, requestorRoles);
		}

		#endregion

		#region IsMetadataModificationAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to modify an ProxyURL with a specific <see cref="ProxyURLState"/>.
		/// </summary>
		/// <param name="state">The state of the ProxyURL that would be edited.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if modification is allowed.</returns>
		public static bool IsMetadataModificationAllowed(ProxyURLStateId state, ICollection<UserRoleId> requestorRoles)
		{
			//NOTE: A specific user is allowed to "edit" an proxyURL's "metadata" (e.g. tags) if the user is an Editor
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
			//TODO: Implement: ProxyURL-instance-specific metadata modification permission logic (if any)
			//#warning Not Implemented: ProxyURL-instance-specific metadata modification permission logic (if any)

			return IsMetadataModificationAllowed(this.ProxyURLState, requestorRoles);
		}

		#endregion

		#region IsStateTransitionAllowed Method

		/// <summary>
		/// Determines whether a user with a specified set of roles is authorized to execute a transition between two specified <see cref="ProxyURLState"/>s.
		/// </summary>
		/// <param name="fromState">The initial/starting state that the ProxyURL would transition from.</param>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if the specified transition is allowed.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if either <paramref name="fromState"/> or <paramref name="toState"/> is invalid.
		/// </exception>
		public static bool IsStateTransitionAllowed(ProxyURLStateId fromState, ProxyURLStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(fromState, "fromState", typeof(ProxyURLStateId));
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(ProxyURLStateId));

			//a transition must be between 2 distinct states
			if (toState == ProxyURLStateId.None)
			{
				return false;
				//throw new ArgumentException("toState cannot be None", "toState");
			}

			//a transition must be between 2 distinct states
			if ((fromState == toState) && (fromState != ProxyURLStateId.Published))
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
		/// Determines whether a user with a specified set of roles is authorized to execute a transition to a specified <see cref="ProxyURLState"/> 
		/// from the ProxyURL's current <see cref="ProxyURLState"/>.
		/// </summary>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(ProxyURLStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			if (this.IsDirty)
			{
				return false;
			}

			if (!IsStateTransitionAllowed(this.ProxyURLState, toState, requestorRoles))
			{
				return false;
			}

			switch (toState)
			{
				case ProxyURLStateId.ReadyForValidation:
				case ProxyURLStateId.Validated:
				case ProxyURLStateId.Published:
					//moving to any of these states requires that the ProxyURL be "publishable"
					if (!this.IsDataPublishable())
					{
						return false;
					}
					break;
				case ProxyURLStateId.Cancelled:
					if (!this.IsDataUnPublishable())
					{
						return false;
					}
					break;
				default:
					//case ProxyURLStateId.None:
					//case ProxyURLStateId.Modified:
					//case ProxyURLStateId.Abandoned:
					//case ProxyURLStateId.Deleted:
					break;
			}

			return true;
		}

		#region IsStateTransitionAllowed Helper Methods

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_StatesPaths(ProxyURLStateId fromState, ProxyURLStateId toState)
		{
			//verify that the current state can transition to the requested state
			switch (toState)
			{
				case SubSonicClient.ProxyURLStateId.Cancelled:
					if (fromState != ProxyURLStateId.Published)
					{
						return false;
					}
					break;
				case SubSonicClient.ProxyURLStateId.Validated:
					if (fromState != ProxyURLStateId.ReadyForValidation)
					{
						return false;
					}
					break;
				case SubSonicClient.ProxyURLStateId.Published:
					if (!((fromState == ProxyURLStateId.Validated) || (fromState == ProxyURLStateId.Published) || (fromState == ProxyURLStateId.Cancelled)))
					{
						return false;
					}
					break;
				case SubSonicClient.ProxyURLStateId.Modified:
					List<ProxyURLStateId> possibleFromStates_Modified = new List<ProxyURLStateId>(
						new[] { ProxyURLStateId.Abandoned, ProxyURLStateId.ReadyForValidation, ProxyURLStateId.Validated, ProxyURLStateId.Cancelled });
					if (!possibleFromStates_Modified.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.ProxyURLStateId.ReadyForValidation:
					List<ProxyURLStateId> possibleFromStates_ReadyForValidation = new List<ProxyURLStateId>(
						new[] { ProxyURLStateId.Abandoned, ProxyURLStateId.Modified });
					if (!possibleFromStates_ReadyForValidation.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.ProxyURLStateId.Abandoned:
					List<ProxyURLStateId> possibleFromStates_Abandoned = new List<ProxyURLStateId>(
						new[] { ProxyURLStateId.Modified, ProxyURLStateId.ReadyForValidation });
					if (!possibleFromStates_Abandoned.Contains(fromState))
					{
						return false;
					}
					break;
				case SubSonicClient.ProxyURLStateId.Deleted:
					if (fromState != ProxyURLStateId.Abandoned)
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
		private static bool IsStateTransitionAllowedHelper_SpecialStates(ProxyURLStateId fromState, ProxyURLStateId toState)
		{
			//the Cancelled state to published state not allowed
			if ((fromState == ProxyURLStateId.Cancelled) && (toState == ProxyURLStateId.Published))
			{
			   return false;
			}

			//the None state is a virtual start state, and may only proceed to the Modified state
			if (fromState == ProxyURLStateId.None)
			{
				return (toState == ProxyURLStateId.Modified);
			}
			return true;
		}

		/// <summary>
		/// Helper method for IsStateTransitionAllowed.
		/// </summary>
		private static bool IsStateTransitionAllowedHelper_RoleBasedRules(ProxyURLStateId fromState, ProxyURLStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			//First, enforce the "simple" transition rules requiring knowledge of only roles and fromState
			switch (fromState)
			{
				case ProxyURLStateId.Validated:
				case ProxyURLStateId.Published:
				case ProxyURLStateId.Cancelled:
					//All transitions from Validated or Published (to any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case ProxyURLStateId.ReadyForValidation:
					//All transitions from ReadyForValidation (to any other state) require the Validator role
					if (!requestorRoles.Contains(UserRoleId.Validator))
					{
						return false;
					}
					break;
				case ProxyURLStateId.Modified:
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
				case ProxyURLStateId.Cancelled:
				case ProxyURLStateId.Published:
					//All transitions to Cancelled or Published (from any other state) require the Coordinator role
					if (!requestorRoles.Contains(UserRoleId.Coordinator))
					{
						return false;
					}
					break;
				case ProxyURLStateId.Validated:
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
		/// Determines whether a user with a specified set of roles would be authorized to execute a transition to a specified <see cref="ProxyURLState"/> 
		/// (from any other possible state).
		/// </summary>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <param name="requestorRoles">The set of roles that should be used to determine whether the state transition is allowed.</param>
		/// <returns><c>true</c> if any potential transition is possible.</returns>
		public static bool IsStateTransitionPossible(ProxyURLStateId toState, ICollection<UserRoleId> requestorRoles)
		{
			Array allStates = Enum.GetValues(typeof(ProxyURLStateId));
			foreach (ProxyURLStateId fromState in allStates)
			{
				if (IsStateTransitionAllowed(fromState, toState, requestorRoles))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Indicates whether the proxyURL's data is complete and valid (i.e. could be published "as is").
		/// </summary>
		/// <returns></returns>
		public bool IsDataPublishable()
		{
			// Validate that all "required" fields are valid
			if (this.ProxyURLTypeId == null)
			{
				return false;
			}

			switch (this.ProxyURLState)
			{
				case ProxyURLStateId.Published:
					//Published proxyURLs can only be re-Published if they could also be UnPublished
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
		/// Indicates whether the proxyURL's data is being replaced (Published ProxyURL has already been copied)
		/// </summary>
		/// <returns></returns>
		public bool IsOriginalProxyURLReplacement()
		{
			if (this.ValidationId == null && this.ProductionId == null)
			{
				// not published so allow.
				return false;
			}
			else
			{
				if ((ProxyURLStateId)this.ProxyURLStatusId != ProxyURLStateId.Published)
				{
					// not original.
					return false;
				}
				else
				{
					// check to see if replacement
					ProxyURLQuerySpecification proxyURLQuerySpecification = new ProxyURLQuerySpecification() { ValidationId = this.ValidationId };
					return ProxyURLController.FetchCount(proxyURLQuerySpecification) > 1;	
				}
			}
		}

		/// <summary>
		/// Indicates whether the proxyURL's data has duplicate query parameter values 
		/// with another published ProxyURL
		/// </summary>
		/// <returns></returns>
		public bool IsQueryParameterValuesDuplicated()
		{
			return ProxyURLController.IsQueryParameterValuesDuplicated(this.Id);
		}

		/// <summary>
		/// Indicates whether the proxyURL's data has duplicate query parameter values 
		/// with another published ProxyURL.  This is utilized for new redirectors or save as new.
		/// </summary>
		/// <returns></returns>
		public bool IsQueryParameterValuesDuplicated(string queryParameterValueIdDelimitedList)
		{
			return ProxyURLController.IsQueryParameterValuesDuplicated(this.Id, queryParameterValueIdDelimitedList);
		}

		/// <summary>
		/// Indicates whether the proxyURL's data is complete and valid enough to be UnPublished
		/// (i.e. could be unpublished "as is" ignoring the proxyURL's State).
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
		/// Validates that the current user is allowed to transition the current ProxyURL to a specified <see cref="ProxyURLState"/>.
		/// </summary>
		/// <param name="toState">The new state that the ProxyURL would transition to.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the state transition is not allowed or the current user is not authorized to initiate it.
		/// </exception>
		private void AuthorizeStateTransition(ProxyURLStateId toState)
		{
			Person currentUser = PersonController.GetCurrentUser();
			List<UserRoleId> currentUserRoles = currentUser.GetRoles();
			if (!this.IsStateTransitionAllowed(toState, currentUserRoles))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Invalid ProxyURL State transition (from {0:G} to {1:G}) attempted on {2} by {3}.", this.ProxyURLState, toState, this, currentUser);
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
		private void PerformSimpleStateTransition(ProxyURLStateId toState)
		{
			ExceptionUtility.ArgumentOutOfRangeEx_ThrowIfInvalidEnumValue(toState, "toState", typeof(ProxyURLStateId));

			//validate that the toState does not require a "complex transition"
			switch (toState)
			{
				case ProxyURLStateId.None:
				case ProxyURLStateId.Published:
				case ProxyURLStateId.Cancelled:
					throw new InvalidOperationException(
						string.Format(CultureInfo.CurrentCulture,
							"The PerformSimpleStateTransition() method cannot be used to transition to state {0:G}", toState));
			}

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			ProxyURLStateId fromState = this.ProxyURLState;
			this.Log(Severity.Debug, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changing: from '{0:G}' to '{1:G}'.", fromState, toState));
			this.ProxyURLState = toState;
			this.Save(SecurityManager.CurrentUserIdentityName);
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
		}

		#endregion

		#region GoToState... Methods

		/// <summary>
		/// Transitions the instance to a specified state.
		/// </summary>
		/// <param name="toState">The state to transition the instance to.</param>
		public void GoToState(ProxyURLStateId toState)
		{
			//TODO: Refactor: Refactor this implementation to a standard BaseLifecycle implementation
#warning Refactor: Refactor this implementation to a standard BaseLifecycle implementation

			//ProxyURLLifecycle.Current.GoToState(this, toState);
			GoToState(this, toState, false);
		}

		private static void GoToState(ProxyURL instance, ProxyURLStateId toState, bool isAutotransition)
		{
			switch (toState)
			{
				case ProxyURLStateId.Modified:
					instance.SubmitBackToEditor();
					break;
				case ProxyURLStateId.Validated:
					instance.Validate();
					break;
				case ProxyURLStateId.Published:
					instance.Publish();
					break;
				case ProxyURLStateId.Cancelled:
					instance.UnPublish();
					break;
				case ProxyURLStateId.Deleted:
					instance.Delete();
					break;
				case ProxyURLStateId.Abandoned:
				case ProxyURLStateId.ReadyForValidation:
					instance.PerformSimpleStateTransition(toState);
					break;
				case ProxyURLStateId.None:
				case ProxyURLStateId.PublishedProductionOnly:
					throw new InvalidOperationException(string.Format("Explicit transition to state {0:G} is not supported.", toState));
				default:
					throw new ArgumentOutOfRangeException("toState");
			}
		}

		#endregion

		/// <summary>
		/// Changes the ProxyURL's <see cref="ProxyURLState"/> to <see cref="ProxyURLStateId.Abandoned"/>.
		/// </summary>
		public void Abandon()
		{
			this.GoToState(ProxyURLStateId.Abandoned); //this.PerformSimpleStateTransition(ProxyURLStateId.Abandoned);
		}


		/// <summary>
		/// Delete the ProxyURL
		/// </summary>
		public void Delete()
		{
			// can only delete proxyURLs that are abandoned.
			if (this.ProxyURLState == ProxyURLStateId.Abandoned)
			{
				try
				{
					this.ClearTags();
					this.ClearQueryParameterValues();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture,
												   "Unable to delete ProxyURLId #{0}.",
												   this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
			}
		}

		/// <summary>
		/// Deletes a specified ProxyURL record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = ProxyURL.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			ProxyURLController.DestroyByQuery(query);
		}


		/// <summary>
		/// Changes the ProxyURL's <see cref="ProxyURLState"/> to <see cref="ProxyURLStateId.Published"/>.
		/// </summary>
		public void Publish()
		{
			ProxyURLStateId toState = ProxyURLStateId.Published;
			int? ProductionId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				ProxyURLStateId fromState = this.ProxyURLState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing redirector on the Elements server
				// this is for multi-replace when replacing the target URLs
				if (this.ProductionId != null)
				{
					ProxyURLQuerySpecification proxyURLQuery = new ProxyURLQuerySpecification() { ProductionId = this.ProductionId };
					ProxyURLCollection proxyURLColl = ProxyURLController.Fetch(proxyURLQuery);
					foreach (ProxyURL proxyURL in proxyURLColl)
					{
						// skip existing one
						if (proxyURL.Id != this.Id)
						{
							proxyURL.ProxyURLState = ProxyURLStateId.Abandoned;
							proxyURL.ValidationId = null;
							proxyURL.ProductionId = null;
							proxyURL.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//  Publish to Elements/CMService here!
				this.ProductionId = ElementsPublisher.Instance.PublishProxyURL(this, HP.ElementsCPS.Data.CmService.Environment.Publication);
				this.ProxyURLState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} was successfully published to Elements as Elements ProxyURL #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, ProductionId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the ProxyURL's <see cref="ProxyURLState"/> to <see cref="ProxyURLStateId.Cancelled"/>.
		/// </summary>
		public void UnPublish()
		{
			const ProxyURLStateId toState = ProxyURLStateId.Cancelled;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			ProxyURLStateId fromState = this.ProxyURLState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				// GADSC unpublish
				ElementsPublisher.Instance.UnPublishProxyURL(this, HP.ElementsCPS.Data.CmService.Environment.Publication);

				this.ProductionId = null;
				this.ProxyURLState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} was not un-published from Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} was successfully un-published from Elements, but the system was unable to complete the UnPublish operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the ProxyURL's <see cref="ProxyURLState"/> to <see cref="ProxyURLStateId.Modified"/>.
		/// </summary>
		public void SubmitBackToEditor()
		{
			const ProxyURLStateId toState = ProxyURLStateId.Modified;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			//perform the transition
			ProxyURLStateId fromState = this.ProxyURLState;
			this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

			try
			{
				if ((fromState == ProxyURLStateId.Validated) || (fromState == ProxyURLStateId.Cancelled))
				{
					// Unpublish from validation server here.
					ElementsPublisher.Instance.UnPublishProxyURL(this, HP.ElementsCPS.Data.CmService.Environment.Validation);

					// if this is a replacement, then re-validate original redirector
					if ((this.ValidationId != null) && (this.ProductionId != null))
					{
						ProxyURLQuerySpecification proxyURLQuery = new ProxyURLQuerySpecification()
							{
								ValidationId = this.ValidationId,
								ProductionId = this.ProductionId,
								ProxyURLStatusId = (int?)ProxyURLStateId.PublishedProductionOnly
							};
						ProxyURLCollection proxyURLColl = ProxyURLController.Fetch(proxyURLQuery);
						foreach (ProxyURL proxyURL in proxyURLColl)
						{
							//
							//  GADSC -> You will re-validate to Elements/CMService here!
							//
							proxyURL.ValidationId = ElementsPublisher.Instance.PublishProxyURL(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
							proxyURL.ProxyURLStatusId = (int)ProxyURLStateId.Published;
							proxyURL.Save(SecurityManager.CurrentUserIdentityName);
						}
						// replacement so remove production id too
						this.ProductionId = null;
					}
					this.ValidationId = null;
				}
				this.ProxyURLState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} was not un-published from the Validation Server.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} was successfully un-published from the Validation Server, but the system was unable to complete the operation and the system may now be out of synch with Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		/// <summary>
		/// Changes the ProxyURL's <see cref="ProxyURLState"/> to <see cref="ProxyURLStateId.ReadyForValidation"/>.
		/// </summary>
		public void SubmitToValidator()
		{
			this.GoToState(ProxyURLStateId.ReadyForValidation); //this.PerformSimpleStateTransition(ProxyURLStateId.ReadyForValidation);
		}

		/// <summary>
		/// Changes the ProxyURL's <see cref="ProxyURLState"/> to <see cref="ProxyURLStateId.Validated"/>.
		/// </summary>
		public void Validate()
		{
			ProxyURLStateId toState = ProxyURLStateId.Validated;
			int? validationId = null;

			//validate the transition
			this.AuthorizeStateTransition(toState);

			try
			{
				//perform the transition
				ProxyURLStateId fromState = this.ProxyURLState;
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changing: from '{0:G}' to '{1:G}'.", fromState, toState));

				// remove any existing redirector on validation server
				// this is for multi-replace when replacing the target URLs
				if (this.ValidationId != null)
				{
					ProxyURLQuerySpecification proxyURLQuery = new ProxyURLQuerySpecification() { ValidationId = this.ValidationId };
					ProxyURLCollection proxyURLColl = ProxyURLController.Fetch(proxyURLQuery);
					foreach (ProxyURL proxyURL in proxyURLColl)
					{
						// skip existing one
						if (proxyURL.Id != this.Id)
						{
							switch (proxyURL.ProxyURLStatusId)
							{
								case (int)ProxyURLStateId.Published:
									proxyURL.ProxyURLStatusId = (int)ProxyURLStateId.PublishedProductionOnly;
									break;
								case (int)ProxyURLStateId.Validated:
									proxyURL.ProxyURLStatusId = (int)ProxyURLStateId.Modified;
									break;
								default:
									break;
							}
							proxyURL.Save(SecurityManager.CurrentUserIdentityName);
						}
					}
				}

				//
				//  GADSC -> You will publish to Elements/CMService validation here!
				//
				//  Note - If validationId exists, then publish using the same validationId (i.e. replace the existing one).
				//
				this.ValidationId = ElementsPublisher.Instance.PublishProxyURL(this, HP.ElementsCPS.Data.CmService.Environment.Validation);
				this.ProxyURLState = toState;
				this.Save(SecurityManager.CurrentUserIdentityName);
				this.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "ProxyURL state changed: from '{0:G}' to '{1:G}'.", fromState, toState));
			}
			catch (PublishException pex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} was not published to Elements.", this.Id);
				LogManager.Current.Log(Severity.Error, this, message, pex);

				throw;
			}
			catch (Exception ex)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "ProxyURL #{0} was successfully published to Elements as Elements ProxyURL #{1}, but the system was unable to complete the Publish operation and the system may now be out of synch with Elements.", this.Id, validationId);
				LogManager.Current.Log(Severity.Error, this, message, ex);

				throw;
			}
		}

		#endregion

		#endregion

		#region QueryParameterValue-related convenience members

		/// <summary>
		/// Removes all associated query parameter values
		/// </summary>
		public void ClearQueryParameterValues()
		{
			if ((this.ProxyURLTypeId == null) || (this.ProxyURLTypeId < 1))
			{
				return; //do nothing
			}
			ProxyURLQueryParameterValue.DestroyByProxyURLId(this.Id);
		}

		public IEnumerable<ProxyURLQueryParameterValue> GetValidQueryParameterValues()
		{
			List<ProxyURLQueryParameterValue> list = new List<ProxyURLQueryParameterValue>();

			var values = this.ProxyURLQueryParameterValueRecords();
			foreach (var v in values)
			{
				var types = v.QueryParameterValue.QueryParameter.QueryParameterProxyURLTypeRecords();
				foreach (var type in types)
				{
					if (type.ProxyURLTypeId == this.ProxyURLType.Id)
						list.Add(v);
				}
			}
			return list;
		}

		#endregion

	}
}
