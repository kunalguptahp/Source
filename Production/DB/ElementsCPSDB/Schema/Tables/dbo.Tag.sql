CREATE TABLE [dbo].[Tag]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Tag_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Tag_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_Tag_RowStatusId] DEFAULT ((1)),
[Notes] [nvarchar] (2048) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [CK_Tag_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [CK_Tag_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [CK_Tag_Name_MinLen] CHECK ((len(rtrim([Name]))>=(5)))
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [CK_Tag_Notes_IsTrimmed] CHECK ((len(ltrim(rtrim([Notes])))=len([Notes])))
GO
ALTER TABLE [dbo].[Tag] ADD CONSTRAINT [PK_Tag] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Tag] WITH NOCHECK ADD CONSTRAINT [FK_Tag_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'Tag', 'CONSTRAINT', N'CK_Tag_Name'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is at least 5 characters.', 'SCHEMA', N'dbo', 'TABLE', N'Tag', 'CONSTRAINT', N'CK_Tag_Name_MinLen'
GO
