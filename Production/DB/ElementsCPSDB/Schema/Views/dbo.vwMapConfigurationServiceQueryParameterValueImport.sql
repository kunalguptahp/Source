
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceQueryParameterValueImport
AS
SELECT     dbo.ConfigurationServiceQueryParameterValueImport.Id, dbo.ConfigurationServiceQueryParameterValueImport.CreatedBy, 
                      dbo.ConfigurationServiceQueryParameterValueImport.CreatedOn, dbo.ConfigurationServiceQueryParameterValueImport.ModifiedBy, 
                      dbo.ConfigurationServiceQueryParameterValueImport.RowStatusId, dbo.ConfigurationServiceQueryParameterValueImport.ModifiedOn, 
                      dbo.ConfigurationServiceQueryParameterValueImport.QueryParameterName, dbo.ConfigurationServiceQueryParameterValueImport.QueryParameterValue, 
                      dbo.ConfigurationServiceQueryParameterValueImport.ConfigurationServiceGroupImportId, dbo.RowStatus.Name, 
                      dbo.ConfigurationServiceGroupImport.Name AS ConfigurationServiceGroupImportName
FROM         dbo.ConfigurationServiceQueryParameterValueImport WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceQueryParameterValueImport.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupImport WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceQueryParameterValueImport.ConfigurationServiceGroupImportId = dbo.ConfigurationServiceGroupImport.Id
GO
