SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- =============================================
-- Author:		Justin Webster
-- Create date: 8/31/08
-- Description:	Returns a bit that indicates whether a specified ProxyURL is associated with a specified Tag.
-- Usage Example:
--		SELECT
--			[proxyURL].[Id] AS ProxyURLId,
--			[proxyURL].[Name] AS ProxyURLName,
--			[tag].[Id] AS TagId,
--			[tag].[Name] AS TagName,
--			(SELECT [TMOMSDB].[dbo].[ProxyURL_HasTag_ByTagId] ([proxyURL].[Id], [tag].[Id])) AS HasTag,
--			[proxyURL].[Tags] AS ProxyURLTags
--		FROM
--			[TMOMSDB].[dbo].[vwMapProxyURL] AS proxyURL,
--			[dbo].[Tag] AS tag
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_HasTag_ByTagId]
    (
     @ProxyURLId INT,
     @TagId INT
    )
RETURNS BIT
AS BEGIN
    DECLARE @IntFoundTag INT
    SET @IntFoundTag = (
                        SELECT TOP 1
                            [ProxyURL_Tag].[ProxyURLID]
                        FROM
                            [dbo].[ProxyURL_Tag] WITH (NOLOCK)
                        WHERE
                            ([ProxyURL_Tag].[ProxyURLID] = @ProxyURLId)
                            AND ([ProxyURL_Tag].[TagId] = @TagId)
                       ) ;

    DECLARE @BitFoundTag BIT
    SET @BitFoundTag = CAST(ISNULL(@IntFoundTag, 0) AS BIT) ;
    RETURN @BitFoundTag
   END




GO
