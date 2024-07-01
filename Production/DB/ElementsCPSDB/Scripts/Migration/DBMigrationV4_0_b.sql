/*
Run this script on:

        (local).ElementsCPSDBTest    -  This database will be modified

to synchronize it with:

        (local).ElementsCPSDB

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.50.10 from Red Gate Software Ltd at 10/24/2011 1:36:54 PM

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
PRINT N'Creating [dbo].[WorkflowModuleVersion]'
GO
CREATE TABLE [dbo].[WorkflowModuleVersion]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[WorkflowModuleCategoryId] [int] NOT NULL,
[WorkflowModuleSubCategoryId] [int] NOT NULL,
[VersionMajor] [int] NOT NULL,
[VersionMinor] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1))
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_WorkflowModuleVersion] on [dbo].[WorkflowModuleVersion]'
GO
ALTER TABLE [dbo].[WorkflowModuleVersion] ADD CONSTRAINT [PK_WorkflowModuleVersion] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_WorkflowModuleVersion_CategoryIdSubCategoryIdVersionMajor] on [dbo].[WorkflowModuleVersion]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_WorkflowModuleVersion_CategoryIdSubCategoryIdVersionMajor] ON [dbo].[WorkflowModuleVersion] ([WorkflowModuleCategoryId], [WorkflowModuleSubCategoryId], [VersionMajor])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowModuleSubCategory]'
GO
CREATE TABLE [dbo].[WorkflowModuleSubCategory]
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
PRINT N'Creating primary key [PK_WorkflowModuleSubCategory] on [dbo].[WorkflowModuleSubCategory]'
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] ADD CONSTRAINT [PK_WorkflowModuleSubCategory] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowModuleCategory]'
GO
CREATE TABLE [dbo].[WorkflowModuleCategory]
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
PRINT N'Creating primary key [PK_WorkflowModuleCategory] on [dbo].[WorkflowModuleCategory]'
GO
ALTER TABLE [dbo].[WorkflowModuleCategory] ADD CONSTRAINT [PK_WorkflowModuleCategory] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] ALTER COLUMN [ConfigurationServiceApplicationId] [int] NOT NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[ConfigurationServiceApplication]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] ALTER COLUMN [ConfigurationServiceApplicationTypeId] [int] NOT NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_ConfigurationServiceApplication_ElementsKey] on [dbo].[ConfigurationServiceApplication]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_ConfigurationServiceApplication_ElementsKey] ON [dbo].[ConfigurationServiceApplication] ([ElementsKey])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[ProxyURL_HasDuplicateQueryParameterValuesDelimitedList]'
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
ALTER FUNCTION [dbo].[ProxyURL_HasDuplicateQueryParameterValuesDelimitedList]
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
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Workflow_Tag]'
GO
CREATE TABLE [dbo].[Workflow_Tag]
(
[WorkflowId] [int] NOT NULL,
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
PRINT N'Creating primary key [PK_Workflow_Tag] on [dbo].[Workflow_Tag]'
GO
ALTER TABLE [dbo].[Workflow_Tag] ADD CONSTRAINT [PK_Workflow_Tag] PRIMARY KEY NONCLUSTERED  ([WorkflowId], [TagId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowApplicationType]'
GO
CREATE TABLE [dbo].[WorkflowApplicationType]
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
PRINT N'Creating primary key [PK_WorkflowApplicationType] on [dbo].[WorkflowApplicationType]'
GO
ALTER TABLE [dbo].[WorkflowApplicationType] ADD CONSTRAINT [PK_WorkflowApplicationType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowApplication]'
GO
CREATE TABLE [dbo].[WorkflowApplication]
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
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[WorkflowApplicationTypeId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_WorkflowApplication] on [dbo].[WorkflowApplication]'
GO
ALTER TABLE [dbo].[WorkflowApplication] ADD CONSTRAINT [PK_WorkflowApplication] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_WorkflowApplication_ElementsKey] on [dbo].[WorkflowApplication]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_WorkflowApplication_ElementsKey] ON [dbo].[WorkflowApplication] ([ElementsKey])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceLabelValueImport]'
GO
CREATE TABLE [dbo].[ConfigurationServiceLabelValueImport]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[ItemName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LabelName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LabelValue] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConfigurationServiceGroupImportId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceLabelValueImport] on [dbo].[ConfigurationServiceLabelValueImport]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelValueImport] ADD CONSTRAINT [PK_ConfigurationServiceLabelValueImport] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroupImport_GetLabelValueList]'
GO

-- User Defined Function
/*
Usage Examples:
	SELECT [ConfigurationServiceGroupImport].[Id], [dbo].[ConfigurationServiceGroup_GetLabelValueList]([ConfigurationServiceGroupImport].[Id], ', ', 1, 1) FROM [dbo].[ConfigurationServiceGroupImport]
	SELECT dbo.ConfigurationServiceGroupImport_GetLabelValueList (1 ', ', 1, 1)
	SELECT TOP 10 [ConfigurationServiceGroupImport].[Id], dbo.ConfigurationServiceGroup_GetLabelValueList ([ConfigurationServiceGroupImport].[Id], '/', 1, 1) FROM [dbo].[ConfigurationServiceGroupImport] ORDER BY [ConfigurationServiceGroupImport].[Id] DESC
*/
-- =============================================
-- Author:		Robert Mukai
-- Create date: 10/6/11
-- Description:	Returns a delimited list of LabelValues for a specified ConfigurationServiceGroupImport.
-- =============================================
CREATE FUNCTION [dbo].[ConfigurationServiceGroupImport_GetLabelValueList]
    (
     @ConfigurationServiceGroupImportId INT,
     @Delimiter VARCHAR(2),
     @RowStatusId INT
    )
