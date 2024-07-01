CREATE TABLE [dbo].[Log]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CreatedAt] [datetime] NOT NULL CONSTRAINT [DF_Log_CreatedAt] DEFAULT (getdate()),
[Date] [datetime] NULL,
[UtcDate] [datetime] NULL,
[Severity] [varchar] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserIdentity] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserWebIdentity] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Logger] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Location] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[WebSessionId] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProcessThread] [int] NULL,
[MachineName] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProcessorCount] [smallint] NULL,
[OSVersion] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ClrVersion] [varchar] (32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AllocatedMemory] [int] NULL,
[WorkingMemory] [int] NULL,
[ProcessUser] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProcessUserInteractive] [bit] NULL,
[ProcessUptime] [bigint] NULL,
[Message] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Exception] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StackTrace] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [dbo].[Log] ADD CONSTRAINT [PK_Log] PRIMARY KEY NONCLUSTERED  ([Id])
GO
