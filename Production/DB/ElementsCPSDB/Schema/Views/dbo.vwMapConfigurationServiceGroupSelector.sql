

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroupSelector
AS
SELECT     dbo.ConfigurationServiceGroupSelector.Id, dbo.ConfigurationServiceGroupSelector.Name, dbo.ConfigurationServiceGroupSelector.CreatedBy, 
                      dbo.ConfigurationServiceGroupSelector.CreatedOn, dbo.ConfigurationServiceGroupSelector.ModifiedBy, 
                      dbo.ConfigurationServiceGroupSelector.ModifiedOn, dbo.ConfigurationServiceGroupSelector.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceGroupSelector.ConfigurationServiceGroupId
FROM         dbo.ConfigurationServiceGroupSelector WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroupSelector.RowStatusId = dbo.RowStatus.Id
GO
