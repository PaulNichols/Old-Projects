IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRoute')
	BEGIN
		DROP  Procedure  Discovery_GetRoute
	END

GO

CREATE Procedure Discovery_GetRoute
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Route
WHERE
	Id=@Id

GO

 