CREATE TABLE [dbo].[Note]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Note_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Note_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_Note_RowStatusId] DEFAULT ((1)),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EntityTypeId] [int] NOT NULL,
[EntityId] [int] NOT NULL,
[NoteTypeId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [CK_Note_Comment_IsTrimmed] CHECK ((len(ltrim(rtrim([Comment])))=len([Comment])))
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [CK_Note_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [CK_Note_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [CK_Note_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[Note] ADD CONSTRAINT [PK_Note] PRIMARY KEY NONCLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_CreatedBy] ON [dbo].[Note] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_CreatedOn] ON [dbo].[Note] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_EntityId] ON [dbo].[Note] ([EntityId]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_EntityTypeId] ON [dbo].[Note] ([EntityTypeId]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_ModifiedBy] ON [dbo].[Note] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_ModifiedOn] ON [dbo].[Note] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_Name] ON [dbo].[Note] ([Name]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_NoteTypeId] ON [dbo].[Note] ([NoteTypeId]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Note_RowStatusId] ON [dbo].[Note] ([RowStatusId]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [FK_Note_EntityType] FOREIGN KEY ([EntityTypeId]) REFERENCES [dbo].[EntityType] ([Id])
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [FK_Note_NoteType] FOREIGN KEY ([NoteTypeId]) REFERENCES [dbo].[NoteType] ([Id])
GO
ALTER TABLE [dbo].[Note] WITH NOCHECK ADD CONSTRAINT [FK_Note_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
