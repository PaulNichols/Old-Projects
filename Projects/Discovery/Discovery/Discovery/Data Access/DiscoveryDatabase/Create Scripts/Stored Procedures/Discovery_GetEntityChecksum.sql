IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetEntityChecksum')
	BEGIN
		DROP  Procedure  Discovery_GetEntityChecksum
	END
GO

CREATE Procedure Discovery_GetEntityChecksum
	(
		@Qualifier varchar(50),
		@Id int = -1,
		@TableName varchar(50)
	)

AS
	DECLARE @SqlDef nvarchar(512)
	SET @SqlDef = N'SELECT ISNULL((SELECT BINARY_CHECKSUM(*) FROM ' + @Qualifier + @TableName + N' WITH (NOLOCK) WHERE Id = ' + CAST(@Id AS varchar(20)) + '), -1) AS CheckSum'
	EXEC sp_executesql @SqlDef
GO


