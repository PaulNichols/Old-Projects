IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveSequence')
	BEGIN
		DROP  Procedure  Discovery_SaveSequence
	END

GO

CREATE PROCEDURE Discovery_SaveSequence
		@id int = -1,
		@name nvarchar(255),
		@seed int = 1,
		@increment int = 1,
		@currentValue int = 1,
		@UpdatedBy varchar(256),
		@CheckSum int

AS
	IF @seed IS NULL SET @seed = 1
	IF @increment IS NULL SET @increment = 1
	
	IF -1 = @id
		BEGIN
			SET @currentValue = @seed
			
			INSERT INTO Discovery_Sequence 
			(
				[Name], 
				[Seed], 
				[Increment], 
				[CurrentValue],
				UpdatedDate,
				UpdatedBy

			)
			VALUES 
			(
				@name, 
				@seed, 
				@increment, 
				@currentValue,
				getdate(),
				@UpdatedBy

			)
			
		SELECT cast(@@IDENTITY  as int)
			
		END
	ELSE
		BEGIN
			UPDATE 
				Discovery_Sequence
			SET 
				[Name] = @name,
				[Seed] = @seed,
				[Increment] = @increment,
				[CurrentValue] = @currentValue,
				UpdatedDate	=	getdate(),
				UpdatedBy	=	@UpdatedBy

			WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
			
			IF @@ROWCOUNT = 1
				SELECT @Id 
			ELSE
				SELECT -1
				
		END
GO
