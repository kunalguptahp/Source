SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwMapWorkflowModuleCategory]
AS
SELECT     dbo.WorkflowModuleCategory.Id, dbo.WorkflowModuleCategory.Name, dbo.WorkflowModuleCategory.CreatedBy, 
                      dbo.WorkflowModuleCategory.CreatedOn, dbo.WorkflowModuleCategory.ModifiedBy, 
                      dbo.WorkflowModuleCategory.ModifiedOn, dbo.WorkflowModuleCategory.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName
FROM         dbo.WorkflowModuleCategory WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowModuleCategory.RowStatusId = dbo.RowStatus.Id
GO
