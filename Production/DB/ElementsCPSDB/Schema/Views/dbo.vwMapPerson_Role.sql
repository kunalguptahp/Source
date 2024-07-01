SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapPerson_Role
AS
SELECT     dbo.Person.Id, dbo.Person.WindowsId, dbo.Person.Name, dbo.Person.FirstName, dbo.Person.MiddleName, dbo.Person.LastName, dbo.Person.Email, 
                      dbo.Person.CreatedOn, dbo.Person.CreatedBy, dbo.Person.ModifiedOn, dbo.Person.ModifiedBy, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.Person.RowStatusId, dbo.Role.Name AS RoleName, dbo.Role.Id AS RoleId
FROM         dbo.Person WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.Person.RowStatusId = dbo.RowStatus.Id INNER JOIN
                      dbo.Person_Role WITH (NOLOCK) ON dbo.Person.Id = dbo.Person_Role.PersonId INNER JOIN
                      dbo.Role WITH (NOLOCK) ON dbo.Person_Role.RoleId = dbo.Role.Id


GO
