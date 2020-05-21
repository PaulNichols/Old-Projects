IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveCommanderSalesOrder')
	BEGIN
		DROP  Procedure  Discovery_SaveCommanderSalesOrder
	END

GO

CREATE Procedure Discovery_SaveCommanderSalesOrder

	(
		@Id						int= null,
		@Site					varchar(10),
        @OrderReference			varchar(20),
        @CustomerNumber			varchar(20),
        @DespatchRouteCode		varchar(50),
        @DropNumber				int,
		@TotalWeight			numeric(18,0),
        @DeliveryAddressLine1	varchar(32),
		@DeliveryAddressLine2	varchar(32),
		@DeliveryAddressLine3	varchar(32),
		@DeliveryAddressLine4	varchar(32),
		@DeliveryAddressLine5	varchar(32),
		@CustomerOrderReference varchar(32),
        @Carrier				varchar(4),
        @CustomerType			varchar(3),
		@UpdatedBy				varchar(256),
		@CheckSum				int
	)


AS

IF @Id =-1
	BEGIN
		INSERT INTO
			Discovery_CommanderSalesOrder
			(
				Site,
				OrderReference,
				CustomerNumber,
				DespatchRouteCode,
				DropNumber,
				TotalWeight,
				DeliveryAddress1,
				DeliveryAddress2,
				DeliveryAddress3,
				DeliveryAddress4,
				DeliveryAddress5,
				CustomerOrderReference,
				Carrier,
				CustomerType,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Site,
				@OrderReference,
				@CustomerNumber,
				@DespatchRouteCode,
				@DropNumber,
				@TotalWeight,
				@DeliveryAddressLine1,
				@DeliveryAddressLine2,
				@DeliveryAddressLine3,
				@DeliveryAddressLine4,
				@DeliveryAddressLine5,
				@CustomerOrderReference,
				@Carrier,
				@CustomerType,
				getdate(),
				@UpdatedBy 
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_CommanderSalesOrder
		SET
			Site					=	@Site,
			OrderReference			=	@OrderReference,
			CustomerNumber			=	@CustomerNumber,
			DespatchRouteCode		=	@DespatchRouteCode,
			DropNumber				=	@DropNumber,
			TotalWeight				=	@TotalWeight,
			DeliveryAddress1		=	@DeliveryAddressLine1,
			DeliveryAddress2		=	@DeliveryAddressLine2,
			DeliveryAddress3		=	@DeliveryAddressLine3,
			DeliveryAddress4		=	@DeliveryAddressLine4,
			DeliveryAddress5		=	@DeliveryAddressLine5,
			CustomerOrderReference	=	@CustomerOrderReference,
			Carrier					=	@Carrier,
			CustomerType			=	@CustomerType,
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


