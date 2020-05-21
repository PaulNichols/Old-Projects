IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveNonWorkingDay')
	BEGIN
		DROP  Procedure  Discovery_SaveNonWorkingDay
	END

GO

CREATE Procedure Discovery_SaveNonWorkingDay

	(
		@Id int= null,
		@NonWorkingDate datetime,
		@Description varchar(256),
		@WarehouseId int,
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

DECLARE @ERROR int

IF @Id = -1
	BEGIN
		INSERT INTO
			Discovery_NonWorkingDay
			(
				NonWorkingDate,
				Description,
				WarehouseId,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@NonWorkingDate,
				@Description,
				@WarehouseId,
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
			Discovery_NonWorkingDay
		SET
				NonWorkingDate	=	@NonWorkingDate,
				Description	=	@Description,
				WarehouseId	=	@WarehouseId,
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

