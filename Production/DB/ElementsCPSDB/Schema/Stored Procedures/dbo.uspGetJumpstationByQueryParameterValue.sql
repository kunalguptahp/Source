
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Robert Mukai
-- Create date: 7/30/12
-- Description:	Returns a list of Jumpstation Groups by Query Parameter Value
-- =============================================
CREATE PROCEDURE [dbo].[uspGetJumpstationByQueryParameterValue]
	@isCountQuery INT,
	@rowCount INT OUTPUT,
	@startRow INT,
	@rowsPerPage INT,
	@statusId INT,
	@jumpstationTypeId INT,
	@queryParameterValueIdDelimitedList VARCHAR(512)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @jumpstationGroupIdTable TABLE (Id INT)
	DECLARE @jumpstationGroupIdIntersectTable TABLE (Id INT)
	
	DECLARE @queryParameterValueIdTable TABLE (Id INT)
	DECLARE @queryParameterValueId INT
	DECLARE @pos INT

	SET @queryParameterValueIdDelimitedList = LTRIM(RTRIM(@queryParameterValueIdDelimitedList))+ ','
	SET @pos = CHARINDEX(',', @queryParameterValueIdDelimitedList, 1)

	-- delimited list of query parameter value ids
	IF REPLACE(@queryParameterValueIdDelimitedList, ',', '') <> ''
	BEGIN
		WHILE @pos > 0
		BEGIN
			SET @queryParameterValueId = LTRIM(RTRIM(LEFT(@queryParameterValueIdDelimitedList, @Pos - 1)))
			IF @queryParameterValueId <> ''
			BEGIN
				INSERT INTO @queryParameterValueIdTable (Id) VALUES (CAST(@queryParameterValueId AS INT)) --Use Appropriate conversion
			END
			SET @queryParameterValueIdDelimitedList = RIGHT(@queryParameterValueIdDelimitedList, LEN(@queryParameterValueIdDelimitedList) - @pos)
			SET @pos = CHARINDEX(',', @queryParameterValueIdDelimitedList, 1)
		END
	END	

	-- check if any query parameters selected
	IF (SELECT COUNT(*) FROM @queryParameterValueIdTable) = 0 
	BEGIN
		-- if No parameter then just load all ids
		INSERT INTO @jumpstationGroupIdIntersectTable
			SELECT DISTINCT dbo.JumpstationGroup.Id
			FROM dbo.JumpstationGroup WITH (NOLOCK)
	END
	ELSE
	BEGIN
		-- find unique jumpstation group ids that have query parameter value ids to filter
		DECLARE @firstPass INT
		SET @firstPass = -1
		WHILE (SELECT COUNT(*) FROM @queryParameterValueIdTable) > 0 
		BEGIN 
			SELECT TOP 1 @queryParameterValueId = Id FROM @queryParameterValueIdTable
	 
			IF @firstPass = -1
			BEGIN
				SET @firstPass = 0	
				INSERT INTO @jumpstationGroupIdIntersectTable
					SELECT DISTINCT dbo.JumpstationGroupSelector.JumpstationGroupId
					FROM dbo.JumpstationGroupSelector WITH (NOLOCK) INNER JOIN
						 dbo.JumpstationGroupSelector_QueryParameterValue WITH (NOLOCK) ON 
				         dbo.JumpstationGroupSelector.Id = dbo.JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId
					WHERE dbo.JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = @queryParameterValueId
			END
			ELSE
			BEGIN
				INSERT INTO @jumpstationGroupIdTable
					SELECT DISTINCT Id FROM @jumpstationGroupIdIntersectTable
					INTERSECT
					SELECT DISTINCT dbo.JumpstationGroupSelector.JumpstationGroupId
					FROM dbo.JumpstationGroupSelector WITH (NOLOCK) INNER JOIN
						 dbo.JumpstationGroupSelector_QueryParameterValue WITH (NOLOCK) ON 
				         dbo.JumpstationGroupSelector.Id = dbo.JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId
					WHERE dbo.JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = @queryParameterValueId
					
				DELETE @jumpstationGroupIdIntersectTable
				INSERT INTO @jumpstationGroupIdIntersectTable
					SELECT Id FROM @jumpstationGroupidTable				
				DELETE @jumpstationGroupIdTable
			END
 
			DELETE @queryParameterValueIdTable WHERE Id = @queryParameterValueId  
		END
	END 

	IF @isCountQuery = -1
	BEGIN
		SELECT @rowCount = COUNT(dbo.JumpstationGroup.Id)
			FROM dbo.JumpstationGroup WITH (NOLOCK) INNER JOIN
			dbo.JumpstationGroupStatus WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupStatusId = dbo.JumpstationGroupStatus.Id INNER JOIN
			dbo.JumpstationGroupType WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id INNER JOIN
			dbo.JumpstationApplication WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationApplicationId = dbo.JumpstationApplication.Id INNER JOIN
			dbo.Person WITH (NOLOCK) ON dbo.JumpstationGroup.OwnerId = dbo.Person.Id INNER JOIN
			@jumpstationGroupIdIntersectTable AS aa ON dbo.JumpstationGroup.Id = aa.Id AND 
			(@statusId = 0 OR dbo.JumpstationGroup.JumpstationGroupStatusId = @statusId) AND
			(@jumpstationTypeId = 0 OR dbo.JumpstationGroup.JumpstationGroupTypeId = @jumpstationTypeId)
		SELECT @rowCount
	END
	ELSE
	BEGIN
		SET @rowCount = 0
		SELECT * FROM 
			(SELECT ROW_NUMBER() OVER ( ORDER BY [dbo].[JumpstationGroup].[Id]) AS Row, 
			dbo.JumpstationGroupStatus.Name AS JumpstationGroupStatusName, dbo.JumpstationGroupType.Name AS JumpstationGroupTypeName, dbo.JumpstationGroup.Id, dbo.JumpstationGroup.CreatedBy, 
			dbo.JumpstationGroup.CreatedOn, dbo.JumpstationGroup.ModifiedBy, dbo.JumpstationGroup.ModifiedOn, dbo.JumpstationGroup.Name AS Name, 
			dbo.JumpstationGroup.Description, dbo.JumpstationGroup.TargetURL, dbo.JumpstationGroup.[Order], 
			dbo.JumpstationApplication.Name AS JumpstationApplicationName, dbo.Person.Name AS PersonName, dbo.JumpstationGroup.ProductionId, 
			dbo.JumpstationGroup.ValidationId
			FROM dbo.JumpstationGroup WITH (NOLOCK) INNER JOIN
			dbo.JumpstationGroupStatus WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupStatusId = dbo.JumpstationGroupStatus.Id INNER JOIN
			dbo.JumpstationGroupType WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id INNER JOIN
			dbo.JumpstationApplication WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationApplicationId = dbo.JumpstationApplication.Id INNER JOIN
			dbo.Person WITH (NOLOCK) ON dbo.JumpstationGroup.OwnerId = dbo.Person.Id INNER JOIN
			@jumpstationGroupIdIntersectTable AS aa ON dbo.JumpstationGroup.Id = aa.Id
			WHERE (@statusId = 0 OR dbo.JumpstationGroup.JumpstationGroupStatusId = @statusId) AND
				(@jumpstationTypeId = 0 OR dbo.JumpstationGroup.JumpstationGroupTypeId = @jumpstationTypeId)
			) AS PagedResults
		WHERE  Row >= @startRow AND Row <= @startRow + @rowsPerPage 
	END
END
GO
