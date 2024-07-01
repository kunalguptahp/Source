
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
-- This function is utilized by Multiple Update where user 
-- may update a single (or less than the maximum parameter)
-- paramter.
-- Returns 0 if not
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_HasDuplicateQueryParameterValuesDelimitedList]
    (
     @ProxyURLId INT,
     @ProxyURLQueryParameterValueIdList VARCHAR(MAX)
    )
RETURNS INT
AS BEGIN
    DECLARE @DuplicateProxyURL INT
	DECLARE @ProxyURLQueryParameterValueIdListCount INT
	DECLARE @ValidationId INT
	DECLARE @ProductionId INT
	DECLARE @ProxyURLTypeId INT
	DECLARE @TempProxyURLQueryParameterValueIdList TABLE (QueryParameterId INT, QueryParameterValueId INT)
	DECLARE @TempProxyURLQueryParameterValueIdDelimitedList TABLE (QueryParameterId INT, QueryParameterValueId INT)
	
	SELECT @ProxyURLId = ISNULL(@ProxyURLId, -1)
	SELECT @ValidationId = ISNULL([dbo].[ProxyURL].[ValidationId], 0), @ProductionId = ISNULL([dbo].[ProxyURL].[ProductionId], 0), @ProxyURLTypeId = [dbo].[ProxyURL].ProxyURLTypeId
		FROM [dbo].[ProxyURL] WITH (NOLOCK)
		WHERE [dbo].[ProxyURL].[Id] = @ProxyURLId
	-- ProxyURL Query Parameter Value Id List
	INSERT INTO @TempProxyURLQueryParameterValueIdList(QueryParameterId, QueryParameterValueId)
	SELECT dbo.QueryParameter.Id, dbo.QueryParameterValue.Id
	FROM 
		dbo.QueryParameter INNER JOIN dbo.QueryParameterValue ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId
		INNER JOIN dbo.ProxyURL_QueryParameterValue ON dbo.QueryParameterValue.Id = dbo.ProxyURL_QueryParameterValue.QueryParameterValueId
	WHERE 
		dbo.ProxyURL_QueryParameterValue.ProxyURLId = @ProxyURLId
		
	-- Query Parameter Value Id Delimited List (New values)
	INSERT INTO @TempProxyURLQueryParameterValueIdDelimitedList(QueryParameterId, QueryParameterValueId)
	SELECT dbo.QueryParameter.Id, dbo.QueryParameterValue.Id
	FROM 
		dbo.QueryParameter INNER JOIN dbo.QueryParameterValue ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId
    WHERE
        ([dbo].[QueryParameterValue].[Id] IN (SELECT ItemValue FROM dbo.SplitStringByDelimiter(@ProxyURLQueryParameterValueIdList, ',')))
        
	-- Update ProxyURL Query Parameter Value Id List with new delimited values
	UPDATE @TempProxyURLQueryParameterValueIdList
	SET QueryParameterValueId = newList.QueryParameterValueId
	FROM 
		@TempProxyURLQueryParameterValueIdList updList,
		@TempProxyURLQueryParameterValueIdDelimitedList newList
	WHERE 
		updList.QueryParameterId = newList.QueryParameterId
	-- Add any new query parameter values
	INSERT INTO @TempProxyURLQueryParameterValueIdList(QueryParameterId, QueryParameterValueId)
	SELECT QueryParameterId, QueryParameterValueId
	FROM 
		@TempProxyURLQueryParameterValueIdDelimitedList
	WHERE 
		QueryParameterId NOT IN (SELECT QueryParameterId FROM @TempProxyURLQueryParameterValueIdList)
    SELECT @DuplicateProxyURL = 0

	-- Number of query parameters
	SELECT @ProxyURLQueryParameterValueIdListCount = COUNT(*) FROM @TempProxyURLQueryParameterValueIdList

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
