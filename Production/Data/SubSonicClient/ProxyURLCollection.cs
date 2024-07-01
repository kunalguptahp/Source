using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ProxyURL Collection class.
	/// </summary>
	public partial class ProxyURLCollection
	{
		public List<int> GetIds()
		{
			List<int> proxyURLs = new List<int>(this.Count);
			foreach (ProxyURL proxyURL in this)
			{
				proxyURLs.Add(proxyURL.Id);
			}
			return proxyURLs;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns a new collection of the <see cref="ProxyURL"/>s created as a result of invoking the <see cref="ProxyURL.SaveAsNew(bool)"/> method on each of the ProxyURLs in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <returns></returns>
		public ProxyURLCollection SaveAllAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAllAsNew(assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Returns a new collection of the <see cref="ProxyURL"/>s created as a result of invoking the <see cref="ProxyURL.SaveAsNew(bool)"/> method on each of the ProxyURLs in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <param name="copyElementsId"></param>
		/// <returns></returns>
		public ProxyURLCollection SaveAllAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			ProxyURLCollection newProxyURLs;
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newProxyURLs = new ProxyURLCollection();
				foreach (ProxyURL proxyURL in this)
				{
					newProxyURLs.Add(proxyURL.SaveAsNew(assignCurrentUserAsOwner, copyElementsId));
				}

				scope.Complete(); // transaction complete
			}

			return newProxyURLs;
		}

		/// <summary>
		/// Invokes the <see cref="ProxyURL.Abandon"/> method on each of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		public void Abandon()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ProxyURL proxyURL in this)
				{
					proxyURL.Abandon();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="ProxyURL.SubmitToValidator"/> method on each of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		public void SubmitToValidator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ProxyURL proxyURL in this)
				{
					proxyURL.SubmitToValidator();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="ProxyURL.Validate"/> method on each of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		public void SubmitToCoordinator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ProxyURL proxyURL in this)
				{
					proxyURL.Validate();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="ProxyURL.Publish"/> method on each of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		public void Publish()
		{
			foreach (ProxyURL proxyURL in this)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					proxyURL.Publish();

					scope.Complete(); // transaction complete
				}
			}
		}

		/// <summary>
		/// Invokes the <see cref="ProxyURL.UnPublish"/> method on each of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		public void UnPublish()
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ProxyURL proxyURL in this)
				{
					proxyURL.UnPublish();

				}
					scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="ProxyURL.SubmitBackToEditor"/> method on each of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		public void SubmitBackToEditor()
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ProxyURL proxyURL in this)
				{
					proxyURL.SubmitBackToEditor();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="ProxyURL.IsDataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (ProxyURL proxyURL in this)
			{
				if (!proxyURL.IsDataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="ProxyURL.IsMetadataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsMetadataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (ProxyURL proxyURL in this)
			{
				if (!proxyURL.IsMetadataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="ProxyURL.IsStateTransitionAllowed(ProxyURLStateId,ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="ProxyURL"/>s in this collection.
		/// </summary>
		/// <param name="toState"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(ProxyURLStateId toState, ICollection<UserRoleId> roles)
		{
			foreach (ProxyURL proxyURL in this)
			{
				if (!proxyURL.IsStateTransitionAllowed(toState, roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="ProxyURL"/>s in this collection have an identical
		/// proxyURL Type. <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsIdenticalProxyURLType()
		{
			bool firstProxyURL = true;
			int proxyURLTypeIdSave = 0;
			foreach (ProxyURL proxyURL in this)
			{
				if (firstProxyURL == true)
				{
					proxyURLTypeIdSave = proxyURL.ProxyURLTypeId;
					firstProxyURL = false;
				}
				else
				{
					if (proxyURL.ProxyURLTypeId != proxyURLTypeIdSave)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="ProxyURL"/>s in this collection have no query parameter values.
		/// <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsChildlessQueryParameterValue()
		{
			foreach (ProxyURL proxyURL in this)
			{
				Query qry = new Query(ProxyURLQueryParameterValue.Schema);
				qry.AddWhere(ProxyURLQueryParameterValue.Columns.ProxyURLId, proxyURL.Id);
				if (qry.GetCount("CreatedBy") > 0)
				{
					return false;
				}
			}
			return true;
		}

		#endregion

	}
}