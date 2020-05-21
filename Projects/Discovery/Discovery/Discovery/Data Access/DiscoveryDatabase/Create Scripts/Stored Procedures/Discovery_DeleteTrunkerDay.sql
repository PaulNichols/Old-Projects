IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteTrunkerDay')
	BEGIN
		DROP  Procedure  Discovery_DeleteTrunkerDay
	END

GO

CREATE Procedure Discovery_DeleteTrunkerDay
(
	@Id int
)
AS
DELETE
FROM Discovery_TrunkerDay
WHERE
	Id=@Id

GO
