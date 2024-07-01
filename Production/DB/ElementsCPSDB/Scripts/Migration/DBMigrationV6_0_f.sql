-- *** Important *** Need to verifty group type ids.
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO

BEGIN TRY

SET NOCOUNT ON

PRINT 'Loading "Jumpstation" DB data'

DECLARE @intRowStatus_Active INT;
DECLARE @intStatus_ReadyToValidate INT;

SET @intRowStatus_Active = 1;
SET @intStatus_ReadyToValidate = 2;

DECLARE @originalJumpstationId INT;
DECLARE @originalJumpstationGroupSelectorId INT;

DECLARE @cycleWildcardId INT;
SELECT @cycleWildcardId = [Id] FROM [QueryParameterValue] where queryParameterId = 7 and Name = '*'

DECLARE @jumpstationGroupId INT;
DECLARE @jumpstationGroupSelectorId INT;
DECLARE @jumpstationGroupCount INT;
DECLARE @jumpstationGroupTypeAllId INT;

SELECT @jumpstationGroupTypeAllId = [Id] FROM [JumpstationGroupType] where Name = 'All Other Redirect URLs (Desktop Icon)'

PRINT 'Cloning for Cycle = 124'
BEGIN

DECLARE jumpstationGroupCursor CURSOR LOCAL FAST_FORWARD FOR 
	SELECT DISTINCT JumpstationGroupSelector.JumpstationGroupId
	FROM JumpstationGroup INNER JOIN
         JumpstationGroupSelector ON JumpstationGroup.Id = JumpstationGroupSelector.JumpstationGroupId INNER JOIN
         JumpstationGroupSelector_QueryParameterValue ON 
         JumpstationGroupSelector.Id = JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId INNER JOIN
         QueryParameterValue ON JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
	WHERE QueryParameterValue.Name IN ('seeker_updater_xml', 'seeker_mobileapp', 'seeker_learnmore', 'iefavs', 'iefavbar', 'dticon', 'onlinesvs') AND 
	QueryParameterValue.QueryParameterId = 13 AND
	JumpstationGroup.JumpstationGroupStatusId = 4 AND
	JumpstationGroup.JumpstationGroupTypeId = @jumpstationGroupTypeAllId
	INTERSECT
	SELECT DISTINCT JumpstationGroupSelector.JumpstationGroupId
	FROM JumpstationGroup INNER JOIN
         JumpstationGroupSelector ON JumpstationGroup.Id = JumpstationGroupSelector.JumpstationGroupId INNER JOIN
         JumpstationGroupSelector_QueryParameterValue ON 
         JumpstationGroupSelector.Id = JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId INNER JOIN
         QueryParameterValue ON JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
	WHERE dbo.QueryParameterValue.Name = '124' AND 
	QueryParameterValue.QueryParameterId = 7 AND
	JumpstationGroup.JumpstationGroupStatusId = 4 AND
	JumpstationGroup.JumpstationGroupTypeId = @jumpstationGroupTypeAllId

OPEN jumpstationGroupCursor 
FETCH NEXT FROM jumpstationGroupCursor INTO 
	@originalJumpstationId
WHILE @@FETCH_STATUS = 0 
BEGIN 
	-- see if already added
    SELECT @jumpstationGroupCount = COUNT(Id) FROM JumpstationGroup WHERE Name = 'Cloned - ' + ltrim(str(@originalJumpstationId))	
	IF (@jumpstationGroupCount = 0)
	BEGIN
		BEGIN TRANSACTION

		PRINT 'Cloned - ' + ltrim(str(@originalJumpstationid))
	
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
		SELECT [CreatedBy],
			GETDATE(),
			[ModifiedBy],
			GETDATE(),
			[RowStatusId],
			'Cloned - ' + ltrim(str(@originalJumpstationid)),
			[Description],
			[TargetURL],
			1,
			@intStatus_ReadyToValidate,
			[JumpstationGroupTypeId],
			[JumpstationApplicationId],
			[OwnerId],
			null,
			null
		FROM JumpstationGroup
		WHERE Id = @originalJumpstationId;

		-- Add selector
		SET @jumpstationGroupId = SCOPE_IDENTITY();
		SELECT TOP 1 @originalJumpstationGroupSelectorId = Id FROM JumpstationGroupSelector WHERE JumpstationGroupId = @originalJumpstationId;
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
		SELECT TOP 1 [CreatedBy],
			GETDATE(),
			[ModifiedBy],
			GETDATE(),
			[RowStatusId],
			'Selector 1 - ' + ltrim(str(@originalJumpstationId)),
			[Description],
			@jumpstationGroupId
		FROM JumpstationGroupSelector
		WHERE JumpstationGroupId = @originalJumpstationId;

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
			SELECT JumpstationGroupSelector_QueryParameterValue.CreatedBy, GETDATE(), JumpstationGroupSelector_QueryParameterValue.ModifiedBy, GETDATE(), @jumpstationGroupSelectorId, QueryParameterValueId, 0
			FROM JumpstationGroupSelector_QueryParameterValue INNER JOIN QueryParameterValue ON JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id	
			WHERE JumpstationGroupSelectorId = @originalJumpstationGroupSelectorId AND QueryParameterId <> 7

		-- Add cycle wildcard
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
			SELECT JumpstationGroupSelector_QueryParameterValue.CreatedBy, GETDATE(), JumpstationGroupSelector_QueryParameterValue.ModifiedBy, GETDATE(), @jumpstationGroupSelectorId, @cycleWildcardId, 0
			FROM JumpstationGroupSelector_QueryParameterValue INNER JOIN QueryParameterValue ON JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id	
			WHERE JumpstationGroupSelectorId = @originalJumpstationGroupSelectorId AND QueryParameterId = 7

		COMMIT TRANSACTION
	END

	FETCH NEXT FROM jumpstationGroupCursor INTO 
		@originalJumpstationId
END 
 
CLOSE jumpstationGroupCursor 
DEALLOCATE jumpstationGroupCursor 

END	

PRINT 'Finished loading "Jumpstation" clone data'

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
