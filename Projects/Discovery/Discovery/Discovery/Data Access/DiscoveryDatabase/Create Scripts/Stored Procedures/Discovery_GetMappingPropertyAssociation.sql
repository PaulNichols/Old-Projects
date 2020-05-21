IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingPropertyAssociation')
	BEGIN
		DROP  Procedure  Discovery_GetMappingPropertyAssociation
	END

GO

CREATE Procedure Discovery_GetMappingPropertyAssociation
	(
		@Id int
	)

AS

SELECT DISTINCT 
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingPropertyAssociation
WHERE
	Id=@Id

GO

 