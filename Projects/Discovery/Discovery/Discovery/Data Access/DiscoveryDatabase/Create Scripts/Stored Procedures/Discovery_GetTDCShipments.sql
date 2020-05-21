IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipments')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipments
	END

GO

CREATE Procedure Discovery_GetTDCShipments

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Shipment


GO
