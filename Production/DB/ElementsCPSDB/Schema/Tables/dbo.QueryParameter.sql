CREATE TABLE [dbo].[QueryParameter]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


GO
ALTER TABLE [dbo].[QueryParameter] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[QueryParameter] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[QueryParameter] WITH NOCHECK ADD CONSTRAINT [CK_QueryParameter_Name_MinLen] CHECK ((len(rtrim([Name]))>=(5)))
GO
ALTER TABLE [dbo].[QueryParameter] ADD CONSTRAINT [PK_QueryParameter] PRIMARY KEY NONCLUSTERED  ([Id])
GO

ALTER TABLE [dbo].[QueryParameter] WITH NOCHECK ADD CONSTRAINT [FK_QueryParameter_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'QueryParameter', 'CONSTRAINT', N'CK_QueryParameter_Name'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is at least 5 characters.', 'SCHEMA', N'dbo', 'TABLE', N'QueryParameter', 'CONSTRAINT', N'CK_QueryParameter_Name_MinLen'
GO
