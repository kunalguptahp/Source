CREATE TABLE [dbo].[Aardvark]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Aardvark_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Aardvark_ModifiedOn] DEFAULT (getdate()),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Aardvark] WITH NOCHECK ADD CONSTRAINT [CK_Aardvark_Comment_IsTrimmed] CHECK ((len(ltrim(rtrim([Comment])))=len([Comment])))
GO
ALTER TABLE [dbo].[Aardvark] WITH NOCHECK ADD CONSTRAINT [CK_Aardvark_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[Aardvark] WITH NOCHECK ADD CONSTRAINT [CK_Aardvark_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[Aardvark] WITH NOCHECK ADD CONSTRAINT [CK_Aardvark_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[Aardvark] ADD CONSTRAINT [PK_Aardvark] PRIMARY KEY NONCLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Aardvark_CreatedBy] ON [dbo].[Aardvark] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Aardvark_CreatedOn] ON [dbo].[Aardvark] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Aardvark_ModifiedBy] ON [dbo].[Aardvark] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Aardvark_ModifiedOn] ON [dbo].[Aardvark] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Aardvark_Name] ON [dbo].[Aardvark] ([Name]) WITH (ALLOW_PAGE_LOCKS=OFF) ON [PRIMARY]
GO
