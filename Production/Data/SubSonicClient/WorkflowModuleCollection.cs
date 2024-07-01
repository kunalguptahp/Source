using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the Workflow Module Collection class.
	/// </summary>
	public partial class WorkflowModuleCollection
	{
		public List<int> GetIds()
		{
			List<int> modules = new List<int>(this.Count);
			foreach (WorkflowModule module in this)
			{
				modules.Add(module.Id);
			}
			return modules;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns a new collection of the <see cref="WorkflowModule"/>s created as a result of invoking the <see cref="WorkflowModule.SaveAsNew(bool)"/> method on each of the Workflow Modules in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <returns></returns>
		public WorkflowModuleCollection SaveAllAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAllAsNew(assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Returns a new collection of the <see cref="WorkflowModule"/>s created as a result of invoking the <see cref="WorkflowModule.SaveAsNew(bool)"/> method on each of the Workflow Modules in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <param name="copyElementsId"></param>
		/// <returns></returns>
		public WorkflowModuleCollection SaveAllAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			WorkflowModuleCollection newModules;
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newModules = new WorkflowModuleCollection();
				foreach (WorkflowModule module in this)
				{
					newModules.Add(module.SaveAsNew(assignCurrentUserAsOwner, copyElementsId));
				}

				scope.Complete(); // transaction complete
			}

			return newModules;
		}

		/// <summary>
		/// Invokes the <see cref="WorkflowModule.Abandon"/> method on each of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		public void Abandon()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (WorkflowModule module in this)
				{
					module.Abandon();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="WorkflowModule.SubmitToValidator"/> method on each of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		public void SubmitToValidator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (WorkflowModule module in this)
				{
					module.SubmitToValidator();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="WorkflowModule.Validate"/> method on each of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		public void SubmitToCoordinator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (WorkflowModule module in this)
				{
					module.Validate();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="WorkflowModule.Publish"/> method on each of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		public void Publish()
		{
			foreach (WorkflowModule module in this)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					module.Publish();

					scope.Complete(); // transaction complete
				}
			}
		}

		/// <summary>
		/// Invokes the <see cref="WorkflowModule.UnPublish"/> method on each of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		public void UnPublish()
		{
			//using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())			
			{
				foreach (WorkflowModule module in this)
				{
					module.UnPublish();
				}
				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="WorkflowModule.SubmitBackToEditor"/> method on each of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		public void SubmitBackToEditor()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (WorkflowModule module in this)
				{
					module.SubmitBackToEditor();
				}
				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="WorkflowModule.IsDataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (WorkflowModule module in this)
			{
				if (!module.IsDataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="WorkflowModule.IsMetadataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsMetadataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (WorkflowModule module in this)
			{
				if (!module.IsMetadataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="WorkflowModule.IsStateTransitionAllowed(WorkflowStateId,ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="WorkflowModule"/>s in this collection.
		/// </summary>
		/// <param name="toState"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(WorkflowStateId toState, ICollection<UserRoleId> roles)
		{
			foreach (WorkflowModule module in this)
			{
				if (!module.IsStateTransitionAllowed(toState, roles))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

	}
}