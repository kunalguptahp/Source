CREATE TABLE [dbo].[JumpstationGroupSelector]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[JumpstationGroupId] [int] NOT NULL
)
CREATE NONCLUSTERED INDEX [IX_JumpstationGroupSelector_JumpstationGroupId] ON [dbo].[JumpstationGroupSelector] ([JumpstationGroupId])

ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupSelector_JumpstationGroup] FOREIGN KEY ([JumpstationGroupId]) REFERENCES [dbo].[JumpstationGroup] ([Id])
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupSelector_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupSelector_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupSelector_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupSelector_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[JumpstationGroupSelector] ADD CONSTRAINT [PK_JumpstationGroupSelector] PRIMARY KEY NONCLUSTERED  ([Id])
GO

ALTER TABLE [dbo].[JumpstationGroupSelector] WITH NOCHECK ADD CONSTRAINT [FK_JumpstationGroupSelector_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
