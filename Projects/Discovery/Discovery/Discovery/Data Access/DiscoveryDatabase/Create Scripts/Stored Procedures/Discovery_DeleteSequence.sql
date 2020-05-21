IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteSequence')
	BEGIN
		DROP  Procedure  Discovery_DeleteSequence
	END

GO

CREATE Procedure Discovery_DeleteSequence
(
	@Id int
)
AS
DELETE
FROM Discovery_Sequence
WHERE
	Id=@Id

GO
