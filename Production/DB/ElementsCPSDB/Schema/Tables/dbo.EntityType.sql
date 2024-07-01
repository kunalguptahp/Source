CREATE TABLE [dbo].[EntityType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_EntityType_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_EntityType_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_EntityType_RowStatusId] DEFAULT ((1)),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [CK_EntityType_Comment_IsTrimmed] CHECK ((len(ltrim(rtrim([Comment])))=len([Comment])))
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [CK_EntityType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [CK_EntityType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [CK_EntityType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[EntityType] ADD CONSTRAINT [PK_EntityType] PRIMARY KEY NONCLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_CreatedBy] ON [dbo].[EntityType] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_CreatedOn] ON [dbo].[EntityType] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_ModifiedBy] ON [dbo].[EntityType] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_ModifiedOn] ON [dbo].[EntityType] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EntityType_Name] ON [dbo].[EntityType] ([Name]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_EntityType_RowStatusId] ON [dbo].[EntityType] ([RowStatusId]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EntityType] WITH NOCHECK ADD CONSTRAINT [FK_EntityType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
