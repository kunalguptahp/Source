CREATE TABLE [dbo].[JumpstationGroup_Tag]
(
[JumpstationGroupId] [int] NOT NULL,
[TagId] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_JumpstationGroup_Tag_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_JumpstationGroup_Tag_ModifiedOn] DEFAULT (getdate())
)
GO
ALTER TABLE [dbo].[JumpstationGroup_Tag] ADD CONSTRAINT [PK_JumpstationGroup_Tag] PRIMARY KEY NONCLUSTERED  ([JumpstationGroupId], [TagId])
GO
ALTER TABLE [dbo].[JumpstationGroup_Tag] WITH NOCHECK ADD CONSTRAINT [FK_JumpstationGroup_Tag_JumpstationGroup] FOREIGN KEY ([JumpstationGroupId]) REFERENCES [dbo].[JumpstationGroup] ([Id])
GO
ALTER TABLE [dbo].[JumpstationGroup_Tag] WITH NOCHECK ADD CONSTRAINT [FK_JumpstationGroup_Tag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
GO
