
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceItem_ConfigurationServiceGroupType
AS
SELECT     dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.Id, dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.CreatedBy, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.CreatedOn, dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ModifiedBy, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ModifiedOn, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceItemId, 
                      dbo.ConfigurationServiceGroupType.Name AS ConfigurationServiceGroupTypeName, 
                      dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName, dbo.RowStatus.Name AS RowStatusName
FROM         dbo.ConfigurationServiceGroupType WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroupType.Id = dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceItem.RowStatusId = dbo.RowStatus.Id
GO
