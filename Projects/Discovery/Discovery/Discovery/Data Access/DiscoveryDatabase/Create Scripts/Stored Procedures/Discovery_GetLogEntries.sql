IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetLogEntries')
	BEGIN
		DROP  Procedure  Discovery_GetLogEntries
	END

GO

CREATE Procedure Discovery_GetLogEntries
(
	@CategoryId int,
    @Acknowledged bit, 
    @ErrorType varchar(256), 
    @MessageText nvarchar(1500), 
    @OpcoCode char(3), 
    @Priority int, 
    @Severity nvarchar(32), 
    @TimeStampFrom datetime, 
    @TimeStampTo datetime,
	@sortExpression varchar(1000),
	@startRowIndex int,
    @maximumRows int
)
AS


select 
*,Category.CategoryName  
from (
	SELECT 
		*
	FROM
	   (
			SELECT 
				 [Log].LogId as Id,				
				 [Log].*,
				ROW_NUMBER() OVER(
					ORDER BY				
					-- 'TimeStamp
					CASE WHEN @sortExpression = 'TimeStamp' 
						 THEN [TimeStamp] ELSE NULL END ASC,
					CASE WHEN @sortExpression = 'TimeStamp desc' 
						 THEN [TimeStamp] ELSE NULL END DESC,
	     
					-- EventId
					CASE WHEN @sortExpression = 'EventId' 
						 THEN EventId ELSE NULL END ASC,
					CASE WHEN @sortExpression = 'EventId desc' 
						 THEN EventId ELSE NULL END DESC,
						 
					-- Title
					CASE WHEN @sortExpression = 'Title' 
						 THEN Title ELSE NULL END ASC,
					CASE WHEN @sortExpression = 'Title desc' 
						 THEN Title ELSE NULL END DESC,
						 
					-- Priority
					CASE WHEN @sortExpression = 'Priority' 
						 THEN Priority ELSE NULL END ASC,
					CASE WHEN @sortExpression = 'Priority desc' 
						 THEN Priority ELSE NULL END DESC,
	       
					-- Severity
					CASE WHEN @sortExpression = 'Severity' 
						 THEN Severity ELSE NULL END ASC,
					CASE WHEN @sortExpression = 'Severity desc' 
						 THEN Severity ELSE NULL END DESC
	  
					) as RowNum
			FROM         [Log] 
			where 
				(ErrorType=@ErrorType  or @ErrorType is null) and
				(@MessageText is null or (not @MessageText is null and MESSAGE LIKE '%'+@MessageText+'%' )) and
				(@OpcoCode is null or (not @OpcoCode  is null and MESSAGE LIKE '%'+@OpcoCode+'%') ) and		
				(Severity=@Severity  or @Severity is null) and
				([TimeStamp] >= @TimeStampFrom OR @TimeStampFrom IS NULL) AND
				([TimeStamp] <= @TimeStampTo OR @TimeStampTo IS NULL) and
				(@Acknowledged=0 or @Acknowledged is null or  (@Acknowledged=1 and (AcknowledgedBy<>'' and not AcknowledgedBy is null)))
	   ) as DerivedTableName
	WHERE RowNum BETWEEN @startRowIndex AND (@startRowIndex + @maximumRows) - 1) as DerivedTableName2  
			INNER JOIN
                      CategoryLog ON DerivedTableName2.LogID = CategoryLog.LogID inner join
                      Category ON CategoryLog.CategoryID = Category.CategoryID
                      where Category.CategoryId=@CategoryId or @CategoryId is null

	--SET @TotalRowCount=(select Count(logid) from [log])
--return 99

/*

SELECT
	logid as activityid,
	threadname as ManagedThreadName,
	formattedmessage as errormessages,
	*
FROM
	[log]*/

GO

