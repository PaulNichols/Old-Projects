<# 
.SYNOPSIS 
    Will get a list of the variables for a given Environment in JSON format. This will be stored in a file to allow for easy editing and then possible importing via Set-VariablesForEnv.
    There is an optional switch that will determine if only variables scoped to just the Environment are included or all variables including shared ones.

.DESCRIPTION 
    The script will assist with the creation of a new Octopus Deploy Environment. Users can select an environment that the new environment will be based on and get a list of variables
    that are defined for that environment. This list will contain all the variables that are scoped to just that environment. If you include an optional switch called IncludeGroupedVariables
    it will include additional variables that are scoped to many environment (where the base environment is one of them).
    It is the expectation the user will then modify the JSON file either manually or via another script to set what the values should be for the new environment.
    The the user will run the Set-VariablesForEnv.ps1 script to create the new variables. This will save many man hours of manually editing pages and pages of Octopus Deploy
    variables.    

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Get-VariablesForEnv+script    

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance.
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER EnvironmentName 
   The name of the environment to extract the variables out from 
.PARAMETER OutputFilename 
   A required field that is the name of the file to save the variables in JSON format.
.PARAMETER IncludeGroupedVariables 
   An optional flag that if set, will also include variables scoped to multiple environments. Useful for setting up a set of variables that are not grouped with any existing environments.


.EXAMPLE 
    .\Get-VariablesForEnv.ps1 -EnvironmentName UAT-A -OutputFilename c:\data\uat-a-vars.json
    This will store all the variables from UAT-A that are scoped to just UAT-A in the file c:\data\uat-a-vars.json

    .\Get-VariablesForEnv.ps1 -EnvironmentName UAT-B -OutputFilename c:\data\uat-b-vars.json -IncludeGroupedVariables
    This will store all the variables from UAT-A that are scoped to UAT-A (either singular or in a group) in the file c:\data\uat-b-vars.json

#> 
param (
    [string]
    $ServerName = "octopusdeploy.racqgroup.local",
    
    [Parameter(Mandatory=$true)]
    [string]
    $EnvironmentName,

    [Parameter(Mandatory=$true)]
    [string]
    $OutputFilename,

    [string]
    $ApiKey = $OctopusApiKey,

    [switch]
    $IncludeGroupedVariables

    )

# MAIN
# ================================================================================================================================================
# load up common functions
. .\CommonFunctions.ps1
. .\HelperFunctions.ps1
$ProgressPreference="SilentlyContinue" 

Write-Host "Reading Octopus Environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
$environment = $environments |Where-Object Name -eq $EnvironmentName

if ($environment -eq $null)
{
    Write-Host "Unable to find environment called '$EnvironmentName'. Check the name and try again" -f Red
    return
}

Write-host "Found Environment $($environment.Name). (ID: $($environment.Id))" -f DarkCyan

Write-Host "Reading all Octopus Projects..." -f DarkCyan
$projects = Get-Projects -Server $ServerName -ApiKey $ApiKey 

$matchingProjectVariables = @()

Write-Host "Reading the variable sets for each project looking for $(if ($IncludeGroupedVariables){"ALL "})variables scoped to $($environment.Name)." -f DarkCyan -NoNewline
foreach ($app in $projects)
{
    write-host "." -NoNewline -f DarkCyan
    # get the variable 
    $appVars = Get-VariableSet $ServerName $ApiKey $app.VariableSetId
    if ($IncludeGroupedVariables)
    {
        $valueMatches = $appVars.Variables | Where-Object {$_.Scope.Environment -contains $($environment.Id)}
    }
    else
    {
        $valueMatches = $appVars.Variables | Where-Object {$_.Scope.Environment -contains $($environment.Id) -and $_.Scope.Environment.Count -eq 1}
    }

    $variables = @()
    foreach ($match in $valueMatches)
    {
        $obj = [PSCustomObject] @{
                Key = $match.Name
                Value = $match.Value
                Sensitive = $match.IsSensitive
                }
        $variables += $obj
    }
    $project = [PSCustomObject] @{
            ProjectName = $app.Name
            Variables = $variables
        }
    $matchingProjectVariables += $project
}

Write-Host
$matchingLibraryVariables = @()
Write-Host "Reading the library variable sets looking for $(if ($IncludeGroupedVariables){"ALL "})variables scoped to $($environment.Name)." -f DarkCyan -NoNewline

$libVarSets = Get-LibraryVariableSets $ServerName $ApiKey
foreach ($libVarSet in $libVarSets)
{
    write-host "." -NoNewline -f DarkCyan
    $libVar = Get-VariableSet $ServerName $ApiKey $libVarSet.VariableSetId
    if ($IncludeGroupedVariables)
    {
        $valueMatches = $libVar.Variables | Where-Object {$_.Scope.Environment -contains $($environment.Id)}
    }
    else
    {
        $valueMatches = $libVar.Variables | Where-Object {$_.Scope.Environment -contains $($environment.Id) -and $_.Scope.Environment.Count -eq 1}
    }

    $variables = @()
    foreach ($match in $valueMatches)
    {
        $obj = [PSCustomObject] @{
                Key = $match.Name
                Value = $match.Value
                Sensitive = $match.IsSensitive
                }
        $variables += $obj
    }
    $library = [PSCustomObject] @{
            LibrarySet = $libVarSet.Name
            Variables = $variables
        }
    $matchingLibraryVariables += $library
}

$matchingVariables = [PSCustomObject] @{
            FromEnvironment = $environment.Name
            VariableGroupEnvLike = $environment.Name
            Projects = $matchingProjectVariables
            LibrarySets = $matchingLibraryVariables
                    }
Write-Host
Write-Host "Outputing matching variables to file: $OutputFilename" -f DarkCyan
$matchingVariables | ConvertTo-Json -Depth 5| Out-File -FilePath $OutputFilename

Write-Host "All done." -f DarkCyan


