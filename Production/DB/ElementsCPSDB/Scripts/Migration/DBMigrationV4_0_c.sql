SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO

BEGIN TRY
BEGIN TRANSACTION

PRINT 'Loading "Workflow" DB data'

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
DECLARE @intProxyURLStatus_Modified int;
DECLARE @intProxyURLStatus_ReadyForValidation int;
DECLARE @intProxyURLStatus_Validated int;
DECLARE @intProxyURLStatus_Published int;
DECLARE @intProxyURLStatus_Expired int;
DECLARE @intProxyURLStatus_Cancelled int;
DECLARE @intProxyURLStatus_Abandoned int;
DECLARE @intParamId int;

SET @strDefaultValue_CreatedBy = N'system\system';
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
SET @intProxyURLStatus_Modified = 1;
SET @intProxyURLStatus_ReadyForValidation = 2;
SET @intProxyURLStatus_Validated = 3;
SET @intProxyURLStatus_Published = 4;
SET @intProxyURLStatus_Expired = 4;
SET @intProxyURLStatus_Cancelled = 5;
SET @intProxyURLStatus_Abandoned = 6;

PRINT 'Adding rows to [dbo].[WorkflowStatus]'
BEGIN
SET IDENTITY_INSERT [dbo].[WorkflowStatus] ON
INSERT INTO [dbo].[WorkflowStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (1,'Modified',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'Modified State')
INSERT INTO [dbo].[WorkflowStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (2,'ReadyForValidation',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'ReadyForValidation State')
INSERT INTO [dbo].[WorkflowStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (3,'Validated',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'Validated State')
INSERT INTO [dbo].[WorkflowStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (4,'Published',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'Published State')
INSERT INTO [dbo].[WorkflowStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (5,'Published (Production Only)',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'Published (Production Only) State')
INSERT INTO [dbo].[WorkflowStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (6,'Cancelled',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'Cancelled State')
INSERT INTO [dbo].[WorkflowStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (7,'Abandoned',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'Abandoned State')
INSERT INTO [dbo].[WorkflowStatus]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES (8,'Deleted',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active, 'Deleted State')
SET IDENTITY_INSERT [dbo].[WorkflowStatus] OFF
END	

PRINT 'Adding rows to [dbo].[WorkflowApplicationType]'
BEGIN
SET IDENTITY_INSERT [dbo].[WorkflowApplicationType] ON
INSERT INTO [dbo].[WorkflowApplicationType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(1,'Workflow',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Workflow Application')
SET IDENTITY_INSERT [dbo].[WorkflowApplicationType] OFF
END	

PRINT 'Adding rows to [dbo].[WorkflowApplication]'
BEGIN
SET IDENTITY_INSERT [dbo].[WorkflowApplication] ON
INSERT INTO [dbo].[WorkflowApplication]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Version],[Description],[ElementsKey],[WorkflowApplicationTypeId])VALUES(1,'Workflow (V0.0.1)',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'0.1.1','Workflow Application','Workflow Application', 1)
SET IDENTITY_INSERT [dbo].[WorkflowApplication] OFF
END	

PRINT 'Adding rows to [dbo].[WorkflowType]'
BEGIN
SET IDENTITY_INSERT [dbo].[WorkflowType] ON
INSERT INTO [dbo].[WorkflowType]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(1,'default',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'New')
SET IDENTITY_INSERT [dbo].[WorkflowType] OFF
END	

PRINT 'Adding rows to [dbo].[WorkflowModuleCategory]'
BEGIN
SET IDENTITY_INSERT [dbo].[WorkflowModuleCategory] ON
INSERT INTO [dbo].[WorkflowModuleCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(1,'consents',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Consents')
INSERT INTO [dbo].[WorkflowModuleCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(2,'offers',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Offers')
INSERT INTO [dbo].[WorkflowModuleCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(3,'registration',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Registration')
INSERT INTO [dbo].[WorkflowModuleCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(4,'review',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Review')
INSERT INTO [dbo].[WorkflowModuleCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(5,'security',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Security')
SET IDENTITY_INSERT [dbo].[WorkflowModuleCategory] OFF
END	

PRINT 'Adding rows to [dbo].[WorkflowModuleSubCategory]'
BEGIN
SET IDENTITY_INSERT [dbo].[WorkflowModuleSubCategory] ON
INSERT INTO [dbo].[WorkflowModuleSubCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(1,'HP',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'HP')
INSERT INTO [dbo].[WorkflowModuleSubCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(2,'symantec',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Symantec')
INSERT INTO [dbo].[WorkflowModuleSubCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(3,'review',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Review')
INSERT INTO [dbo].[WorkflowModuleSubCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(4,'hpproduct',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'HPProduct')
INSERT INTO [dbo].[WorkflowModuleSubCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(5,'getonline',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'GetOnline')
INSERT INTO [dbo].[WorkflowModuleSubCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(6,'recommended',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Recommended')
INSERT INTO [dbo].[WorkflowModuleSubCategory]([Id],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(7,'stayupdated',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'StayUpdated')
SET IDENTITY_INSERT [dbo].[WorkflowModuleSubCategory] OFF
END	


PRINT 'Adding rows to [dbo].[QueryParameter]'
BEGIN
SET IDENTITY_INSERT [dbo].[QueryParameter] ON
INSERT INTO [dbo].[QueryParameter]([Id],[Name],[ElementsKey],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(201,'PC Sub-Brand','subbrand',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'PC Sub-Brand')
INSERT INTO [dbo].[QueryParameter]([Id],[Name],[ElementsKey],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(202,'Release Start','releasestart',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Release Start')
INSERT INTO [dbo].[QueryParameter]([Id],[Name],[ElementsKey],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(203,'Release End','releaseend',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Release End')
INSERT INTO [dbo].[QueryParameter]([Id],[Name],[ElementsKey],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(204,'Model Number','modelnumber',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Model Number')
INSERT INTO [dbo].[QueryParameter]([Id],[Name],[ElementsKey],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(205,'Product Name','productname',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Product Name')
INSERT INTO [dbo].[QueryParameter]([Id],[Name],[ElementsKey],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[Description])VALUES(206,'Product Number', 'productnumber',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,'Product Number')
SET IDENTITY_INSERT [dbo].[QueryParameter] OFF
END	    

PRINT 'Adding rows to [dbo].[QueryParameter_WorkflowType]'
BEGIN
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,107,'default/PC Platform',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,106,'default/PC Brand',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,101,'default/Country Code',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,100,'default/Language',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,201,'default/PC Sub-Brand',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,202,'default/Release Start',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,203,'default/Release End',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,204,'default/Model Number',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,103,'default/OS Version',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,104,'default/OS Bit',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,105,'default/OS Type',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,205,'default/Product Name',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
INSERT INTO [dbo].[QueryParameter_WorkflowType]([WorkflowTypeId],[QueryParameterId],[Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[MaximumSelection],[Wildcard])VALUES(1,206,'default/Product Number',@strDefaultValue_CreatedBy,getdate(),@strDefaultValue_ModifiedBy,getdate(),0,1)
END	    

PRINT 'Adding rows to [dbo].[QueryParameterValue]'BEGIN
SELECT @intParamId = Id FROM [dbo].[QueryParameter] WHERE name = 'PC Sub-Brand'
INSERT INTO [dbo].[QueryParameterValue]([Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[QueryParameterId],[Description])VALUES('*',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,@intParamId,'*')
INSERT INTO [dbo].[QueryParameterValue]([Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[QueryParameterId],[Description])VALUES('Pavilion (sub)',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,@intParamId,'Pavilion')
INSERT INTO [dbo].[QueryParameterValue]([Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[QueryParameterId],[Description])VALUES('Presario (sub)',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,@intParamId,'Presario')

SELECT @intParamId = Id FROM [dbo].[QueryParameter] WHERE name = 'Release Start'
INSERT INTO [dbo].[QueryParameterValue]([Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[QueryParameterId],[Description])VALUES('*',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,@intParamId,NULL)

SELECT @intParamId = Id FROM [dbo].[QueryParameter] WHERE name = 'Release End'
INSERT INTO [dbo].[QueryParameterValue]([Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[QueryParameterId],[Description])VALUES('*',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,@intParamId,NULL)

SELECT @intParamId = Id FROM [dbo].[QueryParameter] WHERE name = 'Model Number'
INSERT INTO [dbo].[QueryParameterValue]([Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[QueryParameterId],[Description])VALUES('*',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,@intParamId,NULL)

SELECT @intParamId = Id FROM [dbo].[QueryParameter] WHERE name = 'Product Name'
INSERT INTO [dbo].[QueryParameterValue]([Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[QueryParameterId],[Description])VALUES('*',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,@intParamId,NULL)

SELECT @intParamId = Id FROM [dbo].[QueryParameter] WHERE name = 'Product Number'
INSERT INTO [dbo].[QueryParameterValue]([Name],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn],[RowStatusId],[QueryParameterId],[Description])VALUES('*',@strDefaultValue_CreatedBy, getdate(), @strDefaultValue_ModifiedBy,getdate(),@intRowStatus_Active,@intParamId,NULL)
END	    

PRINT 'Finished loading "Workflow" DB data'

COMMIT TRANSACTION
END TRY
BEGIN CATCH
	---Capture the details of the original error
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY()

	PRINT 'Error occurred while loading "Representative" DB data'
	ROLLBACK TRANSACTION

	---Raise an error with the details of the original error
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH
GO

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
GO
