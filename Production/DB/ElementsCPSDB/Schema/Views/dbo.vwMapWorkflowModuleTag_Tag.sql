SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwMapWorkflowModuleTag_Tag]
AS
SELECT     dbo.WorkflowModule_Tag.WorkflowModuleId, dbo.WorkflowModule_Tag.TagId, dbo.WorkflowModule_Tag.CreatedBy, dbo.WorkflowModule_Tag.CreatedOn, 
                      dbo.WorkflowModule_Tag.ModifiedBy, dbo.WorkflowModule_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.WorkflowModule_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.WorkflowModule_Tag.TagId = dbo.Tag.Id
GO
