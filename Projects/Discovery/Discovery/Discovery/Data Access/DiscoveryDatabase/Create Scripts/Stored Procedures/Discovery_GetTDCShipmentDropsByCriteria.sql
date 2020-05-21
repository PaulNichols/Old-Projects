IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentDropsByCriteria')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentDropsByCriteria
	END

GO

CREATE Procedure Discovery_GetTDCShipmentDropsByCriteria
(
    @OpCoCode varchar(3) = null,
    @ShipmentNumber varchar(50) = null,        
    @CustomerNumber varchar(50) = null, 
    @CustomerName varchar(50) = null,          
    @Status int = null,        
    @SalesBranchCode varchar(50) = null,       
    @RouteCode varchar(10) = null,             
    @RouteTrip varchar(15) = null,             
    @RouteDrop int = null,             
    --@LocationCode varchar(50) = null,              
    @StockWarehouseCode varchar(10) = null,    
    @DeliveryWarehouseCode varchar(10) = null, 
    @RequiredDateFrom datetime = null,      
    @RequiredDateTo datetime = null
)   

AS

BEGIN

SELECT 

	Discovery_Drop.DropSequenceNumber AS DropSequenceNumber
	
FROM    

	Discovery_Shipment INNER JOIN
	Discovery_ShipmentToDrop ON Discovery_Shipment.Id = Discovery_ShipmentToDrop.ShipmentId INNER JOIN
	Discovery_Drop ON Discovery_ShipmentToDrop.DropId = Discovery_Drop.Id INNER JOIN
	Discovery_Trip ON Discovery_Drop.TripId = Discovery_Trip.Id
                      
WHERE

	(Discovery_Shipment.OpCoCode = @OpCoCode OR @OpCoCode IS NULL) AND
	(Discovery_Shipment.ShipmentNumber = @ShipmentNumber OR @ShipmentNumber IS NULL) AND
	(Discovery_Shipment.CustomerNumber = @CustomerNumber OR @CustomerNumber IS NULL) AND
	(Discovery_Shipment.CustomerName LIKE COALESCE('%' + @CustomerName + '%', '%') OR @CustomerName IS NULL) AND
	(Discovery_Shipment.Status = @Status OR @Status IS NULL) AND
	(Discovery_Shipment.SalesBranchCode = @SalesBranchCode OR @SalesBranchCode IS NULL) AND
	(Discovery_Shipment.RouteCode = @RouteCode OR @RouteCode IS NULL) AND
	(Discovery_Trip.TripNumber = @RouteTrip OR @RouteTrip IS NULL) AND
	--**(Discovery_Shipment.RouteDrop = @RouteDrop OR @RouteDrop IS NULL) AND
	--**(Discovery_Shipment.LocationCode = @LocationCode OR @LocationCode IS NULL) AND
	(Discovery_Shipment.StockWarehouseCode = @StockWarehouseCode OR @StockWarehouseCode IS NULL) AND
	(Discovery_Shipment.DeliveryWarehouseCode = @DeliveryWarehouseCode OR @DeliveryWarehouseCode IS NULL) AND
	(Discovery_Shipment.RequiredShipmentDate >= @RequiredDateFrom OR @RequiredDateFrom IS NULL) AND
	(Discovery_Shipment.RequiredShipmentDate <= @RequiredDateTo OR @RequiredDateTo IS NULL)

ORDER BY Discovery_Trip.TripNumber ASC

END
