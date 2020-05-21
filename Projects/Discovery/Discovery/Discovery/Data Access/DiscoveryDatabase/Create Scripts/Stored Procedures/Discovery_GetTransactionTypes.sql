IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTransactionTypes')
	BEGIN
		DROP  Procedure  Discovery_GetTransactionTypes
	END

GO

CREATE Procedure Discovery_GetTransactionTypes

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_TransactionType

GO

