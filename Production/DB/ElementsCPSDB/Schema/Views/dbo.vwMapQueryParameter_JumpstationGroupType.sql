
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapQueryParameter_JumpstationGroupType
AS
SELECT     dbo.QueryParameter_JumpstationGroupType.Id, dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameter.RowStatusId, 
                      dbo.RowStatus.Name AS RowStatusName, dbo.JumpstationGroupType.Name, dbo.QueryParameter_JumpstationGroupType.CreatedBy, 
                      dbo.QueryParameter_JumpstationGroupType.CreatedOn, dbo.QueryParameter_JumpstationGroupType.ModifiedBy, 
                      dbo.QueryParameter_JumpstationGroupType.ModifiedOn, dbo.QueryParameter_JumpstationGroupType.QueryParameterId, 
                      dbo.QueryParameter_JumpstationGroupType.JumpstationGroupTypeId, 
                      dbo.QueryParameter_JumpstationGroupType.Name AS QueryParameterJumpstationGroupTypeName, dbo.QueryParameter_JumpstationGroupType.Description, 
                      dbo.QueryParameter_JumpstationGroupType.MaximumSelection, dbo.QueryParameter_JumpstationGroupType.Wildcard, 
                      dbo.QueryParameter_JumpstationGroupType.SortOrder
FROM         dbo.QueryParameter_JumpstationGroupType WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) ON dbo.QueryParameter_JumpstationGroupType.QueryParameterId = dbo.QueryParameter.Id INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.QueryParameter.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.JumpstationGroupType ON dbo.QueryParameter_JumpstationGroupType.JumpstationGroupTypeId = dbo.JumpstationGroupType.Id
GO
