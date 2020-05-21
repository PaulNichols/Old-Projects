<# 
.SYNOPSIS 
    This will set the Octopus Project and Library Set variables for a single existing or new environment as defined in the provided json file.
    If the environment does not exist the user will be prompted to create it.

.DESCRIPTION 
    The JSON file will be generated from the Get-VariablesForEnv.ps1 command. The user can edit this json file for the new environment settings. 
    Any variables that don't exist in either the Project or Library Set are created.
    Any variables that do exist in either the Project or Library Set are updated.
    The VariableGroupEnvLike environment listed in the json will be used to link the new environment to variables with a scope of multiple environments.

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Set-VariablesForEnv+script

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance.
.PARAMETER EnvironmentName 
   Required name of the environment to use for creating the variable. If it does not existing, it can be created.
.PARAMETER VariableFilename 
   Required name of the json file containing all the variable config info. This is usually generated from the Get-VariablesForEnv.ps1 command.
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER ForceCreateEnvironment
    Switch option for bypassing the create environment prompts. Will assume it will be created (useful for automation scripts with no user input)
.PARAMETER NewEnvironmentDescription
    Optional description for a new Environment. Only used if the ForceCreateEnvironment switch is active and the user is not prompted for details

.EXAMPLE 
    .\Set-VariablesForEnv.ps1 -EnvName ICC-CI -VariableFilename C:\temp\basevariables.json
    This will create in Octopus Deploy all the variables defined in C:\temp\basevariables.json scoped to environment called ICC-CI
    It will cater for both Projects and Library Sets

    .\Set-VariablesForEnv.ps1 -EnvName ICC-CI -VariableFilename C:\temp\basevariables.json
    This will create in Octopus Deploy all the variables defined in C:\temp\basevariables.json scoped to environment called ICC-CI
    It will cater for both Projects and Library Sets
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [Parameter(Mandatory=$true)]
    [string]$EnvironmentName,

    [Parameter(Mandatory=$true)]
    [string]$VariableFilename,

    [string]$ApiKey = $OctopusApiKey,

    [switch]$ForceCreateEnvironment,
    
    [string]$NewEnvironmentDescription = ""

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
    # assume the environment is to be created if it does not exist
    if ($ForceCreateEnvironment) 
    {
        $environment = Create-Environment -Server $ServerName -ApiKey $ApiKey -EnvironmentName $EnvironmentName -EnvironmentDescription $NewEnvironmentDescription
    }
    else # prompt user to confirm creation of environment and to provide a description
    {
        $prompt = Read-Host -Prompt "Unable to find environment called '$EnvironmentName'. Did you want to create it? (y or n)"
        if ($prompt -eq "y")
        {
            $envDescription = Read-Host -Prompt "Please enter a description for the $EnvironmentName environment"
            $environment = Create-Environment -Server $ServerName -ApiKey $ApiKey -EnvironmentName $EnvironmentName -EnvironmentDescription $envDescription
        }
        else
        {
            Write-Host "Unable to continue as '$EnvironmentName' environment doesn't exist." -f Red
            return
        }
    }
    if ($environment -ne $null)
    {
        Write-Host "Created new environment '$($environment.Id)' for $($environment.name)." -f Green
    }
    else
    {
        Write-Host "Unable to create a new environment in Octopus. Please check logs and permissions levels and try again." -f Red
        return
    }
}

Write-host "Found Environment $($environment.Name). (ID: $($environment.Id))" -f DarkCyan

Write-Host "Reading in json file $VariableFilename..." -f DarkCyan
$jsonData = (Get-Content -Raw -Path $VariableFilename) | ConvertFrom-Json

$envGroupLike = $environments |Where-Object Name -eq $jsonData.VariableGroupEnvLike

if ($envGroupLike -eq $null)
{
    Write-Host "Unable to find the Variable Group Environment called '$($jsonData.VariableGroupEnvLike)'. Check the name and try again" -f Red
    return
}

