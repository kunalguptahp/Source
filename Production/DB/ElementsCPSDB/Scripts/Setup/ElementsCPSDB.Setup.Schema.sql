/*
Run this script on:

        (local).ElementsCPSDBEmpty    -  This database will be modified

to synchronize it with:

        (local).ElementsCPSDB

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.50.10 from Red Gate Software Ltd at 8/10/2012 11:54:15 AM

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
PRINT N'Creating [dbo].[SplitStringByDelimiter]'
GO
SET ANSI_NULLS OFF
GO
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a single-column table containing the values obtained from splitting a delimited string.
-- Usage Example:
--SELECT
--    t1.[ItemIndex],
--    t1.[ItemValue] AS value1,
--    t2.[ItemValue] AS value2
--FROM
--    [ElementsCPSDB].[dbo].[SplitStringByDelimiter]('1,2,8,9', ',') AS t1
--INNER JOIN (
--            SELECT * FROM [ElementsCPSDB] . [dbo] . [SplitStringByDelimiter] ('1,6,7,8,9', ',')
--           ) AS t2
--    ON cast(t1.ItemValue as int) < cast(t2.ItemValue as int)
-- =============================================
CREATE FUNCTION [dbo].[SplitStringByDelimiter]
    (
     @DelimitedString VARCHAR(MAX),
     @Delimiter CHAR(1)
    )
RETURNS @ListValues TABLE
    (
     ItemIndex INTEGER IDENTITY,
     ItemValue VARCHAR(MAX)
    )
AS BEGIN
    DECLARE
        @Value VARCHAR(MAX),
        @StringIndex INT
    SET @Delimiter = LTRIM(RTRIM(@Delimiter))
    SET @DelimitedString = LTRIM(RTRIM(@DelimitedString)) + @Delimiter
    SET @StringIndex = CHARINDEX(@Delimiter, @DelimitedString, 1)
    IF REPLACE(@DelimitedString, @Delimiter, '') <> '' 
        BEGIN
            WHILE @StringIndex > 0
                BEGIN
                    SET @Value = LTRIM(RTRIM(LEFT(@DelimitedString, @StringIndex - 1)))
                    IF @Value <> '' 
                        BEGIN
                            INSERT INTO
                                @ListValues (ItemValue)
                            VALUES
                                (@Value) --Use Appropriate conversion
                        END
                    SET @DelimitedString = RIGHT(@DelimitedString, LEN(@DelimitedString) - @StringIndex)
                    SET @StringIndex = CHARINDEX(@Delimiter, @DelimitedString, 1)
                END
        END	
    RETURN
   END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[GetValueFromDelimitedString]'
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/28/08
-- Description:	Returns a single value (specified by index) from a set of values in a delimited string.
-- =============================================
CREATE FUNCTION [dbo].[GetValueFromDelimitedString]
    (
     @DelimitedString VARCHAR(MAX),
     @Delimiter CHAR(1),
     @ItemIndex INT
    )
RETURNS VARCHAR(MAX)
AS BEGIN
    DECLARE @ItemValue VARCHAR(MAX)
    SELECT
        @ItemValue = [t].[ItemValue]
    FROM
        [dbo].[SplitStringByDelimiter](@DelimitedString, @Delimiter) t
    WHERE
        [t].[ItemIndex] = @ItemIndex
    RETURN @ItemValue
   END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Log]'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE TABLE [dbo].[Log]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CreatedAt] [datetime] NOT NULL DEFAULT (getdate()),
[Date] [datetime] NULL,
[UtcDate] [datetime] NULL,
[Severity] [varchar] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserIdentity] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserWebIdentity] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Logger] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Location] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[WebSessionId] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProcessThread] [int] NULL,
[MachineName] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProcessorCount] [smallint] NULL,
[OSVersion] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ClrVersion] [varchar] (32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AllocatedMemory] [int] NULL,
[WorkingMemory] [int] NULL,
[ProcessUser] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProcessUserInteractive] [bit] NULL,
[ProcessUptime] [bigint] NULL,
[Message] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Exception] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StackTrace] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Log] on [dbo].[Log]'
GO
ALTER TABLE [dbo].[Log] ADD CONSTRAINT [PK_Log] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating primary key [PK_JumpstationDomain] on [dbo].[JumpstationDomain]'
GO
ALTER TABLE [dbo].[JumpstationDomain] ADD CONSTRAINT [PK_JumpstationDomain] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationGroup]'
GO
CREATE TABLE [dbo].[JumpstationGroup]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TargetURL] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Order] [int] NULL,
[JumpstationGroupStatusId] [int] NOT NULL,
[JumpstationGroupTypeId] [int] NOT NULL,
[JumpstationApplicationId] [int] NOT NULL,
[OwnerId] [int] NOT NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_JumpstationGroup] on [dbo].[JumpstationGroup]'
GO
ALTER TABLE [dbo].[JumpstationGroup] ADD CONSTRAINT [PK_JumpstationGroup] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_CreatedBy] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_CreatedBy] ON [dbo].[JumpstationGroup] ([CreatedBy])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_CreatedOn] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_CreatedOn] ON [dbo].[JumpstationGroup] ([CreatedOn])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_ModifiedBy] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ModifiedBy] ON [dbo].[JumpstationGroup] ([ModifiedBy])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_ModifiedOn] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ModifiedOn] ON [dbo].[JumpstationGroup] ([ModifiedOn])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_Name] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_Name] ON [dbo].[JumpstationGroup] ([Name])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_JumpstationGroupTypeId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_JumpstationGroupTypeId] ON [dbo].[JumpstationGroup] ([JumpstationGroupTypeId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_JumpstationApplicationId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_JumpstationApplicationId] ON [dbo].[JumpstationGroup] ([JumpstationApplicationId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_OwnerId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_OwnerId] ON [dbo].[JumpstationGroup] ([OwnerId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_ProductionId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ProductionId] ON [dbo].[JumpstationGroup] ([ProductionId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_ValidationId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ValidationId] ON [dbo].[JumpstationGroup] ([ValidationId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

PRINT N'Creating [dbo].[JumpstationGroupPivot]'
GO
CREATE TABLE [dbo].[JumpstationGroupPivot](
	[JumpstationGroupId] [int] NOT NULL,
	[Brand] [nvarchar](256) NULL,
	[Cycle] [nvarchar](256) NULL,
	[Locale] [nvarchar](256) NULL,
	[Touchpoint] [nvarchar](256) NULL,
	[PartnerCategory] [nvarchar](256) NULL,
	[Platform] [nvarchar](256) NULL,
	CONSTRAINT [PK_JumpstationGroupPivot] PRIMARY KEY CLUSTERED 
	(
		[JumpstationGroupId] ASC
	)
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO


PRINT N'Creating [dbo].[JumpstationGroupSelector]'
GO
CREATE TABLE [dbo].[JumpstationGroupSelector]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[JumpstationGroupId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_JumpstationGroupSelector] on [dbo].[JumpstationGroupSelector]'
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] ADD CONSTRAINT [PK_JumpstationGroupSelector] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroupSelector_JumpstationGroupId] on [dbo].[JumpstationGroupSelector]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroupSelector_JumpstationGroupId] ON [dbo].[JumpstationGroupSelector] ([JumpstationGroupId])
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
PRINT N'Creating [dbo].[RowStatus]'
GO
CREATE TABLE [dbo].[RowStatus]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_RowStatus] on [dbo].[RowStatus]'
GO
ALTER TABLE [dbo].[RowStatus] ADD CONSTRAINT [PK_RowStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationGroupStatus]'
GO
CREATE TABLE [dbo].[JumpstationGroupStatus]
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
PRINT N'Creating primary key [PK_JumpstationGroupStatus] on [dbo].[JumpstationGroupStatus]'
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] ADD CONSTRAINT [PK_JumpstationGroupStatus] PRIMARY KEY NONCLUSTERED  ([Id])
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
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceApplicationId] [int] NOT NULL
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
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConfigurationServiceApplicationTypeId] [int] NOT NULL
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
PRINT N'Creating index [UK_ConfigurationServiceApplication_ElementsKey] on [dbo].[ConfigurationServiceApplication]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_ConfigurationServiceApplication_ElementsKey] ON [dbo].[ConfigurationServiceApplication] ([ElementsKey])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationGroupType]'
GO
CREATE TABLE [dbo].[JumpstationGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[JumpstationApplicationId] [int] NOT NULL DEFAULT ((1)),
[ValidationJumpstationDomainId] [int] NOT NULL DEFAULT ((1)),
[PublicationJumpstationDomainId] [int] NOT NULL DEFAULT ((2))
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_JumpstationGroupType] on [dbo].[JumpstationGroupType]'
GO
ALTER TABLE [dbo].[JumpstationGroupType] ADD CONSTRAINT [PK_JumpstationGroupType] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[JumpstationMacro]'
GO
CREATE TABLE [dbo].[JumpstationMacro]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[JumpstationMacroStatusId] [int] NOT NULL,
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OwnerId] [int] NOT NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL,
[DefaultResultValue] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT ('Invalid')
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_JumpstationMacro] on [dbo].[JumpstationMacro]'
GO
ALTER TABLE [dbo].[JumpstationMacro] ADD CONSTRAINT [PK_JumpstationMacro] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[QueryParameterValue]'
GO
CREATE TABLE [dbo].[QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[QueryParameterId] [int] NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_QueryParameterGroup] on [dbo].[QueryParameterValue]'
GO
ALTER TABLE [dbo].[QueryParameterValue] ADD CONSTRAINT [PK_QueryParameterGroup] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_QueryParameterId] on [dbo].[QueryParameterValue]'
GO
CREATE NONCLUSTERED INDEX [IX_QueryParameterId] ON [dbo].[QueryParameterValue] ([QueryParameterId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[QueryParameter]'
GO
CREATE TABLE [dbo].[QueryParameter]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_QueryParameter] on [dbo].[QueryParameter]'
GO
ALTER TABLE [dbo].[QueryParameter] ADD CONSTRAINT [PK_QueryParameter] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURL_QueryParameterValue]'
GO
CREATE TABLE [dbo].[ProxyURL_QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ProxyURLId] [int] NOT NULL,
[QueryParameterValueId] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ProxyURL_QueryParameterValue] on [dbo].[ProxyURL_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ProxyURL_QueryParameterValue] ADD CONSTRAINT [PK_ProxyURL_QueryParameterValue] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURLId] on [dbo].[ProxyURL_QueryParameterValue]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURLId] ON [dbo].[ProxyURL_QueryParameterValue] ([ProxyURLId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_QueryParameterValue] on [dbo].[ProxyURL_QueryParameterValue]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProxyURL_QueryParameterValue] ON [dbo].[ProxyURL_QueryParameterValue] ([ProxyURLId], [QueryParameterValueId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_QueryParameterValueId] on [dbo].[ProxyURL_QueryParameterValue]'
GO
CREATE NONCLUSTERED INDEX [IX_QueryParameterValueId] ON [dbo].[ProxyURL_QueryParameterValue] ([QueryParameterValueId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[QueryParameter_ProxyURLType]'
GO
CREATE TABLE [dbo].[QueryParameter_ProxyURLType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ProxyURLTypeId] [int] NOT NULL,
[QueryParameterId] [int] NOT NULL,
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_QueryParameter_ProxyURLTypeType] on [dbo].[QueryParameter_ProxyURLType]'
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] ADD CONSTRAINT [PK_QueryParameter_ProxyURLTypeType] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[ProxyURLType]'
GO
CREATE TABLE [dbo].[ProxyURLType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[ProxyURLDomainId] [int] NOT NULL,
[ProxyURLGroupTypeId] [int] NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ElementsKey] [int] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ProxyURLType] on [dbo].[ProxyURLType]'
GO
ALTER TABLE [dbo].[ProxyURLType] ADD CONSTRAINT [PK_ProxyURLType] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[ConfigurationServiceApplicationType]'
GO
CREATE TABLE [dbo].[ConfigurationServiceApplicationType]
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
PRINT N'Creating primary key [PK_ConfigurationServiceApplicationType] on [dbo].[ConfigurationServiceApplicationType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplicationType] ADD CONSTRAINT [PK_ConfigurationServiceApplicationType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationMacroStatus]'
GO
CREATE TABLE [dbo].[JumpstationMacroStatus]
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
PRINT N'Creating primary key [PK_JumpstationMacroStatus] on [dbo].[JumpstationMacroStatus]'
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] ADD CONSTRAINT [PK_JumpstationMacroStatus] PRIMARY KEY NONCLUSTERED  ([Id])
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
[Filename] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL,
[WorkflowModuleStatusId] [int] NOT NULL,
[OwnerId] [int] NOT NULL,
[WorkflowModuleCategoryId] [int] NOT NULL,
[WorkflowModuleSubCategoryId] [int] NOT NULL,
[Title] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
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
PRINT N'Creating [dbo].[Note]'
GO
CREATE TABLE [dbo].[Note]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EntityTypeId] [int] NOT NULL,
[EntityId] [int] NOT NULL,
[NoteTypeId] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Note] on [dbo].[Note]'
GO
ALTER TABLE [dbo].[Note] ADD CONSTRAINT [PK_Note] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_Name] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_Name] ON [dbo].[Note] ([Name]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_CreatedBy] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_CreatedBy] ON [dbo].[Note] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_CreatedOn] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_CreatedOn] ON [dbo].[Note] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_ModifiedBy] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_ModifiedBy] ON [dbo].[Note] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_ModifiedOn] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_ModifiedOn] ON [dbo].[Note] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_RowStatusId] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_RowStatusId] ON [dbo].[Note] ([RowStatusId]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_EntityTypeId] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_EntityTypeId] ON [dbo].[Note] ([EntityTypeId]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_EntityId] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_EntityId] ON [dbo].[Note] ([EntityId]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Note_NoteTypeId] on [dbo].[Note]'
GO
CREATE NONCLUSTERED INDEX [IX_Note_NoteTypeId] ON [dbo].[Note] ([NoteTypeId]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[NoteType]'
GO
CREATE TABLE [dbo].[NoteType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_NoteType] on [dbo].[NoteType]'
GO
ALTER TABLE [dbo].[NoteType] ADD CONSTRAINT [PK_NoteType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_NoteType_Name] on [dbo].[NoteType]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_NoteType_Name] ON [dbo].[NoteType] ([Name]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_NoteType_CreatedBy] on [dbo].[NoteType]'
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_CreatedBy] ON [dbo].[NoteType] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_NoteType_CreatedOn] on [dbo].[NoteType]'
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_CreatedOn] ON [dbo].[NoteType] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_NoteType_ModifiedBy] on [dbo].[NoteType]'
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_ModifiedBy] ON [dbo].[NoteType] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_NoteType_ModifiedOn] on [dbo].[NoteType]'
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_ModifiedOn] ON [dbo].[NoteType] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_NoteType_RowStatusId] on [dbo].[NoteType]'
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_RowStatusId] ON [dbo].[NoteType] ([RowStatusId]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapJumpstationDomain]'
GO
CREATE VIEW dbo.vwMapJumpstationDomain
AS
SELECT     dbo.JumpstationDomain.Id, dbo.JumpstationDomain.Name, dbo.JumpstationDomain.CreatedBy, dbo.JumpstationDomain.CreatedOn, 
                      dbo.JumpstationDomain.ModifiedBy, dbo.JumpstationDomain.ModifiedOn, dbo.JumpstationDomain.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationDomain.DomainURL
FROM         dbo.JumpstationDomain WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationDomain.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Person]'
GO
CREATE TABLE [dbo].[Person]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] AS (((((([LastName]+', ')+[FirstName])+isnull(' '+[MiddleName],''))+' (')+[WindowsId])+')'),
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[WindowsId] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MiddleName] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_user] on [dbo].[Person]'
GO
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [PK_user] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_Person_WindowsId] on [dbo].[Person]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Person_WindowsId] ON [dbo].[Person] ([WindowsId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[DDLChangeLog]'
GO
CREATE TABLE [dbo].[DDLChangeLog]
(
[ID] [int] NOT NULL IDENTITY(1, 1),
[Inserted] [datetime] NOT NULL DEFAULT (getdate()),
[CurrentUser] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT (CONVERT([nvarchar](50),user_name(),(0))),
[LoginName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT (CONVERT([nvarchar](50),suser_sname(),(0))),
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT (CONVERT([nvarchar](50),original_login(),(0))),
[EventType] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[objectName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[objectType] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[tsql] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_DDLChangeLog] on [dbo].[DDLChangeLog]'
GO
ALTER TABLE [dbo].[DDLChangeLog] ADD CONSTRAINT [PK_DDLChangeLog] PRIMARY KEY CLUSTERED  ([ID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[debug_DDLChangeLog_GetAsHtml]'
GO
--// =============================================
--// Author: Justin Webster
--// Create date: 2007/05/31
--// Description: Exports the contents of the DDLChangeLog table as a HTML-formatted string.
--// Modifications:
--// Exec Script:
--// Used in SP:
--// Used in Code:
--// Comments:
--// =============================================
CREATE PROCEDURE [dbo].[debug_DDLChangeLog_GetAsHtml]
AS 
BEGIN
    SET NOCOUNT ON ;
    DECLARE @HTMLCode VARCHAR(MAX) 
    SELECT
        @HTMLCode = COALESCE(@HTMLCode,
                             ' <style type="text/css"> 
		<!-- 
		#changes{ 
		 border: 1px solid silver; 
		 font-family: Arial, Helvetica, sans-serif; 
		 font-size: 11px; 
		 padding: 10px 10px 10px 10px; 
		} 
		#changes td.date{ font-style: italic; } 
		#changes td.tsql{ border-bottom: 1px solid silver; color: #00008B; } 
		--> 
		</style><table id="changes"> 
	') + '<tr class="recordtop"> 
	<td class="date">' + CONVERT(CHAR(18), [DDLChangeLog].[Inserted], 113)
        + '</td> 
	<td class="currentuser">' + [DDLChangeLog].[currentUser] + '</td> 
	<td class="loginname">' + [DDLChangeLog].[LoginName]
        + CASE WHEN [DDLChangeLog].[loginName] <> [DDLChangeLog].[UserName]
               THEN '(' + [DDLChangeLog].[UserName] + ')'
               ELSE ''
          END + '</td> 
	<td class="eventtype">' + [DDLChangeLog].[EventType] + '</td> 
	<td class="objectname">' + [DDLChangeLog].[ObjectName] + ' ('
        + [DDLChangeLog].[objectType] + ')' + '</td></tr> 
	<tr class="recordbase"><td colspan="6" class="tsql"><pre>'
        + [DDLChangeLog].[tsql] + '</pre></td></tr> 
	'
    FROM
        [dbo].[DDLChangeLog]
    ORDER BY
        [DDLChangeLog].[Inserted] ; 
    SELECT
        @HTMLCode + ' 
	</table>' 
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURL]'
GO
CREATE TABLE [dbo].[ProxyURL]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProxyURLStatusId] [int] NOT NULL DEFAULT ((0)),
[ProxyURLTypeId] [int] NOT NULL,
[OwnerId] [int] NOT NULL,
[URL] [varchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ProxyURL] on [dbo].[ProxyURL]'
GO
ALTER TABLE [dbo].[ProxyURL] ADD CONSTRAINT [PK_ProxyURL] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_CreatedBy] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_CreatedBy] ON [dbo].[ProxyURL] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_CreatedOn] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_CreatedOn] ON [dbo].[ProxyURL] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_ModifiedBy] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ModifiedBy] ON [dbo].[ProxyURL] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_ModifiedOn] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ModifiedOn] ON [dbo].[ProxyURL] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_ProxyURLStatusId] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ProxyURLStatusId] ON [dbo].[ProxyURL] ([ProxyURLStatusId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_ProxyURLTypeId] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ProxyURLTypeId] ON [dbo].[ProxyURL] ([ProxyURLTypeId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_OwnerId] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_OwnerId] ON [dbo].[ProxyURL] ([OwnerId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_URL] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_URL] ON [dbo].[ProxyURL] ([URL])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_ProductionId] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ProductionId] ON [dbo].[ProxyURL] ([ProductionId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURL_ValidationId] on [dbo].[ProxyURL]'
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ValidationId] ON [dbo].[ProxyURL] ([ValidationId])
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
PRINT N'Creating [dbo].[ProxyURLDomain]'
GO
CREATE TABLE [dbo].[ProxyURLDomain]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ValidationDomain] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProductionDomain] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ProxyURLDomain] on [dbo].[ProxyURLDomain]'
GO
ALTER TABLE [dbo].[ProxyURLDomain] ADD CONSTRAINT [PK_ProxyURLDomain] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURLGroupType]'
GO
CREATE TABLE [dbo].[ProxyURLGroupType]
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
PRINT N'Creating primary key [PK_ProxyURLGroupType] on [dbo].[ProxyURLGroupType]'
GO
ALTER TABLE [dbo].[ProxyURLGroupType] ADD CONSTRAINT [PK_ProxyURLGroupType] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[ProxyURLStatus]'
GO
CREATE TABLE [dbo].[ProxyURLStatus]
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
PRINT N'Creating primary key [PK_ProxyURLStatus] on [dbo].[ProxyURLStatus]'
GO
ALTER TABLE [dbo].[ProxyURLStatus] ADD CONSTRAINT [PK_ProxyURLStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_ProxyURLStatus_Name] on [dbo].[ProxyURLStatus]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProxyURLStatus_Name] ON [dbo].[ProxyURLStatus] ([Name])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapProxyURLDomain]'
GO
CREATE VIEW dbo.vwMapProxyURLDomain
AS
SELECT     dbo.ProxyURLDomain.Id, dbo.ProxyURLDomain.Name, dbo.ProxyURLDomain.CreatedBy, dbo.ProxyURLDomain.CreatedOn, dbo.ProxyURLDomain.ModifiedBy, dbo.ProxyURLDomain.ModifiedOn, 
                      dbo.ProxyURLDomain.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ProxyURLDomain.ValidationDomain, dbo.ProxyURLDomain.ProductionDomain
FROM         dbo.ProxyURLDomain WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ProxyURLDomain.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURL_HasDuplicateQueryParameterValues]'
GO
-- User Defined Function
-- =============================================
-- Author:		Robert Mukai
-- Create date: 6/24/09
-- Description:	Returns > 0 if ProxyURLId has same 
-- QueryParameterValues as a another published ProxyURL.
-- Returns 0 if not
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_HasDuplicateQueryParameterValues]
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
PRINT N'Creating [dbo].[vwMapProxyURLType]'
GO
CREATE VIEW dbo.vwMapProxyURLType
AS
SELECT     dbo.ProxyURLType.Id, dbo.ProxyURLType.ModifiedBy, dbo.ProxyURLType.CreatedOn, dbo.ProxyURLType.CreatedBy, dbo.ProxyURLType.Name, dbo.ProxyURLType.ElementsKey, 
                      dbo.ProxyURLType.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ProxyURLType.ModifiedOn
FROM         dbo.ProxyURLType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ProxyURLType.RowStatusId = dbo.RowStatus.Id
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
PRINT N'Creating [dbo].[vwMapQueryParameter]'
GO
CREATE VIEW dbo.vwMapQueryParameter
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
PRINT N'Creating [dbo].[QueryParameter_ConfigurationServiceGroupType]'
GO
CREATE TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ConfigurationServiceGroupTypeId] [int] NOT NULL,
[QueryParameterId] [int] NOT NULL,
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MaximumSelection] [int] NOT NULL DEFAULT ((0)),
[Wildcard] [bit] NOT NULL DEFAULT ((0))
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_QueryParameter_ConfigurationServiceGroupTypeType] on [dbo].[QueryParameter_ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] ADD CONSTRAINT [PK_QueryParameter_ConfigurationServiceGroupTypeType] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[Tag]'
GO
CREATE TABLE [dbo].[Tag]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Notes] [nvarchar] (2048) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Tag] on [dbo].[Tag]'
GO
ALTER TABLE [dbo].[Tag] ADD CONSTRAINT [PK_Tag] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[vwMapPerson]'
GO
CREATE VIEW dbo.vwMapPerson
AS
SELECT     dbo.Person.Id, dbo.Person.WindowsId, dbo.Person.Name, dbo.Person.FirstName, dbo.Person.MiddleName, dbo.Person.LastName, dbo.Person.Email, 
                      dbo.Person.CreatedOn, dbo.Person.CreatedBy, dbo.Person.ModifiedOn, dbo.Person.ModifiedBy, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.Person.RowStatusId
FROM         dbo.Person WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.Person.RowStatusId = dbo.RowStatus.Id
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
PRINT N'Creating [dbo].[EntityType]'
GO
CREATE TABLE [dbo].[EntityType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_EntityType] on [dbo].[EntityType]'
GO
ALTER TABLE [dbo].[EntityType] ADD CONSTRAINT [PK_EntityType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_EntityType_Name] on [dbo].[EntityType]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EntityType_Name] ON [dbo].[EntityType] ([Name]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_EntityType_CreatedBy] on [dbo].[EntityType]'
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_CreatedBy] ON [dbo].[EntityType] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_EntityType_CreatedOn] on [dbo].[EntityType]'
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_CreatedOn] ON [dbo].[EntityType] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_EntityType_ModifiedBy] on [dbo].[EntityType]'
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_ModifiedBy] ON [dbo].[EntityType] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_EntityType_ModifiedOn] on [dbo].[EntityType]'
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_ModifiedOn] ON [dbo].[EntityType] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_EntityType_RowStatusId] on [dbo].[EntityType]'
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_RowStatusId] ON [dbo].[EntityType] ([RowStatusId]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapNote]'
GO
CREATE VIEW dbo.vwMapNote
AS
SELECT     dbo.Note.Id
          ,dbo.Note.Name
          ,dbo.Note.CreatedBy
          ,dbo.Note.CreatedOn
          ,dbo.Note.ModifiedBy
          ,dbo.Note.ModifiedOn
          ,dbo.Note.RowStatusId
          ,dbo.RowStatus.Name AS RowStatusName
          ,dbo.Note.Comment
          ,dbo.Note.EntityTypeId
          ,dbo.EntityType.Name As EntityTypeName
          ,dbo.Note.EntityId
          ,dbo.Note.NoteTypeId
          ,dbo.NoteType.Name As NoteTypeName
      ,(SELECT   COUNT(Id) AS Expr1
        FROM     dbo.Note WITH (NOLOCK)
        WHERE    (dbo.Note.EntityTypeId = 27) AND (dbo.Note.EntityId = dbo.Note.Id)) AS NoteCount
FROM         dbo.Note WITH (NOLOCK) INNER JOIN
             dbo.RowStatus WITH (NOLOCK) ON dbo.Note.RowStatusId = dbo.RowStatus.Id INNER JOIN
             dbo.EntityType WITH (NOLOCK) ON dbo.Note.EntityTypeId = dbo.EntityType.Id LEFT OUTER JOIN
             dbo.NoteType WITH (NOLOCK) ON dbo.Note.NoteTypeId = dbo.NoteType.Id 
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceGroupSelector_QueryParameterValue]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroupSelector_QueryParameterValue
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
PRINT N'Creating [dbo].[JumpstationApplication]'
GO
CREATE TABLE [dbo].[JumpstationApplication]
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
PRINT N'Creating primary key [PK_JumpstationApplication] on [dbo].[JumpstationApplication]'
GO
ALTER TABLE [dbo].[JumpstationApplication] ADD CONSTRAINT [PK_JumpstationApplication] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[vwMapJumpstationGroupSelector]'
GO
CREATE VIEW dbo.vwMapJumpstationGroupSelector
AS
SELECT     dbo.JumpstationGroupSelector.Id, dbo.JumpstationGroupSelector.Name, dbo.JumpstationGroupSelector.CreatedBy, 
                      dbo.JumpstationGroupSelector.CreatedOn, dbo.JumpstationGroupSelector.ModifiedBy, 
                      dbo.JumpstationGroupSelector.ModifiedOn, dbo.JumpstationGroupSelector.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationGroupSelector.JumpstationGroupId
FROM         dbo.JumpstationGroupSelector WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationGroupSelector.RowStatusId = dbo.RowStatus.Id
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
PRINT N'Creating [dbo].[ProxyURL_GenerateQueryString]'
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
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapQueryParameterValue]'
GO
CREATE VIEW dbo.vwMapQueryParameterValue
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
PRINT N'Creating [dbo].[JumpstationGroupSelector_QueryParameterValue]'
GO
CREATE TABLE [dbo].[JumpstationGroupSelector_QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[JumpstationGroupSelectorId] [int] NOT NULL,
[QueryParameterValueId] [int] NOT NULL,
[Negation] [bit] NOT NULL DEFAULT ((0))
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_JumpstationGroupSelector_QueryParameterValue] on [dbo].[JumpstationGroupSelector_QueryParameterValue]'
GO
ALTER TABLE [dbo].[JumpstationGroupSelector_QueryParameterValue] ADD CONSTRAINT [PK_JumpstationGroupSelector_QueryParameterValue] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroupSelector_QueryParameterValue_JumpstationGroupSelectorId] on [dbo].[JumpstationGroupSelector_QueryParameterValue]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroupSelector_QueryParameterValue_JumpstationGroupSelectorId] ON [dbo].[JumpstationGroupSelector_QueryParameterValue] ([JumpstationGroupSelectorId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[QueryParameter_JumpstationGroupType]'
GO
CREATE TABLE [dbo].[QueryParameter_JumpstationGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[JumpstationGroupTypeId] [int] NOT NULL,
[QueryParameterId] [int] NOT NULL,
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MaximumSelection] [int] NOT NULL DEFAULT ((0)),
[Wildcard] [bit] NOT NULL DEFAULT ((0)),
[SortOrder] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_QueryParameter_JumpstationGroupTypeType] on [dbo].[QueryParameter_JumpstationGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] ADD CONSTRAINT [PK_QueryParameter_JumpstationGroupTypeType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationGroup_Tag]'
GO
CREATE TABLE [dbo].[JumpstationGroup_Tag]
(
[JumpstationGroupId] [int] NOT NULL,
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
PRINT N'Creating primary key [PK_JumpstationGroup_Tag] on [dbo].[JumpstationGroup_Tag]'
GO
ALTER TABLE [dbo].[JumpstationGroup_Tag] ADD CONSTRAINT [PK_JumpstationGroup_Tag] PRIMARY KEY NONCLUSTERED  ([JumpstationGroupId], [TagId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Role]'
GO
CREATE TABLE [dbo].[Role]
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
PRINT N'Creating primary key [PK_Role] on [dbo].[Role]'
GO
ALTER TABLE [dbo].[Role] ADD CONSTRAINT [PK_Role] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Role_Name] on [dbo].[Role]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Role_Name] ON [dbo].[Role] ([Name])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationGroup_GetQueryParameterValueList]'
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
PRINT N'Creating [dbo].[vwMapJumpstationApplication]'
GO
CREATE VIEW dbo.vwMapJumpstationApplication
AS
SELECT     dbo.JumpstationApplication.Id, dbo.JumpstationApplication.Name, dbo.JumpstationApplication.CreatedBy, 
                      dbo.JumpstationApplication.CreatedOn, dbo.JumpstationApplication.ModifiedBy, dbo.JumpstationApplication.ModifiedOn, 
                      dbo.JumpstationApplication.Version, dbo.JumpstationApplication.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationApplication.ElementsKey
FROM         dbo.JumpstationApplication WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationApplication.RowStatusId = dbo.RowStatus.Id
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
PRINT N'Creating [dbo].[Person_Role]'
GO
CREATE TABLE [dbo].[Person_Role]
(
[PersonId] [int] NOT NULL,
[RoleId] [int] NOT NULL,
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
PRINT N'Creating primary key [PK_Person_Role_1] on [dbo].[Person_Role]'
GO
ALTER TABLE [dbo].[Person_Role] ADD CONSTRAINT [PK_Person_Role_1] PRIMARY KEY NONCLUSTERED  ([PersonId], [RoleId])
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
PRINT N'Creating [dbo].[ConfigurationServiceGroup_GetQueryParameterValueList]'
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
PRINT N'Creating index [IX_ProductionId] on [dbo].[ConfigurationServiceGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_ProductionId] ON [dbo].[ConfigurationServiceGroup] ([ProductionId])
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
PRINT N'Creating [dbo].[Aardvark]'
GO
CREATE TABLE [dbo].[Aardvark]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Aardvark] on [dbo].[Aardvark]'
GO
ALTER TABLE [dbo].[Aardvark] ADD CONSTRAINT [PK_Aardvark] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Aardvark_Name] on [dbo].[Aardvark]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Aardvark_Name] ON [dbo].[Aardvark] ([Name]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Aardvark_CreatedBy] on [dbo].[Aardvark]'
GO
CREATE NONCLUSTERED INDEX [IX_Aardvark_CreatedBy] ON [dbo].[Aardvark] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Aardvark_CreatedOn] on [dbo].[Aardvark]'
GO
CREATE NONCLUSTERED INDEX [IX_Aardvark_CreatedOn] ON [dbo].[Aardvark] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Aardvark_ModifiedBy] on [dbo].[Aardvark]'
GO
CREATE NONCLUSTERED INDEX [IX_Aardvark_ModifiedBy] ON [dbo].[Aardvark] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_Aardvark_ModifiedOn] on [dbo].[Aardvark]'
GO
CREATE NONCLUSTERED INDEX [IX_Aardvark_ModifiedOn] ON [dbo].[Aardvark] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapJumpstationMacro]'
GO
CREATE VIEW dbo.vwMapJumpstationMacro
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceLabel]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceLabel
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
PRINT N'Creating [dbo].[uspGetJumpstationByQueryParameterValue]'
GO
-- =============================================
-- Author:		Robert Mukai
-- Create date: 7/30/12
-- Description:	Returns a list of Jumpstation Groups by Query Parameter Value
-- =============================================
CREATE PROCEDURE [dbo].[uspGetJumpstationByQueryParameterValue]
	@isCountQuery INT,
	@rowCount INT OUTPUT,
	@startRow INT,
	@rowsPerPage INT,
	@statusId INT,
	@jumpstationTypeId INT,
	@queryParameterValueIdDelimitedList VARCHAR(512)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @jumpstationGroupIdTable TABLE (Id INT)
	DECLARE @jumpstationGroupIdIntersectTable TABLE (Id INT)
	
	DECLARE @queryParameterValueIdTable TABLE (Id INT)
	DECLARE @queryParameterValueId INT
	DECLARE @pos INT

	SET @queryParameterValueIdDelimitedList = LTRIM(RTRIM(@queryParameterValueIdDelimitedList))+ ','
	SET @pos = CHARINDEX(',', @queryParameterValueIdDelimitedList, 1)

	-- delimited list of query parameter value ids
	IF REPLACE(@queryParameterValueIdDelimitedList, ',', '') <> ''
	BEGIN
		WHILE @pos > 0
		BEGIN
			SET @queryParameterValueId = LTRIM(RTRIM(LEFT(@queryParameterValueIdDelimitedList, @Pos - 1)))
			IF @queryParameterValueId <> ''
			BEGIN
				INSERT INTO @queryParameterValueIdTable (Id) VALUES (CAST(@queryParameterValueId AS INT)) --Use Appropriate conversion
			END
			SET @queryParameterValueIdDelimitedList = RIGHT(@queryParameterValueIdDelimitedList, LEN(@queryParameterValueIdDelimitedList) - @pos)
			SET @pos = CHARINDEX(',', @queryParameterValueIdDelimitedList, 1)
		END
	END	

	-- check if any query parameters selected
	IF (SELECT COUNT(*) FROM @queryParameterValueIdTable) = 0 
	BEGIN
		-- if No parameter then just load all ids
		INSERT INTO @jumpstationGroupIdIntersectTable
			SELECT DISTINCT dbo.JumpstationGroup.Id
			FROM dbo.JumpstationGroup WITH (NOLOCK)
	END
	ELSE
	BEGIN
		-- find unique jumpstation group ids that have query parameter value ids to filter
		DECLARE @firstPass INT
		SET @firstPass = -1
		WHILE (SELECT COUNT(*) FROM @queryParameterValueIdTable) > 0 
		BEGIN 
			SELECT TOP 1 @queryParameterValueId = Id FROM @queryParameterValueIdTable
	 
			IF @firstPass = -1
			BEGIN
				SET @firstPass = 0	
				INSERT INTO @jumpstationGroupIdIntersectTable
					SELECT DISTINCT dbo.JumpstationGroupSelector.JumpstationGroupId
					FROM dbo.JumpstationGroupSelector WITH (NOLOCK) INNER JOIN
						 dbo.JumpstationGroupSelector_QueryParameterValue WITH (NOLOCK) ON 
				         dbo.JumpstationGroupSelector.Id = dbo.JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId
					WHERE dbo.JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = @queryParameterValueId
			END
			ELSE
			BEGIN
				INSERT INTO @jumpstationGroupIdTable
					SELECT DISTINCT Id FROM @jumpstationGroupIdIntersectTable
					INTERSECT
					SELECT DISTINCT dbo.JumpstationGroupSelector.JumpstationGroupId
					FROM dbo.JumpstationGroupSelector WITH (NOLOCK) INNER JOIN
						 dbo.JumpstationGroupSelector_QueryParameterValue WITH (NOLOCK) ON 
				         dbo.JumpstationGroupSelector.Id = dbo.JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId
					WHERE dbo.JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = @queryParameterValueId
					
				DELETE @jumpstationGroupIdIntersectTable
				INSERT INTO @jumpstationGroupIdIntersectTable
					SELECT Id FROM @jumpstationGroupidTable				
				DELETE @jumpstationGroupIdTable
			END
 
			DELETE @queryParameterValueIdTable WHERE Id = @queryParameterValueId  
		END
	END 

	IF @isCountQuery = -1
	BEGIN
		SELECT @rowCount = COUNT(dbo.JumpstationGroup.Id)
			FROM dbo.JumpstationGroup WITH (NOLOCK) INNER JOIN
			dbo.JumpstationGroupStatus WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupStatusId = dbo.JumpstationGroupStatus.Id INNER JOIN
			dbo.JumpstationGroupType WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id INNER JOIN
			dbo.JumpstationApplication WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationApplicationId = dbo.JumpstationApplication.Id INNER JOIN
			dbo.Person WITH (NOLOCK) ON dbo.JumpstationGroup.OwnerId = dbo.Person.Id INNER JOIN
			@jumpstationGroupIdIntersectTable AS aa ON dbo.JumpstationGroup.Id = aa.Id AND 
			(@statusId = 0 OR dbo.JumpstationGroup.JumpstationGroupStatusId = @statusId) AND
			(@jumpstationTypeId = 0 OR dbo.JumpstationGroup.JumpstationGroupTypeId = @jumpstationTypeId)
		SELECT @rowCount
	END
	ELSE
	BEGIN
		SET @rowCount = 0
		SELECT * FROM 
			(SELECT ROW_NUMBER() OVER ( ORDER BY [dbo].[JumpstationGroup].[Id]) AS Row, 
			dbo.JumpstationGroupStatus.Name AS JumpstationGroupStatusName, dbo.JumpstationGroupType.Name AS JumpstationGroupTypeName, dbo.JumpstationGroup.Id, dbo.JumpstationGroup.CreatedBy, 
			dbo.JumpstationGroup.CreatedOn, dbo.JumpstationGroup.ModifiedBy, dbo.JumpstationGroup.ModifiedOn, dbo.JumpstationGroup.Name AS Name, 
			dbo.JumpstationGroup.Description, dbo.JumpstationGroup.TargetURL, dbo.JumpstationGroup.[Order], 
			dbo.JumpstationApplication.Name AS JumpstationApplicationName, dbo.Person.Name AS PersonName, dbo.JumpstationGroup.ProductionId, 
			dbo.JumpstationGroup.ValidationId
			FROM dbo.JumpstationGroup WITH (NOLOCK) INNER JOIN
			dbo.JumpstationGroupStatus WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupStatusId = dbo.JumpstationGroupStatus.Id INNER JOIN
			dbo.JumpstationGroupType WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id INNER JOIN
			dbo.JumpstationApplication WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationApplicationId = dbo.JumpstationApplication.Id INNER JOIN
			dbo.Person WITH (NOLOCK) ON dbo.JumpstationGroup.OwnerId = dbo.Person.Id INNER JOIN
			@jumpstationGroupIdIntersectTable AS aa ON dbo.JumpstationGroup.Id = aa.Id
			WHERE (@statusId = 0 OR dbo.JumpstationGroup.JumpstationGroupStatusId = @statusId) AND
				(@jumpstationTypeId = 0 OR dbo.JumpstationGroup.JumpstationGroupTypeId = @jumpstationTypeId)
			) AS PagedResults
		WHERE  Row >= @startRow AND Row <= @startRow + @rowsPerPage 
	END
END
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
PRINT N'Creating [dbo].[ProxyURL_Tag]'
GO
CREATE TABLE [dbo].[ProxyURL_Tag]
(
[ProxyURLId] [int] NOT NULL,
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
PRINT N'Creating primary key [PK_ProxyURL_Tag_1] on [dbo].[ProxyURL_Tag]'
GO
ALTER TABLE [dbo].[ProxyURL_Tag] ADD CONSTRAINT [PK_ProxyURL_Tag_1] PRIMARY KEY NONCLUSTERED  ([ProxyURLId], [TagId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURL_HasTag_ByTagId]'
GO
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/31/08
-- Description:	Returns a bit that indicates whether a specified ProxyURL is associated with a specified Tag.
-- Usage Example:
--		SELECT
--			[proxyURL].[Id] AS ProxyURLId,
--			[proxyURL].[Name] AS ProxyURLName,
--			[tag].[Id] AS TagId,
--			[tag].[Name] AS TagName,
--			(SELECT [TMOMSDB].[dbo].[ProxyURL_HasTag_ByTagId] ([proxyURL].[Id], [tag].[Id])) AS HasTag,
--			[proxyURL].[Tags] AS ProxyURLTags
--		FROM
--			[TMOMSDB].[dbo].[vwMapProxyURL] AS proxyURL,
--			[dbo].[Tag] AS tag
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_HasTag_ByTagId]
    (
     @ProxyURLId INT,
     @TagId INT
    )
RETURNS BIT
AS BEGIN
    DECLARE @IntFoundTag INT
    SET @IntFoundTag = (
                        SELECT TOP 1
                            [ProxyURL_Tag].[ProxyURLID]
                        FROM
                            [dbo].[ProxyURL_Tag] WITH (NOLOCK)
                        WHERE
                            ([ProxyURL_Tag].[ProxyURLID] = @ProxyURLId)
                            AND ([ProxyURL_Tag].[TagId] = @TagId)
                       ) ;
    DECLARE @BitFoundTag BIT
    SET @BitFoundTag = CAST(ISNULL(@IntFoundTag, 0) AS BIT) ;
    RETURN @BitFoundTag
   END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURL_HasDuplicateQueryParameterValuesDelimitedList]'
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
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapNoteType]'
GO
CREATE VIEW dbo.vwMapNoteType
AS
SELECT     dbo.NoteType.Id
          ,dbo.NoteType.Name
          ,dbo.NoteType.CreatedBy
          ,dbo.NoteType.CreatedOn
          ,dbo.NoteType.ModifiedBy
          ,dbo.NoteType.ModifiedOn
          ,dbo.NoteType.RowStatusId
      ,dbo.RowStatus.Name AS RowStatusName
           ,dbo.NoteType.Comment
FROM         dbo.NoteType WITH (NOLOCK) INNER JOIN
             dbo.RowStatus WITH (NOLOCK) ON dbo.NoteType.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapProxyURLGroupType]'
GO
CREATE VIEW [dbo].[vwMapProxyURLGroupType]
AS
SELECT     dbo.ProxyURLGroupType.Id, dbo.ProxyURLGroupType.Name, dbo.ProxyURLGroupType.CreatedBy, dbo.ProxyURLGroupType.CreatedOn, dbo.ProxyURLGroupType.ModifiedBy, dbo.ProxyURLGroupType.ModifiedOn, 
                      dbo.ProxyURLGroupType.RowStatusId, dbo.RowStatus.Name AS RowStatusName
FROM         dbo.ProxyURLGroupType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ProxyURLGroupType.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapJumpstationGroupSelector_QueryParameterValue]'
GO
CREATE VIEW dbo.vwMapJumpstationGroupSelector_QueryParameterValue
AS
SELECT     dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameterValue.Name AS QueryParameterValueName, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.Negation, dbo.QueryParameter.Id AS QueryParameterId, 
                      dbo.QueryParameterValue.Id AS QueryParameterValueId, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.Id AS JumpstationGroupSelectorQueryParameterValueId, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.ModifiedOn, dbo.JumpstationGroupSelector_QueryParameterValue.ModifiedBy, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.CreatedOn, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.CreatedBy
FROM         dbo.JumpstationGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameterValue ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId ON 
                      dbo.JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = dbo.QueryParameterValue.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapWorkflow]'
GO
CREATE VIEW dbo.vwMapWorkflow
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
                      dbo.Workflow.VersionMinor, dbo.Workflow.Filename
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
PRINT N'Creating [dbo].[PublishTemp]'
GO
CREATE TABLE [dbo].[PublishTemp]
(
[Id] [int] NOT NULL IDENTITY(1000, 1),
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
PRINT N'Creating primary key [PK_PublishTemp] on [dbo].[PublishTemp]'
GO
ALTER TABLE [dbo].[PublishTemp] ADD CONSTRAINT [PK_PublishTemp] PRIMARY KEY NONCLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceGroupType]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroupType
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
PRINT N'Creating [dbo].[vwMapProxyURL_QueryParameterValue]'
GO
CREATE VIEW dbo.vwMapProxyURL_QueryParameterValue
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
PRINT N'Creating [dbo].[vwMapQueryParameter_ProxyURLType]'
GO
CREATE VIEW dbo.vwMapQueryParameter_ProxyURLType
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceApplication]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceApplication
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceItem]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceItem
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceLabelValue]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceLabelValue
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceLabel_ConfigurationServiceGroupType]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceLabel_ConfigurationServiceGroupType
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
PRINT N'Creating [dbo].[vwMapQueryParameter_ConfigurationServiceGroupType]'
GO
CREATE VIEW dbo.vwMapQueryParameter_ConfigurationServiceGroupType
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
PRINT N'Creating [dbo].[vwMapEntityType]'
GO
CREATE VIEW dbo.vwMapEntityType
AS
SELECT     dbo.EntityType.Id
          ,dbo.EntityType.Name
          ,dbo.EntityType.CreatedBy
          ,dbo.EntityType.CreatedOn
          ,dbo.EntityType.ModifiedBy
          ,dbo.EntityType.ModifiedOn
          ,dbo.EntityType.RowStatusId
      ,dbo.RowStatus.Name AS RowStatusName
      ,(SELECT   COUNT(Id) AS Expr1
        FROM     dbo.Note WITH (NOLOCK)
        WHERE    (dbo.Note.EntityTypeId = 14) AND (dbo.Note.EntityId = dbo.EntityType.Id)) AS NoteCount
           ,dbo.EntityType.Comment
FROM         dbo.EntityType WITH (NOLOCK) INNER JOIN
             dbo.RowStatus WITH (NOLOCK) ON dbo.EntityType.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapConfigurationServiceGroupSelector]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroupSelector
AS
SELECT     dbo.ConfigurationServiceGroupSelector.Id, dbo.ConfigurationServiceGroupSelector.Name, dbo.ConfigurationServiceGroupSelector.CreatedBy, 
                      dbo.ConfigurationServiceGroupSelector.CreatedOn, dbo.ConfigurationServiceGroupSelector.ModifiedBy, 
                      dbo.ConfigurationServiceGroupSelector.ModifiedOn, dbo.ConfigurationServiceGroupSelector.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceGroupSelector.ConfigurationServiceGroupId
FROM         dbo.ConfigurationServiceGroupSelector WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroupSelector.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapJumpstationGroupType]'
GO
CREATE VIEW dbo.vwMapJumpstationGroupType
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
PRINT N'Creating [dbo].[uspDeleteJumpstationGroupSelectorQueryParameterValueByJumpstationGroupSelectorIdQueryParameterId]'
GO
-- =============================================
-- Author:		Robert Mukai
-- Create date: 8/04/10
-- Description:	Delete JumpstationGroupSelector_QueryParameterValue by JumpstationGroupSelectorId and QueryParameterId
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteJumpstationGroupSelectorQueryParameterValueByJumpstationGroupSelectorIdQueryParameterId]
	@JumpstationGroupSelectorId INT,
	@queryParameterId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE
		dbo.JumpstationGroupSelector_QueryParameterValue
	WHERE 
		JumpstationGroupSelectorId = @JumpstationGroupSelectorId AND
		QueryParameterValueId IN (SELECT Id FROM QueryParameterValue WHERE QueryParameterId = @queryParameterId)
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapQueryParameter_JumpstationGroupType]'
GO
CREATE VIEW dbo.vwMapQueryParameter_JumpstationGroupType
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
PRINT N'Creating [dbo].[vwMapJumpstationGroupTag_Tag]'
GO
CREATE VIEW [dbo].[vwMapJumpstationGroupTag_Tag]
AS
SELECT     dbo.JumpstationGroup_Tag.JumpstationGroupId, dbo.JumpstationGroup_Tag.TagId, dbo.JumpstationGroup_Tag.CreatedBy, dbo.JumpstationGroup_Tag.CreatedOn, 
                      dbo.JumpstationGroup_Tag.ModifiedBy, dbo.JumpstationGroup_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.JumpstationGroup_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.JumpstationGroup_Tag.TagId = dbo.Tag.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[JumpstationGroup_GetTagList]'
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [JumpstationGroup].[Id], [dbo].[JumpstationGroup_GetTagList]([JumpstationGroup].[Id], ', ', 1, 1) FROM [dbo].[JumpstationGroup]
	SELECT dbo.JumpstationGroup_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [JumpstationGroup].[Id], dbo.JumpstationGroup_GetTagList ([JumpstationGroup].[Id], '/', 1, 1) FROM [dbo].[JumpstationGroup] ORDER BY [JumpstationGroup].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified JumpstationGroup.
-- =============================================
CREATE FUNCTION [dbo].[JumpstationGroup_GetTagList]
    (
     @JumpstationGroupId INT,
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
        @TagList = COALESCE(@TagList + @Delimiter, '') + [JumpstationGrouptag].[TagName]
    FROM
        [dbo].[vwMapJumpstationGroupTag_Tag] JumpstationGrouptag
    WHERE
        ([JumpstationGrouptag].[JumpstationGroupId] = @JumpstationGroupId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([JumpstationGrouptag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [JumpstationGrouptag].[TagName]
    RETURN @TagList
   END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapJumpstationGroup]'
GO
CREATE VIEW dbo.vwMapJumpstationGroup
AS
SELECT     dbo.JumpstationGroup.Id, dbo.RowStatus.Name AS RowStatusName, dbo.JumpstationGroupStatus.Name AS JumpstationGroupStatusName, 
                      dbo.Person.Name AS PersonName, dbo.JumpstationGroup.CreatedBy, dbo.JumpstationGroup.CreatedOn, dbo.JumpstationGroup.ModifiedBy, 
                      dbo.JumpstationGroup.ModifiedOn, dbo.JumpstationGroup.RowStatusId, dbo.JumpstationGroup.Description, dbo.JumpstationGroup.JumpstationGroupStatusId, 
                      dbo.JumpstationGroup.JumpstationGroupTypeId, dbo.JumpstationGroup.OwnerId, dbo.JumpstationGroupType.Name AS JumpstationGroupTypeName, 
                      dbo.JumpstationGroupPivot.Brand AS BrandQueryParameterValue,
                      dbo.JumpstationGroupPivot.Cycle AS CycleQueryParameterValue,
                      dbo.JumpstationGroupPivot.Locale AS LocaleQueryParameterValue,
                      dbo.JumpstationGroupPivot.Touchpoint AS TouchpointQueryParameterValue,
                      dbo.JumpstationGroupPivot.PartnerCategory AS PartnerCategoryQueryParameterValue,
                      dbo.JumpstationGroupPivot.Platform AS PlatformQueryParameterValue,
                      dbo.JumpstationGroup_GetTagList(dbo.JumpstationGroup.Id, ', ', 1) AS Tags, dbo.JumpstationGroup_GenerateQueryString(dbo.JumpstationGroup.Id) AS QueryString,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.JumpstationGroup_Tag WITH (NOLOCK)
                            WHERE      (dbo.JumpstationGroup.Id = JumpstationGroupId)) AS TagCount, dbo.JumpstationGroup.ProductionId, dbo.JumpstationGroup.ValidationId, 
                      dbo.JumpstationGroup.Name, dbo.JumpstationGroup.JumpstationApplicationId, dbo.JumpstationApplication.Name AS JumpstationApplicationName, 
                      dbo.JumpstationGroup.TargetURL, dbo.JumpstationGroup.[Order], dbo.JumpstationDomain.DomainURL AS PublicationDomain
FROM         dbo.JumpstationGroup WITH (NOLOCK) INNER JOIN
					  dbo.JumpstationGroupPivot WITH (NOLOCK) ON dbo.JumpstationGroup.Id = dbo.JumpstationGroupPivot.JumpstationGroupId INNER JOIN
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

PRINT N'Creating [dbo].[vwMapJumpstationGroupCalcOnFly]'
GO
CREATE VIEW [dbo].[vwMapJumpstationGroupCalcOnFly]
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
PRINT N'Creating [dbo].[vwMapPerson_Role]'
GO
CREATE VIEW dbo.vwMapPerson_Role
AS
SELECT     dbo.Person.Id, dbo.Person.WindowsId, dbo.Person.Name, dbo.Person.FirstName, dbo.Person.MiddleName, dbo.Person.LastName, dbo.Person.Email, 
                      dbo.Person.CreatedOn, dbo.Person.CreatedBy, dbo.Person.ModifiedOn, dbo.Person.ModifiedBy, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.Person.RowStatusId, dbo.Role.Name AS RoleName, dbo.Role.Id AS RoleId
FROM         dbo.Person WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.Person.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person_Role WITH (NOLOCK) ON dbo.Person.Id = dbo.Person_Role.PersonId INNER JOIN
                      dbo.Role WITH (NOLOCK) ON dbo.Person_Role.RoleId = dbo.Role.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapConfigurationServiceGroupTag_Tag]'
GO
CREATE VIEW [dbo].[vwMapConfigurationServiceGroupTag_Tag]
AS
SELECT     dbo.ConfigurationServiceGroup_Tag.ConfigurationServiceGroupId, dbo.ConfigurationServiceGroup_Tag.TagId, dbo.ConfigurationServiceGroup_Tag.CreatedBy, dbo.ConfigurationServiceGroup_Tag.CreatedOn, 
                      dbo.ConfigurationServiceGroup_Tag.ModifiedBy, dbo.ConfigurationServiceGroup_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.ConfigurationServiceGroup_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.ConfigurationServiceGroup_Tag.TagId = dbo.Tag.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ConfigurationServiceGroup_GetTagList]'
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [ConfigurationServiceGroup].[Id], [dbo].[ConfigurationServiceGroup_GetTagList]([ConfigurationServiceGroup].[Id], ', ', 1, 1) FROM [dbo].[ConfigurationServiceGroup]
	SELECT dbo.ConfigurationServiceGroup_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [ConfigurationServiceGroup].[Id], dbo.ConfigurationServiceGroup_GetTagList ([ConfigurationServiceGroup].[Id], '/', 1, 1) FROM [dbo].[ConfigurationServiceGroup] ORDER BY [ConfigurationServiceGroup].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified ConfigurationServiceGroup.
-- =============================================
CREATE FUNCTION [dbo].[ConfigurationServiceGroup_GetTagList]
    (
     @ConfigurationServiceGroupId INT,
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
        @TagList = COALESCE(@TagList + @Delimiter, '') + [ConfigurationServiceGrouptag].[TagName]
    FROM
        [dbo].[vwMapConfigurationServiceGroupTag_Tag] ConfigurationServiceGrouptag
    WHERE
        ([ConfigurationServiceGrouptag].[ConfigurationServiceGroupId] = @ConfigurationServiceGroupId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([ConfigurationServiceGrouptag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [ConfigurationServiceGrouptag].[TagName]
    RETURN @TagList
   END
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceGroup]'
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroup
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
                      PublisherLabelValue.Value AS PublisherLabelValue, InstallerTypeLabelValue.Value AS InstallerTypeLabelValue, 
                      dbo.ConfigurationServiceGroup_GetTagList(dbo.ConfigurationServiceGroup.Id, ', ', 1) AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.ConfigurationServiceGroup_Tag WITH (NOLOCK)
                            WHERE      (dbo.ConfigurationServiceGroup.Id = ConfigurationServiceGroupId)) AS TagCount, dbo.ConfigurationServiceGroup.ProductionId, 
                      dbo.ConfigurationServiceGroup.ValidationId, dbo.ConfigurationServiceGroup.Name, dbo.ConfigurationServiceGroup.ConfigurationServiceApplicationId, 
                      dbo.ConfigurationServiceApplication.Name AS ConfigurationServiceApplicationName
FROM         dbo.ConfigurationServiceGroup WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroup.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.ConfigurationServiceGroup.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.ConfigurationServiceGroupStatus WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupStatusId = dbo.ConfigurationServiceGroupStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupTypeId = dbo.ConfigurationServiceGroupType.Id INNER JOIN
                      dbo.ConfigurationServiceApplication WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceApplicationId = dbo.ConfigurationServiceApplication.Id LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupId, Value
                            FROM          dbo.ConfigurationServiceLabelValue WITH (NOLOCK)
                            WHERE      (ConfigurationServiceLabelId = 7)) AS PublisherLabelValue ON 
                      dbo.ConfigurationServiceGroup.Id = PublisherLabelValue.ConfigurationServiceGroupId LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupId, Value
                            FROM          dbo.ConfigurationServiceLabelValue AS ConfigurationServiceLabelValue_1 WITH (NOLOCK)
                            WHERE      (ConfigurationServiceLabelId = 8)) AS InstallerTypeLabelValue ON 
                      dbo.ConfigurationServiceGroup.Id = InstallerTypeLabelValue.ConfigurationServiceGroupId
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
PRINT N'Creating [dbo].[vwMapProxyURLTag_Tag]'
GO
CREATE VIEW dbo.vwMapProxyURLTag_Tag
AS
SELECT     dbo.ProxyURL_Tag.ProxyURLId, dbo.ProxyURL_Tag.TagId, dbo.ProxyURL_Tag.CreatedBy, dbo.ProxyURL_Tag.CreatedOn, 
                      dbo.ProxyURL_Tag.ModifiedBy, dbo.ProxyURL_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.ProxyURL_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.ProxyURL_Tag.TagId = dbo.Tag.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURL_GetTagList]'
GO
-- User Defined Function
/*
Usage Examples:
	SELECT [ProxyURL].[Id], [dbo].[ProxyURL_GetTagList]([ProxyURL].[Id], ', ', 1, 1) FROM [dbo].[ProxyURL]
	SELECT dbo.ProxyURL_GetTagList (1 ', ', 1, 1)
	SELECT TOP 10 [ProxyURL].[Id], dbo.ProxyURL_GetTagList ([ProxyURL].[Id], '/', 1, 1) FROM [dbo].[ProxyURL] ORDER BY [ProxyURL].[Id] DESC
*/
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/27/08
-- Description:	Returns a delimited list of Tag Names for a specified ProxyURL.
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_GetTagList]
    (
     @ProxyURLId INT,
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
        @TagList = COALESCE(@TagList + @Delimiter, '') + [proxyURLtag].[TagName]
    FROM
        [dbo].[vwMapProxyURLTag_Tag] proxyURLtag
    WHERE
        ([proxyURLtag].[ProxyURLId] = @ProxyURLId)
        AND (
             (@TagRowStatusId IS NULL)
             OR ([proxyURLtag].[TagRowStatusId] = @TagRowStatusId)
            )
    ORDER BY
        [proxyURLtag].[TagName]
    RETURN @TagList
   END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURL_HasTag_ByTagName]'
