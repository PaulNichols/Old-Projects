IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTypeList')
	BEGIN
		DROP  Procedure  Discovery_GetTypeList
	END

GO

CREATE Procedure Discovery_GetTypeList
AS

SELECT DISTINCT Type
FROM
	Discovery_AuditEntry
	ORDER BY Type

GO
