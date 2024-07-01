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
	/// This is an ActiveRecord class which wraps the JumpstationMacroValue table.
	/// </summary>
	public partial class JumpstationMacroValue
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by JumpstationMacroId
		/// </summary>
		public static void DestroyByJumpstationMacroId(int macroId)
		{
			// Destroy JumpstationMacroValues first
			JumpstationMacroValueQuerySpecification jumpstationMacroValueQuerySpecification = 
				new JumpstationMacroValueQuerySpecification() { JumpstationMacroId = macroId};
			JumpstationMacroValueCollection jumpstationMacroValueColl =
				JumpstationMacroValueController.Fetch(jumpstationMacroValueQuerySpecification);
			foreach (JumpstationMacroValue jumpstationMacroValue in jumpstationMacroValueColl)
			{
				jumpstationMacroValue.Destroy();
			}
		}

		#endregion

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? jumpstationMacroValueId = (this.IsNew ? null : (int?)this.Id);
			Log(jumpstationMacroValueId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? jumpstationMacroValueId = (this.IsNew ? null : (int?)this.Id);
			Log(jumpstationMacroValueId, severity, this, message, ex);
		}

        internal static void Log(int? jumpstationMacroValueId, Severity severity, object source, string message)
		{
            string logMessage = string.Format(CultureInfo.CurrentCulture, "JumpstationMacroValue History: #{0}: {1}.", jumpstationMacroValueId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

        internal static void Log(int? jumpstationMacroValueId, Severity severity, object source, string message, Exception ex)
		{
            string logMessage = string.Format(CultureInfo.CurrentCulture, "JumpstationMacroValue History: #{0}: {1}.", jumpstationMacroValueId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

		/// <summary>
		/// Delete the JumpstationMacroValue
		/// </summary>
		public void Delete()
		{
				try
				{
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture, "Unable to delete JumpstationMacroValueId #{0}.", this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
		}

		/// <summary>
		/// Deletes a specified JumpstationMacroValue record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = JumpstationMacroValue.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			JumpstationMacroValueController.DestroyByQuery(query);
		}

		#endregion

	}
}
