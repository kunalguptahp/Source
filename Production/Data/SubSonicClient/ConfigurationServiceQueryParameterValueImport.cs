using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
using System;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceQueryParameterValueImport table.
	/// </summary>
	public partial class ConfigurationServiceQueryParameterValueImport
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Deletes a specified <see cref="ConfigurationServiceQueryParameterValueImport"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void DestroyByConfigurationServiceGroupImportId(int configurationServiceGroupImportId)
		{
			Query query = ConfigurationServiceQueryParameterValueImport.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceQueryParameterValueImport.ConfigurationServiceGroupImportIdColumn.ColumnName, configurationServiceGroupImportId);
			ConfigurationServiceQueryParameterValueImportController.DestroyByQuery(query);
		}

		#endregion

        #region db Methods

        /// <summary>
        /// Delete the ConfigurationServiceQueryParameterValueImport
        /// </summary>
        public void Delete()
        {
            try
            {
                this.Destroy();
            }
            catch (Exception ex)
            {
                string message = string.Format(CultureInfo.CurrentCulture,
                                               "Unable to delete ConfigurationServiceQueryParameterValueImportId #{0}.",
                                               this.Id);
                LogManager.Current.Log(Severity.Error, this, message, ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes a specified ConfigurationServiceQueryParameterValueImport record (whether the table supports logical/soft deletes or not).
        /// </summary>
        public void Destroy()
        {
            Query query = ConfigurationServiceQueryParameterValueImport.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(IdColumn.ColumnName, this.Id);
            ConfigurationServiceQueryParameterValueImportController.DestroyByQuery(query);
        }

        #endregion

	}
}
