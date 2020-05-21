IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeleteAllMappingClassAssociations')
	BEGIN
		DROP  Procedure  Discovery_DeleteAllMappingClassAssociations
	END

GO

CREATE Procedure Discovery_DeleteAllMappingClassAssociations

AS
DELETE
FROM Discovery_MappingClassAssociation


GO
