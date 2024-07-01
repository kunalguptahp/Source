using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the Jumpstation Macro Collection class.
	/// </summary>
	public partial class JumpstationMacroCollection
	{
		public List<int> GetIds()
		{
			List<int> macros = new List<int>(this.Count);
			foreach (JumpstationMacro macro in this)
			{
				macros.Add(macro.Id);
			}
			return macros;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns a new collection of the <see cref="JumpstationMacro"/>s created as a result of invoking the <see cref="JumpstationMacro.SaveAsNew(bool)"/> method on each of the Workflow Modules in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <returns></returns>
		public JumpstationMacroCollection SaveAllAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAllAsNew(assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Returns a new collection of the <see cref="JumpstationMacro"/>s created as a result of invoking the <see cref="JumpstationMacro.SaveAsNew(bool)"/> method on each of the Workflow Modules in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <param name="copyElementsId"></param>
		/// <returns></returns>
		public JumpstationMacroCollection SaveAllAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			JumpstationMacroCollection newMacros;
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newMacros = new JumpstationMacroCollection();
				foreach (JumpstationMacro macro in this)
				{
					newMacros.Add(macro.SaveAsNew(assignCurrentUserAsOwner, copyElementsId));
				}

				scope.Complete(); // transaction complete
			}

			return newMacros;
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationMacro.Abandon"/> method on each of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		public void Abandon()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (JumpstationMacro macro in this)
				{
					macro.Abandon();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationMacro.SubmitToValidator"/> method on each of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		public void SubmitToValidator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (JumpstationMacro macro in this)
				{
					macro.SubmitToValidator();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationMacro.Validate"/> method on each of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		public void SubmitToCoordinator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (JumpstationMacro macro in this)
				{
					macro.Validate();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationMacro.Publish"/> method on each of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		public void Publish()
		{
			foreach (JumpstationMacro macro in this)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					macro.Publish();

					scope.Complete(); // transaction complete
				}
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationMacro.UnPublish"/> method on each of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		public void UnPublish()
		{
			//using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())			
			{
				foreach (JumpstationMacro macro in this)
				{
					macro.UnPublish();
				}
				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="JumpstationMacro.SubmitBackToEditor"/> method on each of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		public void SubmitBackToEditor()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (JumpstationMacro macro in this)
				{
					macro.SubmitBackToEditor();
				}
				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="JumpstationMacro.IsDataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (JumpstationMacro macro in this)
			{
				if (!macro.IsDataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="JumpstationMacro.IsMetadataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsMetadataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (JumpstationMacro macro in this)
			{
				if (!macro.IsMetadataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="JumpstationMacro.IsStateTransitionAllowed(WorkflowStateId,ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="JumpstationMacro"/>s in this collection.
		/// </summary>
		/// <param name="toState"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(JumpstationMacroStateId toState, ICollection<UserRoleId> roles)
		{
			foreach (JumpstationMacro macro in this)
			{
				if (!macro.IsStateTransitionAllowed(toState, roles))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

	}
}