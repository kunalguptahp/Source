SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwMapWorkflowType]
AS
SELECT     dbo.WorkflowType.Id, dbo.WorkflowType.Name, dbo.WorkflowType.CreatedBy, 
                      dbo.WorkflowType.CreatedOn, dbo.WorkflowType.ModifiedBy, dbo.WorkflowType.ModifiedOn, 
                      dbo.WorkflowType.RowStatusId, dbo.RowStatus.Name AS RowStatusName
FROM         dbo.WorkflowType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowType.RowStatusId = dbo.RowStatus.Id
GO
