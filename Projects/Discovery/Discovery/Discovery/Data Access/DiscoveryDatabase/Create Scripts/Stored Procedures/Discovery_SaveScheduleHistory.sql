IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveScheduleHistory')
	BEGIN
		DROP  Procedure  Discovery_SaveScheduleHistory
	END

GO

CREATE Procedure Discovery_SaveScheduleHistory

	(
		@Id int= null,
		@ScheduleId int,
		@StartDate datetime,
		@EndDate datetime,
		@Succeeded bit,
		@LogNotes ntext,
		@NextStart datetime,
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_ScheduleHistory
			(
			ScheduleId,
			StartDate,
			UpdatedDate,
			UpdatedBy
			)
		VALUES
			(
				@ScheduleId,
				@StartDate,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_ScheduleHistory
		SET
				EndDate = @EndDate,
				Succeeded = @Succeeded,
				LogNotes = @LogNotes,
				NextStart = @NextStart,
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


