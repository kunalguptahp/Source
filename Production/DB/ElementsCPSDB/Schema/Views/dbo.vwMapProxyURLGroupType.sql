SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE VIEW [dbo].[vwMapProxyURLGroupType]
AS
SELECT     dbo.ProxyURLGroupType.Id, dbo.ProxyURLGroupType.Name, dbo.ProxyURLGroupType.CreatedBy, dbo.ProxyURLGroupType.CreatedOn, dbo.ProxyURLGroupType.ModifiedBy, dbo.ProxyURLGroupType.ModifiedOn, 
                      dbo.ProxyURLGroupType.RowStatusId, dbo.RowStatus.Name AS RowStatusName
FROM         dbo.ProxyURLGroupType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ProxyURLGroupType.RowStatusId = dbo.RowStatus.Id

GO
