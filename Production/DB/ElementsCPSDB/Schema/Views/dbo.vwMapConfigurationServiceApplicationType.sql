SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceApplicationType
AS
SELECT     dbo.ConfigurationServiceApplicationType.Id, dbo.ConfigurationServiceApplicationType.Name, dbo.ConfigurationServiceApplicationType.CreatedBy, 
                      dbo.ConfigurationServiceApplicationType.CreatedOn, dbo.ConfigurationServiceApplicationType.ModifiedBy, 
                      dbo.ConfigurationServiceApplicationType.ModifiedOn, dbo.ConfigurationServiceApplicationType.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName
FROM         dbo.ConfigurationServiceApplicationType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceApplicationType.RowStatusId = dbo.RowStatus.Id
GO
