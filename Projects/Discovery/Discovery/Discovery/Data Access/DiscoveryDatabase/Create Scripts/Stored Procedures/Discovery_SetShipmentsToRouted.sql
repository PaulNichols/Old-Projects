IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SetShipmentsToRouted')
	BEGIN
		DROP  Procedure  Discovery_SetShipmentsToRouted
	END

GO

CREATE Procedure Discovery_SetShipmentsToRouted

	(
		@RoutingHistoryId int,
		@UpdatedBy varchar(256),
		@UpdatedDate datetime
	)

AS


update 
	discovery_shipment 
set  
	status=4,
	UpdatedBy=@UpdatedBy,
	UpdatedDate=@UpdatedDate
where id in (

select shipmentId as id from Discovery_ShipmentRoutingHistory where RoutingHistoryId=@RoutingHistoryId
)

go