# first check that there are values for all the keys 
$errorMsgs = @()
#check the project first
foreach ($proj in $jsonData.Projects)
{
    $blankValues = $proj.Variables | Where-Object Value -eq $null

    foreach ($blankValue in $blankValues)
    {
        $errorMsgs += "Project $($proj.ProjectName) is missing a value for key '$($blankValue.Key)'" 
    }
}
#then check the library sets
foreach ($libSet in $jsonData.LibrarySets)
{
    $blankValues = $libSet.Variables | Where-Object Value -eq $null

    foreach ($blankValue in $blankValues)
    {
        $errorMsgs += "LibrarySet $($libSet.LibrarySet) is missing a value for key '$($blankValue.Key)'"                      
    }
}

#if any blank fields are found, the display and exit the script
if ($errorMsgs.Length -gt 0)
{
    Write-Host "Errors have been found in the file $VariableFilename. Please correct errors shown below and try again." -f Red
    Write-Host 
    Write-Host "ERRORS:" -f Yellow
    foreach ($errorMsg in $errorMsgs)
    {
        Write-Host " - $errorMsg" -f Yellow
    }
    return
}

Write-Host "Processing Project variables..." 
$projectData = (Get-Projects $ServerName $ApiKey) | Select-Object Id,Name

#now we are certain that all values are correct. Proceeding with update for the Project Variables
foreach ($proj in $jsonData.Projects)
{
    Write-Host
    Write-Host "Project: " -f DarkCyan -NoNewline; Write-host "$($proj.ProjectName)" -f Cyan
    $variablesChanged = $false
    $projId  = $projectData | Where Name -eq $proj.ProjectName | Select Id
    if ($projId)
    {
         $project = Get-Project $ServerName $ApiKey $projId.Id
         $projectVariableSet = Get-VariableSet $ServerName $ApiKey $project.VariableSetId
         
         # create/update the single variables if any exist.
         foreach ($variable in $proj.Variables)
         {
            # search to see if it exists first
            $existingVariable = $projectVariableSet.Variables | Where-Object { $_.Name -eq $variable.Key -and $_.Scope.Environment -contains $environment.Id -and $_.Scope.Environment.Count -eq 1 }
            # if it exists, then update it
            if ($existingVariable)
            {
                if ($existingVariable.Value -ne $variable.Value)
                {
                    Write-Host "- Updating" -f Cyan -NoNewline; Write-Host " $($variable.Key)" -f DarkCyan
                    $variablesChanged = $true
                    $existingVariable.IsSensitive = $variable.Sensitive
                    $existingVariable.Value = $variable.Value
                }
            }
            else # create a new one (take care to mimic the Variable structure)
            {
                Write-Host "- Creating" -f Cyan -NoNewline; Write-Host " $($variable.Key)" -f DarkCyan
                $variablesChanged = $true
                #Make the Variables Array writable
                $projectVariableSet.Variables = {$projectVariableSet.Variables}.Invoke()
                
                $newVariable = [PSCustomObject] @{
                        Id = [guid]::NewGuid().ToString()
                        Name = $variable.Key
                        Value = $variable.Value
                        Scope = [PSCustomObject] @{ Environment = @()}
                        IsSensitive = $variable.Sensitive
                        IsEditable = $true
                        Prompt = ""
                    }
                $newVariable.Scope.Environment += $environment.Id

                $projectVariableSet.Variables.Add($newVariable)
            }
         }
         # check to see if there are any variables with a group of environments attached that include the VariableGroupEnvLike
         $groupedEnvVariables = $projectVariableSet.Variables | Where-Object {$_.Scope.Environment -contains $envGroupLike.Id -and $_.Scope.Environment -notcontains $environment.Id -and $_.Scope.Environment.Count -gt 1 }
         if ($groupedEnvVariables)
         {
            foreach ($groupedVar in $groupedEnvVariables)
            {
                Write-Host " - Adding  " -f Cyan -NoNewline; Write-Host "$($environment.name) to variable $($groupedVar.Name)" -f DarkCyan
                $variablesChanged = $true
                $groupedVar.Scope.Environment += $environment.Id
            }
         }
         if ($variablesChanged)
         {
            $body = $projectVariableSet | ConvertTo-Json -Depth 5
            $result = Invoke-RestMethod -Method Put -Uri "$ServerName/api/variables/$($project.VariableSetId)" -Body $body -Headers @{"X-Octopus-ApiKey"=$ApiKey}
         }
         else
         {
            Write-Host "No changes required." -f DarkCyan
         }
    }
    else
    {
        Write-Host "Unable to find a Octopus Project Id for Project '$($proj.ProjectName)'. Skipping this and continuing... " -f Yellow
    }
}

