CREATE TABLE [dbo].[ConfigurationServiceLabel]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ElementsKey] [nvarchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[ConfigurationServiceItemId] [int] NOT NULL,
[ConfigurationServiceLabelTypeId] [int] NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Help] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ValueList] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InputRequired] [bit] NOT NULL DEFAULT ((1)),
[SortOrder] [int] NULL
)
ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceLabel_ConfigurationServiceLabelType] FOREIGN KEY ([ConfigurationServiceLabelTypeId]) REFERENCES [dbo].[ConfigurationServiceLabelType] ([Id])

ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD
CONSTRAINT [FK_ConfigurationServiceLabel_ConfigurationServiceItem] FOREIGN KEY ([ConfigurationServiceItemId]) REFERENCES [dbo].[ConfigurationServiceItem] ([Id])


GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabel_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabel_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD CONSTRAINT [CK_ConfigurationServiceLabel_Name_MinLen] CHECK ((len(rtrim([Name]))>=(5)))
GO
ALTER TABLE [dbo].[ConfigurationServiceLabel] ADD CONSTRAINT [PK_ConfigurationServiceLabel] PRIMARY KEY NONCLUSTERED  ([Id])
GO

ALTER TABLE [dbo].[ConfigurationServiceLabel] WITH NOCHECK ADD CONSTRAINT [FK_ConfigurationServiceLabel_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
