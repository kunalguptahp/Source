SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapJumpstationDomain
AS
SELECT     dbo.JumpstationDomain.Id, dbo.JumpstationDomain.Name, dbo.JumpstationDomain.CreatedBy, dbo.JumpstationDomain.CreatedOn, 
                      dbo.JumpstationDomain.ModifiedBy, dbo.JumpstationDomain.ModifiedOn, dbo.JumpstationDomain.RowStatusId, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.JumpstationDomain.DomainURL
FROM         dbo.JumpstationDomain WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.JumpstationDomain.RowStatusId = dbo.RowStatus.Id
GO
