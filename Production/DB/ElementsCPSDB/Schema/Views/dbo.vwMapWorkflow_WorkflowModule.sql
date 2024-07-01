
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapWorkflow_WorkflowModule
AS
SELECT     dbo.Workflow_WorkflowModule.SortOrder, dbo.WorkflowModule.Id, dbo.WorkflowModule.Name, dbo.Workflow_WorkflowModule.WorkflowModuleId, 
                      dbo.Workflow_WorkflowModule.WorkflowId, dbo.WorkflowModule.VersionMajor, dbo.WorkflowModule.VersionMinor, dbo.Workflow_WorkflowModule.ModifiedOn, 
                      dbo.Workflow_WorkflowModule.ModifiedBy, dbo.Workflow_WorkflowModule.CreatedOn, dbo.Workflow_WorkflowModule.CreatedBy, dbo.WorkflowModule.Filename, 
                      dbo.WorkflowModuleSubCategory.Name AS ModuleSubCategoryName, dbo.WorkflowModuleCategory.Name AS ModuleCategoryName, 
                      dbo.Workflow.ValidationId AS WorkflowValidationId, dbo.Workflow.ProductionId AS WorkflowProductionId, dbo.Workflow.WorkflowStatusId, 
                      dbo.WorkflowModule.Description, dbo.WorkflowModule.Title
FROM         dbo.Workflow_WorkflowModule WITH (NOLOCK) INNER JOIN
                      dbo.WorkflowModule WITH (NOLOCK) ON dbo.Workflow_WorkflowModule.WorkflowModuleId = dbo.WorkflowModule.Id INNER JOIN
                      dbo.WorkflowModuleCategory WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleCategoryId = dbo.WorkflowModuleCategory.Id INNER JOIN
                      dbo.WorkflowModuleSubCategory WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleSubCategoryId = dbo.WorkflowModuleSubCategory.Id INNER JOIN
                      dbo.Workflow WITH (NOLOCK) ON dbo.Workflow_WorkflowModule.WorkflowId = dbo.Workflow.Id
GO
