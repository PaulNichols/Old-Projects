<# 
.SYNOPSIS 
    Allows the user to compare the variables defined in Applicatoins and Library Set for two existing Environments.
    This is a helper script to ensure that the values have ben set correctly from the Get-VariablesForEnv and Set-VariablesForEnv scripts.

.DESCRIPTION 
    Does a visual compare between the variables set for all apllciationa dn Library set for two different environments.
    If the variables are the same they are displayed in Green. If they are different they are displayed in Yellow.
    Ideally for two separate environments, there should be more yellow than green.

.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Compare-VariablesForEnv+script 

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance.
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. If not provided it will try to use $OctopusApiKey
.PARAMETER EnvNameLeft 
   The name of the environment to compare on the left side. Eg. PR501-SIT, PRE-PROD (must match what is defined in Octopus Deploy)
.PARAMETER EnvNameRight 
   The name of the environment to compare on on the right side. E.g. PR501-DEV, PRE-PROD (must match what is defined in Octopus Deploy)

.EXAMPLE 
    .\Compare-VariablesForEnv.ps1 -EnvNameLeft UAT-A -EnvNameRight UAT-B
    This will display the differences between the application versions deployed in two environments according to Octopus Deploy
#> 
param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [Parameter(Mandatory=$true)]
    [string]$LeftEnvName,

    [Parameter(Mandatory=$true)]
    [string]$RightEnvName,

    [string]$ApiKey = $OctopusApiKey

    )


function Get-VariableUnions
{
    param( $varibles,  
            $leftEnvId,
           $rightEnvId)

    $leftValueMatches = $varibles.Variables | Where-Object {$_.Scope.Environment -contains $leftEnvId}
    $rightValueMatches = $varibles.Variables | Where-Object {$_.Scope.Environment -contains $rightEnvId}

    $varUnion = @()
    foreach ($match in $leftValueMatches)
    {
        $obj = [PSCustomObject] @{
                Key = $match.Name
                LeftValue = $match.Value
                LeftSensitive = $match.IsSensitive                
                RightValue = ""
                RightSensitive = ""
                  }
        $varUnion += $obj
    }
    foreach ($match in $rightValueMatches)
    {
        $existing = $varUnion | where Key -eq $match.Name | select -First 1
        if ($existing)
        {
            $existing.RightValue =  $match.Value
            $existing.RightSensitive = $match.IsSensitive
        }
        else
        {
            $obj = [PSCustomObject] @{
                Key = $match.Name
                LeftValue = ""
                LeftSensitive = ""
                RightValue = $match.Value
                RightSensitive = $match.IsSensitive
                   }
            $varUnion += $obj
        }
    }
    Write-Output $varUnion
}

# MAIN
# ================================================================================================================================================
# load up common functions
. .\CommonFunctions.ps1
$ProgressPreference="SilentlyContinue" 

Write-Host "Reading Octopus Environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
$leftEnv = $environments |Where-Object Name -eq $LeftEnvName

if ($leftEnv -eq $null)
{
    Write-Host "Unable to find left environment called '$LeftEnvName'. Check the name and try again" -f Red
    return
}

$rightEnv = $environments |Where-Object Name -eq $RightEnvName

if ($rightEnv -eq $null)
{
    Write-Host "Unable to find right environment called '$RightEnvName'. Check the name and try again" -f Red
    return
}

Write-host "Found Left  Environment $($leftEnv.Name). (ID: $($leftEnv.Id))" -f DarkCyan
Write-host "Found Right Environment $($rightEnv.Name). (ID: $($rightEnv.Id))" -f DarkCyan

Write-Host "Reading all Octopus Projects..." -f DarkCyan
$projects = Get-Projects -Server $ServerName -ApiKey $ApiKey 

$allProjectVariables = @()
$allLibrarySetVariables = @()

Write-Host "Reading the Application Variables sets" -f DarkCyan -NoNewline
foreach ($app in $projects)
{
    Write-Host "." -NoNewline -f DarkCyan
    # get the variable 
    $appVars = Get-VariableSet $ServerName $ApiKey $app.VariableSetId

    $variableUnion = Get-VariableUnions $appVars $leftEnv.Id $rightEnv.Id

    $project = [PSCustomObject] @{ ProjectName = $app.Name; VariableUnion = $variableUnion }
    $allProjectVariables += $project
}

