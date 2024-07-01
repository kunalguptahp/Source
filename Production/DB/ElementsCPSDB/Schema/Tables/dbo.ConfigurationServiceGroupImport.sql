CREATE TABLE [dbo].[ConfigurationServiceGroupImport]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceGroupTypeName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConfigurationServiceApplicationName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProductionId] [int] NULL,
[ImportMessage] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ImportStatus] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceGroupId] [int] NULL,
[ImportId] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
CREATE NONCLUSTERED INDEX [IX_ImportId] ON [dbo].[ConfigurationServiceGroupImport] ([ImportId])

ALTER TABLE [dbo].[ConfigurationServiceGroupImport] ADD CONSTRAINT [PK_ConfigurationServiceGroupImport] PRIMARY KEY NONCLUSTERED  ([Id])

ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD
CONSTRAINT [CK_ConfigurationServiceGroupImport_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD
CONSTRAINT [CK_ConfigurationServiceGroupImport_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD
CONSTRAINT [CK_ConfigurationServiceGroupImport_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD
CONSTRAINT [CK_ConfigurationServiceGroupImport_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
ALTER TABLE [dbo].[ConfigurationServiceGroupImport] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupImport_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
