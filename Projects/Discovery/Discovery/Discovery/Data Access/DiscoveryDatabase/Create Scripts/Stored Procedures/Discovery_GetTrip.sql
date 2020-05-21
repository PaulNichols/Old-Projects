IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTrip')
	BEGIN
		DROP  Procedure  Discovery_GetTrip
	END

GO

CREATE Procedure Discovery_GetTrip

	(
		@Id int 
		
	)


AS


SELECT     
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM         
	Discovery_Trip
WHERE     
	Id = @id
	
GO


