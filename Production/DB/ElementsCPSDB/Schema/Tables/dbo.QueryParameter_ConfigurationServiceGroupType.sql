CREATE TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ConfigurationServiceGroupTypeId] [int] NOT NULL,
[QueryParameterId] [int] NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MaximumSelection] [int] NOT NULL DEFAULT ((0)),
[Wildcard] [bit] NOT NULL DEFAULT ((0))
)
CREATE UNIQUE NONCLUSTERED INDEX [QueryParameter_ConfigurationServiceGroupType_QueryParameterId_ConfigurationServiceGroupTypeId] ON [dbo].[QueryParameter_ConfigurationServiceGroupType] ([ConfigurationServiceGroupTypeId], [QueryParameterId])

ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD
CONSTRAINT [CK_QueryParameter_ConfigurationServiceGroupType_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD
CONSTRAINT [CK_QueryParameter_ConfigurationServiceGroupType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD
CONSTRAINT [CK_QueryParameter_ConfigurationServiceGroupType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] ADD CONSTRAINT [PK_QueryParameter_ConfigurationServiceGroupTypeType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [FK_QueryParameter_ConfigurationServiceGroupType_ConfigurationServicedGroupType] FOREIGN KEY ([ConfigurationServiceGroupTypeId]) REFERENCES [dbo].[ConfigurationServiceGroupType] ([Id])
GO
ALTER TABLE [dbo].[QueryParameter_ConfigurationServiceGroupType] WITH NOCHECK ADD CONSTRAINT [FK_QueryParameter_QueryParameter_ConfigurationServiceGroupType] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
GO