GO
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/31/08
-- Description:	Returns an int that indicates whether a specified ProxyURL is associated with a specified Tag. (Returns either the ProxyURL's ID or 0.)
-- Usage Example:
--		SELECT
--			[proxyURL].[Id] AS ProxyURLId,
--			[proxyURL].[Name] AS ProxyURLName,
--			[tag].[Id] AS TagId,
--			[tag].[Name] AS TagName,
--			(SELECT [TMOMSDB].[dbo].[ProxyURL_HasTag_ByTagName] ([proxyURL].[Id], [tag].[Name])) AS HasTag,
--			[proxyURL].[Tags] AS ProxyURLTags
--		FROM
--			[TMOMSDB].[dbo].[vwMapProxyURL] AS proxyURL,
--			[dbo].[Tag] AS tag
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_HasTag_ByTagName]
    (
     @ProxyURLId INT,
     @TagName NVARCHAR(256)
    )
RETURNS INT
AS BEGIN
    DECLARE @IntFoundTag INT
    SET @IntFoundTag = (
                        SELECT TOP 1
                            [vwMapProxyURLTag_Tag].[ProxyURLID]
                        FROM
                            [dbo].[vwMapProxyURLTag_Tag] WITH (NOLOCK)
                        WHERE
                            ([vwMapProxyURLTag_Tag].[ProxyURLID] = @ProxyURLId)
                            AND ([vwMapProxyURLTag_Tag].[TagName] = @TagName)
                       ) ;
    SET @IntFoundTag = ISNULL(@IntFoundTag, 0) ;
    RETURN @IntFoundTag
   END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProxyURL_HasTag_ByTagName2]'
