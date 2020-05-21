IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetDropByKey')
	BEGIN
		DROP  Procedure  Discovery_GetDropByKey
	END

GO

CREATE Procedure Discovery_GetDropByKey
	(
		@shipmentId int,
		@tripId int,
		@sequence int,
		@dropSequence int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Drop
WHERE
	(ShipmentId=@shipmentId OR @shipmentId IS NULL) and
	tripId=@tripId and
	--OrderSequence=@sequence and
	dropSequence=@dropSequence

GO

  