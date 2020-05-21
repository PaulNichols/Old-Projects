IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_ResetRoutingLocks')
	BEGIN
		DROP  Procedure  Discovery_ResetRoutingLocks
	END

GO

CREATE Procedure Discovery_ResetRoutingLocks
(         
    @RoutingHistoryId int,      
    @ResetBy varchar(265)
)   

AS

BEGIN

BEGIN TRAN

UPDATE 
	Discovery_Shipment 
SET
	Status=1 --Mapped
WHERE
	Id in 
	(
		SELECT 
			ShipmentId  as Id
		FROM 
			Discovery_ShipmentRoutingHistory 
		WHERE 
			Discovery_ShipmentRoutingHistory.RoutingHistoryId=@RoutingHistoryId
	)

-- Check for error (transaction)
IF (@@ERROR <> 0) GOTO QuitWithRollback

UPDATE 
	Discovery_RoutingHistory
SET
	ResetDate=GetDate(),
	ResetBy=@ResetBy
WHERE
	Id=@RoutingHistoryId


-- Check for error (transaction)
IF (@@ERROR <> 0 or @@RowCount=0) GOTO QuitWithRollback

COMMIT TRAN

-- All done
GOTO Quit

--**********************************
-- Error
--**********************************
QuitWithRollback:
	-- Reset the order id
	SET @RoutingHistoryId = -1
	-- Rollback the transaction if we have one
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION

--**********************************
-- Finally
--**********************************
Quit:

RETURN  0

END
  