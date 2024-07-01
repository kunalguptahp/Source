
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceLabel
AS
SELECT     dbo.ConfigurationServiceLabel.Id, dbo.ConfigurationServiceLabel.Name, dbo.ConfigurationServiceLabel.CreatedBy, 
                      dbo.ConfigurationServiceLabel.CreatedOn, dbo.ConfigurationServiceLabel.ModifiedBy, dbo.ConfigurationServiceLabel.ModifiedOn, 
                      dbo.ConfigurationServiceLabel.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceLabel.ElementsKey, 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceItemId, dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName, 
                      dbo.ConfigurationServiceLabel.SortOrder
FROM         dbo.ConfigurationServiceLabel WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceLabel.RowStatusId = dbo.RowStatus.Id
GO
