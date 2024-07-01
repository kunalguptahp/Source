
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapWorkflow
AS
SELECT     dbo.Workflow.Id, dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowStatus.Name AS WorkflowStatusName, dbo.Person.Name AS PersonName, 
                      dbo.Workflow.CreatedBy, dbo.Workflow.CreatedOn, dbo.Workflow.ModifiedBy, dbo.Workflow.ModifiedOn, dbo.Workflow.RowStatusId, dbo.Workflow.Description, 
                      dbo.Workflow.WorkflowStatusId, dbo.Workflow.WorkflowTypeId, dbo.Workflow.OwnerId, dbo.WorkflowType.Name AS WorkflowTypeName, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 203, ', ', NULL) AS ReleaseQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 101, ', ', NULL) AS CountryQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 107, ', ', NULL) AS PlatformQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 106, ', ', NULL) AS BrandQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 204, ', ', NULL) AS ModelNumberQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 201, ', ', NULL) AS SubBrandQueryParameterValue, dbo.Workflow_GetTagList(dbo.Workflow.Id, ', ', 1) 
                      AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.Workflow_Tag WITH (NOLOCK)
                            WHERE      (dbo.Workflow.Id = WorkflowId)) AS TagCount, dbo.Workflow.ProductionId, dbo.Workflow.ValidationId, dbo.Workflow.Name, 
                      dbo.Workflow.WorkflowApplicationId, dbo.WorkflowApplication.Name AS WorkflowApplicationName, dbo.Workflow.Offline, dbo.Workflow.VersionMajor, 
                      dbo.Workflow.VersionMinor, dbo.Workflow.Filename
FROM         dbo.Workflow WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.Workflow.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.Workflow.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.WorkflowStatus WITH (NOLOCK) ON dbo.Workflow.WorkflowStatusId = dbo.WorkflowStatus.Id INNER JOIN
                      dbo.WorkflowType WITH (NOLOCK) ON dbo.Workflow.WorkflowTypeId = dbo.WorkflowType.Id INNER JOIN
                      dbo.WorkflowApplication WITH (NOLOCK) ON dbo.Workflow.WorkflowApplicationId = dbo.WorkflowApplication.Id
GO
