IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeleteAllMappings')
	BEGIN
		DROP  Procedure  Discovery_DeleteAllMappings
	END

GO

CREATE Procedure Discovery_DeleteAllMappings

AS
DELETE
FROM Discovery_Mapping


GO
