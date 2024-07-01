CREATE TABLE [dbo].[ProxyURLType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[ProxyURLDomainId] [int] NOT NULL,
[ProxyURLGroupTypeId] [int] NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ElementsKey] [int] NOT NULL
)
CREATE UNIQUE NONCLUSTERED INDEX [UK_ProxyURLType_ElementsKey] ON [dbo].[ProxyURLType] ([ElementsKey])

ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURLType_ProxyURLDomain] FOREIGN KEY ([ProxyURLDomainId]) REFERENCES [dbo].[ProxyURLDomain] ([Id])
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURLType_ProxyURLGroupType] FOREIGN KEY ([ProxyURLGroupTypeId]) REFERENCES [dbo].[ProxyURLGroupType] ([Id])

ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURLType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])


ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [CK_ProxyURLType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [CK_ProxyURLType_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [CK_ProxyURLType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [CK_ProxyURLType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))

GO

ALTER TABLE [dbo].[ProxyURLType] ADD CONSTRAINT [PK_ProxyURLType] PRIMARY KEY NONCLUSTERED  ([Id])
GO

EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'ProxyURLType', 'CONSTRAINT', N'CK_ProxyURLType_Name'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is at least 3 characters.', 'SCHEMA', N'dbo', 'TABLE', N'ProxyURLType', 'CONSTRAINT', N'CK_ProxyURLType_Name_MinLen'
GO
