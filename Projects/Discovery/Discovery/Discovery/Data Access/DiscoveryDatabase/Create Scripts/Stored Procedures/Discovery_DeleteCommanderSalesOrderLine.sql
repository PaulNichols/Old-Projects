IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteCommanderSalesOrderLine')
	BEGIN
		DROP  Procedure  Discovery_DeleteCommanderSalesOrderLine
	END

GO

CREATE Procedure Discovery_DeleteCommanderSalesOrderLine
(
	@Id int
)
AS
DELETE
FROM Discovery_CommanderSalesOrderLine
WHERE
	Id=@Id

GO
