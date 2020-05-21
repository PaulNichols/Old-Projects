IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoByCode')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoByCode
	END

GO

CREATE Procedure Discovery_GetOpCoByCode
	(
		@Code char(3)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Opco
WHERE
	Code=@Code

GO

