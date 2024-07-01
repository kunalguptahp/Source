
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapJumpstationMacro
AS
SELECT     dbo.JumpstationMacro.Id, dbo.RowStatus.Name AS RowStatusName, dbo.Person.Name AS PersonName, dbo.JumpstationMacro.CreatedBy, 
                      dbo.JumpstationMacro.CreatedOn, dbo.JumpstationMacro.ModifiedBy, dbo.JumpstationMacro.ModifiedOn, dbo.JumpstationMacro.RowStatusId, 
                      dbo.JumpstationMacro.Name, dbo.JumpstationMacro.Description, dbo.JumpstationMacro.JumpstationMacroStatusId, 
                      dbo.JumpstationMacroStatus.Name AS JumpstationMacroStatusName, dbo.JumpstationMacro.OwnerId, dbo.JumpstationMacro.ProductionId, 
                      dbo.JumpstationMacro.ValidationId
FROM         dbo.JumpstationMacro WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationMacro.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person WITH (NOLOCK) ON dbo.JumpstationMacro.OwnerId = dbo.Person.Id INNER JOIN
                      dbo.JumpstationMacroStatus WITH (NOLOCK) ON dbo.JumpstationMacro.JumpstationMacroStatusId = dbo.JumpstationMacroStatus.Id
GO
