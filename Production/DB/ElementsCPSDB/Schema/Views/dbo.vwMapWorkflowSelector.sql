SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwMapWorkflowSelector]
AS
SELECT     dbo.WorkflowSelector.Id, dbo.WorkflowSelector.Name, dbo.WorkflowSelector.CreatedBy, 
                      dbo.WorkflowSelector.CreatedOn, dbo.WorkflowSelector.ModifiedBy, 
                      dbo.WorkflowSelector.ModifiedOn, dbo.WorkflowSelector.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.WorkflowSelector.WorkflowId
FROM         dbo.WorkflowSelector WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowSelector.RowStatusId = dbo.RowStatus.Id
GO
