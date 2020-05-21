<# 
.SYNOPSIS 
    Will compare the list of applications deployed in Octopus Deploy for two separate environments and display to the user in a list format.

.DESCRIPTION 
    Sometimes the user will need to do a compare between versions of applications installed between two environments. Since there are so many defined now it's
    almost impossible to do this via the dashboard. This script will display a list of all the applications deployed between two environments and highlight where there
    are differences. This will compare what Octopus says is deployed and not what is actually on the Biztalk Server itself. If you want to compare between Octopus and Biztalk
    use the Check-Deployments.ps1 script.

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Compare-Environments+script    

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance.
.PARAMETER EnvNameLeft 
   The name of the environment to compare on the left side. Eg. PR501-SIT, PRE-PROD (must match what is defined in Octopus Deploy)
.PARAMETER EnvNameRight 
   The name of the environment to compare on on the right side. E.g. PR501-DEV, PRE-PROD (must match what is defined in Octopus Deploy)
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER DifferencesOnly 
   An optional flag that will filter out the applications that are the same.

.EXAMPLE 
    .\Compare-Environments.ps1 -EnvNameLeft PR501-DEV -EnvNameRight PR501-SD -ApiKey $octoAPIKey
    This will display the differences between the application versions deployed in two environments according to Octopus Deploy
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [Parameter(Mandatory=$true)]
    [string]$EnvNameLeft,

    [Parameter(Mandatory=$true)]
    [string]$EnvNameRight,
    
    [string]$ApiKey = $OctopusApiKey,

    [switch]$DifferencesOnly

    )

# MAIN
# ================================================================================================================================================
# load up common functions
. .\CommonFunctions.ps1
. .\HelperFunctions.ps1


Write-Host "Reading Octopus Environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
$environmentLeft = $environments |Where-Object Name -eq $EnvNameLeft
$environmentRight = $environments |Where-Object Name -eq $EnvNameRight

if ($environmentLeft -eq $null)
{
    Write-Host "Unable to find environment called '$EnvNameLeft'. Check the name and try again" -f Red
    return
}
if ($environmentRight -eq $null)
{
    Write-Host "Unable to find environment called '$EnvNameRight'. Check the name and try again" -f Red
    return
}

Write-host "Found Environment $($environmentLeft.Name). (ID: $($environmentLeft.Id))" -f DarkCyan
Write-host "Found Environment $($environmentRight.Name). (ID: $($environmentRight.Id))" -f DarkCyan

Write-Host "Reading the Octopus Dashboard..." -f DarkCyan
$dashboard = Get-Dashboard -Server $ServerName -ApiKey $ApiKey 

$currentAppsLeft = Populate-Applications $environmentLeft 
$currentAppsRight = Populate-Applications $environmentRight 

Write-Host "`nEnvironment compare run at $(get-date -Format "F")" -f Yellow

# Build master list of applications (union of apps installed in both environments)
$allApplications = ($currentAppsLeft | Select-Object ProjectName) + ($currentAppsRight | Select-Object ProjectName) | Select-Object ProjectName -Unique

Write-Host "`n$("Application Name".PadRight(80))$($EnvNameLeft.PadRight(37))$EnvNameRight" -f DarkYellow

foreach ($app in $allApplications)
{
    $appLeft = $currentAppsLeft | Where-Object ProjectName -eq $app.ProjectName
    $appRight = $currentAppsRight | Where-Object ProjectName -eq $app.ProjectName

    $result = LaterOrOlderDeployment $appLeft $appRight

   
    if ($result -eq "newerVersion")
    {
        $foreColour = @{'ForegroundColor'='Yellow' }
    }
    elseif ($result -eq "olderVersion")
    {
        $foreColour = @{'ForegroundColor'='Red' }
    }
    else
    {
        if ($DifferencesOnly)
        {
            continue
        }
        $foreColour = @{'ForegroundColor'='Green' }
    }

   Write-Host "Name: " -NoNewline; Write-Host "$($app.ProjectName)".PadRight(74) -NoNewline -ForegroundColor Cyan

   if ($appLeft -ne $null)
   {
        Write-Host "Version: $($appLeft.Version)".PadRight(22) -NoNewline @foreColour
        Write-Host " [$($appLeft.Status)]".PadRight(15) -NoNewline @foreColour
   }
   else
   {
        Write-Host "Not installed".PadRight(25) -NoNewline @foreColour
   }

   if ($appRight -ne $null)
   {
        Write-Host "Version: $($appRight.Version)".PadRight(22) -NoNewline @foreColour
        Write-Host " [$($appRight.Status)]" @foreColour
   }
   else
   {
        Write-Host "Not installed" @foreColour
   }
}

Write-Host "`nAll Done." -f DarkCyan
