IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetScheduleItemSettings')
	BEGIN
		DROP  Procedure  Discovery_GetScheduleItemSettings
	END

GO

CREATE Procedure Discovery_GetScheduleItemSettings
	(
		@ScheduleId int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_ScheduleItemSettings
WHERE
	ScheduleId=@ScheduleId

GO

  