Write-Host
Write-Host "Processing Library Set variables..."

# get the details of the Librry Variable sets from Octopus
$libVarSets = Get-LibraryVariableSets $ServerName $ApiKey

foreach ($libVarSetData in $jsonData.LibrarySets)
{
    Write-Host
    Write-Host "Library Set: " -f DarkCyan -NoNewline; Write-host "$($libVarSetData.LibrarySet)" -f Cyan
    # for each lib var defined in the json file
    $libVarSet = $libVarSets | Where Name -eq $libVarSetData.LibrarySet

    if ($libVarSet)
    {
        # get the variables from Octopus
        $libVar = Get-VariableSet $ServerName $ApiKey $libVarSet.VariableSetId
        $variablesChanged = $false
        
        foreach ($libVarData in $libvarSetData.Variables)
        {
            $existingVariable = $libVar.Variables | Where-Object {$_.Name -eq $libVarData.Key -and $_.Scope.Environment -contains $($environment.Id) -and $_.Scope.Environment.Count -eq 1}         

            # update existing
            if ($existingVariable)
            {
                if ($existingVariable.Value -ne $libVarData.Value)
                {
                    Write-Host "- Updating" -f Cyan -NoNewline; Write-Host " $($libVarData.Key)" -f DarkCyan
                    $variablesChanged = $true
                    $existingVariable.IsSensitive = $libVarData.Sensitive
                    $existingVariable.Value = $libVarData.Value
                }
            }
            else # create new lib var
            {
                #Make the Variables Array writable
                Write-Host "- Creating" -f Cyan -NoNewline; Write-Host " $($libVarData.Key)" -f DarkCyan

                $variablesChanged = $true
                $libVar.Variables = {$libVar.Variables}.Invoke()
                
                $newVariable = [PSCustomObject] @{
                        Id = [guid]::NewGuid().ToString()
                        Name = $libVarData.Key
                        Value = $libVarData.Value
                        Scope = [PSCustomObject] @{ Environment = @()}
                        IsSensitive = $libVarData.Sensitive
                        IsEditable = $true
                        Prompt = ""
                    }
                $newVariable.Scope.Environment += $environment.Id

                $libVar.Variables.Add($newVariable)
            }
        }
        # check to see if there are any variables with a group of environments attached that include the VariableGroupEnvLike
        $groupedLibVarEnvVariables = $libVar.Variables | Where-Object {$_.Scope.Environment -contains $envGroupLike.Id -and $_.Scope.Environment -notcontains $environment.Id -and $_.Scope.Environment.Count -gt 1 }
        if ($groupedLibVarEnvVariables)
        {
            foreach ($groupedLibVar in $groupedLibVarEnvVariables)
            {
                Write-Host " - Adding  " -f Cyan -NoNewline; Write-Host "$($environment.name) to variable $($groupedLibVar.Name)" -f DarkCyan
                $variablesChanged = $true
                $groupedLibVar.Scope.Environment += $environment.Id
            }
        }
        if ($variablesChanged)
        {
            $body = $libVar | ConvertTo-Json -Depth 5
            $result = Invoke-RestMethod -Method Put -Uri "$ServerName/api/variables/$($libVarSet.VariableSetId)" -Body $body -Headers @{"X-Octopus-ApiKey"=$ApiKey}
        }
        else
        {
            Write-Host "No changes required." -f DarkCyan
        }
    }
    else
    {
        Write-Host "Unable to find '$($libVarSetData.LibrarySet)' Library Variable Set in Octopus. Check '$VariableFilename' for correctness." -f Yellow
    }
}
Write-Host "All done." -f DarkCyan
