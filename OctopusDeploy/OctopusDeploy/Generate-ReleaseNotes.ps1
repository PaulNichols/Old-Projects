<# 
.SYNOPSIS 
    Will generate a Release Notes file given a Release Package file and a target environment. 

.DESCRIPTION 
    Given a Release Package file and a target Environment, a release Notes file is generated and store in a given filename
    The list of applications and versions in the target Environment are compared against the list of applications and versions defined in
    the release package file. Where there are differences the Release Notes for each version is looked up. Where there are Jiras mentioned
    in the Release Notes, any populated "Release Notes" sections of those Jiras are also included.

    This functionality is part of Deploy-ReleasePackage as well, but is provided as a stand alone utility if required.    

.LINK 
    http://wiki.racqgroup.local/Confluence/display/tech/Generate-ReleaseNotes+script

.PARAMETER ServerName 
   The name of the Octopus Deploy server to call. Defaults to RACQ's current instance.
.PARAMETER EnvironmentName 
   The name of the environment to query. Eg. PR501-DEV, PRE-PROD (must match what is defined in Octopus Deploy)
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER ReleasePackageFile
   The filename where the Release Packages XML file will be written
.PARAMETER ReleaseNotesFile
   The filename where the Release Note text file will be written. If not provided, it will be the same as the release package file with a .txt instead of .xml

.EXAMPLE 
    .\Generate-ReleaseNotes.ps1 -EnvironmentName PR501-SIT -ApiKey $octoAPIKey -ReleasePackageFile C:\temp\PR501-DEV-RelPackage.xml -ReleaseNotesFile c:\temp\PR501-SITRelNotes.txt
    This will generate a file in c:\temp\PR501-SITRelNotes.txt based on the differences between the applications listed in C:\temp\PR501-DEV-RelPackage.xml
    and what is currently deployed in PR501-SIT.
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [string]$ApiKey = $OctopusApiKey,

    [Parameter(Mandatory=$true)]
    [string]$EnvironmentName,

    [Parameter(Mandatory=$true)]
    [string]$ReleasePackageFile,
        
    [string]$ReleaseNotesFile = ""
    )

# -------------------------------------------------------------------------------------------------------------------------------------------------
# MAIN
# -------------------------------------------------------------------------------------------------------------------------------------------------

# load up common modules
. .\CommonFunctions.ps1
. .\HelperFunctions.ps1
$ProgressPreference="SilentlyContinue" 

Write-Host "Reading Octopus Environments..." -f DarkCyan
$envs = Get-Environments -Server $ServerName -ApiKey $ApiKey
$env = $envs |Where-Object Name -eq $EnvironmentName

if ($env -eq $null)
{
    Write-Host "Unable to find environment called '$EnvironmentName'. Check the name and try again" -f Red
    return
}

Write-host "Found $($env.Name). (Id: $($env.Id))" -f DarkCyan

Write-Host "Reading the Octopus Dashboard..." -f DarkCyan
$dashboard = Get-Dashboard -Server $ServerName -ApiKey $ApiKey 

Write-Host "Reading list of projects..." -f DarkCyan
$projects = Get-Projects -Server $ServerName -ApiKey $ApiKey

$currentAppsFrom = Populate-ApplicationsFromFile $ReleasePackageFile $projects
$currentAppsTo   = Populate-Applications $env 

[xml]$packageConfig = Get-Content $ReleasePackageFile

Write-Host "Generating release notes..." -f DarkCyan
$relNotes = Create-ReleaseNotes -fromApps $currentAppsFrom -toEnv $env -toApps $currentAppsTo -releaseDescription $packageConfig.ReleasePackage.Description

if ($ReleaseNotesFile -eq "")
{
    $ReleaseNotesFile = $ReleasePackageFile -replace ".xml",".txt" 
}

Write-Host "Storing Release Notes in file: $ReleaseNotesFile" -f DarkCyan
$relNotes | Out-File -FilePath $ReleaseNotesFile

Write-Host "All Done." -f DarkCyan
