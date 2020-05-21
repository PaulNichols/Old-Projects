IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetCommanderSalesOrders')
	BEGIN
		DROP  Procedure  Discovery_GetCommanderSalesOrders
	END

GO

CREATE Procedure Discovery_GetCommanderSalesOrders


AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_CommanderSalesOrder

GO

 