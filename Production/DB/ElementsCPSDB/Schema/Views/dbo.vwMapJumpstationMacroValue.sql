SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapJumpstationMacroValue
AS
SELECT     dbo.JumpstationMacroValue.Id, dbo.JumpstationMacroValue.CreatedBy, dbo.JumpstationMacroValue.CreatedOn, dbo.JumpstationMacroValue.ModifiedBy, 
                      dbo.JumpstationMacroValue.ModifiedOn, dbo.JumpstationMacroValue.RowStatusId, dbo.JumpstationMacroValue.JumpstationMacroId, 
                      dbo.JumpstationMacroValue.MatchName, dbo.JumpstationMacroValue.ResultValue, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationMacro.Name AS JumpstationMacroName
FROM         dbo.JumpstationMacro INNER JOIN
                      dbo.JumpstationMacroValue ON dbo.JumpstationMacro.Id = dbo.JumpstationMacroValue.JumpstationMacroId INNER JOIN
                      dbo.RowStatus ON dbo.JumpstationMacroValue.RowStatusId = dbo.RowStatus.Id
GO
