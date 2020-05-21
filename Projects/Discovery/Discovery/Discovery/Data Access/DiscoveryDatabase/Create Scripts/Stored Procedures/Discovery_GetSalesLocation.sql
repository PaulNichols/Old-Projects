IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSalesLocation')
	BEGIN
		DROP  Procedure  Discovery_GetSalesLocation
	END

GO

CREATE Procedure Discovery_GetSalesLocation
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_SalesLocation
WHERE
	Id=@Id

GO

 