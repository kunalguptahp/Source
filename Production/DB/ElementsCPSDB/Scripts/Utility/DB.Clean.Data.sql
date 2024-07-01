-- =============================================
-- Author:	Justin Webster
-- Create date:	2008/07/01
-- Description:	Deletes all data from all tables in the DB, and reseeds all identity columns.
-- Note:	Adapted from http://blogs.officezealot.com/mauro/archive/2006/03/12/9402.aspx
-- Warning:	This script (as currently implemented) may enable triggers and/or constraints 
-- 		that were already disabled before the script was executed.
-- =============================================

--- Make sure we're modifying the correct DB
--USE [ElementsCPSDB]
--GO

--- Configure script options
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO

BEGIN TRY
	BEGIN TRANSACTION

	PRINT N'Cleaning DB data'

	-- BEGIN
	-- 	PRINT N'Disabling ''[trgLogDDLEvent]'' database trigger';
	-- 	DISABLE TRIGGER [trgLogDDLEvent] ON DATABASE;
	-- END

	--- Disable all database triggers
	BEGIN
		PRINT N'Disabling all database triggers';
		DISABLE TRIGGER ALL ON DATABASE;
	END

	--- Disable referential integrity for all tables
	BEGIN
		PRINT N'Disabling all table constraints';
		EXEC sp_MSForEachTable '
			PRINT N''Disabling ''''?'''' table constraints''
			ALTER TABLE ? NOCHECK CONSTRAINT ALL
		';
	END

	--- Disable triggers for all tables
	BEGIN
		PRINT N'Disabling all table triggers';
		EXEC sp_MSForEachTable '
			PRINT N''Disabling ''''?'''' table triggers''
			ALTER TABLE ? DISABLE TRIGGER ALL
		';
	END

	--- Delete/truncate all data from all tables
	BEGIN
		PRINT N'Deleting/truncating data from all tables';
		EXEC sp_MSForEachTable '
			IF OBJECTPROPERTY(object_id(''?''), ''TableHasForeignRef'') = 1
				BEGIN
					PRINT N''Deleting ''''?'''' data''
					DELETE FROM ?
				END
			ELSE
				BEGIN
					PRINT N''Truncating ''''?'''' data''
					TRUNCATE TABLE ?
				END
		';
	END

	--- Reenable referential integrity
	BEGIN
		PRINT N'Enabling all table constraints';
		EXEC sp_MSForEachTable '
			PRINT N''Enabling ''''?'''' table constraints''
			ALTER TABLE ? CHECK CONSTRAINT ALL
		';
	END

	--- Reenable triggers for all tables
	BEGIN
		PRINT N'Enabling all table triggers';
		EXEC sp_MSForEachTable '
			PRINT N''Enabling ''''?'''' table triggers''
			ALTER TABLE ? ENABLE TRIGGER ALL
		';
	END

	--- Reseed identities to 1 for each table [don't run this exec if you don't want all your seeds to be reset] 
	BEGIN
		PRINT N'Reseeding all table identities to 1';
		EXEC sp_MSForEachTable ' 
			IF OBJECTPROPERTY(object_id(''?''), ''TableHasIdentity'') = 1 
				BEGIN
					PRINT N''Reseeding ''''?'''' Identity to 1''
					DBCC CHECKIDENT (''?'', RESEED, 1) 
				END
		' ;
	END

	-- --- Reseed identities for each table [don't run this exec if you don't want your seeds to be reset] 
	-- BEGIN
	-- 	PRINT N'Reseeding all table identities';
	-- 	EXEC sp_MSForEachTable ' 
	-- 		IF OBJECTPROPERTY(object_id(''?''), ''TableHasIdentity'') = 1 
	-- 			BEGIN
	-- 				PRINT N''Reseeding ''''?'''' Identity''
	-- 				DBCC CHECKIDENT (''?'', RESEED) 
	-- 			END
	-- 	' ;
	-- END

	--- Enable all database triggers
	BEGIN
		PRINT N'Enabling all database triggers';
		ENABLE TRIGGER ALL ON DATABASE;
	END

	-- BEGIN
	-- 	PRINT N'Enabling ''[trgLogDDLEvent]'' database trigger';
	-- 	ENABLE TRIGGER [trgLogDDLEvent] ON DATABASE;
	-- END

	PRINT N'Finished cleaning DB data'

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	---Capture the details of the original error
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY()

	PRINT N'Error occurred while cleaning DB data'
	ROLLBACK TRANSACTION

	---Raise an error with the details of the original error
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO

