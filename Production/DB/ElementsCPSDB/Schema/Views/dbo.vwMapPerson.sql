SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW dbo.vwMapPerson
AS
SELECT     dbo.Person.Id, dbo.Person.WindowsId, dbo.Person.Name, dbo.Person.FirstName, dbo.Person.MiddleName, dbo.Person.LastName, dbo.Person.Email, 
                      dbo.Person.CreatedOn, dbo.Person.CreatedBy, dbo.Person.ModifiedOn, dbo.Person.ModifiedBy, dbo.RowStatus.Name AS RowStatusName, 
                      dbo.Person.RowStatusId
FROM         dbo.Person WITH (NOLOCK) INNER JOIN
                      dbo.RowStatus WITH (NOLOCK) ON dbo.Person.RowStatusId = dbo.RowStatus.Id


GO
