SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE usp_GetRolesForUser 
  @sUserID varchar(50)
AS
select distinct SecRoles.SecRoleName as Role
from SecUsers
  join SecMemberships on (SecMemberships.SecUserID = SecUsers.SecUserID)
  join SecGroups on (SecGroups.SecGroupID = SecMemberships.SecGroupID)
  join SecGroupRoles on (SecGroupRoles.SecGroupID = secGroups.SecGroupID)
  join SecRoles on (SecRoles.SecRoleID = secGroupRoles.SecRoleID)
where SecUsers.SecUserID = @sUserID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

