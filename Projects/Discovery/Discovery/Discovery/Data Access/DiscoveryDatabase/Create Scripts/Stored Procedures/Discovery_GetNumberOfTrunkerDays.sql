IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetNumberOfTrunkerDays')
	BEGIN
		DROP  Procedure  Discovery_GetNumberOfTrunkerDays
	END

GO

CREATE Procedure Discovery_GetNumberOfTrunkerDays
	(
		@SourceWarehouseId int,
		@DestinationWarehouseId int
	)

AS

DECLARE @NumberOfDays int 

SELECT
	@NumberOfDays=Days--coalesce
FROM
	Discovery_TrunkerDay
WHERE
	SourceWarehouseId=@SourceWarehouseId AND
	DestinationWarehouseId=@DestinationWarehouseId

if @NumberOfDays is null
	select -2
else
	select 	@NumberOfDays
GO

