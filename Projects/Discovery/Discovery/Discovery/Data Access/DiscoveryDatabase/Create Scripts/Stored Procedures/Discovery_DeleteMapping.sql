IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteMapping')
	BEGIN
		DROP  Procedure  Discovery_DeleteMapping
	END

GO

CREATE Procedure Discovery_DeleteMapping
(
	@Id int
)
AS
DELETE
FROM Discovery_Mapping
WHERE
	Id=@Id

GO
