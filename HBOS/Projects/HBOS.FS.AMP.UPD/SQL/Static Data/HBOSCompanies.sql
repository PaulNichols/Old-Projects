BEGIN TRANSACTION
INSERT INTO [dbo].[HBOSCompanies]([companyCode], [companyName], [lastChangedBy], [lastChangedDate], [valuationPoint], [deleted], [defaultImportSource], [defaultDistributeArchiveFolder])
VALUES('CMIG','Clerical Medical Investment Group','fsys',2005-06-17,1899-12-30,0,'\\elasrv03\data\FPCS\AMP\Unit Pricing and Distribution (UPD)\Data Files\Drop2 Imports\Drop2 CM','\\elasrv03\data\shared\FPCS\UPD\Distribution\Archive\UPD_DEV')
INSERT INTO [dbo].[HBOSCompanies]([companyCode], [companyName], [lastChangedBy], [lastChangedDate], [valuationPoint], [deleted], [defaultImportSource], [defaultDistributeArchiveFolder])
VALUES('HIFM','Halifax Investment Fund Managers','fsys',2005-06-17,1899-12-30,0,'\\elasrv03\data\acc\pvplive\','\\elasrv03\data\shared\FPCS\UPD\Distribution\Archive\UPD_DEV')
INSERT INTO [dbo].[HBOSCompanies]([companyCode], [companyName], [lastChangedBy], [lastChangedDate], [valuationPoint], [deleted], [defaultImportSource], [defaultDistributeArchiveFolder])
VALUES('HLL','Halifax Life Ltd.','fsys',2005-06-17,1899-12-30,0,'\\elasrv03\data\FPCS\AMP\Unit Pricing and Distribution (UPD)\Data Files\Drop2 Imports\Drop2 Halifax Life','\\elasrv03\data\shared\FPCS\UPD\Distribution\Archive\UPD_DEV')
INSERT INTO [dbo].[HBOSCompanies]([companyCode], [companyName], [lastChangedBy], [lastChangedDate], [valuationPoint], [deleted], [defaultImportSource], [defaultDistributeArchiveFolder])
VALUES('SALA','St Andrews Life Assurance','fsys',2005-06-17,1899-12-30,0,'\\elasrv03\data\FPCS\AMP\Unit Pricing and Distribution (UPD)\Data Files\Drop2 Imports\Drop2 SAL','\\elasrv03\data\shared\FPCS\UPD\Distribution\Archive\UPD_DEV')
GO
COMMIT TRANSACTION
GO