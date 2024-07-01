CREATE TABLE [dbo].[QueryParameter_ProxyURLType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ProxyURLTypeId] [int] NOT NULL,
[QueryParameterId] [int] NOT NULL,
[Description] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
CREATE UNIQUE NONCLUSTERED INDEX [UK_QueryParameter_ProxyURLType_QueryParameterId_ProxyURLTypeId] ON [dbo].[QueryParameter_ProxyURLType] ([ProxyURLTypeId], [QueryParameterId])

ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [CK_QueryParameter_ProxyURLType_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [CK_QueryParameter_ProxyURLType_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD
CONSTRAINT [CK_QueryParameter_ProxyURLType_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] ADD CONSTRAINT [PK_QueryParameter_ProxyURLTypeType] PRIMARY KEY NONCLUSTERED  ([Id])





GO

ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD CONSTRAINT [FK_QueryParameter_ProxyURLType_ProxyURLType] FOREIGN KEY ([ProxyURLTypeId]) REFERENCES [dbo].[ProxyURLType] ([Id])
GO
ALTER TABLE [dbo].[QueryParameter_ProxyURLType] WITH NOCHECK ADD CONSTRAINT [FK_QueryParameter_ProxyURLType_QueryParameter] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
GO
