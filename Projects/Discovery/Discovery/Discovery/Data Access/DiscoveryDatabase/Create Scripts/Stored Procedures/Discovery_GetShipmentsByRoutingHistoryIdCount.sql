IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetShipmentsByRoutingHistoryIdCount')
	BEGIN
		DROP  Procedure  Discovery_GetShipmentsByRoutingHistoryIdCount
	END

GO

CREATE Procedure Discovery_GetShipmentsByRoutingHistoryIdCount
	(
		@routingHistoryId int
	)

AS


		   SELECT     
			count(id)
		FROM         
			Discovery_Shipment 	
		WHERE     
			id in 
				(
					select 
						ShipmentId  as id
					from 
						Discovery_ShipmentRoutingHistory 
					where 
						Discovery_ShipmentRoutingHistory.ShipmentId=Discovery_Shipment.Id and
						Discovery_ShipmentRoutingHistory.RoutingHistoryId=@routingHistoryId
				)
		
GO

