

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapJumpstationApplication
AS
SELECT     dbo.JumpstationApplication.Id, dbo.JumpstationApplication.Name, dbo.JumpstationApplication.CreatedBy, 
                      dbo.JumpstationApplication.CreatedOn, dbo.JumpstationApplication.ModifiedBy, dbo.JumpstationApplication.ModifiedOn, 
                      dbo.JumpstationApplication.Version, dbo.JumpstationApplication.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationApplication.ElementsKey
FROM         dbo.JumpstationApplication WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationApplication.RowStatusId = dbo.RowStatus.Id
GO
