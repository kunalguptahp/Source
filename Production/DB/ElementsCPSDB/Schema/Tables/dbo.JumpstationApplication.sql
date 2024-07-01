CREATE TABLE [dbo].[JumpstationApplication]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_JumpstationApplication_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_JumpstationApplication_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_JumpstationApplication_RowStatusId] DEFAULT ((1)),
[Version] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationApplication_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])


GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[JumpstationApplication] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationApplication_Version_IsTrimmed] CHECK ((len(ltrim(rtrim([Version])))=len([Version])))
GO
ALTER TABLE [dbo].[JumpstationApplication] ADD CONSTRAINT [PK_JumpstationApplication] PRIMARY KEY NONCLUSTERED  ([Id])
GO
