
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapProxyURL_QueryParameterValue
AS
SELECT     dbo.ProxyURL_QueryParameterValue.Id, dbo.ProxyURL_QueryParameterValue.QueryParameterValueId, dbo.ProxyURL_QueryParameterValue.ProxyURLId, 
                      dbo.ProxyURL_QueryParameterValue.ModifiedOn, dbo.ProxyURL_QueryParameterValue.ModifiedBy, dbo.ProxyURL_QueryParameterValue.CreatedOn, 
                      dbo.ProxyURL_QueryParameterValue.CreatedBy, dbo.QueryParameterValue.Name AS QueryParameterValueName, dbo.QueryParameterValue.QueryParameterId, 
                      dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, dbo.QueryParameter.ElementsKey
FROM         dbo.QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.ProxyURL_QueryParameterValue WITH (NOLOCK) ON dbo.QueryParameterValue.Id = dbo.ProxyURL_QueryParameterValue.QueryParameterValueId INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameterValue.QueryParameterId = dbo.QueryParameter.Id
GO
