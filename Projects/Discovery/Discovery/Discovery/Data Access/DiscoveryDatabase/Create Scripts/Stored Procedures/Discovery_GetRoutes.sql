IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRoutes')
	BEGIN
		DROP  Procedure  Discovery_GetRoutes
	END

GO

CREATE Procedure Discovery_GetRoutes
(
		@warehouseId int = null,
		@sortExpression varchar(1000),
		@startRowIndex int,
		@maximumRows int
)

AS

Select 
	*
FROM
(
	SELECT
		*,
		BINARY_CHECKSUM(*) as CheckSum,
		ROW_NUMBER() OVER(
			ORDER BY
			-- TypeFullName
			CASE WHEN @sortExpression = 'Code'
				 THEN [Code] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'Code desc'
				 THEN [Code] ELSE NULL END DESC,
			CASE WHEN @sortExpression = 'Description'
				 THEN [Description] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'Description desc'
				 THEN [Description] ELSE NULL END DESC
				 ) as RowNum
	FROM
		Discovery_Route
	WHERE (WarehouseId = @warehouseId OR @warehouseId Is Null)
) as DeriveTableName
WHERE
(@startRowIndex=0 and @maximumRows=0) or 
RowNum
BETWEEN @startRowIndex AND (@startRowIndex + @maximumRows) - 1

GO

