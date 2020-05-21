IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetNonWorkingDaysByRegion')
	BEGIN
		DROP  Procedure  Discovery_GetNonWorkingDaysByRegion
	END

GO

CREATE Procedure Discovery_GetNonWorkingDaysByRegion
	(
		@dateFrom datetime,
		@dateTo datetime,
		@regionId int
	)

AS

SELECT     
	Discovery_NonWorkingDay.*, 
	BINARY_CHECKSUM(Discovery_NonWorkingDay.Id, Discovery_NonWorkingDay.NonWorkingDate, Discovery_NonWorkingDay.Description, 
                      Discovery_NonWorkingDay.WarehouseId, Discovery_NonWorkingDay.UpdatedDate, Discovery_NonWorkingDay.UpdatedBy) AS CheckSum
FROM
	Discovery_NonWorkingDay INNER JOIN
        Discovery_Warehouse ON Discovery_NonWorkingDay.WarehouseId = Discovery_Warehouse.Id 
WHERE     
	(Discovery_Warehouse.RegionId= @regionId) AND 
	(Discovery_NonWorkingDay.NonWorkingDate >= @dateFrom) AND
	(Discovery_NonWorkingDay.NonWorkingDate <= @dateTo)
	

GO

