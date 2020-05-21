IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRoutesCount')
	BEGIN
		DROP  Procedure  Discovery_GetRoutesCount
	END

GO

CREATE Procedure Discovery_GetRoutesCount
(
		@warehouseId int = null
)

AS

SELECT count(*)
	FROM Discovery_Route
	WHERE (WarehouseId = @warehouseId OR @warehouseId Is Null)


GO
