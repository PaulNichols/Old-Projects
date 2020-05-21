IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetLogEntry')
	BEGIN
		DROP  Procedure  Discovery_GetLogEntry
	END

GO

CREATE Procedure Discovery_GetLogEntry
	(
		@Id int
	)

AS

SELECT 
	[Log].*,
	Category.CategoryName  
FROM
	[Log] INNER JOIN
                      CategoryLog ON [Log].logID = CategoryLog.LogID inner join
                      Category ON CategoryLog.CategoryID = Category.CategoryID
WHERE
	[Log].logID=@Id

GO

 