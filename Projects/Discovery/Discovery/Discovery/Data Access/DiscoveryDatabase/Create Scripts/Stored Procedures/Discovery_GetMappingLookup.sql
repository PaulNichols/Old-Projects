IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMappingLookUp')
	BEGIN
		DROP  Procedure  Discovery_GetMappingLookUp
	END

GO

CREATE Procedure Discovery_GetMappingLookUp
	(
		@Id int
	)

AS

DECLARE @LookUpTableName varchar(256)
DECLARE @LookUpTableDisplayColumn varchar(256)

SELECT     
	@LookUpTableName=LookupTableName, 
	@LookUpTableDisplayColumn=LookUpTableDisplayColumn
FROM         
	Discovery_MappingPropertyAssociation
WHERE     
	Id = @Id
	
	if not @LookUpTableName  is null 
		Execute(@LookUpTableName) 
GO

 