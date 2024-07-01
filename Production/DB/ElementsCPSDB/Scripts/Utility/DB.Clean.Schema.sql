-- =============================================
-- Author:	Justin Webster
-- Create date:	2008/07/01
-- Description:	Deletes all primary schema objects (e.g. tables, views, SPs, UDFs) in the DB.
-- Note:	Adapted from http://paigecsharp.blogspot.com/2008/03/drop-all-objects-in-sql-server-database.html
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

	PRINT N'Cleaning DB schema'

	---Declare the variables used throughout the script
	DECLARE @name VARCHAR(128)
	DECLARE @constraint VARCHAR(254)
	DECLARE @SQL VARCHAR(254)

	---Drop all (DATABASE-level) DDL Triggers
	BEGIN
		PRINT N'Dropping all (DATABASE-level) DDL Triggers'
		SELECT @name = (
			SELECT TOP 1
				[t].[name]
			FROM
				[sys].[triggers] t
			WHERE
				[t].[parent_id] = 0
				AND [t].[parent_class] = 0
				AND [t].[parent_class_desc] = 'DATABASE'
				AND [t].[type] = 'TR'
				AND [t].[type_desc] = 'SQL_TRIGGER'
			ORDER BY
				t.[name]
			)

		WHILE @name IS NOT NULL
			BEGIN
				PRINT 'Dropping DDL Trigger [' + RTRIM(@name) + ']'
				SELECT  @SQL = 'DROP TRIGGER [' + RTRIM(@name) + '] ON DATABASE'
				EXEC ( @SQL )
				PRINT 'Dropped  DDL Trigger [' + RTRIM(@name) + ']'
				SELECT @name = (
					SELECT TOP 1
						[t].[name]
					FROM
						[sys].[triggers] t
					WHERE
						[t].[parent_id] = 0
						AND [t].[parent_class] = 0
						AND [t].[parent_class_desc] = 'DATABASE'
						AND [t].[type] = 'TR'
						AND [t].[type_desc] = 'SQL_TRIGGER'
					ORDER BY
						t.[name]
					)
			END
	END

	---Drop all (non-system) Stored Procedures
	BEGIN
		PRINT N'Dropping all (non-system) Stored Procedures'
		SELECT  @name = (	SELECT TOP 1
						[name]
					FROM	sysobjects
					WHERE	[type] = 'P'
						AND category = 0
					ORDER BY	[name]
				)

		WHILE @name IS NOT NULL
			BEGIN
				PRINT 'Dropping Procedure [dbo].[' + RTRIM(@name) + ']'
				SELECT  @SQL = 'DROP PROCEDURE [dbo].[' + RTRIM(@name) + ']'
				EXEC ( @SQL )
				PRINT 'Dropped  Procedure [dbo].[' + RTRIM(@name) + ']'
				SELECT  @name = (	SELECT TOP 1
								[name]
							FROM	sysobjects
							WHERE	[type] = 'P'
								AND category = 0
								AND [name] > @name
							ORDER BY	[name]
						)
			END
	END

	---Drop all Views
	BEGIN
		PRINT N'Dropping all Views'
		SELECT  @name = (	SELECT TOP 1
						[name]
					FROM	sysobjects
					WHERE	[type] = 'V'
						AND category = 0
					ORDER BY  [name]
				)

		WHILE @name IS NOT NULL
			BEGIN
				PRINT 'Dropping View [dbo].[' + RTRIM(@name) + ']'
				SELECT  @SQL = 'DROP VIEW [dbo].[' + RTRIM(@name) + ']'
				EXEC ( @SQL )
				PRINT 'Dropped  View [dbo].[' + RTRIM(@name) + ']'
				SELECT  @name = (	SELECT TOP 1
								[name]
							FROM	sysobjects
							WHERE	[type] = 'V'
								AND category = 0
								AND [name] > @name
							ORDER BY  [name]
						)
			END
	END

	---Drop all functions
	BEGIN
		PRINT N'Dropping all User-Defined Functions'
		SELECT  @name = (	SELECT TOP 1
						[name]
					FROM	sysobjects
					WHERE	[type] IN ( N'FN', N'IF', N'TF', N'FS', N'FT' )
						AND category = 0
					ORDER BY	[name]
				)

		WHILE @name IS NOT NULL
			BEGIN
				PRINT 'Dropping Function [dbo].[' + RTRIM(@name) + ']'
				SELECT  @SQL = 'DROP FUNCTION [dbo].[' + RTRIM(@name) + ']'
				EXEC ( @SQL )
				PRINT 'Dropped  Function [dbo].[' + RTRIM(@name) + ']'
				SELECT  @name = (	SELECT TOP 1
								[name]
							FROM	sysobjects
							WHERE	[type] IN ( N'FN', N'IF', N'TF', N'FS', N'FT' )
								AND category = 0
								AND [name] > @name
							ORDER BY	[name]
						)
			END
	END

	---Drop all Foreign Key constraints
	BEGIN
		PRINT N'Dropping all Foreign Key constraints'
		SELECT  @name = (	SELECT TOP 1
						TABLE_NAME
					FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS
					WHERE	constraint_catalog = DB_NAME()
						AND CONSTRAINT_TYPE = 'FOREIGN KEY'
					ORDER BY	TABLE_NAME
				)

		WHILE @name IS NOT NULL
			BEGIN
				SELECT  @constraint = (	SELECT TOP 1
								CONSTRAINT_NAME
							FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS
							WHERE	constraint_catalog = DB_NAME()
								AND CONSTRAINT_TYPE = 'FOREIGN KEY'
								AND TABLE_NAME = @name
							ORDER BY	CONSTRAINT_NAME
							)

				WHILE @constraint IS NOT NULL
					BEGIN
						PRINT 'Dropping Table FK [dbo].[' + RTRIM(@name) + '].' + RTRIM(@constraint)
						SELECT  @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) + '] DROP CONSTRAINT ' + RTRIM(@constraint)
						EXEC ( @SQL )
						PRINT 'Dropped  Table FK [dbo].[' + RTRIM(@name) + '].' + RTRIM(@constraint)
						SELECT  @constraint = (	SELECT TOP 1
										CONSTRAINT_NAME
									FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS
									WHERE	constraint_catalog = DB_NAME()
										AND CONSTRAINT_TYPE = 'FOREIGN KEY'
										AND CONSTRAINT_NAME <> @constraint
										AND TABLE_NAME = @name
									ORDER BY	CONSTRAINT_NAME
									)
					END

				SELECT  @name = (	SELECT TOP 1
								TABLE_NAME
							FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS
							WHERE	constraint_catalog = DB_NAME()
								AND CONSTRAINT_TYPE = 'FOREIGN KEY'
							ORDER BY	TABLE_NAME
						)
			END
	END

	---Drop all Primary Key constraints
	BEGIN
		PRINT N'Dropping all Primary Key constraints'
		SELECT  @name = (	SELECT TOP 1
						TABLE_NAME
					FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS
					WHERE	constraint_catalog = DB_NAME()
						AND CONSTRAINT_TYPE = 'PRIMARY KEY'
					ORDER BY	TABLE_NAME
				)
		WHILE @name IS NOT NULL
			BEGIN
				SELECT  @constraint = (	SELECT TOP 1
								CONSTRAINT_NAME
							FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS
							WHERE	constraint_catalog = DB_NAME()
								AND CONSTRAINT_TYPE = 'PRIMARY KEY'
								AND TABLE_NAME = @name
							ORDER BY	CONSTRAINT_NAME
							)

				WHILE @constraint IS NOT NULL
					BEGIN
						PRINT 'Dropping Table PK [dbo].[' + RTRIM(@name) + '].' + RTRIM(@constraint)
						SELECT  @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) + '] DROP CONSTRAINT ' + RTRIM(@constraint)
						EXEC ( @SQL )
						PRINT 'Dropped  Table PK [dbo].[' + RTRIM(@name) + '].' + RTRIM(@constraint)
						SELECT  @constraint = (	SELECT TOP 1
										CONSTRAINT_NAME
									FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS
									WHERE	constraint_catalog = DB_NAME()
										AND CONSTRAINT_TYPE = 'PRIMARY KEY'
										AND CONSTRAINT_NAME <> @constraint
										AND TABLE_NAME = @name
									ORDER BY	CONSTRAINT_NAME
									)
					END

				SELECT  @name = (	SELECT TOP 1
								TABLE_NAME
							FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS
							WHERE	constraint_catalog = DB_NAME()
								AND CONSTRAINT_TYPE = 'PRIMARY KEY'
							ORDER BY	TABLE_NAME
						)
			END
	END

	---Drop all tables
	BEGIN
		PRINT N'Dropping all User Tables'
		SELECT  @name = (	SELECT TOP 1
						[name]
					FROM	sysobjects
					WHERE	[type] = 'U'
						AND category = 0
					ORDER BY	[name]
				)

		WHILE @name IS NOT NULL
			BEGIN
				PRINT 'Dropping Table [dbo].[' + RTRIM(@name) + ']'
				SELECT  @SQL = 'DROP TABLE [dbo].[' + RTRIM(@name) + ']'
				EXEC ( @SQL )
				PRINT 'Dropped  Table [dbo].[' + RTRIM(@name) + ']'
				SELECT  @name = (	SELECT TOP 1
								[name]
							FROM	sysobjects
							WHERE	[type] = 'U'
								AND category = 0
								AND [name] > @name
							ORDER BY	[name]
						)
			END
	END

	PRINT N'Finished cleaning DB schema'

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	---Capture the details of the original error
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY()

	PRINT N'Error occurred while cleaning DB schema'
	ROLLBACK TRANSACTION

	---Raise an error with the details of the original error
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO

