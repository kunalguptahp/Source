SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- User Defined Function
/*
Usage Examples:
	SELECT [JumpstationGroup].[Id], [dbo].[JumpstationGroup_GetTagList]([JumpstationGroup].[Id], ', ', 1, 1) FROM [dbo].[JumpstationGroup]
	SELECT dbo.JumpstationGroup_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [JumpstationGroup].[Id], dbo.JumpstationGroup_GetTagList ([JumpstationGroup].[Id], '/', 1, 1) FROM [dbo].[JumpstationGroup] ORDER BY [JumpstationGroup].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified JumpstationGroup.
-- =============================================
CREATE FUNCTION [dbo].[JumpstationGroup_GetTagList]
    (
     @JumpstationGroupId INT,
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
        @TagList = COALESCE(@TagList + @Delimiter, '') + [JumpstationGrouptag].[TagName]
    FROM
        [dbo].[vwMapJumpstationGroupTag_Tag] JumpstationGrouptag
    WHERE
        ([JumpstationGrouptag].[JumpstationGroupId] = @JumpstationGroupId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([JumpstationGrouptag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [JumpstationGrouptag].[TagName]
    RETURN @TagList
   END

GO
