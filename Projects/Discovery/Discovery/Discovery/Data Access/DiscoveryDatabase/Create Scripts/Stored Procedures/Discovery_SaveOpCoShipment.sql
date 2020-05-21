IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveOpCoShipment')
	BEGIN
		DROP  Procedure  Discovery_SaveOpCoShipment
	END

GO

CREATE Procedure Discovery_SaveOpCoShipment

	(
		@Id int= null,
		@OpCoCode varchar(3),
		@OpCoSequenceNumber int,
		@OpCoContactEmail varchar(50),
		@OpCoContactName varchar(50),
		@DespatchNumber varchar(50),
		@RequiredShipmentDate datetime,
		@TransactionTypeCode varchar(10),
		@CustomerReference varchar(50),
		@Instructions varchar(250),
		@RouteCode varchar(10),
		@CustomerNumber varchar(50),
		@CustomerName varchar(50),
		@CustomerAddress1 varchar(50),
		@CustomerAddress2 varchar(50),
		@CustomerAddress3 varchar(50),
		@CustomerAddress4 varchar(50),
		@CustomerAddress5 varchar(50),
		@CustomerPostCode varchar(15),
		@ShipmentNumber varchar(50),
		@ShipmentName varchar(50),
		@ShipmentAddress1 varchar(50),
		@ShipmentAddress2 varchar(50),
		@ShipmentAddress3 varchar(50),
		@ShipmentAddress4 varchar(50),
		@ShipmentAddress5 varchar(50),
		@ShipmentPostCode varchar(15),
		@ShipmentContactName varchar(50), 
		@ShipmentContactTel varchar(50),
		@ShipmentContactEmail varchar(50),
		@SalesBranchCode varchar(50),
		@AfterTime varchar(8),
		@BeforeTime varchar(8),
		@TailLiftRequired bit,
		@VehicleMaxWeight numeric(7, 2),
		@CheckInTime int,
		@DeliveryWarehouseCode varchar(10),
		@StockWarehouseCode varchar(10),
		@DivisionCode varchar(3),
		@GeneratedDateTime datetime,
		@Status int,
		@AuditId int = null,
		@UpdatedBy varchar(256),
		@CheckSum int
	)

AS

IF @Id =-1
	BEGIN
		INSERT INTO
			Discovery_OpCoShipment
			(
				OpCoCode,
				OpCoSequenceNumber,
				OpCoContactEmail,
				OpCoContactName,
				DespatchNumber,
				RequiredShipmentDate,
				TransactionTypeCode,
				CustomerReference,
				Instructions,
				RouteCode,
				CustomerNumber,
				CustomerName,
				CustomerAddress1,
				CustomerAddress2,
				CustomerAddress3,
				CustomerAddress4,
				CustomerAddress5,
				CustomerPostCode,
				ShipmentNumber,
				ShipmentName,
				ShipmentAddress1,
				ShipmentAddress2,
				ShipmentAddress3,
				ShipmentAddress4,
				ShipmentAddress5,
				ShipmentPostCode,
				ShipmentContactName,
				ShipmentContactTel,
				ShipmentContactEmail,
				SalesBranchCode,
				AfterTime,
				BeforeTime,
				TailLiftRequired,
				VehicleMaxWeight,
				CheckInTime,
				DeliveryWarehouseCode,
				StockWarehouseCode,
				DivisionCode,
				GeneratedDateTime,
				Status,
				AuditId,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@OpCoCode,
				@OpCoSequenceNumber,
				@OpCoContactEmail,
				@OpCoContactName,
				@DespatchNumber,
				@RequiredShipmentDate,
				@TransactionTypeCode,
				@CustomerReference,
				@Instructions,
				@RouteCode,
				@CustomerNumber,
				@CustomerName,
				@CustomerAddress1,
				@CustomerAddress2,
				@CustomerAddress3,
				@CustomerAddress4,
				@CustomerAddress5,
				@CustomerPostCode,
				@ShipmentNumber,
				@ShipmentName,
				@ShipmentAddress1,
				@ShipmentAddress2,
				@ShipmentAddress3,
				@ShipmentAddress4,
				@ShipmentAddress5,
				@ShipmentPostCode,
				@ShipmentContactName,
				@ShipmentContactTel,
				@ShipmentContactEmail,
				@SalesBranchCode,
				@AfterTime,
				@BeforeTime,
				@TailLiftRequired,
				@VehicleMaxWeight,
				@CheckInTime,
				@DeliveryWarehouseCode,
				@StockWarehouseCode,
				@DivisionCode,
				@GeneratedDateTime,
				@Status,
				@AuditId,
				getdate(),
				@UpdatedBy
			)
		
		SELECT cast(@@IDENTITY  as int)
	END
ELSE
	BEGIN
		UPDATE
			Discovery_OpCoShipment
		SET
				OpCoCode=@OpCoCode,
				OpCoSequenceNumber=@OpCoSequenceNumber,
				OpCoContactEmail=@OpCoContactEmail,
				OpCoContactName=@OpCoContactName,
				DespatchNumber=@DespatchNumber,
				RequiredShipmentDate=@RequiredShipmentDate,
				TransactionTypeCode=@TransactionTypeCode,
				CustomerReference=@CustomerReference,
				Instructions=@Instructions,
				RouteCode=@RouteCode,
				CustomerNumber=@CustomerNumber,
				CustomerName=@CustomerName,
				CustomerAddress1=@CustomerAddress1,
				CustomerAddress2=@CustomerAddress2,
				CustomerAddress3=@CustomerAddress3,
				CustomerAddress4=@CustomerAddress4,
				CustomerAddress5=@CustomerAddress5,
				CustomerPostCode=@CustomerPostCode,
				ShipmentNumber=@ShipmentNumber,
				ShipmentName=@ShipmentName,
				ShipmentAddress1=@ShipmentAddress1,
				ShipmentAddress2=@ShipmentAddress2,
				ShipmentAddress3=@ShipmentAddress3,
				ShipmentAddress4=@ShipmentAddress4,
				ShipmentAddress5=@ShipmentAddress5,
				ShipmentPostCode=@ShipmentPostCode,
				ShipmentContactName=@ShipmentContactName,
				ShipmentContactTel=@ShipmentContactTel,
				ShipmentContactEmail=@ShipmentContactEmail,
				SalesBranchCode=@SalesBranchCode,
				AfterTime=@AfterTime,
				BeforeTime=@BeforeTime,
				TailLiftRequired=@TailLiftRequired,
				VehicleMaxWeight=@VehicleMaxWeight,
				CheckInTime=@CheckInTime,
				DeliveryWarehouseCode=@DeliveryWarehouseCode,
				StockWarehouseCode=@StockWarehouseCode,
				DivisionCode=@DivisionCode,
				GeneratedDateTime=@GeneratedDateTime,
				Status=@Status,
				AuditId = @AuditId,
				UpdatedDate=getdate(),
				UpdatedBy=@UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum

		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
	
GO


