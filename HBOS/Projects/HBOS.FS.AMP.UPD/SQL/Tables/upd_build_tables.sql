if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AssetFundsInFundGroups_AssetFunds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AssetFundsInFundGroups] DROP CONSTRAINT FK_AssetFundsInFundGroups_AssetFunds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedAssetFundPrice_AssetFunds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedAssetFundPrice] DROP CONSTRAINT FK_AuthorisedAssetFundPrice_AssetFunds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BenchmarkSplit_AssetFunds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[BenchmarkSplit] DROP CONSTRAINT FK_BenchmarkSplit_AssetFunds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Funds_AssetFunds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Funds] DROP CONSTRAINT FK_Funds_AssetFunds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_CompanyUserPermission_CompanyPermission]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[CompanyUserPermission] DROP CONSTRAINT FK_CompanyUserPermission_CompanyPermission
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_MarketIndices_Countries]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[MarketIndices] DROP CONSTRAINT FK_MarketIndices_Countries
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedFundPrice_Currencies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedFundPrice] DROP CONSTRAINT FK_AuthorisedFundPrice_Currencies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Countries_Currencies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Countries] DROP CONSTRAINT FK_Countries_Currencies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_currencyRates_Currencies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[CurrencyRates] DROP CONSTRAINT FK_currencyRates_Currencies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ImportedFundPrices_Currencies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ImportedFundPrices] DROP CONSTRAINT FK_ImportedFundPrices_Currencies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FixedPrices_DistributionFiles]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FixedPrices] DROP CONSTRAINT FK_FixedPrices_DistributionFiles
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FundGroupsInFiles_DistributionFiles]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FundGroupsInFiles] DROP CONSTRAINT FK_FundGroupsInFiles_DistributionFiles
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_DistributionFiles_ExternalSystems]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[DistributionFiles] DROP CONSTRAINT FK_DistributionFiles_ExternalSystems
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FundExternalSystemIdentifiers_ExternalSystems]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FundExternalSystemIdentifiers] DROP CONSTRAINT FK_FundExternalSystemIdentifiers_ExternalSystems
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FixedPrices_FixedPricesType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FixedPrices] DROP CONSTRAINT FK_FixedPrices_FixedPricesType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AssetFundsInFundGroups_FundGroups]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AssetFundsInFundGroups] DROP CONSTRAINT FK_AssetFundsInFundGroups_FundGroups
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FundGroupsInFiles_FundGroups]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FundGroupsInFiles] DROP CONSTRAINT FK_FundGroupsInFiles_FundGroups
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FundsInFundGroups_FundGroups]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FundsInFundGroups] DROP CONSTRAINT FK_FundsInFundGroups_FundGroups
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_DailyFundStatus_FundStatus]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[DailyFundStatus] DROP CONSTRAINT FK_DailyFundStatus_FundStatus
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_DailyFundStatusAuditTrail_FundStatus1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[DailyFundStatusAuditTrail] DROP CONSTRAINT FK_DailyFundStatusAuditTrail_FundStatus1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_DistributionFiles_FundStatus]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[DistributionFiles] DROP CONSTRAINT FK_DistributionFiles_FundStatus
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_DistributionFiles_FundStatus1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[DistributionFiles] DROP CONSTRAINT FK_DistributionFiles_FundStatus1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedAssetFundPrice_FundTolerances]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedAssetFundPrice] DROP CONSTRAINT FK_AuthorisedAssetFundPrice_FundTolerances
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedFundPrice_FundTolerances]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedFundPrice] DROP CONSTRAINT FK_AuthorisedFundPrice_FundTolerances
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedFundPrice_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedFundPrice] DROP CONSTRAINT FK_AuthorisedFundPrice_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BenchmarkSplit_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[BenchmarkSplit] DROP CONSTRAINT FK_BenchmarkSplit_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_DailyFundStatusAuditTrail_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[DailyFundStatusAuditTrail] DROP CONSTRAINT FK_DailyFundStatusAuditTrail_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FixedPrices_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FixedPrices] DROP CONSTRAINT FK_FixedPrices_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FundExternalSystemIdentifiers_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FundExternalSystemIdentifiers] DROP CONSTRAINT FK_FundExternalSystemIdentifiers_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FundsInFundGroups_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FundsInFundGroups] DROP CONSTRAINT FK_FundsInFundGroups_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FundTolerances_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FundTolerances] DROP CONSTRAINT FK_FundTolerances_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ImportedFundPrices_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ImportedFundPrices] DROP CONSTRAINT FK_ImportedFundPrices_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_RevaluationFactor_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[RevaluationFactor] DROP CONSTRAINT FK_RevaluationFactor_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_scalingFactor_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ScalingFactor] DROP CONSTRAINT FK_scalingFactor_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_taxProvisionFactor_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[TaxProvisionFactor] DROP CONSTRAINT FK_taxProvisionFactor_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_valuationBasis_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[valuationBasis] DROP CONSTRAINT FK_valuationBasis_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_xFactor_Funds]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[XFactor] DROP CONSTRAINT FK_xFactor_Funds
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_CompanyPermission_HBOSCompanies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[CompanyPermission] DROP CONSTRAINT FK_CompanyPermission_HBOSCompanies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_currencyRates_HBOSCompanies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[CurrencyRates] DROP CONSTRAINT FK_currencyRates_HBOSCompanies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_FundGroups_HBOSCompanies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[FundGroups] DROP CONSTRAINT FK_FundGroups_HBOSCompanies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ImportedIndexValues_HBOSCompanies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ImportedIndexValues] DROP CONSTRAINT FK_ImportedIndexValues_HBOSCompanies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_PriceFiles_HBOSCompanies]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[PriceFiles] DROP CONSTRAINT FK_PriceFiles_HBOSCompanies
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BenchmarkSplit_MarketIndices]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[BenchmarkSplit] DROP CONSTRAINT FK_BenchmarkSplit_MarketIndices
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ImportedIndexValues_MarketIndices]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ImportedIndexValues] DROP CONSTRAINT FK_ImportedIndexValues_MarketIndices
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_CompanyPermission_Permission]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[CompanyPermission] DROP CONSTRAINT FK_CompanyPermission_Permission
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AssetFunds_PriceFiles]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AssetFunds] DROP CONSTRAINT FK_AssetFunds_PriceFiles
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedFundPrice_revaulationFactor]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedFundPrice] DROP CONSTRAINT FK_AuthorisedFundPrice_revaulationFactor
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedFundPrice_scalingFactor]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedFundPrice] DROP CONSTRAINT FK_AuthorisedFundPrice_scalingFactor
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BenchmarkSplit_Snapshot]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[BenchmarkSplit] DROP CONSTRAINT FK_BenchmarkSplit_Snapshot
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_currencyRates_Snapshot]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[CurrencyRates] DROP CONSTRAINT FK_currencyRates_Snapshot
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedFundPrice_taxProvisionFactor]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedFundPrice] DROP CONSTRAINT FK_AuthorisedFundPrice_taxProvisionFactor
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_CompanyUserPermission_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[CompanyUserPermission] DROP CONSTRAINT FK_CompanyUserPermission_Users
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedFundPrice_xFactor]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedFundPrice] DROP CONSTRAINT FK_AuthorisedFundPrice_xFactor
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AuthorisedFundPrice_valuationBasis]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AuthorisedFundPrice] DROP CONSTRAINT FK_AuthorisedFundPrice_valuationBasis
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AssetFunds]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AssetFunds]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AssetFundsInFundGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AssetFundsInFundGroups]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthorisedAssetFundPrice]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AuthorisedAssetFundPrice]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthorisedFundBenchmarks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AuthorisedFundBenchmarks]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthorisedFundPrice]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AuthorisedFundPrice]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BenchmarkSplit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[BenchmarkSplit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ClientVersions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ClientVersions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CompanyPermission]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CompanyPermission]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CompanyUserPermission]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CompanyUserPermission]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Countries]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Countries]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Currencies]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Currencies]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrencyRates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CurrencyRates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DailyFundStatus]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DailyFundStatus]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DailyFundStatusAuditTrail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DailyFundStatusAuditTrail]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistributionAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistributionAudit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistributionFiles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistributionFiles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ExternalSystems]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ExternalSystems]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ExternalSystemsHBOSCompany]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ExternalSystemsHBOSCompany]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FixedPrices]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FixedPrices]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FixedPricesType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FixedPricesType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FundExternalSystemIdentifiers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FundExternalSystemIdentifiers]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FundGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FundGroups]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FundGroupsInFiles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FundGroupsInFiles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FundStatus]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FundStatus]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FundTolerances]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FundTolerances]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Funds]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Funds]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FundsInFundGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FundsInFundGroups]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[HBOSCompanies]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[HBOSCompanies]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Holidays]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Holidays]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ImportedFundPrices]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ImportedFundPrices]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ImportedIndexValues]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ImportedIndexValues]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ImportedSplitWorkingTable]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ImportedSplitWorkingTable]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MarketIndices]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[MarketIndices]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Permission]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Permission]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PriceFiles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[PriceFiles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Report_Assetfund_Identity]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Report_Assetfund_Identity]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Report_Benchmark_Identity]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Report_Benchmark_Identity]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Report_Company_Identity]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Report_Company_Identity]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Report_Fund_Identity]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Report_Fund_Identity]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Report_Pricing_Fact]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Report_Pricing_Fact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Report_Results]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Report_Results]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Report_Valuationdate_Fact]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Report_Valuationdate_Fact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RevaluationFactor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[RevaluationFactor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ScalingFactor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ScalingFactor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Snapshot]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Snapshot]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TaxProvisionFactor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TaxProvisionFactor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[XFactor]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[XFactor]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dtproperties]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[dtproperties]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[valuationBasis]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[valuationBasis]
GO

