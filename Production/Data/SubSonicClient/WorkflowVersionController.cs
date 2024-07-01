using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using HP.HPFx.Data.Utility;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the WorkflowVersionController class.
	/// </summary>
	public partial class WorkflowVersionController
	{

        #region Other Methods

        /// <summary>
        /// Convenience method.
        /// Returns the <see cref="WorkflowVersion"/>.
        /// </summary>
        /// <param name="versionMajor"></param>
        public static WorkflowVersion FetchByVersionMajor(int versionMajor)
        {
            SqlQuery query = DB.Select().From(WorkflowVersion.Schema);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "VersionMajor", versionMajor);
            WorkflowVersion instance = query.ExecuteSingle<WorkflowVersion>();
            return instance;
        }

        /// <summary>
        /// Convenience method.
        /// Updates the workflow version to the highest version.
        /// </summary>
        /// <param name="name"></param>
        public static void UpdateWorkflowVersionToHighest(int versionMajor, int versionMinor)
        {
            WorkflowVersion saveItem = WorkflowVersionController.FetchByVersionMajor(versionMajor);
            if (saveItem == null)
            {
                saveItem = new WorkflowVersion(true);
                saveItem.VersionMajor = versionMajor;
            }

            // only save the highest minor version
            if (versionMinor > saveItem.VersionMinor)
            {
                saveItem.VersionMinor = versionMinor;
            }

            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
        }

        /// <summary>
        /// Convenience method.
        /// Returns the maximum major version.
        /// </summary>
        public static int FetchVersionMajorMaximum()
        {
            const string sql = "SELECT MAX(VersionMajor) FROM WorkflowVersion";
            int? versionMajorMaximum = SqlUtility.ExecuteAsScalar<int?>(SqlUtility.CreateSqlCommandForSql(ElementsCPSSqlUtility.CreateDefaultConnection(), sql));
            return versionMajorMaximum == null ? 1 : (int)versionMajorMaximum;
        }

        /// <summary>
        /// Convenience method.
        /// Returns the maximum minor version by major version.
        /// </summary>
        /// <param name="name"></param>
        public static int FetchVersionMinorMaximum(int versionMajor)
        {
            WorkflowVersion wrkVersion = WorkflowVersionController.FetchByVersionMajor(versionMajor);
            return wrkVersion == null ? 0 : wrkVersion.VersionMinor;
        }

        #endregion
	}
}
