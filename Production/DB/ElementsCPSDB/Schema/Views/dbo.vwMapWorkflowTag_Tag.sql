SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwMapWorkflowTag_Tag]
AS
SELECT     dbo.Workflow_Tag.WorkflowId, dbo.Workflow_Tag.TagId, dbo.Workflow_Tag.CreatedBy, dbo.Workflow_Tag.CreatedOn, 
                      dbo.Workflow_Tag.ModifiedBy, dbo.Workflow_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.Workflow_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.Workflow_Tag.TagId = dbo.Tag.Id
GO
