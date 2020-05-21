IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteTDCShipment')
	BEGIN
		DROP  Procedure  Discovery_DeleteTDCShipment
	END

GO

CREATE Procedure Discovery_DeleteTDCShipment

	(
		@Id int
	)


AS

DELETE
FROM Discovery_Shipment
WHERE
	Id=@Id
GO

