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
	/// The non-generated portion of the WorkflowModuleVersionController class.
	/// </summary>
	public partial class WorkflowModuleVersionController
	{

        #region Other Methods

        /// <summary>
        /// Convenience method.
        /// Returns the <see cref="WorkflowModuleVersion"/> with the specified categoryId and subCategoryId (if one exists).
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="subCategoryId"></param>
        /// <param name="versionMajor"></param>
        public static WorkflowModuleVersion FetchByCategoryIdSubCategoryIdVersionMajor(int categoryId, int subCategoryId, int versionMajor)
        {
            SqlQuery query = DB.Select().From(WorkflowModuleVersion.Schema);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowModuleCategoryId", categoryId);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowModuleSubCategoryId", subCategoryId);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "VersionMajor", versionMajor);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(WorkflowModuleVersionController));
            WorkflowModuleVersion instance = query.ExecuteSingle<WorkflowModuleVersion>();
            return instance;
        }

        /// <summary>
        /// Convenience method.
        /// Updates the workflow version to the highest version.
        /// </summary>
        /// <param name="name"></param>
        public static void UpdateWorkflowModuleVersionToHighest(int versionMajor, int versionMinor)
        {
            // workflow version is specified by categoryId = 0 and subCategoryId = 0 (WorkflowModuleVersion is shared by Workflow and Module)
            UpdateWorkflowModuleVersionToHighest(0, 0, versionMajor, versionMinor);
        }

        /// <summary>
        /// Convenience method.
        /// Updates the workflow module version to the highest version.
        /// </summary>
        /// <param name="name"></param>
        public static void UpdateWorkflowModuleVersionToHighest(int categoryId, int subCategoryId, int versionMajor, int versionMinor)
        {
            WorkflowModuleVersion saveItem = WorkflowModuleVersionController.FetchByCategoryIdSubCategoryIdVersionMajor(categoryId, subCategoryId, versionMajor);
            if (saveItem == null)
            {
                saveItem = new WorkflowModuleVersion(true);
                saveItem.WorkflowModuleCategoryId = categoryId;
                saveItem.WorkflowModuleSubCategoryId = subCategoryId;
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
        /// <param name="categoryId"></param>
        /// <param name="subCategoryId"></param>
        public static int FetchVersionMajorMaximum(int categoryId, int subCategoryId)
        {
            const string sql = "SELECT MAX(VersionMajor) FROM WorkflowModuleVersion WITH (NOLOCK) WHERE WorkflowModuleCategoryId = @CategoryId AND WorkflowModuleSubCategoryId = @SubCategoryId";
            int? versionMajorMaximum = SqlUtility.ExecuteAsScalar<int?>(SqlUtility.CreateSqlCommandForSql(ElementsCPSSqlUtility.CreateDefaultConnection(), sql,
                SqlUtility.CreateSqlParameter("@CategoryId", SqlDbType.Int, categoryId),
                SqlUtility.CreateSqlParameter("@SubCategoryId", SqlDbType.Int, subCategoryId)));
            return versionMajorMaximum == null ? 1 : (int)versionMajorMaximum;
        }

        /// <summary>
        /// Convenience method.
        /// Returns the maximum minor version by major version, Category
        /// </summary>
        /// <param name="versionMajor"></param>
        /// <param name="categoryId"></param>
        /// <param name="subcategoryId"></param>
        public static int FetchVersionMinorMaximum(int versionMajor, int categoryId, int subCategoryId)
        {
            WorkflowModuleVersion wrkModVersion = WorkflowModuleVersionController.FetchByCategoryIdSubCategoryIdVersionMajor(categoryId, subCategoryId, versionMajor);
            return wrkModVersion == null ? 0 : wrkModVersion.VersionMinor;
        }

        #endregion

	}
}
