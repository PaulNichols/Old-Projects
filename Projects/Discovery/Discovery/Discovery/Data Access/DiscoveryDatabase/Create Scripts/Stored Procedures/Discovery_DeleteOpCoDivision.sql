IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteOpCoDivision')
	BEGIN
		DROP  Procedure  Discovery_DeleteOpCoDivision
	END

GO

CREATE Procedure Discovery_DeleteOpCoDivision
(
	@Id int
)
AS
DELETE
FROM Discovery_OpCoDivision
WHERE
	Id=@Id

GO