RETURNS NVARCHAR(MAX)
AS BEGIN
    DECLARE @LabelValueList NVARCHAR(MAX)
	DECLARE @TempLabelValueList TABLE (LabelValue NVARCHAR(256))
    SELECT
        @LabelValueList = NULL
        
	--Build the delimited string of LabelValue
	INSERT INTO @TempLabelValueList(LabelValue)
    SELECT
        DISTINCT [LabelValue]
    FROM
		ConfigurationServiceLabelValueImport WITH (NOLOCK)
    WHERE
        ((ConfigurationServiceGroupImportId = @ConfigurationServiceGroupImportId) 
        AND (@RowStatusId IS NULL
             OR [RowStatusId] = @RowStatusId))
    ORDER BY
        LabelValue
	SELECT @labelValueList = COALESCE(@LabelValueList + @Delimiter, '') + [LabelValue]
	FROM @TempLabelValueList
    RETURN @LabelValueList
   END

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowModule_WorkflowCondition]'
GO
CREATE TABLE [dbo].[WorkflowModule_WorkflowCondition]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[WorkflowConditionId] [int] NOT NULL,
[WorkflowModuleId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_WorkflowModule_WorkflowConditionType] on [dbo].[WorkflowModule_WorkflowCondition]'
GO
ALTER TABLE [dbo].[WorkflowModule_WorkflowCondition] ADD CONSTRAINT [PK_WorkflowModule_WorkflowConditionType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowModule]'
GO
CREATE TABLE [dbo].[WorkflowModule]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[VersionMajor] [int] NOT NULL,
[VersionMinor] [int] NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Filename] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL,
[WorkflowModuleStatusId] [int] NULL,
[OwnerId] [int] NULL,
[WorkflowModuleCategoryId] [int] NOT NULL,
[WorkflowModuleSubCategoryId] [int] NOT NULL,
[Title] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_WorkflowModule] on [dbo].[WorkflowModule]'
GO
ALTER TABLE [dbo].[WorkflowModule] ADD CONSTRAINT [PK_WorkflowModule] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowCondition]'
GO
CREATE TABLE [dbo].[WorkflowCondition]
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
[Operator] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Value] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_WorkflowCondition] on [dbo].[WorkflowCondition]'
GO
ALTER TABLE [dbo].[WorkflowCondition] ADD CONSTRAINT [PK_WorkflowCondition] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceQueryParameterValueImport]'
GO
CREATE TABLE [dbo].[ConfigurationServiceQueryParameterValueImport]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[QueryParameterName] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[QueryParameterValue] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConfigurationServiceGroupImportId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceQueryParameterValueImport] on [dbo].[ConfigurationServiceQueryParameterValueImport]'
GO
ALTER TABLE [dbo].[ConfigurationServiceQueryParameterValueImport] ADD CONSTRAINT [PK_ConfigurationServiceQueryParameterValueImport] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroupImport]'
GO
CREATE TABLE [dbo].[ConfigurationServiceGroupImport]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceGroupTypeName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConfigurationServiceApplicationName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProductionId] [int] NULL,
[ImportMessage] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ImportStatus] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceGroupId] [int] NULL,
[ImportId] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceGroupImport] on [dbo].[ConfigurationServiceGroupImport]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] ADD CONSTRAINT [PK_ConfigurationServiceGroupImport] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ImportId] on [dbo].[ConfigurationServiceGroupImport]'
GO
CREATE NONCLUSTERED INDEX [IX_ImportId] ON [dbo].[ConfigurationServiceGroupImport] ([ImportId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowType]'
GO
CREATE TABLE [dbo].[WorkflowType]
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
PRINT N'Creating primary key [PK_WorkflowType] on [dbo].[WorkflowType]'
GO
ALTER TABLE [dbo].[WorkflowType] ADD CONSTRAINT [PK_WorkflowType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowType]'
GO
CREATE VIEW [dbo].[vwMapWorkflowType]
AS
SELECT     dbo.WorkflowType.Id, dbo.WorkflowType.Name, dbo.WorkflowType.CreatedBy, 
                      dbo.WorkflowType.CreatedOn, dbo.WorkflowType.ModifiedBy, dbo.WorkflowType.ModifiedOn, 
                      dbo.WorkflowType.RowStatusId, dbo.RowStatus.Name AS RowStatusName
FROM         dbo.WorkflowType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowType.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowSelector_QueryParameterValue]'
GO
CREATE TABLE [dbo].[WorkflowSelector_QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[WorkflowSelectorId] [int] NOT NULL,
[QueryParameterValueId] [int] NOT NULL,
[Negation] [bit] NOT NULL DEFAULT ((0))
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_WorkflowSelector_QueryParameterValue] on [dbo].[WorkflowSelector_QueryParameterValue]'
GO
ALTER TABLE [dbo].[WorkflowSelector_QueryParameterValue] ADD CONSTRAINT [PK_WorkflowSelector_QueryParameterValue] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[uspDeleteWorkflowSelectorQueryParameterValueByWorkflowSelectorIdQueryParameterId]'
GO
-- =============================================
-- Author:		Robert Mukai
-- Create date: 4/6/11
-- Description:	Delete WorkflowSelector_QueryParameterValue by WorkflowSelectorId and QueryParameterId
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteWorkflowSelectorQueryParameterValueByWorkflowSelectorIdQueryParameterId]
	@WorkflowSelectorId INT,
	@queryParameterId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE
		dbo.WorkflowSelector_QueryParameterValue
	WHERE 
		WorkflowSelectorId = @WorkflowSelectorId AND
		QueryParameterValueId IN (SELECT Id FROM QueryParameterValue WHERE QueryParameterId = @queryParameterId)
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowModule_Tag]'
GO
CREATE TABLE [dbo].[WorkflowModule_Tag]
(
[WorkflowModuleId] [int] NOT NULL,
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
PRINT N'Creating primary key [PK_WorkflowModule_Tag] on [dbo].[WorkflowModule_Tag]'
GO
ALTER TABLE [dbo].[WorkflowModule_Tag] ADD CONSTRAINT [PK_WorkflowModule_Tag] PRIMARY KEY NONCLUSTERED  ([WorkflowModuleId], [TagId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowModuleTag_Tag]'
GO
CREATE VIEW [dbo].[vwMapWorkflowModuleTag_Tag]
AS
SELECT     dbo.WorkflowModule_Tag.WorkflowModuleId, dbo.WorkflowModule_Tag.TagId, dbo.WorkflowModule_Tag.CreatedBy, dbo.WorkflowModule_Tag.CreatedOn, 
                      dbo.WorkflowModule_Tag.ModifiedBy, dbo.WorkflowModule_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.WorkflowModule_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.WorkflowModule_Tag.TagId = dbo.Tag.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowModule_GetTagList]'
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [WorkflowModule].[Id], [dbo].[WorkflowModule_GetTagList]([WorkflowModule].[Id], ', ', 1, 1) FROM [dbo].[WorkflowModule]
	SELECT dbo.WorkflowModule_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [WorkflowModule].[Id], dbo.WorkflowModule_GetTagList ([WorkflowModule].[Id], '/', 1, 1) FROM [dbo].[WorkflowModule] ORDER BY [WorkflowModule].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified WorkflowModule.
-- =============================================
CREATE FUNCTION [dbo].[WorkflowModule_GetTagList]
    (
     @WorkflowModuleId INT,
     @Delimiter VARCHAR(2),
     @TagRowStatusId INT
    )
RETURNS NVARCHAR(MAX)
AS BEGIN
    DECLARE @TagList NVARCHAR(MAX)
    SELECT
        @TagList = NULL
	--Build the delimited string of tag names
    SELECT
        @TagList = COALESCE(@TagList + @Delimiter, '') + [WorkflowModuleTag].[TagName]
    FROM
        [dbo].[vwMapWorkflowModuleTag_Tag] WorkflowModuleTag
    WHERE
        ([WorkflowModuleTag].[WorkflowModuleId] = @WorkflowModuleId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([WorkflowModuleTag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [WorkflowModuleTag].[TagName]
    RETURN @TagList
   END
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
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.CreatedOn, dbo.ConfigurationServiceGroupSelector_QueryParameterValue.CreatedBy
FROM         dbo.ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameterValue WITH (NOLOCK) ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId ON 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = dbo.QueryParameterValue.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[QueryParameter_ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] ALTER COLUMN [Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [QueryParameter_ConfigurationServiceGroupType_QueryParameterId_ConfigurationServiceGroupTypeId] on [dbo].[QueryParameter_ConfigurationServiceGroupType]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [QueryParameter_ConfigurationServiceGroupType_QueryParameterId_ConfigurationServiceGroupTypeId] ON [dbo].[QueryParameter_ConfigurationServiceGroupType] ([ConfigurationServiceGroupTypeId], [QueryParameterId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[QueryParameter_ProxyURLType]'
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] ALTER COLUMN [Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_QueryParameter_ProxyURLType_QueryParameterId_ProxyURLTypeId] on [dbo].[QueryParameter_ProxyURLType]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_QueryParameter_ProxyURLType_QueryParameterId_ProxyURLTypeId] ON [dbo].[QueryParameter_ProxyURLType] ([ProxyURLTypeId], [QueryParameterId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[QueryParameter_WorkflowType]'
GO
CREATE TABLE [dbo].[QueryParameter_WorkflowType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[WorkflowTypeId] [int] NOT NULL,
[QueryParameterId] [int] NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MaximumSelection] [int] NOT NULL DEFAULT ((0)),
[Wildcard] [bit] NOT NULL DEFAULT ((0))
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_QueryParameter_WorkflowTypeType] on [dbo].[QueryParameter_WorkflowType]'
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] ADD CONSTRAINT [PK_QueryParameter_WorkflowTypeType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_QueryParameter_WorkflowType_QueryParameterId_WorkflowTypeId] on [dbo].[QueryParameter_WorkflowType]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_QueryParameter_WorkflowType_QueryParameterId_WorkflowTypeId] ON [dbo].[QueryParameter_WorkflowType] ([WorkflowTypeId], [QueryParameterId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowStatus]'
GO
CREATE TABLE [dbo].[WorkflowStatus]
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
PRINT N'Creating primary key [PK_WorkflowStatus] on [dbo].[WorkflowStatus]'
GO
ALTER TABLE [dbo].[WorkflowStatus] ADD CONSTRAINT [PK_WorkflowStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowModule]'
GO
CREATE VIEW dbo.vwMapWorkflowModule
AS
SELECT     dbo.WorkflowModule.Id, dbo.WorkflowModule.Name, dbo.WorkflowModule.CreatedBy, dbo.WorkflowModule.CreatedOn, dbo.WorkflowModule.ModifiedBy, 
                      dbo.WorkflowModule.ModifiedOn, dbo.WorkflowModule.VersionMajor, dbo.WorkflowModule.VersionMinor, dbo.WorkflowModule.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowModule.Description, dbo.WorkflowModule.Filename, dbo.WorkflowModule.ProductionId, 
                      dbo.WorkflowModule.ValidationId, dbo.WorkflowModule.WorkflowModuleStatusId, dbo.WorkflowModule.OwnerId, dbo.WorkflowModule.WorkflowModuleCategoryId, 
                      dbo.WorkflowModule.WorkflowModuleSubCategoryId, dbo.WorkflowModule_GetTagList(dbo.WorkflowModule.Id, ', ', 1) AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.WorkflowModule_Tag WITH (NOLOCK)
                            WHERE      (dbo.WorkflowModule.Id = WorkflowModuleId)) AS TagCount, dbo.WorkflowModuleSubCategory.Name AS ModuleSubCategoryName, 
                      dbo.WorkflowModuleCategory.Name AS ModuleCategoryName, dbo.WorkflowStatus.Name AS WorkflowModuleStatusName, dbo.Person.Name AS PersonName
FROM         dbo.WorkflowModule WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowModule.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.WorkflowModuleCategory WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleCategoryId = dbo.WorkflowModuleCategory.Id INNER JOIN
                      dbo.WorkflowModuleSubCategory WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleSubCategoryId = dbo.WorkflowModuleSubCategory.Id INNER JOIN
                      dbo.WorkflowStatus WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleStatusId = dbo.WorkflowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.WorkflowModule.OwnerId = dbo.Person.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapConfigurationServiceGroupImport]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroupImport
AS
SELECT     dbo.ConfigurationServiceGroupImport.Id, dbo.ConfigurationServiceGroupImport.CreatedBy, dbo.ConfigurationServiceGroupImport.CreatedOn, 
                      dbo.ConfigurationServiceGroupImport.ModifiedBy, dbo.ConfigurationServiceGroupImport.ModifiedOn, dbo.ConfigurationServiceGroupImport.Name, 
                      dbo.ConfigurationServiceGroupImport.Description, dbo.ConfigurationServiceGroupImport.ConfigurationServiceGroupTypeName, 
                      dbo.ConfigurationServiceGroupImport.ConfigurationServiceApplicationName, dbo.ConfigurationServiceGroupImport.ProductionId, 
                      dbo.ConfigurationServiceGroupImport.ImportMessage, dbo.ConfigurationServiceGroupImport.ImportStatus, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceGroupImport.RowStatusId, dbo.ConfigurationServiceGroupImport.ConfigurationServiceGroupId, 
                      dbo.ConfigurationServiceGroupImport_GetLabelValueList(dbo.ConfigurationServiceGroupImport.Id, ', ', NULL) AS LabelValue
FROM         dbo.ConfigurationServiceGroupImport WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroupImport.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapConfigurationServiceLabelValueImport]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceLabelValueImport
AS
SELECT     dbo.ConfigurationServiceLabelValueImport.LabelName, dbo.ConfigurationServiceLabelValueImport.ItemName, dbo.ConfigurationServiceLabelValueImport.LabelValue, 
                      dbo.ConfigurationServiceLabelValueImport.ConfigurationServiceGroupImportId, dbo.RowStatus.Name, dbo.ConfigurationServiceLabelValueImport.Id, 
                      dbo.ConfigurationServiceLabelValueImport.CreatedBy, dbo.ConfigurationServiceLabelValueImport.CreatedOn, dbo.ConfigurationServiceLabelValueImport.ModifiedBy, 
                      dbo.ConfigurationServiceLabelValueImport.ModifiedOn, dbo.ConfigurationServiceLabelValueImport.RowStatusId, 
                      dbo.ConfigurationServiceGroupImport.Name AS ConfigurationServiceGroupImportName
FROM         dbo.ConfigurationServiceLabelValueImport WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceLabelValueImport.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupImport WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabelValueImport.ConfigurationServiceGroupImportId = dbo.ConfigurationServiceGroupImport.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Workflow_WorkflowModule]'
GO
CREATE TABLE [dbo].[Workflow_WorkflowModule]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[WorkflowId] [int] NOT NULL,
[WorkflowModuleId] [int] NOT NULL,
[SortOrder] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Workflow_WorkflowModule] on [dbo].[Workflow_WorkflowModule]'
GO
ALTER TABLE [dbo].[Workflow_WorkflowModule] ADD CONSTRAINT [PK_Workflow_WorkflowModule] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapConfigurationServiceApplicationType]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceApplicationType
AS
SELECT     dbo.ConfigurationServiceApplicationType.Id, dbo.ConfigurationServiceApplicationType.Name, dbo.ConfigurationServiceApplicationType.CreatedBy, 
                      dbo.ConfigurationServiceApplicationType.CreatedOn, dbo.ConfigurationServiceApplicationType.ModifiedBy, 
                      dbo.ConfigurationServiceApplicationType.ModifiedOn, dbo.ConfigurationServiceApplicationType.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName
