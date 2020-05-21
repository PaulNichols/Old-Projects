IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRegions')
	BEGIN
		DROP  Procedure  Discovery_GetRegions
	END

GO

CREATE Procedure Discovery_GetRegions

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Region

GO

