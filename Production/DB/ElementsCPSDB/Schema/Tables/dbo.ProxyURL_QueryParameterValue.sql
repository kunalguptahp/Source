CREATE TABLE [dbo].[ProxyURL_QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURL_QueryParameterValue_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ProxyURL_QueryParameterValue_ModifiedOn] DEFAULT (getdate()),
[ProxyURLId] [int] NOT NULL,
[QueryParameterValueId] [int] NOT NULL
)
CREATE NONCLUSTERED INDEX [IX_ProxyURLId] ON [dbo].[ProxyURL_QueryParameterValue] ([ProxyURLId])

CREATE NONCLUSTERED INDEX [IX_QueryParameterValueId] ON [dbo].[ProxyURL_QueryParameterValue] ([QueryParameterValueId])

CREATE UNIQUE NONCLUSTERED INDEX [IX_ProxyURL_QueryParameterValue] ON [dbo].[ProxyURL_QueryParameterValue] ([ProxyURLId], [QueryParameterValueId])

ALTER TABLE [dbo].[ProxyURL_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_QueryParameterValue_ProxyURL] FOREIGN KEY ([ProxyURLId]) REFERENCES [dbo].[ProxyURL] ([Id])
ALTER TABLE [dbo].[ProxyURL_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_ProxyURL_QueryParameterValue_QueryParameterValue] FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id])
ALTER TABLE [dbo].[ProxyURL_QueryParameterValue] ADD 
CONSTRAINT [PK_ProxyURL_QueryParameterValue] PRIMARY KEY CLUSTERED  ([Id])


GO
