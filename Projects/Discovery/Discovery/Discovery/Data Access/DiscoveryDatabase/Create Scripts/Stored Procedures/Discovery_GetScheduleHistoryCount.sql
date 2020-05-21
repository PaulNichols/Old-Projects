IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetScheduleHistoryCount')
	BEGIN
		DROP  Procedure  Discovery_GetScheduleHistoryCount
	END

GO

CREATE Procedure Discovery_GetScheduleHistoryCount
(
		@ScheduleId int = Null
)

AS

SELECT count(S.Id)
	FROM Discovery_Schedule S
	INNER JOIN Discovery_ScheduleHistory SH
	ON S.Id = SH.ScheduleId
	WHERE S.Id = @ScheduleId or @ScheduleId = -1

GO
