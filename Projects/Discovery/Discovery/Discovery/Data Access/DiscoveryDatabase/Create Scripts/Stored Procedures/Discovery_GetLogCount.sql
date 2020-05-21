IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetLogCount ')
	BEGIN
		DROP  Procedure  Discovery_GetLogCount 
	END

GO

CREATE Procedure Discovery_GetLogCount 
(
	@CategoryId int,
	@Acknowledged bit, 
    @ErrorType varchar(256), 
    @MessageText nvarchar(1500), 
    @OpcoCode char(3), 
    @Priority int, 
    @Severity nvarchar(32), 
    @TimeStampFrom datetime, 
    @TimeStampTo datetime
    )
AS


select count(DerivedTable.logid) 
from
(
	select logid from [log]
		where 
			(ErrorType=@ErrorType  or @ErrorType is null) and
				(@MessageText is null or (not @MessageText is null and MESSAGE LIKE '%'+@MessageText+'%' )) and
				(@OpcoCode is null or (not @OpcoCode  is null and MESSAGE LIKE '%'+@OpcoCode+'%') ) and		
			(Severity=@Severity  or @Severity is null) and
			([TimeStamp] >= @TimeStampFrom OR @TimeStampFrom IS NULL) AND
			([TimeStamp] <= @TimeStampTo OR @TimeStampTo IS NULL)  and
			(@Acknowledged=0 or @Acknowledged is null or  (@Acknowledged=1 and (AcknowledgedBy<>'' and not AcknowledgedBy is null)))
) as DerivedTable
	INNER JOIN
                      CategoryLog ON DerivedTable.LogID = CategoryLog.LogID 
                      where CategoryId=@CategoryId or @CategoryId is null
GO



