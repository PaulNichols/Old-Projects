IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_SaveDownTime')
	BEGIN
		DROP  Procedure  Integration_SaveDownTime
	END

GO

CREATE Procedure Integration_SaveDownTime

	(
		@Id int= null,
		@DayOfWeek int,
		@StartTime datetime,
		@EndTime datetime,
		@ConnectionId int,
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Integration_DownTime
			(
				DayOfWeek,
				StartTime,
				EndTime,
				ConnectionId,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@DayOfWeek,
				@StartTime,
				@EndTime,
				@ConnectionId,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Integration_DownTime
		SET
				DayOfWeek		=	@DayOfWeek,
				StartTime		=	@StartTime,
				EndTime			=	@EndTime,
				ConnectionId	=	@ConnectionId,
				UpdatedDate		=	getdate(),
				UpdatedBy		=	@UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


