
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapProxyURLDomain
AS
SELECT     dbo.ProxyURLDomain.Id, dbo.ProxyURLDomain.Name, dbo.ProxyURLDomain.CreatedBy, dbo.ProxyURLDomain.CreatedOn, dbo.ProxyURLDomain.ModifiedBy, dbo.ProxyURLDomain.ModifiedOn, 
                      dbo.ProxyURLDomain.RowStatusId, dbo.RowStatus.Name AS RowStatusName, dbo.ProxyURLDomain.ValidationDomain, dbo.ProxyURLDomain.ProductionDomain
FROM         dbo.ProxyURLDomain WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ProxyURLDomain.RowStatusId = dbo.RowStatus.Id
GO
