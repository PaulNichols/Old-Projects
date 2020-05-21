IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteOpCoShipmentLine')
	BEGIN
		DROP  Procedure  Discovery_DeleteOpCoShipmentLine
	END

GO

CREATE Procedure Discovery_DeleteOpCoShipmentLine

	(
		@Id int
	)


AS

DELETE
FROM Discovery_OpCoShipmentLine
WHERE
	Id=@Id
GO

