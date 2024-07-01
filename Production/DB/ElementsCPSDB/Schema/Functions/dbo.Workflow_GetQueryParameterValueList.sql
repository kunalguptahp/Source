SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [Workflow].[Id], [dbo].[Workflow_GetQueryParameterValueList]([Workflow].[Id], ', ', 1, 1, 1) FROM [dbo].[Workflow]
	SELECT dbo.Workflow_GetQueryParameterValueList (1 ', ', 1, 1, 1)
	SELECT TOP 10 [Workflow].[Id], dbo.Workflow_GetQueryParameterValueList ([Workflow].[Id], '/', 1, 1, 1) FROM [dbo].[Workflow] ORDER BY [Workflow].[Id] DESC
*/
-- =============================================
-- Author:		Robert Mukai
-- Create date: 10/27/10
-- Description:	Returns a delimited list of QueryParameterValues for a specified Workflow.
-- =============================================
Create FUNCTION [dbo].[Workflow_GetQueryParameterValueList]
    (
     @WorkflowId INT,
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
		dbo.WorkflowSelector AS WorkflowSelector WITH (NOLOCK) INNER JOIN
        dbo.WorkflowSelector_QueryParameterValue AS WorkflowSelector_QueryParameterValue WITH (NOLOCK) ON 
        WorkflowSelector.Id = WorkflowSelector_QueryParameterValue.WorkflowSelectorId INNER JOIN
        dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
        WorkflowSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
        dbo.QueryParameter AS QueryParameter WITH (NOLOCK) ON
        QueryParameterValue.QueryParameterId = QueryParameter.Id
    WHERE
        ([WorkflowSelector].[WorkflowId] = @WorkflowId) AND
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
