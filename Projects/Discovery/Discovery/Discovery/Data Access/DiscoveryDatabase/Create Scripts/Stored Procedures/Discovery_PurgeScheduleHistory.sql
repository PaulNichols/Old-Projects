IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_PurgeScheduleHistory')
	BEGIN
		DROP  Procedure  Discovery_PurgeScheduleHistory
	END

GO

CREATE Procedure Discovery_PurgeScheduleHistory

AS

DELETE
FROM Discovery_ScheduleHistory
FROM Discovery_Schedule S
WHERE
    (
    SELECT COUNT(*)
    FROM Discovery_ScheduleHistory SH
    WHERE
        SH.ScheduleId = Discovery_ScheduleHistory.ScheduleId AND
        SH.ScheduleId = S.Id AND
        SH.StartDate >= Discovery_ScheduleHistory.StartDate
    ) > S.RetainHistoryNum
AND RetainHistoryNum<>-1

GO
