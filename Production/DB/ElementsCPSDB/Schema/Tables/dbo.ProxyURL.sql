CREATE TABLE [dbo].[ProxyURL]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURL_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURL_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_ProxyURL_RowStatusId] DEFAULT ((1)),
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProxyURLStatusId] [int] NOT NULL CONSTRAINT [DF_ProxyURL_ProxyURLStatusId] DEFAULT ((0)),
[ProxyURLTypeId] [int] NOT NULL,
[OwnerId] [int] NOT NULL,
[URL] [varchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL
)
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ProductionId] ON [dbo].[ProxyURL] ([ProductionId])





CREATE NONCLUSTERED INDEX [IX_ProxyURL_ValidationId] ON [dbo].[ProxyURL] ([ValidationId])



CREATE NONCLUSTERED INDEX [IX_ProxyURL_OwnerId] ON [dbo].[ProxyURL] ([OwnerId])

CREATE NONCLUSTERED INDEX [IX_ProxyURL_ProxyURLStatusId] ON [dbo].[ProxyURL] ([ProxyURLStatusId])

CREATE NONCLUSTERED INDEX [IX_ProxyURL_ProxyURLTypeId] ON [dbo].[ProxyURL] ([ProxyURLTypeId])

CREATE NONCLUSTERED INDEX [IX_ProxyURL_URL] ON [dbo].[ProxyURL] ([URL])







ALTER TABLE [dbo].[ProxyURL] WITH NOCHECK ADD
CONSTRAINT [CK_ProxyURL_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))







ALTER TABLE [dbo].[ProxyURL] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])

ALTER TABLE [dbo].[ProxyURL] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_ProxyURLStatus] FOREIGN KEY ([ProxyURLStatusId]) REFERENCES [dbo].[ProxyURLStatus] ([Id])
ALTER TABLE [dbo].[ProxyURL] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_ProxyURLType] FOREIGN KEY ([ProxyURLTypeId]) REFERENCES [dbo].[ProxyURLType] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[ProxyURL] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO

ALTER TABLE [dbo].[ProxyURL] ADD CONSTRAINT [PK_ProxyURL] PRIMARY KEY NONCLUSTERED  ([Id])
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_CreatedBy] ON [dbo].[ProxyURL] ([CreatedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_CreatedOn] ON [dbo].[ProxyURL] ([CreatedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ModifiedBy] ON [dbo].[ProxyURL] ([ModifiedBy]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
CREATE NONCLUSTERED INDEX [IX_ProxyURL_ModifiedOn] ON [dbo].[ProxyURL] ([ModifiedOn]) WITH (ALLOW_PAGE_LOCKS=OFF)
GO
