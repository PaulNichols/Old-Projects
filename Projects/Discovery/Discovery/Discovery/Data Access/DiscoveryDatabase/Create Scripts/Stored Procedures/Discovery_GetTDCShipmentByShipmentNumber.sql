IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentByShipmentNumber')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentByShipmentNumber
	END

GO

CREATE Procedure Discovery_GetTDCShipmentByShipmentNumber

	(
		@OpCoCode varchar(50),
		@ShipmentNumber varchar(50),
		@DespatchNumber varchar(50)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Shipment
WHERE
	OpCoCode = @OpCoCode AND
	ShipmentNumber = @ShipmentNumber AND
	DespatchNumber = @DespatchNumber

GO



