IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteTransactionSubType')
	BEGIN
		DROP  Procedure  Discovery_DeleteTransactionSubType
	END

GO

CREATE Procedure Discovery_DeleteTransactionSubType
(
	@Id int
)
AS
DELETE
FROM Discovery_TransactionSubType
WHERE
	Id=@Id

GO
