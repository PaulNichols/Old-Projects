IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_GetConnection')
	BEGIN
		DROP  Procedure  Integration_GetConnection
	END

GO

CREATE Procedure Integration_GetConnection
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Integration_Connection
WHERE
	Id=@Id

GO

 