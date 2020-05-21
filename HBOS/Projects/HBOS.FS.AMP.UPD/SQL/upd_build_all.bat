echo off

REM ************************************************************************
REM  File:  upd_build_all.bat
REM
REM  Build all objects in UPD database (defaultdb of user upd)
REM
REM  This bat file is in the directory SQL and run from this directory.
REM
REM  Build:- (1) Tables (with constraints, triggers)
REM          (2) User Defined Functions
REM          (3) Views
REM          (4) Stored procedures
REM          (5) Users db access + roles
REM
REM  Can run this bat file from command window as follows:-
REM
REM         upd_build_all.bat > result.log
REM
REM  By: TG    Date: 9/2005
REM  ************************************************************************

set userid=upd
set pwd=upd
set SRV=SQLSRV95

set RUNSQL=osql -U%userid% -P%pwd% -S%SRV% -i

REM  *********************************************************************************************
REM  Build dbo.udf_CheckCircularReferenceBenchmarks.UDF
REM 
REM  NB:  used by Constraint of BenchmarkSplit Table
REM  *********************************************************************************************

cd "User Defined Functions"

%RUNSQL%	dbo.udf_CheckCircularReferenceBenchmarks.UDF

REM  *******************************************************************************
REM  Build all TABLEs (39 as of 14/9/2005)
REM
REM  Have to generate ONE script files for all tables which contains the constraints 
REM  and the triggers as well.
REM  *******************************************************************************

cd ..\Tables

%RUNSQL% upd_build_tables.sql


REM  *********************************************************************************************
REM  Build all UDFs (45 as of 14/9/2005)
REM 
REM  NB:  
REM  *********************************************************************************************

cd ..\"User Defined Functions"

%RUNSQL%	dbo.udf_calculateAssetMovement.UDF
%RUNSQL%	dbo.udf_calculateAuthorisedPriceMovement.UDF
%RUNSQL%	dbo.udf_calculateCurrencyMovement.UDF
%RUNSQL%	dbo.udf_calculateMarketMovement.UDF
%RUNSQL%	dbo.udf_calculatePriceMovement.UDF

%RUNSQL%	dbo.udf_DetermineStockMarket.UDF
%RUNSQL%	dbo.udf_DistributionFilesAvailableFundCount.UDF
%RUNSQL%	dbo.udf_DistributionFilesAvailableFundCountForComposite.UDF
%RUNSQL%	dbo.udf_DistributionFilesExpectedFundCount.UDF
%RUNSQL%	dbo.udf_DistributionFilesExpectedFundCountForComposite.UDF
%RUNSQL%	dbo.udf_getCurrencyCodeForAssetFund.UDF
%RUNSQL%	dbo.udf_getCurrencyCodeForStockMarketIndex.UDF
%RUNSQL%	dbo.udf_getCurrencyCurrentRate.UDF
%RUNSQL%	dbo.udf_getCurrencyPreviousRate.UDF
%RUNSQL%	dbo.udf_getCurrentAssetMovementTolerance.UDF
%RUNSQL%	dbo.udf_getCurrentAssetPrice.UDF
%RUNSQL%	dbo.udf_getCurrentAuthorisedFundPrice.UDF
%RUNSQL%	dbo.udf_getCurrentFundImportedPrice.UDF
%RUNSQL%	dbo.udf_getCurrentFundPrice.UDF
%RUNSQL%	dbo.udf_getCurrentFundStatus.UDF
%RUNSQL%	dbo.udf_GetCurrentPricingDayForCompany.UDF
%RUNSQL%	dbo.udf_getCurrentStockMarketIndex.UDF
%RUNSQL%	dbo.udf_getCurrentValuationPoint.UDF
%RUNSQL%	dbo.udf_getExternalFundIdentifier.UDF
%RUNSQL%	dbo.udf_getExternalFundIdentifierByFileId.UDF
%RUNSQL%	dbo.udf_getFundGroupFullNameByHiPortfolioCode.UDF
%RUNSQL%	dbo.udf_getFundGroupNumberByHiPortfolioCode.UDF
%RUNSQL%	dbo.udf_getImportedFundPriceFromAuthorisedDetails.UDF
%RUNSQL%	dbo.udf_getLastAuthorisedYield.UDF
%RUNSQL%	dbo.udf_getLastYield.UDF
%RUNSQL%	dbo.udf_GetNextPricingDayForCompany.UDF
%RUNSQL%	dbo.udf_GetNextWorkingDate.UDF
%RUNSQL%	dbo.udf_getNumericVerionOfFundCode.UDF
%RUNSQL%	dbo.udf_getPreviousAuthorisedAssetPrice.UDF
%RUNSQL%	dbo.udf_getPreviousAuthorisedPrice.UDF
%RUNSQL%	dbo.udf_getPreviousAuthorisedPriceUsingCurrentValuationBasis.UDF
%RUNSQL%	dbo.udf_getPreviousAuthorisedPriceWasFromPrediction.UDF
%RUNSQL%	dbo.udf_getPreviousOfferPrice.UDF
%RUNSQL%	dbo.udf_GetPreviousPricingDay.UDF
%RUNSQL%	dbo.udf_GetPreviousPricingDayForCompany.UDF
%RUNSQL%	dbo.udf_getPreviousStockMarketIndex.UDF
%RUNSQL%	dbo.udf_getScalingFactor.UDF
%RUNSQL%	dbo.udf_getValuationBasisEffect.UDF
%RUNSQL%	dbo.udf_isInitialUnits.UDF

