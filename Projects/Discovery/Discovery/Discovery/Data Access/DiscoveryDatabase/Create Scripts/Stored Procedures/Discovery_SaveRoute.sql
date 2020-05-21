IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveRoute')
	BEGIN
		DROP  Procedure  Discovery_SaveRoute
	END

GO

CREATE Procedure Discovery_SaveRoute

	(
		@Id int = null,
		@Code varchar(10),
		@Description varchar(256),
		@WarehouseId int,
		@IsSameDay bit,
		@IsNextDay bit,
		@IsCollection bit,
		@IsSpecial bit,
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_Route
			(
				Code,
				Description,
				WarehouseId,
				IsSameDay ,
				IsNextDay ,
				IsCollection ,
				IsSpecial,
				UpdatedDate,
				UpdatedBy		
			)
		VALUES
			(
				@Code,
				@Description,
				@WarehouseId,
				@IsSameDay ,
				@IsNextDay ,
				@IsCollection ,
				@IsSpecial,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_Route
		SET
				Code			=	@Code,
				Description		=	@Description,
				WarehouseId		=	@WarehouseId,
				IsSameDay		=	@IsSameDay,
				IsNextDay		=	@IsNextDay,
				IsCollection	=	@IsCollection,
				IsSpecial		=	@IsSpecial,
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


