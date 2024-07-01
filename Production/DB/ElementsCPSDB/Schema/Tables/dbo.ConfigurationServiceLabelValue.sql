CREATE TABLE [dbo].[ConfigurationServiceLabelValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[ConfigurationServiceLabelId] [int] NOT NULL,
[ConfigurationServiceGroupId] [int] NOT NULL,
[Value] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
ALTER TABLE [dbo].[ConfigurationServiceLabelValue] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceLabelValue_ConfigurationServiceGroup] FOREIGN KEY ([ConfigurationServiceGroupId]) REFERENCES [dbo].[ConfigurationServiceGroup] ([Id])

ALTER TABLE [dbo].[ConfigurationServiceLabelValue] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceLabelValue_ConfigurationServiceLabel] FOREIGN KEY ([ConfigurationServiceLabelId]) REFERENCES [dbo].[ConfigurationServiceLabel] ([Id])


GO

ALTER TABLE [dbo].[ConfigurationServiceLabelValue] ADD CONSTRAINT [PK_ConfigurationServiceLabelValue] PRIMARY KEY NONCLUSTERED  ([Id])
GO

ALTER TABLE [dbo].[ConfigurationServiceLabelValue] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceLabelValue_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
