IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoShipmentByShipmentNumber')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoShipmentByShipmentNumber
	END

GO

CREATE Procedure Discovery_GetOpCoShipmentByShipmentNumber

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
	Discovery_OpCoShipment
WHERE
	OpCoCode = @OpCoCode AND
	ShipmentNumber = @ShipmentNumber AND
	DespatchNumber = @DespatchNumber

GO



