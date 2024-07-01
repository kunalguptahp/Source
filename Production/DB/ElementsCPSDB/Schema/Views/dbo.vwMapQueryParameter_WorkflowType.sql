SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapQueryParameter_WorkflowType
AS
SELECT     dbo.QueryParameter_WorkflowType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowType.Name, dbo.QueryParameter_WorkflowType.CreatedBy, dbo.QueryParameter_WorkflowType.CreatedOn, 
                      dbo.QueryParameter_WorkflowType.ModifiedBy, dbo.QueryParameter_WorkflowType.ModifiedOn, dbo.QueryParameter_WorkflowType.QueryParameterId, 
                      dbo.QueryParameter_WorkflowType.WorkflowTypeId, dbo.QueryParameter_WorkflowType.Name AS QueryParameterWorkflowTypeName, 
                      dbo.QueryParameter_WorkflowType.Description, dbo.QueryParameter_WorkflowType.MaximumSelection, dbo.QueryParameter_WorkflowType.Wildcard
FROM         dbo.QueryParameter_WorkflowType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_WorkflowType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.WorkflowType WITH (NOLOCK) ON dbo.QueryParameter_WorkflowType.WorkflowTypeId = dbo.WorkflowType.Id
GO
