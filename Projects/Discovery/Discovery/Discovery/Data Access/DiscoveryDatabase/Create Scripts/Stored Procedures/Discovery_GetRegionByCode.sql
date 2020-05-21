IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRegionByCode')
	BEGIN
		DROP  Procedure  Discovery_GetRegionByCode
	END

GO

CREATE Procedure Discovery_GetRegionByCode
	(
		@Code varchar(10)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Region
WHERE
	Code=@Code

GO

  