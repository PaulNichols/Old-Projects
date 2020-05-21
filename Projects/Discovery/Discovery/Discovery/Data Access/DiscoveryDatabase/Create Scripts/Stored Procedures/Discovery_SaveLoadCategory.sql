IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveLoadCategory')
	BEGIN
		DROP  Procedure  Discovery_SaveLoadCategory
	END

GO

CREATE Procedure Discovery_SaveLoadCategory

	(
		@Id int= null,
		@Code varchar(10),
		@Description varchar(256),
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_LoadCategory
			(
				Code,
				Description,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Code,
				@Description,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_LoadCategory
		SET
				Code		=	@Code,
				Description	=	@Description,
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


