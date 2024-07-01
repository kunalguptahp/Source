SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwMapWorkflowApplicationType]
AS
SELECT     dbo.WorkflowApplicationType.Id, dbo.WorkflowApplicationType.Name, dbo.WorkflowApplicationType.CreatedBy, 
                      dbo.WorkflowApplicationType.CreatedOn, dbo.WorkflowApplicationType.ModifiedBy, 
                      dbo.WorkflowApplicationType.ModifiedOn, dbo.WorkflowApplicationType.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName
FROM         dbo.WorkflowApplicationType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowApplicationType.RowStatusId = dbo.RowStatus.Id
GO
