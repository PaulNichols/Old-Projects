IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteLoadCategory')
	BEGIN
		DROP  Procedure  Discovery_DeleteLoadCategory
	END

GO

CREATE Procedure Discovery_DeleteLoadCategory
(
	@Id int
)
AS
DELETE
FROM Discovery_LoadCategory
WHERE
	Id=@Id

GO
