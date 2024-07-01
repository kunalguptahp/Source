
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapJumpstationGroupType
AS
SELECT     dbo.JumpstationGroupType.Id, dbo.JumpstationGroupType.Name, dbo.JumpstationGroupType.CreatedBy, dbo.JumpstationGroupType.CreatedOn, 
                      dbo.JumpstationGroupType.ModifiedBy, dbo.JumpstationGroupType.ModifiedOn, dbo.JumpstationGroupType.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationApplication.Name AS JumpstationApplicationName, dbo.JumpstationGroupType.JumpstationApplicationId
FROM         dbo.JumpstationGroupType WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationGroupType.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.JumpstationApplication ON dbo.JumpstationGroupType.JumpstationApplicationId = dbo.JumpstationApplication.Id
GO
