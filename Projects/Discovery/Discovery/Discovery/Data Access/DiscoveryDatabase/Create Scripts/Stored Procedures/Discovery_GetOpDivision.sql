IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoDivision')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoDivision
	END

GO

CREATE Procedure Discovery_GetOpCoDivision
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_OpcoDivision
WHERE
	Id=@Id

GO

