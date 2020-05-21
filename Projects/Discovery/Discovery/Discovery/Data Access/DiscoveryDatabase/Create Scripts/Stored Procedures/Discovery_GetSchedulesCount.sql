IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSchedulesCount')
	BEGIN
		DROP  Procedure  Discovery_GetSchedulesCount
	END

GO

CREATE Procedure Discovery_GetSchedulesCount

AS

SELECT count(S.Id)
	FROM dbo.Discovery_Schedule S
	LEFT JOIN dbo.Discovery_ScheduleHistory SH
	ON S.Id = SH.ScheduleId
	WHERE SH.Id = (SELECT TOP 1 S1.Id 
					FROM dbo.Discovery_ScheduleHistory S1
					WHERE S1.ScheduleId = S.Id 
					ORDER BY S1.NextStart DESC)
	OR  SH.Id IS NULL

GO
