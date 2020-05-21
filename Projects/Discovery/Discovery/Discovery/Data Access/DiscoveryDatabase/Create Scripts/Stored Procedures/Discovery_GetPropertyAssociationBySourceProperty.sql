

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'GetMappingPropertyAssociationsBySourceProperty')
	BEGIN
		DROP  Procedure  GetMappingPropertyAssociationsBySourceProperty
	END

GO

CREATE Procedure GetMappingPropertyAssociationsBySourceProperty
	(
		@SourceProperty varchar(100)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_MappingPropertyAssociation
WHERE
	SourceProperty=@SourceProperty
Order By 
	DestinationProperty
GO

  