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
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD CHECK ((len(rtrim([Name]))>=(2)))
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] ADD PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[WorkflowModuleSubCategory] WITH NOCHECK ADD FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
