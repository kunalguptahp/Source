
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceItem
AS
SELECT     dbo.ConfigurationServiceItem.Id, dbo.ConfigurationServiceItem.Name, dbo.ConfigurationServiceItem.CreatedBy, 
                      dbo.ConfigurationServiceItem.CreatedOn, dbo.ConfigurationServiceItem.ModifiedBy, dbo.ConfigurationServiceItem.ModifiedOn, 
                      dbo.ConfigurationServiceItem.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceItem.ElementsKey, 
                      dbo.ConfigurationServiceItem.SortOrder, dbo.ConfigurationServiceItem.Parent
FROM         dbo.ConfigurationServiceItem WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceItem.RowStatusId = dbo.RowStatus.Id
GO
