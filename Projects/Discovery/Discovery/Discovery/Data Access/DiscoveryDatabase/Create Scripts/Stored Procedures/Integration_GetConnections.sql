IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_GetConnections')
	BEGIN
		DROP  Procedure  Integration_GetConnections
	END

GO

CREATE Procedure Integration_GetConnections

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Integration_Connection

GO

