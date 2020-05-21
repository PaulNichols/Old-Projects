IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveSchedule')
	BEGIN
		DROP  Procedure  Discovery_SaveSchedule
	END

GO

CREATE Procedure Discovery_SaveSchedule

	(
		@Id int= null,
		@TypeFullName varchar(200),
		@TimeLapse int,
		@TimeLapseMeasurement varchar(2),
		@RetryTimeLapse int,
		@RetryTimeLapseMeasurement varchar(2),
		@RetainHistoryNum int,
		@AttachToEvent varchar(50),
		@CatchUpEnabled bit,
		@Enabled bit,
		@ObjectDependencies varchar(300),
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_Schedule
			(
			TypeFullName,
			TimeLapse,
			TimeLapseMeasurement,
			RetryTimeLapse,
			RetryTimeLapseMeasurement,
			RetainHistoryNum,
			AttachToEvent,
			CatchUpEnabled,
			Enabled,
			ObjectDependencies,
			UpdatedDate,
			UpdatedBy
			)
		VALUES
			(
				@TypeFullName,
				@TimeLapse,
				@TimeLapseMeasurement,
				@RetryTimeLapse,
				@RetryTimeLapseMeasurement,
				@RetainHistoryNum,
				@AttachToEvent,
				@CatchUpEnabled,
				@Enabled,
				@ObjectDependencies,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_Schedule
		SET
				TypeFullName = @TypeFullName,
				TimeLapse = @TimeLapse,
				TimeLapseMeasurement = @TimeLapseMeasurement,
				RetryTimeLapse = @RetryTimeLapse,
				RetryTimeLapseMeasurement = @RetryTimeLapseMeasurement,
				RetainHistoryNum = @RetainHistoryNum,
				AttachToEvent = @AttachToEvent,
				CatchUpEnabled = @CatchUpEnabled,
				Enabled = @Enabled,
				ObjectDependencies = @ObjectDependencies,
				UpdatedDate	=	getdate(),
				UpdatedBy	=	@UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


