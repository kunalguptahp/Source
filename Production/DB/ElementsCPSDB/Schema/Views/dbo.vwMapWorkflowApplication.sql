
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapWorkflowApplication
AS
SELECT     dbo.WorkflowApplication.Id, dbo.WorkflowApplication.Name, dbo.WorkflowApplication.CreatedBy, dbo.WorkflowApplication.CreatedOn, 
                      dbo.WorkflowApplication.ModifiedBy, dbo.WorkflowApplication.ModifiedOn, dbo.WorkflowApplication.Version, dbo.WorkflowApplication.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowApplication.ElementsKey, dbo.WorkflowApplicationType.Name AS WorkflowApplicationTypeName
FROM         dbo.WorkflowApplication WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowApplication.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.WorkflowApplicationType WITH (NOLOCK) ON dbo.WorkflowApplication.WorkflowApplicationTypeId = dbo.WorkflowApplicationType.Id
GO
