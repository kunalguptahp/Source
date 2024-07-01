-- =============================================
-- Author:	Justin Webster
-- Create date:	2010/06/28
-- Description:	Loads (i.e. inserts) the minimum set of data that is required to initialize the DEMO environment.
-- =============================================

SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO

BEGIN TRY
	BEGIN TRANSACTION

	PRINT 'Loading "DEMO" DB data'

	DECLARE @strNewLine VARCHAR(2);
	DECLARE @dtDefaultValue_CreatedOn DATETIME;
	DECLARE @dtDefaultValue_ModifiedOn DATETIME;
	DECLARE @strDefaultValue_CreatedBy NVARCHAR(50);
	DECLARE @strDefaultValue_ModifiedBy NVARCHAR(50);
	DECLARE @intRowStatus_Active int;
	DECLARE @intRowStatus_Inactive int;
	DECLARE @intRowStatus_Deleted int;
	DECLARE @intRole_Administrator int;
	DECLARE @intRole_AccountLocked int;
	DECLARE @intRole_RestrictedAccess int;
	DECLARE @intRole_Everyone int;
	DECLARE @intRole_DataAdmin int;
	DECLARE @intRole_UserAdmin int;
	DECLARE @intRole_TechSupport int;
	DECLARE @intRole_Viewer int;
	DECLARE @intRole_Editor int;
	DECLARE @intRole_Validator int;
	DECLARE @intRole_Coordinator int;
	DECLARE @intPlatform_ITG int;
	DECLARE @intPlatform_Production int;
	DECLARE @intPlatform_Development int;
	
	SET @strNewLine = Char(13) + Char(10);
	SET @dtDefaultValue_CreatedOn = CONVERT(DATETIME, '20090101', 112);
	SET @dtDefaultValue_ModifiedOn = @dtDefaultValue_CreatedOn;
	SET @strDefaultValue_CreatedBy = N'system\system';
	SET @strDefaultValue_ModifiedBy = @strDefaultValue_CreatedBy;
	SET @intRowStatus_Active = 1;
	SET @intRowStatus_Inactive = 2;
	SET @intRowStatus_Deleted = 3;
	SET @intRole_Administrator = 1;
	SET @intRole_DataAdmin = 2;
	SET @intRole_UserAdmin = 3;
	SET @intRole_Viewer = 4;
	SET @intRole_Editor = 5;
	SET @intRole_Validator = 6;
	SET @intRole_Coordinator = 7;
	SET @intRole_AccountLocked = 8;
	SET @intRole_RestrictedAccess = 9;
	SET @intRole_Everyone = 10;
	SET @intRole_TechSupport = 11;

	--PRINT 'Adding rows to [dbo].[Person]'
	--BEGIN
	--	SET IDENTITY_INSERT [dbo].[Person] ON
	--	INSERT INTO [dbo].[Person] ([Id], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [RowStatusId], [WindowsId], [FirstName], [MiddleName], [LastName], [Email]) VALUES (8, '2008-09-05 20:29:24.910', N'americas\scott_trimber', '2008-09-05 20:30:37.080', N'americas\scott_trimber', 2, N'AMERICAS\martin', N'Samuel', N'', N'Martin', N'samuel.martin@hp.com')
	--	SET IDENTITY_INSERT [dbo].[Person] OFF
	--END

--	PRINT 'Adding rows to [dbo].[]'
--	BEGIN
--	END

	PRINT 'Finished loading "DEMO" DB data'

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	---Capture the details of the original error
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY()

	PRINT 'Error occurred while loading "DEMO" DB data'
	ROLLBACK TRANSACTION

	---Raise an error with the details of the original error
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO

