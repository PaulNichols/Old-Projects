 IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingSystems')
	BEGIN
		DROP  Procedure  Discovery_GetMappingSystems
	END

GO

CREATE Procedure Discovery_GetMappingSystems
	
AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingSystem
Order by
	[Name]

GO

 