REM  *********************************************************************************************
REM  Build all VIEWs (6 as of 14/9/2005)
REM 
REM  NB:  dbo.uvw_LatestFundStatuses.VIW  created before dbo.uvw_FundsAvailableForDistribution.VIW
REM  *********************************************************************************************

cd ..\Views

%RUNSQL% dbo.uvw_AssetFundPrices.VIW
%RUNSQL% dbo.uvw_FundPrices.VIW
%RUNSQL% dbo.uvw_LatestFundStatuses.VIW
%RUNSQL% dbo.uvw_FundsAvailableForDistribution.VIW
%RUNSQL% dbo.uvw_FundsForDistribution.VIW
%RUNSQL% dbo.uvw_GetCurrentPricingDayForCompany.VIW

REM  *********************************************************************************************
REM  Build all STORED PROCEDUREs (183 as of 14/9/2005)
REM 
REM  NB:  
REM  *********************************************************************************************

cd ..\"Stored Procedures"

REM  ************* NB: The following 13 SPROCS are created first.
%RUNSQL% 	dbo.dt_generateansiname.PRC
%RUNSQL% 	dbo.dt_adduserobject.PRC
%RUNSQL% 	dbo.dt_adduserobject_vcs.PRC
%RUNSQL% 	dbo.dt_setpropertybyid.PRC
%RUNSQL% 	dbo.dt_setpropertybyid_u.PRC
%RUNSQL% 	dbo.dt_displayoaerror.PRC
%RUNSQL% 	dbo.dt_displayoaerror_u.PRC
%RUNSQL% 	dbo.dt_getpropertiesbyid.PRC
%RUNSQL% 	dbo.dt_getpropertiesbyid_u.PRC
%RUNSQL% 	dbo.dt_getpropertiesbyid_vcs.PRC
%RUNSQL% 	dbo.dt_getpropertiesbyid_vcs_u.PRC
%RUNSQL% 	dbo.usp_DailyFundStatusUpdate.PRC
%RUNSQL% 	dbo.usp_ImportedSplitWorkingTableClear.PRC

