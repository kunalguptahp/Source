CREATE TABLE [dbo].[Person]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] AS (((((([LastName]+', ')+[FirstName])+isnull(' '+[MiddleName],''))+' (')+[WindowsId])+')'),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Person_CreatedOn] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Person_ModifiedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_Person_RowStatusId] DEFAULT ((1)),
[WindowsId] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MiddleName] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [CK_Person_Email_IsTrimmed] CHECK ((len(ltrim(rtrim([Email])))=len([Email])))
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [CK_Person_FirstName] CHECK ((len(rtrim([FirstName]))>(0)))
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [CK_Person_FirstName_IsTrimmed] CHECK ((len(ltrim(rtrim([FirstName])))=len([FirstName])))
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [CK_Person_LastName] CHECK ((len(rtrim([LastName]))>(0)))
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [CK_Person_LastName_IsTrimmed] CHECK ((len(ltrim(rtrim([LastName])))=len([LastName])))
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [CK_Person_MiddleName_IsTrimmed] CHECK ((len(ltrim(rtrim([MiddleName])))=len([MiddleName])))
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [CK_Person_WindowsId] CHECK ((len(rtrim([WindowsId]))>(0)))
ALTER TABLE [dbo].[Person] WITH NOCHECK ADD
CONSTRAINT [FK_Person_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO

ALTER TABLE [dbo].[Person] ADD CONSTRAINT [PK_user] PRIMARY KEY NONCLUSTERED  ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Person_WindowsId] ON [dbo].[Person] ([WindowsId])
GO

EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'Person', 'CONSTRAINT', N'CK_Person_FirstName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'Person', 'CONSTRAINT', N'CK_Person_LastName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'Person', 'CONSTRAINT', N'CK_Person_WindowsId'
GO
