/*
Run this script on:

        (local).ElementsCPSDBITG    -  This database will be modified

to synchronize it with:

        (local).ElementsCPSDBEmpty

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.2.0 from Red Gate Software Ltd at 10/14/2010 2:32:34 PM

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

PRINT 'Adding rows to [dbo].[ConfigurationServiceApplicationType]'
BEGIN
SET IDENTITY_INSERT [dbo].[ConfigurationServiceApplicationType] ON
INSERT INTO [dbo].[ConfigurationServiceApplicationType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(1,'UsageTracking', 'americas\system', getdate(), 'americas\system',getdate(),1,'Usage Tracking Application')
INSERT INTO [dbo].[ConfigurationServiceApplicationType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(2,'Cyclone', 'americas\system', getdate(), 'americas\system',getdate(),1,'Cyclone Application')
SET IDENTITY_INSERT [dbo].[ConfigurationServiceApplicationType] OFF
END	
GO

PRINT N'Add columns [dbo].[ConfigurationServiceApplication]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] ADD [ConfigurationServiceApplicationTypeId] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

UPDATE [dbo].[ConfigurationServiceApplication] SET ConfigurationServiceApplicationTypeId=1
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
UPDATE [dbo].[ConfigurationServiceApplication] SET ConfigurationServiceApplicationTypeId=2 WHERE ElementsKey = 'HPApplicationAssistant'
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

PRINT N'Add columns [dbo].[ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] ADD [ConfigurationServiceApplicationId] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

UPDATE [dbo].[ConfigurationServiceGroupType] SET ConfigurationServiceApplicationId=1 WHERE Name IN ('appconfig','tracking')
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

UPDATE [dbo].[ConfigurationServiceGroupType] SET ConfigurationServiceApplicationId=2 WHERE Name IN ('CycloneConfig')
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] ADD [Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] ADD [Description] [nvarchar](512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] ADD [MaximumSelection] [int] NOT NULL DEFAULT ((0))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] ADD [Wildcard] [bit] NOT NULL DEFAULT ((0))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

UPDATE [dbo].[QueryParameter_ConfigurationServiceGroupType] SET Name='QueryParameter_ConfigurationServiceGroupType - ' + STR(id), Wildcard=1
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

PRINT N'Add columns [dbo].[QueryParameter_JumpstationGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] ADD [Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] ADD [Description] [nvarchar](512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

UPDATE [dbo].[QueryParameter_JumpstationGroupType] SET Name='QueryParameter_JumpstationGroupType - ' + STR(id)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

PRINT N'Add columns [dbo].[QueryParameter_ProxyURLType]'
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] ADD [Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] ADD [Description] [nvarchar](512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

UPDATE [dbo].[QueryParameter_ProxyURLType] SET Name='QueryParameter_ProxyURLType - ' + STR(id)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
