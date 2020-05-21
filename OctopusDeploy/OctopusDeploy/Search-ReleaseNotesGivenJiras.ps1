<# 
.SYNOPSIS 
    Searches for releases and environments deployed based on a list of one or more Jiras.

.DESCRIPTION 
    If a user wants to know what releases are linked to a given Jira and if they are deployed to any environments, run the script to provide this information.
    The JiraIds parameter can contain one or more Jira Ids in a comma separated list

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance. 
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER JiraIds
   Required parameter that defines one or more Jira Ids in a comma separated list. (ensure to enclose in quotes) e.g. "GF-1234,ICC-8765"

.EXAMPLE 
    .\Search-ReleaseNotesGivenJira.ps1 -JiraIds "GF-1234,ICC-8765"
    This will search for and show the list of Releases and Environments for Jiras GF-1234 and ICC-8765
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    [string]$ApiKey  = $OctopusApiKey,
    [Parameter(Mandatory=$true)]
    [string]$JiraIds
       ) 

# ---------------------------------------------------------------------------------------------------------------------------------------------------
# MAIN
# ---------------------------------------------------------------------------------------------------------------------------------------------------
# load up common modules
. .\CommonFunctions.ps1
. .\HelperFunctions.ps1

Write-Host "Reading Octopus Releases... (this will take a little while)" -f DarkCyan
#unfortunately there is no way to filter release notes via the API. So we need to read in ALL of them first, and then filter. This will take 2-3 mins. :(
$releases = Get-AllReleases $ServerName -ApiKey $ApiKey

$ProgressPreference="SilentlyContinue" 
Write-Host "Reading the Octopus Environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
Write-Host "Reading the Octopus Dashboard..." -f DarkCyan
$dashboard = Get-Dashboard -Server $ServerName -ApiKey $ApiKey 

$JiraIdArray = $JiraIds -split ","

$deployedEnvs = @()
foreach ($jiraId in $JiraIdArray)
{
    $deployedEnvs = @()
    $jiraRecord = Get-JiraDetails $jiraId
    if ($jiraRecord -ne $null)
    {
        $jiraTitle = $jiraRecord.fields.Summary
        Write-Host "`n`nLooking for releases with Jira : " -f DarkCyan -NoNewline; Write-Host "$jiraId - ($($jiraRecord.fields.Summary))" -f Cyan    
        $matchingRels = $releases | Where-Object ReleaseNotes -like "*$jiraId*"
        foreach ($foundRelease in $matchingRels)
        {
            $proj = Get-Project -Server $ServerName -ApiKey $ApiKey -ProjectId $foundRelease.ProjectId
            Write-Host "- $($proj.Name) $($foundRelease.Version)" -f Cyan
            $deployedItems = $dashboard.Items | Where-Object ReleaseId -eq $foundRelease.Id

            foreach ($item in $deployedItems)
            {
                $env = $environments | Where-Object Id -eq $item.EnvironmentId
                if ($deployedEnvs -notcontains $env.Name)
                {
                    $deployedEnvs += $env.Name
                }
            }
        }
        Write-Host "`nCurrently deployed in : " -f DarkCyan
        foreach ($env in $deployedEnvs)
        {        
            Write-Host "- $env" -f Cyan
        }
    }
}
Write-Host "`nAll done." -f DarkCyan
