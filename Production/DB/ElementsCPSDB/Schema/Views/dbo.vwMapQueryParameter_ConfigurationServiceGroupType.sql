
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapQueryParameter_ConfigurationServiceGroupType
AS
SELECT     dbo.QueryParameter_ConfigurationServiceGroupType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.ConfigurationServiceGroupType.Name, dbo.QueryParameter_ConfigurationServiceGroupType.CreatedBy, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.CreatedOn, dbo.QueryParameter_ConfigurationServiceGroupType.ModifiedBy, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.ModifiedOn, dbo.QueryParameter_ConfigurationServiceGroupType.QueryParameterId, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.Name AS QueryParameterConfigurationServiceGroupTypeName, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.Description, dbo.QueryParameter_ConfigurationServiceGroupType.MaximumSelection, 
                      dbo.QueryParameter_ConfigurationServiceGroupType.Wildcard, dbo.QueryParameter.ElementsKey
FROM         dbo.QueryParameter_ConfigurationServiceGroupType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_ConfigurationServiceGroupType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ConfigurationServiceGroupType WITH (NOLOCK) ON 
                      dbo.QueryParameter_ConfigurationServiceGroupType.ConfigurationServiceGroupTypeId = dbo.ConfigurationServiceGroupType.Id
GO
