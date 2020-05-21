IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetWarehousesByRegionCode')
	BEGIN
		DROP  Procedure  Discovery_GetWarehousesByRegionCode
	END

GO

CREATE Procedure Discovery_GetWarehousesByRegionCode
(
		@RegionCode varchar(10)
)
AS

SELECT     
	Discovery_Warehouse.*
FROM         
	Discovery_Warehouse INNER JOIN
	Discovery_Region ON Discovery_Warehouse.RegionId = Discovery_Region.Id
WHERE     
	(Discovery_Region.Code = @RegionCode)

	
GO

