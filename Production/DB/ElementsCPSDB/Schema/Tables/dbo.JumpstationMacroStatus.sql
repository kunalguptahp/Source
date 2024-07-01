CREATE TABLE [dbo].[JumpstationMacroStatus]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_JumpstationMacroStatus_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_JumpstationMacroStatus_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_JumpstationMacroStatus_RowStatusId] DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacroStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacroStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacroStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationMacroStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] ADD CONSTRAINT [PK_JumpstationMacroStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[JumpstationMacroStatus] WITH NOCHECK ADD CONSTRAINT [FK_JumpstationMacroStatus_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
