IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveMappingClassAssociation')
	BEGIN
		DROP  Procedure  Discovery_SaveMappingClassAssociation
	END

GO

CREATE Procedure Discovery_SaveMappingClassAssociation

	(
		@Id int= null,
		@SourceType varchar(200),
		@SourceTypeFullName varchar(200),
		@DestinationType varchar(200),
		@DestinationTypeFullName varchar(200),
		@CheckSum int
	)


AS

IF @Id =-1
begin
	INSERT INTO
		Discovery_MappingClassAssociation
		(
			SourceType,
			SourceTypeFullName ,
			DestinationType ,
			DestinationTypeFullName 
		)
	VALUES
		(
			@SourceType,
			@SourceTypeFullName ,
			@DestinationType ,
			@DestinationTypeFullName 
		)
		
		SELECT cast(@@IDENTITY  as int)
	end
ELSE

begin
	UPDATE
		Discovery_MappingClassAssociation
	SET
			SourceType=@SourceType,
			SourceTypeFullName =@SourceTypeFullName,
			DestinationType =@DestinationType,
			DestinationTypeFullName =@DestinationTypeFullName
			
	WHERE
		Id=@Id AND
		BINARY_CHECKSUM(*)=@CheckSum
	
		if @@ROWCOUNT=1
			SELECT @Id 
		Else
			SELECT -1
end

GO


