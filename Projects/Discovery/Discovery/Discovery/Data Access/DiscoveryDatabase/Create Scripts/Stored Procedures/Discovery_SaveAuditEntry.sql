IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveAuditEntry')
	BEGIN
		DROP  Procedure  Discovery_SaveAuditEntry
	END

GO

CREATE Procedure Discovery_SaveAuditEntry

	(
		@Id int= null,
		@SourceSystem varchar(256),
		@DestinationSystem varchar(256),
		@ReceivedDate datetime,
		@Message text, 
		@Sequence int,
		@Type varchar(256),
		@Name varchar(256),
		@Label varchar(256),
		@UpdatedBy varchar(256),
		@CheckSum int
	)

AS

DECLARE @ERROR int

IF @Id = -1
	BEGIN
		INSERT INTO
			Discovery_AuditEntry
			(
				SourceSystem ,
				DestinationSystem ,
				ReceivedDate ,
				Message , 
				[Sequence] ,
				Type ,
				[Name] ,
				Label ,
				UpdatedDate,
				UpdatedBy 
				
			)
		VALUES
			(
				@SourceSystem ,
				@DestinationSystem ,
				@ReceivedDate ,
				@Message , 
				@Sequence ,
				@Type ,
				@Name ,
				@Label ,
				getdate(),
				@UpdatedBy
			)

		SELECT @ERROR = @@ERROR
		
		IF @ERROR = 0
		BEGIN
			SELECT cast(@@IDENTITY  as int)
		END
		ELSE
		BEGIN
			SELECT -1
		END
	END
ELSE
	BEGIN
		UPDATE
			Discovery_AuditEntry
		SET
				SourceSystem = @SourceSystem,
				DestinationSystem = @DestinationSystem,
				ReceivedDate = @ReceivedDate,
				Message = @Message, 
				[Sequence] = @Sequence ,
				Type = @Type ,
				[Name] = @Name ,
				Label = @Label ,
				UpdatedDate	=	getdate(),
				UpdatedBy	=	@UpdatedBy
		WHERE
			Id = @Id AND
			BINARY_CHECKSUM(*)=@CheckSum

		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
	
GO



