CREATE TABLE [dbo].[ConfigurationServiceGroupStatus]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ConfigurationServiceGroupStatus_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ConfigurationServiceGroupStatus_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_ConfigurationServiceGroupStatus_RowStatusId] DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroupStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] ADD CONSTRAINT [PK_ConfigurationServiceGroupStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupStatus] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceGroupStatus_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
