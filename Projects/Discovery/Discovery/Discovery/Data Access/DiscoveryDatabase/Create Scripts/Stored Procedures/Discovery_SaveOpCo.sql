IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveOpCo')
	BEGIN
		DROP  Procedure  Discovery_SaveOpCo
	END

GO

CREATE Procedure Discovery_SaveOpCo

	(
		@Id int= null,
		@Code varchar(3),
		@Description varchar(256),
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id =-1
	BEGIN
		INSERT INTO
			Discovery_OpCo
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
			Discovery_OpCo
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


