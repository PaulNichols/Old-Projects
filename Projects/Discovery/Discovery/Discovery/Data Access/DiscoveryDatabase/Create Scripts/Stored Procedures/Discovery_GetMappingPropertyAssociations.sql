IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingPropertyAssociations')
	BEGIN
		DROP  Procedure  Discovery_GetMappingPropertyAssociations
	END

GO

CREATE Procedure Discovery_GetMappingPropertyAssociations


AS

SELECT DISTINCT 
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingPropertyAssociation
Order By 
	DestinationProperty
GO

  