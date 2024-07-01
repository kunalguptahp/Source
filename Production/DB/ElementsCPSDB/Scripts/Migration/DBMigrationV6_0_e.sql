/*
Run this script on:

        (local).ElementsCPSDBTest    -  This database will be modified

to synchronize it with:

        (local).ElementsCPSDB

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.50.10 from Red Gate Software Ltd at 8/14/2012 3:27:30 PM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Creating [dbo].[vwMapJumpstationDomain]'
GO
CREATE VIEW dbo.vwMapJumpstationDomain
AS
SELECT     dbo.JumpstationDomain.Id, dbo.JumpstationDomain.Name, dbo.JumpstationDomain.CreatedBy, dbo.JumpstationDomain.CreatedOn, 
                      dbo.JumpstationDomain.ModifiedBy, dbo.JumpstationDomain.ModifiedOn, dbo.JumpstationDomain.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationDomain.DomainURL
FROM         dbo.JumpstationDomain WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationDomain.RowStatusId = dbo.RowStatus.Id
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[uspGetJumpstationByQueryParameterValue]'
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
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_JumpstationDomain] on [dbo].[JumpstationDomain]'
GO
ALTER TABLE [dbo].[JumpstationDomain] ADD CONSTRAINT [PK_JumpstationDomain] PRIMARY KEY NONCLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_CreatedBy] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_CreatedBy] ON [dbo].[JumpstationGroup] ([CreatedBy])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_CreatedOn] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_CreatedOn] ON [dbo].[JumpstationGroup] ([CreatedOn])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_JumpstationApplicationId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_JumpstationApplicationId] ON [dbo].[JumpstationGroup] ([JumpstationApplicationId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_JumpstationGroupTypeId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_JumpstationGroupTypeId] ON [dbo].[JumpstationGroup] ([JumpstationGroupTypeId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_ModifiedBy] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ModifiedBy] ON [dbo].[JumpstationGroup] ([ModifiedBy])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_ModifiedOn] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ModifiedOn] ON [dbo].[JumpstationGroup] ([ModifiedOn])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_Name] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_Name] ON [dbo].[JumpstationGroup] ([Name])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_OwnerId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_OwnerId] ON [dbo].[JumpstationGroup] ([OwnerId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_ProductionId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ProductionId] ON [dbo].[JumpstationGroup] ([ProductionId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroup_ValidationId] on [dbo].[JumpstationGroup]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroup_ValidationId] ON [dbo].[JumpstationGroup] ([ValidationId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroupSelector_JumpstationGroupId] on [dbo].[JumpstationGroupSelector]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroupSelector_JumpstationGroupId] ON [dbo].[JumpstationGroupSelector] ([JumpstationGroupId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_JumpstationGroupSelector_QueryParameterValue_JumpstationGroupSelectorId] on [dbo].[JumpstationGroupSelector_QueryParameterValue]'
GO
CREATE NONCLUSTERED INDEX [IX_JumpstationGroupSelector_QueryParameterValue_JumpstationGroupSelectorId] ON [dbo].[JumpstationGroupSelector_QueryParameterValue] ([JumpstationGroupSelectorId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[JumpstationDomain]'
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationDomain_Description_IsTrimmed] CHECK ((len(ltrim(rtrim([Description])))=len([Description])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationDomain_Name] CHECK ((len(rtrim([Name]))>(0)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationDomain_Name_IsTrimmed] CHECK ((len(ltrim(rtrim([Name])))=len([Name])))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD CONSTRAINT [CK_JumpstationDomain_Name_MinLen] CHECK ((len(rtrim([Name]))>=(3)))
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationDomain]'
GO
ALTER TABLE [dbo].[JumpstationDomain] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationDomain_RowStatus] FOREIGN KEY ([RowStatusId]) REFERENCES [dbo].[RowStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[JumpstationGroupType]'
GO
ALTER TABLE [dbo].[JumpstationGroupType] WITH NOCHECK ADD
CONSTRAINT [FK_JumpstationGroupType_PublicationJumpstationDomain] FOREIGN KEY ([PublicationJumpstationDomainId]) REFERENCES [dbo].[JumpstationDomain] ([Id]),
CONSTRAINT [FK_JumpstationGroupType_ValidationJumpstationDomain] FOREIGN KEY ([ValidationJumpstationDomainId]) REFERENCES [dbo].[JumpstationDomain] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
