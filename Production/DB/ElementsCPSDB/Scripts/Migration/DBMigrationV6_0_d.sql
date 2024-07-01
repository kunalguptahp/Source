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

DECLARE @id int;
DECLARE @createdBy nvarchar(50);
DECLARE @createdOn DateTime;
DECLARE @modifiedBy nvarchar(50);
DECLARE @modifiedOn DateTime;
DECLARE @rowStatusId int;
DECLARE @description nvarchar(max);
DECLARE @proxyURLStatusId int;
DECLARE @proxyURLTypeId int;
DECLARE @queryParameterId int;
DECLARE @queryParameterValueId int;
DECLARE @queryParameterValue3Id int;
DECLARE @queryparameterValue4Id int;
DECLARE @queryparameterValue6Id int;
DECLARE @ownerId int;
DECLARE @URL varchar(512);
DECLARE @productionId int;
DECLARE @validationId int;

DECLARE @jumpstationGroupId int;
DECLARE @jumpstationGroupSelectorId int;
DECLARE @jumpstationGroupTypeId int;

DECLARE @simpleGroupTypeId int;
DECLARE @allGroupTypeId int;
DECLARE @defaultGroupTypeId int;
DECLARE @jumpstationGroupCount int;

PRINT 'Migrating rows from [dbo].[ProxyURL] to [dbo].[JumpstationGroup]'
BEGIN

SELECT @simpleGroupTypeId = Id FROM [JumpstationGroupType] WHERE Name = 'Simple Redirect URLs'
SELECT @allGroupTypeId = Id FROM [JumpstationGroupType] WHERE Name = 'All Other Redirect URLs (Desktop Icon)'
SELECT @defaultGroupTypeId = Id FROM [JumpstationGroupType] WHERE Name = 'Default Redirect URLs (IE Home/Search)'

SELECT @queryParameterId = Id FROM [QueryParameter] WHERE ElementsKey = 'Type'
SELECT @queryParameterValue3Id = Id FROM [QueryParameterValue] WHERE QueryParameterId = @queryParameterId AND Name = '3'
SELECT @queryParameterValue4Id = Id FROM [QueryParameterValue] WHERE QueryParameterId = @queryParameterId AND Name = '4'
SELECT @queryParameterValue6Id = Id FROM [QueryParameterValue] WHERE QueryParameterId = @queryParameterId AND Name = '6'

DECLARE proxyURLCursor CURSOR LOCAL FAST_FORWARD FOR 
  SELECT [Id]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[ModifiedBy]
      ,[ModifiedOn]
      ,[RowStatusId]
      ,[Description]
      ,[ProxyURLStatusId]
      ,[ProxyURLTypeId]
      ,[OwnerId]
      ,[URL]
      ,[ProductionId]
      ,[ValidationId]
  FROM [ProxyURL]

OPEN proxyURLCursor 
FETCH NEXT FROM proxyURLCursor INTO 
	@id,
	@createdBy,
	@createdOn,
	@modifiedBy,
	@modifiedOn,
	@rowStatusId,
	@description,
	@proxyURLStatusId,
	@proxyURLTypeId,
	@ownerId,
	@URL,
	@productionId,
	@validationId
