using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the JumpstationGroup Collection class.
	/// </summary>
	public partial class JumpstationGroupCollection
	{
		public List<int> GetIds()
		{
			List<int> JumpstationGroups = new List<int>(this.Count);
			foreach (JumpstationGroup JumpstationGroup in this)
			{
				JumpstationGroups.Add(JumpstationGroup.Id);
			}
			return JumpstationGroups;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns a new collection of the <see cref="JumpstationGroup"/>s created as a result of invoking the <see cref="JumpstationGroup.SaveAsNew(bool)"/> method on each of the JumpstationGroups in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <returns></returns>
		public JumpstationGroupCollection SaveAllAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAllAsNew(assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Returns a new collection of the <see cref="JumpstationGroup"/>s created as a result of invoking the <see cref="JumpstationGroup.SaveAsNew(bool)"/> method on each of the JumpstationGroups in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <param name="copyElementsId"></param>
		/// <returns></returns>
		public JumpstationGroupCollection SaveAllAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			JumpstationGroupCollection newJumpstationGroups;
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newJumpstationGroups = new JumpstationGroupCollection();
				foreach (JumpstationGroup JumpstationGroup in this)
				{
					newJumpstationGroups.Add(JumpstationGroup.SaveAsNew(assignCurrentUserAsOwner, copyElementsId));
				}

				scope.Complete(); // transaction complete
			}

			return newJumpstationGroups;
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationGroup.Abandon"/> method on each of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		public void Abandon()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (JumpstationGroup JumpstationGroup in this)
				{
					JumpstationGroup.Abandon();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationGroup.SubmitToValidator"/> method on each of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		public void SubmitToValidator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (JumpstationGroup JumpstationGroup in this)
				{
					JumpstationGroup.SubmitToValidator();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationGroup.Validate"/> method on each of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		public void SubmitToCoordinator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (JumpstationGroup JumpstationGroup in this)
				{
					JumpstationGroup.Validate();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationGroup.Publish"/> method on each of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		public void Publish()
		{
			foreach (JumpstationGroup JumpstationGroup in this)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					JumpstationGroup.Publish();

					scope.Complete(); // transaction complete
				}
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationGroup.UnPublish"/> method on each of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		public void UnPublish()
		{
			//using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())			
			{
				foreach (JumpstationGroup JumpstationGroup in this)
				{
					JumpstationGroup.UnPublish();

				}
					scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationGroup.SubmitBackToEditor"/> method on each of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		public void SubmitBackToEditor()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (JumpstationGroup JumpstationGroup in this)
				{
					JumpstationGroup.SubmitBackToEditor();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="JumpstationGroup.IsDataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (JumpstationGroup JumpstationGroup in this)
			{
				if (!JumpstationGroup.IsDataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="JumpstationGroup.IsMetadataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsMetadataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (JumpstationGroup JumpstationGroup in this)
			{
				if (!JumpstationGroup.IsMetadataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="JumpstationGroup.IsStateTransitionAllowed(JumpstationGroupStateId,ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="JumpstationGroup"/>s in this collection.
		/// </summary>
		/// <param name="toState"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(JumpstationGroupStateId toState, ICollection<UserRoleId> roles)
		{
			foreach (JumpstationGroup JumpstationGroup in this)
			{
				if (!JumpstationGroup.IsStateTransitionAllowed(toState, roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="JumpstationGroup"/>s in this collection have an identical
		/// JumpstationGroup Type. <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsIdenticalJumpstationGroupType()
		{
			bool firstJumpstationGroup = true;
			int JumpstationGroupTypeIdSave = 0;
			foreach (JumpstationGroup JumpstationGroup in this)
			{
				if (firstJumpstationGroup == true)
				{
					JumpstationGroupTypeIdSave = JumpstationGroup.JumpstationGroupTypeId;
					firstJumpstationGroup = false;
				}
				else
				{
					if (JumpstationGroup.JumpstationGroupTypeId != JumpstationGroupTypeIdSave)
					{
						return false;
					}
				}
			}
			return true;
		}

		#endregion

	}
}