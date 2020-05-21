IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteSchedule')
	BEGIN
		DROP  Procedure  Discovery_DeleteSchedule
	END

GO

CREATE Procedure Discovery_DeleteSchedule
(
	@Id int
)
AS
DELETE
FROM Discovery_Schedule
WHERE
	Id=@Id

GO
