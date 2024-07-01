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
ALTER TABLE [dbo].[WorkflowModuleVersion] ADD CONSTRAINT [PK_WorkflowModuleVersion] PRIMARY KEY NONCLUSTERED  ([Id])

ALTER TABLE [dbo].[WorkflowModuleVersion] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModuleVersion_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
ALTER TABLE [dbo].[WorkflowModuleVersion] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModuleVersion_WorkflowModuleCategory] FOREIGN KEY ([WorkflowModuleCategoryId]) REFERENCES [dbo].[WorkflowModuleCategory] ([Id])
ALTER TABLE [dbo].[WorkflowModuleVersion] WITH NOCHECK ADD
CONSTRAINT [FK_WorkflowModuleVersion_WorkflowModuleSubCategory] FOREIGN KEY ([WorkflowModuleSubCategoryId]) REFERENCES [dbo].[WorkflowModuleSubCategory] ([Id])
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_WorkflowModuleVersion_CategoryIdSubCategoryIdVersionMajor] ON [dbo].[WorkflowModuleVersion] ([WorkflowModuleCategoryId], [WorkflowModuleSubCategoryId], [VersionMajor])
GO
