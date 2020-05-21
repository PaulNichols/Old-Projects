IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoRoutes')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoRoutes
	END

GO

CREATE Procedure Discovery_GetOpCoRoutes

AS

SELECT DISTINCT
	RouteCode as Code,
	RouteCode as Description
FROM
	Discovery_OpCoShipment
	
ORDER BY RouteCode ASC

GO

