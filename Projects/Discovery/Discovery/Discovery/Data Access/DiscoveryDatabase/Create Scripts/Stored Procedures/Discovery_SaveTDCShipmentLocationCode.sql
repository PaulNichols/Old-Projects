IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveTDCShipmentLocationCode')
	BEGIN
		DROP  Procedure  Discovery_SaveTDCShipmentLocationCode
	END

GO

CREATE Procedure Discovery_SaveTDCShipmentLocationCode

	(
		@Id int= null,
		@LocationCode varchar(50)
	)


AS

IF @Id<>-1
	BEGIN
		
		UPDATE
			Discovery_Shipment
		SET
			LocationCode=@LocationCode
		WHERE
			Id=@Id 
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


 