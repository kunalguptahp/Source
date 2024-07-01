SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- =============================================
-- Author:		Justin Webster
-- Create date: 8/31/08
-- Description:	Returns an int that indicates whether a specified ProxyURL is associated with a specified Tag. (Returns either the ProxyURL's ID or 0.)
-- Usage Example:
--		SELECT
--			[proxyURL].[Id] AS ProxyURLId,
--			[proxyURL].[Name] AS ProxyURLName,
--			[tag].[Id] AS TagId,
--			[tag].[Name] AS TagName,
--			(SELECT [TMOMSDB].[dbo].[ProxyURL_HasTag_ByTagName] ([proxyURL].[Id], [tag].[Name])) AS HasTag,
--			[proxyURL].[Tags] AS ProxyURLTags
--		FROM
--			[TMOMSDB].[dbo].[vwMapProxyURL] AS proxyURL,
--			[dbo].[Tag] AS tag
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_HasTag_ByTagName]
    (
     @ProxyURLId INT,
     @TagName NVARCHAR(256)
    )
RETURNS INT
AS BEGIN
    DECLARE @IntFoundTag INT
    SET @IntFoundTag = (
                        SELECT TOP 1
                            [vwMapProxyURLTag_Tag].[ProxyURLID]
                        FROM
                            [dbo].[vwMapProxyURLTag_Tag] WITH (NOLOCK)
                        WHERE
                            ([vwMapProxyURLTag_Tag].[ProxyURLID] = @ProxyURLId)
                            AND ([vwMapProxyURLTag_Tag].[TagName] = @TagName)
                       ) ;

    SET @IntFoundTag = ISNULL(@IntFoundTag, 0) ;
    RETURN @IntFoundTag
   END





GO
