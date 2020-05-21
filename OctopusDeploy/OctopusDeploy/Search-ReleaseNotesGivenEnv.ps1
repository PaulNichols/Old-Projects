<# 
.SYNOPSIS 
    Searches for releases deployed in a given Enviromment and lists out the Jiras that are mentioned in the Releases Notes.

.DESCRIPTION 
    If a user/tester wants information on the Jiras that have been actioned via release deployments in a given environment then that is
    what this script will provide. Will also cater for Jiras that are not found by displaying a list of issues found at the end of the list.

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance. 
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER EnvName
   Required parameter that defines the Environment Name to check. (much match what is defined in Octopus Deploy)

.EXAMPLE 
    .\Search-ReleaseNotesGivenEnv.ps1 -EnvName PR501-SIT
    This wil output the list of Jiras linked (via Release Notes) to releases currently deployed in PR501-SIT environment
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [string]$ApiKey  = $OctopusApiKey,
    
    [Parameter(Mandatory=$true)]
    [string]$EnvName
       ) 

# MAIN
# ---------------------------------------------------------------------------------------------------------------------------------------------------
# load up common modules
. .\CommonFunctions.ps1
. .\HelperFunctions.ps1
$ProgressPreference="SilentlyContinue" 

Write-Host "Reading the Octopus Environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
$env = $environments |Where-Object Name -eq $EnvName
Write-host "Found Environment $($env.Name). (ID: $($env.Id))" -f DarkCyan

Write-Host "Reading the Octopus Dashboard..." -f DarkCyan
$dashboard = Get-Dashboard -Server $ServerName -ApiKey $ApiKey 

$items = $dashboard.Items | Where-Object EnvironmentId -eq $env.Id

Write-host "Searching for jiras in the application releases in $($env.Name)" -f DarkCyan -NoNewline

$jiraIds = @()
$errors = @()
foreach ($item in $items)
{
    Write-Host "." -f DarkCyan -NoNewline
    try
    {
        $rel = Get-Release -Server $ServerName -ApiKey $ApiKey -ReleaseId $item.ReleaseId
        $jiras = [regex]::Matches($rel.ReleaseNotes, $JiraRegexExpression) | select value
        foreach ($jira in $Jiras)
        {
            $jiraId = $jira.Value -replace '[\[\]]',''
            if (($jiraIds | Where-Object Id -eq $jiraId) -eq $null)
            {
                try
                {
                    $jiraRecord = Get-JiraDetails $jiraId
                    $jiraTitle = $jiraRecord.fields.Summary
                    $jiraIds += New-Object PsObject -Property @{Id=$jiraId; Title=$jiraRecord.fields.Summary}        
                }
                catch 
                {
                    # Catch errors if devs have made typos with the Jira Ids (this can happen)
                    Write-Host "X" -f Red -NoNewline
                    $errors += "Issue reading Jira: $jiraId. Details: $($_.Exception.Message)"
                }
            }
        }
    }
    catch
    {
        #catch errors with the release Info (this is less likely to happen)
        Write-Host "X" -f Red -NoNewline
        $errors += "Issue reading Release: $($item.ReleaseId). Details: $($_.Exception.Message)"
    }
}
Write-Host

# display the output
Write-Host "`n`nJiras that are linked to currently deployed applications in $($env.Name):`n" -ForegroundColor Cyan
$jiraIds = $jiraIds |Sort-Object -Property Id
foreach ($jira in $jiraIds)
{
    Write-Host " - $(($jira.Id).PadRight(9))" -f Cyan -NoNewline; Write-Host "  $($jira.Title)" -f DarkCyan
}

# if any errors were detected, then display them underneath.
if ($errors.Length -gt 0)
{
    Write-Host "`n`nSome errors were detected:" -f Yellow
    foreach ($error in $errors)
    {
        Write-Host $error -f Yellow
    }
}
Write-Host "`nAll done." -f DarkCyan

