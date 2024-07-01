CREATE TABLE [dbo].[JumpstationGroupSelector_QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[JumpstationGroupSelectorId] [int] NOT NULL,
[QueryParameterValueId] [int] NOT NULL,
[Negation] [bit] NOT NULL DEFAULT ((0))
)
CREATE NONCLUSTERED INDEX [IX_JumpstationGroupSelector_QueryParameterValue_JumpstationGroupSelectorId] ON [dbo].[JumpstationGroupSelector_QueryParameterValue] ([JumpstationGroupSelectorId])

ALTER TABLE [dbo].[JumpstationGroupSelector_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupSelector_QueryParameterValue_JumpstationGroupSelector] FOREIGN KEY ([JumpstationGroupSelectorId]) REFERENCES [dbo].[JumpstationGroupSelector] ([Id])
ALTER TABLE [dbo].[JumpstationGroupSelector_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupSelector_QueryParameterValue_QueryParameterValue] FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id])
GO
ALTER TABLE [dbo].[JumpstationGroupSelector_QueryParameterValue] ADD CONSTRAINT [PK_JumpstationGroupSelector_QueryParameterValue] PRIMARY KEY CLUSTERED  ([Id])
GO
