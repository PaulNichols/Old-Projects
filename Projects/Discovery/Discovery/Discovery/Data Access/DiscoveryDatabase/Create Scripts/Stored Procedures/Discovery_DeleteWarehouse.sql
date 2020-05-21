IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteWarehouse')
	BEGIN
		DROP  Procedure  Discovery_DeleteWarehouse
	END

GO

CREATE Procedure Discovery_DeleteWarehouse
(
	@Id int
)
AS
DELETE
FROM Discovery_Warehouse
WHERE
	Id=@Id

GO
