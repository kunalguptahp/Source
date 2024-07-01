CREATE TABLE [dbo].[ConfigurationServiceGroupSelector]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ConfigurationServiceGroupSelector_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ConfigurationServiceGroupSelector_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_ConfigurationServiceGroupSelector_RowStatusId] DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceGroupId] [int] NOT NULL
)
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupSelector_ConfigurationServiceGroup] FOREIGN KEY ([ConfigurationServiceGroupId]) REFERENCES [dbo].[ConfigurationServiceGroup] ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupSelector_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupSelector_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupSelector_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupSelector_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] ADD CONSTRAINT [PK_ConfigurationServiceGroupSelector] PRIMARY KEY NONCLUSTERED  ([Id])
GO

ALTER TABLE [dbo].[ConfigurationServiceGroupSelector] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceGroupSelector_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
