IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetLogEntries')
	BEGIN
		DROP  Procedure  Discovery_GetLogCategories
	END

GO

CREATE Procedure Discovery_GetLogCategories

AS	
	SELECT     distinct Category.*
	FROM         Category INNER JOIN
	                      CategoryLog ON Category.CategoryID = CategoryLog.CategoryID 
	order by categoryname

GO

 