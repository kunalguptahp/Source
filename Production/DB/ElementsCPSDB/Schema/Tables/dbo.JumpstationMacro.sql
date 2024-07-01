SET ANSI_PADDING ON

CREATE TABLE [dbo].[JumpstationMacro]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[JumpstationMacroStatusId] [int] NOT NULL,
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OwnerId] [int] NOT NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL,
[DefaultResultValue] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT ('Invalid')
)

ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacro_JumpstationMacroStatus] FOREIGN KEY ([JumpstationMacroStatusId]) REFERENCES [dbo].[JumpstationMacroStatus] ([Id])

ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacro_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationMacro_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])

GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacro_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacro_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacro_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[JumpstationMacro] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacro_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[JumpstationMacro] ADD CONSTRAINT [PK_JumpstationMacro] PRIMARY KEY NONCLUSTERED  ([Id])
GO