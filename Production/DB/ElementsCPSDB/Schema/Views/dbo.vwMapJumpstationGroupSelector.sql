

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapJumpstationGroupSelector
AS
SELECT     dbo.JumpstationGroupSelector.Id, dbo.JumpstationGroupSelector.Name, dbo.JumpstationGroupSelector.CreatedBy, 
                      dbo.JumpstationGroupSelector.CreatedOn, dbo.JumpstationGroupSelector.ModifiedBy, 
                      dbo.JumpstationGroupSelector.ModifiedOn, dbo.JumpstationGroupSelector.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationGroupSelector.JumpstationGroupId
FROM         dbo.JumpstationGroupSelector WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationGroupSelector.RowStatusId = dbo.RowStatus.Id
GO
