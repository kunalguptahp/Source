-- *** Important *** Need to verifty group type ids.
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO

BEGIN TRY

PRINT 'Loading "Jumpstation" DB data'

DECLARE @strDefaultValue_CreatedBy NVARCHAR(50);
DECLARE @strDefaultValue_ModifiedBy NVARCHAR(50);
DECLARE @intRowStatus_Active int;
DECLARE @intRowStatus_Inactive int;
DECLARE @intRowStatus_Deleted int;
DECLARE @intStatus_modified int;

SET @strDefaultValue_CreatedBy = N'system\system';
SET @strDefaultValue_ModifiedBy = @strDefaultValue_CreatedBy;
SET @intRowStatus_Active = 1;
SET @intRowStatus_Inactive = 2;
SET @intRowStatus_Deleted = 3;
SET @intStatus_modified = 1;

DECLARE @queryParameterId int;

DECLARE @simpleGroupTypeId int;
DECLARE @allGroupTypeId int;
DECLARE @defaultGroupTypeId int;

PRINT 'Migrating rows from [dbo].[ProxyURL] to [dbo].[JumpstationGroup]'

SELECT @simpleGroupTypeId = Id FROM [JumpstationGroupType] WHERE Name = 'Simple Redirect URLs'
SELECT @allGroupTypeId = Id FROM [JumpstationGroupType] WHERE Name = 'All Other Redirect URLs (Desktop Icon)'
SELECT @defaultGroupTypeId = Id FROM [JumpstationGroupType] WHERE Name = 'Default Redirect URLs (IE Home/Search)'

BEGIN TRANSACTION

-- update existing jumpstation group validationId = null, publicationId = null and status = readytovalidate
UPDATE JumpstationGroup SET ValidationId = null, ProductionId = null, JumpstationGroupStatusId = 2 WHERE ValidationId IS NOT NULL

-- add Type Query Parameter
INSERT INTO QueryParameter 
	(
	[CreatedBy], 
	[CreatedOn], 
	[ModifiedBy], 
	[ModifiedOn], 
	[RowStatusId], 
	[Name], 
	[Description],
	[ElementsKey]
	)
	VALUES 
	(
	@strDefaultValue_CreatedBy,
	getdate(),
	@strDefaultValue_ModifiedBy,
	getdate(),
	@intRowStatus_Active,
	'Type (Legacy)',
	'Type Parameter',
	'TYPE'
	);

SET @queryParameterId = SCOPE_IDENTITY();

-- add Type Query Parameter Values
INSERT INTO QueryParameterValue
	(
	[CreatedBy], 
	[CreatedOn], 
	[ModifiedBy], 
	[ModifiedOn], 
	[RowStatusId], 
	[Name], 
	[Description],
	[QueryParameterId] 
	)
	VALUES 
	(
	@strDefaultValue_CreatedBy,
	getdate(),
	@strDefaultValue_ModifiedBy,
	getdate(),
	@intRowStatus_Active,
	'3',
	'Default Redirect URLs (IE Home/Search)',
	@queryParameterId	
	);

INSERT INTO QueryParameterValue
	(
	[CreatedBy], 
	[CreatedOn], 
	[ModifiedBy], 
	[ModifiedOn], 
	[RowStatusId], 
	[Name], 
	[Description],
	[QueryParameterId] 
	)
	VALUES 
	(
	@strDefaultValue_CreatedBy,
	getdate(),
	@strDefaultValue_ModifiedBy,
	getdate(),
	@intRowStatus_Active,
	'4',
	'All Other Redirect URLs (Desktop Icon)',
	@queryParameterId	
	);

INSERT INTO QueryParameterValue
	(
	[CreatedBy], 
	[CreatedOn], 
	[ModifiedBy], 
	[ModifiedOn], 
	[RowStatusId], 
	[Name], 
	[Description],
	[QueryParameterId] 
	)
	VALUES 
	(
	@strDefaultValue_CreatedBy,
	getdate(),
	@strDefaultValue_ModifiedBy,
	getdate(),
	@intRowStatus_Active,
	'6',
	'Simple Redirect URLs',
	@queryParameterId	
	);

-- map query parameter to jumpstation type
INSERT INTO QueryParameter_JumpstationGroupType
	(
	[CreatedBy], 
	[CreatedOn], 
	[ModifiedBy], 
	[ModifiedOn],  
	[Name], 
	[JumpstationGroupTypeId],
	[QueryParameterId],
	[MaximumSelection],
	[Wildcard],
	[SortOrder]
	)
	VALUES 
	(
	@strDefaultValue_CreatedBy,
	getdate(),
	@strDefaultValue_ModifiedBy,
	getdate(),
	'Simple Redirect URLs/Type',
	@simpleGroupTypeId,
	@queryParameterId,
	1,
	0,
	2	
	);

INSERT INTO QueryParameter_JumpstationGroupType
	(
	[CreatedBy], 
	[CreatedOn], 
	[ModifiedBy], 
	[ModifiedOn],  
	[Name], 
	[JumpstationGroupTypeId],
	[QueryParameterId],
	[MaximumSelection],
	[Wildcard],
	[SortOrder]
	)
	VALUES 
	(
	@strDefaultValue_CreatedBy,
	getdate(),
	@strDefaultValue_ModifiedBy,
	getdate(),
	'Default Redirect URLs/Type',
	@defaultGroupTypeId,
	@queryParameterId,
	1,
	0,
	6	
	);

INSERT INTO QueryParameter_JumpstationGroupType
	(
	[CreatedBy], 
	[CreatedOn], 
	[ModifiedBy], 
	[ModifiedOn],  
	[Name], 
	[JumpstationGroupTypeId],
	[QueryParameterId],
	[MaximumSelection],
	[Wildcard],
	[SortOrder]
	)
	VALUES 
	(
	@strDefaultValue_CreatedBy,
	getdate(),
	@strDefaultValue_ModifiedBy,
	getdate(),
	'All Other Redirect URLs/Type',
	@allGroupTypeId,
	@queryParameterId,
	1,
	0,
	7	
	);
	
PRINT 'Finished loading "Jumpstation" DB data'

COMMIT TRANSACTION

END TRY
BEGIN CATCH
	---Capture the details of the original error
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY()

	PRINT 'Error occurred while updating Jumpstation Type Parameter'
	ROLLBACK TRANSACTION

	---Raise an error with the details of the original error
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO
