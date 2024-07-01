CREATE TABLE [dbo].[RowStatus]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_RowStatus_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_RowStatus_ModifiedOn] DEFAULT (getdate()),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
ALTER TABLE [dbo].[RowStatus] WITH NOCHECK ADD
CONSTRAINT [CK_RowStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
ALTER TABLE [dbo].[RowStatus] WITH NOCHECK ADD
CONSTRAINT [CK_RowStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[RowStatus] WITH NOCHECK ADD
CONSTRAINT [CK_RowStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[RowStatus] WITH NOCHECK ADD
CONSTRAINT [CK_RowStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO

ALTER TABLE [dbo].[RowStatus] ADD CONSTRAINT [PK_RowStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'RowStatus', 'CONSTRAINT', N'CK_RowStatus_Name'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is at least 3 characters.', 'SCHEMA', N'dbo', 'TABLE', N'RowStatus', 'CONSTRAINT', N'CK_RowStatus_Name_MinLen'
GO