WHILE @@FETCH_STATUS = 0 
BEGIN 
	-- see if already added
    SELECT @jumpstationGroupCount = COUNT(Id) FROM JumpstationGroup WHERE Name = 'Redirect - ' + ltrim(str(@id))	
	IF (@jumpstationGroupCount = 0)
	BEGIN
		BEGIN TRANSACTION
	PRINT 'Redirect - ' + ltrim(str(@id))
	
		SELECT @jumpstationGroupTypeId = 
			CASE 
				WHEN @proxyURLTypeId = 1 THEN (SELECT @simpleGroupTypeId)
				WHEN @proxyURLTypeId = 2 THEN (SELECT @allGroupTypeId)
				WHEN @proxyURLTypeId = 3 THEN (SELECT @defaultGroupTypeId)
			ELSE -1
	    END

		INSERT INTO JumpstationGroup 
			(
			[CreatedBy], 
			[CreatedOn], 
			[ModifiedBy], 
			[ModifiedOn], 
			[RowStatusId], 
			[Name], 
			[Description], 
			[TargetURL], 
			[Order],
			[JumpstationGroupStatusId],
			[JumpstationGroupTypeId],
			[JumpstationApplicationId],
			[OwnerId],
			[ProductionId],
			[ValidationId]
			)
		VALUES 
			(
			@createdBy,
			@createdOn,
			@modifiedBy,
			@modifiedOn,
			@rowStatusId,
			'Redirect - ' + ltrim(str(@id)),
			@description,
			@URL,
			1,
			@proxyURLStatusId,
			@jumpstationGroupTypeId,
			2, -- *** Important *** check for proper application id
			@ownerId,
			@productionId,
			@validationId
			);

		-- Add selector
		SET @jumpstationGroupId = SCOPE_IDENTITY();
		INSERT INTO JumpstationGroupSelector
			(
			[CreatedBy], 
			[CreatedOn], 
			[ModifiedBy], 
			[ModifiedOn], 
			[RowStatusId], 
			[Name], 
			[Description], 
			[JumpstationGroupId]
			)
		VALUES 
			(
			@createdBy,
			@createdOn,
			@modifiedBy,
			@modifiedOn,
			@rowStatusId,
			'Selector 1 - ' + ltrim(str(@id)),
			null,
			@jumpstationGroupId
			);

		-- Add query parameter values
		SET @jumpstationGroupSelectorId = SCOPE_IDENTITY();
		INSERT INTO JumpstationGroupSelector_QueryParameterValue
			(
			[CreatedBy], 
			[CreatedOn], 
			[ModifiedBy], 
			[ModifiedOn], 
			[JumpstationGroupSelectorId], 
			[QueryParameterValueId], 
			[Negation]
			)
			SELECT CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, @jumpstationGroupSelectorId, QueryParameterValueId, 0
			FROM ProxyURL_QueryParameterValue
			WHERE ProxyURLId = @Id
	
		-- Add the type query parameter value
		SELECT @queryParameterValueId = 
			CASE 
				WHEN @proxyURLTypeId = 1 THEN (SELECT @queryParameterValue6Id )  -- simple
				WHEN @proxyURLTypeId = 2 THEN (SELECT @queryParameterValue4Id )  -- all
				WHEN @proxyURLTypeId = 3 THEN (SELECT @queryParameterValue3Id )  -- default
			ELSE -1
		END

		INSERT INTO JumpstationGroupSelector_QueryParameterValue
			(
			[CreatedBy], 
			[CreatedOn], 
			[ModifiedBy], 
			[ModifiedOn], 
			[JumpstationGroupSelectorId], 
			[QueryParameterValueId], 
			[Negation]
			)
			SELECT @createdBy, getdate(), @modifiedBy, getdate(), @jumpstationGroupSelectorId, @queryParameterValueId, 0

		COMMIT TRANSACTION
	END

	FETCH NEXT FROM proxyURLCursor INTO 
		@id,
		@createdBy,
		@createdOn,	
		@modifiedBy,
		@modifiedOn,
		@rowStatusId,
		@description,
		@proxyURLStatusId,
		@proxyURLTypeId,
		@ownerId,
		@URL,
		@productionId,
		@validationId
END 
 
CLOSE proxyURLCursor 
DEALLOCATE proxyURLCursor 

END	

PRINT 'Finished loading "Jumpstation" DB data'

END TRY
BEGIN CATCH
	---Capture the details of the original error
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY()

	PRINT 'Error occurred while migrating rows from [dbo].[ProxyURL] to [dbo].[JumpstationGroup]'
	ROLLBACK TRANSACTION

	---Raise an error with the details of the original error
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO
