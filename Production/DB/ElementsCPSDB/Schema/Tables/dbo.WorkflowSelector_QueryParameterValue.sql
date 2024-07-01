CREATE TABLE [dbo].[WorkflowSelector_QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[WorkflowSelectorId] [int] NOT NULL,
[QueryParameterValueId] [int] NOT NULL,
[Negation] [bit] NOT NULL DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[WorkflowSelector_QueryParameterValue] ADD PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[WorkflowSelector_QueryParameterValue] WITH NOCHECK ADD FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id])
GO
ALTER TABLE [dbo].[WorkflowSelector_QueryParameterValue] WITH NOCHECK ADD FOREIGN KEY ([WorkflowSelectorId]) REFERENCES [dbo].[WorkflowSelector] ([Id])
GO