Write-Host
Write-Host "Reading the library variable set" -f DarkCyan -NoNewline

$libVarSets = Get-LibraryVariableSets $ServerName $ApiKey
foreach ($libVarSet in $libVarSets)
{
    Write-Host "." -NoNewline -f DarkCyan
    $libVars = Get-VariableSet $ServerName $ApiKey $libVarSet.VariableSetId

    $variableUnion = Get-VariableUnions $libVars $leftEnv.Id $rightEnv.Id

    $libSet = [PSCustomObject] @{ LibrarySet = $libVarSet.Name; VariableUnion = $variableUnion }
    $allLibrarySetVariables += $libSet
}

Write-Host
Write-Host

# display the output
Write-Host "Applications:"
$keyPadLength = ($allProjectVariables.VariableUnion.Key | Measure-Object -Maximum -Property Length).Maximum
foreach ($proj in $allProjectVariables)
{
   Write-Host
   Write-Host "$($proj.ProjectName): "
   #$proj.VariableUnion | Format-Table -Property Key, LeftValue, RightValue -AutoSize
   $LeftPadLength = ($proj.VariableUnion.LeftValue | Measure-Object -Maximum -Property Length).Maximum
   $LeftPadLength = ($LeftPadLength,6 | Measure-Object -Maximum).Maximum
   
   if ($proj.VariableUnion.Count -gt 0)
   {
       Write-Host "$("Variable".PadRight($keyPadLength + 2))$(($leftEnv.Name).PadRight($LeftPadLength + 2))$($rightEnv.Name)" -f DarkCyan
       foreach ($variable in $proj.VariableUnion)
       {
            $leftVal = "<none>"
            $rightVal = "<none>"

            if (![string]::IsNullOrEmpty($variable.LeftValue)) { $leftVal = $variable.LeftValue}
            if (![string]::IsNullOrEmpty($variable.RightValue)) { $rightVal = $variable.RightValue}
            if ($leftVal -ne $rightVal)
            {
                Write-Host "$($variable.Key)".PadRight($keyPadLength + 2) -NoNewline -f Cyan; Write-Host $leftVal.PadRight($LeftPadLength + 2) -NoNewline -f Yellow; Write-Host $rightVal -f Yellow
            }
            else
            {
                Write-Host "$($variable.Key)".PadRight($keyPadLength + 2) -NoNewline -f Cyan; Write-Host $leftVal.PadRight($LeftPadLength + 2) -NoNewline -f Green; Write-Host $rightVal -f Green
            }
        }
    }
   
}

Write-Host "Library Sets:"
$keyPadLength = ($allLibrarySetVariables.VariableUnion.Key | Measure-Object -Maximum -Property Length).Maximum
foreach ($libSet in $allLibrarySetVariables)
{
    Write-Host
   Write-Host "$($libSet.LibrarySet) : "
   #$libSet.VariableUnion | Format-Table -Property Key, LeftValue, RightValue -AutoSize
   $LeftPadLength = ($libSet.VariableUnion.LeftValue | Measure-Object -Maximum -Property Length).Maximum
   $LeftPadLength = ($LeftPadLength,6 | Measure-Object -Maximum).Maximum
   if ($libSet.VariableUnion.Count -gt 0)
   {
       Write-Host "$("Variable".PadRight($keyPadLength + 2))$(($leftEnv.Name).PadRight($LeftPadLength + 2))$($rightEnv.Name)" -f DarkCyan
       foreach ($variable in $libSet.VariableUnion)
       {
            $leftVal = "<none>"
            $rightVal = "<none>"

            if (![string]::IsNullOrEmpty($variable.LeftValue)) { $leftVal = $variable.LeftValue}
            if (![string]::IsNullOrEmpty($variable.RightValue)) { $rightVal = $variable.RightValue}
            if ($leftVal -ne $rightVal)
            {
                Write-Host "$($variable.Key)".PadRight($keyPadLength + 2) -NoNewline -f Cyan; Write-Host $leftVal.PadRight($LeftPadLength + 2) -NoNewline -f Yellow; Write-Host $rightVal -f Yellow
            }
            else
            {
                Write-Host "$($variable.Key)".PadRight($keyPadLength + 2) -NoNewline -f Cyan; Write-Host $leftVal.PadRight($LeftPadLength + 2) -NoNewline -f Green; Write-Host $rightVal -f Green
            }
        }
    }
}

Write-Host "All done." -f DarkCyan


