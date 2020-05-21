IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_DeleteDownTime')
	BEGIN
		DROP  Procedure  Integration_DeleteDownTime
	END

GO

CREATE Procedure Integration_DeleteDownTime
(
	@Id int
)
AS
DELETE
FROM Integration_DownTime
WHERE
	Id=@Id

GO
