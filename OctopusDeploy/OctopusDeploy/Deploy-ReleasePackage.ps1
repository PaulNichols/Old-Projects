<# 
.SYNOPSIS 
    Automates the task of deploying (promoting) multiple applications from a snapshot XML config file to a target environment. Release Notes will also be created. 

.DESCRIPTION 
    This script will deploy any applications that require updating from a Release Package file to a target Environment. 
    The Release Package file can be generated via Generate-ReleasePackage script. (the user can modify afterwards as required)
    Validation is performed on the Release Package file (to confirm the installation order) if the TFSRootDirectory parameter is set. Optionally the 
    validation can be performed separately via the Validate-ReleasePackage script.
    Release Notes will be generated and stored in the file provided by the ReleaseNotesFile parameter.
    This script will work out what to deploy and sequentially trigger new Tasks via the Octopus Deploy REST API to install all the applications as required.
    
    If other applications not in the Release Package list are undeployed as part of this deployment they can be redeployed via the Check-Deployment script 
    with the -RedeployMissingApplications switch parameter.
    
    This script heavily utilises the Octopus and the Jira REST APIs to query for the list of packages deployed in an environment.

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Deploy-ReleasePackage+script

.PARAMETER ServerName 
   The name of the Octopus Deploy Server. If not provided it will default to the current RACQ instance.
.PARAMETER EnvironmentName
   The name of the environment to deploy TO. Eg. PR501-DEV, PRE-PROD (must match what is defined in Octopus Deploy)
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER ReleasePackageFile 
   The filename where the Release Packages XML file is read from
.PARAMETER ReleaseNotesFile 
   The filename for this release notes. Will default to ReleasePackage File with .txt instead of .xml extention if not provided
.PARAMETER TFSRootDirectory 
   The root folder for the RACQ ESB source code. If set this is used to validate the application order in the Release Package file. This is expected to be 
   utilised when developers do deployments. It is not expected to be utilised when Support teams do deployments.
.PARAMETER UpdateReleaseVariables
    If set, any releases that are deployed will have the associated Octopus variables refreshed before being re-deployed. 
.PARAMETER ForceInstall
    If set, this will trigger an install of the application even if it is already deployed. This is useful for if you want to clone to a new environment and want to ensure everything is installed even if OD already says it is.

.EXAMPLE 
    .\Deploy-ReleasePackage.ps1 -EnvironmentName PR501-SIT -ReleasePackageFile C:\temp\releasePackage.xml -ReleaseNotesFile C:\temp\ReleaseNotes.txt -TFSRootDirectory C:\work\RACQESB -UpdateReleaseVariables
    This will deploy all applications from PR501-SD into PR501-SIT that are not already there. It will utilise the C:\temp\releasePackage.xml file for the list of applications to deploy and store the
    generated Release Notes into C:\temp\ReleaseNotes.txt
#> 

param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [Parameter(Mandatory=$true)]
    [string]$EnvironmentName,

    [string]$ApiKey = $OctopusApiKey,

    [Parameter(Mandatory=$true)]
    [string]$ReleasePackageFile,

    [string]$ReleaseNotesFile = "",

    [string]$TFSRootDirectory = "",

    [switch]$UpdateReleaseVariables,

    [switch]$ForceInstall

    )

# --------------------------------------------------------------------------------------------------------------------------------------------------------

function CreateReleaseAndDeploy ($appName, $releaseId)
{
    if ($UpdateReleaseVariables)
    {
        Write-Host "Updating the Release variables for $appName..." -f DarkCyan
        Update-ReleaseVariables -Server $ServerName -ApiKey $ApiKey -ReleaseId $releaseId   
    }
    $deployment = Create-Deployment -Server $ServerName -ApiKey $ApiKey -EnvironmentId $env.Id -ProjectId $fromApp.ProjectId -ReleaseId $releaseId
    $task = Get-Task -Server $ServerName -ApiKey $ApiKey -TaskId $deployment.TaskId

    if ($task -eq $null -or $task.Id -eq $null -or $task.Id -eq "")
    {
        Write-Host "Unable to create a task to deploy $appName into $($env.Name). Check the Octopus Deploy UI." -f Red
        $action = Read-Host -Prompt "Do you want to continue with the rest of the deployments? (Y or N): "

        if ($action -eq "N")
        {
            return $false
        }
    }                    
    Write-Host "Created Task Id: $($task.Id) for redeploying $appName into $($env.Name). Waiting for task to complete." -f Cyan
                
    while (!$task.IsCompleted)
    {
        Start-Sleep -Seconds 5
        Write-Host "." -f Gray -NoNewline
        # refresh the task info. This might time out occasionally. If it does put in a red X instead of a grey .
        try
        {
            $task = Get-Task -Server $ServerName -ApiKey $ApiKey -TaskId $deployment.TaskId
        }
        catch [Exception]
        {
            Write-Host "X" -f Red -NoNewline
        }
    }
    Write-Host 
    if ($task.FinishedSuccessfully)
    {
        Write-Host "Deployment of $appName into $($env.Name) was successful. Duration: $($task.Duration)`n" -f Green
        return $true
    }
    else
    {
        Write-Host "Deployment of $appName into $($env.Name) was NOT successful. Current state: $($task.State)" -f Red
        Write-Host "Stopping further deployments until manual check is complete.`n" -f Red
        return $false
    }
}

