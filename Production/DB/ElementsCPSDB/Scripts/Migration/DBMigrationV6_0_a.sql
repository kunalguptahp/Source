/*
Run this script on:

        (local).ElementsCPSDBTest    -  This database will be modified

to synchronize it with:

        (local).ElementsCPSDB

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.50.10 from Red Gate Software Ltd at 4/4/2012 1:40:17 PM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Dropping foreign keys from [dbo].[JumpstationItem_JumpstationGroupType]'
GO
ALTER TABLE [dbo].[JumpstationItem_JumpstationGroupType] DROP
CONSTRAINT [FK_JumpstationItem_JumpstationItem_JumpstationGroupType],
CONSTRAINT [FK_JumpstationItem_JumpstationGroupType_JumpstationdGroupType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[JumpstationLabel]'
GO
ALTER TABLE [dbo].[JumpstationLabel] DROP
CONSTRAINT [FK_JumpstationLabel_JumpstationItem],
CONSTRAINT [FK_JumpstationLabel_RowStatus],
CONSTRAINT [FK_JumpstationLabel_JumpstationLabelType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[JumpstationItem]'
GO
ALTER TABLE [dbo].[JumpstationItem] DROP
CONSTRAINT [FK_JumpstationItem_RowStatus]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[JumpstationLabelValue]'
GO
ALTER TABLE [dbo].[JumpstationLabelValue] DROP
CONSTRAINT [FK_JumpstationLabelValue_JumpstationLabel],
CONSTRAINT [FK_JumpstationLabelValue_RowStatus],
CONSTRAINT [FK_JumpstationLabelValue_JumpstationGroup]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[JumpstationLabelType]'
GO
ALTER TABLE [dbo].[JumpstationLabelType] DROP
CONSTRAINT [FK__JumpstationLabel__B6901D3]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationItem]'
GO
ALTER TABLE [dbo].[JumpstationItem] DROP CONSTRAINT [CK_JumpstationItem_Name]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationItem]'
GO
ALTER TABLE [dbo].[JumpstationItem] DROP CONSTRAINT [CK_JumpstationItem_Name_IsTrimmed]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationItem]'
GO
ALTER TABLE [dbo].[JumpstationItem] DROP CONSTRAINT [CK_JumpstationItem_Name_MinLen]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationItem]'
GO
ALTER TABLE [dbo].[JumpstationItem] DROP CONSTRAINT [PK_JumpstationItem]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationLabel]'
GO
ALTER TABLE [dbo].[JumpstationLabel] DROP CONSTRAINT [CK_JumpstationLabel_Name]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationLabel]'
GO
ALTER TABLE [dbo].[JumpstationLabel] DROP CONSTRAINT [CK_JumpstationLabel_Name_IsTrimmed]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationLabel]'
GO
ALTER TABLE [dbo].[JumpstationLabel] DROP CONSTRAINT [CK_JumpstationLabel_Name_MinLen]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationLabel]'
GO
ALTER TABLE [dbo].[JumpstationLabel] DROP CONSTRAINT [PK_JumpstationLabel]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationLabelType]'
GO
ALTER TABLE [dbo].[JumpstationLabelType] DROP CONSTRAINT [CK__Jumpstati____5CCE6E54]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationLabelType]'
GO
ALTER TABLE [dbo].[JumpstationLabelType] DROP CONSTRAINT [CK__Jumpstati____3AE3CB46]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationLabelType]'
GO
ALTER TABLE [dbo].[JumpstationLabelType] DROP CONSTRAINT [PK__JumpstationLabel__736B857C]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationItem_JumpstationGroupType]'
GO
ALTER TABLE [dbo].[JumpstationItem_JumpstationGroupType] DROP CONSTRAINT [PK_JumpstationItem_JumpstationGroupTypeType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[JumpstationLabelValue]'
GO
ALTER TABLE [dbo].[JumpstationLabelValue] DROP CONSTRAINT [PK_JumpstationLabelValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[vwMapJumpstationLabelValue]'
GO
DROP VIEW [dbo].[vwMapJumpstationLabelValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[vwMapJumpstationItem_JumpstationGroupType]'
GO
DROP VIEW [dbo].[vwMapJumpstationItem_JumpstationGroupType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[JumpstationLabelValue]'
GO
DROP TABLE [dbo].[JumpstationLabelValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[vwMapJumpstationLabel_JumpstationGroupType]'
GO
DROP VIEW [dbo].[vwMapJumpstationLabel_JumpstationGroupType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[vwMapJumpstationLabel]'
GO
DROP VIEW [dbo].[vwMapJumpstationLabel]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[vwMapJumpstationItem]'
GO
DROP VIEW [dbo].[vwMapJumpstationItem]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[JumpstationLabelType]'
GO
DROP TABLE [dbo].[JumpstationLabelType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[JumpstationLabel]'
GO
DROP TABLE [dbo].[JumpstationLabel]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[JumpstationItem]'
GO
DROP TABLE [dbo].[JumpstationItem]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[JumpstationItem_JumpstationGroupType]'
GO
DROP TABLE [dbo].[JumpstationItem_JumpstationGroupType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationDomain]'
GO
CREATE TABLE [dbo].[JumpstationDomain]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DomainURL] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[JumpstationGroupType]'
GO
ALTER TABLE [dbo].[JumpstationGroupType] ADD
[JumpstationApplicationId] [int] NOT NULL CONSTRAINT [DF__Jumpstati__Jumps__35043EB3] DEFAULT ((1)),
[ValidationJumpstationDomainId] [int] NOT NULL CONSTRAINT [DF_JumpstationGroupType_ValidationJumpstationDomainId] DEFAULT ((1)),
[PublicationJumpstationDomainId] [int] NOT NULL CONSTRAINT [DF_JumpstationGroupType_PublicationJumpstationDomainId] DEFAULT ((2))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[JumpstationMacro]'
GO
ALTER TABLE [dbo].[JumpstationMacro] DROP
COLUMN [MatchName],
COLUMN [ResultValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[JumpstationMacro]'
GO
ALTER TABLE [dbo].[JumpstationMacro] ADD
[DefaultResultValue] [nvarchar](256) NOT NULL DEFAULT 'Invalid'
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapJumpstationGroupType]'
GO
ALTER VIEW dbo.vwMapJumpstationGroupType
AS
SELECT     dbo.JumpstationGroupType.Id, dbo.JumpstationGroupType.Name, dbo.JumpstationGroupType.CreatedBy, dbo.JumpstationGroupType.CreatedOn, 
                      dbo.JumpstationGroupType.ModifiedBy, dbo.JumpstationGroupType.ModifiedOn, dbo.JumpstationGroupType.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationApplication.Name AS JumpstationApplicationName, dbo.JumpstationGroupType.JumpstationApplicationId
FROM         dbo.JumpstationGroupType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationGroupType.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.JumpstationApplication ON dbo.JumpstationGroupType.JumpstationApplicationId = dbo.JumpstationApplication.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapQueryParameterValue]'
GO
ALTER VIEW dbo.vwMapQueryParameterValue
AS
SELECT     TOP (100) PERCENT dbo.QueryParameterValue.Id, dbo.QueryParameterValue.Name, dbo.QueryParameterValue.CreatedBy, dbo.QueryParameterValue.CreatedOn, 
                      dbo.QueryParameterValue.ModifiedBy, dbo.QueryParameterValue.ModifiedOn, dbo.QueryParameterValue.RowStatusId, dbo.QueryParameterValue.QueryParameterId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.QueryParameter.Name AS QueryParameterName
FROM         dbo.QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameterValue.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameterValue.QueryParameterId = dbo.QueryParameter.Id
ORDER BY dbo.QueryParameterValue.Name
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapJumpstationMacro]'
GO
ALTER VIEW dbo.vwMapJumpstationMacro
AS
SELECT     dbo.JumpstationMacro.Id, dbo.RowStatus.Name AS RowStatusName, dbo.Person.Name AS PersonName, dbo.JumpstationMacro.CreatedBy, 
                      dbo.JumpstationMacro.CreatedOn, dbo.JumpstationMacro.ModifiedBy, dbo.JumpstationMacro.ModifiedOn, dbo.JumpstationMacro.RowStatusId, 
                      dbo.JumpstationMacro.Name, dbo.JumpstationMacro.Description, dbo.JumpstationMacro.JumpstationMacroStatusId, 
                      dbo.JumpstationMacroStatus.Name AS JumpstationMacroStatusName, dbo.JumpstationMacro.OwnerId, dbo.JumpstationMacro.ProductionId, 
                      dbo.JumpstationMacro.ValidationId
FROM         dbo.JumpstationMacro WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationMacro.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.JumpstationMacro.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.JumpstationMacroStatus WITH (NOLOCK) ON dbo.JumpstationMacro.JumpstationMacroStatusId = dbo.JumpstationMacroStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationMacroValue]'
GO
CREATE TABLE [dbo].[JumpstationMacroValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[JumpstationMacroId] [int] NOT NULL,
[MatchName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ResultValue] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_JumpstationMacroValue] on [dbo].[JumpstationMacroValue]'
GO
ALTER TABLE [dbo].[JumpstationMacroValue] ADD CONSTRAINT [PK_JumpstationMacroValue] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[QueryParameter_JumpstationGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] ADD
[MaximumSelection] [int] NOT NULL CONSTRAINT [DF__QueryPara__Maxim__227B7A24] DEFAULT ((0)),
[Wildcard] [bit] NOT NULL CONSTRAINT [DF__QueryPara__Wildc__236F9E5D] DEFAULT ((0)),
[SortOrder] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapJumpstationMacroValue]'
GO
CREATE VIEW dbo.vwMapJumpstationMacroValue
AS
SELECT     dbo.JumpstationMacroValue.Id, dbo.JumpstationMacroValue.CreatedBy, dbo.JumpstationMacroValue.CreatedOn, dbo.JumpstationMacroValue.ModifiedBy, 
                      dbo.JumpstationMacroValue.ModifiedOn, dbo.JumpstationMacroValue.RowStatusId, dbo.JumpstationMacroValue.JumpstationMacroId, 
                      dbo.JumpstationMacroValue.MatchName, dbo.JumpstationMacroValue.ResultValue, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationMacro.Name AS JumpstationMacroName
FROM         dbo.JumpstationMacro INNER JOIN
                      dbo.JumpstationMacroValue ON dbo.JumpstationMacro.Id = dbo.JumpstationMacroValue.JumpstationMacroId INNER JOIN
                      dbo.RowStatus ON dbo.JumpstationMacroValue.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapQueryParameter_JumpstationGroupType]'
GO
ALTER VIEW dbo.vwMapQueryParameter_JumpstationGroupType
AS
SELECT     dbo.QueryParameter_JumpstationGroupType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.JumpstationGroupType.Name, dbo.QueryParameter_JumpstationGroupType.CreatedBy, 
                      dbo.QueryParameter_JumpstationGroupType.CreatedOn, dbo.QueryParameter_JumpstationGroupType.ModifiedBy, 
                      dbo.QueryParameter_JumpstationGroupType.ModifiedOn, dbo.QueryParameter_JumpstationGroupType.QueryParameterId, 
                      dbo.QueryParameter_JumpstationGroupType.JumpstationGroupTypeId, 
                      dbo.QueryParameter_JumpstationGroupType.Name AS QueryParameterJumpstationGroupTypeName, dbo.QueryParameter_JumpstationGroupType.Description, 
                      dbo.QueryParameter_JumpstationGroupType.MaximumSelection, dbo.QueryParameter_JumpstationGroupType.Wildcard, 
                      dbo.QueryParameter_JumpstationGroupType.SortOrder
FROM         dbo.QueryParameter_JumpstationGroupType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_JumpstationGroupType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.JumpstationGroupType ON dbo.QueryParameter_JumpstationGroupType.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationGroup_GenerateQueryString]'
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
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapJumpstationGroup]'
GO
ALTER VIEW dbo.vwMapJumpstationGroup
AS
SELECT     dbo.JumpstationGroup.Id, dbo.RowStatus.Name AS RowStatusName, dbo.JumpstationGroupStatus.Name AS JumpstationGroupStatusName, 
                      dbo.Person.Name AS PersonName, dbo.JumpstationGroup.CreatedBy, dbo.JumpstationGroup.CreatedOn, dbo.JumpstationGroup.ModifiedBy, 
                      dbo.JumpstationGroup.ModifiedOn, dbo.JumpstationGroup.RowStatusId, dbo.JumpstationGroup.Description, dbo.JumpstationGroup.JumpstationGroupStatusId, 
                      dbo.JumpstationGroup.JumpstationGroupTypeId, dbo.JumpstationGroup.OwnerId, dbo.JumpstationGroupType.Name AS JumpstationGroupTypeName, 
                      dbo.JumpstationGroup_GetQueryParameterValueList(dbo.JumpstationGroup.Id, 15, ', ', NULL) AS BrandQueryParameterValue, 
                      dbo.JumpstationGroup_GetQueryParameterValueList(dbo.JumpstationGroup.Id, 7, ', ', NULL) AS CycleQueryParameterValue, 
                      dbo.JumpstationGroup_GetQueryParameterValueList(dbo.JumpstationGroup.Id, 3, ', ', NULL) AS LocaleQueryParameterValue, 
                      dbo.JumpstationGroup_GetQueryParameterValueList(dbo.JumpstationGroup.Id, 13, ', ', NULL) AS TouchpointQueryParameterValue, 
                      dbo.JumpstationGroup_GetQueryParameterValueList(dbo.JumpstationGroup.Id, 5, ', ', NULL) AS PartnerCategoryQueryParameterValue, 
                      dbo.JumpstationGroup_GetQueryParameterValueList(dbo.JumpstationGroup.Id, 14, ', ', NULL) AS PlatformQueryParameterValue, 
                      dbo.JumpstationGroup_GetTagList(dbo.JumpstationGroup.Id, ', ', 1) AS Tags, dbo.JumpstationGroup_GenerateQueryString(dbo.JumpstationGroup.Id) AS QueryString,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.JumpstationGroup_Tag WITH (NOLOCK)
                            WHERE      (dbo.JumpstationGroup.Id = JumpstationGroupId)) AS TagCount, dbo.JumpstationGroup.ProductionId, dbo.JumpstationGroup.ValidationId, 
                      dbo.JumpstationGroup.Name, dbo.JumpstationGroup.JumpstationApplicationId, dbo.JumpstationApplication.Name AS JumpstationApplicationName, 
                      dbo.JumpstationGroup.TargetURL, dbo.JumpstationGroup.[Order], dbo.JumpstationDomain.DomainURL AS PublicationDomain
FROM         dbo.JumpstationGroup WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationGroup.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.JumpstationGroup.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.JumpstationGroupStatus WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupStatusId = dbo.JumpstationGroupStatus.Id INNER JOIN
                      dbo.JumpstationGroupType WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id INNER JOIN
                      dbo.JumpstationApplication WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationApplicationId = dbo.JumpstationApplication.Id INNER JOIN
                      dbo.JumpstationDomain ON dbo.JumpstationGroupType.PublicationJumpstationDomainId = dbo.JumpstationDomain.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationGroupType]'
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupType_JumpstationApplication] FOREIGN KEY ([JumpstationApplicationId]) REFERENCES [dbo].[JumpstationApplication] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationMacroValue]'
GO
ALTER TABLE [dbo].[JumpstationMacroValue] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacroValue_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_JumpstationMacroValue_JumpstationMacro] FOREIGN KEY ([JumpstationMacroId]) REFERENCES [dbo].[JumpstationMacro] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
