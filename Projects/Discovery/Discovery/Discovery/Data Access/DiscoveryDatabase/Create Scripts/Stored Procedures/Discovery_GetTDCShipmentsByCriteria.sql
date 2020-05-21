IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentsByCriteria')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentsByCriteria
	END

GO

CREATE Procedure Discovery_GetTDCShipmentsByCriteria
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
    @Type int = null,
    @SortExpression varchar(100),
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
			S.*,
			T.TripNumber as RouteTrip,
			D.OrderSequence as RouteDrop,
			T.StartDate as RoutingDateTime,
			BINARY_CHECKSUM(
					S.Id,
					Type,
					OpCoShipmentId,
					OpCoCode,
					OpCoSequenceNumber,
					OpCoContactEmail,
					OpCoContactName,
					DespatchNumber,
					RequiredShipmentDate,
					TransactionTypeCode,
					CustomerReference,
					Instructions,
					RouteCode,
					CustomerNumber,
					CustomerName,
					CustomerAddress1,
					CustomerAddress2,
					CustomerAddress3,
					CustomerAddress4,
					CustomerAddress5,
					CustomerPostCode,
					ShipmentNumber,
					ShipmentName,
					ShipmentAddress1,
					ShipmentAddress2,
					ShipmentAddress3,
					ShipmentAddress4,
					ShipmentAddress5,
					ShipmentPostCode,
					ShipmentContactName,
					ShipmentContactTel,
					ShipmentContactEmail,
					SalesBranchCode,
					AfterTime,
					BeforeTime,
					TailLiftRequired,
					VehicleMaxWeight,
					CheckInTime,
					DeliveryWarehouseCode,
					StockWarehouseCode,
					DivisionCode,
					GeneratedDateTime,
					Status,
					[Type],
					ActualDeliveryDate,
					EstimatedDeliveryDate,
					IsRecurring,
					IsValidAddress,
					LocationCode,
					PAFAddress1,
					PAFAddress2,
					PAFAddress3,
					PAFAddress4,
					PAFAddress5,
					PAFPostCode,
					PAFDPS,
					PAFEasting,
					PAFNorthing,
					PAFLocation,
					PAFMatch,
					SentToWMS,
					SplitSequence,
					IsSplit,
					TransactionSubTypeCode,
					AuditId,
					UpdatedDate,
					UpdatedBy) as CheckSum,
			
			ROW_NUMBER() OVER
			(
				ORDER BY 

				-- OpCoCode
				CASE WHEN @sortExpression = 'OpCoCode Ascending'
					THEN [OpCoCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'OpCoCode Descending'
					THEN [OpCoCode] ELSE NULL END DESC,
				-- Type
				CASE WHEN @sortExpression = 'Type Ascending'
					THEN [Type] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'Type Descending'
					THEN [Type] ELSE NULL END DESC,
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
				-- ShipmentName,pafpostcode,estimateddeliverydate
				CASE WHEN @sortExpression = 'ShipmentName,pafpostcode,estimateddeliverydate'
					THEN ShipmentName ELSE NULL END ASC,
				-- EstimatedDeliveryDate
				CASE WHEN @sortExpression = 'EstimatedDeliveryDate Ascending'
					THEN [EstimatedDeliveryDate] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'EstimatedDeliveryDate Descending'
					THEN [EstimatedDeliveryDate] ELSE NULL END DESC,
				-- TransactionTypeCode
				CASE WHEN @sortExpression = 'TransactionTypeCode Ascending'
					THEN [TransactionTypeCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'TransactionTypeCode Descending'
					THEN [TransactionTypeCode] ELSE NULL END DESC,
				-- TransactionSubTypeCode
				CASE WHEN @sortExpression = 'TransactionSubTypeCode Ascending'
					THEN [TransactionSubTypeCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'TransactionSubTypeCode Descending'
					THEN [TransactionSubTypeCode] ELSE NULL END DESC,
				-- Route Trip
				CASE WHEN @sortExpression = 'RouteTrip Ascending'
					THEN T.TripNumber ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'RouteTrip Descending'
					THEN T.TripNumber ELSE NULL END DESC,
				-- Route Drop
				CASE WHEN @sortExpression = 'RouteDrop Ascending'
					THEN D.OrderSequence ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'RouteDrop Descending'
					THEN D.OrderSequence ELSE NULL END DESC,
				-- Route Trip/Drop
				CASE WHEN @sortExpression = 'RouteTripDrop Ascending'
					THEN  T.TripNumber + '-' + CAST(D.OrderSequence AS VARCHAR(10)) ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'RouteTripDrop Descending'
					THEN T.TripNumber + '-' + CAST(D.OrderSequence AS VARCHAR(10)) ELSE NULL END DESC,
				-- Type
				CASE WHEN @sortExpression = 'Type Ascending'
					THEN [Type] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'Type Descending'
					THEN [Type] ELSE NULL END DESC,
				-- PAFPostCode
				CASE WHEN @sortExpression = 'PAFPostCode Ascending'
					THEN [PAFPostCode] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'PAFPostCode Descending'
					THEN [PAFPostCode] ELSE NULL END DESC
					
			) AS [Row]
			
		FROM 
			Discovery_Shipment AS S 
			LEFT OUTER JOIN
				Discovery_Drop AS D ON S.Id = D.ShipmentId LEFT OUTER JOIN
				Discovery_Trip AS T ON T.Id = D.TripId
		WHERE
			(S.OpCoCode = @OpCoCode OR @OpCoCode IS NULL) AND
			(S.ShipmentNumber LIKE COALESCE('' + @ShipmentNumber + '%', '%') OR @ShipmentNumber IS NULL) AND
			(S.CustomerNumber LIKE COALESCE('' + @CustomerNumber + '%', '%') OR @CustomerNumber IS NULL) AND
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
	)

	SELECT 
		*
	FROM 
		Shipments
	WHERE 
		@NumRows = -1 OR 
		([Row] BETWEEN @StartRowIndex AND @startRowIndex + (@NumRows - 1)) 

END
