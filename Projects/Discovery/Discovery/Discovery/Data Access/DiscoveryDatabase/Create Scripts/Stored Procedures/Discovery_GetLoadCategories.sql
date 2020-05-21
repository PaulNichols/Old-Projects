IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetLoadCategories')
	BEGIN
		DROP  Procedure  Discovery_GetLoadCategories
	END

GO

CREATE Procedure Discovery_GetLoadCategories

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_LoadCategory

GO

