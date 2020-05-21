IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoShipmentLines')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoShipmentLines
	END

GO

CREATE Procedure Discovery_GetOpCoShipmentLines

	(
		@ShipmentId int,
		@SortExpression varchar(100) = 'LineNumber'
	)

AS


SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_OpCoShipmentLine
WHERE
	ShipmentId = @ShipmentId
ORDER BY

-- ProductCode
CASE WHEN @sortExpression = 'ProductCode Ascending'
	THEN [ProductCode] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'ProductCode Descending'
	THEN [ProductCode] ELSE NULL END DESC,
-- LineNumber
CASE WHEN @sortExpression = 'LineNumber Ascending'
	THEN [LineNumber] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'LineNumber Descending'
	THEN [LineNumber] ELSE NULL END DESC,
-- Quantity
CASE WHEN @sortExpression = 'Quantity Ascending'
	THEN [Quantity] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'Quantity Descending'
	THEN [Quantity] ELSE NULL END DESC,
-- Description1
CASE WHEN @sortExpression = 'Description1 Ascending'
	THEN [Description1] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'Description1 Descending'
	THEN [Description1] ELSE NULL END DESC,
-- Description2
CASE WHEN @sortExpression = 'Description2 Ascending'
	THEN [Description2] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'Description2 Descending'
	THEN [Description2] ELSE NULL END DESC,
-- NetWeight
CASE WHEN @sortExpression = 'NetWeight Ascending'
	THEN [NetWeight] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'NetWeight Descending'
	THEN [NetWeight] ELSE NULL END DESC,
-- Length
CASE WHEN @sortExpression = 'Length Ascending'
	THEN [Length] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'Length Descending'
	THEN [Length] ELSE NULL END DESC,
-- Width
CASE WHEN @sortExpression = 'Width Ascending'
	THEN [Width] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'Width Descending'
	THEN [Width] ELSE NULL END DESC,
-- Volume
CASE WHEN @sortExpression = 'Volume Ascending'
	THEN [Volume] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'Volume Descending'
	THEN [Volume] ELSE NULL END DESC,
-- CustomerReference
CASE WHEN @sortExpression = 'CustomerReference Ascending'
	THEN [CustomerReference] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'CustomerReference Descending'
	THEN [CustomerReference] ELSE NULL END DESC,
-- ConversionQuantity
CASE WHEN @sortExpression = 'ConversionQuantity Ascending'
	THEN [ConversionQuantity] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'ConversionQuantity Descending'
	THEN [ConversionQuantity] ELSE NULL END DESC,
-- IsPanel
CASE WHEN @sortExpression = 'IsPanel Ascending'
	THEN [IsPanel] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'IsPanel Descending'
	THEN [IsPanel] ELSE NULL END DESC,
-- IsISO9000Approved
CASE WHEN @sortExpression = 'IsISO9000Approved Ascending'
	THEN [IsISO9000Approved] ELSE NULL END ASC,
CASE WHEN @sortExpression = 'IsISO9000Approved Descending'
	THEN [IsISO9000Approved] ELSE NULL END DESC

GO



