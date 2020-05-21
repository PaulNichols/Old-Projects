IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_SaveConnection')
	BEGIN
		DROP  Procedure  Integration_SaveConnection
	END

GO

CREATE Procedure Integration_SaveConnection

	(
		@Id int= null,
		@Name varchar(256),
		@ScheduleId int,
		@Active bit,
		@ConnectionType int,
		@ChannelType int,
		@ConnectionSettings varchar(MAX),
		@UpdatedBy varchar(256),
		@CheckSum int
	)


AS

IF @Id=-1
	BEGIN
		INSERT INTO
			Integration_Connection
			(
				[Name],
				ScheduleId ,
				Active ,
				ConnectionType ,
				ChannelType,
				SettingsSerialised ,
				UpdatedDate,
				UpdatedBy
			)
		VALUES
			(
				@Name,
				@ScheduleId ,
				@Active ,
				@ConnectionType ,
				@ChannelType,
				@ConnectionSettings ,
				getdate(),
				@UpdatedBy
			)
			
		SELECT cast(@@IDENTITY  as int)
	END	
ELSE
	BEGIN
		UPDATE
			Integration_Connection
		SET
				[Name]				=	@Name,
				ScheduleId			=	@ScheduleId,
				Active				=	@Active,
				ConnectionType		=	@ConnectionType,
				ChannelType			=	@ChannelType,
				SettingsSerialised	=	@ConnectionSettings,
				UpdatedDate			=	getdate(),
				UpdatedBy			=	@UpdatedBy
		WHERE
			Id=@Id AND
			BINARY_CHECKSUM(*)=@CheckSum
		
		IF @@ROWCOUNT=1
			SELECT @Id 
		ELSE
			SELECT -1
	END
GO


