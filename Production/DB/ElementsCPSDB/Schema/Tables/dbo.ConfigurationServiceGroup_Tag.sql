CREATE TABLE [dbo].[ConfigurationServiceGroup_Tag]
(
[ConfigurationServiceGroupId] [int] NOT NULL,
[TagId] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ConfigurationServiceGroup_Tag_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ConfigurationServiceGroup_Tag_ModifiedOn] DEFAULT (getdate())
)
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_Tag] ADD CONSTRAINT [PK_ConfigurationServiceGroup_Tag] PRIMARY KEY NONCLUSTERED  ([ConfigurationServiceGroupId], [TagId])
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_Tag] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceGroup_Tag_ConfigurationServiceGroup] FOREIGN KEY ([ConfigurationServiceGroupId]) REFERENCES [dbo].[ConfigurationServiceGroup] ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup_Tag] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceGroup_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
