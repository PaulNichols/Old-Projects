IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoShipment')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoShipment
	END

GO

CREATE Procedure Discovery_GetOpCoShipment

	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_OpCoShipment
WHERE
	Id=@Id

GO



