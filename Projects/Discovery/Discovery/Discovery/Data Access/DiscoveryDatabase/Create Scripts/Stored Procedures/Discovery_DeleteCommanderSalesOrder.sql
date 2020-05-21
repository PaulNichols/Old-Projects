IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteCommanderSalesOrder')
	BEGIN
		DROP  Procedure  Discovery_DeleteCommanderSalesOrder
	END

GO

CREATE Procedure Discovery_DeleteCommanderSalesOrder
(
	@Id int
)
AS
DELETE
FROM Discovery_CommanderSalesOrder
WHERE
	Id=@Id

GO
