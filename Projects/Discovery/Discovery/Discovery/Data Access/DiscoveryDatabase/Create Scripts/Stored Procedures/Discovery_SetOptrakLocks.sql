IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SetOptrakLocks')
	BEGIN
		DROP  Procedure  Discovery_SetOptrakLocks
	END

GO

CREATE Procedure Discovery_SetOptrakLocks
(         
    @EstimatedDateTo datetime,
    @LockedBy varchar(265),
    @WarehouseId int,
    @RegionId int 
)   

AS

BEGIN

DECLARE @RoutingHistoryId int

CREATE TABLE #RoutingTemp  
(
	ShipmentId int
)


INSERT INTO 
	#RoutingTemp (ShipmentId)
SELECT
	S.Id 
FROM 
	Discovery_Shipment AS S INNER JOIN
	Discovery_Warehouse ON S.DeliveryWarehouseCode = Discovery_Warehouse.Code 
WHERE
	S.Status = 1  AND	--mapped
	(S.Type <>4) AND	--not warehouse orders
	(S.EstimatedDeliveryDate <= @EstimatedDateTo OR @EstimatedDateTo IS NULL) AND
	Discovery_Warehouse.Id=@WarehouseId
ORDER BY 
	ShipmentName,
	pafpostcode,
	estimateddeliverydate

--ARE THERE ANY SHIPMENTS TO ROUTE
 if (select count(ShipmentId) from #RoutingTemp)=0
 BEGIN
	SET @RoutingHistoryId=-2
		-- Rollback the transaction if we have one
	--IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
	GOTO Quit
 END

-- Check for error (transaction)
IF (@@ERROR <> 0) GOTO QuitWithRollback
	 
UPDATE 
	Discovery_Shipment  
SET 
	Status=3			--routing	
WHERE
	Discovery_Shipment.ID IN (SELECT ShipmentId FROM #RoutingTemp)
	
-- Check for error (transaction)
IF (@@ERROR <> 0) GOTO QuitWithRollback

declare @processStartDate datetime
set @processStartDate=getdate()

EXECUTE dbo.Discovery_SaveRoutingHistory
		-1,
		@RegionId,
		null,
		null,
		null,
		null,
		@processStartDate,
		null,
		@lockedBy,
		null,
		null


-- Check for error (transaction)
IF (@@ERROR <> 0) GOTO QuitWithRollback

SET @RoutingHistoryId=@@IDENTITY

INSERT INTO 
	Discovery_ShipmentRoutingHistory 
	(
		Shipmentid,
		Routinghistoryid
	)
SELECT
	ShipmentId ,
	@RoutingHistoryId 
FROM 
	#RoutingTemp

-- Check for error (transaction)
IF (@@ERROR <> 0) GOTO QuitWithRollback


DROP TABLE #RoutingTemp

--COMMIT TRAN

-- All done
GOTO Quit

--**********************************
-- Error
--**********************************
QuitWithRollback:
	-- Reset the order id
	SET @RoutingHistoryId = -1
	-- Rollback the transaction if we have one
	--IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION

--**********************************
-- Finally
--**********************************
Quit:

-- Return the new order id
RETURN  @RoutingHistoryId

END
 