CREATE TABLE [dbo].[QueryParameter_WorkflowType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[WorkflowTypeId] [int] NOT NULL,
[QueryParameterId] [int] NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MaximumSelection] [int] NOT NULL DEFAULT ((0)),
[Wildcard] [bit] NOT NULL DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] ADD PRIMARY KEY NONCLUSTERED  ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_QueryParameter_WorkflowType_QueryParameterId_WorkflowTypeId] ON [dbo].[QueryParameter_WorkflowType] ([WorkflowTypeId], [QueryParameterId])
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
GO
ALTER TABLE [dbo].[QueryParameter_WorkflowType] WITH NOCHECK ADD FOREIGN KEY ([WorkflowTypeId]) REFERENCES [dbo].[WorkflowType] ([Id])
GO
