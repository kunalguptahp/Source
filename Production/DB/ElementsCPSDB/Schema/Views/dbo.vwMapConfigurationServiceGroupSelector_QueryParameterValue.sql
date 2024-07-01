
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapConfigurationServiceGroupSelector_QueryParameterValue
AS
SELECT     dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameterValue.Name AS QueryParameterValueName, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.Negation, dbo.QueryParameter.Id AS QueryParameterId, 
                      dbo.QueryParameterValue.Id AS QueryParameterValueId, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.Id AS ConfigurationServiceGroupSelectorQueryParameterValueId, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ModifiedOn, dbo.ConfigurationServiceGroupSelector_QueryParameterValue.ModifiedBy, 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.CreatedOn, dbo.ConfigurationServiceGroupSelector_QueryParameterValue.CreatedBy
FROM         dbo.ConfigurationServiceGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameterValue WITH (NOLOCK) ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId ON 
                      dbo.ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = dbo.QueryParameterValue.Id
GO
