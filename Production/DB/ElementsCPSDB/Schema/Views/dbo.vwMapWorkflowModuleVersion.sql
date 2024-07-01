
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapWorkflowModuleVersion
AS
SELECT     TOP (100) PERCENT dbo.RowStatus.Name, dbo.WorkflowModuleVersion.VersionMajor, dbo.WorkflowModuleVersion.VersionMinor, 
                      dbo.WorkflowModuleVersion.CreatedBy, dbo.WorkflowModuleVersion.CreatedOn, dbo.WorkflowModuleVersion.ModifiedOn, dbo.WorkflowModuleVersion.ModifiedBy, 
                      dbo.WorkflowModuleVersion.WorkflowModuleCategoryId, dbo.WorkflowModuleVersion.WorkflowModuleSubCategoryId, 
                      dbo.WorkflowModuleSubCategory.Name AS WorkflowModuleSubCategoryName, dbo.WorkflowModuleCategory.Name AS WorkflowModuleCategoryName, 
                      dbo.WorkflowModuleVersion.Id
FROM         dbo.RowStatus WITH (NOLOCK) INNER JOIN
                      dbo.WorkflowModuleVersion WITH (NOLOCK) ON dbo.RowStatus.Id = dbo.WorkflowModuleVersion.RowStatusId INNER JOIN
                      dbo.WorkflowModuleCategory WITH (NOLOCK) ON dbo.WorkflowModuleVersion.WorkflowModuleCategoryId = dbo.WorkflowModuleCategory.Id INNER JOIN
                      dbo.WorkflowModuleSubCategory WITH (NOLOCK) ON dbo.WorkflowModuleVersion.WorkflowModuleSubCategoryId = dbo.WorkflowModuleSubCategory.Id
ORDER BY WorkflowModuleCategoryName, WorkflowModuleSubCategoryName, dbo.WorkflowModuleVersion.VersionMajor
GO
