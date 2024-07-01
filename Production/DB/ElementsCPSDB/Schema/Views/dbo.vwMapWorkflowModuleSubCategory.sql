SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwMapWorkflowModuleSubCategory]
AS
SELECT     dbo.WorkflowModuleSubCategory.Id, dbo.WorkflowModuleSubCategory.Name, dbo.WorkflowModuleSubCategory.CreatedBy, 
                      dbo.WorkflowModuleSubCategory.CreatedOn, dbo.WorkflowModuleSubCategory.ModifiedBy, 
                      dbo.WorkflowModuleSubCategory.ModifiedOn, dbo.WorkflowModuleSubCategory.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName
FROM         dbo.WorkflowModuleSubCategory WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowModuleSubCategory.RowStatusId = dbo.RowStatus.Id
GO
