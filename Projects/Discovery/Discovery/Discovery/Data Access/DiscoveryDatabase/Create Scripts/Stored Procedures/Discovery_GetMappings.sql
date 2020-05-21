IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappings')
	BEGIN
		DROP  Procedure  Discovery_GetMappings
	END

GO

CREATE Procedure Discovery_GetMappings
(
	@sourceSystem int,
	@destinationSystem int,
	@sourceType varchar(MAX),
	@destinationtype varchar(MAX),
	@sourceProperty varchar(MAX),
	@destinationProperty varchar(MAX),
	@fromValue varchar(MAX),
	@toValue varchar(MAX),
	@startRowIndex int,
	@maximumRows int
)
as

			
Select 
	* 
FROM 
(
	SELECT     
		Discovery_Mapping.*, 
		BINARY_CHECKSUM(*) AS CheckSum,
		ROW_NUMBER() OVER(
			ORDER BY				
			Discovery_Mapping.Id

			) as RowNum
	FROM         
		Discovery_Mapping  JOIN
		Discovery_MappingPropertyAssociation ON 
		Discovery_Mapping.MappingPropertyAssociationId = Discovery_MappingPropertyAssociation.Id INNER JOIN
		Discovery_MappingClassAssociation ON 
		Discovery_MappingPropertyAssociation.MappingClassAssociationId = Discovery_MappingClassAssociation.Id INNER JOIN
		Discovery_MappingSystem ON Discovery_Mapping.SourceSystemId = Discovery_MappingSystem.Id INNER JOIN
		Discovery_MappingSystem AS Discovery_MappingSystem_1 ON Discovery_Mapping.DestinationSystemId = Discovery_MappingSystem_1.Id
	WHERE     
		(Discovery_MappingSystem.Id = @sourceSystem OR  @sourceSystem IS NULL) AND 
		(Discovery_MappingSystem_1.Id = @destinationSystem OR  @destinationSystem IS NULL) AND 
		(Discovery_MappingClassAssociation.SourceTypeFullName = @sourceType OR  @sourceType IS NULL) AND 
		(Discovery_MappingClassAssociation.DestinationTypeFullName = @destinationtype OR  @destinationtype IS NULL) AND 
		(Discovery_MappingPropertyAssociation.SourceProperty = @sourceProperty OR  @sourceProperty IS NULL) AND 
		(Discovery_MappingPropertyAssociation.DestinationProperty = @destinationProperty OR  @destinationProperty IS NULL) AND 
		(Discovery_Mapping.SourceValue LIKE COALESCE('%' + @fromValue + '%', '%') OR  @fromValue IS NULL) AND 
		(Discovery_Mapping.DestinationValue LIKE COALESCE('%' + @toValue + '%', '%') OR  @toValue IS NULL) 
) as DerivedTableName
WHERE 
(@startRowIndex is null AND @maximumRows is null) or 
(RowNum 
BETWEEN @startRowIndex AND (@startRowIndex + @maximumRows) - 1)

go
