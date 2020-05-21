<# 
.SYNOPSIS 
    Will check the differences between what applications Octopus Deploy says is deployed and what is actually deployed for a given BizTalk environment. 
    Script will prompt user to deploy missing applications or not (if any found)

.DESCRIPTION 
    Sometimes after a schema application is re-deployed to an environment, there are logic based applications that are auto undeployed.
    This can occur when Deploy-ReleasePackage.ps1 is called with a subset of applications to install.
    If the schema application change is trivial, there will not be any changes required to the removed logic apps. This script automates the process to quickly get them back into the environment.
    
    Given an environment name, the script will determine the list of applications that Octopus Deploy says is deployed and compare with what is actually installed in that BizTalk environment.
    The Octopus Deploy REST API and the BizTalk Powershell interface are both utilised to obtain this information.
    This list of differences are displayed to the user in an easy to read and coloured format. These include status differences, installation differences and now version differences.
    The user will be prompted to reinstall missing applications if any are found. If they enter Y or y, then the missing applications are then redeployed sequentially to the BizTalk environment.

.EXAMPLE 
    .\Check-Deployments.ps1 -EnvName PR501-SD -ApiKey $octoAPIKey
    This will display the differences between the applications that Octopus thinks is deployed and what is actually deployed.
    Any missing applications will be redeployed into the environment in a sequential order.
.LINK
    http://wiki.racqgroup.local/Confluence/display/tech/Check-Deployments+script

.PARAMETER ServerName 
   The name of the Octopus Deploy Server.If not provided it will default to the current RACQ instance.
.PARAMETER EnvName 
   The name of the environment to check Eg. PR501-DEV, PRE-PROD (must match what is defined in Octopus Deploy)
.PARAMETER ApiKey 
   The Octopus Deploy API key for the user running this script. Will look for $OctopusApiKey if not provided
.PARAMETER OverrideBizTalkSQLServer 
   An optional field that allows the user to override the BizTalk SQL Server. Can be useful for Biztalk environments with a cluster (Pre-Prod and Prod)
#> 

param (
    [string]$ServerName = "octopusdeploy.racqgroup.local",
    
    [Parameter(Mandatory=$true)]
    [string]$EnvName,
    
    [string]$ApiKey = $OctopusApiKey,

    [string]$OverrideBizTalkSQLServer =""

    )

# ================================================================================================================================================

function IsInInstalledApps($btsApps, $appname, $appProjectId)
{
    # get the Project Id
    $proj = Get-Project -Server $ServerName -ApiKey $ApiKey -ProjectId $appProjectId
    # get the deployment process
    $deployProcess = Get-DeploymentProcess -Server $ServerName -ApiKey $ApiKey -DeploymentProcessId $proj.DeploymentProcessId

    #Assume its NOT a BizTalk application if it does not have the step defined below
    if (($deployProcess.Steps.Actions.Name -contains "BTDF Deploy - Biztalk Application") -eq $false)
    {
        return 2  # not applicable
    }
    #However there ARE some exceptions to the rule above. :(
    if ($appName -eq "RACQESB.Integration")
    {
        return 2 # not applicable
    }

    foreach ($btsApp in $btsApps)
    {
        if ($btsApp.Name -eq $appname)
        {
            return 0 #installed
        }
    }
    return 1 #not installed
}

# ================================================================================================================================================

function WriteStatusOutput($status)
{
    switch ($status)
    {
        'Success'   {Write-Host $status.PadRight(10) -f Green -NoNewline}
        'Executing' {Write-Host $status.PadRight(10) -f Yellow -NoNewline}
        'Canceled'  {Write-Host $status.PadRight(10) -f Yellow -NoNewline}
        Default     {Write-Host $status.PadRight(10) -f Red -NoNewline}
    }
}

# ================================================================================================================================================

