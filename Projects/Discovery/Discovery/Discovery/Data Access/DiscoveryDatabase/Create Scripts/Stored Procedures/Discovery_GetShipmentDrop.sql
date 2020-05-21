IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetShipmentDrop')
	BEGIN
		DROP  Procedure  Discovery_GetShipmentDrop
	END

GO

CREATE Procedure Discovery_GetShipmentDrop

	(
		@Id int
	)


AS

SELECT  --SUM(Discovery_ShipmentDropLine.Quantity) AS Quantity,
        --SUM(Discovery_ShipmentDropLine.Weight) AS Weight,
        Discovery_Drop.Weight,
        --SUM(Discovery_ShipmentDropLine.Volume) AS Volume,
        Discovery_Drop.Volume,
        Discovery_Drop.DepartTime,
        Discovery_Drop.ArriveTime,
        Discovery_Drop.LoadingTime,
        Discovery_Drop.Distance,
        Discovery_Drop.TravellingTime,
        Isnull(Discovery_Opco.Code+'-'+ Discovery_Shipment.ShipmentNumber+'-'+Discovery_Shipment.DespatchNumber,'') as ShipmentNumberAndDespatch,
        Discovery_Drop.DropSequence  
FROM      Discovery_Opco RIGHT OUTER JOIN
                      Discovery_Shipment ON Discovery_Opco.Code = Discovery_Shipment.OpCoCode RIGHT OUTER JOIN
                      Discovery_Drop ON Discovery_Shipment.Id = Discovery_Drop.ShipmentId

WHERE   ( Discovery_Drop.TripId = @Id )
GROUP BY Discovery_Drop.DepartTime,
        Discovery_Drop.ArriveTime,
        Discovery_Drop.LoadingTime,
        Discovery_Drop.Distance,
        Discovery_Drop.TravellingTime,
        Discovery_Shipment.ShipmentNumber,
        Discovery_Shipment.DespatchNumber,
        Discovery_Opco.Code,
        Discovery_Drop.DropSequence,
        Discovery_Drop.Weight,
        Discovery_Drop.Volume
GO

