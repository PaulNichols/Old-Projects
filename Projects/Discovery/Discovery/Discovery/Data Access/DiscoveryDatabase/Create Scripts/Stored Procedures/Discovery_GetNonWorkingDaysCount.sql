IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetNonWorkingDaysCount')
	BEGIN
		DROP  Procedure  Discovery_GetNonWorkingDaysCount
	END

GO

CREATE Procedure Discovery_GetNonWorkingDaysCount
(
		@dateFrom datetime,
		@dateTo datetime,
		@warehouseId int = Null,
		@regionId int = Null
	)

AS

SELECT count(    
	Discovery_NonWorkingDay.id)
FROM
		Discovery_NonWorkingDay INNER JOIN
                      Discovery_Warehouse ON Discovery_NonWorkingDay.WarehouseId = Discovery_Warehouse.Id
WHERE     
	(Discovery_NonWorkingDay.NonWorkingDate >= @dateFrom) AND
	(Discovery_NonWorkingDay.NonWorkingDate <= @dateTo)AND
		(Discovery_NonWorkingDay.WarehouseId=@warehouseId OR @warehouseId Is Null) AND
		(Discovery_Warehouse.RegionId=@regionId OR @regionId Is Null) 
	

GO
