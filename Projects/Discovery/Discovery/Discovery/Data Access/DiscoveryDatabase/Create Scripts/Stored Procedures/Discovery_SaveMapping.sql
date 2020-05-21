IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveMapping')
	BEGIN
		DROP  Procedure  Discovery_SaveMapping
	END

GO

CREATE Procedure Discovery_SaveMapping

	(
		@Id int= null,
		@MappingPropertyAssociationId int,
		@DestinationSystemId int,
		@DestinationValue varchar(256),
		@SourceSystemId int,
		@SourceValue varchar(256),
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id =-1
begin
	INSERT INTO
		Discovery_Mapping
		(
			MappingPropertyAssociationId ,
			DestinationSystemId ,
			DestinationValue ,
			SourceSystemId ,
			SourceValue ,
			UpdatedDate,
			UpdatedBy
		)
	VALUES
		(
			@MappingPropertyAssociationId ,
			@DestinationSystemId ,
			@DestinationValue ,
			@SourceSystemId ,
			@SourceValue ,
			getdate(),
			@UpdatedBy
		)
		
		SELECT cast(@@IDENTITY  as int)
	end
ELSE

begin
	UPDATE
		Discovery_Mapping
	SET
			MappingPropertyAssociationId=@MappingPropertyAssociationId ,
			DestinationSystemId=@DestinationSystemId ,
			DestinationValue=@DestinationValue ,
			SourceSystemId=@SourceSystemId ,
			SourceValue=@SourceValue ,
			UpdatedDate=getdate(),
			UpdatedBy=@UpdatedBy
			
	WHERE
		Id=@Id AND
		BINARY_CHECKSUM(*)=@CheckSum
	
		if @@ROWCOUNT=1
			SELECT @Id 
		Else
			SELECT -1
end

GO


