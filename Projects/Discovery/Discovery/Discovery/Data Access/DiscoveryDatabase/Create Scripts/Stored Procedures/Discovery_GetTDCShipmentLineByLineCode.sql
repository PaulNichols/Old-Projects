IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentLineByLineCode')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentLineByLineCode
		
	END

GO

CREATE Procedure Discovery_GetTDCShipmentLineByLineCode
	(
		@lineCode int, 
		@opcoCode varchar(3), 
		@shipmentNumber varchar(50), 
		@despatchNumber varchar(50)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(
	   Discovery_ShipmentLine.Id
      ,Discovery_ShipmentLine.ShipmentId
      ,Discovery_ShipmentLine.ConversionInstructions
      ,Discovery_ShipmentLine.ConversionQuantity
      ,Discovery_ShipmentLine.CustomerReference
      ,Discovery_ShipmentLine.Description1
      ,Discovery_ShipmentLine.Description2
      ,Discovery_ShipmentLine.Exceptions
      ,Discovery_ShipmentLine.Grammage
      ,Discovery_ShipmentLine.IsISO9000Approved
      ,Discovery_ShipmentLine.IsPanel
      ,Discovery_ShipmentLine.Length
      ,Discovery_ShipmentLine.LineNumber
      ,Discovery_ShipmentLine.LoadCategoryCode
      ,Discovery_ShipmentLine.Microns
      ,Discovery_ShipmentLine.Packing
      ,Discovery_ShipmentLine.ProductCode
      ,Discovery_ShipmentLine.ProductGroup
      ,Discovery_ShipmentLine.Quantity
      ,Discovery_ShipmentLine.OriginalQuantity
      ,Discovery_ShipmentLine.QuantityUnit
      ,Discovery_ShipmentLine.Volume
      ,Discovery_ShipmentLine.Width
      ,Discovery_ShipmentLine.NetWeight
      ,Discovery_ShipmentLine.UpdatedDate
      ,Discovery_ShipmentLine.UpdatedBy
	) as CheckSum
FROM
	Discovery_ShipmentLine join Discovery_Shipment on Discovery_ShipmentLine.ShipmentId=Discovery_Shipment.Id
WHERE
	LineNumber = @lineCode and 
	Discovery_Shipment.OpCoCode=@opcoCode and
	Discovery_Shipment.ShipmentNumber=@shipmentNumber and
	Discovery_Shipment.DespatchNumber=@despatchNumber 


GO

