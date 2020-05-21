IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSequence')
	BEGIN
		DROP  Procedure  Discovery_GetSequence
	END

GO

CREATE Procedure Discovery_GetSequence
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Sequence
WHERE
	Id=@Id

GO

  