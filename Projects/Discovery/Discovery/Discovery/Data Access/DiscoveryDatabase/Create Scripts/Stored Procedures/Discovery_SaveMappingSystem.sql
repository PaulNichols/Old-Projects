IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveMappingSystem')
	BEGIN
		DROP  Procedure  Discovery_SaveMappingSystem
	END

GO

CREATE Procedure Discovery_SaveMappingSystem

	(
		@Id int= null,
		@Name varchar(50),
		@IsSource bit,
		@IsDestination bit,
		@CheckSum int
	)


AS

IF @Id =-1
begin
	INSERT INTO
		Discovery_MappingSystem
		(
			[Name],
			IsSource ,
			IsDestination 
		)
	VALUES
		(
			@Name,
			@IsSource ,
			@IsDestination
		)
		
		SELECT cast(@@IDENTITY  as int)
	end
ELSE

begin
	UPDATE
		Discovery_MappingSystem
	SET
			[Name]=@Name,
			IsSource =@IsSource,
			IsDestination =@IsDestination
			
	WHERE
		Id=@Id AND
		BINARY_CHECKSUM(*)=@CheckSum
	
		if @@ROWCOUNT=1
			SELECT @Id 
		Else
			SELECT -1
end

GO


