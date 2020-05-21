IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveDrop')
	BEGIN
		DROP  Procedure  Discovery_SaveDrop
	END

GO

CREATE Procedure Discovery_SaveDrop

	(
		@Id int,
		@TripId int,
		@OrderSequence int,
		@shipmentId int,
		@weight decimal(9, 2),
		@volume decimal(7, 3),
		@arriveTime char(5),
		@departTime char(5),	
		@loadingTime char(5)	,
		@waitingTime char(5),
		@travellingTime char(5),
		@distance decimal(5, 2),
		@callType int,
		@dropSequenceNumber int,
		@originalDepotId int
		
	)

AS


IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_Drop
			(
				tripId ,
				OrderSequence ,
				shipmentId ,
				weight,
				volume,
				arriveTime ,
				departTime ,	
				loadingTime 	,
				waitingTime ,
				travellingTime ,
				distance ,
				callType ,
				dropSequence ,
				originalDepotId
			)
		VALUES
			(
				@tripId ,
				@ordersequence ,
				@shipmentId ,
				@weight,
				@volume,
				@arriveTime ,
				@departTime ,	
				@loadingTime 	,
				@waitingTime ,
				@travellingTime ,
				@distance ,
				@callType ,
				@dropSequenceNumber ,
				@originalDepotId
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_Drop
		SET
			tripId			=	@tripId,
			OrderSequence	=	@ordersequence,
			shipmentId		=	@shipmentId,
			arriveTime		=	@arriveTime,
			departTime		=	@departTime,	
			loadingTime		=	@loadingTime 	,
			waitingTime		=	@waitingTime,
			travellingTime	=	@travellingTime,
			distance		=	@distance,
			callType		=	@callType,
			dropSequence	=	@dropSequenceNumber,
			originalDepotId	=	@originalDepotId
		WHERE
			Id=@Id 
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


