IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetWarehouseByCode')
	BEGIN
		DROP  Procedure  Discovery_GetWarehouseByCode
	END

GO

CREATE Procedure Discovery_GetWarehouseByCode
(
		@Code varchar(10)
)
AS

SELECT     
	*
FROM         
	Discovery_Warehouse
WHERE     
	Code LIKE @Code

GO

