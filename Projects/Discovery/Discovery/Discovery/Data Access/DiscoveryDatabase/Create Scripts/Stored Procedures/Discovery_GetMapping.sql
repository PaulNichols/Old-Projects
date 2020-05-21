IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMapping')
	BEGIN
		DROP  Procedure  Discovery_GetMapping
	END

GO

CREATE Procedure Discovery_GetMapping
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Mapping
WHERE
	Id=@Id

GO

 