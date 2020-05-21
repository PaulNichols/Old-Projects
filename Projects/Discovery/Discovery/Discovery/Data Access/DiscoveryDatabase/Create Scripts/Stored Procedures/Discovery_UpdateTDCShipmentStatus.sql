IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_UpDateTDCShipmentStatus')
	BEGIN
		DROP  Procedure  Discovery_UpDateTDCShipmentStatus
	END

GO

CREATE Procedure Discovery_UpDateTDCShipmentStatus
	(
		@Id int = -1,
		@Status int,
		@UpdatedBy varchar(256)
	)

AS

		UPDATE
			Discovery_Shipment
		SET
			Status=@Status,
			UpdatedBy=@UpdatedBy,
			UpdatedDate=GetDate()
		WHERE
			Id = @Id

GO
