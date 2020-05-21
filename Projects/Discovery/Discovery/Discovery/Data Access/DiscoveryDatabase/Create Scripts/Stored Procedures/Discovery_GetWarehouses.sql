IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetWarehouses')
	BEGIN
		DROP  Procedure  Discovery_GetWarehouses
	END

GO

CREATE Procedure Discovery_GetWarehouses

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Warehouse

GO

