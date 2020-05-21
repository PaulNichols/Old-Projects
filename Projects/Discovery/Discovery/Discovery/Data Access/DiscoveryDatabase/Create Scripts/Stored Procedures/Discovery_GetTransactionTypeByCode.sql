IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTransactionTypeByCode')
	BEGIN
		DROP  Procedure  Discovery_GetTransactionTypeByCode
	END

GO

CREATE Procedure Discovery_GetTransactionTypeByCode
	(
		@Code varchar(20)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_TransactionType
WHERE
	Code=@Code

GO

 