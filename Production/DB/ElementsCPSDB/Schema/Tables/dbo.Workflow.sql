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
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[Workflow] ADD PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD FOREIGN KEY ([WorkflowApplicationId]) REFERENCES [dbo].[WorkflowApplication] ([Id])
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD FOREIGN KEY ([WorkflowStatusId]) REFERENCES [dbo].[WorkflowStatus] ([Id])
GO
ALTER TABLE [dbo].[Workflow] WITH NOCHECK ADD FOREIGN KEY ([WorkflowTypeId]) REFERENCES [dbo].[WorkflowType] ([Id])
GO
