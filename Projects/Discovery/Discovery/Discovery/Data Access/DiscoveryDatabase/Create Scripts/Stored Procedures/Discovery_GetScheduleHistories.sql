IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetScheduleHistories')
	BEGIN
		DROP  Procedure  Discovery_GetScheduleHistories
	END

GO

CREATE Procedure Discovery_GetScheduleHistories
	(
		@ScheduleId int,
		@sortExpression varchar(1000),
		@startRowIndex int,
		@maximumRows int
	)

AS

Select
    *
From
(
	SELECT 
		S.Id, 
		S.TypeFullName, 
		SH.StartDate, 
		SH.EndDate, 
		SH.Succeeded, 
		SH.LogNotes, 
		SH.NextStart,
		BINARY_CHECKSUM(*) as CheckSum,
		ROW_NUMBER() OVER(
			ORDER BY
			-- StartDate
			CASE WHEN @sortExpression = 'StartDate'
				 THEN [StartDate] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'StartDate desc'
				 THEN [StartDate] ELSE NULL END DESC,

			-- EndDate
			CASE WHEN @sortExpression = 'EndDate'
				 THEN [EndDate] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'EndDate desc'
				 THEN [EndDate] ELSE NULL END DESC,
				 
			-- NextStart
			CASE WHEN @sortExpression = 'NextStart'
				 THEN [NextStart] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'NextStart desc'
				 THEN [NextStart] ELSE NULL END DESC
				 
				 ) as RowNum				 				 
	FROM Discovery_Schedule S
	INNER JOIN Discovery_ScheduleHistory SH
	ON S.Id = SH.ScheduleId
	WHERE S.Id = @ScheduleId or @ScheduleId = -1
	) as DeriveTableName
WHERE RowNum BETWEEN @startRowIndex AND (@startRowIndex + @maximumRows) - 1

GO

  