IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetNonWorkingDayLinkedToWarehouse')
	BEGIN
		DROP  Procedure  Discovery_GetNonWorkingDayLinkedToWarehouse
	END

GO

CREATE Procedure Discovery_GetNonWorkingDayLinkedToWarehouse
	(
		@warehouseCode Varchar(256),
		@nonWorkingDate DateTime
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_NonWorkingDay AS a
	INNER JOIN Discovery_Warehouse AS b
	ON a.WarehouseId = b.Id
WHERE
    a.NonWorkingDate = @nonWorkingDate
AND b.Code = @warehouseCode

GO

