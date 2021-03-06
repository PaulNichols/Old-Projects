/*
This script was created by Visual Studio on 25/10/2006 at 08:45.
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

SET IDENTITY_INSERT [dbo].[Discovery_Opco] ON
INSERT INTO [dbo].[Discovery_Opco] ([Id], [Code], [Description], [UpdatedDate], [UpdatedBy]) VALUES (1, 'RHG', 'Robert Horne Group', '20060719 17:56:06.890', 'Paul Nichols')
INSERT INTO [dbo].[Discovery_Opco] ([Id], [Code], [Description], [UpdatedDate], [UpdatedBy]) VALUES (2, 'TPC', 'The Paper Company', '20060712 11:18:17.340', 'Paul Nichols')
INSERT INTO [dbo].[Discovery_Opco] ([Id], [Code], [Description], [UpdatedDate], [UpdatedBy]) VALUES (3, 'HSP', 'Howard Smith Paper', '20060731 12:20:20.010', 'Paul Nichols')
SET IDENTITY_INSERT [dbo].[Discovery_Opco] OFF

COMMIT TRANSACTION
