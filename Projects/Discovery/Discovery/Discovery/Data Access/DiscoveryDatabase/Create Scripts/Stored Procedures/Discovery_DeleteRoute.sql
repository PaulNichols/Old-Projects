IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteRoute')
	BEGIN
		DROP  Procedure  Discovery_DeleteRoute
	END

GO

CREATE Procedure Discovery_DeleteRoute
(
	@Id int
)
AS
DELETE
FROM Discovery_Route
WHERE
	Id=@Id

GO
