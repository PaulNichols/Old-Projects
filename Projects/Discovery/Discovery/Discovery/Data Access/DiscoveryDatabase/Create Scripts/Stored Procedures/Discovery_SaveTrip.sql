IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveTrip')
	BEGIN
		DROP  Procedure  Discovery_SaveTrip
	END

GO

CREATE Procedure Discovery_SaveTrip
(
	
	@Id int,
	@WarehouseId int,
	@TripNumber varchar(8),
    @AssignedDriver varchar(16),    
    @LeaveTime char(5),   
	@FinishTime char(5),   
    @DeliveryCount int,
	@CollectionCount int,
	@DeliveryWeight numeric(9,2),
	@DeliveryVolume  numeric(7,3),
	@CollectionWeight numeric(9,2),
	@CollectionVolume  numeric(7,3),
	@PeakWeight numeric(9,2),
	@PeakVolume numeric(7,3),
	@Feasible int,
	@ItemCount int,
	@TotalDistance numeric(5,1),
	@TravellingTime char(5),   
	@WaitingTime char(5),   
	@LoadingTime char(5),   
	@TotalTime char(5),   
	@VehicleRegistration varchar(16),
	@MaximumLoadWeight numeric(9,2),
	@MaximumLoadVolume numeric(7,3),
	@VehicleCost money,
	@RegionId int,
	@DropsOnTrip int,
	@StartDate datetime,
	@CheckSum int
)


AS


IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_Trip
			(
				WarehouseId,
				TripNumber,
				AssignedDriver ,    
				LeaveTime,   
				FinishTime,   
				DeliveryCount ,
				CollectionCount ,
				DeliveryWeight,
				DeliveryVolume,
				CollectionWeight ,
				CollectionVolume,
				PeakWeight,
				PeakVolume ,
				Feasible ,
				ItemCount ,
				TotalDistance,
				TravellingTime,   
				WaitingTime,   
				LoadingTime,   
				TotalTime,   
				VehicleRegistration,
				MaximumLoadWeight,
				MaximumLoadVolume,
				VehicleCost ,
				RegionId ,
				DropsOnTrip ,
				StartDate 
			)
		VALUES
			(
				@WarehouseId,
				@TripNumber,
				@AssignedDriver ,    
				@LeaveTime,   
				@FinishTime,   
				@DeliveryCount ,
				@CollectionCount ,
				@DeliveryWeight,
				@DeliveryVolume,
				@CollectionWeight ,
				@CollectionVolume,
				@PeakWeight,
				@PeakVolume ,
				@Feasible ,
				@ItemCount ,
				@TotalDistance,
				@TravellingTime,   
				@WaitingTime,   
				@LoadingTime,   
				@TotalTime,   
				@VehicleRegistration,
				@MaximumLoadWeight,
				@MaximumLoadVolume,
				@VehicleCost ,
				@RegionId ,
				@DropsOnTrip ,
				@StartDate 
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_Trip
		SET
			WarehouseId			=	@WarehouseId,
			TripNumber			=	@TripNumber,
			AssignedDriver		=	@AssignedDriver ,    
			LeaveTime			=	@LeaveTime,   
			FinishTime			=	@FinishTime,   
			DeliveryCount		=	@DeliveryCount,
			CollectionCount		=	@CollectionCount ,
			DeliveryWeight		=	@DeliveryWeight,
			DeliveryVolume		=	@DeliveryVolume,
			CollectionWeight	=	@CollectionWeight ,
			CollectionVolume	=	@CollectionVolume,
			PeakWeight			=	@PeakWeight,
			PeakVolume			=	@PeakVolume,
			Feasible			=	@Feasible,
			ItemCount			=	@ItemCount,
			TotalDistance		=	@TotalDistance,
			TravellingTime		=	@TravellingTime,   
			WaitingTime			=	@WaitingTime,   
			LoadingTime			=	@LoadingTime,   
			TotalTime			=	@TotalTime,   
			VehicleRegistration	=	@VehicleRegistration,
			MaximumLoadWeight	=	@MaximumLoadWeight,
			MaximumLoadVolume	=	@MaximumLoadVolume,
			VehicleCost			=	@VehicleCost,
			RegionId			=	@RegionId,
			DropsOnTrip			=	@DropsOnTrip,
			StartDate			=	@StartDate
		WHERE
			Id=@Id and
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


