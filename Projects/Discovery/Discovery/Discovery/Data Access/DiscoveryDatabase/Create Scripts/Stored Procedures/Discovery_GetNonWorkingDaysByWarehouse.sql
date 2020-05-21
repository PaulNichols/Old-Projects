IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetNonWorkingDaysByWarehouse')
	BEGIN
		DROP  Procedure  Discovery_GetNonWorkingDaysByWarehouse
	END

GO

CREATE Procedure Discovery_GetNonWorkingDaysByWarehouse
	(
		@dateFrom datetime,
		@dateTo datetime,
		@warehouseId int
	)

AS

SELECT     
	Discovery_NonWorkingDay.*, 
	BINARY_CHECKSUM(*) AS CheckSum
FROM
	Discovery_NonWorkingDay 
WHERE     
	(Discovery_NonWorkingDay.WarehouseId = @warehouseId) AND 
	(Discovery_NonWorkingDay.NonWorkingDate >= @dateFrom) AND
	(Discovery_NonWorkingDay.NonWorkingDate <= @dateTo)
	

GO

