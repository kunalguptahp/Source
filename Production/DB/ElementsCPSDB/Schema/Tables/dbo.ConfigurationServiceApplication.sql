CREATE TABLE [dbo].[ConfigurationServiceApplication]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Version] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConfigurationServiceApplicationTypeId] [int] NOT NULL
)
CREATE UNIQUE NONCLUSTERED INDEX [UK_ConfigurationServiceApplication_ElementsKey] ON [dbo].[ConfigurationServiceApplication] ([ElementsKey])

ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceApplication_ConfigurationServiceApplicationType] FOREIGN KEY ([ConfigurationServiceApplicationTypeId]) REFERENCES [dbo].[ConfigurationServiceApplicationType] ([Id])
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceApplication_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])


GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceApplication_Version_IsTrimmed] CHECK ((len(ltrim(rtrim([Version])))=len([Version])))
GO
ALTER TABLE [dbo].[ConfigurationServiceApplication] ADD CONSTRAINT [PK_ConfigurationServiceApplication] PRIMARY KEY NONCLUSTERED  ([Id])
GO
