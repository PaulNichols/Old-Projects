IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSourceSystemList')
	BEGIN
		DROP  Procedure  Discovery_GetSourceSystemList
	END

GO

CREATE Procedure Discovery_GetSourceSystemList
AS

SELECT DISTINCT SourceSystem
FROM
	Discovery_AuditEntry
	ORDER BY SourceSystem

GO