function Load-BizTalkPowerShell
{
    if ((Get-Module "BizTalkFactory.PowerShell.Extensions") -eq $null) 
    {
        Write-Host "Initialising BizTalkFactory.PowerShell.Extensions..." -f DarkCyan
        $InitializeDefaultBTSDrive = $false
        # check if the updated BizTalk Factory is installed. Otherwise use the older version provided by BizTalk 2013 
        if (Test-Path "C:\Program Files (x86)\BizTalkFactory PowerShell Provider\BizTalkFactory.PowerShell.Extensions.dll")
        {
            $BizTalkPSDll = "C:\Program Files (x86)\BizTalkFactory PowerShell Provider\BizTalkFactory.PowerShell.Extensions.dll"
        }
        else
        {
            $BizTalkPSDll = "C:\Program Files (x86)\Microsoft BizTalk Server 2013\SDK\Utilities\PowerShell\BizTalkFactory.PowerShell.Extensions.dll"
        }
        Import-Module $BizTalkPSDll -DisableNameChecking
     }
 }

# MAIN
# ================================================================================================================================================
# load up common functions
. .\CommonFunctions.ps1

$BTSSQLServer = "biztalk-sql-server"

Write-Host "Reading Octopus Environments..." -f DarkCyan
$environments = Get-Environments -Server $ServerName -ApiKey $ApiKey
$env = $environments | Where-Object Name -eq $EnvName

if ($env -eq $null)
{
    Write-Host "Unable to find environment called '$EnvName'. Check the name and try again" -f Red
    return
 }
Write-host "Found environment $($env.Name). (Id: $($env.Id))" -f DarkCyan

 # Get the Primary BizTalk Server
 Write-Host "Getting machines for $($env.Name)..." -f DarkCyan
 $machines = Get-Machines -Server $ServerName -ApiKey $ApiKey -EnvironmentId $env.Id

 $BizTalkSQLMachine = $machines.Items | Where-Object Roles -Contains $BTSSQLServer | select -First 1

 if ($BizTalkSQLMachine -eq $null)
 {
    Write-Host "Unable to find the BizTalk SQL Server in Environment $($env.Name) with the role '$PrimBTSServer'.`nCheck the Octopus configuration for $($env.Name) and try again" -f Red
    return
 }

 if ($OverrideBizTalkSQLServer -eq "")
 {
    Write-Host "$($env.Name) BizTalk SQL Server    : $($BizTalkSQLMachine.Name)" -f DarkCyan
    $BizTalkServerSQL = $BizTalkSQLMachine.Name
 }
 else
 {
    Write-Host "$($env.Name) BizTalk SQL Server    : $OverrideBizTalkSQLServer  (overridden)" -f DarkCyan
    $BizTalkServerSQL = $OverrideBizTalkSQLServer
 }
 
 Write-Host "Reading the Octopus Dashboard..." -f DarkCyan
 $dashboard = Get-Dashboard -Server $ServerName -ApiKey $ApiKey 

 $currentApps = @()
 Write-Host "Building current applications array" -f DarkCyan -NoNewline
 
 foreach ($item in $dashboard.Items)
 {
    if ($item.EnvironmentId -eq $env.Id)
    {
        Write-Host "." -NoNewline -f DarkCyan
        $project = Get-Project -Server $ServerName -ApiKey $ApiKey -ProjectId $item.ProjectId
        $release = Get-Release -Server $ServerName -ApiKey $ApiKey -ReleaseId $item.ReleaseId
        $task = Get-Task -Server $ServerName -ApiKey $ApiKey -TaskId $item.TaskId
        $currentApps += New-Object PsObject -Property @{ProjectId=$project.Id;ProjectName=$project.Name;Version=$release.Version;ReleaseId=$release.Id;Status=$task.State}
    }
 }

 Write-Host "`nGetting list of BizTalk applications currently in $($env.Name)..." -f DarkCyan
 
 # ensure the Powershell extensions required are loaded
Load-BizTalkPowerShell

# Ensure no drive exists already
If (Test-Path BizTalk:)
{
	Remove-PSDrive -Name "BizTalk"
}


# create the PS-Drive, get the application names and remove the ps drive
# PsDrive Name cannot contain special chars like dot. Root must begin with Name
# Use generic Name of "BizTalk"
$output = New-PSDrive -Name "BizTalk" -PSProvider BizTalk -Root "BizTalk:\" -Instance $BizTalkServerSQL -Database BizTalkMgmtDb -Scope Global
 

