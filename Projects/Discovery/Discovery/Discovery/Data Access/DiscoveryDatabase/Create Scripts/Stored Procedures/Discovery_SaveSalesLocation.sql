IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_SaveSalesLocation')
	BEGIN
		DROP  Procedure  Discovery_SaveSalesLocation
	END

GO

CREATE Procedure Discovery_SaveSalesLocation

	(
		@Id int= null,
		@Location varchar(50),
		@Telephone varchar(20),
		@Description varchar(256),
		@OpcoId int,
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id =-1
	BEGIN
		INSERT INTO
			Discovery_SalesLocation
			(
				Location,
				TelephoneNumber,
				Description,
				OpcoId,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Location,
				@Telephone,
				@Description,
				@OpcoId,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Discovery_SalesLocation
		SET
				Location		=	@Location,
				TelephoneNumber		=	@Telephone,
				Description		=	@Description,
				OpcoId			=	@OpcoId,
				UpdatedDate		=	getdate(),
				UpdatedBy		=	@UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


