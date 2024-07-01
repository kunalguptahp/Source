CREATE TABLE [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ConfigurationServiceItem_ConfigurationServiceGroupTypeGroup_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ConfigurationServiceItem_ConfigurationServiceGroupTypeGroup_ModifiedOn] DEFAULT (getdate()),
[ConfigurationServiceGroupTypeId] [int] NOT NULL,
[ConfigurationServiceItemId] [int] NOT NULL
)
GO
ALTER TABLE [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType] ADD CONSTRAINT [PK_ConfigurationServiceItem_ConfigurationServiceGroupTypeType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceItem_ConfigurationServiceGroupType_ConfigurationServicedGroupType] FOREIGN KEY ([ConfigurationServiceGroupTypeId]) REFERENCES [dbo].[ConfigurationServiceGroupType] ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceItem_ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceItem_ConfigurationServiceItem_ConfigurationServiceGroupType] FOREIGN KEY ([ConfigurationServiceItemId]) REFERENCES [dbo].[ConfigurationServiceItem] ([Id])
GO
