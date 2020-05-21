IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetAuditEntry')
	BEGIN
		DROP  Procedure  Discovery_GetAuditEntry
	END

GO

CREATE Procedure Discovery_GetAuditEntry
	(
		@AuditEntryId int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_AuditEntry AS a
WHERE
    a.Id = @AuditEntryId

GO

