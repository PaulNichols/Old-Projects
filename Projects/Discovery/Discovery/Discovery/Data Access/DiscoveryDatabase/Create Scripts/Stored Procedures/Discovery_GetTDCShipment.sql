IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipment')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipment
	END

GO

CREATE Procedure Discovery_GetTDCShipment

	(
		@Id int
	)

AS

SELECT
S.*, 
T.TripNumber AS RouteTrip, 
D.DropSequence AS RouteDrop,
BINARY_CHECKSUM(
		S.Id,
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
		UpdatedBy
		) as CheckSum

FROM 
Discovery_Shipment AS S 
LEFT OUTER JOIN
	Discovery_Drop AS D ON S.Id = D.ShipmentId LEFT OUTER JOIN
	Discovery_Trip AS T ON T.Id = D.TripId
WHERE
	S.Id = @Id

GO