FROM         dbo.ConfigurationServiceApplicationType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceApplicationType.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[QueryParameter_JumpstationGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] ALTER COLUMN [Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Workflow]'
GO
CREATE TABLE [dbo].[Workflow]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[WorkflowStatusId] [int] NOT NULL,
[WorkflowTypeId] [int] NOT NULL,
[WorkflowApplicationId] [int] NOT NULL,
[Offline] [bit] NOT NULL DEFAULT ((0)),
[OwnerId] [int] NOT NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL,
[VersionMajor] [int] NOT NULL,
[VersionMinor] [int] NOT NULL,
[Filename] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Workflow] on [dbo].[Workflow]'
GO
ALTER TABLE [dbo].[Workflow] ADD CONSTRAINT [PK_Workflow] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowSelector]'
GO
CREATE TABLE [dbo].[WorkflowSelector]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[WorkflowId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_WorkflowSelector] on [dbo].[WorkflowSelector]'
GO
ALTER TABLE [dbo].[WorkflowSelector] ADD CONSTRAINT [PK_WorkflowSelector] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowSelector]'
GO
CREATE VIEW [dbo].[vwMapWorkflowSelector]
AS
SELECT     dbo.WorkflowSelector.Id, dbo.WorkflowSelector.Name, dbo.WorkflowSelector.CreatedBy, 
                      dbo.WorkflowSelector.CreatedOn, dbo.WorkflowSelector.ModifiedBy, 
                      dbo.WorkflowSelector.ModifiedOn, dbo.WorkflowSelector.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.WorkflowSelector.WorkflowId
