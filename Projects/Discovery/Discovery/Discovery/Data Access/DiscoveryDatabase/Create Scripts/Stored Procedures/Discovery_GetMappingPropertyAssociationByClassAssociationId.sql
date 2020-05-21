 IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingPropertyAssociationsByClassAssociationId')
	BEGIN
		DROP  Procedure  Discovery_GetMappingPropertyAssociationsByClassAssociationId
	END

GO

CREATE Procedure Discovery_GetMappingPropertyAssociationsByClassAssociationId
	(
		@MappingClassAssociationId int
	)

AS

SELECT DISTINCT 
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingPropertyAssociation
WHERE
	MappingClassAssociationId=@MappingClassAssociationId

GO

 