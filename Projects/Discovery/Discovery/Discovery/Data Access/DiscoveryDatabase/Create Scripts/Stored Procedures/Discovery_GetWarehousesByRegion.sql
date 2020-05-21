IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetWarehousesByRegion')
	BEGIN
		DROP  Procedure  Discovery_GetWarehousesByRegion
	END

GO

CREATE Procedure Discovery_GetWarehousesByRegion
(
		@RegionId int
)
AS

SELECT     
	*
FROM         
	Discovery_Warehouse 
WHERE     
	(RegionId = @RegionId)

	
GO

