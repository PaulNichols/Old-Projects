IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveCommanderProduct')
	BEGIN
		DROP  Procedure  Discovery_SaveCommanderProduct
	END

GO

CREATE Procedure Discovery_SaveCommanderProduct

	(
		@Id						int= null,
		@ProductCode			varchar(20),
		@Description			varchar(40),
		@ShortDescription		varchar(30),
		@Account				varchar(20),
		@UOM					varchar(10),
		@UpdatedBy				varchar(256)
	)


AS

IF @Id =-1
	BEGIN
		INSERT INTO
			Discovery_CommanderProduct
			(
				ProductCode,
				Description,
				ShortDescription,
				Account,
				UOM,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@ProductCode,
				@Description,
				@ShortDescription,
				@Account,
				@UOM,
				getdate(),
				@UpdatedBy 
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_CommanderProduct
		SET
			ProductCode			=	@ProductCode,
			Description			=	@Description,
			ShortDescription	=	@ShortDescription,
			Account				=	@Account,			
			UpdatedDate			=	getdate(),
			UpdatedBy			=	@UpdatedBy 
		WHERE
			Id=@Id 
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


