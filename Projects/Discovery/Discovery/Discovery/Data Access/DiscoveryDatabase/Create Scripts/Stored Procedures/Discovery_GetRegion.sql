IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRegion')
	BEGIN
		DROP  Procedure  Discovery_GetRegion
	END

GO

CREATE Procedure Discovery_GetRegion
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Region
WHERE
	Id=@Id

GO

 