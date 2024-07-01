SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [Workflow].[Id], [dbo].[Workflow_GetTagList]([Workflow].[Id], ', ', 1, 1) FROM [dbo].[Workflow]
	SELECT dbo.Workflow_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [Workflow].[Id], dbo.Workflow_GetTagList ([Workflow].[Id], '/', 1, 1) FROM [dbo].[Workflow] ORDER BY [Workflow].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified Workflow.
-- =============================================
CREATE FUNCTION [dbo].[Workflow_GetTagList]
    (
     @WorkflowId INT,
     @Delimiter VARCHAR(2),
     @TagRowStatusId INT
    )
RETURNS NVARCHAR(MAX)
AS BEGIN
    DECLARE @TagList NVARCHAR(MAX)
    SELECT
        @TagList = NULL
	--Build the delimited string of tag names
    SELECT
        @TagList = COALESCE(@TagList + @Delimiter, '') + [Workflowtag].[TagName]
    FROM
        [dbo].[vwMapWorkflowTag_Tag] Workflowtag
    WHERE
        ([Workflowtag].[WorkflowId] = @WorkflowId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([Workflowtag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [Workflowtag].[TagName]
    RETURN @TagList
   END
GO