%RUNSQL% 	dbo.aaa_AuthorisedFundPriceUnRelease.PRC
%RUNSQL% 	dbo.dt_addtosourcecontrol.PRC
%RUNSQL% 	dbo.dt_addtosourcecontrol_u.PRC
%RUNSQL% 	dbo.dt_checkinobject.PRC
%RUNSQL% 	dbo.dt_checkinobject_u.PRC
%RUNSQL% 	dbo.dt_checkoutobject.PRC
%RUNSQL% 	dbo.dt_checkoutobject_u.PRC
%RUNSQL% 	dbo.dt_droppropertiesbyid.PRC
%RUNSQL% 	dbo.dt_dropuserobjectbyid.PRC
%RUNSQL% 	dbo.dt_getobjwithprop.PRC
%RUNSQL% 	dbo.dt_getobjwithprop_u.PRC
%RUNSQL% 	dbo.dt_isundersourcecontrol.PRC
%RUNSQL% 	dbo.dt_isundersourcecontrol_u.PRC
%RUNSQL% 	dbo.dt_removefromsourcecontrol.PRC
%RUNSQL% 	dbo.dt_validateloginparams.PRC
%RUNSQL% 	dbo.dt_validateloginparams_u.PRC
%RUNSQL% 	dbo.dt_vcsenabled.PRC
%RUNSQL% 	dbo.dt_verstamp006.PRC
%RUNSQL% 	dbo.dt_verstamp007.PRC
%RUNSQL% 	dbo.dt_whocheckedout.PRC
%RUNSQL% 	dbo.dt_whocheckedout_u.PRC

%RUNSQL% 	dbo.kaj_AssetFundAuthorise.PRC
%RUNSQL% 	dbo.kaj_AuthorisedFundPriceCreate.PRC
%RUNSQL% 	dbo.rrn_DistributionFilesGetForMaintenance.PRC
%RUNSQL% 	dbo.rrn_DistributionFilesUpdateForMaintenance.PRC
%RUNSQL% 	dbo.usp_AssetFundCheckExistence.PRC
%RUNSQL% 	dbo.usp_AssetFundCheckFullNameShortNameDuplication.PRC
%RUNSQL% 	dbo.usp_AssetFundCreate.PRC
%RUNSQL% 	dbo.usp_AssetFundDelete.PRC
%RUNSQL% 	dbo.usp_AssetFundGetStaticData.PRC
%RUNSQL% 	dbo.usp_AssetFundIDsGet.PRC
%RUNSQL% 	dbo.usp_AssetFundIndexWeightingsImport.PRC
%RUNSQL% 	dbo.usp_AssetFundsForStaticDataExportByCompanyCode.PRC
%RUNSQL% 	dbo.usp_AssetFundsGetLookupsForCompanyCode.PRC
%RUNSQL% 	dbo.usp_AssetFundsGetLookupsForCompanyCodeByType.PRC
%RUNSQL% 	dbo.usp_AssetFundsGetPriceDetailsForAssetFundID.PRC
%RUNSQL% 	dbo.usp_AssetFundsGetPriceDetailsForCompany.PRC
%RUNSQL% 	dbo.usp_AssetFundsGetPriceDetailsForFundGroupID.PRC
%RUNSQL% 	dbo.usp_AssetFundUpdate.PRC

REM redundant
REM %RUNSQL% 	dbo.usp_AuthorisedFundPriceCreate.PRC

