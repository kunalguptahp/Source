CREATE TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ConfigurationServiceGroupSelectorId] [int] NOT NULL,
[QueryParameterValueId] [int] NOT NULL,
[Negation] [bit] NOT NULL DEFAULT ((0))
)
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupSelector_QueryParameterValue_ConfigurationServiceGroupSelector] FOREIGN KEY ([ConfigurationServiceGroupSelectorId]) REFERENCES [dbo].[ConfigurationServiceGroupSelector] ([Id])
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroupSelector_QueryParameterValue_QueryParameterValue] FOREIGN KEY ([QueryParameterValueId]) REFERENCES [dbo].[QueryParameterValue] ([Id])
GO
ALTER TABLE [dbo].[ConfigurationServiceGroupSelector_QueryParameterValue] ADD CONSTRAINT [PK_ConfigurationServiceGroupSelector_QueryParameterValue] PRIMARY KEY CLUSTERED  ([Id])
GO
