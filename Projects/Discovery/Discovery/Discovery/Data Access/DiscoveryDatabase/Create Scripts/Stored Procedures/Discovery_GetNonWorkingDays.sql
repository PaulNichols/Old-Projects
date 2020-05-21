IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetNonWorkingDays')
	BEGIN
		DROP  Procedure  Discovery_GetNonWorkingDays
	END

GO

CREATE Procedure Discovery_GetNonWorkingDays
(
		@dateFrom datetime,
		@dateTo datetime,
		@warehouseId int = Null,
		@regionId int = Null,
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
		Discovery_NonWorkingDay.*, 
		Discovery_Warehouse.Code as WarehouseCode,
		BINARY_CHECKSUM(Discovery_NonWorkingDay.Id, Discovery_NonWorkingDay.NonWorkingDate, Discovery_NonWorkingDay.Description, 
						  Discovery_NonWorkingDay.WarehouseId, Discovery_NonWorkingDay.UpdatedDate, Discovery_NonWorkingDay.UpdatedBy) AS CheckSum,
		ROW_NUMBER() OVER(
			ORDER BY				
			-- NonWorkingDate
			CASE WHEN @sortExpression = 'NonWorkingDate' 
				 THEN [NonWorkingDate] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'NonWorkingDate desc' 
				 THEN [NonWorkingDate] ELSE NULL END DESC,
 
			-- Description
			CASE WHEN @sortExpression = 'Description' 
				 THEN Discovery_NonWorkingDay.Description ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'Description desc' 
				 THEN Discovery_NonWorkingDay.Description ELSE NULL END DESC,
				 
			-- WarehouseCode
			CASE WHEN @sortExpression = 'WarehouseCode' 
				 THEN Discovery_Warehouse.Code ELSE NULL END ASC,	
			CASE WHEN @sortExpression = 'WarehouseCode desc' 
				 THEN Discovery_Warehouse.Code ELSE NULL END DESC
		

			) as RowNum
	FROM
		Discovery_NonWorkingDay INNER JOIN
                      Discovery_Warehouse ON Discovery_NonWorkingDay.WarehouseId = Discovery_Warehouse.Id
	WHERE     
		((Discovery_NonWorkingDay.NonWorkingDate >= @dateFrom) AND
		(Discovery_NonWorkingDay.NonWorkingDate <= @dateTo)) AND
		(Discovery_NonWorkingDay.WarehouseId=@warehouseId OR @warehouseId Is Null) AND
		(Discovery_Warehouse.RegionId=@regionId OR @regionId Is Null) 
) as DerivedTableName
WHERE 
RowNum 
BETWEEN @startRowIndex AND (@startRowIndex + @maximumRows) - 1

GO
