IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveWarehouse')
	BEGIN
		DROP  Procedure  Discovery_SaveWarehouse
	END

GO

CREATE Procedure Discovery_SaveWarehouse

	(
		@Id int= null,
		@Code varchar(10),
		@Description varchar(256),
		@PrinterName varchar(50),
		@SalesEmail varchar(50),
		@HasOptrak bit,
		@HasCommander bit,
		--@PrinterName varchar(100),
		@ContactName varchar(50),
		@ContactTelephone varchar(50),
		@AddressLine1 varchar(50),
		@AddressLine2 varchar(50),
		@AddressLine3 varchar(50),
		@AddressLine4 varchar(50),
		@PostCode varchar(15),
		@IsTDC bit,
		@RegionId int,
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_Warehouse
			(
				Code,
				Description,
				PrinterName,
				SalesEmail ,
				HasOptrak ,
				HasCommander ,
				--PrinterName ,
				ContactName ,
				ContactTelephone ,
				AddressLine1 ,
				AddressLine2 ,
				AddressLine3 ,
				AddressLine4 ,
				PostCode ,
				IsTDC ,
				RegionId ,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Code,
				@Description,
				@PrinterName,
				@SalesEmail ,
				@HasOptrak ,
				@HasCommander ,
				--@PrinterName ,
				@ContactName ,
				@ContactTelephone ,
				@AddressLine1 ,
				@AddressLine2 ,
				@AddressLine3 ,
				@AddressLine4 ,
				@PostCode ,
				@IsTDC ,
				@RegionId ,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_Warehouse
		SET
				Code				=	@Code,
				Description			=	@Description,
				PrinterName			=	@PrinterName,
				SalesEmail			=	@SalesEmail,
				HasOptrak			=	@HasOptrak,
				HasCommander		=	@HasCommander,
				--PrinterName			=	@PrinterName ,
				ContactName			=	@ContactName ,
				ContactTelephone	=	@ContactTelephone ,
				AddressLine1		=	@AddressLine1,
				AddressLine2		=	@AddressLine2,
				AddressLine3		=	@AddressLine3,
				AddressLine4		=	@AddressLine4,
				PostCode			=	@PostCode ,
				IsTDC				=	@IsTDC,
				RegionId			=	@RegionId,
				UpdatedDate			=	getdate(),
				UpdatedBy			=	@UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