CREATE TABLE [dbo].[AssetFunds] (
	[assetFundID] [char] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[shortName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[fullName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[companyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[fundType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PriceFileId] [int] NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AssetFundsInFundGroups] (
	[assetFundID] [char] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[fundGroupID] [int] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AuthorisedAssetFundPrice] (
	[authorisedAssetFundPriceID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[assetFundID] [char] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[authorisationDate] [smalldatetime] NOT NULL ,
	[assetUnitPrice] [money] NOT NULL ,
	[currencyRateSnapshotID] [bigint] NULL ,
	[importedFundPricesID] [bigint] NULL ,
	[tolerancesID] [bigint] NULL ,
	[marketIndiciesSnapshotID] [bigint] NULL ,
	[benchmarkSplitSnapshotID] [bigint] NULL ,
	[AssetMovement] [decimal](12, 6) NULL ,
	[indexValue] [decimal](18, 6) NULL ,
	[active] [int] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AuthorisedFundBenchmarks] (
	[authorisedAssetFundPriceID] [bigint] NOT NULL ,
	[authorisedFundPriceID] [bigint] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AuthorisedFundPrice] (
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[authorisationDate] [datetime] NOT NULL ,
	[price] [money] NOT NULL ,
	[currencyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[valuationPoint] [datetime] NOT NULL ,
	[wasFromPrediction] [bit] NOT NULL ,
	[fundPriceSnapshotID] [bigint] NULL ,
	[authorisedAssetFundPriceID] [bigint] NULL ,
	[tolerancesID] [bigint] NULL ,
	[taxProvisionFactorID] [int] NULL ,
	[revaluationFactorID] [int] NULL ,
	[scalingFactorID] [int] NULL ,
	[xFactorID] [int] NULL ,
	[valuationBasisID] [int] NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL ,
	[authorisedFundPriceID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[importedPrice] [money] NULL ,
	[predictedPrice] [money] NULL ,
	[distributedOfferPrice] [money] NULL ,
	[distributedMidPrice] [money] NULL ,
	[distributedBarePrice] [money] NULL ,
	[wasMidPriceBidPrice] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BenchmarkSplit] (
	[benchmarkSplitID] [int] IDENTITY (1, 1) NOT NULL ,
	[assetFundID] [char] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[snapshotID] [bigint] NOT NULL ,
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[marketIndexID] [int] NULL ,
	[proportion] [decimal](18, 6) NOT NULL ,
	[active] [smallint] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[deleted] [smallint] NOT NULL ,
	[ts] [timestamp] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ClientVersions] (
	[Major] [int] NOT NULL ,
	[Minor] [int] NOT NULL ,
	[Build] [int] NOT NULL ,
	[Revision] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CompanyPermission] (
	[CompanyPermissionId] [int] IDENTITY (1, 1) NOT NULL ,
	[CompanyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PermissionId] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CompanyUserPermission] (
	[CompanyPermissionId] [int] NOT NULL ,
	[loginID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Countries] (
	[countryCode] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[country] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL ,
	[currencyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Currencies] (
	[currencyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[currency] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CurrencyRates] (
	[snapshotID] [bigint] NOT NULL ,
	[currencyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[valuationPoint] [datetime] NOT NULL ,
	[rate] [decimal](18, 6) NOT NULL ,
	[active] [int] NOT NULL ,
	[companyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DailyFundStatus] (
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[statusDate] [smalldatetime] NOT NULL ,
	[statusID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DailyFundStatusAuditTrail] (
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[statusDate] [smalldatetime] NOT NULL ,
	[changeDateTime] [datetime] NOT NULL ,
	[changedFrom] [int] NULL ,
	[changedTo] [int] NOT NULL ,
	[changedBy] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DistributionAudit] (
	[DistributionAuditId] [bigint] IDENTITY (1, 1) NOT NULL ,
	[FileContents] [varbinary] (8000) NOT NULL ,
	[FileId] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DistributionFiles] (
	[FileID] [int] IDENTITY (1, 1) NOT NULL ,
	[FileDesc] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CompanyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Filename] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Filepath] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[XsltLocation] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[XsltSource] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SprocName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[LastDistributed] [datetime] NULL ,
	[SystemId] [int] NULL ,
	[lastChangedBy] [char] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[VersionNumber] [int] NOT NULL ,
	[AvailableStatus] [int] NOT NULL ,
	[StatusToMoveTo] [int] NOT NULL ,
	[AllowPartialDistribution] [bit] NOT NULL ,
	[FundGroupNumberRequired] [bit] NOT NULL ,
	[manipulationClassToInvoke] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[DecimalPlacesRequired] [bit] NOT NULL ,
	[SignificantDecimalPlacesRequired] [bit] NOT NULL ,
	[MajorDenominationRequired] [bit] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[ExternalSystems] (
	[systemID] [int] IDENTITY (1, 1) NOT NULL ,
	[systemName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[identifierName] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ExternalSystemsHBOSCompany] (
	[ExternalSystemsId] [int] NOT NULL ,
	[CompanyCode] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FixedPrices] (
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Price] [money] NOT NULL ,
	[FixedPriceTypeId] [int] NOT NULL ,
	[FileID] [int] NOT NULL ,
	[AlternateCode] AS ([dbo].[udf_getExternalFundIdentifierByFileId]([hiportfoliocode], [fileid])) 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FixedPricesType] (
	[FixedPricesTypeId] [int] IDENTITY (1, 1) NOT NULL ,
	[PriceType] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FundExternalSystemIdentifiers] (
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[systemID] [int] NOT NULL ,
	[alternativeFundCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FundGroups] (
	[fundGroupID] [int] IDENTITY (1, 1) NOT NULL ,
	[companyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[containsType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[fullName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[shortName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[forRelease] [bit] NOT NULL ,
	[allowSelectAllAuthorisation] [bit] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FundGroupsInFiles] (
	[FileID] [int] NOT NULL ,
	[FundGroupID] [int] NOT NULL ,
	[FundGroupNumber] [int] NULL ,
	[UseMajorDenomination] [bit] NOT NULL ,
	[NumberOfDecimalPlaces] [smallint] NOT NULL ,
	[NumberOfSignificantDecimalPlaces] [smallint] NOT NULL ,
	[lastChangedBy] [char] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FundStatus] (
	[statusID] [int] NOT NULL ,
	[description] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FundTolerances] (
	[tolerancesID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inUse] [int] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[assetMovement] [decimal](18, 6) NULL ,
	[upperTolerance] [decimal](18, 6) NULL ,
	[lowerTolerance] [decimal](18, 6) NULL ,
	[priceIncreaseOnly] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Funds] (
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[fullName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[shortName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[fundClassOrSeries] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[fundType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[isDual] [bit] NULL ,
	[isExDividend] [bit] NULL ,
	[isLife] [bit] NULL ,
	[isMidPriceBidPrice] [bit] NULL ,
	[assetFundID] [char] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[fundOnHi3] [bit] NOT NULL ,
	[priceType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[fundSpecificValuationPoint] [datetime] NULL ,
	[securityCode] [varchar] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[isBenchmarkable] [bit] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FundsInFundGroups] (
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[fundGroupID] [int] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[HBOSCompanies] (
	[companyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[companyName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[valuationPoint] [datetime] NOT NULL ,
	[activePricingDay] [datetime] NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL ,
	[defaultImportSource] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[defaultDistributeArchiveFolder] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Holidays] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[HolidayDate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ImportedFundPrices] (
	[snapshotID] [bigint] NOT NULL ,
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[valuationPoint] [datetime] NOT NULL ,
	[active] [int] NOT NULL ,
	[bidPrice] [money] NULL ,
	[offerPrice] [money] NULL ,
	[assetUnitPrice] [money] NULL ,
	[barePrice] [money] NULL ,
	[yield] [money] NULL ,
	[unroundedBidPrice] [money] NULL ,
	[unroundedOfferPrice] [money] NULL ,
	[currencyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[valuationBasis] [char] (1) COLLATE Latin1_General_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[policyHolderUnits] [decimal](14, 4) NULL ,
	[compositeUnits] [decimal](14, 4) NULL ,
	[equitableUnits] [decimal](14, 4) NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ImportedIndexValues] (
	[snapshotID] [bigint] NOT NULL ,
	[marketIndexID] [int] NOT NULL ,
	[active] [int] NOT NULL ,
	[indexValue] [decimal](18, 5) NOT NULL ,
	[valuationPoint] [datetime] NOT NULL ,
	[companyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ImportedSplitWorkingTable] (
	[snapshotID] [bigint] NOT NULL ,
	[primaryKey] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[secondaryKey] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[importedValue] [decimal](18, 4) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MarketIndices] (
	[marketIndexID] [int] IDENTITY (1, 1) NOT NULL ,
	[indexName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[countryCode] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[global] [bit] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Permission] (
	[PermissionId] [int] IDENTITY (1, 1) NOT NULL ,
	[DisplayName] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ParentId] [int] NULL ,
	[UniqueName] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[IsGroup] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PriceFiles] (
	[PriceFilesId] [int] IDENTITY (1, 1) NOT NULL ,
	[FileName] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Description] [varchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Extension] [char] (3) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CompanyCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Report_Assetfund_Identity] (
	[AssetFundID] [char] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ShortName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FullName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FundType] [varchar] (9) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Report_Benchmark_Identity] (
	[BenchmarkID] [bigint] NOT NULL ,
	[ValuationDate] [datetime] NOT NULL ,
	[BenchmarkName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[BenchmarkCurrency] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IsStockMarket] [varchar] (1) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Report_Company_Identity] (
	[CompanyID] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CompanyName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Report_Fund_Identity] (
	[HIPortCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FullName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ShortName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ShareClassPriceSeries] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FundType] [varchar] (9) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SecurityCode] [varchar] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FundOnHI3] [char] (1) COLLATE Latin1_General_CI_AS NOT NULL ,
	[DualPrice] [char] (1) COLLATE Latin1_General_CI_AS NOT NULL ,
	[MIDBIDPrice] [char] (1) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ExDividend] [char] (1) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Life] [char] (1) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Benchmarkable] [char] (1) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Report_Pricing_Fact] (
	[FundID] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ValuationDate] [datetime] NOT NULL ,
	[FundXFactor] [decimal](18, 7) NULL ,
	[FundScalingFactor] [decimal](18, 7) NULL ,
	[FundTPE] [decimal](18, 7) NULL ,
	[FundRevaluationFactor] [decimal](18, 7) NULL ,
	[FundLowerTolerance] [decimal](22, 6) NULL ,
	[FundUpperTolerance] [decimal](22, 6) NULL ,
	[FundPriceIncreaseOnly] [char] (1) COLLATE Latin1_General_CI_AS NULL ,
	[FundImportedPrice] [money] NULL ,
	[FundPriceVariance] [decimal](15, 6) NULL ,
	[FundWithinPriceTolerance] [varchar] (11) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FundAssetUnitPrice] [money] NULL ,
	[AssetFundLowerTolerance] [decimal](22, 6) NULL ,
	[AssetFundUpperTolerance] [decimal](22, 6) NULL ,
	[AssetFundPriceIncreaseOnly] [char] (1) COLLATE Latin1_General_CI_AS NULL ,
	[BenchmarkID] [bigint] NULL ,
	[CompanyID] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[AssetFundID] [char] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DistributedOfferPrice] [money] NULL ,
	[DistributedMidPrice] [money] NULL ,
	[DistributedBarePrice] [money] NULL ,
	[DistributedBidPrice] [money] NULL ,
	[ValuationBasisEffect] [decimal](10, 6) NULL ,
	[ValuationBasis] [char] (1) COLLATE Latin1_General_CI_AS NULL ,
	[PredictedPriceUsed] [char] (1) COLLATE Latin1_General_CI_AS NULL ,
	[PredictedPrice] [money] NULL ,
	[AssetFundPredictedAM] [decimal](18, 6) NULL ,
	[AssetFundAMVariance] [decimal](22, 6) NULL ,
	[AssetFundWithinTolerance] [varchar] (1) COLLATE Latin1_General_CI_AS NULL ,
	[IndexValue] [decimal](18, 6) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Report_Results] (
	[RunDate] [smalldatetime] NOT NULL ,
	[Result] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Report_Valuationdate_Fact] (
	[DD] [int] NOT NULL ,
	[MM] [int] NOT NULL ,
	[YY] [int] NOT NULL ,
	[CC] [int] NOT NULL ,
	[ValuationDate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RevaluationFactor] (
	[revaluationFactorID] [int] IDENTITY (1, 1) NOT NULL ,
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[totalChange] [decimal](18, 7) NOT NULL ,
	[effectiveDate] [smalldatetime] NOT NULL ,
	[endDate] [smalldatetime] NULL ,
	[workingDays] [int] NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ScalingFactor] (
	[scalingFactorID] [int] IDENTITY (1, 1) NOT NULL ,
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[scalingFactor] [decimal](18, 7) NOT NULL ,
	[appliedDate] [smalldatetime] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Snapshot] (
	[snapshotID] [bigint] IDENTITY (10000, 1) NOT NULL ,
	[userID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[snapshotDate] [datetime] NOT NULL ,
	[process] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[importFilename] [varchar] (512) COLLATE Latin1_General_CI_AS NULL ,
	[companyCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TaxProvisionFactor] (
	[taxProvisionFactorID] [int] IDENTITY (1, 1) NOT NULL ,
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[provisionEstimate] [decimal](18, 7) NOT NULL ,
	[effectiveDate] [smalldatetime] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Users] (
	[loginID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[userName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastCompany] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[XFactor] (
	[xFactorID] [int] IDENTITY (1, 1) NOT NULL ,
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[xFactor] [decimal](18, 7) NOT NULL ,
	[appliedDate] [smalldatetime] NOT NULL ,
	[narrative] [varchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[dtproperties] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[objectid] [int] NULL ,
	[property] [varchar] (64) COLLATE Latin1_General_CI_AS NOT NULL ,
	[value] [varchar] (255) COLLATE Latin1_General_CI_AS NULL ,
	[uvalue] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
	[lvalue] [image] NULL ,
	[version] [int] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[valuationBasis] (
	[valuationBasisID] [int] IDENTITY (1, 1) NOT NULL ,
	[hiPortfolioCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[valuationBasisEffect] [decimal](10, 6) NOT NULL ,
	[appliedDate] [smalldatetime] NOT NULL ,
	[lastChangedBy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[lastChangedDate] [datetime] NOT NULL ,
	[ts] [timestamp] NOT NULL ,
	[deleted] [int] NOT NULL 
) ON [PRIMARY]
GO

