
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [ConfigurationServiceGroup].[Id], [dbo].[ConfigurationServiceGroup_GetQueryParameterValueList]([ConfigurationServiceGroup].[Id], ', ', 1, 1, 1) FROM [dbo].[ConfigurationServiceGroup]
	SELECT dbo.ConfigurationServiceGroup_GetQueryParameterValueList (1 ', ', 1, 1, 1)
	SELECT TOP 10 [ConfigurationServiceGroup].[Id], dbo.ConfigurationServiceGroup_GetQueryParameterValueList ([ConfigurationServiceGroup].[Id], '/', 1, 1, 1) FROM [dbo].[ConfigurationServiceGroup] ORDER BY [ConfigurationServiceGroup].[Id] DESC
*/
-- =============================================
-- Author:		Robert Mukai
-- Create date: 10/27/10
-- Description:	Returns a delimited list of QueryParameterValues for a specified ConfigurationServiceGroup.
-- =============================================
Create FUNCTION [dbo].[ConfigurationServiceGroup_GetQueryParameterValueList]
    (
     @ConfigurationServiceGroupId INT,
     @QueryParameterId INT,
     @Delimiter VARCHAR(2),
     @RowStatusId INT
    )
RETURNS NVARCHAR(MAX)
AS BEGIN
    DECLARE @QueryParameterValueList NVARCHAR(MAX)
	DECLARE @TempQueryParameterValueList TABLE (Name NVARCHAR(256))

    SELECT
        @QueryParameterValueList = NULL
	--Build the delimited string of QueryParameterValue names

	INSERT INTO @TempQueryParameterValueList(Name)
    SELECT
        DISTINCT CASE WHEN Negation=1 THEN '!' ELSE '' END + [QueryParameterValue].[Name] AS Name
    FROM
		dbo.ConfigurationServiceGroupSelector AS ConfigurationServiceGroupSelector WITH (NOLOCK) INNER JOIN
        dbo.ConfigurationServiceGroupSelector_QueryParameterValue AS ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) ON 
        ConfigurationServiceGroupSelector.Id = ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId INNER JOIN
        dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
        ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
        dbo.QueryParameter AS QueryParameter WITH (NOLOCK) ON
        QueryParameterValue.QueryParameterId = QueryParameter.Id
    WHERE
        ([ConfigurationServiceGroupSelector].[ConfigurationServiceGroupId] = @ConfigurationServiceGroupId) AND
        ([QueryParameterValue].[QueryParameterId] = @QueryParameterId)      
        AND (
             (@RowStatusId IS NULL)
             OR ([QueryParameter].[RowStatusId] = @RowStatusId)
            )
    ORDER BY
        Name

	SELECT @QueryParameterValueList = COALESCE(@QueryParameterValueList + @Delimiter, '') + [Name]
	FROM @TempQueryParameterValueList

    RETURN @QueryParameterValueList
   END


GO