GO
-- =============================================
-- Author:		Justin Webster
-- Create date: 8/31/08
-- Description:	Returns a bit that indicates whether a specified ProxyURL is associated with a specified Tag.
-- Usage Example:
--		SELECT
--			[proxyURL].[Id] AS ProxyURLId,
--			[proxyURL].[Name] AS ProxyURLName,
--			[tag].[Id] AS TagId,
--			[tag].[Name] AS TagName,
--			(SELECT [TMOMSDB].[dbo].[ProxyURL_HasTag_ByTagName2] ([proxyURL].[Id], [tag].[Name])) AS HasTag,
--			[proxyURL].[Tags] AS ProxyURLTags
--		FROM
--			[TMOMSDB].[dbo].[vwMapProxyURL] AS proxyURL,
--			[dbo].[Tag] AS tag
-- =============================================
CREATE FUNCTION [dbo].[ProxyURL_HasTag_ByTagName2]
    (
     @ProxyURLId INT,
     @TagName NVARCHAR(256)
    )
RETURNS BIT
AS BEGIN
    DECLARE @IntFoundTag INT
    SET @IntFoundTag = (
                        SELECT TOP 1
                            [vwMapProxyURLTag_Tag].[ProxyURLID]
                        FROM
                            [dbo].[vwMapProxyURLTag_Tag] WITH (NOLOCK)
                        WHERE
                            ([vwMapProxyURLTag_Tag].[ProxyURLID] = @ProxyURLId)
                            AND ([vwMapProxyURLTag_Tag].[TagName] = @TagName)
                       ) ;
    DECLARE @BitFoundTag BIT
    SET @BitFoundTag = CAST(ISNULL(@IntFoundTag, 0) AS BIT) ;
    RETURN @BitFoundTag
   END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[vwMapProxyURL]'
