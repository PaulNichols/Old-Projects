IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetLoadCategoryByCode')
	BEGIN
		DROP  Procedure  Discovery_GetLoadCategoryByCode
	END

GO

CREATE Procedure Discovery_GetLoadCategoryByCode
	(
		@code varchar(2) = null
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_LoadCategory
WHERE
	Code=@code

GO

  