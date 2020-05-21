IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoTransactionTypes')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoTransactionTypes
	END

GO

CREATE Procedure Discovery_GetOpCoTransactionTypes

AS

SELECT DISTINCT
	TransactionTypeCode as Code,
	TransactionTypeCode as Description
FROM
	Discovery_OpCoShipment
	
ORDER BY TransactionTypeCode ASC

GO

