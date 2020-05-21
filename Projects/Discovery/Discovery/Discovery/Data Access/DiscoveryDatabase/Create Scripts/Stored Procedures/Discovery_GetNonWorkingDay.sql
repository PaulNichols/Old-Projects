IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetNonWorkingDay')
	BEGIN
		DROP  Procedure  Discovery_GetNonWorkingDay
	END

GO

CREATE Procedure Discovery_GetNonWorkingDay
	(
		@nonWorkingDayId int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_NonWorkingDay AS a
WHERE
    a.Id = @nonWorkingDayId

GO

