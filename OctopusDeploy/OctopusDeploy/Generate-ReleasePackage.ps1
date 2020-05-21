<# 
.SYNOPSIS 
    Will generate an XML file containing the list of applications installed in a given environment. Version numbers will be included.

.DESCRIPTION 
    Given an environment name and an optional release description, this script will generate an XML file containing 
    the list of applications deployed in that environment. Sorted by deployment date/time order
    This XML file can be used for input into the Deploy-ReleasePackage.ps1 command.

    This script heavily utilises the Octopus REST API to query for the list of packages deployed in an environment.

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Generate-ReleasePackage+script

.PARAMETER ServerName 
   The name of the Octopus Deploy server to call. Defaults to RACQ's current instance.
.PARAMETER EnvironmentName 
   The name of the environment to query. Eg. PR501-DEV, PRE-PROD (must match what is defined in Octopus Deploy)
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER ReleasePackageFilename 
   The filename where the Release Packages XML file will be written
.PARAMETER ReleaseDescription 
   A description for this release notes. Will default to an empty string if not provided. It is recommended to be provided.

.EXAMPLE 
    .\Generate-EnvironmentSnapshot.ps1 -EnvName PR501-DEV -ApiKey $octoAPIKey -Filename C:\work\RACQESB\Tools\Powershell\OctopusDeploy\testing.xml -ReleaseDescription "This is a test of the release notes" 
    This will generate a file in C:\work\RACQESB\Tools\Powershell\OctopusDeploy\testing.xml for all the applications and their versions in PR501-DEV environment
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [Parameter(Mandatory=$true)]
    [string]$EnvironmentName,

    [string]$ApiKey = $OctopusApiKey,

    [Parameter(Mandatory=$true)]
    [string]$ReleasePackageFilename,

    [string]$ReleaseDescription = ""
    )


# MAIN
# ================================================================================================================================================
# load up common functions
. .\CommonFunctions.ps1
. .\HelperFunctions.ps1

#Get Environment Info
Write-Host "Reading Octopus Environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
$env = $environments |Where-Object Name -eq $EnvironmentName

if ($env -eq $null)
{
    Write-Host "Unable to find environment called '$EnvironmentName'. Check the name and try again" -f Red
    return
}
Write-host "Found $($env.Name)." -f DarkCyan

Write-Host "Reading the Octopus Dashboard..." -f DarkCyan
$dashboard = Get-Dashboard -Server $ServerName -ApiKey $ApiKey 

Write-Host "Generating application detail list for $($env.Name)..." -f DarkCyan
$currentApps = Populate-Applications $env

#Using XMLDocument instead of ConvertTo-XML so we can control the XML structure more accurately.
[xml]$xmlDoc = New-Object System.Xml.XmlDocument

$xmlDec = $xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", $null)
$xmlDoc.InsertBefore( $xmlDec, $xmlDoc.DocumentElement) | Out-Null

$xmlroot = $xmlDoc.CreateElement("ReleasePackage")
$xmlDoc.AppendChild($xmlRoot) | Out-Null

$xmlDesc = $xmlDoc.CreateElement("Description")
$xmlDesc.InnerText = $ReleaseDescription
$xmlroot.AppendChild($xmlDesc) | Out-Null
$xmlEnv = $xmlDoc.CreateElement("SnapShotEnvironment")
$xmlEnv.InnerText = $env.Name
$xmlroot.AppendChild($xmlEnv) | Out-Null
$xmlDateTime = $xmlDoc.CreateElement("SnapShotDateTime")
$xmlDateTime.InnerText = Get-Date
$xmlroot.AppendChild($xmlDateTime) | Out-Null

$xmlApps = $xmlDoc.CreateElement("Applications")
foreach ($app in $currentApps)
{
    # Add a line for each application
    [System.Xml.XmlElement]$xmlApp = $xmlDoc.CreateElement("Application")
    $xmlApp.SetAttribute("Name", $app.ProjectName) 
    $xmlApp.SetAttribute("Version", $app.Version) 
    $xmlApps.AppendChild($xmlApp) | Out-Null
}
$xmlroot.AppendChild($xmlApps)| Out-Null

Write-Host "Writing XML to file $ReleasePackageFilename..." -f DarkCyan
$xmlDoc.Save($ReleasePackageFilename) 

Write-Host "All Done." -f DarkCyan
