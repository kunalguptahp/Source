CREATE TABLE [dbo].[JumpstationGroup]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TargetURL] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Order] [int] NULL,
[JumpstationGroupStatusId] [int] NOT NULL,
[JumpstationGroupTypeId] [int] NOT NULL,
[JumpstationApplicationId] [int] NOT NULL,
[OwnerId] [int] NOT NULL,
[ProductionId] [int] NULL,
[ValidationId] [int] NULL
)
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_CreatedBy] ON [dbo].[JumpstationGroup] ([CreatedBy])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_CreatedOn] ON [dbo].[JumpstationGroup] ([CreatedOn])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_JumpstationApplicationId] ON [dbo].[JumpstationGroup] ([JumpstationApplicationId])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_JumpstationGroupTypeId] ON [dbo].[JumpstationGroup] ([JumpstationGroupTypeId])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ModifiedBy] ON [dbo].[JumpstationGroup] ([ModifiedBy])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ModifiedOn] ON [dbo].[JumpstationGroup] ([ModifiedOn])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_Name] ON [dbo].[JumpstationGroup] ([Name])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_OwnerId] ON [dbo].[JumpstationGroup] ([OwnerId])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ProductionId] ON [dbo].[JumpstationGroup] ([ProductionId])

CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ValidationId] ON [dbo].[JumpstationGroup] ([ValidationId])

ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroup_JumpstationApplication] FOREIGN KEY ([JumpstationApplicationId]) REFERENCES [dbo].[JumpstationApplication] ([Id])

ALTER TABLE [dbo].[JumpstationGroup] ADD CONSTRAINT [PK_JumpstationGroup] PRIMARY KEY NONCLUSTERED  ([Id])

ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [CK_JumpstationGroup_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [CK_JumpstationGroup_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [CK_JumpstationGroup_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [CK_JumpstationGroup_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroup_JumpstationGroupStatus] FOREIGN KEY ([JumpstationGroupStatusId]) REFERENCES [dbo].[JumpstationGroupStatus] ([Id])
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroup_JumpstationGroupType] FOREIGN KEY ([JumpstationGroupTypeId]) REFERENCES [dbo].[JumpstationGroupType] ([Id])
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroup_Person] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Person] ([Id])
ALTER TABLE [dbo].[JumpstationGroup] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroup_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
