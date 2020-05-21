IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteSalesLocation')
	BEGIN
		DROP  Procedure  Discovery_DeleteSalesLocation
	END

GO

CREATE Procedure Discovery_DeleteSalesLocation
(
	@Id int
)
AS
DELETE
FROM Discovery_SalesLocation
WHERE
	Id=@Id

GO
