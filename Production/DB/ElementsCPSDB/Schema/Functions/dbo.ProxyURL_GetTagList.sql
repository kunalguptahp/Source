SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- User Defined Function

/*
Usage Examples:
	SELECT [ProxyURL].[Id], [dbo].[ProxyURL_GetTagList]([ProxyURL].[Id], ', ', 1, 1) FROM [dbo].[ProxyURL]
	SELECT dbo.ProxyURL_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [ProxyURL].[Id], dbo.ProxyURL_GetTagList ([ProxyURL].[Id], '/', 1, 1) FROM [dbo].[ProxyURL] ORDER BY [ProxyURL].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified ProxyURL.
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_GetTagList]
    (
     @ProxyURLId INT,
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
        @TagList = COALESCE(@TagList + @Delimiter, '') + [proxyURLtag].[TagName]
    FROM
        [dbo].[vwMapProxyURLTag_Tag] proxyURLtag
    WHERE
        ([proxyURLtag].[ProxyURLId] = @ProxyURLId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([proxyURLtag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [proxyURLtag].[TagName]
    RETURN @TagList
   END







GO
