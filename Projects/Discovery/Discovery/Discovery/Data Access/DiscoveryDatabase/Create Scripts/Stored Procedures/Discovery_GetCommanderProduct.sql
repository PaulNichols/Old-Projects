IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetCommanderProduct')
	BEGIN
		DROP  Procedure  Discovery_GetCommanderProduct
	END

GO

CREATE Procedure Discovery_GetCommanderProduct
	(
		@ProductCode varchar(20)
	)

AS

SELECT
	*
FROM
	Discovery_CommanderProduct
WHERE
	ProductCode=@ProductCode

GO

 