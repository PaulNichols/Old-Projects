IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoShipments')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoShipments
	END

GO

CREATE Procedure Discovery_GetOpCoShipments

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_OpCoShipment

GO



