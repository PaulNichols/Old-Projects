IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveRoutingHistory')
	BEGIN
		DROP  Procedure  Discovery_SaveRoutingHistory
	END

GO

CREATE Procedure Discovery_SaveRoutingHistory
	(
		@Id int= null,
		@RegionId int,
		@DropFileReceivedDate datetime,
		@TripPartFileReceivedDate datetime,
		@TripFileReceivedDate datetime,
		@SentDate datetime,
		@ProcessStartedDate datetime,
		@ResetDate datetime,
		@ProcessedBy varchar(256),
		@ResetBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_RoutingHistory
			(
				RegionId ,
				SentDate ,
				ProcessStartedDate ,
				ResetDate ,
				ProcessedBy ,
				ResetBy ,
				TripFileReceivedDate,
				DropFileReceivedDate,
				TripPartFileReceivedDate
			)
		VALUES
			(
				@RegionId ,
				@SentDate ,
				@ProcessStartedDate ,
				@ResetDate ,
				@ProcessedBy ,
				@ResetBy ,
				@TripFileReceivedDate ,
				@DropFileReceivedDate,
				@TripPartFileReceivedDate				
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_RoutingHistory
		SET
				RegionId =@RegionId,
				SentDate =@SentDate,
				ProcessStartedDate =@ProcessStartedDate,
				ResetDate =@ResetDate,
				ProcessedBy =@ProcessedBy,
				ResetBy =@ResetBy,
				TripFileReceivedDate=@TripFileReceivedDate,
				DropFileReceivedDate=@DropFileReceivedDate,
				TripPartFileReceivedDate=@TripPartFileReceivedDate
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


