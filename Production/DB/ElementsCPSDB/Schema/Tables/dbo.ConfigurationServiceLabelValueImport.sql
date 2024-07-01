CREATE TABLE [dbo].[ConfigurationServiceLabelValueImport]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[ItemName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LabelName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LabelValue] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ConfigurationServiceGroupImportId] [int] NOT NULL
)
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelValueImport] ADD PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelValueImport] WITH NOCHECK ADD FOREIGN KEY ([ConfigurationServiceGroupImportId]) REFERENCES [dbo].[ConfigurationServiceGroupImport] ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceLabelValueImport] WITH NOCHECK ADD FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
