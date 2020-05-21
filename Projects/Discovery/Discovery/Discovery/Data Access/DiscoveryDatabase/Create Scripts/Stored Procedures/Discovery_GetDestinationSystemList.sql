IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetDestinationSystemList')
	BEGIN
		DROP  Procedure  Discovery_GetDestinationSystemList
	END

GO

CREATE Procedure Discovery_GetDestinationSystemList
AS

SELECT DISTINCT DestinationSystem
FROM
	Discovery_AuditEntry
	ORDER BY DestinationSystem

GO

