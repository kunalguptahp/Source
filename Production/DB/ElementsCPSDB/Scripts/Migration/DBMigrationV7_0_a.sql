CREATE TABLE [dbo].[JumpstationGroupPivot](
	[JumpstationGroupId] [int] NOT NULL,
	[Brand] [nvarchar](256) NULL,
	[Cycle] [nvarchar](256) NULL,
	[Locale] [nvarchar](256) NULL,
	[Touchpoint] [nvarchar](256) NULL,
	[PartnerCategory] [nvarchar](256) NULL,
	[Platform] [nvarchar](256) NULL,
	CONSTRAINT [PK_JumpstationGroupPivot] PRIMARY KEY CLUSTERED 
	(
		[JumpstationGroupId] ASC
	)
)
GO

