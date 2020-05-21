IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetErrorType')
	BEGIN
		DROP  Procedure  Discovery_GetErrorType
	END

GO

CREATE Procedure Discovery_GetErrorType
	(
		@ExceptionType varchar(256),
		@PolicyName varchar(256),
		@OpCoCode char(3)
	)

AS

DECLARE @OpCoId int

SELECT
	@OpCoId=Id
FROM
	Discovery_Opco
WHERE
	Code=@OpCoCode
	
SELECT     
	Discovery_ErrorType.Id, 
	Discovery_ErrorType.Policy, 
	Discovery_ErrorType.EmailOperator, 
	Discovery_ErrorType.Prority as Priority, 
	Discovery_ErrorType.RequiresAcknowledgement, 
	Discovery_ErrorType.ExceptionType, 
	Discovery_Opco.Id AS OpCoId, 
	Discovery_ErrorType.EmailSubject, 
	COALESCE (Discovery_ErrorOpcoSettings.EmailRecipients,'') as EmailRecipients, 
	BINARY_CHECKSUM(Discovery_ErrorType.Id, Discovery_ErrorType.Policy, 
                      Discovery_ErrorType.EmailOperator, Discovery_ErrorType.Prority, Discovery_ErrorType.RequiresAcknowledgement, 
                      Discovery_ErrorType.ExceptionType,Discovery_ErrorType.EmailSubject) AS CheckSum, 
	Discovery_Opco.Code AS OpcoCode
FROM         Discovery_ErrorType LEFT OUTER JOIN
                      Discovery_Opco ON Discovery_Opco.Id = @OpCoId LEFT OUTER JOIN
                      Discovery_ErrorOpcoSettings ON Discovery_ErrorType.Id = Discovery_ErrorOpcoSettings.ErrorTypeId AND 
                      (Discovery_ErrorOpcoSettings.OpCoId = @OpCoId or @OpCoId is null)
WHERE    
	(Discovery_ErrorType.ExceptionType = @ExceptionType) AND 
	(Discovery_ErrorType.Policy = @PolicyName)

	GO

