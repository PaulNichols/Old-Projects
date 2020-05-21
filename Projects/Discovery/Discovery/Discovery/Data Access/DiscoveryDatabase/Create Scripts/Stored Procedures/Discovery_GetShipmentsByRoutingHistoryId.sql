IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetShipmentsByRoutingHistoryId')
	BEGIN
		DROP  Procedure  Discovery_GetShipmentsByRoutingHistoryId
	END

GO

CREATE Procedure Discovery_GetShipmentsByRoutingHistoryId
	(
		@routingHistoryId int, 
		@sortExpression varchar(1000),
		@PageIndex int, 
		@maximumRows int
	)

AS

	DECLARE @StartRowIndex int;
	SET @StartRowIndex = ((@PageIndex ) * @maximumRows) + 1;
	
	
SELECT 
		Id,		
		*
	FROM
	   (
		   SELECT     
			*, 
			BINARY_CHECKSUM(*) AS CheckSum,
			ROW_NUMBER() OVER(
				ORDER BY				
				-- EstimatedDeliveryDate
				CASE WHEN @sortExpression = 'EstimatedDeliveryDate' 
					 THEN [EstimatedDeliveryDate] ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'EstimatedDeliveryDate desc' 
					 THEN [EstimatedDeliveryDate] ELSE NULL END DESC,
     
				-- ShipmentNumber
				CASE WHEN @sortExpression = 'ShipmentNumber' 
					 THEN ShipmentNumber ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'ShipmentNumber desc' 
					 THEN ShipmentNumber ELSE NULL END DESC,
					 
				-- DespatchNumber
				CASE WHEN @sortExpression = 'DespatchNumber' 
					 THEN DespatchNumber ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'DespatchNumber desc' 
					 THEN DespatchNumber ELSE NULL END DESC,
					 
				-- ShipmentName
				CASE WHEN @sortExpression = 'ShipmentName' 
					 THEN ShipmentName ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'ShipmentName desc' 
					 THEN ShipmentName ELSE NULL END DESC,
				
				-- DeliveryWarehouseCode
				CASE WHEN @sortExpression = 'DeliveryWarehouseCode' 
					 THEN DeliveryWarehouseCode ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'DeliveryWarehouseCode desc' 
					 THEN DeliveryWarehouseCode ELSE NULL END DESC,
					 
				-- PostCode
				CASE WHEN @sortExpression = 'PostCode' 
					 THEN PAFPostCode ELSE NULL END ASC,
				CASE WHEN @sortExpression = 'PostCode desc' 
					 THEN PAFPostCode ELSE NULL END DESC
  
				) as RowNum
		FROM         
			Discovery_Shipment 	
		WHERE     
			id in 
				(
					select 
						ShipmentId  as id
					from 
						Discovery_ShipmentRoutingHistory 
					where 
						Discovery_ShipmentRoutingHistory.ShipmentId=Discovery_Shipment.Id and
						Discovery_ShipmentRoutingHistory.RoutingHistoryId=@routingHistoryId
				)
		 ) as DerivedTableName
	WHERE 
	@maximumRows=-1 OR 
		(RowNum BETWEEN @startRowIndex AND @startRowIndex + (@maximumRows - 1)) 
GO

