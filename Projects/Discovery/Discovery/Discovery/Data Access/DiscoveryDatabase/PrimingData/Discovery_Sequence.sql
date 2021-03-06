/*
This script was created by Visual Studio on 13/11/2006 at 09:52.
Run this script on robins.DiscoveryStaging.dbo to make it the same as robins.Discovery.dbo.
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- Pointer used for text / image updates. This might not be needed, but is declared here just in case
DECLARE @pv binary(16)
BEGIN TRANSACTION
SET IDENTITY_INSERT [dbo].[Discovery_Sequence] ON
INSERT INTO [dbo].[Discovery_Sequence] ([Id], [Name], [Seed], [Increment], [CurrentValue], [UpdatedDate], [UpdatedBy]) VALUES (2, N'ADHSHIPMENT', 1, 1, 0, '20061026 00:00:00.000', 'lo_t')
INSERT INTO [dbo].[Discovery_Sequence] ([Id], [Name], [Seed], [Increment], [CurrentValue], [UpdatedDate], [UpdatedBy]) VALUES (3, N'WHSSHIPEMNT', 1, 1, 0, '20061026 00:00:00.000', 'lo_t')
SET IDENTITY_INSERT [dbo].[Discovery_Sequence] OFF
COMMIT TRANSACTION