FROM         dbo.WorkflowSelector WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowSelector.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowModuleSubCategory]'
GO
CREATE VIEW [dbo].[vwMapWorkflowModuleSubCategory]
AS
SELECT     dbo.WorkflowModuleSubCategory.Id, dbo.WorkflowModuleSubCategory.Name, dbo.WorkflowModuleSubCategory.CreatedBy, 
                      dbo.WorkflowModuleSubCategory.CreatedOn, dbo.WorkflowModuleSubCategory.ModifiedBy, 
                      dbo.WorkflowModuleSubCategory.ModifiedOn, dbo.WorkflowModuleSubCategory.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName
FROM         dbo.WorkflowModuleSubCategory WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowModuleSubCategory.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowSelector_QueryParameterValue]'
GO
CREATE VIEW dbo.vwMapWorkflowSelector_QueryParameterValue
AS
SELECT     dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameterValue.Name AS QueryParameterValueName, 
                      dbo.WorkflowSelector_QueryParameterValue.Negation, dbo.QueryParameter.Id AS QueryParameterId, dbo.QueryParameterValue.Id AS QueryParameterValueId, 
                      dbo.WorkflowSelector_QueryParameterValue.Id AS WorkflowSelectorQueryParameterValueId, dbo.WorkflowSelector_QueryParameterValue.WorkflowSelectorId, 
                      dbo.WorkflowSelector_QueryParameterValue.ModifiedOn, dbo.WorkflowSelector_QueryParameterValue.ModifiedBy, 
                      dbo.WorkflowSelector_QueryParameterValue.CreatedOn, dbo.WorkflowSelector_QueryParameterValue.CreatedBy
FROM         dbo.WorkflowSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameterValue WITH (NOLOCK) ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId ON 
                      dbo.WorkflowSelector_QueryParameterValue.QueryParameterValueId = dbo.QueryParameterValue.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowApplicationType]'
GO
CREATE VIEW [dbo].[vwMapWorkflowApplicationType]
AS
SELECT     dbo.WorkflowApplicationType.Id, dbo.WorkflowApplicationType.Name, dbo.WorkflowApplicationType.CreatedBy, 
                      dbo.WorkflowApplicationType.CreatedOn, dbo.WorkflowApplicationType.ModifiedBy, 
                      dbo.WorkflowApplicationType.ModifiedOn, dbo.WorkflowApplicationType.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName
FROM         dbo.WorkflowApplicationType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowApplicationType.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowCondition]'
GO
CREATE VIEW dbo.vwMapWorkflowCondition
AS
SELECT     dbo.WorkflowCondition.Id, dbo.WorkflowCondition.Name, dbo.WorkflowCondition.CreatedBy, dbo.WorkflowCondition.CreatedOn, 
                      dbo.WorkflowCondition.ModifiedBy, dbo.WorkflowCondition.ModifiedOn, dbo.WorkflowCondition.Version, dbo.WorkflowCondition.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowCondition.Operator, dbo.WorkflowCondition.Value, dbo.WorkflowCondition.Description
FROM         dbo.WorkflowCondition WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowCondition.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapProxyURL_QueryParameterValue]'
GO
ALTER VIEW dbo.vwMapProxyURL_QueryParameterValue
AS
SELECT     dbo.ProxyURL_QueryParameterValue.Id, dbo.ProxyURL_QueryParameterValue.QueryParameterValueId, dbo.ProxyURL_QueryParameterValue.ProxyURLId, 
                      dbo.ProxyURL_QueryParameterValue.ModifiedOn, dbo.ProxyURL_QueryParameterValue.ModifiedBy, dbo.ProxyURL_QueryParameterValue.CreatedOn, 
                      dbo.ProxyURL_QueryParameterValue.CreatedBy, dbo.QueryParameterValue.Name AS QueryParameterValueName, dbo.QueryParameterValue.QueryParameterId, 
                      dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, dbo.QueryParameter.ElementsKey
FROM         dbo.QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.ProxyURL_QueryParameterValue WITH (NOLOCK) ON dbo.QueryParameterValue.Id = dbo.ProxyURL_QueryParameterValue.QueryParameterValueId INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameterValue.QueryParameterId = dbo.QueryParameter.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowModuleCategory]'
GO
CREATE VIEW [dbo].[vwMapWorkflowModuleCategory]
AS
SELECT     dbo.WorkflowModuleCategory.Id, dbo.WorkflowModuleCategory.Name, dbo.WorkflowModuleCategory.CreatedBy, 
                      dbo.WorkflowModuleCategory.CreatedOn, dbo.WorkflowModuleCategory.ModifiedBy, 
                      dbo.WorkflowModuleCategory.ModifiedOn, dbo.WorkflowModuleCategory.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName
