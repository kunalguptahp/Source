CREATE TABLE [dbo].[PublishTemp]
(
[Id] [int] NOT NULL IDENTITY(1000, 1),
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_PublishTemp_CreatedOn] DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_PublishTemp_ModifiedOn] DEFAULT (getdate())
)
GO
ALTER TABLE [dbo].[PublishTemp] ADD CONSTRAINT [PK_PublishTemp] PRIMARY KEY NONCLUSTERED  ([Id])
GO
