

/*
Script created by SQL Data Compare version 7.1.0.230 from Red Gate Software Ltd at 6/21/2010 3:04:56 PM

Run this script on (local).ElementsCPSDBProSave

This script will make changes to (local).ElementsCPSDBProSave to make it the same as (local).ElementsCPSDB

Note that this script will carry out all DELETE commands for all tables first, then all the UPDATES and then all the INSERTS
It will disable foreign key constraints at the beginning of the script, and re-enable them at the end
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO
SET DATEFORMAT YMD
GO
-- Pointer used for text / image updates. This might not be needed, but is declared here just in case
DECLARE @pv binary(16)

BEGIN TRANSACTION

-- Drop constraints from [dbo].[NoteType]
ALTER TABLE [dbo].[NoteType] DROP CONSTRAINT [FK_NoteType_RowStatus]

-- Drop constraints from [dbo].[EntityType]
ALTER TABLE [dbo].[EntityType] DROP CONSTRAINT [FK_EntityType_RowStatus]

-- Add 38 rows to [dbo].[EntityType]
SET IDENTITY_INSERT [dbo].[EntityType] ON
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (1, N'Aardvark', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (2, N'Addendum', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (3, N'AddendumType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (4, N'DDLChangeLog', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (5, N'EntityType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (6, N'Log', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (7, N'Note', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (8, N'NoteType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (9, N'Person', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (10, N'Person_Role', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (11, N'Role', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (12, N'RowStatus', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (13, N'Tag', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (14, N'Task', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (15, N'TaskType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (16, N'ConfigurationServiceApplication', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (17, N'ConfigurationServiceGroup', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (18, N'ConfigurationServiceGroup_ConfigurationServiceLabelValue', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (19, N'ConfigurationServiceGroup_Tag', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (20, N'ConfigurationServiceGroupSelector', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (21, N'ConfigurationServiceGroupSelector_QueryParameterValue', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (22, N'ConfigurationServiceGroupStatus', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (23, N'ConfigurationServiceGroupType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (24, N'ConfigurationServiceItem', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (25, N'ConfigurationServiceLabel', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (26, N'ConfigurationServiceLabelValue', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (27, N'ProxyURL', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (28, N'ProxyURL_QueryParameterValue', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (29, N'ProxyURL_Tag', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (30, N'ProxyURLDomain', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (31, N'ProxyURLGroupType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (32, N'ProxyURLStatus', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (33, N'ProxyURLType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (34, N'PublishTemp', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (35, N'QueryParameter', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (36, N'QueryParameter_ConfigurationServiceGroupType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (37, N'QueryParameter_ProxyURLType', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[EntityType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (38, N'QueryParameterValue', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
SET IDENTITY_INSERT [dbo].[EntityType] OFF

-- Add 8 rows to [dbo].[NoteType]
SET IDENTITY_INSERT [dbo].[NoteType] ON
INSERT INTO [dbo].[NoteType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (1, N'Debug', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[NoteType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (2, N'Info', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[NoteType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (3, N'Warning', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[NoteType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (4, N'Error', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[NoteType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (5, N'Milestone', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[NoteType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (6, N'State Change', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[NoteType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (7, N'Data Change', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
INSERT INTO [dbo].[NoteType] ([Id], [Name], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [RowStatusId], [Comment]) VALUES (8, N'Data Change (Related Data)', N'system\system', '2009-01-01 00:00:00.000', N'system\system', '2009-01-01 00:00:00.000', 1, N'')
SET IDENTITY_INSERT [dbo].[NoteType] OFF

-- Add constraints to [dbo].[NoteType]
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [FK_NoteType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])

-- Add constraints to [dbo].[EntityType]
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [FK_EntityType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])

COMMIT TRANSACTION
GO

-- Reseed identity on [dbo].[NoteType]
DBCC CHECKIDENT('[dbo].[NoteType]', RESEED, 8)
GO

-- Reseed identity on [dbo].[EntityType]
DBCC CHECKIDENT('[dbo].[EntityType]', RESEED, 38)
GO
