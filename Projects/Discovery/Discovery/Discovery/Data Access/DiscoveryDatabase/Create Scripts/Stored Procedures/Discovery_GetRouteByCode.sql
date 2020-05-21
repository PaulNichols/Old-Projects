IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRouteByCode')
	BEGIN
		DROP  Procedure  Discovery_GetRouteByCode
	END

GO

CREATE Procedure Discovery_GetRouteByCode
	(
		@Code varchar(20)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Route
WHERE
	Code=@Code

GO

 