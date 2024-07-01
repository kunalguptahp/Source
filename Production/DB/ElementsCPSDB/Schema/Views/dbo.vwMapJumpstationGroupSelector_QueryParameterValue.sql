
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapJumpstationGroupSelector_QueryParameterValue
AS
SELECT     dbo.QueryParameter.Name AS QueryParameterName, dbo.QueryParameterValue.Name AS QueryParameterValueName, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.Negation, dbo.QueryParameter.Id AS QueryParameterId, 
                      dbo.QueryParameterValue.Id AS QueryParameterValueId, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.Id AS JumpstationGroupSelectorQueryParameterValueId, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.ModifiedOn, dbo.JumpstationGroupSelector_QueryParameterValue.ModifiedBy, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.CreatedOn, 
                      dbo.JumpstationGroupSelector_QueryParameterValue.CreatedBy
FROM         dbo.JumpstationGroupSelector_QueryParameterValue WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameter WITH (NOLOCK) INNER JOIN
                      dbo.QueryParameterValue ON dbo.QueryParameter.Id = dbo.QueryParameterValue.QueryParameterId ON 
                      dbo.JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = dbo.QueryParameterValue.Id
GO
