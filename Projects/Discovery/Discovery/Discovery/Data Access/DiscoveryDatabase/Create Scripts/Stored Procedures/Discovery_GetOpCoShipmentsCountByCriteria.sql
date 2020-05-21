IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoShipmentsCountByCriteria')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoShipmentsCountByCriteria
	END

GO

CREATE Procedure Discovery_GetOpCoShipmentsCountByCriteria
(
    @OpCoCode varchar(3) = null,
    @ShipmentNumber varchar(50) = null,        
    @CustomerNumber varchar(50) = null, 
    @CustomerName varchar(50) = null,          
    @Status int = null,        
    @SalesBranchCode varchar(50) = null,       
    @RouteCode varchar(10) = null,             
    @StockWarehouseCode varchar(10) = null,    
    @DeliveryWarehouseCode varchar(10) = null, 
    @RequiredDateFrom datetime = null,      
    @RequiredDateTo datetime = null,        
    @TransactionTypeCode varchar(10) = null
)    

AS

BEGIN

	SELECT COUNT(Id) 
	
	FROM 
	
		Discovery_OpCoShipment
		
	WHERE
	
		([OpCoCode] = @OpCoCode OR @OpCoCode IS NULL) AND
		([ShipmentNumber] = @ShipmentNumber OR @ShipmentNumber IS NULL) AND
	    ([CustomerNumber] LIKE COALESCE('%' + @CustomerNumber + '%', '%') OR @CustomerNumber IS NULL) AND
		([CustomerName] LIKE COALESCE('%' + @CustomerName + '%', '%') OR @CustomerName IS NULL) AND
		([Status] = @Status OR @Status IS NULL) AND
		([SalesBranchCode] = @SalesBranchCode OR @SalesBranchCode IS NULL) AND
		([RouteCode] = @RouteCode OR @RouteCode IS NULL) AND
		([StockWarehouseCode] = @StockWarehouseCode OR @StockWarehouseCode IS NULL) AND
		([DeliveryWarehouseCode] = @DeliveryWarehouseCode OR @DeliveryWarehouseCode IS NULL) AND
		([RequiredShipmentDate] >= @RequiredDateFrom OR @RequiredDateFrom IS NULL) AND
		([RequiredShipmentDate] <= @RequiredDateTo OR @RequiredDateTo IS NULL) AND
		([TransactionTypeCode] = @TransactionTypeCode OR @TransactionTypeCode IS NULL)
		

END

GO

