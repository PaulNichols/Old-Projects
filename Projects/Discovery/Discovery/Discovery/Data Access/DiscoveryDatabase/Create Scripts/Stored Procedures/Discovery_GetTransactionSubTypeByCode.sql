IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTransactionSubTypeByCode')
	BEGIN
		DROP  Procedure  Discovery_GetTransactionSubTypeByCode
	END

GO

CREATE Procedure Discovery_GetTransactionSubTypeByCode
	(
		@code varchar(20)
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_TransactionSubType
WHERE
	Code=@code

GO

 