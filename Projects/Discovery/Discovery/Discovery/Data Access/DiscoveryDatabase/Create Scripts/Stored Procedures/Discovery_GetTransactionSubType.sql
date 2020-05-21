IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTransactionSubType')
	BEGIN
		DROP  Procedure  Discovery_GetTransactionSubType
	END

GO

CREATE Procedure Discovery_GetTransactionSubType
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_TransactionSubType
WHERE
	Id=@Id

GO

 