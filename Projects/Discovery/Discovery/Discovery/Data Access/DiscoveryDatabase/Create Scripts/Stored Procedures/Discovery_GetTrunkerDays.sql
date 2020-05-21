IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTrunkerDays')
	BEGIN
		DROP  Procedure  Discovery_GetTrunkerDays
	END

GO

CREATE Procedure Discovery_GetTrunkerDays

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_TrunkerDay

GO

