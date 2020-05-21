IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveDropLine')
	BEGIN
		DROP  Procedure  Discovery_SaveDropLine
	END

GO

CREATE Procedure Discovery_SaveDropLine

	(
		@Id int,
		@Quantity int,
		@Weight numeric(9, 2),
		@Volume numeric(7, 3),
		@ShipmentLineId int,
		@DropId int,
		@SiteCode varchar(16),
		@OriginalShipmentLineId int
	)

AS


IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_ShipmentDropLine
			(
				Quantity ,
				Weight ,
				Volume ,
				ShipmentLineId ,
				DropId ,
				SiteCode ,
				OriginalShipmentLineId
			)
		VALUES
			(
				@Quantity ,
				@Weight ,
				@Volume ,
				@ShipmentLineId ,
				@DropId ,
				@SiteCode ,
				@OriginalShipmentLineId
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_ShipmentDropLine
		SET
				Quantity =@Quantity,
				Weight =@Weight ,
				Volume =@Volume ,
				ShipmentLineId =@ShipmentLineId ,
				DropId =@DropId ,
				SiteCode =@SiteCode ,
				OriginalShipmentLineId=@OriginalShipmentLineId
		WHERE
			Id=@Id 
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


