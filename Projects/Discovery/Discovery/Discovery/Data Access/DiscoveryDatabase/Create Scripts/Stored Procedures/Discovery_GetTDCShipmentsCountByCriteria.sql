IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentsCountByCriteria')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentsCountByCriteria
	END

GO

CREATE Procedure Discovery_GetTDCShipmentsCountByCriteria
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
    @StockWarehouseCode varchar(10) = null,    
    @DeliveryWarehouseCode varchar(10) = null, 
    @RequiredDateFrom datetime = null,      
    @RequiredDateTo datetime = null,      
    @EstimatedDateFrom datetime = null,      
    @EstimatedDateTo datetime = null,        
    @TransactionTypeCode varchar(10) = null,
    @TransactionSubTypeCode varchar(50) = null,
    @Type int = null
)    

AS

BEGIN

	SELECT COUNT(S.Id) 
	
	FROM 
		Discovery_Shipment AS S 
		LEFT OUTER JOIN
			Discovery_Drop AS D ON S.Id = D.ShipmentId LEFT OUTER JOIN
			Discovery_Trip AS T ON T.Id = D.TripId
		
	WHERE
	
		(S.OpCoCode = @OpCoCode OR @OpCoCode IS NULL) AND
		(S.ShipmentNumber = @ShipmentNumber OR @ShipmentNumber IS NULL) AND
		(S.CustomerNumber LIKE COALESCE('%' + @CustomerNumber + '%', '%') OR @CustomerNumber IS NULL) AND
		(S.CustomerName LIKE COALESCE('%' + @CustomerName + '%', '%') OR @CustomerName IS NULL) AND
		(S.Status = @Status OR @Status IS NULL) AND
		(S.SalesBranchCode = @SalesBranchCode OR @SalesBranchCode IS NULL) AND
		(S.RouteCode = @RouteCode OR @RouteCode IS NULL) AND
		(T.TripNumber = @RouteTrip OR @RouteTrip IS NULL) AND
		(D.DropSequence = @RouteDrop OR @RouteDrop IS NULL) AND
		(S.StockWarehouseCode = @StockWarehouseCode OR @StockWarehouseCode IS NULL) AND
		(S.DeliveryWarehouseCode = @DeliveryWarehouseCode OR @DeliveryWarehouseCode IS NULL) AND
		(S.RequiredShipmentDate >= @RequiredDateFrom OR @RequiredDateFrom IS NULL) AND
		(S.RequiredShipmentDate <= @RequiredDateTo OR @RequiredDateTo IS NULL) and 
		(S.TransactionTypeCode = @TransactionTypeCode OR @TransactionTypeCode IS NULL) AND
		(S.TransactionSubTypeCode = @TransactionSubTypeCode OR @TransactionSubTypeCode IS NULL) AND
		((S.Type = @Type & S.Type ) OR @Type IS NULL) AND
		(S.EstimatedDeliveryDate >= @EstimatedDateFrom OR @EstimatedDateFrom IS NULL) AND
		(S.EstimatedDeliveryDate <= @EstimatedDateTo OR @EstimatedDateTo IS NULL)
		

END

GO

