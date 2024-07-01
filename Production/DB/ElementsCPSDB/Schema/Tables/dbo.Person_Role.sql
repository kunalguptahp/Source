CREATE TABLE [dbo].[Person_Role]
(
[PersonId] [int] NOT NULL,
[RoleId] [int] NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Person_Role_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Person_Role_ModifiedOn] DEFAULT (getdate())
)
ALTER TABLE [dbo].[Person_Role] WITH NOCHECK ADD
CONSTRAINT [FK_Person_Role_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Person_Role] WITH NOCHECK ADD
CONSTRAINT [FK_Person_Role_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Person_Role] ADD CONSTRAINT [PK_Person_Role_1] PRIMARY KEY NONCLUSTERED  ([PersonId], [RoleId])
GO
