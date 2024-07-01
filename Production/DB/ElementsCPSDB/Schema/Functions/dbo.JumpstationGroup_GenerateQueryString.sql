
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- User Defined Function
-- =============================================
-- Author:		Robert Mukai
-- Create date: 5/17/12
-- Description:	Returns the QueryString for Jumpstation.
-- Returns null if none
-- =============================================
CREATE FUNCTION [dbo].[JumpstationGroup_GenerateQueryString]
    (
     @JumpstationGroupId INT
    )
RETURNS VARCHAR(1024)
AS BEGIN
	DECLARE @QueryString VARCHAR(1024)
	DECLARE @JumpstationGroupSelectorId INT
	
	SELECT @QueryString = ''
	SELECT @JumpstationGroupId = ISNULL(@JumpstationGroupId, -1)
	SELECT TOP 1 @JumpstationGroupSelectorId = ISNULL(Id, -1) FROM JumpstationGroupSelector WITH (NOLOCK) WHERE dbo.JumpstationGroupSelector.JumpstationGroupId = @JumpstationGroupId
	
	SELECT @QueryString = @QueryString + queryString FROM
	(SELECT '&' + QueryParameter.ElementsKey + '=' + CASE WHEN Negation=1 THEN '!' ELSE '' END + MIN(QueryParameterValue.NAME) AS queryString
	FROM	JumpstationGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
            QueryParameterValue WITH (NOLOCK) ON JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
            QueryParameter WITH (NOLOCK) ON QueryParameterValue.QueryParameterId = QueryParameter.Id
    WHERE dbo.JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId = @JumpstationGroupSelectorId 
    GROUP BY QueryParameter.ElementsKey, Negation) T1
	IF LEN(@QueryString) > 0
		-- remove leading &
		SELECT @QueryString = SUBSTRING(@QueryString, 2, LEN(@QueryString))
    RETURN @QueryString
   END
GO
