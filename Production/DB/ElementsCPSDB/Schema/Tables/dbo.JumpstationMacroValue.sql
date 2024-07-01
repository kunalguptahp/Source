CREATE TABLE [dbo].[JumpstationMacroValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[JumpstationMacroId] [int] NOT NULL,
[MatchName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ResultValue] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
ALTER TABLE [dbo].[JumpstationMacroValue] ADD CONSTRAINT [PK_JumpstationMacroValue] PRIMARY KEY NONCLUSTERED  ([Id])

ALTER TABLE [dbo].[JumpstationMacroValue] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacroValue_JumpstationMacro] FOREIGN KEY ([JumpstationMacroId]) REFERENCES [dbo].[JumpstationMacro] ([Id])
ALTER TABLE [dbo].[JumpstationMacroValue] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacroValue_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
