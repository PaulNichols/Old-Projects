IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_SaveTask')
	BEGIN
		DROP  Procedure  Integration_SaveTask
	END

GO

CREATE Procedure Integration_SaveTask

	(
		@Id int= null,
		@Name varchar(256),
		@RemoveDataFile bit,
		@RemoveFlagFile bit,
		@DataFileExtension varchar(5),
		@FlagFileExtension varchar(5),
		@MonitorSequenceNumber bit,
		@SequenceNumber int,
		@SourceDirectory varchar(256),
		@DestinationDirectory varchar(256),
		@SourceConnectionId int,
		@SourceConnectionIdentifier varchar(256),
		@DestinationConnectionId int,
		@DestinationConnectionIdentifier varchar(256),
		@Frequency int,
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Integration_Task
			(
				[Name],
				RemoveDataFile,
				RemoveFlagFile ,
				DataFileExtension ,
				FlagFileExtension ,
				MonitorSequenceNumber ,
				SequenceNumber ,
				SourceDirectory ,
				DestinationDirectory ,
				SourceConnectionId ,
				SourceConnectionIdentifier	,
				DestinationConnectionId ,
				DestinationConnectionIdentifier ,
				Frequency,
				UpdatedDate,
				UpdatedBy 
			)
		VALUES
			(
				@Name,
				@RemoveDataFile,
				@RemoveFlagFile ,
				@DataFileExtension ,
				@FlagFileExtension ,
				@MonitorSequenceNumber ,
				@SequenceNumber ,
				@SourceDirectory ,
				@DestinationDirectory ,
				@SourceConnectionId ,
				@SourceConnectionIdentifier	,
				@DestinationConnectionId ,
				@DestinationConnectionIdentifier ,
				@Frequency,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Integration_Task
		SET
				[Name]							=	@Name,
				RemoveDataFile					=	@RemoveDataFile,
				RemoveFlagFile					=	@RemoveFlagFile ,
				DataFileExtension				=	@DataFileExtension,
				FlagFileExtension				=	@FlagFileExtension ,
				MonitorSequenceNumber			=	@MonitorSequenceNumber,
				SequenceNumber					=	@SequenceNumber ,
				SourceDirectory					=	@SourceDirectory,
				DestinationDirectory			=	@DestinationDirectory ,
				SourceConnectionId				=	@SourceConnectionId,
				SourceConnectionIdentifier		=	@SourceConnectionIdentifier	,
				DestinationConnectionId			=	@DestinationConnectionId,
				DestinationConnectionIdentifier	=	@DestinationConnectionIdentifier ,
				Frequency						=	@Frequency,
				UpdatedDate						=	getdate(),
				UpdatedBy						=	@UpdatedBy
		WHERE
			Id=@Id --AND
			--BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


