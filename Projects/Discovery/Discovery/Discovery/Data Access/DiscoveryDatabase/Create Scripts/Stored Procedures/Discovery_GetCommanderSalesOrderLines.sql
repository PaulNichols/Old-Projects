IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetCommanderSalesOrderLines')
	BEGIN
		DROP  Procedure  Discovery_GetCommanderSalesOrderLines
	END

GO

CREATE Procedure Discovery_GetCommanderSalesOrderLines

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_CommanderSalesOrderLine

GO

