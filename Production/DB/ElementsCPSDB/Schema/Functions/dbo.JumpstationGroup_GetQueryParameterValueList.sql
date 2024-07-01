
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [JumpstationGroup].[Id], [dbo].[JumpstationGroup_GetQueryParameterValueList]([JumpstationGroup].[Id], ', ', 1, 1, 1) FROM [dbo].[JumpstationGroup]
	SELECT dbo.JumpstationGroup_GetQueryParameterValueList (1 ', ', 1, 1, 1)
	SELECT TOP 10 [JumpstationGroup].[Id], dbo.JumpstationGroup_GetQueryParameterValueList ([JumpstationGroup].[Id], '/', 1, 1, 1) FROM [dbo].[JumpstationGroup] ORDER BY [JumpstationGroup].[Id] DESC
*/
-- =============================================
-- Author:		Robert Mukai
-- Create date: 10/27/10
-- Description:	Returns a delimited list of QueryParameterValues for a specified JumpstationGroup.
-- =============================================
Create FUNCTION [dbo].[JumpstationGroup_GetQueryParameterValueList]
    (
     @JumpstationGroupId INT,
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
		dbo.JumpstationGroupSelector AS JumpstationGroupSelector WITH (NOLOCK) INNER JOIN
        dbo.JumpstationGroupSelector_QueryParameterValue AS JumpstationGroupSelector_QueryParameterValue WITH (NOLOCK) ON 
        JumpstationGroupSelector.Id = JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId INNER JOIN
        dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
        JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
        dbo.QueryParameter AS QueryParameter WITH (NOLOCK) ON
        QueryParameterValue.QueryParameterId = QueryParameter.Id
    WHERE
        ([JumpstationGroupSelector].[JumpstationGroupId] = @JumpstationGroupId) AND
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