# --------------------------------------------------------------------------------------------------------------------------------------------------------
#   MAIN 
# --------------------------------------------------------------------------------------------------------------------------------------------------------

# load up common functions
. .\CommonFunctions.ps1
. .\HelperFunctions.ps1

if ((Test-Path -Path $ReleasePackageFile -PathType Leaf) -eq $false)
{
    Write-Host "Release Package file $ReleasePackageFile is not found. Check the parameter and try again." -f Red
    return
}

if ((.\Validate-ReleasePackage.ps1 -ReleasePackageFile $ReleasePackageFile -RootLocalTFSPath $TFSRootDirectory) -eq $false)
{
    return
}

#Load the Release Package XML
[xml]$releaseConfig = Get-Content $ReleasePackageFile

# Get environments for From and To
Write-Host "Reading Octopus Environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
$env = $environments | Where-Object Name -eq $EnvironmentName

if ($env -eq $null)
{
    Write-Host "Unable to find environment called '$EnvironmentName'. Check the name and try again" -f Red
    return
}

Write-host "Found environment $($env.Name)." -f DarkCyan

Write-Host "Reading the Octopus Dashboard..." -f DarkCyan
$dashboard = Get-Dashboard -Server $ServerName -ApiKey $ApiKey 

#Get List of differences between environments
$projects = Get-Projects -Server $ServerName -ApiKey $ApiKey
$currentAppsFrom = Populate-ApplicationsFromFile $ReleasePackageFile $projects
$currentAppsTo   = Populate-Applications $env 

#Generate Release Notes
Write-Host "Generating Release Notes for Release '$($releaseConfig.ReleasePackage.Description)'" -f DarkCyan
$relNotes = Create-ReleaseNotes -fromApps $currentAppsFrom -toEnv $env -toApps $currentAppsTo -releaseDescription $releaseConfig.ReleasePackage.Description

if ($ReleaseNotesFile -eq "")
{
    $ReleaseNotesFile = $ReleasePackageFile -replace ".xml",".txt" 
}
Write-Host "Store release notes in file $ReleaseNotesFile..." -f DarkCyan
$relNotes | Out-File $ReleaseNotesFile

$continueProcessing = $true
#go through the list in the Release Packages

Write-Host "`Confirmation required from the user before proceeding..." -f Yellow
Write-Host "`nApplications that are about to be deployed to $($env.Name):" -f Yellow
foreach ($fromApp in $currentAppsFrom)
{
    $toApp = $currentAppsTo | Where-Object ProjectName -eq $fromApp.ProjectName
        
    # only deploy if required or Force Install is set as true
    if ((AreReleaseNoteRequired $fromApp $toApp) -or $ForceInstall)
    {
        # get the info for that application in the Release Package config
        $releaseApp = $releaseConfig.ReleasePackage.Applications.Application | Where-Object Name -eq $fromApp.ProjectName
        
        Write-Host "- $($fromApp.ProjectName) $($releaseApp.Version)" -f Yellow
    }
}
Write-Host

if ((Read-Host -Prompt "Are you sure you wish to continue? (y or n)") -eq "y")
{
    foreach ($fromApp in $currentAppsFrom)
    {
        if ($continueProcessing)
        {
            $toApp = $currentAppsTo | Where-Object ProjectName -eq $fromApp.ProjectName
        
            # only deploy if required or Force Install is set as true
            if ((AreReleaseNoteRequired $fromApp $toApp) -or $ForceInstall)
            {
                # get the info for that application in the Release Package config
                $releaseApp = $releaseConfig.ReleasePackage.Applications.Application | Where-Object Name -eq $fromApp.ProjectName
        
                $releaseVersion = Get-ProjectReleaseVersion $ServerName -ApiKey $ApiKey -ProjectId $fromApp.ProjectId -Version $releaseApp.Version

                if ((CreateReleaseAndDeploy $fromApp.ProjectName $releaseVersion.Id) -eq $false)
                {
                    # if return value is $false than an error occured. Will stop processing for manual intervention.
                    $continueProcessing = $false
                }
            }
        }
    }
}
else
{
    Write-Host "Aborting install.`n" -f DarkCyan
}
Write-Host "All Done." -f DarkCyan
