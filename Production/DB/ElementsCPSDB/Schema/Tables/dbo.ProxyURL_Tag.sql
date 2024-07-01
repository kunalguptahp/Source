CREATE TABLE [dbo].[ProxyURL_Tag]
(
[ProxyURLId] [int] NOT NULL,
[TagId] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURL_Tag_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURL_Tag_ModifiedOn] DEFAULT (getdate())
)
ALTER TABLE [dbo].[ProxyURL_Tag] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_Tag_ProxyURL] FOREIGN KEY ([ProxyURLId]) REFERENCES [dbo].[ProxyURL] ([Id])
GO
ALTER TABLE [dbo].[ProxyURL_Tag] ADD CONSTRAINT [PK_ProxyURL_Tag_1] PRIMARY KEY NONCLUSTERED  ([ProxyURLId], [TagId])
GO

ALTER TABLE [dbo].[ProxyURL_Tag] WITH NOCHECK ADD CONSTRAINT [FK_ProxyURL_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
