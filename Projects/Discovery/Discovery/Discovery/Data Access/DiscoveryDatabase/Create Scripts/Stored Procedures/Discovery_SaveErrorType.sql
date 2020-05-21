IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveErrorType')
	BEGIN
		DROP  Procedure  Discovery_SaveErrorType
	END

GO

Create Procedure Discovery_SaveErrorType

	(
		@Id int,
		@Policy varchar(256),
		@EmailOperator bit,
		@Prority int,
		@RequiresAcknowledgement bit,
		@ExceptionType varchar(256),
		@HasEmailHandler bit,
		@EmailRecipients varchar(256) ,
		@EmailSubject varchar(256) ,
		@OpCoId int,
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_ErrorType
			(
				Policy,
				EmailOperator,
				Prority,
				RequiresAcknowledgement,
				ExceptionType,
				EmailSubject
			)
		VALUES
			(
				@Policy,
				@EmailOperator,
				@Prority,
				@RequiresAcknowledgement,
				@ExceptionType,
				@EmailSubject
			)
			
		Declare @IDENTITY int
		SET @IDENTITY=@@IDENTITY
		if @IDENTITY>0 
			BEGIN
				INSERT INTO
					Discovery_ErrorOpcoSettings
					(
						EmailRecipients,
						OpCoId,
						ErrorTypeId
					)
				VALUES
					(
						@EmailRecipients,
						@OpCoId,
						@IDENTITY
					)
			END
		SELECT @IDENTITY 
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_ErrorType
		SET
				Policy					=	@Policy,
				EmailOperator			=	@EmailOperator,
				Prority					=	@Prority,
				RequiresAcknowledgement	=	@RequiresAcknowledgement,
				ExceptionType			=	@ExceptionType,
				EmailSubject			=	@EmailSubject
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			BEGIN
				IF    EXISTS(SELECT Id FROM Discovery_ErrorOpcoSettings WHERE OpCoId=@OpCoId AND ErrorTypeId=@Id)
				--update the Discovery_ErrorOpcoSettings table
					BEGIN
						UPDATE
							Discovery_ErrorOpcoSettings
						SET
							EmailRecipients=@EmailRecipients
						WHERE
							OpCoId=@OpCoId AND
							ErrorTypeId=@Id
					END
				ELSE
					--insert into the Discovery_ErrorOpcoSettings table
					BEGIN
						 
							INSERT INTO
								Discovery_ErrorOpcoSettings
								(
									EmailRecipients,
									OpCoId,
									ErrorTypeId
								)
							VALUES
								(
									@EmailRecipients,
									@OpCoId,
									@Id
								)
			
					END
					
				IF @@ROWCOUNT=1
					SELECT @Id 
				ELSE
					SELECT -1
			END
			
		ELSE
			SELECT -1
	END




GO


