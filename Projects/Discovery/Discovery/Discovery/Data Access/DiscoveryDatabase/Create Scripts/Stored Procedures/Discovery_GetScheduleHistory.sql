IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetScheduleHistory')
	BEGIN
		DROP  Procedure  Discovery_GetScheduleHistory
	END

GO

CREATE Procedure Discovery_GetScheduleHistory
	(
		@ScheduleId int
	)

AS

	SELECT 
		S.Id, 
		S.TypeFullName, 
		SH.StartDate, 
		SH.EndDate, 
		SH.Succeeded, 
		SH.LogNotes, 
		SH.NextStart,
		BINARY_CHECKSUM(*) as CheckSum
	FROM Discovery_Schedule S
	INNER JOIN Discovery_ScheduleHistory SH
	ON S.Id = SH.ScheduleId
	WHERE S.Id = @ScheduleId or @ScheduleId = -1

GO

  