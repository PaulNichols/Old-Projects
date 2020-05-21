IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveTrunkerDay')
	BEGIN
		DROP  Procedure  Discovery_SaveTrunkerDay
	END

GO

CREATE Procedure Discovery_SaveTrunkerDay

	(
		@Id int= null,
		@SourceWarehouseId int,
		@DestinationWarehouseId int,
		@Days int,
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_TrunkerDay
			(
				SourceWarehouseId,
				DestinationWarehouseId,
				Days,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@SourceWarehouseId,
				@DestinationWarehouseId,
				@Days,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_TrunkerDay
		SET
				SourceWarehouseId		=	@SourceWarehouseId,
				DestinationWarehouseId	=	@DestinationWarehouseId,
				Days					=	@Days,
				UpdatedDate				=	getdate(),
				UpdatedBy				=	@UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


