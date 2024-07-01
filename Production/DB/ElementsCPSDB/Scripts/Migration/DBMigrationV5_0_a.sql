/*
Run this script on:

        (local).ElementsCPSDBTest    -  This database will be modified

to synchronize it with:

        (local).ElementsCPSDB

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.50.10 from Red Gate Software Ltd at 1/9/2012 1:23:54 PM

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
PRINT N'Dropping foreign keys from [dbo].[WorkflowModule]'
GO
ALTER TABLE [dbo].[WorkflowModule] DROP
CONSTRAINT [FK_WorkflowModule_WorkflowStatus],
CONSTRAINT [FK_WorkflowModule_Person]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping foreign keys from [dbo].[WorkflowModuleVersion]'
GO
ALTER TABLE [dbo].[WorkflowModuleVersion] DROP
CONSTRAINT [FK_WorkflowModuleVersion_WorkflowModuleCategory],
CONSTRAINT [FK_WorkflowModuleVersion_WorkflowModuleSubCategory]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[WorkflowModule]'
GO
ALTER TABLE [dbo].[WorkflowModule] ALTER COLUMN [Filename] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
ALTER TABLE [dbo].[WorkflowModule] ALTER COLUMN [WorkflowModuleStatusId] [int] NOT NULL
ALTER TABLE [dbo].[WorkflowModule] ALTER COLUMN [OwnerId] [int] NOT NULL
ALTER TABLE [dbo].[WorkflowModule] ALTER COLUMN [Title] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[vwMapWorkflow]'
GO
ALTER VIEW dbo.vwMapWorkflow
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
PRINT N'Adding foreign keys to [dbo].[WorkflowModule]'
GO
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_WorkflowStatus] FOREIGN KEY ([WorkflowModuleStatusId]) REFERENCES [dbo].[WorkflowStatus] ([Id]),
CONSTRAINT [FK_WorkflowModule_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[WorkflowModuleVersion]'
GO
ALTER TABLE [dbo].[WorkflowModuleVersion] WITH NOCHECK ADD
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
