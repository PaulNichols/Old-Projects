IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeletePrinter')
	BEGIN
		DROP  Procedure  Discovery_DeletePrinter
	END

GO

CREATE Procedure Discovery_DeletePrinter
(
	@Id int
)
AS
DELETE
FROM Discovery_Printer
WHERE
	Id=@Id

GO
