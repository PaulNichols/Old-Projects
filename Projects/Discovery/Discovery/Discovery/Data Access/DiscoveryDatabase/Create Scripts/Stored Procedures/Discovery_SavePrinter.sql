IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SavePrinter')
	BEGIN
		DROP  Procedure  Discovery_SavePrinter
	END

GO

CREATE Procedure Discovery_SavePrinter

	(
		@Id int= null,
		@Name varchar(256),		
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Discovery_Printer
			(
				[Name]/*,
				UpdatedDate,
				UpdatedBy*/
			)
		VALUES
			(
				@Name/*,
				getdate(),
				@UpdatedBy*/
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_Printer
		SET
				[Name]		=	@Name/*,
				UpdatedDate	=	getdate(),
				UpdatedBy	=	@UpdatedBy*/
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


