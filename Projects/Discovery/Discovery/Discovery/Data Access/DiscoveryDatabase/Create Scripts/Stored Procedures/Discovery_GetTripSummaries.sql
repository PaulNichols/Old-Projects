IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTripSummaries')
	BEGIN
		DROP  Procedure  Discovery_GetTripSummaries
	END

GO

CREATE Procedure Discovery_GetTripSummaries

(
	@WarehouseId int,
	@RequiredDeliveryDateFrom datetime,
	@RequiredDeliveryDateTo datetime
	
)

AS

SELECT     
	Discovery_Trip.TripNumber, 
	Discovery_Trip.Id,
	Discovery_Trip.StartDate, 
	VehicleRegistration , 
	Discovery_Trip.LeaveTime, 
	Discovery_Trip.FinishTime, 
	Discovery_Trip.ItemCount, 
	SUM(CollectionWeight*1.05) AS CollectionWeight, 
	SUM(DeliveryWeight*1.05) AS DeliveryWeight, 
	SUM(CollectionVolume) AS CollectionVolume, 
	SUM(DeliveryVolume) AS DeliveryVolume, 
	Discovery_Trip.TotalDistance
FROM         
	Discovery_Trip 
GROUP BY 
	Discovery_Trip.TripNumber, 
	Discovery_Trip.StartDate, 
	VehicleRegistration ,
	Discovery_Trip.WarehouseId,
	Discovery_Trip.Id,
	Discovery_Trip.LeaveTime, 
	Discovery_Trip.FinishTime ,
	Discovery_Trip.TotalDistance,
	Discovery_Trip.ItemCount
HAVING      
	((@RequiredDeliveryDateTo is null and Discovery_Trip.StartDate=@RequiredDeliveryDateFrom) or (Discovery_Trip.StartDate between @RequiredDeliveryDateFrom and @RequiredDeliveryDateTo)) AND 
	(Discovery_Trip.WarehouseId = @WarehouseId)
GO
