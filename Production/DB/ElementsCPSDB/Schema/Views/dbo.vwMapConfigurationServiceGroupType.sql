
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroupType
AS
SELECT     dbo.ConfigurationServiceGroupType.Id, dbo.ConfigurationServiceGroupType.Name, dbo.ConfigurationServiceGroupType.CreatedBy, 
                      dbo.ConfigurationServiceGroupType.CreatedOn, dbo.ConfigurationServiceGroupType.ModifiedBy, dbo.ConfigurationServiceGroupType.ModifiedOn, 
                      dbo.ConfigurationServiceGroupType.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceApplication.Name AS ConfigurationServiceApplicationName, dbo.ConfigurationServiceGroupType.ConfigurationServiceApplicationId, 
                      dbo.ConfigurationServiceApplication.ElementsKey
FROM         dbo.ConfigurationServiceGroupType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroupType.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceApplication WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroupType.ConfigurationServiceApplicationId = dbo.ConfigurationServiceApplication.Id
GO
