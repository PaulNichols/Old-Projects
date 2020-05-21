IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetCommanderSalesOrderLine')
	BEGIN
		DROP  Procedure  Discovery_GetCommanderSalesOrderLine
	END

GO

CREATE Procedure Discovery_GetCommanderSalesOrderLine
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_CommanderSalesOrderLine
WHERE
	Id=@Id

GO

 