
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- User Defined Function
-- =============================================
-- Author:		Robert Mukai
-- Create date: 6/24/09
-- Description:	Returns > 0 if ProxyURLId has same 
-- QueryParameterValues as a another published ProxyURL.
-- Returns 0 if not
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_HasDuplicateQueryParameterValues]
    (
     @ProxyURLId INT
    )
RETURNS INT
AS BEGIN
	DECLARE @ValidationId INT
	DECLARE @ProductionId INT
	DECLARE @ProxyURLTypeId INT
    DECLARE @DuplicateProxyURL INT
	DECLARE @ProxyURLQueryParameterValueIdListCount INT
	DECLARE @TempProxyURLQueryParameterValueIdList TABLE (QueryParameterValueId INT)
	DECLARE	@TempProxyURLQueryParameterValueCount TABLE (ProxyURLId INT, ProxyURLQueryParameterValueCount INT)
	
    SELECT
        @DuplicateProxyURL = 0
	SELECT @ProxyURLId = ISNULL(@ProxyURLId, -1)
	SELECT @ValidationId = ISNULL([dbo].[ProxyURL].[ValidationId], 0), @ProductionId = ISNULL([dbo].[ProxyURL].[ProductionId], 0), @ProxyURLTypeId = [dbo].[ProxyURL].ProxyURLTypeId
		FROM [dbo].[ProxyURL] WITH (NOLOCK)
		WHERE [dbo].[ProxyURL].[Id] = @ProxyURLId
	-- The query parameter value list for the proxyURL
	INSERT INTO @TempProxyURLQueryParameterValueIdList(QueryParameterValueId)
		SELECT [dbo].[ProxyURL_QueryParameterValue].QueryParameterValueId FROM [dbo].[ProxyURL_QueryParameterValue] WITH (NOLOCK)
		WHERE [dbo].[ProxyURL_QueryParameterValue].ProxyURLId = @ProxyURLId

	-- The number of query parameter value list ids
	SELECT @ProxyURLQueryParameterValueIdListCount = COUNT(QueryParameterValueId) FROM @TempProxyURLQueryParameterValueIdList

	-- The number of duplicate published proxyIds with identical parameters
	SELECT @DuplicateProxyURL = COUNT([dbo].[ProxyURL].Id)
	FROM
        [dbo].[ProxyURL_QueryParameterValue] WITH (NOLOCK) INNER JOIN [dbo].[ProxyURL] WITH (NOLOCK) 
        ON dbo.ProxyURL_QueryParameterValue.ProxyURLId = dbo.ProxyURL.Id 
    WHERE
        ([dbo].[ProxyURL].[Id] <> @ProxyURLId) AND
        -- Allow redirector replacements
        (ISNULL([dbo].[ProxyURL].[ValidationId], -1) <> @ValidationId) AND
        (ISNULL([dbo].[ProxyURL].[ProductionId], -1) <> @ProductionId) AND
        -- Only same proxyURL Type
        ([dbo].[ProxyURL].[ProxyURLTypeId] = @ProxyURLTypeId) AND
        -- 2 (ready for validation), 3 (validated) and 4 (published)
        ([dbo].[ProxyURL].[ProxyURLStatusId] IN (2,3,4)) AND
        ([dbo].[ProxyURL_QueryParameterValue].[QueryParameterValueId] IN (SELECT QueryParameterValueId FROM @TempProxyURLQueryParameterValueIdList))
	GROUP BY [dbo].[ProxyURL].Id
	HAVING COUNT([dbo].[ProxyURL].Id) = @ProxyURLQueryParameterValueIdListCount

    RETURN @DuplicateProxyURL
   END
GO
