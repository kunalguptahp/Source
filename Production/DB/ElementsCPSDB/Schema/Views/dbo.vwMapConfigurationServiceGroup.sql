


SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroup
AS
SELECT     dbo.ConfigurationServiceGroup.Id, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.ConfigurationServiceGroupStatus.Name AS ConfigurationServiceGroupStatusName, dbo.Person.Name AS PersonName, 
                      dbo.ConfigurationServiceGroup.CreatedBy, dbo.ConfigurationServiceGroup.CreatedOn, dbo.ConfigurationServiceGroup.ModifiedBy, 
                      dbo.ConfigurationServiceGroup.ModifiedOn, dbo.ConfigurationServiceGroup.RowStatusId, dbo.ConfigurationServiceGroup.Description, 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupStatusId, dbo.ConfigurationServiceGroup.ConfigurationServiceGroupTypeId, 
                      dbo.ConfigurationServiceGroup.OwnerId, dbo.ConfigurationServiceGroupType.Name AS ConfigurationServiceGroupTypeName, 
                      dbo.ConfigurationServiceGroup_GetQueryParameterValueList(dbo.ConfigurationServiceGroup.Id, 102, ', ', NULL) AS ReleaseQueryParameterValue, 
                      dbo.ConfigurationServiceGroup_GetQueryParameterValueList(dbo.ConfigurationServiceGroup.Id, 101, ', ', NULL) AS CountryQueryParameterValue, 
                      dbo.ConfigurationServiceGroup_GetQueryParameterValueList(dbo.ConfigurationServiceGroup.Id, 107, ', ', NULL) AS PlatformQueryParameterValue, 
                      dbo.ConfigurationServiceGroup_GetQueryParameterValueList(dbo.ConfigurationServiceGroup.Id, 106, ', ', NULL) AS BrandQueryParameterValue, 
                      PublisherLabelValue.Value AS PublisherLabelValue, InstallerTypeLabelValue.Value AS InstallerTypeLabelValue, 
                      dbo.ConfigurationServiceGroup_GetTagList(dbo.ConfigurationServiceGroup.Id, ', ', 1) AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.ConfigurationServiceGroup_Tag WITH (NOLOCK)
                            WHERE      (dbo.ConfigurationServiceGroup.Id = ConfigurationServiceGroupId)) AS TagCount, dbo.ConfigurationServiceGroup.ProductionId, 
                      dbo.ConfigurationServiceGroup.ValidationId, dbo.ConfigurationServiceGroup.Name, dbo.ConfigurationServiceGroup.ConfigurationServiceApplicationId, 
                      dbo.ConfigurationServiceApplication.Name AS ConfigurationServiceApplicationName
FROM         dbo.ConfigurationServiceGroup WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ConfigurationServiceGroup.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.ConfigurationServiceGroup.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.ConfigurationServiceGroupStatus WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupStatusId = dbo.ConfigurationServiceGroupStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceGroupTypeId = dbo.ConfigurationServiceGroupType.Id INNER JOIN
                      dbo.ConfigurationServiceApplication WITH (NOLOCK) ON 
                      dbo.ConfigurationServiceGroup.ConfigurationServiceApplicationId = dbo.ConfigurationServiceApplication.Id LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupId, Value
                            FROM          dbo.ConfigurationServiceLabelValue WITH (NOLOCK)
                            WHERE      (ConfigurationServiceLabelId = 7)) AS PublisherLabelValue ON 
                      dbo.ConfigurationServiceGroup.Id = PublisherLabelValue.ConfigurationServiceGroupId LEFT OUTER JOIN
                          (SELECT     ConfigurationServiceGroupId, Value
                            FROM          dbo.ConfigurationServiceLabelValue AS ConfigurationServiceLabelValue_1 WITH (NOLOCK)
                            WHERE      (ConfigurationServiceLabelId = 8)) AS InstallerTypeLabelValue ON 
                      dbo.ConfigurationServiceGroup.Id = InstallerTypeLabelValue.ConfigurationServiceGroupId
GO
