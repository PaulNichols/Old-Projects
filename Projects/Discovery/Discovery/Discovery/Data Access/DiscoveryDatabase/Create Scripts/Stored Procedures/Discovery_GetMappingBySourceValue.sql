IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingBySourceValue')
	BEGIN
		DROP  Procedure  Discovery_GetMappingBySourceValue
	END

GO

CREATE Procedure Discovery_GetMappingBySourceValue
	(
		@sourceMappingType varchar(256),
		@destinationMappingType varchar(256),
		@sourceSystem varchar(256),
		@destinationSystem varchar(256),
		@sourcePropertyName varchar(256),
		@destinationPropertyName varchar(256),
		@sourcePropertyValue varchar(256)
	)
AS

SELECT     Discovery_Mapping.*
FROM         Discovery_MappingClassAssociation INNER JOIN
                      Discovery_MappingPropertyAssociation ON 
                      Discovery_MappingClassAssociation.Id = Discovery_MappingPropertyAssociation.MappingClassAssociationId INNER JOIN
                      Discovery_Mapping ON Discovery_MappingPropertyAssociation.Id = Discovery_Mapping.MappingPropertyAssociationId INNER JOIN
                      Discovery_MappingSystem ON Discovery_Mapping.DestinationSystemId = Discovery_MappingSystem.Id INNER JOIN
                      Discovery_MappingSystem AS Discovery_MappingSystem_1 ON Discovery_Mapping.SourceSystemId = Discovery_MappingSystem_1.Id
WHERE     (Discovery_MappingClassAssociation.SourceTypeFullName = @sourceMappingType) AND 
                      (Discovery_MappingClassAssociation.DestinationTypeFullName = @destinationMappingType) AND 
                      (Discovery_MappingSystem.Name = @destinationSystem) AND (Discovery_MappingSystem_1.Name = @sourceSystem) AND 
                      (Discovery_MappingPropertyAssociation.SourceProperty = @sourcePropertyName) AND (Discovery_Mapping.SourceValue = @sourcePropertyValue) 
                      AND (Discovery_MappingPropertyAssociation.DestinationProperty = @destinationPropertyName)

GO
