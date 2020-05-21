IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTransactionType')
	BEGIN
		DROP  Procedure  Discovery_GetTransactionType
	END

GO

CREATE Procedure Discovery_GetTransactionType
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_TransactionType
WHERE
	Id=@Id

GO

 