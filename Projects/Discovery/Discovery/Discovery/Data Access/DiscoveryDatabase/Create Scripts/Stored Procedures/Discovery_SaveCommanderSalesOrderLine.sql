IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveCommanderSalesOrderLine')
	BEGIN
		DROP  Procedure  Discovery_SaveCommanderSalesOrderLine
	END

GO

CREATE Procedure Discovery_SaveCommanderSalesOrderLine

	(
		@Id							int= null,
		@Site						varchar(10),
        @OrderReference				varchar(20),
        @LineNumber					int,
        @ProductCode				varchar(20),
        @QuantityOrdered			int,
		@CustomerReferenceNumber	varchar(20),
		@UOM						varchar(50),
        @SpecialInstructions1		varchar(33),
		@SpecialInstructions2		varchar(33),
		@SpecialInstructions3		varchar(33),
		@SpecialInstructions4		varchar(33),
		@SpecialInstructions5		varchar(33),
		@CommanderSalesOrderId		int,
		@UpdatedBy					varchar(256),
		@CheckSum					int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_CommanderSalesOrderLine
			(
				Site,
				OrderReference,
				LineNumber,
				ProductCode,
				QuantityOrdered,
				CustomerReferenceNumber,
				SpecialInstructions1,
				SpecialInstructions2,
				SpecialInstructions3,
				SpecialInstructions4,
				SpecialInstructions5,
				CommanderSalesOrderId,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Site,
				@OrderReference,
				@LineNumber,
				@ProductCode,
				@QuantityOrdered,
				@CustomerReferenceNumber,
				@SpecialInstructions1,
				@SpecialInstructions2,
				@SpecialInstructions3,
				@SpecialInstructions4,
				@SpecialInstructions5,
				@CommanderSalesOrderId,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_CommanderSalesOrderLine
		SET
			Site					=	@Site,
			OrderReference			=	@OrderReference,
			LineNumber				=	@LineNumber,
			ProductCode				=	@ProductCode,
			QuantityOrdered			=	@QuantityOrdered,
			CustomerReferenceNumber	=	@CustomerReferenceNumber,
			SpecialInstructions1	=	@SpecialInstructions1,
			SpecialInstructions2	=	@SpecialInstructions2,
			SpecialInstructions3	=	@SpecialInstructions3,
			SpecialInstructions4	=	@SpecialInstructions4,
			SpecialInstructions5	=	@SpecialInstructions5,
			CommanderSalesOrderId	=	@CommanderSalesOrderId,
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


