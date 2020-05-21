IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingClassAssociationsBySourceType')
	BEGIN
		DROP  Procedure  Discovery_GetMappingClassAssociationsBySourceType
	END

GO

CREATE Procedure Discovery_GetMappingClassAssociationsBySourceType
	(
		@SourceType varchar(256)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingClassAssociation
WHERE
	SourceTypeFullName=@SourceType
Order By 
	DestinationType
GO

 