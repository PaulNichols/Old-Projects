IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingSystem')
	BEGIN
		DROP  Procedure  Discovery_GetMappingSystem
	END

GO

CREATE Procedure Discovery_GetMappingSystem
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingSystem
WHERE
	Id=@Id

GO

 