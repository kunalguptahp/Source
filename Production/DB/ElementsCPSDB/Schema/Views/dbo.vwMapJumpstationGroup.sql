
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapJumpstationGroup
AS
SELECT     dbo.JumpstationGroup.Id, dbo.RowStatus.Name AS RowStatusName, dbo.JumpstationGroupStatus.Name AS JumpstationGroupStatusName, 
                      dbo.Person.Name AS PersonName, dbo.JumpstationGroup.CreatedBy, dbo.JumpstationGroup.CreatedOn, dbo.JumpstationGroup.ModifiedBy, 
                      dbo.JumpstationGroup.ModifiedOn, dbo.JumpstationGroup.RowStatusId, dbo.JumpstationGroup.Description, dbo.JumpstationGroup.JumpstationGroupStatusId, 
                      dbo.JumpstationGroup.JumpstationGroupTypeId, dbo.JumpstationGroup.OwnerId, dbo.JumpstationGroupType.Name AS JumpstationGroupTypeName, 
                      dbo.JumpstationGroupPivot.Brand AS BrandQueryParameterValue,
                      dbo.JumpstationGroupPivot.Cycle AS CycleQueryParameterValue,
                      dbo.JumpstationGroupPivot.Locale AS LocaleQueryParameterValue,
                      dbo.JumpstationGroupPivot.Touchpoint AS TouchpointQueryParameterValue,
                      dbo.JumpstationGroupPivot.PartnerCategory AS PartnerCategoryQueryParameterValue,
                      dbo.JumpstationGroupPivot.Platform AS PlatformQueryParameterValue,
                      dbo.JumpstationGroup_GetTagList(dbo.JumpstationGroup.Id, ', ', 1) AS Tags, dbo.JumpstationGroup_GenerateQueryString(dbo.JumpstationGroup.Id) AS QueryString,
                          (SELECT     COUNT(TagId) AS Expr1
                            FROM          dbo.JumpstationGroup_Tag WITH (NOLOCK)
                            WHERE      (dbo.JumpstationGroup.Id = JumpstationGroupId)) AS TagCount, dbo.JumpstationGroup.ProductionId, dbo.JumpstationGroup.ValidationId, 
                      dbo.JumpstationGroup.Name, dbo.JumpstationGroup.JumpstationApplicationId, dbo.JumpstationApplication.Name AS JumpstationApplicationName, 
                      dbo.JumpstationGroup.TargetURL, dbo.JumpstationGroup.[Order], dbo.JumpstationDomain.DomainURL AS PublicationDomain
FROM         dbo.JumpstationGroup WITH (NOLOCK) INNER JOIN
					  dbo.JumpstationGroupPivot WITH (NOLOCK) ON dbo.JumpstationGroup.Id = dbo.JumpstationGroupPivot.JumpstationGroupId INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationGroup.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.JumpstationGroup.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.JumpstationGroupStatus WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupStatusId = dbo.JumpstationGroupStatus.Id INNER JOIN
                      dbo.JumpstationGroupType WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id INNER JOIN
                      dbo.JumpstationApplication WITH (NOLOCK) ON dbo.JumpstationGroup.JumpstationApplicationId = dbo.JumpstationApplication.Id INNER JOIN
                      dbo.JumpstationDomain ON dbo.JumpstationGroupType.PublicationJumpstationDomainId = dbo.JumpstationDomain.Id
GO
