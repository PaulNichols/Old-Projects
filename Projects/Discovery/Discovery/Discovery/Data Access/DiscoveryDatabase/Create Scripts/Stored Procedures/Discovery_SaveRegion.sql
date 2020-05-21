IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveRegion')
	BEGIN
		DROP  Procedure  Discovery_SaveRegion
	END

GO

CREATE Procedure Discovery_SaveRegion

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
			Discovery_Region
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
			Discovery_Region
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


