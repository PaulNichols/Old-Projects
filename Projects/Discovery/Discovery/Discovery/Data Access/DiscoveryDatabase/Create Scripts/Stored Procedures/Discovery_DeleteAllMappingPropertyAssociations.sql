IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeleteAllMappingPropertyAssociations')
	BEGIN
		DROP  Procedure  Discovery_DeleteAllMappingPropertyAssociations
	END

GO

CREATE Procedure Discovery_DeleteAllMappingPropertyAssociations

AS
DELETE
FROM Discovery_MappingPropertyAssociation


GO
