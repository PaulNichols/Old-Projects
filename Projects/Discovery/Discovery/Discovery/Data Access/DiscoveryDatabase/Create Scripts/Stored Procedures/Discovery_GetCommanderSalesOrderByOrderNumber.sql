IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetCommanderSalesOrderByOrderNumber')
	BEGIN
		DROP  Procedure  Discovery_GetCommanderSalesOrderByOrderNumber
	END

GO

CREATE Procedure Discovery_GetCommanderSalesOrderByOrderNumber
	(
		@OrderReference varchar(20)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_CommanderSalesOrder
WHERE
	OrderReference=@OrderReference

GO

 