
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceApplication
AS
SELECT     dbo.ConfigurationServiceApplication.Id, dbo.ConfigurationServiceApplication.Name, dbo.ConfigurationServiceApplication.CreatedBy, 
                      dbo.ConfigurationServiceApplication.CreatedOn, dbo.ConfigurationServiceApplication.ModifiedBy, dbo.ConfigurationServiceApplication.ModifiedOn, 
                      dbo.ConfigurationServiceApplication.Version, dbo.ConfigurationServiceApplication.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceApplication.ElementsKey, dbo.ConfigurationServiceApplicationType.Name AS ConfigurationServiceApplicationTypeName
FROM         dbo.ConfigurationServiceApplication WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceApplication.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceApplicationType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceApplication.ConfigurationServiceApplicationTypeId = dbo.ConfigurationServiceApplicationType.Id
GO
