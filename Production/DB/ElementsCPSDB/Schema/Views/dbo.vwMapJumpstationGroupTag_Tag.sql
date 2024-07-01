SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE VIEW [dbo].[vwMapJumpstationGroupTag_Tag]
AS
SELECT     dbo.JumpstationGroup_Tag.JumpstationGroupId, dbo.JumpstationGroup_Tag.TagId, dbo.JumpstationGroup_Tag.CreatedBy, dbo.JumpstationGroup_Tag.CreatedOn, 
                      dbo.JumpstationGroup_Tag.ModifiedBy, dbo.JumpstationGroup_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.JumpstationGroup_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.JumpstationGroup_Tag.TagId = dbo.Tag.Id

GO
