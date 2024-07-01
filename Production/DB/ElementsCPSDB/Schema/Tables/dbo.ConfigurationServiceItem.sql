CREATE TABLE [dbo].[ConfigurationServiceItem]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Parent] [bit] NOT NULL,
[SortOrder] [int] NULL
)
ALTER TABLE [dbo].[ConfigurationServiceItem] WITH NOCHECK ADD
CONSTRAINT [CK_ConfigurationServiceItem_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))


GO
ALTER TABLE [dbo].[ConfigurationServiceItem] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceItem_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[ConfigurationServiceItem] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceItem_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO

ALTER TABLE [dbo].[ConfigurationServiceItem] ADD CONSTRAINT [PK_ConfigurationServiceItem] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceItem] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceItem_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
