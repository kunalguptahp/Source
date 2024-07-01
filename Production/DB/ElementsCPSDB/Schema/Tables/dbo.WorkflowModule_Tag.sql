CREATE TABLE [dbo].[WorkflowModule_Tag]
(
[WorkflowModuleId] [int] NOT NULL,
[TagId] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate())
)
GO
ALTER TABLE [dbo].[WorkflowModule_Tag] ADD PRIMARY KEY NONCLUSTERED  ([WorkflowModuleId], [TagId])
GO
ALTER TABLE [dbo].[WorkflowModule_Tag] WITH NOCHECK ADD FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
ALTER TABLE [dbo].[WorkflowModule_Tag] WITH NOCHECK ADD FOREIGN KEY ([WorkflowModuleId]) REFERENCES [dbo].[WorkflowModule] ([Id])
GO
