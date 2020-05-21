 
 IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingClassAssociation')
	BEGIN
		DROP  Procedure  Discovery_GetMappingClassAssociation
	END

GO

CREATE Procedure Discovery_GetMappingClassAssociation
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingClassAssociation
WHERE
	Id=@Id

GO

 