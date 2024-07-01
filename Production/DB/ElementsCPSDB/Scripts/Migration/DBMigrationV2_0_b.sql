/*
Run this script on:

        (local).ElementsCPSDBITG    -  This database will be modified

to synchronize it with:

        (local).ElementsCPSDB

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.2.0 from Red Gate Software Ltd at 10/14/2010 2:38:48 PM

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
PRINT N'Dropping constraints from [dbo].[QueryParameterValue]'
GO
ALTER TABLE [dbo].[QueryParameterValue] DROP CONSTRAINT [CK_QueryParameterValue_Name_MinLen]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[uspDeletePersonRoleByPersonId]'
GO
DROP PROCEDURE [dbo].[uspDeletePersonRoleByPersonId]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[vwMapConfigurationServiceGroupSelector_QueryParameterValueAll]'
GO
DROP VIEW [dbo].[vwMapConfigurationServiceGroupSelector_QueryParameterValueAll]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapQueryParameterValue]'
GO
ALTER VIEW dbo.vwMapQueryParameterValue
AS
SELECT     dbo.QueryParameterValue.Id, dbo.QueryParameterValue.Name, dbo.QueryParameterValue.CreatedBy, dbo.QueryParameterValue.CreatedOn, 
                      dbo.QueryParameterValue.ModifiedBy, dbo.QueryParameterValue.ModifiedOn, dbo.QueryParameterValue.RowStatusId, 
                      dbo.QueryParameterValue.QueryParameterId, dbo.RowStatus.Name AS RowStatusName, dbo.QueryParameter.Name AS QueryParameterName
FROM         dbo.QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameterValue.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameterValue.QueryParameterId = dbo.QueryParameter.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[ProxyURL_HasDuplicateQueryParameterValues]'
GO
-- User Defined Function
-- =============================================
-- Author:		Robert Mukai
-- Create date: 6/24/09
-- Description:	Returns > 0 if ProxyURLId has same 
-- QueryParameterValues as a another published ProxyURL.
-- Returns 0 if not
-- =============================================
ALTER FUNCTION [dbo].[ProxyURL_HasDuplicateQueryParameterValues]
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
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapQueryParameter]'
GO
ALTER VIEW dbo.vwMapQueryParameter
AS
SELECT     dbo.QueryParameter.Id, dbo.QueryParameter.Name, dbo.QueryParameter.CreatedBy, dbo.QueryParameter.CreatedOn, 
                      dbo.QueryParameter.ModifiedBy, dbo.QueryParameter.ModifiedOn, dbo.QueryParameter.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.QueryParameter.ElementsKey
FROM         dbo.QueryParameter WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue]'
GO
CREATE TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ConfigurationServiceGroupSelectorId] [int] NOT NULL,
[QueryParameterValueId] [int] NOT NULL,
[Negation] [bit] NOT NULL DEFAULT ((0))
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceGroupSelector_QueryParameterValue] on [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue] ADD CONSTRAINT [PK_ConfigurationServiceGroupSelector_QueryParameterValue] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceItem]'
GO
CREATE TABLE [dbo].[ConfigurationServiceItem]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Parent] [bit] NOT NULL,
[SortOrder] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceItem] on [dbo].[ConfigurationServiceItem]'
GO
ALTER TABLE [dbo].[ConfigurationServiceItem] ADD CONSTRAINT [PK_ConfigurationServiceItem] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroupType]'
GO
CREATE TABLE [dbo].[ConfigurationServiceGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceGroupType] on [dbo].[ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] ADD CONSTRAINT [PK_ConfigurationServiceGroupType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapConfigurationServiceGroupSelector_QueryParameterValue]'
GO
ALTER VIEW dbo.vwMapConfigurationServiceGroupSelector_QueryParameterValue
AS
SELECT     dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameterValue.Name AS QueryParameterValueName, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.Negation, dbo.QueryParameter.Id AS QueryParameterId, 
                      dbo.QueryParameterValue.Id AS QueryParameterValueId, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.Id AS ConfigurationServiceGroupSelectorQueryParameterValueId, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ModifiedOn, dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ModifiedBy, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.CreatedOn, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.CreatedBy
FROM         dbo.ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameterValue ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId ON 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = dbo.QueryParameterValue.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType]'
GO
CREATE TABLE [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ConfigurationServiceGroupTypeId] [int] NOT NULL,
[ConfigurationServiceItemId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceItem_ConfigurationServiceGroupTypeType] on [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType] ADD CONSTRAINT [PK_ConfigurationServiceItem_ConfigurationServiceGroupTypeType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceLabel]'
GO
CREATE TABLE [dbo].[ConfigurationServiceLabel]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[ConfigurationServiceItemId] [int] NOT NULL,
[ConfigurationServiceLabelTypeId] [int] NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Help] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ValueList] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InputRequired] [bit] NOT NULL DEFAULT ((1)),
[SortOrder] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceLabel] on [dbo].[ConfigurationServiceLabel]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] ADD CONSTRAINT [PK_ConfigurationServiceLabel] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroupSelector]'
GO
CREATE TABLE [dbo].[ConfigurationServiceGroupSelector]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceGroupId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceGroupSelector] on [dbo].[ConfigurationServiceGroupSelector]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] ADD CONSTRAINT [PK_ConfigurationServiceGroupSelector] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceApplication]'
GO
CREATE TABLE [dbo].[ConfigurationServiceApplication]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Version] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceApplication] on [dbo].[ConfigurationServiceApplication]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] ADD CONSTRAINT [PK_ConfigurationServiceApplication] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroup_Tag]'
GO
CREATE TABLE [dbo].[ConfigurationServiceGroup_Tag]
(
[ConfigurationServiceGroupId] [int] NOT NULL,
[TagId] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate())
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceGroup_Tag] on [dbo].[ConfigurationServiceGroup_Tag]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_Tag] ADD CONSTRAINT [PK_ConfigurationServiceGroup_Tag] PRIMARY KEY NONCLUSTERED  ([ConfigurationServiceGroupId], [TagId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceLabelType]'
GO
CREATE TABLE [dbo].[ConfigurationServiceLabelType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceLabelType] on [dbo].[ConfigurationServiceLabelType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelType] ADD CONSTRAINT [PK_ConfigurationServiceLabelType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceLabelValue]'
GO
CREATE TABLE [dbo].[ConfigurationServiceLabelValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[ConfigurationServiceLabelId] [int] NOT NULL,
[ConfigurationServiceGroupId] [int] NOT NULL,
[Value] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceLabelValue] on [dbo].[ConfigurationServiceLabelValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelValue] ADD CONSTRAINT [PK_ConfigurationServiceLabelValue] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroup]'
GO
CREATE TABLE [dbo].[ConfigurationServiceGroup]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceGroupStatusId] [int] NOT NULL,
[ConfigurationServiceGroupTypeId] [int] NOT NULL,
[ConfigurationServiceApplicationId] [int] NOT NULL,
[OwnerId] [int] NOT NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceGroup] on [dbo].[ConfigurationServiceGroup]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] ADD CONSTRAINT [PK_ConfigurationServiceGroup] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroupStatus]'
GO
CREATE TABLE [dbo].[ConfigurationServiceGroupStatus]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceGroupStatus] on [dbo].[ConfigurationServiceGroupStatus]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] ADD CONSTRAINT [PK_ConfigurationServiceGroupStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[uspDeleteConfigurationServiceGroupSelectorQueryParameterValueByConfigurationServiceGroupSelectorIdQueryParameterId]'
GO
-- =============================================
-- Author:		Robert Mukai
-- Create date: 8/04/10
-- Description:	Delete ConfigurationServiceGroupSelector_QueryParameterValue by ConfigurationServiceGroupSelectorId and QueryParameterId
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteConfigurationServiceGroupSelectorQueryParameterValueByConfigurationServiceGroupSelectorIdQueryParameterId]
	@configurationServiceGroupSelectorId INT,
	@queryParameterId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE
		dbo.ConfigurationServiceGroupSelector_QueryParameterValue
	WHERE 
		ConfigurationServiceGroupSelectorId = @configurationServiceGroupSelectorId AND
		QueryParameterValueId IN (SELECT Id FROM QueryParameterValue WHERE QueryParameterId = @queryParameterId)
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapConfigurationServiceItem]'
GO
ALTER VIEW dbo.vwMapConfigurationServiceItem
AS
SELECT     dbo.ConfigurationServiceItem.Id, dbo.ConfigurationServiceItem.Name, dbo.ConfigurationServiceItem.CreatedBy, 
                      dbo.ConfigurationServiceItem.CreatedOn, dbo.ConfigurationServiceItem.ModifiedBy, dbo.ConfigurationServiceItem.ModifiedOn, 
                      dbo.ConfigurationServiceItem.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceItem.ElementsKey, 
                      dbo.ConfigurationServiceItem.SortOrder, dbo.ConfigurationServiceItem.Parent
FROM         dbo.ConfigurationServiceItem WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceItem.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapConfigurationServiceGroupType]'
GO
ALTER VIEW dbo.vwMapConfigurationServiceGroupType
AS
SELECT     dbo.ConfigurationServiceGroupType.Id, dbo.ConfigurationServiceGroupType.Name, dbo.ConfigurationServiceGroupType.CreatedBy, 
                      dbo.ConfigurationServiceGroupType.CreatedOn, dbo.ConfigurationServiceGroupType.ModifiedBy, dbo.ConfigurationServiceGroupType.ModifiedOn, 
                      dbo.ConfigurationServiceGroupType.RowStatusId, dbo.RowStatus.Name AS RowStatusName
FROM         dbo.ConfigurationServiceGroupType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroupType.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapConfigurationServiceItem_ConfigurationServiceGroupType]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceItem_ConfigurationServiceGroupType
AS
SELECT     dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.Id, dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.CreatedBy, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.CreatedOn, dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ModifiedBy, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ModifiedOn, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceItemId, 
                      dbo.ConfigurationServiceGroupType.Name AS ConfigurationServiceGroupTypeName, 
                      dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName, dbo.RowStatus.Name AS RowStatusName
FROM         dbo.ConfigurationServiceGroupType WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroupType.Id = dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceItem.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapConfigurationServiceLabel]'
GO
ALTER VIEW dbo.vwMapConfigurationServiceLabel
AS
SELECT     dbo.ConfigurationServiceLabel.Id, dbo.ConfigurationServiceLabel.Name, dbo.ConfigurationServiceLabel.CreatedBy, 
                      dbo.ConfigurationServiceLabel.CreatedOn, dbo.ConfigurationServiceLabel.ModifiedBy, dbo.ConfigurationServiceLabel.ModifiedOn, 
                      dbo.ConfigurationServiceLabel.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceLabel.ElementsKey, 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceItemId, dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName, 
                      dbo.ConfigurationServiceLabel.SortOrder
FROM         dbo.ConfigurationServiceLabel WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceLabel.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapConfigurationServiceApplication]'
GO
ALTER VIEW dbo.vwMapConfigurationServiceApplication
AS
SELECT     dbo.ConfigurationServiceApplication.Id, dbo.ConfigurationServiceApplication.Name, dbo.ConfigurationServiceApplication.CreatedBy, 
                      dbo.ConfigurationServiceApplication.CreatedOn, dbo.ConfigurationServiceApplication.ModifiedBy, dbo.ConfigurationServiceApplication.ModifiedOn, 
                      dbo.ConfigurationServiceApplication.Version, dbo.ConfigurationServiceApplication.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceApplication.ElementsKey
FROM         dbo.ConfigurationServiceApplication WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceApplication.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapConfigurationServiceLabel_ConfigurationServiceGroupType]'
GO
CREATE VIEW [dbo].[vwMapConfigurationServiceLabel_ConfigurationServiceGroupType]
AS
SELECT     TOP (100) PERCENT dbo.ConfigurationServiceLabel.Id, dbo.ConfigurationServiceLabel.Name, dbo.ConfigurationServiceLabel.CreatedBy, 
                      dbo.ConfigurationServiceLabel.CreatedOn, dbo.ConfigurationServiceLabel.ModifiedBy, dbo.ConfigurationServiceLabel.ModifiedOn, 
                      dbo.ConfigurationServiceLabel.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceLabel.ElementsKey, 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceItemId, dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId, 
                      dbo.ConfigurationServiceGroupType.Name AS ConfigurationServiceGroupTypeName, dbo.ConfigurationServiceLabel.ConfigurationServiceLabelTypeId, 
                      dbo.ConfigurationServiceLabel.ValueList, dbo.ConfigurationServiceLabel.Help, dbo.ConfigurationServiceLabel.Description, 
                      dbo.ConfigurationServiceLabelType.Name AS ConfigurationServiceLabelTypeName, dbo.ConfigurationServiceLabel.InputRequired, 
                      dbo.ConfigurationServiceItem.SortOrder AS ConfigurationServiceItemSortOrder, dbo.ConfigurationServiceLabel.SortOrder
FROM         dbo.ConfigurationServiceLabel WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceLabel.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceItem.Id = dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceItemId INNER JOIN
                      dbo.ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId = dbo.ConfigurationServiceGroupType.Id INNER JOIN
                      dbo.ConfigurationServiceLabelType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceLabelTypeId = dbo.ConfigurationServiceLabelType.Id
ORDER BY ConfigurationServiceItemName, dbo.ConfigurationServiceLabel.Name
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapConfigurationServiceLabelValue]'
GO
ALTER VIEW dbo.vwMapConfigurationServiceLabelValue
AS
SELECT     dbo.ConfigurationServiceLabelValue.Id, dbo.ConfigurationServiceLabelValue.CreatedBy, dbo.ConfigurationServiceLabelValue.CreatedOn, 
                      dbo.ConfigurationServiceLabelValue.ModifiedBy, dbo.ConfigurationServiceLabelValue.ModifiedOn, dbo.ConfigurationServiceLabelValue.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceLabelValue.ConfigurationServiceGroupId, 
                      dbo.ConfigurationServiceLabel.Id AS ConfigurationServiceLabelId, dbo.ConfigurationServiceLabel.Name AS ConfigurationServiceLabelName, 
                      dbo.ConfigurationServiceLabelValue.Value, dbo.ConfigurationServiceLabel.ConfigurationServiceLabelTypeId, 
                      dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName
FROM         dbo.ConfigurationServiceLabelValue WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceLabel WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabel.Id = dbo.ConfigurationServiceLabelValue.ConfigurationServiceLabelId INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceLabelValue.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON dbo.ConfigurationServiceLabel.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapConfigurationServiceGroup]'
GO

ALTER VIEW [dbo].[vwMapConfigurationServiceGroup]
AS
SELECT     dbo.ConfigurationServiceGroup.Id, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceGroupStatus.Name AS ConfigurationServiceGroupStatusName, dbo.Person.Name AS PersonName, 
                      dbo.ConfigurationServiceGroup.CreatedBy, dbo.ConfigurationServiceGroup.CreatedOn, dbo.ConfigurationServiceGroup.ModifiedBy, 
                      dbo.ConfigurationServiceGroup.ModifiedOn, dbo.ConfigurationServiceGroup.RowStatusId, dbo.ConfigurationServiceGroup.Description, 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupStatusId, dbo.ConfigurationServiceGroup.ConfigurationServiceGroupTypeId, 
                      dbo.ConfigurationServiceGroup.OwnerId, dbo.ConfigurationServiceGroupType.Name AS ConfigurationServiceGroupTypeName, 
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
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceApplication]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Version_IsTrimmed] CHECK ((len(ltrim(rtrim([Version])))=len([Version])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceGroup]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroup_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroup_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroup_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroup_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceGroupSelector]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupSelector_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupSelector_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupSelector_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupSelector_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceGroupStatus]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceItem]'
GO
ALTER TABLE [dbo].[ConfigurationServiceItem] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceItem_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceItem] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceItem_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceItem] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceItem_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceLabel]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabel_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabel_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabel_Name_MinLen] CHECK ((len(rtrim([Name]))>=(5)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceLabelType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabelType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabelType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabelType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabelType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroup]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceApplication] FOREIGN KEY ([ConfigurationServiceApplicationId]) REFERENCES [dbo].[ConfigurationServiceApplication] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroup_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceGroupStatus] FOREIGN KEY ([ConfigurationServiceGroupStatusId]) REFERENCES [dbo].[ConfigurationServiceGroupStatus] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceGroupType] FOREIGN KEY ([ConfigurationServiceGroupTypeId]) REFERENCES [dbo].[ConfigurationServiceGroupType] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroup_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceApplication]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceApplication_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroup_Tag]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_Tag] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroup_Tag_ConfigurationServiceGroup] FOREIGN KEY ([ConfigurationServiceGroupId]) REFERENCES [dbo].[ConfigurationServiceGroup] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroup_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroupSelector]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupSelector_ConfigurationServiceGroup] FOREIGN KEY ([ConfigurationServiceGroupId]) REFERENCES [dbo].[ConfigurationServiceGroup] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroupSelector_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceLabelValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelValue] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceLabelValue_ConfigurationServiceGroup] FOREIGN KEY ([ConfigurationServiceGroupId]) REFERENCES [dbo].[ConfigurationServiceGroup] ([Id]),
CONSTRAINT [FK_ConfigurationServiceLabelValue_ConfigurationServiceLabel] FOREIGN KEY ([ConfigurationServiceLabelId]) REFERENCES [dbo].[ConfigurationServiceLabel] ([Id]),
CONSTRAINT [FK_ConfigurationServiceLabelValue_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupSelector_QueryParameterValue_ConfigurationServiceGroupSelector] FOREIGN KEY ([ConfigurationServiceGroupSelectorId]) REFERENCES [dbo].[ConfigurationServiceGroupSelector] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroupSelector_QueryParameterValue_QueryParameterValue] FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroupStatus]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupStatus_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceItem_ConfigurationServiceGroupType_ConfigurationServicedGroupType] FOREIGN KEY ([ConfigurationServiceGroupTypeId]) REFERENCES [dbo].[ConfigurationServiceGroupType] ([Id]),
CONSTRAINT [FK_ConfigurationServiceItem_ConfigurationServiceItem_ConfigurationServiceGroupType] FOREIGN KEY ([ConfigurationServiceItemId]) REFERENCES [dbo].[ConfigurationServiceItem] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[QueryParameter_ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameter_ConfigurationServiceGroupType_ConfigurationServicedGroupType] FOREIGN KEY ([ConfigurationServiceGroupTypeId]) REFERENCES [dbo].[ConfigurationServiceGroupType] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceLabel]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceLabel_ConfigurationServiceItem] FOREIGN KEY ([ConfigurationServiceItemId]) REFERENCES [dbo].[ConfigurationServiceItem] ([Id]),
CONSTRAINT [FK_ConfigurationServiceLabel_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_ConfigurationServiceLabel_ConfigurationServiceLabelType] FOREIGN KEY ([ConfigurationServiceLabelTypeId]) REFERENCES [dbo].[ConfigurationServiceLabelType] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceItem]'
GO
ALTER TABLE [dbo].[ConfigurationServiceItem] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceItem_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceLabelType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelType] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceLabelType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
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
