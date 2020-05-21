IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteOpCo')
	BEGIN
		DROP  Procedure  Discovery_DeleteOpCo
	END

GO

CREATE Procedure Discovery_DeleteOpCo
(
	@Id int
)
AS
DELETE
FROM Discovery_opCo
WHERE
	Id=@Id

GO