GO
CREATE VIEW dbo.vwMapProxyURL
AS
SELECT     dbo.ProxyURL.Id, dbo.RowStatus.Name AS RowStatusName, dbo.ProxyURLStatus.Name AS ProxyURLStatusName, dbo.Person.Name AS PersonName,
                       dbo.ProxyURL.CreatedBy, dbo.ProxyURL.CreatedOn, dbo.ProxyURL.ModifiedBy, dbo.ProxyURL.ModifiedOn, dbo.ProxyURL.RowStatusId, 
                      dbo.ProxyURL.Description, dbo.ProxyURL.ProxyURLStatusId, dbo.ProxyURL.ProxyURLTypeId, dbo.ProxyURL.OwnerId, dbo.ProxyURL.URL, 
                      dbo.ProxyURLType.Name AS ProxyURLTypeName, dbo.ProxyURLType.ElementsKey AS ProxyURLTypeElementsKey, 
                      TouchpointParameterValue.QueryParameterValueId AS TouchpointParameterValueQueryParameterValueId, 
                      TouchpointParameterValue.QueryParameterId AS TouchpointParameterValueQueryParameterId, 
                      TouchpointParameterValue.Name AS TouchpointParameterValueName, 
                      BrandParameterValue.QueryParameterValueId AS BrandParameterValueQueryParameterValueId, 
                      BrandParameterValue.QueryParameterId AS BrandParameterValueQueryParameterId, BrandParameterValue.Name AS BrandParameterValueName, 
                      LocaleParameterValue.QueryParameterValueId AS LocaleParameterValueQueryParameterValueId, 
                      LocaleParameterValue.QueryParameterId AS LocaleParameterValueQueryParameterId, LocaleParameterValue.Name AS LocaleParameterValueName, 
                      CycleParameterValue.QueryParameterValueId AS CycleParameterValueQueryParameterValueId, 
                      CycleParameterValue.QueryParameterId AS CycleParameterValueQueryParameterId, CycleParameterValue.Name AS CycleParameterValueName, 
                      PlatformParameterValue.QueryParameterValueId AS PlatformParameterValueQueryParameterValueId, 
                      PlatformParameterValue.QueryParameterId AS PlatformParameterValueQueryParameterId, 
                      PlatformParameterValue.Name AS PlatformParameterValueName, 
                      PartnerCategoryParameterValue.QueryParameterValueId AS PartnerCategoryParameterValueQueryParameterValueId, 
                      PartnerCategoryParameterValue.QueryParameterId AS PartnerCategoryParameterValueQueryParameterId, 
                      PartnerCategoryParameterValue.Name AS PartnerCategoryParameterValueName, dbo.ProxyURL_GetTagList(dbo.ProxyURL.Id, ', ', 1) AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.ProxyURL_Tag WITH (NOLOCK)
                            WHERE      (dbo.ProxyURL.Id = ProxyURLId)) AS TagCount, dbo.ProxyURL.ProductionId, dbo.ProxyURL.ValidationId, 
                      dbo.ProxyURL_GenerateQueryString(dbo.ProxyURL.Id) AS QueryString, dbo.ProxyURLDomain.ProductionDomain, dbo.ProxyURLDomain.ValidationDomain
