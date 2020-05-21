IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveMappingPropertyAssociation')
	BEGIN
		DROP  Procedure  Discovery_SaveMappingPropertyAssociation
	END

GO

CREATE Procedure Discovery_SaveMappingPropertyAssociation

	(
		@Id int= null,
		@MappingClassAssociationId int,
	    @SourceProperty varchar(100),
        @DestinationProperty varchar(100),
        @LookupTableName varchar(256),
        @LookUpTableDisplayColumn  varchar(256),
		@CheckSum int
	)


AS

IF @Id =-1
begin
	INSERT INTO
		Discovery_MappingPropertyAssociation
		(
			MappingClassAssociationId ,
			SourceProperty ,
			DestinationProperty ,
			LookupTableName ,
			LookUpTableDisplayColumn  
		)
	VALUES
		(
			@MappingClassAssociationId ,
			@SourceProperty ,
			@DestinationProperty ,
			@LookupTableName ,
			@LookUpTableDisplayColumn  
		)
		
		SELECT cast(@@IDENTITY  as int)
	end
ELSE

begin
	UPDATE
		Discovery_MappingPropertyAssociation
	SET
		MappingClassAssociationId =@MappingClassAssociationId,
		SourceProperty =@SourceProperty,
		DestinationProperty =@DestinationProperty,
		LookupTableName =@LookupTableName,
		LookUpTableDisplayColumn  =@LookUpTableDisplayColumn			
	WHERE
		Id=@Id AND
		BINARY_CHECKSUM(*)=@CheckSum
	
		if @@ROWCOUNT=1
			SELECT @Id 
		Else
			SELECT -1
end

GO


