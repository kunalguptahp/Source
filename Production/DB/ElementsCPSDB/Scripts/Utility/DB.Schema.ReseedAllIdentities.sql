-- =============================================
-- Author:	Justin Webster
-- Create date:	2009/09/29
-- Description:	Reseeds all Identity columns in the DB's entire schema.
-- =============================================

SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO

BEGIN TRY
	BEGIN TRANSACTION

	PRINT 'Reseeding all Identity columns.'

	--- Reseed identities for each table
	BEGIN
		PRINT 'Reseeding Identities'
		EXEC sp_MSForEachTable ' 
			IF OBJECTPROPERTY(object_id(''?''), ''TableHasIdentity'') = 1 
				BEGIN
					--DECLARE @highestId int
					--DECLARE @newSeed int
					--SET @highestId = (SELECT MAX([Id]) FROM ?);
					--SET @newSeed = (SELECT 1+ISNULL(@highestId, 0));
					--PRINT N''Reseeding ''''?'''' Identity to '' + CONVERT(NVARCHAR(20), @newSeed)
					--DBCC CHECKIDENT (''?'', RESEED, @newSeed) 
					PRINT N''Reseeding ''''?'''' Identity''
					DBCC CHECKIDENT (''?'', RESEED)
				END
		' 
	END

	PRINT 'Finished reseeding all Identity columns.'

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	---Capture the details of the original error
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY()

	PRINT 'Error occurred while loading "Bootstrap" DB data'
	ROLLBACK TRANSACTION

	---Raise an error with the details of the original error
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO
