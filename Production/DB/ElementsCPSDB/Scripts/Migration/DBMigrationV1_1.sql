/*
Run this script on:

        gvs00351\i01,2048.ElementsCPSDB    -  This database will be modified

to synchronize it with:

        (local).ElementsCPSDB

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.0.0 from Red Gate Software Ltd at 6/21/2010 3:40:47 PM

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
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceGroup_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_QueryParameterValue] DROP
CONSTRAINT [FK_ConfigurationServiceGroup_QueryParameterValue_ConfigurationServiceGroup],
CONSTRAINT [FK_ConfigurationServiceGroup_QueryParameterValue_QueryParameterValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[ConfigurationServiceGroup_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_QueryParameterValue] DROP CONSTRAINT [PK_ConfigurationServiceGroup_QueryParameterValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[ConfigurationServiceGroup_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_QueryParameterValue] DROP CONSTRAINT [DF_ConfigurationServiceGroup_QueryParameterValue_CreatedOn]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[ConfigurationServiceGroup_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_QueryParameterValue] DROP CONSTRAINT [DF_ConfigurationServiceGroup_QueryParameterValue_ModifiedOn]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[vwMapConfigurationServiceGroup_QueryParameterValue]'
GO
DROP VIEW [dbo].[vwMapConfigurationServiceGroup_QueryParameterValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceGroup_QueryParameterValue]'
GO
DROP TABLE [dbo].[ConfigurationServiceGroup_QueryParameterValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[SplitStringByDelimiter]'
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
ALTER FUNCTION [dbo].[SplitStringByDelimiter]
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
PRINT N'Creating [dbo].[EntityType]'
GO
SET ANSI_NULLS ON
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
[Negation] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceGroupSelector_QueryParameterValueAll]'
GO
CREATE VIEW [dbo].[vwMapConfigurationServiceGroupSelector_QueryParameterValueAll]
AS
SELECT     dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameterValue.Name AS QueryParameterValueName, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.Negation, dbo.QueryParameter.Id AS QueryParameterId, 
                      dbo.QueryParameterValue.Id AS QueryParameterValueId, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.Id AS ConfigurationServiceGroupSelectorQueryParameterValueId
FROM         dbo.QueryParameter INNER JOIN
                      dbo.QueryParameterValue ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId LEFT OUTER JOIN
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue ON 
                      dbo.QueryParameterValue.Id = dbo.ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId
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
PRINT N'Creating [dbo].[uspDeletePersonRoleByPersonId]'
GO
-- =============================================
-- Author:		Robert Mukai
-- Create date: 6/30/08
-- Description:	Delete PersonRole by PersonId
-- =============================================
CREATE PROCEDURE [dbo].[uspDeletePersonRoleByPersonId]
	@personId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE
		dbo.Person_Role
	WHERE 
		PersonId = @personId
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[usp_ElementsCPS_DataMigration]'
GO
-- =============================================
-- Author:		Van Tran
-- Create date: 06/28/2009
-- Description:	Migrating data from staging tables to ElementCPS database.
-- Based on the URLProp.xml mapped between Site_ID, Group_Type_ID and to get the ProxyURLTypeId. 
-- =============================================
ALTER PROCEDURE [usp_ElementsCPS_DataMigration] 
AS
BEGIN
	DECLARE @dtDefaultValue_CreatedOn DATETIME;
	DECLARE @dtDefaultValue_ModifiedOn DATETIME;
	DECLARE @strDefaultValue_CreatedBy NVARCHAR(50);
	DECLARE @strDefaultValue_ModifiedBy NVARCHAR(50);
	DECLARE @intRowStatus_Active int;
	DECLARE @intRowStatus_Inactive int;
	DECLARE @intRowStatus_Deleted int;
	DECLARE @intProxyStatus_Published int;
	DECLARE @intPlatform_Production int;
	DECLARE @intSimpleRedirect int;
	DECLARE @intAllOtherRedirect int;
	DECLARE @intDefaultRedirect int;
	DECLARE @intPresarioKeyboard int;
	DECLARE @intPavilionKeyboard int;
	DECLARE @intOwner_System int;
	SET @dtDefaultValue_CreatedOn = GETUTCDATE();
	SET @dtDefaultValue_ModifiedOn = @dtDefaultValue_CreatedOn;
	SET @strDefaultValue_CreatedBy = N'system\system';
	SET @strDefaultValue_ModifiedBy = @strDefaultValue_CreatedBy;
	SET @intRowStatus_Active = 1;
	SET @intRowStatus_Inactive = 2;
	SET @intRowStatus_Deleted = 3;
	SET @intProxyStatus_Published = 4;
	SET @intPlatform_Production = 2;
	SET @intSimpleRedirect = 1;
	SET @intAllOtherRedirect = 2;
	SET @intDefaultRedirect = 3;
	SET @intPresarioKeyboard = 4;
	SET @intPavilionKeyboard = 5;
	SET @intOwner_System = 1;
	-- Migrating Attribute to Element CPS
	SET IDENTITY_INSERT [QueryParameter] ON
	INSERT INTO [QueryParameter]
           ([Id]
           ,[Name]
           ,[ElementsKey]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[RowStatusId]
           ,[Description])           
    SELECT  ATT_ID,
			NAME = CASE ATT_KEY
			WHEN 'dll' THEN 'Dynamic Link Library (DLL)'
			WHEN 's' THEN 'Partner Category'
			WHEN 'c' THEN 'Cycle'
			WHEN 'tp' THEN 'Touchpoint'
			WHEN 'pf' THEN 'Platform'
			WHEN 'bd' THEN 'Brand'
			WHEN 'param' THEN 'Simple Parameter'
			WHEN 'locale' THEN 'Locale'
			ELSE Description + ' (need to be changed) ' END,
			ATT_KEY, 
			@strDefaultValue_CreatedBy, 
			@dtDefaultValue_CreatedOn, 
			@strDefaultValue_ModifiedBy, 
			@dtDefaultValue_ModifiedOn,
			@intRowStatus_Active,
			'data migrated from the old system, ATTRIBUTE table'
    FROM dbo.STAGE_ATTRIBUTE
    SET IDENTITY_INSERT [QueryParameter] OFF
    -- Migrating selector items to Element CPS
    SET IDENTITY_INSERT [QueryParameterValue] ON
    INSERT INTO [QueryParameterValue]
			([Id]
			,[Name]
			,[CreatedBy]
			,[CreatedOn]
			,[ModifiedBy]
			,[ModifiedOn]
			,[RowStatusId]
			,[QueryParameterId]
			,[Description])
	SELECT  SELECTOR_ITEM_ID, 
		    ATT_VALUE, 
		    @strDefaultValue_CreatedBy, 
		    @dtDefaultValue_CreatedOn,  
		    @strDefaultValue_ModifiedBy,
		    @dtDefaultValue_ModifiedOn,
		    @intRowStatus_Active,
		    ATT_ID,
		    'data migrated from the old system, SELECTOR_ITEM table'
	FROM dbo.STAGE_SELECTOR_ITEM
	SET IDENTITY_INSERT [QueryParameterValue] OFF
	BEGIN
	SET IDENTITY_INSERT [ProxyURLDomain] ON
	INSERT INTO [ProxyURLDomain]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description],[ValidationDomain],[ProductionDomain]) VALUES(1,'redirect.hp.com',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New', 'vtest.redirect.hp.com', 'ptest.redirect.hp.com')
	INSERT INTO [ProxyURLDomain]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description],[ValidationDomain],[ProductionDomain]) VALUES(2,'ie.redirect.hp.com',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New', 'vtest.redirect.hp.com', 'ptest.redirect.hp.com')
	SET IDENTITY_INSERT [ProxyURLDomain] OFF
	END
	BEGIN
	SET IDENTITY_INSERT [ProxyURLGroupType] ON
	INSERT INTO [ProxyURLGroupType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description]) VALUES(1,'redirect.hp.passthrou.new',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	INSERT INTO [ProxyURLGroupType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description]) VALUES(2,'redirect.hp.default.new',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	INSERT INTO [ProxyURLGroupType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description]) VALUES(3,'redirect.hp.simple.new',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	SET IDENTITY_INSERT [ProxyURLGroupType] OFF
	END
	BEGIN
	SET IDENTITY_INSERT [ProxyURLType] ON
	INSERT INTO [ProxyURLType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[ProxyURLDomainId],[ProxyURLGroupTypeId],[Description],[ElementsKey])VALUES(1,'Simple Redirect URLs',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 1, 3,'New',6)
	INSERT INTO [ProxyURLType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[ProxyURLDomainId],[ProxyURLGroupTypeId],[Description],[ElementsKey])VALUES(2,'All Other Redirect URLs (Desktop Icon)',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 1, 1, 'New',4)
	INSERT INTO [ProxyURLType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[ProxyURLDomainId],[ProxyURLGroupTypeId],[Description],[ElementsKey])VALUES(3,'Default Redirect URLs (IE Home/Search)',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 2, 2, 'New',3)
	SET IDENTITY_INSERT [ProxyURLType] OFF
	END	
	BEGIN
	SET IDENTITY_INSERT [ProxyURLStatus] ON
	INSERT INTO [ProxyURLStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (1,'Modified',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	INSERT INTO [ProxyURLStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (2,'ReadyForValidation',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	INSERT INTO [ProxyURLStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (3,'Validated',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	INSERT INTO [ProxyURLStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (4,'Published',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	INSERT INTO [ProxyURLStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (5,'Published (Production Only)',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	INSERT INTO [ProxyURLStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (6,'Cancelled',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	INSERT INTO [ProxyURLStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (7,'Abandoned',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'New')
	SET IDENTITY_INSERT [ProxyURLStatus] OFF
	END	
	-- Migrating target to Element CPS
    INSERT INTO [ProxyURL]
           ([CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[RowStatusId]
           ,[Description]
           ,[ProxyURLStatusId]
           ,[ProxyURLTypeId]
           ,[OwnerId]
           ,[URL]
           ,[ProductionId]
           ,[ValidationId])
    SELECT  DISTINCT
		    @strDefaultValue_CreatedBy, 
		    CONVERT(DATETIME, '2' + SUBSTRING(Creation_date, 2, 100), 120),  
		    @strDefaultValue_ModifiedBy,
		    CONVERT(DATETIME, '2' + SUBSTRING(Last_Update_date, 2, 100), 120),
		    @intRowStatus_Active,
		    LTRIM(RTRIM(Description)),
		    @intProxyStatus_Published,
		    ProxyURLTypeId = CASE WHEN Group_Type_ID = 9 THEN 1
								  WHEN Group_Type_ID = 7 THEN 2
								  WHEN Group_Type_ID = 8 THEN 3 
							 END,
		    @intOwner_System,
		    URL = CI.CONTENT_VALUE,
		    TARGET_ID,
		    TARGET_ID
	FROM dbo.STAGE_TARGET T WITH (NOLOCK)
		INNER JOIN dbo.STAGE_CONTENT C WITH (NOLOCK) ON T.CONTENT_ID = C.CONTENT_ID
		INNER JOIN dbo.STAGE_CONTENT_ITEM CI WITH (NOLOCK) ON C.CONTENT_ITEM_ID = CI.CONTENT_ITEM_ID
	-- WHERE (CI.CONTENT_KEY = 'url' OR CI.CONTENT_KEY = 'non_aol_url') AND Group_Type_ID IN (1,7,8,9)
	WHERE Group_Type_ID IN (7,8,9)
	-- Link ProxyURL and QueryparameterValue based on SELECTOR_GROUP table
    INSERT INTO [ProxyURL_QueryParameterValue]
           ([CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[ProxyURLId]
           ,[QueryParameterValueId])
	SELECT  DISTINCT @strDefaultValue_CreatedBy, 
		    @dtDefaultValue_CreatedOn,  
		    @strDefaultValue_ModifiedBy,
		    @dtDefaultValue_ModifiedOn,
		    P.Id, 
            S.SELECTOR_ITEM_ID
	FROM ProxyURL P
		INNER JOIN dbo.STAGE_TARGET T WITH (NOLOCK) ON T.Target_ID = P.ProductionId
		INNER JOIN dbo.STAGE_SELECTOR_GROUP S WITH (NOLOCK) ON T.GROUP_ID = S.GROUP_ID
		--INNER JOIN dbo.STAGE_SELECTOR_ITEM I WITH (NOLOCK) ON S.SELECTOR_ITEM_ID = I.SELECTOR_ITEM_ID
	-- Link ProxyURLType and Queryparameter based on GROUP_TYPE_DEFINITION table	
	INSERT INTO [QueryParameter_ProxyURLType]
           ([ProxyURLTypeId]
           ,[QueryParameterId]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
    Select
           ProxyURLTypeId = CASE WHEN Group_Type_ID = 9 THEN 1 
								  WHEN Group_Type_ID = 7 THEN 2
								  WHEN Group_Type_ID = 8 THEN 3 
							 END, 
           Q.Id,
           @strDefaultValue_CreatedBy, 
		   @dtDefaultValue_CreatedOn,  
		   @strDefaultValue_ModifiedBy,
		   @dtDefaultValue_ModifiedOn
	FROM QueryParameter Q
		INNER JOIN dbo.STAGE_GROUP_TYPE_DEFINITION G ON Q.Id = G.ATT_ID
	WHERE G.Group_Type_ID IN (7,8,9)
	/*============================Clean up per user request==================================*/
UPDATE QueryParameterValue
SET    RowStatusId = 2
FROM   QueryParameter AS p
       INNER JOIN QueryParameterValue
         ON p.Id = QueryParameterValue.QueryParameterId
WHERE  (p.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Brand')
       AND (QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'corssfire'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'desktop'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Pavilion'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'pavlion'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Presario'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smb')
       AND (QueryParameterValue.Id IN (SELECT QueryParameterValueId
                                       FROM   ProxyURL_QueryParameterValue))
DELETE FROM QueryParameterValue
FROM        QueryParameter AS p
            INNER JOIN QueryParameterValue
              ON p.Id = QueryParameterValue.QueryParameterId
WHERE       (p.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Brand')
            AND (QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'corssfire'
				  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'desktop'
				  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Pavilion'
				  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'pavlion'
				  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Presario'
				  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smb')
            AND (QueryParameterValue.Id NOT IN (SELECT QueryParameterValueId
                                                FROM   ProxyURL_QueryParameterValue)) 
UPDATE QueryParameterValue
SET    RowStatusId = 2
FROM   QueryParameter AS p
       INNER JOIN QueryParameterValue
         ON p.Id = QueryParameterValue.QueryParameterId
WHERE  (p.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Partner Category')
       AND (QueryParameterValue.Name LIKE '10%'
             OR QueryParameterValue.Name LIKE '35%'
             OR QueryParameterValue.Name LIKE '7000%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'cm%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'cp%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'dticon'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'ec%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'el%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'ex%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'f%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'Humetrix%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'ly%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'mp%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'msn%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'music%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'oem%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'oobe%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'option%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'orderprintswsm'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'osp%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'pa1%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'paginas%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Pandora'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'pms%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'ssver'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'start%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'tech%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'test'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'thankscontrol'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'ticketmaster'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'TiendaCompaq'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'tim'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'tr%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'tv%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'ty%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'win%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'yc%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'smb%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'smc%'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smforkids'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smhome'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smmp'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smom'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smrc'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'sms')
       AND (QueryParameterValue.Id IN (SELECT QueryParameterValueId
                                       FROM   ProxyURL_QueryParameterValue)) 
DELETE FROM QueryParameterValue
FROM        QueryParameter AS p
            INNER JOIN QueryParameterValue
              ON p.Id = QueryParameterValue.QueryParameterId
WHERE       (p.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Partner Category')
            AND (QueryParameterValue.Name LIKE '10%'
                  OR QueryParameterValue.Name LIKE '35%'
                  OR QueryParameterValue.Name LIKE '7000%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'cm%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'cp%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'dticon'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'ec%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'el%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'ex%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'f%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'Humetrix%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'ly%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'mp%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'msn%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'music%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'oem%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'oobe%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'option%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'orderprintswsm'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'osp%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'pa1%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'paginas%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Pandora'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'pms%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'ssver'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'start%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'tech%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'test'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'thankscontrol'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'ticketmaster'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'TiendaCompaq'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'tim'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'tr%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'tv%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'ty%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'win%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'yc%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'smb%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'smc%'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smforkids'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smhome'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smmp'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smom'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'smrc'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'sms')
            AND (QueryParameterValue.Id NOT IN (SELECT QueryParameterValueId
                                                FROM   ProxyURL_QueryParameterValue)) 
UPDATE QueryParameterValue
SET    RowStatusId = 2
FROM   QueryParameter AS p
       INNER JOIN QueryParameterValue
         ON p.Id = QueryParameterValue.QueryParameterId
WHERE  (p.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Platform')
       AND (QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'D%'
             OR QueryParameterValue.Name = '63'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'crossfire'
             OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'drsuess')
       AND (QueryParameterValue.Id IN (SELECT QueryParameterValueId
                                       FROM   ProxyURL_QueryParameterValue)) 
DELETE FROM QueryParameterValue
FROM        QueryParameter AS p
            INNER JOIN QueryParameterValue
              ON p.Id = QueryParameterValue.QueryParameterId
WHERE       (p.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Platform')
            AND (QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS LIKE 'D%'
                  OR QueryParameterValue.Name = '63'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'crossfire'
                  OR QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'drsuess')
            AND (QueryParameterValue.Id NOT IN (SELECT QueryParameterValueId
                                                FROM   ProxyURL_QueryParameterValue)) 
UPDATE QueryParameterValue
SET    RowStatusId = 2
FROM   QueryParameter AS p
       INNER JOIN QueryParameterValue
         ON p.Id = QueryParameterValue.QueryParameterId
WHERE  (p.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Cycle')
       AND (Isnumeric(QueryParameterValue.Name) = 0)
       AND (QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS NOT LIKE 'q%')
       AND (QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS <> 'all')
       AND (QueryParameterValue.Id IN (SELECT QueryParameterValueId
                                       FROM   ProxyURL_QueryParameterValue)) 
DELETE FROM QueryParameterValue
FROM        QueryParameter AS p
            INNER JOIN QueryParameterValue
              ON p.Id = QueryParameterValue.QueryParameterId
WHERE       (p.Name COLLATE SQL_Latin1_General_CP1_CS_AS = 'Cycle')
            AND (Isnumeric(QueryParameterValue.Name) = 0)
            AND (QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS NOT LIKE 'q%')
            AND (QueryParameterValue.Name COLLATE SQL_Latin1_General_CP1_CS_AS <> 'all')
            AND (QueryParameterValue.Id NOT IN (SELECT QueryParameterValueId
                                                FROM   ProxyURL_QueryParameterValue)) 
END
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
	INSERT INTO @TempProxyURLQueryParameterValueCount(ProxyURLId, ProxyURLQueryParameterValueCount)
	--SELECT DISTINCT [dbo].[ProxyURL].Id, (SELECT COUNT([dbo].[ProxyURL_QueryParameterValue].QueryParameterValueId) FROM [dbo].[ProxyURL_QueryParameterValue] WHERE [dbo].[ProxyURL_QueryParameterValue].ProxyURLId = [dbo].[ProxyURL].Id)
	SELECT [dbo].[ProxyURL].Id, COUNT([dbo].[ProxyURL].Id)
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
	-- The number of duplicate published proxyIds with identical parameters
    SELECT @DuplicateProxyURL = COUNT(ProxyURLId) FROM @TempProxyURLQueryParameterValueCount WHERE ProxyURLQueryParameterValueCount = @ProxyURLQueryParameterValueIdListCount
    RETURN @DuplicateProxyURL
   END
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
	DECLARE	@TempProxyURLQueryParameterValueCount TABLE (ProxyURLId INT, ProxyURLQueryParameterValueCount INT)		
	INSERT INTO @TempProxyURLQueryParameterValueCount(ProxyURLId, ProxyURLQueryParameterValueCount)
	SELECT [dbo].[ProxyURL].Id, COUNT([dbo].[ProxyURL].Id)
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
	-- Number of query parameters
	SELECT @ProxyURLQueryParameterValueIdListCount = COUNT(*) FROM @TempProxyURLQueryParameterValueIdList
	-- The number of duplicate published proxyIds with identical parameters
    SELECT @DuplicateProxyURL = COUNT(ProxyURLId) FROM @TempProxyURLQueryParameterValueCount WHERE ProxyURLQueryParameterValueCount = @ProxyURLQueryParameterValueIdListCount
    RETURN @DuplicateProxyURL
   END
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
PRINT N'Creating [dbo].[vwMapConfigurationServiceGroupSelector_QueryParameterValue]'
GO
CREATE VIEW [dbo].[vwMapConfigurationServiceGroupSelector_QueryParameterValue]
AS
SELECT     dbo.ConfigurationServiceGroupSelector_QueryParameterValue.Id, dbo.ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ModifiedOn, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ModifiedBy, dbo.ConfigurationServiceGroupSelector_QueryParameterValue.CreatedOn, dbo.ConfigurationServiceGroupSelector_QueryParameterValue.CreatedBy, 
                      dbo.QueryParameterValue.Name AS QueryParameterValueName, dbo.QueryParameterValue.QueryParameterId, 
                      dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, dbo.QueryParameter.ElementsKey
FROM         dbo.QueryParameterValue INNER JOIN
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) ON 
                      dbo.QueryParameterValue.Id = dbo.ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameterValue.QueryParameterId = dbo.QueryParameter.Id
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
                      dbo.ConfigurationServiceGroupType.ElementsKey AS ConfigurationServiceGroupTypeElementsKey, 
                      LanguageParameterValue.QueryParameterValueId AS LanguageParameterValueQueryParameterValueId, 
                      LanguageParameterValue.QueryParameterId AS LanguageParameterValueQueryParameterId, 
                      LanguageParameterValue.Name AS LanguageParameterValueName, 
                      CountryParameterValue.QueryParameterValueId AS CountryParameterValueQueryParameterValueId, 
                      CountryParameterValue.QueryParameterId AS CountryParameterValueQueryParameterId, 
                      CountryParameterValue.Name AS CountryParameterValueName, 
                      ReleaseParameterValue.QueryParameterValueId AS ReleaseParameterValueQueryParameterValueId, 
                      ReleaseParameterValue.QueryParameterId AS ReleaseParameterValueQueryParameterId, 
                      ReleaseParameterValue.Name AS ReleaseParameterValueName, 
                      OSVersionParameterValue.QueryParameterValueId AS OSVersionParameterValueQueryParameterValueId, 
                      OSVersionParameterValue.QueryParameterId AS OSVersionParameterValueQueryParameterId, 
                      OSVersionParameterValue.Name AS OSVersionParameterValueName, 
                      OSTypeParameterValue.QueryParameterValueId AS OSTypeParameterValueQueryParameterValueId, 
                      OSTypeParameterValue.QueryParameterId AS OSTypeParameterValueQueryParameterId, 
                      OSTypeParameterValue.Name AS OSTypeParameterValueName, 
                      PCBrandParameterValue.QueryParameterValueId AS PCBrandParameterValueQueryParameterValueId, 
                      PCBrandParameterValue.QueryParameterId AS PCBrandParameterValueQueryParameterId, 
                      PCBrandParameterValue.Name AS PCBrandParameterValueName, 
                      PCPlatformParameterValue.QueryParameterValueId AS PCPlatformParameterValueQueryParameterValueId, 
                      PCPlatformParameterValue.QueryParameterId AS PCPlatformParameterValueQueryParameterId, 
                      PCPlatformParameterValue.Name AS PCPlatformParameterValueName, dbo.ConfigurationServiceGroup_GetTagList(dbo.ConfigurationServiceGroup.Id, 
                      ', ', 1) AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.ConfigurationServiceGroup_Tag WITH (NOLOCK)
                            WHERE      (dbo.ConfigurationServiceGroup.Id = ConfigurationServiceGroupId)) AS TagCount, dbo.ConfigurationServiceGroup.ProductionId, 
                      dbo.ConfigurationServiceGroup.ValidationId
FROM         dbo.ConfigurationServiceGroup WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroup.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.ConfigurationServiceGroup.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.ConfigurationServiceGroupStatus WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupStatusId = dbo.ConfigurationServiceGroupStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupTypeId = dbo.ConfigurationServiceGroupType.Id LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId, QueryParameterValue.QueryParameterId, 
                                      QueryParameterValue.Name, ConfigurationServiceGroup.Id AS ConfigurationServiceGroupId
                           FROM       ConfigurationServiceGroupSelector_QueryParameterValue AS ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                      QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
                                      ConfigurationServiceGroup WITH (NOLOCK) ON ConfigurationServiceGroupSelector_QueryParameterValue.Id = ConfigurationServiceGroup.Id
                           WHERE     (QueryParameterValue.QueryParameterId = 100)) AS LanguageParameterValue ON 
                      dbo.ConfigurationServiceGroup.Id = LanguageParameterValue.ConfigurationServiceGroupId LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId, QueryParameterValue.QueryParameterId, 
                                      QueryParameterValue.Name, ConfigurationServiceGroup.Id AS ConfigurationServiceGroupId
                            FROM      dbo.ConfigurationServiceGroupSelector_QueryParameterValue AS ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                      dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
                                      ConfigurationServiceGroup WITH (NOLOCK) ON ConfigurationServiceGroupSelector_QueryParameterValue.Id = ConfigurationServiceGroup.Id
                            WHERE     (QueryParameterValue.QueryParameterId = 101)) AS CountryParameterValue ON 
                      dbo.ConfigurationServiceGroup.Id = CountryParameterValue.ConfigurationServiceGroupId LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId, QueryParameterValue.QueryParameterId, 
                                      QueryParameterValue.Name, ConfigurationServiceGroup.Id AS ConfigurationServiceGroupId
                            FROM      dbo.ConfigurationServiceGroupSelector_QueryParameterValue AS ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                      dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
                                      ConfigurationServiceGroup WITH (NOLOCK) ON ConfigurationServiceGroupSelector_QueryParameterValue.Id = ConfigurationServiceGroup.Id
                            WHERE     (QueryParameterValue.QueryParameterId = 102)) AS ReleaseParameterValue ON 
                      dbo.ConfigurationServiceGroup.Id = ReleaseParameterValue.ConfigurationServiceGroupId LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId, QueryParameterValue.QueryParameterId, 
                                      QueryParameterValue.Name, ConfigurationServiceGroup.Id AS ConfigurationServiceGroupId
                            FROM      dbo.ConfigurationServiceGroupSelector_QueryParameterValue AS ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                      dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
                                      ConfigurationServiceGroup WITH (NOLOCK) ON ConfigurationServiceGroupSelector_QueryParameterValue.Id = ConfigurationServiceGroup.Id
                            WHERE     (QueryParameterValue.QueryParameterId = 103)) AS OSVersionParameterValue ON 
                      dbo.ConfigurationServiceGroup.Id = OSVersionParameterValue.ConfigurationServiceGroupId LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId, QueryParameterValue.QueryParameterId, 
                                      QueryParameterValue.Name, ConfigurationServiceGroup.Id AS ConfigurationServiceGroupId
                            FROM      dbo.ConfigurationServiceGroupSelector_QueryParameterValue AS ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                      dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
                                      ConfigurationServiceGroup WITH (NOLOCK) ON ConfigurationServiceGroupSelector_QueryParameterValue.Id = ConfigurationServiceGroup.Id
                            WHERE     (QueryParameterValue.QueryParameterId = 104)) AS OSTypeParameterValue ON 
                      dbo.ConfigurationServiceGroup.Id = OSTypeParameterValue.ConfigurationServiceGroupId LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId, QueryParameterValue.QueryParameterId, 
                                      QueryParameterValue.Name, ConfigurationServiceGroup.Id AS ConfigurationServiceGroupId
                            FROM      dbo.ConfigurationServiceGroupSelector_QueryParameterValue AS ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                      dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
                                      ConfigurationServiceGroup WITH (NOLOCK) ON ConfigurationServiceGroupSelector_QueryParameterValue.Id = ConfigurationServiceGroup.Id
                            WHERE     (QueryParameterValue.QueryParameterId = 105)) AS PCBrandParameterValue ON 
                      dbo.ConfigurationServiceGroup.Id = PCBrandParameterValue.ConfigurationServiceGroupId LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId, QueryParameterValue.QueryParameterId, 
                                      QueryParameterValue.Name, ConfigurationServiceGroup.Id AS ConfigurationServiceGroupId
                            FROM      dbo.ConfigurationServiceGroupSelector_QueryParameterValue AS ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                      dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                      ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id INNER JOIN
                                      ConfigurationServiceGroup WITH (NOLOCK) ON ConfigurationServiceGroupSelector_QueryParameterValue.Id = ConfigurationServiceGroup.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 106)) AS PCPlatformParameterValue ON 
                      dbo.ConfigurationServiceGroup.Id = PCPlatformParameterValue.ConfigurationServiceGroupId
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
PRINT N'Adding foreign keys to [dbo].[ConfigurationServiceGroupSelector]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupSelector_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id]),
CONSTRAINT [FK_ConfigurationServiceGroupSelector_ConfigurationServiceGroup] FOREIGN KEY ([ConfigurationServiceGroupId]) REFERENCES [dbo].[ConfigurationServiceGroup] ([Id])
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
PRINT N'Adding foreign keys to [dbo].[NoteType]'
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD
CONSTRAINT [FK_NoteType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
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
