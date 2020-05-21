SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE usp_IsUserIdValid 
  @sUserID varchar(50)
AS
  select count(SecUserID) as IsValid from SecUsers where SecUserID = @sUserID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

