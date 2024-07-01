SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE VIEW [dbo].[vwMapConfigurationServiceGroupTag_Tag]
AS
SELECT     dbo.ConfigurationServiceGroup_Tag.ConfigurationServiceGroupId, dbo.ConfigurationServiceGroup_Tag.TagId, dbo.ConfigurationServiceGroup_Tag.CreatedBy, dbo.ConfigurationServiceGroup_Tag.CreatedOn, 
                      dbo.ConfigurationServiceGroup_Tag.ModifiedBy, dbo.ConfigurationServiceGroup_Tag.ModifiedOn, dbo.Tag.Name AS TagName, dbo.Tag.RowStatusId AS TagRowStatusId
FROM         dbo.ConfigurationServiceGroup_Tag WITH (NOLOCK) INNER JOIN
                      dbo.Tag WITH (NOLOCK) ON dbo.ConfigurationServiceGroup_Tag.TagId = dbo.Tag.Id

GO
