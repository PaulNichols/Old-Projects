IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingClassAssociations')
	BEGIN
		DROP  Procedure  Discovery_GetMappingClassAssociations
	END

GO

CREATE Procedure Discovery_GetMappingClassAssociations


AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingClassAssociation
Order By
	SourceType

GO

 