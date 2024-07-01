CREATE TABLE [dbo].[JumpstationGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[JumpstationApplicationId] [int] NOT NULL DEFAULT ((1)),
[ValidationJumpstationDomainId] [int] NOT NULL DEFAULT ((1)),
[PublicationJumpstationDomainId] [int] NOT NULL DEFAULT ((2))
)

ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupType_PublicationJumpstationDomain] FOREIGN KEY ([PublicationJumpstationDomainId]) REFERENCES [dbo].[JumpstationDomain] ([Id])

ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupType_ValidationJumpstationDomain] FOREIGN KEY ([ValidationJumpstationDomainId]) REFERENCES [dbo].[JumpstationDomain] ([Id])


ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupType_JumpstationApplication] FOREIGN KEY ([JumpstationApplicationId]) REFERENCES [dbo].[JumpstationApplication] ([Id])

GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupType_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupType_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[JumpstationGroupType] ADD CONSTRAINT [PK_JumpstationGroupType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [FK_JumpstationGroupType_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
