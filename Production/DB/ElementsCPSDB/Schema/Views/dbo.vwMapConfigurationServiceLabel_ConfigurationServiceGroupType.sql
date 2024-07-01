
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceLabel_ConfigurationServiceGroupType
AS
SELECT     TOP (100) PERCENT dbo.ConfigurationServiceLabel.Id, dbo.ConfigurationServiceLabel.Name, dbo.ConfigurationServiceLabel.CreatedBy, 
                      dbo.ConfigurationServiceLabel.CreatedOn, dbo.ConfigurationServiceLabel.ModifiedBy, dbo.ConfigurationServiceLabel.ModifiedOn, 
                      dbo.ConfigurationServiceLabel.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceLabel.ElementsKey, 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceItemId, dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName, 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId, 
                      dbo.ConfigurationServiceGroupType.Name AS ConfigurationServiceGroupTypeName, dbo.ConfigurationServiceLabel.ConfigurationServiceLabelTypeId, 
                      dbo.ConfigurationServiceLabel.ValueList, dbo.ConfigurationServiceLabel.Help, dbo.ConfigurationServiceLabel.Description, 
                      dbo.ConfigurationServiceLabelType.Name AS ConfigurationServiceLabelTypeName, dbo.ConfigurationServiceLabel.InputRequired, 
                      dbo.ConfigurationServiceItem.SortOrder AS ConfigurationServiceItemSortOrder, dbo.ConfigurationServiceLabel.SortOrder, 
                      dbo.ConfigurationServiceItem.ElementsKey AS ConfigurationServiceItemElementsKey
FROM         dbo.ConfigurationServiceLabel WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON dbo.ConfigurationServiceLabel.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceLabel.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceItem.Id = dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceItemId INNER JOIN
                      dbo.ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceItem_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId = dbo.ConfigurationServiceGroupType.Id INNER JOIN
                      dbo.ConfigurationServiceLabelType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabel.ConfigurationServiceLabelTypeId = dbo.ConfigurationServiceLabelType.Id
ORDER BY ConfigurationServiceItemName, dbo.ConfigurationServiceLabel.Name
GO
