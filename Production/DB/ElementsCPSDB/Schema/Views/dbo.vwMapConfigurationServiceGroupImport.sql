
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroupImport
AS
SELECT     dbo.ConfigurationServiceGroupImport.Id, dbo.ConfigurationServiceGroupImport.CreatedBy, dbo.ConfigurationServiceGroupImport.CreatedOn, 
                      dbo.ConfigurationServiceGroupImport.ModifiedBy, dbo.ConfigurationServiceGroupImport.ModifiedOn, dbo.ConfigurationServiceGroupImport.Name, 
                      dbo.ConfigurationServiceGroupImport.Description, dbo.ConfigurationServiceGroupImport.ConfigurationServiceGroupTypeName, 
                      dbo.ConfigurationServiceGroupImport.ConfigurationServiceApplicationName, dbo.ConfigurationServiceGroupImport.ProductionId, 
                      dbo.ConfigurationServiceGroupImport.ImportMessage, dbo.ConfigurationServiceGroupImport.ImportStatus, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceGroupImport.RowStatusId, dbo.ConfigurationServiceGroupImport.ConfigurationServiceGroupId, 
                      dbo.ConfigurationServiceGroupImport_GetLabelValueList(dbo.ConfigurationServiceGroupImport.Id, ', ', NULL) AS LabelValue
FROM         dbo.ConfigurationServiceGroupImport WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroupImport.RowStatusId = dbo.RowStatus.Id
GO
