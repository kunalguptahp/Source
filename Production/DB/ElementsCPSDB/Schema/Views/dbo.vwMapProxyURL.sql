
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapProxyURL
AS
SELECT     dbo.ProxyURL.Id, dbo.RowStatus.Name AS RowStatusName, dbo.ProxyURLStatus.Name AS ProxyURLStatusName, dbo.Person.Name AS PersonName,
                       dbo.ProxyURL.CreatedBy, dbo.ProxyURL.CreatedOn, dbo.ProxyURL.ModifiedBy, dbo.ProxyURL.ModifiedOn, dbo.ProxyURL.RowStatusId, 
                      dbo.ProxyURL.Description, dbo.ProxyURL.ProxyURLStatusId, dbo.ProxyURL.ProxyURLTypeId, dbo.ProxyURL.OwnerId, dbo.ProxyURL.URL, 
                      dbo.ProxyURLType.Name AS ProxyURLTypeName, dbo.ProxyURLType.ElementsKey AS ProxyURLTypeElementsKey, 
                      TouchpointParameterValue.QueryParameterValueId AS TouchpointParameterValueQueryParameterValueId, 
                      TouchpointParameterValue.QueryParameterId AS TouchpointParameterValueQueryParameterId, 
                      TouchpointParameterValue.Name AS TouchpointParameterValueName, 
                      BrandParameterValue.QueryParameterValueId AS BrandParameterValueQueryParameterValueId, 
                      BrandParameterValue.QueryParameterId AS BrandParameterValueQueryParameterId, BrandParameterValue.Name AS BrandParameterValueName, 
                      LocaleParameterValue.QueryParameterValueId AS LocaleParameterValueQueryParameterValueId, 
                      LocaleParameterValue.QueryParameterId AS LocaleParameterValueQueryParameterId, LocaleParameterValue.Name AS LocaleParameterValueName, 
                      CycleParameterValue.QueryParameterValueId AS CycleParameterValueQueryParameterValueId, 
                      CycleParameterValue.QueryParameterId AS CycleParameterValueQueryParameterId, CycleParameterValue.Name AS CycleParameterValueName, 
                      PlatformParameterValue.QueryParameterValueId AS PlatformParameterValueQueryParameterValueId, 
                      PlatformParameterValue.QueryParameterId AS PlatformParameterValueQueryParameterId, 
                      PlatformParameterValue.Name AS PlatformParameterValueName, 
                      PartnerCategoryParameterValue.QueryParameterValueId AS PartnerCategoryParameterValueQueryParameterValueId, 
                      PartnerCategoryParameterValue.QueryParameterId AS PartnerCategoryParameterValueQueryParameterId, 
                      PartnerCategoryParameterValue.Name AS PartnerCategoryParameterValueName, dbo.ProxyURL_GetTagList(dbo.ProxyURL.Id, ', ', 1) AS Tags,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.ProxyURL_Tag WITH (NOLOCK)
                            WHERE      (dbo.ProxyURL.Id = ProxyURLId)) AS TagCount, dbo.ProxyURL.ProductionId, dbo.ProxyURL.ValidationId, 
                      dbo.ProxyURL_GenerateQueryString(dbo.ProxyURL.Id) AS QueryString, dbo.ProxyURLDomain.ProductionDomain, dbo.ProxyURLDomain.ValidationDomain
FROM         dbo.ProxyURL WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.ProxyURL.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.ProxyURL.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.ProxyURLStatus WITH (NOLOCK) ON dbo.ProxyURL.ProxyURLStatusId = dbo.ProxyURLStatus.Id INNER JOIN
                      dbo.ProxyURLType WITH (NOLOCK) ON dbo.ProxyURL.ProxyURLTypeId = dbo.ProxyURLType.Id INNER JOIN
                      dbo.ProxyURLDomain WITH (NOLOCK) ON dbo.ProxyURLType.ProxyURLDomainId = dbo.ProxyURLDomain.Id LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 13)) AS TouchpointParameterValue ON 
                      dbo.ProxyURL.Id = TouchpointParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 15)) AS BrandParameterValue ON 
                      dbo.ProxyURL.Id = BrandParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 3)) AS LocaleParameterValue ON 
                      dbo.ProxyURL.Id = LocaleParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 7)) AS CycleParameterValue ON 
                      dbo.ProxyURL.Id = CycleParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 14)) AS PlatformParameterValue ON 
                      dbo.ProxyURL.Id = PlatformParameterValue.ProxyURLId LEFT OUTER JOIN
                          (SELECT     ProxyURL_QueryParameterValue.ProxyURLId, ProxyURL_QueryParameterValue.QueryParameterValueId, 
                                                   QueryParameterValue.QueryParameterId, QueryParameterValue.Name
                            FROM          dbo.ProxyURL_QueryParameterValue AS ProxyURL_QueryParameterValue WITH (NOLOCK) INNER JOIN
                                                   dbo.QueryParameterValue AS QueryParameterValue WITH (NOLOCK) ON 
                                                   ProxyURL_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id
                            WHERE      (QueryParameterValue.QueryParameterId = 5)) AS PartnerCategoryParameterValue ON 
                      dbo.ProxyURL.Id = PartnerCategoryParameterValue.ProxyURLId
GO
