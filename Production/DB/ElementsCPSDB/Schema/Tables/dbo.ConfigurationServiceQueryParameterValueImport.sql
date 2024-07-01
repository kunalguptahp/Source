CREATE TABLE [dbo].[ConfigurationServiceQueryParameterValueImport]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[QueryParameterName] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[QueryParameterValue] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConfigurationServiceGroupImportId] [int] NOT NULL
)
ALTER TABLE [dbo].[ConfigurationServiceQueryParameterValueImport] ADD CONSTRAINT [PK_ConfigurationServiceQueryParameterValueImport] PRIMARY KEY NONCLUSTERED  ([Id])

ALTER TABLE [dbo].[ConfigurationServiceQueryParameterValueImport] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceQueryParameterValueImport_ConfigurationServiceGroupImport] FOREIGN KEY ([ConfigurationServiceGroupImportId]) REFERENCES [dbo].[ConfigurationServiceGroupImport] ([Id])
ALTER TABLE [dbo].[ConfigurationServiceQueryParameterValueImport] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceQueryParameterValueImport_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
