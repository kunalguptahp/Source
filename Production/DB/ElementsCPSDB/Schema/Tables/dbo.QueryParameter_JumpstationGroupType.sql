CREATE TABLE [dbo].[QueryParameter_JumpstationGroupType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[JumpstationGroupTypeId] [int] NOT NULL,
[QueryParameterId] [int] NOT NULL,
[MaximumSelection] [int] NOT NULL DEFAULT ((0)),
[Wildcard] [bit] NOT NULL DEFAULT ((0)),
[SortOrder] [int] NULL
)
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] ADD CONSTRAINT [PK_QueryParameter_JumpstationGroupTypeType] PRIMARY KEY NONCLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [FK_QueryParameter_JumpstationGroupType_JumpstationdGroupType] FOREIGN KEY ([JumpstationGroupTypeId]) REFERENCES [dbo].[JumpstationGroupType] ([Id])
GO
ALTER TABLE [dbo].[QueryParameter_JumpstationGroupType] WITH NOCHECK ADD CONSTRAINT [FK_QueryParameter_QueryParameter_JumpstationGroupType] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
GO
