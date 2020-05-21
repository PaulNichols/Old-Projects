
<# 
.SYNOPSIS 
    Will search the Octopus variables for all projects for a given environment and the global library variable sets for a match on a provided name or value.
    Will display on the screen the projects where the matches are found.

.DESCRIPTION 
    Sometimes some of the Octopus variables are changed. This will require the use to go through possibly many variable sets for applications and library sets to search for the value to update.
    To try and remove this tedious process this script will do the searching for the user and just provide the list of places to go to perform the updates.
    Users also can just specify the EnvironmentScope parameter to get all keys and values that are linked to the provided environment.

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Find-VariableValue+script
    
.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance.
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER SearchText 
   Optional text string to search for. This can be a partial text as it is compared in a like clause. If not provided then all values will be returned.
.PARAMETER EnvironmentScope 
   Optional environment name to filter scope on. This is required to be a correct Environment Name defined in Octopus. If not provided no Scope filtering will occur.
.PARAMETER IncludeGlobalScope 
   Flag if set will also include scopes that have no environment filter. This flag is only checked if the EnvironmentScope is defined.


.EXAMPLE 
    .\Find-VariableValue.ps1 -SearchText vaws00mrm3030 -EnvironmentScope PR501-SIT
    This will search all of the variable sets for all applications and the global libaray variable sets for any name or value fields that have 'vaws00mrm3030' defined that are for the PR501-SIT environment.
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [string]$ApiKey = $OctopusApiKey,

    [string]$SearchText = "",

    [string]$EnvironmentScope = "",

    [switch]$IncludeGlobalScope

    )

# MAIN
# ================================================================================================================================================
# load up common functions
. .\CommonFunctions.ps1
. .\HelperFunctions.ps1
$ProgressPreference="SilentlyContinue" 

Write-Host "Reading all Octopus Projects..." -f DarkCyan
$projects = Get-Projects -Server $ServerName -ApiKey $ApiKey 

Write-Host "Reading the variable sets for each project looking for a match on " -f DarkCyan -NoNewline

$WhereArray = @()

if ($SearchText -ne "")
{
    Write-Host "Name or Value with '$SearchText'" -f DarkCyan -NoNewline
    $WhereArray += '$_.Name -like "*$SearchText*" -or $_.Value -like "*$SearchText*"'
}
if ($EnvironmentScope -ne "")
{
    Write-Host " Environment Scope of '$EnvironmentScope'" -f DarkCyan -NoNewline
    $environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
    $env = $environments | Where-Object Name -eq $EnvironmentScope

    if ($env -eq $null)
    {
        Write-Host "`nUnable to find environment called '$EnvironmentScope'. Check the name and try again" -f Red
        return
    }
    if ($IncludeGlobalScope)
    {
        Write-Host " (Including gloabal scoped variables)" -f DarkCyan -NoNewline
        # if set, then include the scopes that have no environment defined. (means they would be global)
        $WhereArray += '$_.Scope.Environment -contains "'+$($env.Id)+'" -or $_.Scope.Environment.Length -eq 0'
    }
    else
    {
       $WhereArray += '$_.Scope.Environment -contains "'+$($env.Id)+'"'
    }
}
Write-Host "..." -f DarkCyan


$WhereString = $WhereArray -join " -and "
$WhereBlock = [scriptblock]::Create($WhereString)

foreach ($app in $projects)
{
    # get the variable Set
    $appVars = Get-VariableSet $ServerName $ApiKey $app.VariableSetId
    $valueMatches = $appVars.Variables | Where-Object $WhereBlock
    if ($valueMatches -ne $null)
    {
        Write-Host "`nProject: $($app.Name)" -f Yellow
        $padLength = ($valueMatches.Name | Measure-Object -Maximum -Property Length).Maximum
        foreach ($match in $valueMatches)
        {
            Write-Host "Variable: " -NoNewline; Write-Host "$($match.Name)".PadRight($padLength + 2) -NoNewline -f Cyan; Write-Host "Value: " -NoNewline; Write-Host "$($match.Value)" -f Cyan
        }
    }
}
Write-Host "`nChecking the library variable sets...." -f DarkCyan
$libVarSets = Get-LibraryVariableSets $ServerName $ApiKey

foreach ($libVarSet in $libVarSets.Items)
{
    $libVar = Get-VariableSet $ServerName $ApiKey $libVarSet.VariableSetId
    $valueMatches = $libVar.Variables | Where-Object $WhereBlock
    if ($valueMatches -ne $null)
    {
        Write-Host "`Library Set: $($libVarSet.Name)" -f Yellow
        $padLength = ($valueMatches.Name | Measure-Object -Maximum -Property Length).Maximum
        foreach ($match in $valueMatches)
        {
            Write-Host "Variable: " -NoNewline; Write-Host "$($match.Name)".PadRight($padLength + 2) -NoNewline -f Cyan; Write-Host "Value: " -NoNewline; Write-Host "$($match.Value)" -f Cyan
        }
    }
}

Write-Host "`nAll done!" -f DarkCyan