FROM         dbo.WorkflowModuleCategory WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowModuleCategory.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[WorkflowVersion]'
GO
CREATE TABLE [dbo].[WorkflowVersion]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[VersionMajor] [int] NOT NULL,
[VersionMinor] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1))
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_WorkflowVersion] on [dbo].[WorkflowVersion]'
GO
ALTER TABLE [dbo].[WorkflowVersion] ADD CONSTRAINT [PK_WorkflowVersion] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_WorkflowVersion_VersionMajor] on [dbo].[WorkflowVersion]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_WorkflowVersion_VersionMajor] ON [dbo].[WorkflowVersion] ([VersionMajor])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowModuleVersion]'
GO
CREATE VIEW dbo.vwMapWorkflowModuleVersion
AS
SELECT     TOP (100) PERCENT dbo.RowStatus.Name, dbo.WorkflowModuleVersion.VersionMajor, dbo.WorkflowModuleVersion.VersionMinor, 
                      dbo.WorkflowModuleVersion.CreatedBy, dbo.WorkflowModuleVersion.CreatedOn, dbo.WorkflowModuleVersion.ModifiedOn, dbo.WorkflowModuleVersion.ModifiedBy, 
                      dbo.WorkflowModuleVersion.WorkflowModuleCategoryId, dbo.WorkflowModuleVersion.WorkflowModuleSubCategoryId, 
                      dbo.WorkflowModuleSubCategory.Name AS WorkflowModuleSubCategoryName, dbo.WorkflowModuleCategory.Name AS WorkflowModuleCategoryName, 
                      dbo.WorkflowModuleVersion.Id
FROM         dbo.RowStatus WITH (NOLOCK) INNER JOIN
                      dbo.WorkflowModuleVersion WITH (NOLOCK) ON dbo.RowStatus.Id = dbo.WorkflowModuleVersion.RowStatusId INNER JOIN
                      dbo.WorkflowModuleCategory WITH (NOLOCK) ON dbo.WorkflowModuleVersion.WorkflowModuleCategoryId = dbo.WorkflowModuleCategory.Id INNER JOIN
                      dbo.WorkflowModuleSubCategory WITH (NOLOCK) ON dbo.WorkflowModuleVersion.WorkflowModuleSubCategoryId = dbo.WorkflowModuleSubCategory.Id
ORDER BY WorkflowModuleCategoryName, WorkflowModuleSubCategoryName, dbo.WorkflowModuleVersion.VersionMajor
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
                      dbo.ConfigurationServiceGroupType.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceApplication.Name AS ConfigurationServiceApplicationName, dbo.ConfigurationServiceGroupType.ConfigurationServiceApplicationId, 
                      dbo.ConfigurationServiceApplication.ElementsKey
FROM         dbo.ConfigurationServiceGroupType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroupType.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceApplication WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroupType.ConfigurationServiceApplicationId = dbo.ConfigurationServiceApplication.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowTag_Tag]'
GO
CREATE VIEW [dbo].[vwMapWorkflowTag_Tag]
AS
SELECT     dbo.Workflow_Tag.WorkflowId, dbo.Workflow_Tag.TagId, dbo.Workflow_Tag.CreatedBy, dbo.Workflow_Tag.CreatedOn, 
                      dbo.Workflow_Tag.ModifiedBy, dbo.Workflow_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.Workflow_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.Workflow_Tag.TagId = dbo.Tag.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowApplication]'
GO
CREATE VIEW dbo.vwMapWorkflowApplication
AS
SELECT     dbo.WorkflowApplication.Id, dbo.WorkflowApplication.Name, dbo.WorkflowApplication.CreatedBy, dbo.WorkflowApplication.CreatedOn, 
                      dbo.WorkflowApplication.ModifiedBy, dbo.WorkflowApplication.ModifiedOn, dbo.WorkflowApplication.Version, dbo.WorkflowApplication.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowApplication.ElementsKey, dbo.WorkflowApplicationType.Name AS WorkflowApplicationTypeName
FROM         dbo.WorkflowApplication WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.WorkflowApplication.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.WorkflowApplicationType WITH (NOLOCK) ON dbo.WorkflowApplication.WorkflowApplicationTypeId = dbo.WorkflowApplicationType.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflowModule_WorkflowCondition]'
GO
CREATE VIEW dbo.vwMapWorkflowModule_WorkflowCondition
AS
SELECT     dbo.WorkflowModule.Name AS WorkflowModuleName, dbo.WorkflowModule_WorkflowCondition.WorkflowConditionId, 
                      dbo.WorkflowModule_WorkflowCondition.WorkflowModuleId, dbo.WorkflowCondition.Name AS WorkflowConditionName, 
                      dbo.WorkflowModule_WorkflowCondition.CreatedBy, dbo.WorkflowModule_WorkflowCondition.CreatedOn, 
                      dbo.WorkflowModule_WorkflowCondition.ModifiedBy, dbo.WorkflowModule_WorkflowCondition.ModifiedOn, 
                      dbo.WorkflowModule_WorkflowCondition.Id, dbo.WorkflowCondition.Version, dbo.WorkflowCondition.Description, dbo.WorkflowCondition.Operator, 
                      dbo.WorkflowCondition.Value
FROM         dbo.WorkflowCondition WITH (NOLOCK) INNER JOIN
                      dbo.WorkflowModule_WorkflowCondition WITH (NOLOCK) ON 
                      dbo.WorkflowCondition.Id = dbo.WorkflowModule_WorkflowCondition.WorkflowConditionId INNER JOIN
                      dbo.WorkflowModule WITH (NOLOCK) ON dbo.WorkflowModule_WorkflowCondition.WorkflowModuleId = dbo.WorkflowModule.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapConfigurationServiceQueryParameterValueImport]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceQueryParameterValueImport
AS
SELECT     dbo.ConfigurationServiceQueryParameterValueImport.Id, dbo.ConfigurationServiceQueryParameterValueImport.CreatedBy, 
                      dbo.ConfigurationServiceQueryParameterValueImport.CreatedOn, dbo.ConfigurationServiceQueryParameterValueImport.ModifiedBy, 
                      dbo.ConfigurationServiceQueryParameterValueImport.RowStatusId, dbo.ConfigurationServiceQueryParameterValueImport.ModifiedOn, 
                      dbo.ConfigurationServiceQueryParameterValueImport.QueryParameterName, dbo.ConfigurationServiceQueryParameterValueImport.QueryParameterValue, 
                      dbo.ConfigurationServiceQueryParameterValueImport.ConfigurationServiceGroupImportId, dbo.RowStatus.Name, 
                      dbo.ConfigurationServiceGroupImport.Name AS ConfigurationServiceGroupImportName
FROM         dbo.ConfigurationServiceQueryParameterValueImport WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceQueryParameterValueImport.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupImport WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceQueryParameterValueImport.ConfigurationServiceGroupImportId = dbo.ConfigurationServiceGroupImport.Id
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
                      dbo.ConfigurationServiceApplication.ElementsKey, dbo.ConfigurationServiceApplicationType.Name AS ConfigurationServiceApplicationTypeName
