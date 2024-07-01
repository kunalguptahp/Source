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
ALTER TABLE [dbo].[WorkflowModule] ADD CONSTRAINT [PK_WorkflowModule] PRIMARY KEY NONCLUSTERED  ([Id])

ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [CK_WorkflowModule_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [CK_WorkflowModule_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [CK_WorkflowModule_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [CK_WorkflowModule_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_WorkflowModuleCategory] FOREIGN KEY ([WorkflowModuleCategoryId]) REFERENCES [dbo].[WorkflowModuleCategory] ([Id])
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_WorkflowStatus] FOREIGN KEY ([WorkflowModuleStatusId]) REFERENCES [dbo].[WorkflowStatus] ([Id])
ALTER TABLE [dbo].[WorkflowModule] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModule_WorkflowModuleSubCategory] FOREIGN KEY ([WorkflowModuleSubCategoryId]) REFERENCES [dbo].[WorkflowModuleSubCategory] ([Id])
GO