%RUNSQL% 	dbo.usp_AuthorisedFundPriceRelease.PRC
%RUNSQL% 	dbo.usp_AuthorisedFundPriceUnauthorise.PRC
%RUNSQL% 	dbo.usp_BenchMarkGetAll.PRC
%RUNSQL% 	dbo.usp_BenchmarkSplitActivate.PRC
%RUNSQL% 	dbo.usp_BenchmarkSplitClear.PRC
%RUNSQL% 	dbo.usp_BenchmarkSplitCreate.PRC
%RUNSQL% 	dbo.usp_BenchmarkSplitGetAllByCompanyCode.PRC
%RUNSQL% 	dbo.usp_BenchmarkSplitGetByAssetFundID.PRC
%RUNSQL% 	dbo.usp_ClientVersionCheck.PRC
%RUNSQL% 	dbo.usp_CompanyPricingDayAdvance.PRC
%RUNSQL% 	dbo.usp_CompositeWeightingsImport.PRC
%RUNSQL% 	dbo.usp_CountriesList.PRC
%RUNSQL% 	dbo.usp_countryCreate.PRC
%RUNSQL% 	dbo.usp_CountryDelete.PRC
%RUNSQL% 	dbo.usp_CountryGetStaticData.PRC
%RUNSQL% 	dbo.usp_CountryUpdate.PRC
%RUNSQL% 	dbo.usp_CurrenciesList.PRC
%RUNSQL% 	dbo.usp_CurrencyCreate.PRC
%RUNSQL% 	dbo.usp_CurrencyDelete.PRC
%RUNSQL% 	dbo.usp_CurrencyGetRatesForCompany.PRC
%RUNSQL% 	dbo.usp_CurrencyGetStaticData.PRC
%RUNSQL% 	dbo.usp_CurrencyRatesActivate.PRC
%RUNSQL% 	dbo.usp_CurrencyRatesCreate.PRC
%RUNSQL% 	dbo.usp_CurrencyRatesListForSnapshotID.PRC
%RUNSQL% 	dbo.usp_CurrencyUpdate.PRC
%RUNSQL% 	dbo.usp_DateTimeGet.PRC
%RUNSQL% 	dbo.usp_DistributionFileAssociateFundGroup.PRC
%RUNSQL% 	dbo.usp_DistributionFilesDeleteForFundGroupID.PRC
%RUNSQL% 	dbo.usp_DistributionFilesExecute.PRC
%RUNSQL% 	dbo.usp_DistributionFilesGetForDistribution.PRC
%RUNSQL% 	dbo.usp_DistributionFilesGetForFundGroupID.PRC
%RUNSQL% 	dbo.usp_DistributionFilesGetLookupsForCompanyCode.PRC
%RUNSQL% 	dbo.usp_DistributionFilesUpdate.PRC
%RUNSQL% 	dbo.usp_DoesUserExistForLoginID.PRC
%RUNSQL% 	dbo.usp_ExternalSystemsList.PRC
%RUNSQL% 	dbo.usp_FundCheckDuplication.PRC
%RUNSQL% 	dbo.usp_FundCheckExistence.PRC
%RUNSQL% 	dbo.usp_FundCreate.PRC
%RUNSQL% 	dbo.usp_FundDelete.PRC
%RUNSQL% 	dbo.usp_FundExternalSystemIdentifiersCreate.PRC
%RUNSQL% 	dbo.usp_FundExternalSystemIdentifiersDelete.PRC
%RUNSQL% 	dbo.usp_FundExternalSystemIdentifiersListForHiPortfolioCode.PRC
%RUNSQL% 	dbo.usp_FundExternalSystemIdentifiersUpdate.PRC
%RUNSQL% 	dbo.usp_FundGetImportFields.PRC
%RUNSQL% 	dbo.usp_FundGetImportFieldsForCompanyCode.PRC
%RUNSQL% 	dbo.usp_FundGetLookupsForCompanyCode.PRC
%RUNSQL% 	dbo.usp_FundGetStaticData.PRC
%RUNSQL% 	dbo.usp_FundGetStaticDataForAssetFundID.PRC
%RUNSQL% 	dbo.usp_FundGetStaticDataForCompanyCode.PRC
%RUNSQL% 	dbo.usp_FundGroupAssociateAssetFund.PRC
%RUNSQL% 	dbo.usp_FundGroupAssociateFund.PRC
%RUNSQL% 	dbo.usp_FundGroupCheckForDuplication.PRC
%RUNSQL% 	dbo.usp_FundGroupCreate.PRC
%RUNSQL% 	dbo.usp_FundGroupDelete.PRC
%RUNSQL% 	dbo.usp_FundGroupGetStaticData.PRC
%RUNSQL% 	dbo.usp_FundGroupsDeleteForAssetFundID.PRC
%RUNSQL% 	dbo.usp_FundGroupsDeleteForHiPortfolioCode.PRC
%RUNSQL% 	dbo.usp_FundGroupsGetForAssetFundID.PRC
%RUNSQL% 	dbo.usp_FundGroupsGetForCompanyCode.PRC
%RUNSQL% 	dbo.usp_FundGroupsGetForCompanyCodeByType.PRC
%RUNSQL% 	dbo.usp_FundGroupsGetForHiPortfolioCodeDirect.PRC
%RUNSQL% 	dbo.usp_FundGroupsGetLookupsForAssetFundID.PRC
%RUNSQL% 	dbo.usp_FundGroupsGetLookupsForCompanyCode.PRC
%RUNSQL% 	dbo.usp_FundGroupUpdate.PRC
%RUNSQL% 	dbo.usp_FundsGetPriceDetailsForCompany.PRC
%RUNSQL% 	dbo.usp_FundsGetPriceDetailsForFundGroupID.PRC
%RUNSQL% 	dbo.usp_FundsGetPriceDetailsForHiPortfolioCode.PRC
%RUNSQL% 	dbo.usp_FundToleranceCreate.PRC
%RUNSQL% 	dbo.usp_FundUpdate.PRC
%RUNSQL% 	dbo.usp_GetNextWorkingDate.PRC
%RUNSQL% 	dbo.usp_GrantPermissions.PRC
%RUNSQL% 	dbo.usp_HBOSCompanyCheckForAuthorisedPrice.PRC
%RUNSQL% 	dbo.usp_HBOSCompanyCreate.PRC
%RUNSQL% 	dbo.usp_HBOSCompanyGetForCompanyCode.PRC
%RUNSQL% 	dbo.usp_HBOSCompanyGetForCompanyName.PRC
%RUNSQL% 	dbo.usp_HBOSCompanyListForUser.PRC
%RUNSQL% 	dbo.usp_HBOSCompanyUpdate.PRC
%RUNSQL% 	dbo.usp_HolidaysGet.PRC
%RUNSQL% 	dbo.usp_ImportedAssetFundSplitListForSnapshotID.PRC
%RUNSQL% 	dbo.usp_ImportedCompositeFundSplitActivate.PRC
%RUNSQL% 	dbo.usp_ImportedCompositeFundSplitListForSnapshotID.PRC
%RUNSQL% 	dbo.usp_ImportedFileVerifyNewFile.PRC
%RUNSQL% 	dbo.usp_ImportedFundPricesActivate.PRC
%RUNSQL% 	dbo.usp_ImportedFundPricesCreate.PRC
%RUNSQL% 	dbo.usp_ImportedFundPricesListForSnapshotID.PRC
%RUNSQL% 	dbo.usp_ImportedIndexValuesActivate.PRC
%RUNSQL% 	dbo.usp_ImportedIndexValuesCreate.PRC
%RUNSQL% 	dbo.usp_ImportedIndexValuesListForSnapshotID.PRC
%RUNSQL% 	dbo.usp_ImportedSplitWorkingTableCreate.PRC
%RUNSQL% 	dbo.usp_ImportedSplitWorkingTableDelete.PRC
%RUNSQL% 	dbo.usp_ImportSourceGetCurrent.PRC
%RUNSQL% 	dbo.usp_IsBenchmarkRelatedToAssetFund.PRC
%RUNSQL% 	dbo.usp_LoadAssetFundSplitsFromTempTable.PRC
%RUNSQL% 	dbo.usp_MarketIndicesList.PRC
%RUNSQL% 	dbo.usp_PermissionsAddAllForAllUsersToCompany.PRC
%RUNSQL% 	dbo.usp_PredictedPriceHistoricalReportByFund.PRC
%RUNSQL% 	dbo.usp_PredictedPriceHistoricalReportByFundGroup.PRC
%RUNSQL% 	dbo.usp_ReportDataGenerateBoxExtract.PRC
%RUNSQL% 	dbo.usp_ReportDataGenerateBoxExtractOnDemand.PRC
%RUNSQL% 	dbo.usp_ReportDataGenerateComposite.PRC
%RUNSQL% 	dbo.usp_ReportDataGenerateGRP.PRC
%RUNSQL% 	dbo.usp_ReportDataGenerateNew.PRC
%RUNSQL% 	dbo.usp_ReportLoadPriceComparisionData.PRC
%RUNSQL% 	dbo.usp_RevaluationFactorCreate.PRC
%RUNSQL% 	dbo.usp_ScalingFactorCreate.PRC
%RUNSQL% 	dbo.usp_SnapshotActivate.PRC
%RUNSQL% 	dbo.usp_SnapshotCancel.PRC
%RUNSQL% 	dbo.usp_SnapshotCreate.PRC
%RUNSQL% 	dbo.usp_StockMarketCreate.PRC
%RUNSQL% 	dbo.usp_StockMarketDelete.PRC
%RUNSQL% 	dbo.usp_StockMarketGetStaticData.PRC
%RUNSQL% 	dbo.usp_StockMarketIndexGetWithoutPricing.PRC
%RUNSQL% 	dbo.usp_StockMarketIndexGetWithPricing.PRC
%RUNSQL% 	dbo.usp_StockMarketListForCountryCode.PRC
%RUNSQL% 	dbo.usp_StockMarketUpdate.PRC
%RUNSQL% 	dbo.usp_SystemStatusImports.PRC
%RUNSQL% 	dbo.usp_TaxProvisionFactorCreate.PRC
%RUNSQL% 	dbo.usp_UserCompanyPermissions.PRC
%RUNSQL% 	dbo.usp_UserGetStaticData.PRC
%RUNSQL% 	dbo.usp_UserIsValid.PRC
%RUNSQL% 	dbo.usp_UserPermissionsGetAll.PRC
%RUNSQL% 	dbo.usp_UserPermissionsUpdate.PRC
%RUNSQL% 	dbo.usp_UsersCreate.PRC
%RUNSQL% 	dbo.usp_UsersDelete.PRC
%RUNSQL% 	dbo.usp_UsersGetAll.PRC
%RUNSQL% 	dbo.usp_UsersGetLastCompanyForLoginID.PRC
%RUNSQL% 	dbo.usp_UsersSetLastCompanyForLoginID.PRC
%RUNSQL% 	dbo.usp_UsersStaticDataExport.PRC
%RUNSQL% 	dbo.usp_UsersUpdate.PRC
%RUNSQL% 	dbo.usp_XFactorCreate.PRC

REM  22-9-2005
%RUNSQL% 	dbo.usp_AuthorisedFundPriceCreate.PRC

REM  *********************************************************************************************
REM  Build all USERs + roles (17 as of 14/9/2005)
REM 
REM  NB:  
REM  *********************************************************************************************

cd ..\Users

REM  grant db access

%RUNSQL% ACCSPXN.USR
%RUNSQL% ACCSRXN.USR             
%RUNSQL% accstxg.USR             
%RUNSQL% conpxn.USR

REM  add role member

%RUNSQL% db_backupoperator.USR   
%RUNSQL% db_datareader.USR       
%RUNSQL% db_datawriter.USR
%RUNSQL% db_owner.USR   

REM  grant db access
         
%RUNSQL% djr.USR                 
%RUNSQL% maw.USR
%RUNSQL% pdcsazb.USR             
%RUNSQL% pdcsmxb.USR             
%RUNSQL% pdcsrzb.USR
%RUNSQL% pjp.USR                 
%RUNSQL% sjr.USR                 
%RUNSQL% upd.USR

REM  add role member

%RUNSQL% UPD_USER.USR    


:buildexit        

cd ..
echo Build completed
echo Press any key to exit

REM pause > nul

