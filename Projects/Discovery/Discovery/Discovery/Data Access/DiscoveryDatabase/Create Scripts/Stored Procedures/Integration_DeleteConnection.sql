IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_DeleteConnection')
	BEGIN
		DROP  Procedure  Integration_DeleteConnection
	END

GO

CREATE Procedure Integration_DeleteConnection
(
	@Id int
)
AS
DELETE
FROM Integration_Connection
WHERE
	Id=@Id

GO
