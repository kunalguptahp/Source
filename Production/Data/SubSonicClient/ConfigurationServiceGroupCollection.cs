using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceGroup Collection class.
	/// </summary>
	public partial class ConfigurationServiceGroupCollection
	{
		public List<int> GetIds()
		{
			List<int> ConfigurationServiceGroups = new List<int>(this.Count);
			foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
			{
				ConfigurationServiceGroups.Add(ConfigurationServiceGroup.Id);
			}
			return ConfigurationServiceGroups;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns a new collection of the <see cref="ConfigurationServiceGroup"/>s created as a result of invoking the <see cref="ConfigurationServiceGroup.SaveAsNew(bool)"/> method on each of the ConfigurationServiceGroups in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <returns></returns>
		public ConfigurationServiceGroupCollection SaveAllAsNew(bool assignCurrentUserAsOwner)
		{
			return SaveAllAsNew(assignCurrentUserAsOwner, false);
		}

		/// <summary>
		/// Returns a new collection of the <see cref="ConfigurationServiceGroup"/>s created as a result of invoking the <see cref="ConfigurationServiceGroup.SaveAsNew(bool)"/> method on each of the ConfigurationServiceGroups in this collection.
		/// </summary>
		/// <param name="assignCurrentUserAsOwner"></param>
		/// <param name="copyElementsId"></param>
		/// <returns></returns>
		public ConfigurationServiceGroupCollection SaveAllAsNew(bool assignCurrentUserAsOwner, bool copyElementsId)
		{
			ConfigurationServiceGroupCollection newConfigurationServiceGroups;
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newConfigurationServiceGroups = new ConfigurationServiceGroupCollection();
				foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
				{
					newConfigurationServiceGroups.Add(ConfigurationServiceGroup.SaveAsNew(assignCurrentUserAsOwner, copyElementsId));
				}

				scope.Complete(); // transaction complete
			}

			return newConfigurationServiceGroups;
		}

		/// <summary>
		/// Invokes the <see cref="ConfigurationServiceGroup.Abandon"/> method on each of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		public void Abandon()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
				{
					ConfigurationServiceGroup.Abandon();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="ConfigurationServiceGroup.SubmitToValidator"/> method on each of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		public void SubmitToValidator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
				{
					ConfigurationServiceGroup.SubmitToValidator();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="ConfigurationServiceGroup.Validate"/> method on each of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		public void SubmitToCoordinator()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
				{
					ConfigurationServiceGroup.Validate();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="ConfigurationServiceGroup.Publish"/> method on each of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		public void Publish()
		{
			foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
			{
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					ConfigurationServiceGroup.Publish();

					scope.Complete(); // transaction complete
				}
			}
		}

		/// <summary>
		/// Invokes the <see cref="ConfigurationServiceGroup.UnPublish"/> method on each of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		public void UnPublish()
		{
			//using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew)) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())			
			{
				foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
				{
					ConfigurationServiceGroup.UnPublish();

				}
					scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Invokes the <see cref="ConfigurationServiceGroup.SubmitBackToEditor"/> method on each of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		public void SubmitBackToEditor()
		{
			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
				{
					ConfigurationServiceGroup.SubmitBackToEditor();
				}

				scope.Complete(); // transaction complete
			}
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="ConfigurationServiceGroup.IsDataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsDataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
			{
				if (!ConfigurationServiceGroup.IsDataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="ConfigurationServiceGroup.IsMetadataModificationAllowed(ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsMetadataModificationAllowed(ICollection<UserRoleId> roles)
		{
			foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
			{
				if (!ConfigurationServiceGroup.IsMetadataModificationAllowed(roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>false</c> unless the <see cref="ConfigurationServiceGroup.IsStateTransitionAllowed(ConfigurationServiceGroupStateId,ICollection{UserRoleId})"/> method returns <c>true</c> 
		/// for all of the <see cref="ConfigurationServiceGroup"/>s in this collection.
		/// </summary>
		/// <param name="toState"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public bool IsStateTransitionAllowed(ConfigurationServiceGroupStateId toState, ICollection<UserRoleId> roles)
		{
			foreach (ConfigurationServiceGroup ConfigurationServiceGroup in this)
			{
				if (!ConfigurationServiceGroup.IsStateTransitionAllowed(toState, roles))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="ConfigurationServiceGroup"/>s in this collection have an identical
		/// ConfigurationServiceGroup Type. <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsIdenticalConfigurationServiceGroupType()
		{
			bool firstConfigurationServiceGroup = true;
			int configurationServiceGroupTypeIdSave = 0;
			foreach (ConfigurationServiceGroup configurationServiceGroup in this)
			{
				if (firstConfigurationServiceGroup == true)
				{
					configurationServiceGroupTypeIdSave = configurationServiceGroup.ConfigurationServiceGroupTypeId;
					firstConfigurationServiceGroup = false;
				}
				else
				{
					if (configurationServiceGroup.ConfigurationServiceGroupTypeId != configurationServiceGroupTypeIdSave)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="ConfigurationServiceGroup"/>s in this collection have no label values.
		/// <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsChildlessLabelValue()
		{
			foreach (ConfigurationServiceGroup configurationServiceGroup in this)
			{
				Query qry = new Query(ConfigurationServiceLabelValue.Schema);
				qry.AddWhere(ConfigurationServiceLabelValue.Columns.ConfigurationServiceGroupId, configurationServiceGroup.Id);
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