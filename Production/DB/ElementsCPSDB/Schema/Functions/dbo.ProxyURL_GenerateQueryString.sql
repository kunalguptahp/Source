


SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


-- User Defined Function

-- =============================================
-- Author:		Robert Mukai
-- Create date: 6/24/09
-- Description:	Returns the QueryString for ProxyURL.
-- Returns null if none
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_GenerateQueryString]
    (
     @ProxyURLId INT
    )
RETURNS VARCHAR(512)
AS BEGIN
	DECLARE @QueryString VARCHAR(512)

	SELECT @QueryString = ''
	SELECT @ProxyURLId = ISNULL(@ProxyURLId, -1)
	
	SELECT @QueryString = @QueryString + queryString FROM
	(SELECT '&' + QueryParameter.ElementsKey + '=' + QueryParameterValue.NAME AS queryString
	FROM	ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
            QueryParameterValue WITH (NOLOCK) ON ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
            QueryParameter WITH (NOLOCK) ON QueryParameterValue.QueryParameterId = QueryParameter.Id
    WHERE dbo.ProxyURL_QueryParameterValue.ProxyURLId = @ProxyURLId) T1
 
	IF LEN(@QueryString) > 0
		-- remove leading &
		SELECT @QueryString = SUBSTRING(@QueryString, 2, LEN(@QueryString))
 
    RETURN @QueryString
   END













GO
