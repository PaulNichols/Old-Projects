IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSchedules')
	BEGIN
		DROP  Procedure  Discovery_GetSchedules
	END

GO

CREATE Procedure Discovery_GetSchedules
(
		@sortExpression varchar(1000),
		@startRowIndex int,
		@maximumRows int
)

AS

Select 
	*
FROM
(
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
		ISNULL(SH.NextStart, getDate()) 'NextStart',
		BINARY_CHECKSUM(S.Id, 
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
		NextStart) as CheckSum,
		ROW_NUMBER() OVER(
			ORDER BY
			-- TypeFullName
			CASE WHEN @sortExpression = 'TypeFullName'
				 THEN [TypeFullName] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'TypeFullName desc'
				 THEN [TypeFullName] ELSE NULL END DESC,

			-- TimeLapse
			CASE WHEN @sortExpression = 'TimeLapse'
				 THEN [TimeLapse] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'TimeLapse desc'
				 THEN [TimeLapse] ELSE NULL END DESC,

			-- TimeLapseMeasurement
			CASE WHEN @sortExpression = 'TimeLapseMeasurement'
				 THEN [TimeLapseMeasurement] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'TimeLapseMeasurement desc'
				 THEN [TimeLapseMeasurement] ELSE NULL END DESC,

			-- RetryTimeLapse
			CASE WHEN @sortExpression = 'RetryTimeLapse'
				 THEN [RetryTimeLapse] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'RetryTimeLapse desc'
				 THEN [RetryTimeLapse] ELSE NULL END DESC,

			-- RetryTimeLapseMeasurement
			CASE WHEN @sortExpression = 'RetryTimeLapseMeasurement'
				 THEN [RetryTimeLapseMeasurement] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'RetryTimeLapseMeasurement desc'
				 THEN [RetryTimeLapseMeasurement] ELSE NULL END DESC,

			-- ObjectDependencies
			CASE WHEN @sortExpression = 'ObjectDependencies'
				 THEN [ObjectDependencies] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'ObjectDependencies desc'
				 THEN [ObjectDependencies] ELSE NULL END DESC,

			-- AttachToEvent
			CASE WHEN @sortExpression = 'AttachToEvent'
				 THEN [AttachToEvent] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'AttachToEvent desc'
				 THEN [AttachToEvent] ELSE NULL END DESC,

			-- RetainHistoryNum
			CASE WHEN @sortExpression = 'RetainHistoryNum'
				 THEN [RetainHistoryNum] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'RetainHistoryNum desc'
				 THEN [RetainHistoryNum] ELSE NULL END DESC,

			-- CatchUpEnabled
			CASE WHEN @sortExpression = 'CatchUpEnabled'
				 THEN [CatchUpEnabled] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'CatchUpEnabled desc'
				 THEN [CatchUpEnabled] ELSE NULL END DESC,

			-- Enabled
			CASE WHEN @sortExpression = 'Enabled'
				 THEN [Enabled] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'Enabled desc'
				 THEN [Enabled] ELSE NULL END DESC,

			-- NextStart
			CASE WHEN @sortExpression = 'NextStart'
				 THEN [NextStart] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'NextStart desc'
				 THEN [NextStart] ELSE NULL END DESC
				 ) as RowNum				 
	FROM dbo.Discovery_Schedule S
	LEFT JOIN dbo.Discovery_ScheduleHistory SH
	ON S.Id = SH.ScheduleId
	WHERE SH.Id = (SELECT TOP 1 S1.Id 
					FROM dbo.Discovery_ScheduleHistory S1
					WHERE S1.ScheduleId = S.Id 
					ORDER BY S1.NextStart DESC)
	OR  SH.Id IS NULL
	GROUP BY S.Id, S.TypeFullName, S.TimeLapse, S.TimeLapseMeasurement,  S.RetryTimeLapse, S.RetryTimeLapseMeasurement, S.ObjectDependencies, S.AttachToEvent, S.RetainHistoryNum, S.CatchUpEnabled, S.Enabled, SH.NextStart
) as DeriveTableName
WHERE
RowNum
BETWEEN @startRowIndex AND (@startRowIndex + @maximumRows) - 1

GO

