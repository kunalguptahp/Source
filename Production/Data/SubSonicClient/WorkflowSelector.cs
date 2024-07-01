using System;
using System.Globalization;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the WorkflowSelector table.
	/// </summary>
	public partial class WorkflowSelector
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by WorkflowId
		/// </summary>
		public static void DestroyByWorkflowId(int WorkflowId)
		{
			// Destroy GroupSelectorQueryParameterValues first
			WorkflowSelectorQuerySpecification WorkflowSelectorQuerySpecification = 
				new WorkflowSelectorQuerySpecification() { WorkflowId = WorkflowId};
			WorkflowSelectorCollection WorkflowSelectorColl =
				WorkflowSelectorController.Fetch(WorkflowSelectorQuerySpecification);
			foreach (WorkflowSelector wrkflowSelector in WorkflowSelectorColl)
			{
				WorkflowSelectorQueryParameterValue.DestroyByWorkflowSelectorId(wrkflowSelector.Id);
			}
			
			// Destroy workflow selectors
			Query query = WorkflowSelector.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(WorkflowIdColumn.ColumnName, WorkflowId);
			WorkflowSelectorController.DestroyByQuery(query);
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? WorkflowSelectorId = (this.IsNew ? null : (int?)this.Id);
			Log(WorkflowSelectorId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? WorkflowSelectorId = (this.IsNew ? null : (int?)this.Id);
			Log(WorkflowSelectorId, severity, this, message, ex);
		}

		internal static void Log(int? WorkflowSelectorId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "WorkflowSelector History: #{0}: {1}.", WorkflowSelectorId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? WorkflowSelectorId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "WorkflowSelector History: #{0}: {1}.", WorkflowSelectorId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <returns></returns>
		public WorkflowSelector SaveAsNew()
		{
			return SaveAsNew(this);
		}

		/// <summary>
		/// Creates a new (not quite identical) copy of an existing <see cref="WorkflowSelector"/>.
		/// </summary>
		/// <param name="originalWorkflowSelector">The <see cref="Workflow"/> to copy/duplicate.</param>
		private static WorkflowSelector SaveAsNew(WorkflowSelector originalWorkflowSelector)
		{
			WorkflowSelector newWorkflowSelector;

			using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
			{
				newWorkflowSelector = WorkflowSelector.Copy(originalWorkflowSelector);

				//mark the newWorkflowSelector instance as an unsaved instance
				newWorkflowSelector.MarkNew();

				//modify the audit trail fields' values appropriately
				string createdBy = SecurityManager.CurrentUserIdentityName;
				DateTime createdOn = DateTime.Now;
				newWorkflowSelector.CreatedBy = createdBy;
				newWorkflowSelector.CreatedOn = createdOn;
				newWorkflowSelector.ModifiedBy = createdBy;
				newWorkflowSelector.ModifiedOn = createdOn;
				newWorkflowSelector.RowStatusId = (int)RowStatus.RowStatusId.Active;

				//save the newWorkflow to the DB so that it is assigned an ID
				newWorkflowSelector.Save(SecurityManager.CurrentUserIdentityName);

				//copy all of the originalWorkflowSelector's WorkflowSelectorQueryParameterValue data to the newWorkflowSelector
				WorkflowSelectorQueryParameterValueCollection colWorkflowSelectorQueryParameterValue = new WorkflowSelectorQueryParameterValueCollection();
				foreach (WorkflowSelectorQueryParameterValue qpvOriginalRecord in originalWorkflowSelector.WorkflowSelectorQueryParameterValueRecords())
				{
					WorkflowSelectorQueryParameterValue qpvNewRecord = WorkflowSelectorQueryParameterValue.Copy(qpvOriginalRecord);
					qpvNewRecord.MarkNew();
					qpvNewRecord.WorkflowSelectorId = newWorkflowSelector.Id;
					qpvNewRecord.CreatedBy = createdBy;
					qpvNewRecord.CreatedOn = createdOn;
					qpvNewRecord.ModifiedBy = createdBy;
					qpvNewRecord.ModifiedOn = createdOn;
					colWorkflowSelectorQueryParameterValue.Add(qpvNewRecord);
				}
				colWorkflowSelectorQueryParameterValue.SaveAll(SecurityManager.CurrentUserIdentityName);

				originalWorkflowSelector.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied to {0}.", newWorkflowSelector));
				newWorkflowSelector.Log(Severity.Info, string.Format(CultureInfo.CurrentCulture, "Copied from {0}.", originalWorkflowSelector));

				scope.Complete(); // transaction complete
			}
			return newWorkflowSelector;
		}

		/// <summary>
		/// Delete the WorkflowSelector
		/// </summary>
		public void Delete()
		{
				try
				{
					this.ClearWorkflowSelectorQueryParameterValue();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture,
												   "Unable to delete WorkflowSelectorId #{0}.",
												   this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
		}

		/// <summary>
		/// Deletes a specified WorkflowSelector record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = WorkflowSelector.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			WorkflowSelectorController.DestroyByQuery(query);
		}

		#region WorkflowSelectorQueryParameterValue-related convenience members

		/// <summary>
		/// Removes all associated workflow selector query parameter values
		/// </summary>
		public void ClearWorkflowSelectorQueryParameterValue()
		{
			WorkflowSelectorQueryParameterValue.DestroyByWorkflowSelectorId(this.Id);
		}

		#endregion

		#endregion

	}
}
