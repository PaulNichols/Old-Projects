IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveOpCoDivision')
	BEGIN
		DROP  Procedure  Discovery_SaveOpCoDivision
	END

GO

CREATE Procedure Discovery_SaveOpCoDivision

	(
		@Id int= null,
		@Code varchar(3),
		@OpCoId int,
		@Logo VarBinary(MAX),
		@LogoURI varchar(256),
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id =-1
	BEGIN
		INSERT INTO
			Discovery_OpCoDivision
			(
				Code,				
				OpCoId,
				Logo,
				LogoURI,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Code,
				@OpCoId,
				@Logo,
				@LogoURI,
				getdate(),
				@UpdatedBy
			)
		
		SELECT cast(@@IDENTITY  as int)
	END
ELSE
	BEGIN
		UPDATE
			Discovery_OpCoDivision
		SET
				Code		=	@Code,
				OpCoId		=	@OpCoId,
				Logo		=	@Logo,
				@LogoURI	=	@LogoURI,
				UpdatedDate	=	getdate(),
				UpdatedBy	=	@UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum

		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
	
GO


