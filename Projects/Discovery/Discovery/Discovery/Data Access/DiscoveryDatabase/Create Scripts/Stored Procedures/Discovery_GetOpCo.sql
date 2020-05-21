IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCo')
	BEGIN
		DROP  Procedure  Discovery_GetOpCo
	END

GO

CREATE Procedure Discovery_GetOpCo
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Opco
WHERE
	Id=@Id

GO

