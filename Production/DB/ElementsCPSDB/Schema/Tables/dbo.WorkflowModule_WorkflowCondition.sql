CREATE TABLE [dbo].[WorkflowModule_WorkflowCondition]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[WorkflowConditionId] [int] NOT NULL,
[WorkflowModuleId] [int] NOT NULL
)
GO
ALTER TABLE [dbo].[WorkflowModule_WorkflowCondition] ADD PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[WorkflowModule_WorkflowCondition] WITH NOCHECK ADD FOREIGN KEY ([WorkflowConditionId]) REFERENCES [dbo].[WorkflowCondition] ([Id])
GO
ALTER TABLE [dbo].[WorkflowModule_WorkflowCondition] WITH NOCHECK ADD FOREIGN KEY ([WorkflowModuleId]) REFERENCES [dbo].[WorkflowModule] ([Id])
GO
