
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapWorkflowModule
AS
SELECT     dbo.WorkflowModule.Id, dbo.WorkflowModule.Name, dbo.WorkflowModule.CreatedBy, dbo.WorkflowModule.CreatedOn, dbo.WorkflowModule.ModifiedBy, 
                      dbo.WorkflowModule.ModifiedOn, dbo.WorkflowModule.VersionMajor, dbo.WorkflowModule.VersionMinor, dbo.WorkflowModule.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowModule.Description, dbo.WorkflowModule.Filename, dbo.WorkflowModule.ProductionId, 
                      dbo.WorkflowModule.ValidationId, dbo.WorkflowModule.WorkflowModuleStatusId, dbo.WorkflowModule.OwnerId, dbo.WorkflowModule.WorkflowModuleCategoryId, 
                      dbo.WorkflowModule.WorkflowModuleSubCategoryId, dbo.WorkflowModule_GetTagList(dbo.WorkflowModule.Id, ', ', 1) AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.WorkflowModule_Tag WITH (NOLOCK)
                            WHERE      (dbo.WorkflowModule.Id = WorkflowModuleId)) AS TagCount, dbo.WorkflowModuleSubCategory.Name AS ModuleSubCategoryName, 
                      dbo.WorkflowModuleCategory.Name AS ModuleCategoryName, dbo.WorkflowStatus.Name AS WorkflowModuleStatusName, dbo.Person.Name AS PersonName
FROM         dbo.WorkflowModule WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowModule.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.WorkflowModuleCategory WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleCategoryId = dbo.WorkflowModuleCategory.Id INNER JOIN
                      dbo.WorkflowModuleSubCategory WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleSubCategoryId = dbo.WorkflowModuleSubCategory.Id INNER JOIN
                      dbo.WorkflowStatus WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleStatusId = dbo.WorkflowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.WorkflowModule.OwnerId = dbo.Person.Id
GO
