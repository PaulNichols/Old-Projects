IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Integration_GetTasks')
	BEGIN
		DROP  Procedure  Integration_GetTasks
	END

GO

CREATE Procedure Integration_GetTasks


AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Integration_Task


GO

 