IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoShipmentsByCriteria')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoShipmentsByCriteria
	END

GO

CREATE Procedure Discovery_GetOpCoShipmentsByCriteria
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
    @TransactionTypeCode varchar(10) = null,
    @SortExpression varchar(100) = 'RequiredShipmentDate',
	@PageIndex int = 0,
    @NumRows int = 10
)   

AS

BEGIN

	-- Calculate the start row index --
	DECLARE @StartRowIndex int;
	SET @StartRowIndex = ((@PageIndex ) * @NumRows) + 1;
	
	-- Get the specified rows by page --
	WITH Shipments AS 
	(
		SELECT 
			*,
			BINARY_CHECKSUM(*) as CheckSum,
			ROW_NUMBER() OVER
			(
				ORDER BY

				-- OpCoCode
				CASE WHEN @sortExpression = 'OpCoCode Ascending'
					THEN [OpCoCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'OpCoCode Descending'
					THEN [OpCoCode] ELSE NULL END DESC,
				-- ShipmentNumber
				CASE WHEN @sortExpression = 'ShipmentNumber Ascending'
					THEN [ShipmentNumber] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'ShipmentNumber Descending'
					THEN [ShipmentNumber] ELSE NULL END DESC,
				-- DespatchNumber
				CASE WHEN @sortExpression = 'DespatchNumber Ascending'
					THEN [DespatchNumber] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'DespatchNumber Descending'
					THEN [DespatchNumber] ELSE NULL END DESC,
				-- OpCoSequenceNumber
				CASE WHEN @sortExpression = 'OpCoSequenceNumber Ascending'
					THEN [OpCoSequenceNumber] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'OpCoSequenceNumber Descending'
					THEN [OpCoSequenceNumber] ELSE NULL END DESC,
				-- SalesBranchCode
				CASE WHEN @sortExpression = 'SalesBranchCode Ascending'
					THEN [SalesBranchCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'SalesBranchCode Descending'
					THEN [SalesBranchCode] ELSE NULL END DESC,
				-- RouteCode
				CASE WHEN @sortExpression = 'RouteCode Ascending'
					THEN [RouteCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'RouteCode Descending'
					THEN [RouteCode] ELSE NULL END DESC,
				-- CustomerName
				CASE WHEN @sortExpression = 'CustomerName Ascending'
					THEN [CustomerName] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'CustomerName Descending'
					THEN [CustomerName] ELSE NULL END DESC,
				-- RequiredShipmentDate
				CASE WHEN @sortExpression = 'RequiredShipmentDate Ascending'
					THEN [RequiredShipmentDate] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'RequiredShipmentDate Descending'
					THEN [RequiredShipmentDate] ELSE NULL END DESC,
				-- StockWarehouseCode
				CASE WHEN @sortExpression = 'StockWarehouseCode Ascending'
					THEN [StockWarehouseCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'StockWarehouseCode Descending'
					THEN [StockWarehouseCode] ELSE NULL END DESC,
				-- DeliveryWarehouseCode
				CASE WHEN @sortExpression = 'DeliveryWarehouseCode Ascending'
					THEN [DeliveryWarehouseCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'DeliveryWarehouseCode Descending'
					THEN [DeliveryWarehouseCode] ELSE NULL END DESC,
				-- TransactionTypeCode
				CASE WHEN @sortExpression = 'TransactionTypeCode Ascending'
					THEN TransactionTypeCode ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'TransactionTypeCode Descending'
					THEN TransactionTypeCode ELSE NULL END DESC
			) AS [Row]
			
		FROM 
			[Discovery_OpCoShipment]
			
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
	)

	SELECT *
	FROM Shipments
	WHERE
		@NumRows = -1 OR 
		([Row] BETWEEN @StartRowIndex AND @startRowIndex + (@NumRows - 1)) 

END
