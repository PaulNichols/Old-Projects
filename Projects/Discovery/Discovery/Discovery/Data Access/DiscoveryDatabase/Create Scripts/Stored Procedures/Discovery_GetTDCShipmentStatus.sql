IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentStatus')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentStatus
	END

GO

CREATE Procedure Discovery_GetTDCShipmentStatus

	(
		@Id int
	)

AS

SELECT
	S.Status
FROM 
	Discovery_Shipment AS S 
WHERE
	S.Id = @Id

GO



