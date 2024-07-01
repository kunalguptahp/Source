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
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceGroup]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] DROP
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceApplication],
CONSTRAINT [FK_ConfigurationServiceGroup_RowStatus],
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceGroupStatus],
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceGroupType],
CONSTRAINT [FK_ConfigurationServiceGroup_Person]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceApplication]'
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] DROP
CONSTRAINT [FK_ConfigurationServiceApplication_RowStatus]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceGroup_ConfigurationServiceLabelValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_ConfigurationServiceLabelValue] DROP
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceLabelValue_ConfigurationServiceGroup],
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceLabelValue_ConfigurationServiceLabelValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceGroup_Tag]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_Tag] DROP
CONSTRAINT [FK_ConfigurationServiceGroup_Tag_ConfigurationServiceGroup],
CONSTRAINT [FK_ConfigurationServiceGroup_Tag_Tag]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceGroupSelector]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] DROP
CONSTRAINT [FK_ConfigurationServiceGroupSelector_ConfigurationServiceGroup],
CONSTRAINT [FK_ConfigurationServiceGroupSelector_RowStatus]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue] DROP
CONSTRAINT [FK_ConfigurationServiceGroupSelector_QueryParameterValue_ConfigurationServiceGroupSelector],
CONSTRAINT [FK_ConfigurationServiceGroupSelector_QueryParameterValue_QueryParameterValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceGroupStatus]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] DROP
CONSTRAINT [FK_ConfigurationServiceGroupStatus_RowStatus]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[QueryParameter_ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] DROP
CONSTRAINT [FK_QueryParameter_ConfigurationServiceGroupType_ConfigurationServicedGroupType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceGroupType]'
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupType] DROP
CONSTRAINT [FK_ConfigurationServiceGroupType_RowStatus]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceLabel]'
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] DROP
CONSTRAINT [FK_ConfigurationServiceLabel_ConfigurationServiceItem],
CONSTRAINT [FK_ConfigurationServiceLabel_RowStatus]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[ConfigurationServiceItem]'
GO
ALTER TABLE [dbo].[ConfigurationServiceItem] DROP
CONSTRAINT [FK_ConfigurationServiceItem_RowStatus]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_TARGET_SERVER_GROUP_TYPE]'
GO
DROP TABLE [dbo].[STAGE_TARGET_SERVER_GROUP_TYPE]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_TARGET_SERVER]'
GO
DROP TABLE [dbo].[STAGE_TARGET_SERVER]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_GROUP_TYPE]'
GO
DROP TABLE [dbo].[STAGE_GROUP_TYPE]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceGroup_ConfigurationServiceLabelValue]'
GO
DROP TABLE [dbo].[ConfigurationServiceGroup_ConfigurationServiceLabelValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceGroupSelector]'
GO
DROP TABLE [dbo].[ConfigurationServiceGroupSelector]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceGroup]'
GO
DROP TABLE [dbo].[ConfigurationServiceGroup]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceGroupStatus]'
GO
DROP TABLE [dbo].[ConfigurationServiceGroupStatus]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceGroup_Tag]'
GO
DROP TABLE [dbo].[ConfigurationServiceGroup_Tag]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceApplication]'
GO
DROP TABLE [dbo].[ConfigurationServiceApplication]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[usp_ElementsCPS_DataMigration]'
GO
DROP PROCEDURE [dbo].[usp_ElementsCPS_DataMigration]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_TARGET]'
GO
DROP TABLE [dbo].[STAGE_TARGET]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceLabelValue]'
GO
DROP TABLE [dbo].[ConfigurationServiceLabelValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceLabel]'
GO
DROP TABLE [dbo].[ConfigurationServiceLabel]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceGroupType]'
GO
DROP TABLE [dbo].[ConfigurationServiceGroupType]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[spReplaceProxyURLs]'
GO
DROP PROCEDURE [dbo].[spReplaceProxyURLs]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[spImportProxyURLs]'
GO
DROP PROCEDURE [dbo].[spImportProxyURLs]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceItem]'
GO
DROP TABLE [dbo].[ConfigurationServiceItem]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue]'
GO
DROP TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_ATTRIBUTE]'
GO
DROP TABLE [dbo].[STAGE_ATTRIBUTE]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_CONTENT]'
GO
DROP TABLE [dbo].[STAGE_CONTENT]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_CONTENT_ITEM]'
GO
DROP TABLE [dbo].[STAGE_CONTENT_ITEM]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_GROUP_TYPE_DEFINITION]'
GO
DROP TABLE [dbo].[STAGE_GROUP_TYPE_DEFINITION]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_SELECTOR_GROUP]'
GO
DROP TABLE [dbo].[STAGE_SELECTOR_GROUP]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[STAGE_SELECTOR_ITEM]'
GO
DROP TABLE [dbo].[STAGE_SELECTOR_ITEM]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[TempReplaceProxyURL]'
GO
DROP TABLE [dbo].[TempReplaceProxyURL]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[TempImportProxyURL]'
GO
--DROP TABLE [dbo].[TempImportProxyURL]
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
