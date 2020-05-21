IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSequences')
	BEGIN
		DROP  Procedure  Discovery_GetSequences
	END

GO

CREATE Procedure Discovery_GetSequences

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Sequence

GO

