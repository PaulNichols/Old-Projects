IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetAuditEntriesCount')
	BEGIN
		DROP  Procedure  Discovery_GetAuditEntriesCount
	END

GO

CREATE Procedure Discovery_GetAuditEntriesCount
(
     @dateFrom datetime,
     @dateTo datetime,
     @sourceSystem varchar(256),
     @destinationSystem varchar(256),
     @type varchar(256),
     @message varchar(1000)
	)

AS

SELECT count(    
	Discovery_AuditEntry.id)
	FROM
		Discovery_AuditEntry
	WHERE 
		(Discovery_AuditEntry.SourceSystem LIKE COALESCE('%' + @sourceSystem + '%', '%') OR @sourceSystem IS NULL) AND
		(Discovery_AuditEntry.DestinationSystem LIKE COALESCE('%' + @destinationSystem + '%', '%') OR @destinationSystem IS NULL) AND
		(Discovery_AuditEntry.Type LIKE COALESCE('%' + @type + '%', '%') OR @type IS NULL) AND
		(Discovery_AuditEntry.Message LIKE COALESCE('%' + @message + '%', '%') OR @message IS NULL) AND
		(Discovery_AuditEntry.ReceivedDate >= @dateFrom OR @dateFrom IS NULL) AND
		(Discovery_AuditEntry.ReceivedDate <= @dateTo OR @dateTo IS NULL)
	

GO
