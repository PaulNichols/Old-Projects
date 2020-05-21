IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCos')
	BEGIN
		DROP  Procedure  Discovery_GetOpCos
	END

GO

CREATE Procedure Discovery_GetOpCos

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Opco

GO

