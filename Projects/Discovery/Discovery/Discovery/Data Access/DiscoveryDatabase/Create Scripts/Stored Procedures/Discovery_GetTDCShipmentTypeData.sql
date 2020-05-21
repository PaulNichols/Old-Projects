IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentTypeData')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentTypeData
	END

GO

CREATE Procedure Discovery_GetTDCShipmentTypeData

	(
		@Id int
	)

AS

SELECT
	S.StockWarehouseCode,
	S.DeliveryWarehouseCode,
	S.TransactionTypeCode,
	S.TransactionSubTypeCode,
	S.RouteCode
FROM 
	Discovery_Shipment AS S 
WHERE
	S.Id = @Id

GO



