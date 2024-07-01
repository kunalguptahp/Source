CREATE TABLE [dbo].[ConfigurationServiceGroup]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConfigurationServiceGroupStatusId] [int] NOT NULL,
[ConfigurationServiceGroupTypeId] [int] NOT NULL,
[ConfigurationServiceApplicationId] [int] NOT NULL,
[OwnerId] [int] NOT NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL
)
CREATE NONCLUSTERED INDEX [IX_ProductionId] ON [dbo].[ConfigurationServiceGroup] ([ProductionId])

ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceGroupStatus] FOREIGN KEY ([ConfigurationServiceGroupStatusId]) REFERENCES [dbo].[ConfigurationServiceGroupStatus] ([Id])
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceApplication] FOREIGN KEY ([ConfigurationServiceApplicationId]) REFERENCES [dbo].[ConfigurationServiceApplication] ([Id])
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroup_ConfigurationServiceGroupType] FOREIGN KEY ([ConfigurationServiceGroupTypeId]) REFERENCES [dbo].[ConfigurationServiceGroupType] ([Id])

ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroup_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceGroup_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])











GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroup_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroup_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroup_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceGroup_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[ConfigurationServiceGroup] ADD CONSTRAINT [PK_ConfigurationServiceGroup] PRIMARY KEY NONCLUSTERED  ([Id])
GO
