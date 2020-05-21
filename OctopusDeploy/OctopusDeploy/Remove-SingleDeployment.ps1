<# 
.SYNOPSIS 
    Removes a single  Octopus Deploy deployment record for a given Deployment Id

.DESCRIPTION 
    The list of deployed applications in Octopus for each environment is important when doing comparisons and deployments. If a project has been accidently deployed to
    an environment this script provides the ability to remove it. 
    As with all Delete scripts, the user will be prompted to confirm the delete before it is actually done.

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Remove-SingleDeployment+script

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance. 
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER DeploymentId
   Required parameter that defines the Deployment record Id to remove 

.EXAMPLE 
    .\Remove-SingleDeployment.ps1 -DeploymentId deployments-10409
    This will remove a single deployment record with the ID of deployments-10409
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [string]$ApiKey  = $OctopusApiKey,
    
    [Parameter(Mandatory=$true)]
    [string]$DeploymentId
    
    ) 

# MAIN
# ================================================================================================================================================
# load up common functions
. .\CommonFunctions.ps1

Write-Host "Loading Deployment details..." -f DarkCyan
$deployment = Get-Deployment -Server $ServerName -ApiKey $ApiKey -DeploymentId $DeploymentId

if ($deployment -eq $null)
{
    Write-Host "Unable to find a Deployment with Id '$DeploymentId'. Check the supplied parameter and try again." -f Red
    return
}

$project = Get-Project -Server $ServerName -ApiKey $ApiKey -ProjectId $deployment.ProjectId
$release = Get-Release -Server $ServerName -ApiKey $ApiKey -ReleaseId $deployment.ReleaseId

$confirmation = Read-Host "This will delete deployment record of $($project.Name) - Version $($release.Version) that was $($deployment.Name). Are you sure? [Y or N] "

if ($confirmation -eq "y") # case insensitive so will catch both y and Y
{
    Write-Host "Removing Deployment: $DeploymentId" -f Cyan
    $task = Remove-Deployment -Server $ServerName -ApiKey $ApiKey -DeploymentId $DeploymentId
    Write-Host "Task Id: $($task.Id) created." -f DarkCyan
}
else
{
    Write-Host "Aborted." -f Cyan
}
Write-Host "All done." -f DarkCyan