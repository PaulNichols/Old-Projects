IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetAuditEntries')
	BEGIN
		DROP  Procedure  Discovery_GetAuditEntries
	END

GO

CREATE Procedure Discovery_GetAuditEntries
  (
     @dateFrom datetime = null,
     @dateTo datetime = null,
     @sourceSystem varchar(256),
     @destinationSystem varchar(256),
     @type varchar(256),
     @message varchar(1000),
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
			-- ReceivedDate
			CASE WHEN @sortExpression = 'ReceivedDate' 
				 THEN [ReceivedDate] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'ReceivedDate desc' 
				 THEN [ReceivedDate] ELSE NULL END DESC,
			-- SourceSystem
			CASE WHEN @sortExpression = 'SourceSystem' 
				 THEN [SourceSystem] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'SourceSystem desc' 
				 THEN [SourceSystem] ELSE NULL END DESC,
			-- DestinationSystem
			CASE WHEN @sortExpression = 'DestinationSystem' 
				 THEN DestinationSystem ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'DestinationSystem desc' 
				 THEN DestinationSystem ELSE NULL END DESC,
			-- Type
			CASE WHEN @sortExpression = 'Type' 
				 THEN Type ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'Type desc' 
				 THEN Type ELSE NULL END DESC,
			-- Sequence
			CASE WHEN @sortExpression = 'Sequence' 
				 THEN [Sequence] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'Sequence desc' 
				 THEN [Sequence] ELSE NULL END DESC,
			-- Label
			CASE WHEN @sortExpression = 'Label' 
				 THEN Label ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'Label desc' 
				 THEN Label ELSE NULL END DESC,
			-- Name
			CASE WHEN @sortExpression = 'Name' 
				 THEN [Name] ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'Name desc' 
				 THEN [Name] ELSE NULL END DESC
			) as RowNum
	FROM
		Discovery_AuditEntry
	WHERE 
		(Discovery_AuditEntry.SourceSystem LIKE COALESCE('%' + @sourceSystem + '%', '%') OR @sourceSystem IS NULL) AND
		(Discovery_AuditEntry.DestinationSystem LIKE COALESCE('%' + @destinationSystem + '%', '%') OR @destinationSystem IS NULL) AND
		(Discovery_AuditEntry.Type LIKE COALESCE('%' + @type + '%', '%') OR @type IS NULL) AND
		(Discovery_AuditEntry.Message LIKE COALESCE('%' + @message + '%', '%') OR @message IS NULL) AND
		(Discovery_AuditEntry.ReceivedDate >= @dateFrom OR @dateFrom IS NULL) AND
		(Discovery_AuditEntry.ReceivedDate <= @dateTo OR @dateTo IS NULL)
	) as DerivedTableName
WHERE 
RowNum 
BETWEEN @startRowIndex AND (@startRowIndex + @maximumRows) - 1
GO
 