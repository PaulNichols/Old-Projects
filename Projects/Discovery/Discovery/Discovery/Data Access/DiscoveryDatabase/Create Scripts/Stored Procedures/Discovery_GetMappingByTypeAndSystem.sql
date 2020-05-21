IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingByTypeAndSystem')
	BEGIN
		DROP  Procedure  Discovery_GetMappingByTypeAndSystem
	END

GO

CREATE Procedure Discovery_GetMappingByTypeAndSystem
	(
		@SourceMappingType varchar(256),
		@DestinationMappingType varchar(256),
		@SourceSystem varchar(256),
		@DestinationSystem varchar(256)
	)

AS

DECLARE @DestinationSystemId int
DECLARE @SourceSystemId int

SELECT
	@SourceSystemId=Id
FROM
	Discovery_MappingSystem
WHERE
	[Name]=@SourceSystem AND
	IsSource=1
	
SELECT
	@DestinationSystemId=Id
FROM
	Discovery_MappingSystem
WHERE
	[Name]=@DestinationSystem AND
	IsDestination=1
	
SELECT     
	Discovery_Mapping.*, 
	BINARY_CHECKSUM(*) AS CheckSum
FROM         
	Discovery_Mapping INNER JOIN
	Discovery_MappingPropertyAssociation ON 
	Discovery_Mapping.MappingPropertyAssociationId = Discovery_MappingPropertyAssociation.Id INNER JOIN
	Discovery_MappingClassAssociation ON 
	Discovery_MappingPropertyAssociation.MappingClassAssociationId = Discovery_MappingClassAssociation.Id
WHERE     
	(Discovery_Mapping.SourceSystemId = @SourceSystemId) AND 
	(Discovery_Mapping.DestinationSystemId = @DestinationSystemId) AND 
	(Discovery_MappingClassAssociation.SourceTypeFullName = @SourceMappingType) AND 
	(Discovery_MappingClassAssociation.DestinationTypeFullName = @DestinationMappingType)

GO

 