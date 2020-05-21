IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoDivisionByCode')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoDivisionByCode
	END

GO

CREATE Procedure Discovery_GetOpCoDivisionByCode
	(
		@code varchar(50)
	)
AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_OpcoDivision
WHERE
	[Code] = @code

GO

