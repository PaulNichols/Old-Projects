IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteMappingConfiguration')
	BEGIN
		DROP  Procedure  Discovery_DeleteMappingConfiguration
	END

GO

CREATE Procedure Discovery_DeleteMappingConfiguration
(
	@Id int
)
AS
DELETE
FROM Discovery_MappingConfiguration
WHERE
	Id=@Id

GO
