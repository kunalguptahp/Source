SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapWorkflowSelector_QueryParameterValue
AS
SELECT     dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameterValue.Name AS QueryParameterValueName, 
                      dbo.WorkflowSelector_QueryParameterValue.Negation, dbo.QueryParameter.Id AS QueryParameterId, dbo.QueryParameterValue.Id AS QueryParameterValueId, 
                      dbo.WorkflowSelector_QueryParameterValue.Id AS WorkflowSelectorQueryParameterValueId, dbo.WorkflowSelector_QueryParameterValue.WorkflowSelectorId, 
                      dbo.WorkflowSelector_QueryParameterValue.ModifiedOn, dbo.WorkflowSelector_QueryParameterValue.ModifiedBy, 
                      dbo.WorkflowSelector_QueryParameterValue.CreatedOn, dbo.WorkflowSelector_QueryParameterValue.CreatedBy
FROM         dbo.WorkflowSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameterValue WITH (NOLOCK) ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId ON 
                      dbo.WorkflowSelector_QueryParameterValue.QueryParameterValueId = dbo.QueryParameterValue.Id
GO
