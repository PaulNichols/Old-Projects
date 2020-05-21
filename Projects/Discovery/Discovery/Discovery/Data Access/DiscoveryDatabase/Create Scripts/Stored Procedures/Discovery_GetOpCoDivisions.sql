IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoDivisions')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoDivisions
	END

GO

CREATE Procedure Discovery_GetOpCoDivisions

AS

SELECT
	id,code,opcoid
FROM
	Discovery_OpcoDivision

GO

