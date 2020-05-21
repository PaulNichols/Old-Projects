IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteOpCoShipment')
	BEGIN
		DROP  Procedure  Discovery_DeleteOpCoShipment
	END

GO

CREATE Procedure Discovery_DeleteOpCoShipment

	(
		@Id int
	)


AS

DELETE
FROM Discovery_OpCoShipment
WHERE
	Id=@Id
GO

