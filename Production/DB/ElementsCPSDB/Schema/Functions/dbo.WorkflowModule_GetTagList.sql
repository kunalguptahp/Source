SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [WorkflowModule].[Id], [dbo].[WorkflowModule_GetTagList]([WorkflowModule].[Id], ', ', 1, 1) FROM [dbo].[WorkflowModule]
	SELECT dbo.WorkflowModule_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [WorkflowModule].[Id], dbo.WorkflowModule_GetTagList ([WorkflowModule].[Id], '/', 1, 1) FROM [dbo].[WorkflowModule] ORDER BY [WorkflowModule].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified WorkflowModule.
-- =============================================
CREATE FUNCTION [dbo].[WorkflowModule_GetTagList]
    (
     @WorkflowModuleId INT,
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
        @TagList = COALESCE(@TagList + @Delimiter, '') + [WorkflowModuleTag].[TagName]
    FROM
        [dbo].[vwMapWorkflowModuleTag_Tag] WorkflowModuleTag
    WHERE
        ([WorkflowModuleTag].[WorkflowModuleId] = @WorkflowModuleId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([WorkflowModuleTag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [WorkflowModuleTag].[TagName]
    RETURN @TagList
   END
GO
