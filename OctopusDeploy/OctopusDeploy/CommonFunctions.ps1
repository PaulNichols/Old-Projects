<# 
.SYNOPSIS 
    Contains all the helper functions for calling Octopus Deploy REST API methods.

.DESCRIPTION 
    ALmost all other ps1 files in this folder will call functions in this folder.
#> 

function Get-Environments($Server, $ApiKey) 
{
    return Invoke-RestMethod -Uri "$Server/api/environments/all" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Projects($Server, $ApiKey) 
{
    return Invoke-RestMethod -Uri "$Server/api/projects/all" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Dashboard($Server, $ApiKey) 
{
    return Invoke-RestMethod -Uri "$Server/api/dashboard/dynamic" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Project($Server, $ApiKey, $ProjectId) 
{
    return Invoke-RestMethod -Uri "$Server/api/projects/$ProjectId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Projects($Server, $ApiKey) 
{
    return Invoke-RestMethod -Uri "$Server/api/projects/all" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-DeploymentProcess($Server, $ApiKey, $DeploymentProcessId)
{
    return Invoke-RestMethod -Uri "$Server/api/deploymentprocesses/$DeploymentProcessId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
} 

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Deployments($Server, $ApiKey, $ProjectId, $EnvironmentId)
{
    return Invoke-RestMethod -Uri "$Server/api/deployments?projects=$ProjectId&environments=$EnvironmentId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Deployment($Server, $ApiKey, $DeploymentId)
{
    return Invoke-RestMethod -Uri "$Server/api/deployments/$DeploymentId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-ProjectReleases($Server, $ApiKey, $ProjectId) 
{
    return Invoke-RestMethod -Uri "$Server/api/projects/$ProjectId/releases" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-ProjectReleaseVersion($Server, $ApiKey, $ProjectId, $Version) 
{
    return Invoke-RestMethod -Uri "$Server/api/projects/$ProjectId/releases/$Version" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Release($Server, $ApiKey, $ReleaseId) 
{
    return Invoke-RestMethod -Uri "$Server/api/releases/$ReleaseId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-AllReleases($Server, $ApiKey) 
{
    $releases = @()
    $readPage = "/api/releases"
    $count = 0
    $totalReleases = 0
    while ($readPage -ne $null)
    {
        $pageRead = Invoke-RestMethod -Uri "$Server$readPage" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
        $count += $pageRead.ItemsPerPage
        if ($readPage -eq "/api/releases") {$totalReleases = $pageRead.TotalResults}
        
        Write-Progress -Activity "Reading Releases" -Status "$count of $totalReleases" -PercentComplete (($count / $totalReleases) * 100)
        $releases += $pageRead.Items
        $readPage = $pageRead.Links.{Page.Next}
    }
    return $releases
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-AllMachines($Server, $ApiKey) 
{
    $machines = @()
    $readPage = "/api/machines"
    while ($readPage -ne $null)
    {
        $pageRead = Invoke-RestMethod -Uri "$Server$readPage" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
        $count += $pageRead.ItemsPerPage
        $machines += $pageRead.Items
        $readPage = $pageRead.Links.{Page.Next}
    }
    return $machines
}


# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Task($Server, $ApiKey, $TaskId) 
{
    return Invoke-RestMethod -Uri "$Server/api/tasks/$TaskId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-VariableSet($Server, $ApiKey, $VariableId) 
{
    return Invoke-RestMethod -Uri "$Server/api/variables/$VariableId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-LibraryVariableSet($Server, $ApiKey, $VariableId) 
{
    return Invoke-RestMethod -Uri "$Server/api/libraryvariablesets/$VariableId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-LibraryVariableSets($Server, $ApiKey) 
{
    return Invoke-RestMethod -Uri "$Server/api/libraryvariablesets/all" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-Machines($Server, $ApiKey, $EnvironmentId)
{
    return Invoke-RestMethod -Uri "$Server/api/environments/$EnvironmentId/machines" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Create-Deployment($Server, $ApiKey, $EnvironmentId, $ProjectId, $ReleaseId)
{
    $newDeployment = @{ EnvironmentId = $EnvironmentId
                        ProjectId = $ProjectId
                        ReleaseId = $ReleaseId
                      }
    $body = $newDeployment | ConvertTo-Json
    
    return Invoke-RestMethod -Method Post  -Uri "$Server/api/deployments" -Body $body -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

function Create-Environment($Server, $ApiKey, $EnvironmentName, $EnvironmentDescription)
{
    $newEnvironment = @{ Name = $EnvironmentName
                         Description = $EnvironmentDescription
                         SortOrder = 1
                         UseGuidedFailure = $false
                       }
    $body = $newEnvironment | ConvertTo-Json

    return Invoke-RestMethod -Method Post  -Uri "$Server/api/environments" -Body $body -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Update-ReleaseVariables($Server, $ApiKey, $ReleaseId)
{
    return Invoke-RestMethod -Method Post  -Uri "$Server/api/releases/$ReleaseId/snapshot-variables" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Remove-Deployment($Server, $ApiKey, $DeploymentId)
{
    return Invoke-RestMethod -Method Delete  -Uri "$Server/api/deployments/$DeploymentId" -Headers @{"X-Octopus-ApiKey"=$ApiKey}
}

