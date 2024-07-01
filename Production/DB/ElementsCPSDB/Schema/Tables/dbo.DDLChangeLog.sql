CREATE TABLE [dbo].[DDLChangeLog]
(
[ID] [int] NOT NULL IDENTITY(1, 1),
[Inserted] [datetime] NOT NULL CONSTRAINT [DF_ddl_log_InsertionDate] DEFAULT (getdate()),
[CurrentUser] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_ddl_log_CurrentUser] DEFAULT (CONVERT([nvarchar](50),user_name(),(0))),
[LoginName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_DDLChangeLog_LoginName] DEFAULT (CONVERT([nvarchar](50),suser_sname(),(0))),
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_DDLChangeLog_Username] DEFAULT (CONVERT([nvarchar](50),original_login(),(0))),
[EventType] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[objectName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[objectType] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[tsql] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [dbo].[DDLChangeLog] ADD CONSTRAINT [PK_DDLChangeLog] PRIMARY KEY CLUSTERED  ([ID])
GO
