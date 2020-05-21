IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetLoadCategory')
	BEGIN
		DROP  Procedure  Discovery_GetLoadCategory
	END

GO

CREATE Procedure Discovery_GetLoadCategory
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_LoadCategory
WHERE
	Id=@Id

GO

  