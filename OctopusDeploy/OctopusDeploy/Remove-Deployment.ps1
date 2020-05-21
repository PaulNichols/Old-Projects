<# 
.SYNOPSIS 
    Removes all Octopus Deploy deployment records for a given Project Name and Environment Name

.DESCRIPTION 
    The list of deployed applications in Octopus for each environment is important when doing comparisons and deployments. If a project has been accidently deployed to
    an environment this script provides the ability to remove it. 
    As with all Delete scripts, the user will be prompted to confirm the delete before it is actually done.

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Remove-Deployment+script

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance. 
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER EnvName
   Required parameter that defines the Environment name. 
.PARAMETER ProjectName
   Required parameter that defines the Project Name

.EXAMPLE 
    .\Remove-Deployment.ps1 -EnvName PR501-SD -Project RACQESB.Membership.MembershipManagement
    This will search for all deployments of RACQESB.Membership.MembershipManagement into the PR501-SD environment.
    A prompt will be displayed to the user to confirm the delete of x number of deployments before proceeding.
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [string]$ApiKey  = $OctopusApiKey,
    
    [Parameter(Mandatory=$true)]
    [string]$EnvName,
    
    [Parameter(Mandatory=$true)]    
    [string]$ProjectName
    ) 

# MAIN
# ================================================================================================================================================
# load up common functions
. .\CommonFunctions.ps1

Write-host "Reading Octopus projects..." -f DarkCyan
$projects = Get-Projects -Server $ServerName -ApiKey $ApiKey
$project = $projects |Where-Object Name -eq $ProjectName

if ($project -eq $null)
{
    Write-Host "Unable to find a Project with name '$ProjectName'. Check the name and try again." -f Red
    return
}
Write-host "Found Project $ProjectName. (Id: $($project.Id))" -f DarkCyan

Write-host "Reading Octopus environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
$environment = $environments |Where-Object Name -eq $EnvName

if ($environment -eq $null)
{
    Write-Host "Unable to find environment called '$EnvName'. Check the name and try again" -f Red
    return
}
Write-host "Found Environment $EnvName. (Id: $($environment.Id))" -f DarkCyan

Write-host "Reading deployments of $ProjectName into $EnvName..." -f DarkCyan
$deployments = Get-Deployments -Server $ServerName -ApiKey $ApiKey -EnvironmentId $environment.Id -ProjectId $project.Id

if ($deployments.Items.Count -eq 0)
{
    Write-Host "No deployments of $ProjectName into $EnvName have been found." -f Yellow
    return
}

# Get confirmation of the deletes
$confirmation = Read-Host "This will delete $($deployments.Items.Count) deployment records of $ProjectName into $EnvName. Are you sure [Y or N] "

if ($confirmation -eq "y") # case insensitive so will catch both y and Y
{
    foreach ($deployment in $deployments.Items)
    {
        Write-Host "Removing Deployment: $($deployment.Id)" -f Cyan
        $task = Remove-Deployment -Server $ServerName -ApiKey $ApiKey -DeploymentId $deployment.Id
        Write-Host "Task Id: $($task.Id) created." -f DarkCyan
    }
}
else
{
    Write-Host "Aborted." -f Cyan
}
Write-Host "All done." -f DarkCyan