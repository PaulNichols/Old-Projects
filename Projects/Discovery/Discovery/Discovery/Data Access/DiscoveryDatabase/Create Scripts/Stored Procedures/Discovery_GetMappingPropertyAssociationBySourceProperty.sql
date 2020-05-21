IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingPropertyAssociationsBySourceProperty')
	BEGIN
		DROP  Procedure  Discovery_GetMappingPropertyAssociationsBySourceProperty
	END

GO

CREATE Procedure Discovery_GetMappingPropertyAssociationsBySourceProperty
	(
		@SourceProperty varchar(100),
		@ClassAssociationId int
	)

AS

SELECT DISTINCT 
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingPropertyAssociation
WHERE
	SourceProperty=@SourceProperty and 
	MappingClassAssociationId=@ClassAssociationId
Order By 
	DestinationProperty
GO

  