FROM         dbo.ConfigurationServiceApplication WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceApplication.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceApplicationType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceApplication.ConfigurationServiceApplicationTypeId = dbo.ConfigurationServiceApplicationType.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapQueryParameter_ConfigurationServiceGroupType]'
GO
ALTER VIEW dbo.vwMapQueryParameter_ConfigurationServiceGroupType
AS
SELECT     dbo.QueryParameter_ConfigurationServiceGroupType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceGroupType.Name, dbo.QueryParameter_ConfigurationServiceGroupType.CreatedBy, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.CreatedOn, dbo.QueryParameter_ConfigurationServiceGroupType.ModifiedBy, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.ModifiedOn, dbo.QueryParameter_ConfigurationServiceGroupType.QueryParameterId, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.Name AS QueryParameterConfigurationServiceGroupTypeName, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.Description, dbo.QueryParameter_ConfigurationServiceGroupType.MaximumSelection, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.Wildcard, dbo.QueryParameter.ElementsKey
FROM         dbo.QueryParameter_ConfigurationServiceGroupType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_ConfigurationServiceGroupType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.QueryParameter_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId = dbo.ConfigurationServiceGroupType.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapQueryParameter_ProxyURLType]'
GO
ALTER VIEW dbo.vwMapQueryParameter_ProxyURLType
AS
SELECT     dbo.QueryParameter_ProxyURLType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.ProxyURLType.Name, dbo.QueryParameter_ProxyURLType.CreatedBy, 
                      dbo.QueryParameter_ProxyURLType.CreatedOn, dbo.QueryParameter_ProxyURLType.ModifiedBy, dbo.QueryParameter_ProxyURLType.ModifiedOn, 
                      dbo.QueryParameter_ProxyURLType.QueryParameterId, dbo.QueryParameter_ProxyURLType.ProxyURLTypeId, 
                      dbo.QueryParameter_ProxyURLType.Name AS QueryParameterProxyURLTypeName, dbo.QueryParameter_ProxyURLType.Description
FROM         dbo.QueryParameter_ProxyURLType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_ProxyURLType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ProxyURLType WITH (NOLOCK) ON dbo.QueryParameter_ProxyURLType.ProxyURLTypeId = dbo.ProxyURLType.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapConfigurationServiceLabel_ConfigurationServiceGroupType]'
GO
ALTER VIEW dbo.vwMapConfigurationServiceLabel_ConfigurationServiceGroupType
AS
SELECT     TOP (100) PERCENT dbo.ConfigurationServiceLabel.Id, dbo.ConfigurationServiceLabel.Name, dbo.ConfigurationServiceLabel.CreatedBy, 
                      dbo.ConfigurationServiceLabel.CreatedOn, dbo.ConfigurationServiceLabel.ModifiedBy, dbo.ConfigurationServiceLabel.ModifiedOn, 
                      dbo.ConfigurationServiceLabel.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceLabel.ElementsKey, 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceItemId, dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId, 
                      dbo.ConfigurationServiceGroupType.Name AS ConfigurationServiceGroupTypeName, dbo.ConfigurationServiceLabel.ConfigurationServiceLabelTypeId, 
                      dbo.ConfigurationServiceLabel.ValueList, dbo.ConfigurationServiceLabel.Help, dbo.ConfigurationServiceLabel.Description, 
                      dbo.ConfigurationServiceLabelType.Name AS ConfigurationServiceLabelTypeName, dbo.ConfigurationServiceLabel.InputRequired, 
                      dbo.ConfigurationServiceItem.SortOrder AS ConfigurationServiceItemSortOrder, dbo.ConfigurationServiceLabel.SortOrder, 
                      dbo.ConfigurationServiceItem.ElementsKey AS ConfigurationServiceItemElementsKey
FROM         dbo.ConfigurationServiceLabel WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON dbo.ConfigurationServiceLabel.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id INNER JOIN
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
PRINT N'Creating [dbo].[vwMapQueryParameter_WorkflowType]'
GO
CREATE VIEW dbo.vwMapQueryParameter_WorkflowType
AS
SELECT     dbo.QueryParameter_WorkflowType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowType.Name, dbo.QueryParameter_WorkflowType.CreatedBy, dbo.QueryParameter_WorkflowType.CreatedOn, 
                      dbo.QueryParameter_WorkflowType.ModifiedBy, dbo.QueryParameter_WorkflowType.ModifiedOn, dbo.QueryParameter_WorkflowType.QueryParameterId, 
                      dbo.QueryParameter_WorkflowType.WorkflowTypeId, dbo.QueryParameter_WorkflowType.Name AS QueryParameterWorkflowTypeName, 
                      dbo.QueryParameter_WorkflowType.Description, dbo.QueryParameter_WorkflowType.MaximumSelection, dbo.QueryParameter_WorkflowType.Wildcard
FROM         dbo.QueryParameter_WorkflowType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_WorkflowType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.WorkflowType WITH (NOLOCK) ON dbo.QueryParameter_WorkflowType.WorkflowTypeId = dbo.WorkflowType.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflow_WorkflowModule]'
GO
CREATE VIEW dbo.vwMapWorkflow_WorkflowModule
AS
SELECT     dbo.Workflow_WorkflowModule.SortOrder, dbo.WorkflowModule.Id, dbo.WorkflowModule.Name, dbo.Workflow_WorkflowModule.WorkflowModuleId, 
                      dbo.Workflow_WorkflowModule.WorkflowId, dbo.WorkflowModule.VersionMajor, dbo.WorkflowModule.VersionMinor, dbo.Workflow_WorkflowModule.ModifiedOn, 
                      dbo.Workflow_WorkflowModule.ModifiedBy, dbo.Workflow_WorkflowModule.CreatedOn, dbo.Workflow_WorkflowModule.CreatedBy, dbo.WorkflowModule.Filename, 
                      dbo.WorkflowModuleSubCategory.Name AS ModuleSubCategoryName, dbo.WorkflowModuleCategory.Name AS ModuleCategoryName, 
                      dbo.Workflow.ValidationId AS WorkflowValidationId, dbo.Workflow.ProductionId AS WorkflowProductionId, dbo.Workflow.WorkflowStatusId, 
                      dbo.WorkflowModule.Description, dbo.WorkflowModule.Title
FROM         dbo.Workflow_WorkflowModule WITH (NOLOCK) INNER JOIN
                      dbo.WorkflowModule WITH (NOLOCK) ON dbo.Workflow_WorkflowModule.WorkflowModuleId = dbo.WorkflowModule.Id INNER JOIN
                      dbo.WorkflowModuleCategory WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleCategoryId = dbo.WorkflowModuleCategory.Id INNER JOIN
                      dbo.WorkflowModuleSubCategory WITH (NOLOCK) ON dbo.WorkflowModule.WorkflowModuleSubCategoryId = dbo.WorkflowModuleSubCategory.Id INNER JOIN
                      dbo.Workflow WITH (NOLOCK) ON dbo.Workflow_WorkflowModule.WorkflowId = dbo.Workflow.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Workflow_GetTagList]'
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [Workflow].[Id], [dbo].[Workflow_GetTagList]([Workflow].[Id], ', ', 1, 1) FROM [dbo].[Workflow]
	SELECT dbo.Workflow_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [Workflow].[Id], dbo.Workflow_GetTagList ([Workflow].[Id], '/', 1, 1) FROM [dbo].[Workflow] ORDER BY [Workflow].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified Workflow.
