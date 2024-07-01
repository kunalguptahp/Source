CREATE TABLE [dbo].[Workflow_WorkflowModule]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[WorkflowId] [int] NOT NULL,
[WorkflowModuleId] [int] NOT NULL,
[SortOrder] [int] NOT NULL
)
GO
ALTER TABLE [dbo].[Workflow_WorkflowModule] ADD PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Workflow_WorkflowModule] WITH NOCHECK ADD FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflow] ([Id])
GO
ALTER TABLE [dbo].[Workflow_WorkflowModule] WITH NOCHECK ADD FOREIGN KEY ([WorkflowModuleId]) REFERENCES [dbo].[WorkflowModule] ([Id])
GO
