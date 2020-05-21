IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTrunkerDay')
	BEGIN
		DROP  Procedure  Discovery_GetTrunkerDay
	END

GO

CREATE Procedure Discovery_GetTrunkerDay
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_TrunkerDay
WHERE
	Id=@Id

GO

 