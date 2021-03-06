/*
This script was created by Visual Studio on 14/11/2006 at 09:13.
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
ALTER TABLE [dbo].[Discovery_MappingPropertyAssociation] DROP CONSTRAINT [FK_Discovery_MappingPropertyAssociation_Discovery_MappingClassAssociation]
SET IDENTITY_INSERT [dbo].[Discovery_MappingPropertyAssociation] ON
INSERT INTO [dbo].[Discovery_MappingPropertyAssociation] ([Id], [MappingClassAssociationId], [SourceProperty], [DestinationProperty], [LookupTableName], [LookUpTableDisplayColumn]) VALUES (3, 1, 'RouteCode', 'RouteCode', 'Discovery_Route', 'Description')
INSERT INTO [dbo].[Discovery_MappingPropertyAssociation] ([Id], [MappingClassAssociationId], [SourceProperty], [DestinationProperty], [LookupTableName], [LookUpTableDisplayColumn]) VALUES (4, 1, 'StockWarehouseCode', 'StockWarehouseCode', 'Discovery_Warehouse', 'Code')
INSERT INTO [dbo].[Discovery_MappingPropertyAssociation] ([Id], [MappingClassAssociationId], [SourceProperty], [DestinationProperty], [LookupTableName], [LookUpTableDisplayColumn]) VALUES (5, 1, 'DeliveryWarehouseCode', 'DeliveryWarehouseCode', 'Discovery_Warehouse', 'Code')
INSERT INTO [dbo].[Discovery_MappingPropertyAssociation] ([Id], [MappingClassAssociationId], [SourceProperty], [DestinationProperty], [LookupTableName], [LookUpTableDisplayColumn]) VALUES (4501, 1, 'TransactionType', 'TransactionType', 'Discovery_TransactionType', 'Code')
INSERT INTO [dbo].[Discovery_MappingPropertyAssociation] ([Id], [MappingClassAssociationId], [SourceProperty], [DestinationProperty], [LookupTableName], [LookUpTableDisplayColumn]) VALUES (4502, 1, 'TransactionSubType', 'TransactionSubType', 'Discovery_TransactionSubType', 'Description')
SET IDENTITY_INSERT [dbo].[Discovery_MappingPropertyAssociation] OFF
SET IDENTITY_INSERT [dbo].[Discovery_MappingSystem] ON
INSERT INTO [dbo].[Discovery_MappingSystem] ([Id], [Name], [IsSource], [IsDestination]) VALUES (1, 'RHG', 1, 0)
INSERT INTO [dbo].[Discovery_MappingSystem] ([Id], [Name], [IsSource], [IsDestination]) VALUES (2, 'TDC', 0, 1)
INSERT INTO [dbo].[Discovery_MappingSystem] ([Id], [Name], [IsSource], [IsDestination]) VALUES (3, 'Commander', 0, 1)
INSERT INTO [dbo].[Discovery_MappingSystem] ([Id], [Name], [IsSource], [IsDestination]) VALUES (4, 'HSP', 1, 0)
INSERT INTO [dbo].[Discovery_MappingSystem] ([Id], [Name], [IsSource], [IsDestination]) VALUES (5, 'TPC', 1, 0)
SET IDENTITY_INSERT [dbo].[Discovery_MappingSystem] OFF
SET IDENTITY_INSERT [dbo].[Discovery_MappingClassAssociation] ON
INSERT INTO [dbo].[Discovery_MappingClassAssociation] ([Id], [SourceType], [DestinationType], [SourceTypeFullName], [DestinationTypeFullName]) VALUES (1, 'OpCoShipment', 'TDCShipment', 'Discovery.BusinessObjects.OpCoShipment', 'Discovery.BusinessObjects.TDCShipment')
SET IDENTITY_INSERT [dbo].[Discovery_MappingClassAssociation] OFF
ALTER TABLE [dbo].[Discovery_MappingPropertyAssociation] ADD CONSTRAINT [FK_Discovery_MappingPropertyAssociation_Discovery_MappingClassAssociation] FOREIGN KEY ([MappingClassAssociationId]) REFERENCES [dbo].[Discovery_MappingClassAssociation] ([Id])
COMMIT TRANSACTION
