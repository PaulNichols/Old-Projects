IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'WriteLog')
	BEGIN
		DROP  Procedure  [dbo].[WriteLog]
	END

GO

create PROCEDURE [dbo].[WriteLog]
(
	@EventID int, 
	@Priority int, 
	@Severity nvarchar(32), 
	@Title nvarchar(256), 
	@Timestamp datetime,
	@MachineName nvarchar(32), 
	@AppDomainName nvarchar(512),
	@ProcessID nvarchar(256),
	@ProcessName nvarchar(512),
	@ThreadName nvarchar(512),
	@Win32ThreadId nvarchar(128),
	@Message nvarchar(1500),
	@FormattedMessage ntext,
	@LogId int OUTPUT
)
AS 

	INSERT INTO [Log] (
		EventID,
		Priority,
		Severity,
		Title,
		[Timestamp],
		MachineName,
		AppDomainName,
		ProcessID,
		ProcessName,
		ThreadName,
		Win32ThreadId,
		Message,
		FormattedMessage,
		ErrorType
	)
	VALUES (
		@EventID, 
		@Priority, 
		@Severity, 
		@Title, 
		@Timestamp,
		@MachineName, 
		@AppDomainName,
		@ProcessID,
		@ProcessName,
		@ThreadName,
		@Win32ThreadId,
		@Message,
		@FormattedMessage,
		(case when patindex('%exceptiontype%',@Message)>(0) then substring(@Message,patindex('%exceptiontype%',@Message)+(14),(charindex(',',@Message,patindex('%exceptiontype%',@Message))-patindex('%exceptiontype%',@Message))-(14)) else '' end)
		)

	SET @LogID = @@IDENTITY
	RETURN @LogID



go