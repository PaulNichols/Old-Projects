IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteTransactionType')
	BEGIN
		DROP  Procedure  Discovery_DeleteTransactionType
	END

GO

CREATE Procedure Discovery_DeleteTransactionType
(
	@Id int
)
AS
DELETE
FROM Discovery_TransactionType
WHERE
	Id=@Id

GO
