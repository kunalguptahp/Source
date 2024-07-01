CREATE TABLE [dbo].[ProxyURLStatus]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURLStatus_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURLStatus_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_ProxyURLStatus_RowStatusId] DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
ALTER TABLE [dbo].[ProxyURLStatus] WITH NOCHECK ADD
CONSTRAINT [CK_ProxyURLStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
ALTER TABLE [dbo].[ProxyURLStatus] WITH NOCHECK ADD
CONSTRAINT [CK_ProxyURLStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[ProxyURLStatus] WITH NOCHECK ADD
CONSTRAINT [CK_ProxyURLStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO

ALTER TABLE [dbo].[ProxyURLStatus] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO

ALTER TABLE [dbo].[ProxyURLStatus] ADD CONSTRAINT [PK_ProxyURLStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProxyURLStatus_Name] ON [dbo].[ProxyURLStatus] ([Name])
GO

EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'ProxyURLStatus', 'CONSTRAINT', N'CK_ProxyURLStatus_Name'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is at least 3 characters.', 'SCHEMA', N'dbo', 'TABLE', N'ProxyURLStatus', 'CONSTRAINT', N'CK_ProxyURLStatus_Name_MinLen'
GO
