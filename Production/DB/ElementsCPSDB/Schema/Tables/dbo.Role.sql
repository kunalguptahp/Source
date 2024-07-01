CREATE TABLE [dbo].[Role]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Role_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Role_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_Role_RowStatusId] DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD
CONSTRAINT [CK_Role_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD
CONSTRAINT [CK_Role_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD
CONSTRAINT [CK_Role_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD
CONSTRAINT [CK_Role_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
ALTER TABLE [dbo].[Role] WITH NOCHECK ADD
CONSTRAINT [FK_Role_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO

ALTER TABLE [dbo].[Role] ADD CONSTRAINT [PK_Role] PRIMARY KEY NONCLUSTERED  ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Role_Name] ON [dbo].[Role] ([Name])
GO

EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'Role', 'CONSTRAINT', N'CK_Role_Name'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is at least 3 characters.', 'SCHEMA', N'dbo', 'TABLE', N'Role', 'CONSTRAINT', N'CK_Role_Name_MinLen'
GO
