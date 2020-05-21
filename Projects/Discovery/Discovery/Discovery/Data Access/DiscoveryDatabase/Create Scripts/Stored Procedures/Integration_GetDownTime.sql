IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_GetDownTime')
	BEGIN
		DROP  Procedure  Integration_GetDownTime
	END

GO

CREATE Procedure Integration_GetDownTime
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Integration_DownTime
WHERE
	Id=@Id

GO

 