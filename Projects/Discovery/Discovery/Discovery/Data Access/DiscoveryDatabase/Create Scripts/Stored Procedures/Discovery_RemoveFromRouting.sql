IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_RemoveFromRouting')
	BEGIN
		DROP  Procedure  Discovery_RemoveFromRouting
	END

GO

CREATE Procedure Discovery_RemoveFromRouting

	(
		@RoutingHistoryId int,
		@RemovedBy varchar(265)
	)


AS

BEGIN

DECLARE @ReturnValue int

BEGIN TRAN

UPDATE
	Discovery_Shipment 
SET 
	Status=1 , --mapping
	UpdatedBy=@RemovedBy
WHERE
	Id in
	(
		SELECT 
			ShipmentId as Id
		FROM 
			Discovery_ShipmentRoutingHistory
		WHERE
			RoutingHistoryId=@RoutingHistoryId	
	)
	
	-- Check for error (transaction)
IF (@@ERROR <> 0) GOTO QuitWithRollback

DELETE FROM
	Discovery_RoutingHistory
WHERE
	Id=@RoutingHistoryId

-- Check for error (transaction)
IF (@@ERROR <> 0) GOTO QuitWithRollback

COMMIT TRAN

SET @ReturnValue=0

-- All done
GOTO Quit

--**********************************
-- Error
--**********************************
QuitWithRollback:
	-- Reset the order id
	SET @ReturnValue = -1
	-- Rollback the transaction if we have one
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION

--**********************************
-- Finally
--**********************************
Quit:

-- Return the new order id
RETURN  @ReturnValue

END

