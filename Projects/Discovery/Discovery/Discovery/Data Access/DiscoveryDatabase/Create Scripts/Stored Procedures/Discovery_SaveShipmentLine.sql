IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveTDCShipmentLine')
	BEGIN
		DROP  Procedure  Discovery_SaveTDCShipmentLine
	END

GO

CREATE Procedure Discovery_SaveTDCShipmentLine
	(
		@Id int = null,
		@ShipmentId int,
		@ConversionInstructions text,
		@ConversionQuantity int,
		@CustomerReference varchar(30),
		@Description1 varchar(50),
		@Description2 varchar(50),
		@Exceptions varchar(50),
		@Grammage int,
		@IsISO9000Approved bit,
		@IsPanel bit,
		@Length int,
		@LineNumber int,
		@LoadCategoryCode varchar(2),
		@Microns int,
		@Packing text,
		@ProductCode varchar(20),
		@ProductGroup varchar(20),
		@Quantity int,
		@OriginalQuantity int,
		@QuantityUnit varchar(15),
		@Volume numeric(11, 2),
		@Width int,
		@NetWeight numeric(9, 2),
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id =-1
	BEGIN
		INSERT INTO
			Discovery_ShipmentLine
			(
				ShipmentId ,
				ConversionInstructions ,
				ConversionQuantity ,
				CustomerReference,
				Description1 ,
				Description2 ,
				Exceptions ,
				Grammage ,
				IsISO9000Approved ,
				IsPanel ,
				Length ,
				LineNumber ,
				LoadCategoryCode ,
				Microns ,
				Packing ,
				ProductCode ,
				ProductGroup ,
				Quantity ,
				OriginalQuantity,
				QuantityUnit ,
				Volume ,
				Width ,
				NetWeight ,
				UpdatedBy,
				UpdatedDate		
			)
		VALUES
			(
				@ShipmentId ,
				@ConversionInstructions ,
				@ConversionQuantity ,
				@CustomerReference,
				@Description1 ,
				@Description2 ,
				@Exceptions ,
				@Grammage ,
				@IsISO9000Approved ,
				@IsPanel ,
				@Length ,
				@LineNumber ,
				@LoadCategoryCode ,
				@Microns ,
				@Packing ,
				@ProductCode ,
				@ProductGroup ,
				@Quantity ,
				@OriginalQuantity,
				@QuantityUnit ,
				@Volume ,
				@Width ,
				@NetWeight ,
				@UpdatedBy,
				getdate()		
			)
		
		SELECT cast(@@IDENTITY  as int)
	END
ELSE
	BEGIN
		UPDATE
			Discovery_ShipmentLine
		SET
			ShipmentId=@ShipmentId,
			ConversionInstructions= @ConversionInstructions,
			ConversionQuantity= @ConversionQuantity,
			CustomerReference=@CustomerReference,
			Description1=@Description1 ,
			Description2=@Description2 ,
			Exceptions= @Exceptions,
			Grammage= @Grammage,
			IsISO9000Approved= @IsISO9000Approved,
			IsPanel= @IsPanel,
			Length=@Length ,
			LineNumber=@LineNumber ,
			LoadCategoryCode=@LoadCategoryCode ,
			Microns=@Microns ,
			Packing= @Packing,
			ProductCode =@ProductCode,
			ProductGroup=@ProductGroup ,
			Quantity=@Quantity ,
			OriginalQuantity=@OriginalQuantity,
			QuantityUnit=@QuantityUnit ,
			Volume=@Volume ,
			Width=@Width ,
			NetWeight=@NetWeight ,
			UpdatedBy=@UpdatedBy,
			UpdatedDate=getdate()
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum

		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
	
GO


