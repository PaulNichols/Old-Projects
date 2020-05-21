IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetScheduleByEventName')
	BEGIN
		DROP  Procedure  Discovery_GetScheduleByEventName
	END

GO

CREATE Procedure Discovery_GetScheduleByEventName
	(
		@EventName varchar(50)
	)

AS

SELECT 
	S.Id, 
	S.TypeFullName, 
	S.TimeLapse, 
	S.TimeLapseMeasurement,  
	S.RetryTimeLapse, 
	S.RetryTimeLapseMeasurement, 
	S.ObjectDependencies, 
	S.AttachToEvent, 
	S.RetainHistoryNum, 
	S.CatchUpEnabled, 
	S.Enabled,
	BINARY_CHECKSUM(
	S.Id, 
	S.TypeFullName, 
	S.TimeLapse, 
	S.TimeLapseMeasurement,  
	S.RetryTimeLapse, 
	S.RetryTimeLapseMeasurement, 
	S.ObjectDependencies, 
	S.AttachToEvent, 
	S.RetainHistoryNum, 
	S.CatchUpEnabled, 
	S.Enabled) as CheckSum
FROM
	Discovery_Schedule S
WHERE S.AttachToEvent = @EventName
GROUP BY S.Id, S.TypeFullName, S.TimeLapse, S.TimeLapseMeasurement,  S.RetryTimeLapse, S.RetryTimeLapseMeasurement, S.ObjectDependencies, S.AttachToEvent, S.RetainHistoryNum, S.CatchUpEnabled, S.Enabled

GO

  