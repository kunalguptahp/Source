SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapWorkflowModule_WorkflowCondition
AS
SELECT     dbo.WorkflowModule.Name AS WorkflowModuleName, dbo.WorkflowModule_WorkflowCondition.WorkflowConditionId, 
                      dbo.WorkflowModule_WorkflowCondition.WorkflowModuleId, dbo.WorkflowCondition.Name AS WorkflowConditionName, 
                      dbo.WorkflowModule_WorkflowCondition.CreatedBy, dbo.WorkflowModule_WorkflowCondition.CreatedOn, 
                      dbo.WorkflowModule_WorkflowCondition.ModifiedBy, dbo.WorkflowModule_WorkflowCondition.ModifiedOn, 
                      dbo.WorkflowModule_WorkflowCondition.Id, dbo.WorkflowCondition.Version, dbo.WorkflowCondition.Description, dbo.WorkflowCondition.Operator, 
                      dbo.WorkflowCondition.Value
FROM         dbo.WorkflowCondition WITH (NOLOCK) INNER JOIN
                      dbo.WorkflowModule_WorkflowCondition WITH (NOLOCK) ON 
                      dbo.WorkflowCondition.Id = dbo.WorkflowModule_WorkflowCondition.WorkflowConditionId INNER JOIN
                      dbo.WorkflowModule WITH (NOLOCK) ON dbo.WorkflowModule_WorkflowCondition.WorkflowModuleId = dbo.WorkflowModule.Id
GO
