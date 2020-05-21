IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetWarehouse')
	BEGIN
		DROP  Procedure  Discovery_GetWarehouse
	END

GO

CREATE Procedure Discovery_GetWarehouse
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Warehouse
WHERE
	Id=@Id

GO

 