IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_UpDateOpCoShipmentStatus')
	BEGIN
		DROP  Procedure  Discovery_UpDateOpCoShipmentStatus
	END

GO

CREATE Procedure Discovery_UpDateOpCoShipmentStatus
	(
		@Id int = -1,
		@Status int
	)

AS

		UPDATE
			Discovery_OpCoShipment
		SET
			Status=@Status
		WHERE
			Id = @Id
				

GO
