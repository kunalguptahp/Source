CREATE TABLE [dbo].[JumpstationGroupStatus]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_JumpstationGroupStatus_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_JumpstationGroupStatus_ModifiedOn] DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL CONSTRAINT [DF_JumpstationGroupStatus_RowStatusId] DEFAULT ((1)),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupStatus_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupStatus_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupStatus_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationGroupStatus_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] ADD CONSTRAINT [PK_JumpstationGroupStatus] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[JumpstationGroupStatus] WITH NOCHECK ADD CONSTRAINT [FK_JumpstationGroupStatus_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO