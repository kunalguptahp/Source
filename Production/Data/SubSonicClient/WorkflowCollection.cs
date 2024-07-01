using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the Workflow Collection class.
	/// </summary>
	public partial class WorkflowCollection
	{
		public List<int> GetIds()
		{
			List<int> Workflows = new List<int>(this.Count);
			foreach (Workflow Workflow in this)
			{
				Workflows.Add(Workflow.Id);
			}
			return Workflows;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns a new collection of the <see cref="Workflow"/>s created as a result of invoking the <see cref="Workflow.SaveAsNew(bool)"/> method on each of the Workflows in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <returns></returns>
		public WorkflowCollection SaveAllAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAllAsNew(assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Returns a new collection of the <see cref="Workflow"/>s created as a result of invoking the <see cref="Workflow.SaveAsNew(bool)"/> method on each of the Workflows in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <param name="copyElementsId"></param>
		/// <returns></returns>
		public WorkflowCollection SaveAllAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			WorkflowCollection newWorkflows;
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newWorkflows = new WorkflowCollection();
				foreach (Workflow Workflow in this)
				{
					newWorkflows.Add(Workflow.SaveAsNew(assignCurrentUserAsOwner, copyElementsId));
				}

				scope.Complete(); // transaction complete
			}

			return newWorkflows;
		}

		/// <summary>
		/// Invokes the <see cref="Workflow.Abandon"/> method on each of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		public void Abandon()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (Workflow Workflow in this)
				{
					Workflow.Abandon();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="Workflow.SubmitToValidator"/> method on each of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		public void SubmitToValidator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (Workflow Workflow in this)
				{
					Workflow.SubmitToValidator();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="Workflow.Validate"/> method on each of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		public void SubmitToCoordinator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (Workflow Workflow in this)
				{
					Workflow.Validate();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="Workflow.Publish"/> method on each of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		public void Publish()
		{
			foreach (Workflow Workflow in this)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					Workflow.Publish();

					scope.Complete(); // transaction complete
				}
			}
		}

		/// <summary>
		/// Invokes the <see cref="Workflow.UnPublish"/> method on each of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		public void UnPublish()
		{
			//using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())			
			{
				foreach (Workflow Workflow in this)
				{
					Workflow.UnPublish();

				}
					scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="Workflow.SubmitBackToEditor"/> method on each of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		public void SubmitBackToEditor()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (Workflow Workflow in this)
				{
					Workflow.SubmitBackToEditor();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="Workflow.IsDataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (Workflow Workflow in this)
			{
				if (!Workflow.IsDataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="Workflow.IsMetadataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsMetadataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (Workflow Workflow in this)
			{
				if (!Workflow.IsMetadataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="Workflow.IsStateTransitionAllowed(WorkflowStateId,ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="Workflow"/>s in this collection.
		/// </summary>
		/// <param name="toState"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(WorkflowStateId toState, ICollection<UserRoleId> roles)
		{
			foreach (Workflow Workflow in this)
			{
				if (!Workflow.IsStateTransitionAllowed(toState, roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="Workflow"/>s in this collection have an identical
		/// Workflow Type. <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsIdenticalWorkflowType()
		{
			bool firstWorkflow = true;
			int WorkflowTypeIdSave = 0;
			foreach (Workflow Workflow in this)
			{
				if (firstWorkflow == true)
				{
					WorkflowTypeIdSave = Workflow.WorkflowTypeId;
					firstWorkflow = false;
				}
				else
				{
					if (Workflow.WorkflowTypeId != WorkflowTypeIdSave)
					{
						return false;
					}
				}
			}
			return true;
		}

		///// <summary>
		///// Returns <c>true</c> if for all of the <see cref="Workflow"/>s in this collection have no label values.
		///// <c>false</c> otherwise.
		///// </summary>
		///// <returns></returns>
		//public bool IsChildlessLabelValue()
		//{
		//    foreach (Workflow Workflow in this)
		//    {
		//        Query qry = new Query(ConfigurationServiceLabelValue.Schema);
		//        qry.AddWhere(ConfigurationServiceLabelValue.Columns.WorkflowId, Workflow.Id);
		//        if (qry.GetCount("CreatedBy") > 0)
		//        {
		//            return false;
		//        }
		//    }
		//    return true;
		//}

		#endregion

	}
}