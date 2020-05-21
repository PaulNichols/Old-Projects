IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveTransactionType')
	BEGIN
		DROP  Procedure  Discovery_SaveTransactionType
	END

GO

CREATE Procedure Discovery_SaveTransactionType

	(
		@Id int= null,
		@Code varchar(10),
		@Description varchar(256),
		@IsStock bit = 0,
		@IsNonStock bit = 0,
		@IsCollection bit = 0,
		@IsSample bit = 0,		
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_TransactionType
			(
				Code,
				Description,
				IsStock,
				IsNonStock,
				IsCollection,
				IsSample,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Code,
				@Description,
				@IsStock,
				@IsNonStock,
				@IsCollection,
				@IsSample,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_TransactionType
		SET
				Code		=	@Code,
				Description	=	@Description,
				IsStock		=	@IsStock,
				IsNonStock	=	@IsNonStock,
				IsCollection=	@IsCollection,
				IsSample	=	@IsSample,
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


