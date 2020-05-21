IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetCommanderSalesOrder')
	BEGIN
		DROP  Procedure  Discovery_GetCommanderSalesOrder
	END

GO

CREATE Procedure Discovery_GetCommanderSalesOrder
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_CommanderSalesOrder
WHERE
	Id=@Id

GO

 