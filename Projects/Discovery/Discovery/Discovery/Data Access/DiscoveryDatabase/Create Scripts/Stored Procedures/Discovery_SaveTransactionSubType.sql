IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveTransactionSubType')
	BEGIN
		DROP  Procedure  Discovery_SaveTransactionSubType
	END

GO

CREATE Procedure Discovery_SaveTransactionSubType

	(
		@Id int= null,
		@Code varchar(10),
		@Description varchar(256),
		@IsNormal bit = 0,
		@IsTransfer bit = 0,
		@IsLocalConversion bit = 0,
		@Is3rdPartyConversion bit = 0,
		@UpdatedBy varchar(256),
		@CheckSum int
	)

AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_TransactionSubType
			(
				Code,
				Description,
				IsNormal,
				IsTransfer,
				IsLocalConversion,
				Is3rdPartyConversion,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Code,
				@Description,
				@IsNormal,
				@IsTransfer,
				@IsLocalConversion,
				@Is3rdPartyConversion,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_TransactionSubType
		SET
				Code					= @Code,
				Description				= @Description,
				IsNormal				= @IsNormal,
				IsTransfer				= @IsTransfer,
				IsLocalConversion		= @IsLocalConversion,
				Is3rdPartyConversion	= @Is3rdPartyConversion,
				UpdatedDate				= getdate(),
				UpdatedBy				= @UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


