 IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_AcknowledgeLog')
	BEGIN
		DROP  Procedure  Discovery_AcknowledgeLog
	END

GO

CREATE Procedure Discovery_AcknowledgeLog

	(
		@LogID int,
		@AcknowledgedBy varchar(256),
		@AcknowledgedDate datetime
	)

AS


update 
	[Log]
set  
	AcknowledgedBy=@AcknowledgedBy,
	AcknowledgedDate=@AcknowledgedDate
where 
	LogID=@LogId

go


