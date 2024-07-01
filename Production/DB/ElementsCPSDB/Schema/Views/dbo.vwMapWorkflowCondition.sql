SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapWorkflowCondition
AS
SELECT     dbo.WorkflowCondition.Id, dbo.WorkflowCondition.Name, dbo.WorkflowCondition.CreatedBy, dbo.WorkflowCondition.CreatedOn, 
                      dbo.WorkflowCondition.ModifiedBy, dbo.WorkflowCondition.ModifiedOn, dbo.WorkflowCondition.Version, dbo.WorkflowCondition.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowCondition.Operator, dbo.WorkflowCondition.Value, dbo.WorkflowCondition.Description
FROM         dbo.WorkflowCondition WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowCondition.RowStatusId = dbo.RowStatus.Id
GO
