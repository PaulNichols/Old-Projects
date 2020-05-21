IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTripByWarehouseDateAndNumber')
	BEGIN
		DROP  Procedure  Discovery_GetTripByWarehouseDateAndNumber
	END

GO

CREATE Procedure Discovery_GetTripByWarehouseDateAndNumber

	(
		@TripNumber varchar(15), 
		@TripDate datetime,
		@WarehouseId int
	)


AS


SELECT     
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM         
	Discovery_Trip
WHERE     
	TripNumber = @TripNumber and
	StartDate=@TripDate and
	WarehouseId=@WarehouseId
	
GO


