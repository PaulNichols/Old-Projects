IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSchedule')
	BEGIN
		DROP  Procedure  Discovery_GetSchedule
	END

GO

CREATE Procedure Discovery_GetSchedule
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Schedule
WHERE
	Id=@Id

GO

  