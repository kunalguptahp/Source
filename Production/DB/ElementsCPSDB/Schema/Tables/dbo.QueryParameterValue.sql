CREATE TABLE [dbo].[QueryParameterValue]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
[ModifiedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModifiedOn] [datetime] NOT NULL DEFAULT (getdate()),
[RowStatusId] [int] NOT NULL DEFAULT ((1)),
[QueryParameterId] [int] NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
CREATE NONCLUSTERED INDEX [IX_QueryParameterId] ON [dbo].[QueryParameterValue] ([QueryParameterId])

ALTER TABLE [dbo].[QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [CK_QueryParameterValue_Name] CHECK ((len(rtrim([Name]))>(0)))
ALTER TABLE [dbo].[QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [CK_QueryParameterValue_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))

ALTER TABLE [dbo].[QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameterValue_QueryParameter] FOREIGN KEY ([QueryParameterId]) REFERENCES [dbo].[QueryParameter] ([Id])
ALTER TABLE [dbo].[QueryParameterValue] WITH NOCHECK ADD
CONSTRAINT [FK_QueryParameterValue_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO

ALTER TABLE [dbo].[QueryParameterValue] ADD CONSTRAINT [PK_QueryParameterGroup] PRIMARY KEY NONCLUSTERED  ([Id])
GO

EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is valid (e.g. not empty).', 'SCHEMA', N'dbo', 'TABLE', N'QueryParameterValue', 'CONSTRAINT', N'CK_QueryParameterGroup_Name'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Verifies that the field is at least 5 characters.', 'SCHEMA', N'dbo', 'TABLE', N'QueryParameterValue', 'CONSTRAINT', N'CK_QueryParameterGroup_Name_MinLen'
GO
