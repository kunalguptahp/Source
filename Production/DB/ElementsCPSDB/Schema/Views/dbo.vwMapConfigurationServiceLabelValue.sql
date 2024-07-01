
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceLabelValue
AS
SELECT     dbo.ConfigurationServiceLabelValue.Id, dbo.ConfigurationServiceLabelValue.CreatedBy, dbo.ConfigurationServiceLabelValue.CreatedOn, 
                      dbo.ConfigurationServiceLabelValue.ModifiedBy, dbo.ConfigurationServiceLabelValue.ModifiedOn, dbo.ConfigurationServiceLabelValue.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceLabelValue.ConfigurationServiceGroupId, 
                      dbo.ConfigurationServiceLabel.Id AS ConfigurationServiceLabelId, dbo.ConfigurationServiceLabel.Name AS ConfigurationServiceLabelName, 
                      dbo.ConfigurationServiceLabelValue.Value, dbo.ConfigurationServiceLabel.ConfigurationServiceLabelTypeId, 
                      dbo.ConfigurationServiceItem.Name AS ConfigurationServiceItemName
FROM         dbo.ConfigurationServiceLabelValue WITH (NOLOCK) INNER JOIN
                      dbo.ConfigurationServiceLabel WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceLabel.Id = dbo.ConfigurationServiceLabelValue.ConfigurationServiceLabelId INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceLabelValue.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceItem WITH (NOLOCK) ON dbo.ConfigurationServiceLabel.ConfigurationServiceItemId = dbo.ConfigurationServiceItem.Id
GO
