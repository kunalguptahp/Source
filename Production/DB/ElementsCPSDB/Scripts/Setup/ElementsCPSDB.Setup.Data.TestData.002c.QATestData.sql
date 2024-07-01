-- =============================================
-- Author:	Justin Webster
-- Create date:	2008/08/06
-- Description:	Loads (i.e. inserts) the minimum set of data that is required to initialize the QA environment.
-- =============================================

SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO

BEGIN TRY
	BEGIN TRANSACTION

	PRINT 'Loading "QA" DB data'

	DECLARE @strDefaultValue_CreatedBy NVARCHAR(50);
	DECLARE @strDefaultValue_ModifiedBy NVARCHAR(50);
	DECLARE @intRowStatus_Active int;
	DECLARE @intRowStatus_Inactive int;
	DECLARE @intRowStatus_Deleted int;
	DECLARE @intRole_Administrator int;
	DECLARE @intRole_DataAdmin int;
	DECLARE @intRole_UserAdmin int;
	DECLARE @intRole_OfferViewer int;
	DECLARE @intRole_OfferEditor int;
	DECLARE @intRole_OfferValidator int;
	DECLARE @intRole_OfferCoordinator int;
	DECLARE @intRole_AccountLocked int;
	SET @strDefaultValue_CreatedBy = N'system';
	SET @strDefaultValue_ModifiedBy = @strDefaultValue_CreatedBy;
	SET @intRowStatus_Active = 1;
	SET @intRowStatus_Inactive = 2;
	SET @intRowStatus_Deleted = 3;
	SET @intRole_Administrator = 1;
	SET @intRole_DataAdmin = 2;
	SET @intRole_UserAdmin = 3;
	SET @intRole_OfferViewer = 4;
	SET @intRole_OfferEditor = 5;
	SET @intRole_OfferValidator = 6;
	SET @intRole_OfferCoordinator = 7;
	SET @intRole_AccountLocked = 8;

	PRINT 'Adding rows to [dbo].[Person]'
	BEGIN
		INSERT INTO [dbo].[Person] ([WindowsId], [FirstName], [MiddleName], [LastName], [Email], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [RowStatusId]) VALUES (N'asiapacific\yangqi', N'Qing (Tracy)', null, N'Yang', N'qing.yang@hp.com', getdate(), @strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy, @intRowStatus_Active)
		INSERT INTO [dbo].[Person] ([WindowsId], [FirstName], [MiddleName], [LastName], [Email], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [RowStatusId]) VALUES (N'asiapacific\xujingy', N'Jing-Yuan (Steele)', null, N'Xu', N'jingyuan.xu@hp.com', getdate(), @strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy, @intRowStatus_Active)
		INSERT INTO [dbo].[Person] ([WindowsId], [FirstName], [MiddleName], [LastName], [Email], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [RowStatusId]) VALUES (N'asiapacific\fuxi', N'Xiao-Yi (Jesse)', null, N'Fu', N'xiao-yi.fu@hp.com', getdate(), @strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy, @intRowStatus_Active)
	END

	PRINT 'Adding rows to [dbo].[Person_Role]'
	BEGIN
		DECLARE @personId INT;
		SELECT @personId = Id FROM [dbo].[Person] WHERE WindowsId = 'asiapacific\yangqi'
		INSERT INTO [dbo].[Person_Role] ([PersonId], [RoleId], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (@personId, @intRole_Administrator, @strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy, getdate())
		SELECT @personId = Id FROM [dbo].[Person] WHERE WindowsId = 'asiapacific\xujingy'
		INSERT INTO [dbo].[Person_Role] ([PersonId], [RoleId], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (@personId, @intRole_Administrator, @strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy, getdate())
		SELECT @personId = Id FROM [dbo].[Person] WHERE WindowsId = 'asiapacific\fuxi'
		INSERT INTO [dbo].[Person_Role] ([PersonId], [RoleId], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (@personId, @intRole_Administrator, @strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy, getdate())
	END
	
	PRINT 'Finished loading "QA" DB data'

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	---Capture the details of the original error
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY()

	PRINT 'Error occurred while loading "QA" DB data'
	ROLLBACK TRANSACTION

	---Raise an error with the details of the original error
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO

