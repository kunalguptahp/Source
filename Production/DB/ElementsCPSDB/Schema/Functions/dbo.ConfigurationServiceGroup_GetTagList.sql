SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- User Defined Function
/*
Usage Examples:
	SELECT [ConfigurationServiceGroup].[Id], [dbo].[ConfigurationServiceGroup_GetTagList]([ConfigurationServiceGroup].[Id], ', ', 1, 1) FROM [dbo].[ConfigurationServiceGroup]
	SELECT dbo.ConfigurationServiceGroup_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [ConfigurationServiceGroup].[Id], dbo.ConfigurationServiceGroup_GetTagList ([ConfigurationServiceGroup].[Id], '/', 1, 1) FROM [dbo].[ConfigurationServiceGroup] ORDER BY [ConfigurationServiceGroup].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified ConfigurationServiceGroup.
-- =============================================
CREATE FUNCTION [dbo].[ConfigurationServiceGroup_GetTagList]
    (
     @ConfigurationServiceGroupId INT,
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
        @TagList = COALESCE(@TagList + @Delimiter, '') + [ConfigurationServiceGrouptag].[TagName]
    FROM
        [dbo].[vwMapConfigurationServiceGroupTag_Tag] ConfigurationServiceGrouptag
    WHERE
        ([ConfigurationServiceGrouptag].[ConfigurationServiceGroupId] = @ConfigurationServiceGroupId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([ConfigurationServiceGrouptag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [ConfigurationServiceGrouptag].[TagName]
    RETURN @TagList
   END

GO