if ($output -eq $null)
{
    Write-Host "Unable to create the PS Drive to query the $($env.Name) Biztalk environment. Check permissions and networks to ensure you have access from this PC to $BizTalkServerSQL ." -f Red
    Write-Host "New-PSDrive -Name "BizTalk" -PSProvider BizTalk -Root "BizTalk:\" -Instance $BizTalkServerSQL -Database BizTalkMgmtDb -Scope Global" -f Red
    return
}
# returns an array of all the installed apps (name, version and status)
$installedApps = Get-Item -Path "BizTalk:\Applications\*" | Select-Object Name, @{n='Version';e={([regex]::Match($_.description, '\d+.\d+.\d+.\d+')).Value }},status
Remove-PSDrive -Name "BizTalk"

Write-Host
# sort the list ready for screen display
$currentApps = $currentApps | Sort-Object -Property ProjectName

Write-Host "                          APPLICATION                                  OCTOPUS DEPLOY               BIZTALK"
Write-Host "                             NAME                                     STATUS    VERSION       STATUS       VERSION"
Write-Host "-------------------------------------------------------------------------------------------------------------------"
$redeployApps = @()
foreach ($app in $currentApps)
{
    Write-Host $($app.ProjectName).PadRight(70) -f Cyan -NoNewline; WriteStatusOutput $app.Status; Write-Host $($app.Version).PadRight(13) -f Cyan -NoNewline;
    
    $check = IsInInstalledApps $installedApps $app.ProjectName $app.ProjectId

    switch ($check)
    {
        '0' {
                Write-Host " Installed" -f Green -NoNewline
                $bizTalkApp = $installedApps | Where-Object Name -eq $app.ProjectName
                if ($bizTalkApp.Version -ne "")
                {
                    if ($app.Version -ne $bizTalkApp.Version) 
                    {  
                        Write-Host "    $($bizTalkApp.Version)"  -f Red
                    }
                    else
                    {
                       Write-Host "    $($bizTalkApp.Version)"  -f Cyan
                    }
                }
                else
                {
                    Write-Host "    Unknown" -f Yellow
                }
            }
        '1' {
                Write-Host " NOT Installed" -f Red
                $redeployApps += $app
            }
        Default {Write-Host " Not applicable" -f Yellow}
    }
 }
 Write-Host

if ($redeployApps.count -gt 0)
{
    $redeploy = Read-Host -Prompt "Do you wish to redeploy the $($redeployApps.count) BizTalk applications with a status of 'NOT Installed'? (y or n)"

     if ($redeploy -eq "y")
     {
        Write-Host "Will re-deploy applications that should be present in $($env.Name)" -f DarkCyan
        Write-Host "Auto triggered deployments started at $(Get-Date -format "f")" -f DarkCyan
    
        foreach ($app in $redeployApps)
        {
            $deployment = Create-Deployment -Server $ServerName -ApiKey $ApiKey -EnvironmentId $env.Id -ProjectId $app.ProjectId -ReleaseId $app.ReleaseId
            $task = Get-Task -Server $ServerName -ApiKey $ApiKey -TaskId $deployment.TaskId

            if ($task -eq $null -or $task.Id -eq $null -or $task.Id -eq "")
            {
                Write-Host "Unable to create a task to deploy $($app.ProjectName) version $($app.Version) into $($env.Name). Check the Octopus Deploy UI." -f Red
                $action = Read-Host -Prompt "Do you want to continue? (Y or N): "

                if ($action -eq "Y")
                {
                    continue
                }
                else
                {
                    return
                }
            }    
            Write-Host "Created Task Id: $($task.Id) for redeploying $($app.ProjectName) into $($env.Name). Waiting for task to complete." -f Cyan
                
            while (!$task.IsCompleted)
            {
                Start-Sleep -Seconds 5
                Write-Host "." -f Gray -NoNewline
                # refresh the task info
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
                Write-Host "Deployment of $($app.ProjectName) into $($env.Name) was successful. Duration: $($task.Duration)" -f Green
            }
            else
            {
                Write-Host "Deployment of $($app.ProjectName) into $($env.Name) was NOT successful. Current state: $($task.State)" -f Red
                Write-Host "Stopping further deployments until manual check is complete." -f Red
                return
            }
        }
        Write-Host "Auto triggered deployments completed at $(Get-Date -format "f")" -f DarkCyan
     }
 }
 Write-Host "All Done!" -f DarkCyan
