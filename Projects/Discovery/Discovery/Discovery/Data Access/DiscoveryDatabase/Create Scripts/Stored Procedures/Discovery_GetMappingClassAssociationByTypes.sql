IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingClassAssociationByTypes')
	BEGIN
		DROP  Procedure  Discovery_GetMappingClassAssociationByTypes
	END

GO

CREATE Procedure Discovery_GetMappingClassAssociationByTypes
	(
		@sourceMappingType varchar(256),
		@destinationMappingType varchar(256)
	)

AS

SELECT     Discovery_MappingClassAssociation.*
FROM         Discovery_MappingClassAssociation
WHERE     (SourceTypeFullName = @sourceMappingType) AND (DestinationTypeFullName = @destinationMappingType)

GO

