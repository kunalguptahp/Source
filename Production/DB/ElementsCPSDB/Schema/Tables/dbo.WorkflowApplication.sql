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
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD CHECK ((len(ltrim(rtrim([Version])))=len([Version])))
GO
ALTER TABLE [dbo].[WorkflowApplication] ADD PRIMARY KEY NONCLUSTERED  ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_WorkflowApplication_ElementsKey] ON [dbo].[WorkflowApplication] ([ElementsKey])
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
ALTER TABLE [dbo].[WorkflowApplication] WITH NOCHECK ADD FOREIGN KEY ([WorkflowApplicationTypeId]) REFERENCES [dbo].[WorkflowApplicationType] ([Id])
GO
