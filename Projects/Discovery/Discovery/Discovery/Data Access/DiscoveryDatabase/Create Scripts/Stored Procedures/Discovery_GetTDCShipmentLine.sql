IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentLine')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentLine
	END

GO

CREATE Procedure Discovery_GetTDCShipmentLine
	(
		@ShipmentLineId int = null
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_ShipmentLine
WHERE
	Id = @ShipmentLineId


GO

