
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapQueryParameter_ProxyURLType
AS
SELECT     dbo.QueryParameter_ProxyURLType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.ProxyURLType.Name, dbo.QueryParameter_ProxyURLType.CreatedBy, 
                      dbo.QueryParameter_ProxyURLType.CreatedOn, dbo.QueryParameter_ProxyURLType.ModifiedBy, dbo.QueryParameter_ProxyURLType.ModifiedOn, 
                      dbo.QueryParameter_ProxyURLType.QueryParameterId, dbo.QueryParameter_ProxyURLType.ProxyURLTypeId, 
                      dbo.QueryParameter_ProxyURLType.Name AS QueryParameterProxyURLTypeName, dbo.QueryParameter_ProxyURLType.Description
FROM         dbo.QueryParameter_ProxyURLType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_ProxyURLType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.ProxyURLType WITH (NOLOCK) ON dbo.QueryParameter_ProxyURLType.ProxyURLTypeId = dbo.ProxyURLType.Id
GO
