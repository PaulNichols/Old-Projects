IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTransactionSubTypes')
	BEGIN
		DROP  Procedure  Discovery_GetTransactionSubTypes
	END

GO

CREATE Procedure Discovery_GetTransactionSubTypes

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_TransactionSubType

GO

