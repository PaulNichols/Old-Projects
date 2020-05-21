IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_DeleteTask')
	BEGIN
		DROP  Procedure  Integration_DeleteTask
	END

GO

CREATE Procedure Integration_DeleteTask
(
	@Id int
)
AS
DELETE
FROM Integration_Task
WHERE
	Id=@Id

GO
