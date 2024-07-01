
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceLabelValueImport
AS
SELECT     dbo.ConfigurationServiceLabelValueImport.LabelName, dbo.ConfigurationServiceLabelValueImport.ItemName, dbo.ConfigurationServiceLabelValueImport.LabelValue, 
                      dbo.ConfigurationServiceLabelValueImport.ConfigurationServiceGroupImportId, dbo.RowStatus.Name, dbo.ConfigurationServiceLabelValueImport.Id, 
                      dbo.ConfigurationServiceLabelValueImport.CreatedBy, dbo.ConfigurationServiceLabelValueImport.CreatedOn, dbo.ConfigurationServiceLabelValueImport.ModifiedBy, 
                      dbo.ConfigurationServiceLabelValueImport.ModifiedOn, dbo.ConfigurationServiceLabelValueImport.RowStatusId, 
                      dbo.ConfigurationServiceGroupImport.Name AS ConfigurationServiceGroupImportName
FROM         dbo.ConfigurationServiceLabelValueImport WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceLabelValueImport.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupImport WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabelValueImport.ConfigurationServiceGroupImportId = dbo.ConfigurationServiceGroupImport.Id
GO