-- =============================================
CREATE FUNCTION [dbo].[Workflow_GetTagList]
    (
     @WorkflowId INT,
     @Delimiter VARCHAR(2),
     @TagRowStatusId INT
    )
RETURNS NVARCHAR(MAX)
AS BEGIN
    DECLARE @TagList NVARCHAR(MAX)
    SELECT
        @TagList = NULL
	--Build the delimited string of tag names
    SELECT
        @TagList = COALESCE(@TagList + @Delimiter, '') + [Workflowtag].[TagName]
    FROM
        [dbo].[vwMapWorkflowTag_Tag] Workflowtag
    WHERE
        ([Workflowtag].[WorkflowId] = @WorkflowId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([Workflowtag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [Workflowtag].[TagName]
    RETURN @TagList
   END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapQueryParameter_JumpstationGroupType]'
GO
ALTER VIEW [dbo].[vwMapQueryParameter_JumpstationGroupType]
AS
SELECT     dbo.QueryParameter_JumpstationGroupType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.JumpstationGroupType.Name, dbo.QueryParameter_JumpstationGroupType.CreatedBy, 
                      dbo.QueryParameter_JumpstationGroupType.CreatedOn, dbo.QueryParameter_JumpstationGroupType.ModifiedBy, dbo.QueryParameter_JumpstationGroupType.ModifiedOn, 
                      dbo.QueryParameter_JumpstationGroupType.QueryParameterId, dbo.QueryParameter_JumpstationGroupType.JumpstationGroupTypeId,
                      dbo.QueryParameter_JumpstationGroupType.Name AS QueryParameterJumpstationGroupTypeName, 
                      dbo.QueryParameter_JumpstationGroupType.Description
FROM         dbo.QueryParameter_JumpstationGroupType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_JumpstationGroupType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.JumpstationGroupType ON dbo.QueryParameter_JumpstationGroupType.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Workflow_GetQueryParameterValueList]'
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
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflow]'
GO
CREATE VIEW [dbo].[vwMapWorkflow]
AS
SELECT     dbo.Workflow.Id, dbo.RowStatus.Name AS RowStatusName, dbo.WorkflowStatus.Name AS WorkflowStatusName, dbo.Person.Name AS PersonName, 
                      dbo.Workflow.CreatedBy, dbo.Workflow.CreatedOn, dbo.Workflow.ModifiedBy, dbo.Workflow.ModifiedOn, dbo.Workflow.RowStatusId, dbo.Workflow.Description, 
                      dbo.Workflow.WorkflowStatusId, dbo.Workflow.WorkflowTypeId, dbo.Workflow.OwnerId, dbo.WorkflowType.Name AS WorkflowTypeName, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 203, ', ', NULL) AS ReleaseQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 101, ', ', NULL) AS CountryQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 107, ', ', NULL) AS PlatformQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 106, ', ', NULL) AS BrandQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 204, ', ', NULL) AS ModelNumberQueryParameterValue, 
                      dbo.Workflow_GetQueryParameterValueList(dbo.Workflow.Id, 201, ', ', NULL) AS SubBrandQueryParameterValue, dbo.Workflow_GetTagList(dbo.Workflow.Id, ', ', 1) 
                      AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.Workflow_Tag WITH (NOLOCK)
                            WHERE      (dbo.Workflow.Id = WorkflowId)) AS TagCount, dbo.Workflow.ProductionId, dbo.Workflow.ValidationId, dbo.Workflow.Name, 
                      dbo.Workflow.WorkflowApplicationId, dbo.WorkflowApplication.Name AS WorkflowApplicationName, dbo.Workflow.Offline, dbo.Workflow.VersionMajor, 
                      dbo.Workflow.VersionMinor
