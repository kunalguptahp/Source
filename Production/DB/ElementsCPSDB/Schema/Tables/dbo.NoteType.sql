CREATE TABLE [dbo].[NoteType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_NoteType_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_NoteType_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_NoteType_RowStatusId] DEFAULT ((1)),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [CK_NoteType_Comment_IsTrimmed] CHECK ((len(ltrim(rtrim([Comment])))=len([Comment])))
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [CK_NoteType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [CK_NoteType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [CK_NoteType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[NoteType] ADD CONSTRAINT [PK_NoteType] PRIMARY KEY NONCLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_CreatedBy] ON [dbo].[NoteType] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_CreatedOn] ON [dbo].[NoteType] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_ModifiedBy] ON [dbo].[NoteType] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_ModifiedOn] ON [dbo].[NoteType] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_NoteType_Name] ON [dbo].[NoteType] ([Name]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_NoteType_RowStatusId] ON [dbo].[NoteType] ([RowStatusId]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NoteType] WITH NOCHECK ADD CONSTRAINT [FK_NoteType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
