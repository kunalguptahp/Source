CREATE TABLE [dbo].[WorkflowVersion]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[VersionMajor] [int] NOT NULL,
[VersionMinor] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1))
)
GO
ALTER TABLE [dbo].[WorkflowVersion] ADD PRIMARY KEY NONCLUSTERED  ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_WorkflowVersion_VersionMajor] ON [dbo].[WorkflowVersion] ([VersionMajor])
GO
ALTER TABLE [dbo].[WorkflowVersion] WITH NOCHECK ADD FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
