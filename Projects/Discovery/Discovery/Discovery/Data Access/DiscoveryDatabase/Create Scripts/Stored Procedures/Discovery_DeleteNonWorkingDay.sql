IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteNonWorkingDay')
	BEGIN
		DROP  Procedure  Discovery_DeleteNonWorkingDay
	END

GO

CREATE Procedure Discovery_DeleteNonWorkingDay
(
	@Id int
)
AS
DELETE
FROM Discovery_NonWorkingDay
WHERE
	Id=@Id

GO

