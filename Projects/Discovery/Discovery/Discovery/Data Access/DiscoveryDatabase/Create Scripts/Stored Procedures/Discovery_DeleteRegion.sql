IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteRegion')
	BEGIN
		DROP  Procedure  Discovery_DeleteRegion
	END

GO

CREATE Procedure Discovery_DeleteRegion
(
	@Id int
)
AS
DELETE
FROM Discovery_Region
WHERE
	Id=@Id

GO
