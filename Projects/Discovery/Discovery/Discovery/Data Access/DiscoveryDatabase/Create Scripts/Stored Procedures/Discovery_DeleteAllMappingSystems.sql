IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeleteAllMappingSystem')
	BEGIN
		DROP  Procedure  Discovery_DeleteAllMappingSystem
	END

GO

CREATE Procedure Discovery_DeleteAllMappingSystem

AS
DELETE
FROM Discovery_MappingSystem


GO