FROM         dbo.Workflow WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.Workflow.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.Workflow.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.WorkflowStatus WITH (NOLOCK) ON dbo.Workflow.WorkflowStatusId = dbo.WorkflowStatus.Id INNER JOIN
                      dbo.WorkflowType WITH (NOLOCK) ON dbo.Workflow.WorkflowTypeId = dbo.WorkflowType.Id INNER JOIN
                      dbo.WorkflowApplication WITH (NOLOCK) ON dbo.Workflow.WorkflowApplicationId = dbo.WorkflowApplication.Id

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ConfigurationServiceApplicationType] on [dbo].[ConfigurationServiceApplicationType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplicationType] ADD CONSTRAINT [PK_ConfigurationServiceApplicationType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProductionId] on [dbo].[ConfigurationServiceGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_ProductionId] ON [dbo].[ConfigurationServiceGroup] ([ProductionId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_ProxyURLType_ElementsKey] on [dbo].[ProxyURLType]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_ProxyURLType_ElementsKey] ON [dbo].[ProxyURLType] ([ElementsKey])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceApplicationType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplicationType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplicationType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplicationType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplicationType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ConfigurationServiceGroupImport]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupImport_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupImport_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupImport_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupImport_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[QueryParameter_ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_ConfigurationServiceGroupType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_ConfigurationServiceGroupType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_ConfigurationServiceGroupType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[QueryParameter_ProxyURLType]'
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_ProxyURLType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_ProxyURLType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_ProxyURLType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[QueryParameter_WorkflowType]'
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_WorkflowType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_WorkflowType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_WorkflowType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Workflow]'
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD CONSTRAINT [CK_Workflow_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD CONSTRAINT [CK_Workflow_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD CONSTRAINT [CK_Workflow_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD CONSTRAINT [CK_Workflow_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowApplication]'
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplication_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplication_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplication_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplication_Version_IsTrimmed] CHECK ((len(ltrim(rtrim([Version])))=len([Version])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplication_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowApplicationType]'
GO
ALTER TABLE [dbo].[WorkflowApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplicationType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplicationType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplicationType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowApplicationType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowCondition]'
GO
ALTER TABLE [dbo].[WorkflowCondition] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowCondition_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowCondition] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowCondition_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowCondition] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowCondition_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowCondition] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowCondition_Version_IsTrimmed] CHECK ((len(ltrim(rtrim([Version])))=len([Version])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowCondition] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowCondition_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowModule]'
GO
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModule_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModule_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModule_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModule_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowModuleCategory]'
GO
ALTER TABLE [dbo].[WorkflowModuleCategory] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModuleCategory_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModuleCategory] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModuleCategory_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModuleCategory] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModuleCategory_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModuleCategory] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModuleCategory_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowModuleSubCategory]'
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModuleSubCategory_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModuleSubCategory_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModuleSubCategory_Name_MinLen] CHECK ((len(rtrim([Name]))>=(2)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowModuleSubCategory_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowSelector]'
GO
ALTER TABLE [dbo].[WorkflowSelector] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowSelector_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowSelector] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowSelector_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowSelector] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowSelector_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowSelector] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowSelector_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowStatus]'
GO
ALTER TABLE [dbo].[WorkflowStatus] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowStatus] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowStatus] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowStatus] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[WorkflowType]'
GO
ALTER TABLE [dbo].[WorkflowType] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowType] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowType] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[WorkflowType] WITH NOCHECK ADD CONSTRAINT [CK_WorkflowType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceApplication]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceApplication_ConfigurationServiceApplicationType] FOREIGN KEY ([ConfigurationServiceApplicationTypeId]) REFERENCES [dbo].[ConfigurationServiceApplicationType] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceApplicationType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplicationType] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceApplicationType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceLabelValueImport]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelValueImport] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceLabelValueImport_ConfigurationServiceGroupImport] FOREIGN KEY ([ConfigurationServiceGroupImportId]) REFERENCES [dbo].[ConfigurationServiceGroupImport] ([Id]),
CONSTRAINT [FK_ConfigurationServiceLabelValueImport_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceQueryParameterValueImport]'
GO
ALTER TABLE [dbo].[ConfigurationServiceQueryParameterValueImport] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceQueryParameterValueImport_ConfigurationServiceGroupImport] FOREIGN KEY ([ConfigurationServiceGroupImportId]) REFERENCES [dbo].[ConfigurationServiceGroupImport] ([Id]),
CONSTRAINT [FK_ConfigurationServiceQueryParameterValueImport_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroupImport]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupImport_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupType_ConfigurationServiceApplication] FOREIGN KEY ([ConfigurationServiceApplicationId]) REFERENCES [dbo].[ConfigurationServiceApplication] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[QueryParameter_WorkflowType]'
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameter_WorkflowType_ConfigurationServicedGroupType] FOREIGN KEY ([WorkflowTypeId]) REFERENCES [dbo].[WorkflowType] ([Id]),
CONSTRAINT [FK_QueryParameter_QueryParameter_WorkflowType] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Workflow_Tag]'
GO
ALTER TABLE [dbo].[Workflow_Tag] WITH NOCHECK ADD
CONSTRAINT [FK_Workflow_Tag_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflow] ([Id]),
CONSTRAINT [FK_Workflow_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Workflow_WorkflowModule]'
GO
ALTER TABLE [dbo].[Workflow_WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_Workflow_WorkflowModule_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflow] ([Id]),
CONSTRAINT [FK_Workflow_WorkflowModule_WorkflowModule] FOREIGN KEY ([WorkflowModuleId]) REFERENCES [dbo].[WorkflowModule] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowSelector]'
GO
ALTER TABLE [dbo].[WorkflowSelector] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowSelector_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflow] ([Id]),
CONSTRAINT [FK_WorkflowSelector_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Workflow]'
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD
CONSTRAINT [FK_Workflow_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_Workflow_WorkflowStatus] FOREIGN KEY ([WorkflowStatusId]) REFERENCES [dbo].[WorkflowStatus] ([Id]),
CONSTRAINT [FK_Workflow_WorkflowType] FOREIGN KEY ([WorkflowTypeId]) REFERENCES [dbo].[WorkflowType] ([Id]),
CONSTRAINT [FK_Workflow_WorkflowApplication] FOREIGN KEY ([WorkflowApplicationId]) REFERENCES [dbo].[WorkflowApplication] ([Id]),
CONSTRAINT [FK_Workflow_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowApplication]'
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowApplication_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_WorkflowApplication_WorkflowApplicationType] FOREIGN KEY ([WorkflowApplicationTypeId]) REFERENCES [dbo].[WorkflowApplicationType] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowApplicationType]'
GO
ALTER TABLE [dbo].[WorkflowApplicationType] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowApplicationType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModule_WorkflowCondition]'
GO
ALTER TABLE [dbo].[WorkflowModule_WorkflowCondition] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_WorkflowCondition_ConfigurationServicedGroupType] FOREIGN KEY ([WorkflowConditionId]) REFERENCES [dbo].[WorkflowCondition] ([Id]),
CONSTRAINT [FK_WorkflowModule_WorkflowModule_WorkflowCondition] FOREIGN KEY ([WorkflowModuleId]) REFERENCES [dbo].[WorkflowModule] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowCondition]'
GO
ALTER TABLE [dbo].[WorkflowCondition] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowCondition_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModule_Tag]'
GO
ALTER TABLE [dbo].[WorkflowModule_Tag] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_Tag_WorkflowModule] FOREIGN KEY ([WorkflowModuleId]) REFERENCES [dbo].[WorkflowModule] ([Id]),
CONSTRAINT [FK_WorkflowModule_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModule]'
GO
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_WorkflowModule_WorkflowStatus] FOREIGN KEY ([WorkflowModuleStatusId]) REFERENCES [dbo].[WorkflowStatus] ([Id]),
CONSTRAINT [FK_WorkflowModule_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id]),
CONSTRAINT [FK_WorkflowModule_WorkflowModuleCategory] FOREIGN KEY ([WorkflowModuleCategoryId]) REFERENCES [dbo].[WorkflowModuleCategory] ([Id]),
CONSTRAINT [FK_WorkflowModule_WorkflowModuleSubCategory] FOREIGN KEY ([WorkflowModuleSubCategoryId]) REFERENCES [dbo].[WorkflowModuleSubCategory] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModuleCategory]'
GO
ALTER TABLE [dbo].[WorkflowModuleCategory] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModuleCategory_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModuleSubCategory]'
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModuleSubCategory_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModuleVersion]'
GO
ALTER TABLE [dbo].[WorkflowModuleVersion] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModuleVersion_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowSelector_QueryParameterValue]'
GO
ALTER TABLE [dbo].[WorkflowSelector_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowSelector_QueryParameterValue_WorkflowSelector] FOREIGN KEY ([WorkflowSelectorId]) REFERENCES [dbo].[WorkflowSelector] ([Id]),
CONSTRAINT [FK_WorkflowSelector_QueryParameterValue_QueryParameterValue] FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowStatus]'
GO
ALTER TABLE [dbo].[WorkflowStatus] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowStatus_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowType]'
GO
ALTER TABLE [dbo].[WorkflowType] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowVersion]'
GO
ALTER TABLE [dbo].[WorkflowVersion] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowVersion_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModuleVersion]'
GO
ALTER TABLE [dbo].[WorkflowModuleVersion] ADD
CONSTRAINT [FK_WorkflowModuleVersion_WorkflowModuleCategory] FOREIGN KEY ([WorkflowModuleCategoryId]) REFERENCES [dbo].[WorkflowModuleCategory] ([Id]),
CONSTRAINT [FK_WorkflowModuleVersion_WorkflowModuleSubCategory] FOREIGN KEY ([WorkflowModuleSubCategoryId]) REFERENCES [dbo].[WorkflowModuleSubCategory] ([Id])
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
