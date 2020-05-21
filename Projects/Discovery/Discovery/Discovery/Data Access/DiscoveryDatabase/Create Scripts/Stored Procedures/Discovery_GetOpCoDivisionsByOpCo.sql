IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoDivisionsByOpCo')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoDivisionsByOpCo
	END

GO

CREATE Procedure Discovery_GetOpCoDivisionsByOpCo
(
	@opCoCode varchar(10)
)

AS

SELECT 
	D.*, O.Code 
FROM 
	discovery_opcodivision AS D
JOIN  
	discovery_opco AS O on O.Id = D.OpCoId
WHERE 
	O.Code = @opCoCode

GO