FROM         dbo.ProxyURL WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ProxyURL.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.ProxyURL.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.ProxyURLStatus WITH (NOLOCK) ON dbo.ProxyURL.ProxyURLStatusId = dbo.ProxyURLStatus.Id INNER JOIN
                      dbo.ProxyURLType WITH (NOLOCK) ON dbo.ProxyURL.ProxyURLTypeId = dbo.ProxyURLType.Id INNER JOIN
                      dbo.ProxyURLDomain WITH (NOLOCK) ON dbo.ProxyURLType.ProxyURLDomainId = dbo.ProxyURLDomain.Id LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 13)) AS TouchpointParameterValue ON 
                      dbo.ProxyURL.Id = TouchpointParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 15)) AS BrandParameterValue ON 
                      dbo.ProxyURL.Id = BrandParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 3)) AS LocaleParameterValue ON 
                      dbo.ProxyURL.Id = LocaleParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 7)) AS CycleParameterValue ON 
                      dbo.ProxyURL.Id = CycleParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 14)) AS PlatformParameterValue ON 
                      dbo.ProxyURL.Id = PlatformParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 5)) AS PartnerCategoryParameterValue ON 
                      dbo.ProxyURL.Id = PartnerCategoryParameterValue.ProxyURLId
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Aardvark]'
GO
ALTER TABLE [dbo].[Aardvark] WITH NOCHECK ADD CONSTRAINT [CK_Aardvark_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Aardvark] WITH NOCHECK ADD CONSTRAINT [CK_Aardvark_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Aardvark] WITH NOCHECK ADD CONSTRAINT [CK_Aardvark_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Aardvark] WITH NOCHECK ADD CONSTRAINT [CK_Aardvark_Comment_IsTrimmed] CHECK ((len(ltrim(rtrim([Comment])))=len([Comment])))
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
PRINT N'Adding constraints to [dbo].[ConfigurationServiceApplicationType]'
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
ALTER TABLE [dbo].[ConfigurationServiceApplicationType] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplicationType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
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
PRINT N'Adding constraints to [dbo].[EntityType]'
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [CK_EntityType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [CK_EntityType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [CK_EntityType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [CK_EntityType_Comment_IsTrimmed] CHECK ((len(ltrim(rtrim([Comment])))=len([Comment])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationApplication]'
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Version_IsTrimmed] CHECK ((len(ltrim(rtrim([Version])))=len([Version])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationDomain]'
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationDomain_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationDomain_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationDomain_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationDomain_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationGroup]'
GO
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroup_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroup_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroup_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroup_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationGroupSelector]'
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupSelector_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupSelector_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupSelector_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupSelector_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationGroupStatus]'
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationGroupType]'
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationMacro]'
GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacro_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacro_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacro_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacro_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationMacroStatus]'
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacroStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacroStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacroStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacroStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Note]'
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [CK_Note_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [CK_Note_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [CK_Note_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [CK_Note_Comment_IsTrimmed] CHECK ((len(ltrim(rtrim([Comment])))=len([Comment])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[NoteType]'
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [CK_NoteType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [CK_NoteType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [CK_NoteType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [CK_NoteType_Comment_IsTrimmed] CHECK ((len(ltrim(rtrim([Comment])))=len([Comment])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Person]'
GO
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD CONSTRAINT [CK_Person_WindowsId] CHECK ((len(rtrim([WindowsId]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD CONSTRAINT [CK_Person_FirstName] CHECK ((len(rtrim([FirstName]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD CONSTRAINT [CK_Person_FirstName_IsTrimmed] CHECK ((len(ltrim(rtrim([FirstName])))=len([FirstName])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD CONSTRAINT [CK_Person_MiddleName_IsTrimmed] CHECK ((len(ltrim(rtrim([MiddleName])))=len([MiddleName])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD CONSTRAINT [CK_Person_LastName] CHECK ((len(rtrim([LastName]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD CONSTRAINT [CK_Person_LastName_IsTrimmed] CHECK ((len(ltrim(rtrim([LastName])))=len([LastName])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD CONSTRAINT [CK_Person_Email_IsTrimmed] CHECK ((len(ltrim(rtrim([Email])))=len([Email])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ProxyURL]'
GO
ALTER TABLE [dbo].[ProxyURL] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURL_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ProxyURLDomain]'
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLDomain_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLDomain_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLDomain_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLDomain_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ProxyURLGroupType]'
GO
ALTER TABLE [dbo].[ProxyURLGroupType] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLGroupType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLGroupType] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLGroupType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLGroupType] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLGroupType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLGroupType] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLGroupType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ProxyURLStatus]'
GO
ALTER TABLE [dbo].[ProxyURLStatus] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLStatus] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLStatus] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLStatus] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ProxyURLType]'
GO
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[QueryParameter]'
GO
ALTER TABLE [dbo].[QueryParameter] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_Name_MinLen] CHECK ((len(rtrim([Name]))>=(5)))
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
PRINT N'Adding constraints to [dbo].[QueryParameterValue]'
GO
ALTER TABLE [dbo].[QueryParameterValue] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameterValue_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameterValue] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameterValue_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Role]'
GO
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD CONSTRAINT [CK_Role_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD CONSTRAINT [CK_Role_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD CONSTRAINT [CK_Role_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD CONSTRAINT [CK_Role_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[RowStatus]'
GO
ALTER TABLE [dbo].[RowStatus] WITH NOCHECK ADD CONSTRAINT [CK_RowStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[RowStatus] WITH NOCHECK ADD CONSTRAINT [CK_RowStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[RowStatus] WITH NOCHECK ADD CONSTRAINT [CK_RowStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[RowStatus] WITH NOCHECK ADD CONSTRAINT [CK_RowStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Tag]'
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [CK_Tag_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [CK_Tag_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [CK_Tag_Name_MinLen] CHECK ((len(rtrim([Name]))>=(5)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [CK_Tag_Notes_IsTrimmed] CHECK ((len(ltrim(rtrim([Notes])))=len([Notes])))
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
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupType_ConfigurationServiceApplication] FOREIGN KEY ([ConfigurationServiceApplicationId]) REFERENCES [dbo].[ConfigurationServiceApplication] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroupType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceApplication]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceApplication_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
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
CONSTRAINT [FK_QueryParameter_ConfigurationServiceGroupType_ConfigurationServicedGroupType] FOREIGN KEY ([ConfigurationServiceGroupTypeId]) REFERENCES [dbo].[ConfigurationServiceGroupType] ([Id]),
CONSTRAINT [FK_QueryParameter_QueryParameter_ConfigurationServiceGroupType] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
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
PRINT N'Adding foreign keys to [dbo].[Note]'
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD
CONSTRAINT [FK_Note_EntityType] FOREIGN KEY ([EntityTypeId]) REFERENCES [dbo].[EntityType] ([Id]),
CONSTRAINT [FK_Note_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_Note_NoteType] FOREIGN KEY ([NoteTypeId]) REFERENCES [dbo].[NoteType] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[EntityType]'
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD
CONSTRAINT [FK_EntityType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationGroup]'
GO
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroup_JumpstationApplication] FOREIGN KEY ([JumpstationApplicationId]) REFERENCES [dbo].[JumpstationApplication] ([Id]),
CONSTRAINT [FK_JumpstationGroup_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_JumpstationGroup_JumpstationGroupStatus] FOREIGN KEY ([JumpstationGroupStatusId]) REFERENCES [dbo].[JumpstationGroupStatus] ([Id]),
CONSTRAINT [FK_JumpstationGroup_JumpstationGroupType] FOREIGN KEY ([JumpstationGroupTypeId]) REFERENCES [dbo].[JumpstationGroupType] ([Id]),
CONSTRAINT [FK_JumpstationGroup_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationGroupType]'
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupType_JumpstationApplication] FOREIGN KEY ([JumpstationApplicationId]) REFERENCES [dbo].[JumpstationApplication] ([Id]),
CONSTRAINT [FK_JumpstationGroupType_PublicationJumpstationDomain] FOREIGN KEY ([PublicationJumpstationDomainId]) REFERENCES [dbo].[JumpstationDomain] ([Id]),
CONSTRAINT [FK_JumpstationGroupType_ValidationJumpstationDomain] FOREIGN KEY ([ValidationJumpstationDomainId]) REFERENCES [dbo].[JumpstationDomain] ([Id]),
CONSTRAINT [FK_JumpstationGroupType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationApplication]'
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationApplication_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationDomain]'
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationDomain_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationGroup_Tag]'
GO
ALTER TABLE [dbo].[JumpstationGroup_Tag] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroup_Tag_JumpstationGroup] FOREIGN KEY ([JumpstationGroupId]) REFERENCES [dbo].[JumpstationGroup] ([Id]),
CONSTRAINT [FK_JumpstationGroup_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationGroupSelector]'
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupSelector_JumpstationGroup] FOREIGN KEY ([JumpstationGroupId]) REFERENCES [dbo].[JumpstationGroup] ([Id]),
CONSTRAINT [FK_JumpstationGroupSelector_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationGroupSelector_QueryParameterValue]'
GO
ALTER TABLE [dbo].[JumpstationGroupSelector_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupSelector_QueryParameterValue_JumpstationGroupSelector] FOREIGN KEY ([JumpstationGroupSelectorId]) REFERENCES [dbo].[JumpstationGroupSelector] ([Id]),
CONSTRAINT [FK_JumpstationGroupSelector_QueryParameterValue_QueryParameterValue] FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationGroupStatus]'
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupStatus_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[QueryParameter_JumpstationGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameter_JumpstationGroupType_JumpstationdGroupType] FOREIGN KEY ([JumpstationGroupTypeId]) REFERENCES [dbo].[JumpstationGroupType] ([Id]),
CONSTRAINT [FK_QueryParameter_QueryParameter_JumpstationGroupType] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationMacroValue]'
GO
ALTER TABLE [dbo].[JumpstationMacroValue] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacroValue_JumpstationMacro] FOREIGN KEY ([JumpstationMacroId]) REFERENCES [dbo].[JumpstationMacro] ([Id]),
CONSTRAINT [FK_JumpstationMacroValue_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationMacro]'
GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacro_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_JumpstationMacro_JumpstationMacroStatus] FOREIGN KEY ([JumpstationMacroStatusId]) REFERENCES [dbo].[JumpstationMacroStatus] ([Id]),
CONSTRAINT [FK_JumpstationMacro_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationMacroStatus]'
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacroStatus_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[NoteType]'
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD
CONSTRAINT [FK_NoteType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Person_Role]'
GO
ALTER TABLE [dbo].[Person_Role] WITH NOCHECK ADD
CONSTRAINT [FK_Person_Role_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE,
CONSTRAINT [FK_Person_Role_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProxyURL]'
GO
ALTER TABLE [dbo].[ProxyURL] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id]),
CONSTRAINT [FK_ProxyURL_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_ProxyURL_ProxyURLStatus] FOREIGN KEY ([ProxyURLStatusId]) REFERENCES [dbo].[ProxyURLStatus] ([Id]),
CONSTRAINT [FK_ProxyURL_ProxyURLType] FOREIGN KEY ([ProxyURLTypeId]) REFERENCES [dbo].[ProxyURLType] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Workflow]'
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD
CONSTRAINT [FK_Workflow_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id]),
CONSTRAINT [FK_Workflow_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_Workflow_WorkflowStatus] FOREIGN KEY ([WorkflowStatusId]) REFERENCES [dbo].[WorkflowStatus] ([Id]),
CONSTRAINT [FK_Workflow_WorkflowType] FOREIGN KEY ([WorkflowTypeId]) REFERENCES [dbo].[WorkflowType] ([Id]),
CONSTRAINT [FK_Workflow_WorkflowApplication] FOREIGN KEY ([WorkflowApplicationId]) REFERENCES [dbo].[WorkflowApplication] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModule]'
GO
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id]),
CONSTRAINT [FK_WorkflowModule_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_WorkflowModule_WorkflowStatus] FOREIGN KEY ([WorkflowModuleStatusId]) REFERENCES [dbo].[WorkflowStatus] ([Id]),
CONSTRAINT [FK_WorkflowModule_WorkflowModuleCategory] FOREIGN KEY ([WorkflowModuleCategoryId]) REFERENCES [dbo].[WorkflowModuleCategory] ([Id]),
CONSTRAINT [FK_WorkflowModule_WorkflowModuleSubCategory] FOREIGN KEY ([WorkflowModuleSubCategoryId]) REFERENCES [dbo].[WorkflowModuleSubCategory] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Person]'
GO
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [FK_Person_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProxyURL_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ProxyURL_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_QueryParameterValue_ProxyURL] FOREIGN KEY ([ProxyURLId]) REFERENCES [dbo].[ProxyURL] ([Id]),
CONSTRAINT [FK_ProxyURL_QueryParameterValue_QueryParameterValue] FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProxyURL_Tag]'
GO
ALTER TABLE [dbo].[ProxyURL_Tag] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_Tag_ProxyURL] FOREIGN KEY ([ProxyURLId]) REFERENCES [dbo].[ProxyURL] ([Id]),
CONSTRAINT [FK_ProxyURL_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProxyURLType]'
GO
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURLType_ProxyURLDomain] FOREIGN KEY ([ProxyURLDomainId]) REFERENCES [dbo].[ProxyURLDomain] ([Id]),
CONSTRAINT [FK_ProxyURLType_ProxyURLGroupType] FOREIGN KEY ([ProxyURLGroupTypeId]) REFERENCES [dbo].[ProxyURLGroupType] ([Id]),
CONSTRAINT [FK_ProxyURLType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProxyURLDomain]'
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURLDomain_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProxyURLGroupType]'
GO
ALTER TABLE [dbo].[ProxyURLGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURLGroupType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[QueryParameter_ProxyURLType]'
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameter_ProxyURLType_ProxyURLType] FOREIGN KEY ([ProxyURLTypeId]) REFERENCES [dbo].[ProxyURLType] ([Id]),
CONSTRAINT [FK_QueryParameter_ProxyURLType_QueryParameter] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[QueryParameter_WorkflowType]'
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameter_QueryParameter_WorkflowType] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id]),
CONSTRAINT [FK_QueryParameter_WorkflowType_ConfigurationServicedGroupType] FOREIGN KEY ([WorkflowTypeId]) REFERENCES [dbo].[WorkflowType] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[QueryParameterValue]'
GO
ALTER TABLE [dbo].[QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameterValue_QueryParameter] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id]),
CONSTRAINT [FK_QueryParameterValue_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[QueryParameter]'
GO
ALTER TABLE [dbo].[QueryParameter] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameter_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowSelector_QueryParameterValue]'
GO
ALTER TABLE [dbo].[WorkflowSelector_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowSelector_QueryParameterValue_QueryParameterValue] FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id]),
CONSTRAINT [FK_WorkflowSelector_QueryParameterValue_WorkflowSelector] FOREIGN KEY ([WorkflowSelectorId]) REFERENCES [dbo].[WorkflowSelector] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Role]'
GO
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD
CONSTRAINT [FK_Role_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Tag]'
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD
CONSTRAINT [FK_Tag_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
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
PRINT N'Adding foreign keys to [dbo].[WorkflowCondition]'
GO
ALTER TABLE [dbo].[WorkflowCondition] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowCondition_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
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
CONSTRAINT [FK_WorkflowModuleVersion_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_WorkflowModuleVersion_WorkflowModuleCategory] FOREIGN KEY ([WorkflowModuleCategoryId]) REFERENCES [dbo].[WorkflowModuleCategory] ([Id]),
CONSTRAINT [FK_WorkflowModuleVersion_WorkflowModuleSubCategory] FOREIGN KEY ([WorkflowModuleSubCategoryId]) REFERENCES [dbo].[WorkflowModuleSubCategory] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowSelector]'
GO
ALTER TABLE [dbo].[WorkflowSelector] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowSelector_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_WorkflowSelector_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflow] ([Id])
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
PRINT N'Adding foreign keys to [dbo].[Workflow_Tag]'
GO
ALTER TABLE [dbo].[Workflow_Tag] WITH NOCHECK ADD
CONSTRAINT [FK_Workflow_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id]),
CONSTRAINT [FK_Workflow_Tag_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflow] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModule_Tag]'
GO
ALTER TABLE [dbo].[WorkflowModule_Tag] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id]),
CONSTRAINT [FK_WorkflowModule_Tag_WorkflowModule] FOREIGN KEY ([WorkflowModuleId]) REFERENCES [dbo].[WorkflowModule] ([Id])
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
PRINT N'Creating DDL triggers'
GO
/* now we'll create the trigger that fires whenever any database level 
DDL events occur. We won't bother to record CREATE STATISTIC events*/ 
CREATE TRIGGER trgLogDDLEvent ON DATABASE 
    FOR DDL_DATABASE_LEVEL_EVENTS 
AS 
    DECLARE @data XML 
    SET @data = EVENTDATA() 
    IF @data.value('(/EVENT_INSTANCE/EventType)[1]', 'nvarchar(100)') 
        <> 'CREATE_STATISTICS'  
        INSERT  INTO DDLChangeLog 
                ( 
                  EventType, 
                  ObjectName, 
                  ObjectType, 
                  tsql 
                ) 
        VALUES  ( 
                   @data.value('(/EVENT_INSTANCE/EventType)[1]', 
                              'nvarchar(100)'), 
                  @data.value('(/EVENT_INSTANCE/ObjectName)[1]', 
                              'nvarchar(100)'), 
                  @data.value('(/EVENT_INSTANCE/ObjectType)[1]', 
                              'nvarchar(100)'), 
                  @data.value('(/EVENT_INSTANCE/TSQLCommand)[1]', 
                              'nvarchar(max)') 
                ) ; 
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
