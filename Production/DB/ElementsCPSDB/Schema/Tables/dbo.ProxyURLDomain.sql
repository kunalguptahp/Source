CREATE TABLE [dbo].[ProxyURLDomain]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURLDomain_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURLDomain_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_ProxyURLDomain_RowStatusId] DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ValidationDomain] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProductionDomain] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLDomain_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLDomain_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLDomain_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [CK_ProxyURLDomain_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[ProxyURLDomain] ADD CONSTRAINT [PK_ProxyURLDomain] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[ProxyURLDomain] WITH NOCHECK ADD CONSTRAINT [FK_ProxyURLDomain_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
