SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapProxyURLTag_Tag
AS
SELECT     dbo.ProxyURL_Tag.ProxyURLId, dbo.ProxyURL_Tag.TagId, dbo.ProxyURL_Tag.CreatedBy, dbo.ProxyURL_Tag.CreatedOn, 
                      dbo.ProxyURL_Tag.ModifiedBy, dbo.ProxyURL_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.ProxyURL_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.ProxyURL_Tag.TagId = dbo.Tag.Id
GO
