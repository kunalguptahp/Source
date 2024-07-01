/*
Script created by SQL Data Compare version 7.1.0.230 from Red Gate Software Ltd at 10/18/2010 2:58:23 PM

Run this script on (local).ElementsCPSDB

This script will make changes to (local).ElementsCPSDB to make it the same as cpcbiz03.ads.corp.hp.com.ElementsCPSDB

Note that this script will carry out all DELETE commands for all tables first, then all the UPDATES and then all the INSERTS
It will disable foreign key constraints at the beginning of the script, and re-enable them at the end
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO

BEGIN TRANSACTION

-- set to modified and null validationId/productionId
UPDATE ConfigurationServiceGroup SET ConfigurationServiceGroupStatusId = 1, ValidationId = null, ProductionId = NULL, OwnerId=13

COMMIT TRANSACTION
GO


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



ALTER VIEW [dbo].[vwMapConfigurationServiceGroup]
AS
SELECT     dbo.ConfigurationServiceGroup.Id, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceGroupStatus.Name AS ConfigurationServiceGroupStatusName, dbo.Person.Name AS PersonName, 
                      dbo.ConfigurationServiceGroup.CreatedBy, dbo.ConfigurationServiceGroup.CreatedOn, dbo.ConfigurationServiceGroup.ModifiedBy, 
                      dbo.ConfigurationServiceGroup.ModifiedOn, dbo.ConfigurationServiceGroup.RowStatusId, dbo.ConfigurationServiceGroup.Description, 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupStatusId, dbo.ConfigurationServiceGroup.ConfigurationServiceGroupTypeId, 
                      dbo.ConfigurationServiceGroup.OwnerId, dbo.ConfigurationServiceGroupType.Name AS ConfigurationServiceGroupTypeName, 
                      dbo.ConfigurationServiceGroup_GetQueryParameterValueList(dbo.ConfigurationServiceGroup.Id, 102, ', ', NULL) AS ReleaseQueryParameterValue, 
                      dbo.ConfigurationServiceGroup_GetQueryParameterValueList(dbo.ConfigurationServiceGroup.Id, 101, ', ', NULL) AS CountryQueryParameterValue, 
                      dbo.ConfigurationServiceGroup_GetQueryParameterValueList(dbo.ConfigurationServiceGroup.Id, 107, ', ', NULL) AS PlatformQueryParameterValue, 
                      dbo.ConfigurationServiceGroup_GetQueryParameterValueList(dbo.ConfigurationServiceGroup.Id, 106, ', ', NULL) AS BrandQueryParameterValue, 
                      dbo.ConfigurationServiceGroup_GetTagList(dbo.ConfigurationServiceGroup.Id, ', ', 1) AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.ConfigurationServiceGroup_Tag WITH (NOLOCK)
                            WHERE      (dbo.ConfigurationServiceGroup.Id = ConfigurationServiceGroupId)) AS TagCount, dbo.ConfigurationServiceGroup.ProductionId, 
                      dbo.ConfigurationServiceGroup.ValidationId, dbo.ConfigurationServiceGroup.Name
FROM         dbo.ConfigurationServiceGroup WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroup.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.ConfigurationServiceGroup.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.ConfigurationServiceGroupStatus WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupStatusId = dbo.ConfigurationServiceGroupStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupTypeId = dbo.ConfigurationServiceGroupType.Id



GO


