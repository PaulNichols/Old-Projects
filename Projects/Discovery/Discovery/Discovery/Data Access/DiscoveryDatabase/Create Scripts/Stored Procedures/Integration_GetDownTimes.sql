IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_GetDownTimes')
	BEGIN
		DROP  Procedure  Integration_GetDownTimes
	END

GO

CREATE Procedure Integration_GetDownTimes
	(
		@ConnectionId int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Integration_DownTime
where 
	@ConnectionId is null or ConnectionId=@ConnectionId
GO

 