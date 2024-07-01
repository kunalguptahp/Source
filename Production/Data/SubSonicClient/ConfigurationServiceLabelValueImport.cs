using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
using System;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceLabelValueImport table.
	/// </summary>
	public partial class ConfigurationServiceLabelValueImport
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Deletes a specified <see cref="ConfigurationServiceLabelValueImport"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void DestroyByConfigurationServiceGroupImportId(int configurationServiceGroupImportId)
		{
			Query query = ConfigurationServiceLabelValueImport.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ConfigurationServiceLabelValueImport.ConfigurationServiceGroupImportIdColumn.ColumnName, configurationServiceGroupImportId);
			ConfigurationServiceLabelValueImportController.DestroyByQuery(query);
		}

		#endregion


        #region db Methods

        /// <summary>
        /// Delete the ConfigurationServiceLabelValueImport
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
                                               "Unable to delete ConfigurationServiceLabelValueImportId #{0}.",
                                               this.Id);
                LogManager.Current.Log(Severity.Error, this, message, ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes a specified ConfigurationServiceLabelValueImport record (whether the table supports logical/soft deletes or not).
        /// </summary>
        public void Destroy()
        {
            Query query = ConfigurationServiceLabelValueImport.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(IdColumn.ColumnName, this.Id);
            ConfigurationServiceLabelValueImportController.DestroyByQuery(query);
        }

        #endregion
	}
}
