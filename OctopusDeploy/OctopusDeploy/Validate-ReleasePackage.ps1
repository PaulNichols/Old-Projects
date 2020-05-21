<# 
.SYNOPSIS 
    Checks the application order of the supplied Release Package config file agains the AppsToRemove sections defined in related BTDF solution files.
    Allows the user to reorder the applications if required.

.DESCRIPTION 
    Given a release package XML file and the root source code folder, the script will load up all found BTDF config files on the main branch for each application.
    Then it will confirm the application order in the Release Package XML with the corresponding BTDF list of AppsToRemove to ensure that the any applications to remove
    are listed after the application gettign checked. This will ensure that as the applications are installed from the Release Package XML no applications will be undeployed.
    If there are any duplicate applications listed in the Release Package file they will be noted as well. 
    If the order is incorrect, the user will be prompted to correct the order. If y is pressed, the order will be resorted and saved to the file
    There is no automated fixing of duplicates as the script cannot determine which version is the the intended one for the package. Duplications will need to be manually corrected.

.EXAMPLE 
    .\Validate-ReleasePackage.ps1 -ReleasePackageFile C:\temp\release-notes.xml -RootLocalTFSPath C:\work\RACQESB 
    This will validate the Release Package file C:\temp\release-notes.xml for both application order and duplicate check against source code located in C:\work\RACQESB.
    If the user presses 'y' when prompted, the script will re-order the applications and save to the file.

.LINK
	http://wiki.racqgroup.local/Confluence/display/tech/Validate-ReleasePackage+script

.PARAMETER ReleasePackageFile 
   The Release Package filename. The xml file to validate. Usually generated from Generate-ReleasePackage.ps1

.PARAMETER RootLocalTFSPath 
   The root folder for the local TFS RACQESB source code. e.g. C:\work\RACQESB or C:\tfs\RACQESB. If blank, the validation check is aborted.
#> 
Param (
    [Parameter(Mandatory=$true)]
    [string]
    $ReleasePackageFile,

    [string]
    $RootLocalTFSPath = ""
    )

# Check the path
if ( $RootLocalTFSPath -eq "")
{
    Write-Host "Root TFS path not provided. Not doing validation of Release Package file." -f DarkCyan
    return $true
}

# Check the path
if (!(Test-Path $RootLocalTFSPath -PathType Container))
{
    Write-Host "Unable to find directory '$RootLocalTFSPath'. Please check parameter and try again." -f Red
    return $false
}

if (!(Test-Path $ReleasePackageFile -PathType Leaf))
{
    Write-Host "Unable to find release package file '$ReleasePackageFile'. Please check parameter and try again." -f Red
    return $false
}

Write-Host "Finding all *.btdfproj files on the main branch from $RootLocalTFSPath..." -f DarkCyan
# load all Deployment.btdfproj files in the main folder 
$configFiles = Get-ChildItem  -Path $RootLocalTFSPath -filter *.btdfproj -Recurse | Where-Object {$_.Directory -like "*\main\*"} 

Write-Host "Scanning all $($configFiles.Length) files found..." -f DarkCyan
$configData = @()
# build array of the files and the list of apps to remove.
foreach ($file in $configFiles)
{
    [xml]$data = Get-Content $file.FullName
    
    # get the list of apps to remove
    $configData += New-Object PsObject -Property @{ApplicationName=$data.Project.PropertyGroup.ProjectName[0]; AppsToRemove=$data.Project.ItemGroup.AppsToRemove.Include}
}

[xml]$releaseData = Get-Content $ReleasePackageFile

$releaseApps = $releaseData.ReleasePackage.Applications.Application.Name
$orderIssues = $false
$dupeIssues = $false
$fixList = @()
Write-Host "Validating application order in $ReleasePackageFile" -f DarkCyan
foreach ($app in $releaseApps)
{
    Write-Host "Checking " -f DarkCyan -NoNewline; Write-Host "$app " -f Cyan -NoNewline; Write-Host ": " -f DarkCyan -NoNewline
    $appCheck = $configData | Where-Object ApplicationName -eq $app 
    $appCount = ($releaseData.ReleasePackage.Applications.Application | Where-Object Name -eq $app | Measure-Object).Count

    if (($appCheck -eq $null) -or ($appCheck.AppsToRemove -eq $null) -or ($appCheck.AppsToRemove -eq ""))
    {
        if ($appCount -ne 1)
        {
            $dupeIssues = $true
            Write-Host "ERROR: Duplicate application entries exist. ($appCount found)." -f Red
        }
        else
        {
            # No apps to remove defined so flag it as okay.
            Write-Host "OK" -f Green
        }
    }
    else
    {
        # Get the list of apps to check
        $apps = $appCheck.AppsToRemove -split " "
        $selfIndex = $releaseApps.IndexOf($app)
        $errorText = ""
        foreach ($a in $apps)
        {
            $index = $releaseApps.IndexOf($a)
            if ($index -ne -1 -and $index -lt $selfIndex)
            {
                $errorText += "$a "
                $orderIssues = $true
                $fixList += New-Object PsObject -Property @{MoveApp=$app; BeforeApp=$a}
            }
        }
        if ($errorText -ne "")
        {
            Write-Host "ERROR: Following apps need to be listed AFTER this one: $errorText" -f Red
        }
        elseif ($appCount -ne 1)
        {
            $dupeIssues = $true
            Write-Host "ERROR: Duplicate application entries exist. ($appCount found)." -f Red
        }
        else
        {
            Write-Host "OK" -f Green
        }
    }
}
Write-Host

if ($orderIssues)
{
    $performFix = Read-Host -Prompt "Do you wish to correct the application order? (y or n)"

    if ($performFix -eq "y")
    {
        if ($dupeIssues)
        {
            Write-Host "Duplicate applications detected. Fix those first and try the command again." -f Yellow
        }
        else
        {
            Write-Host "Fixing the application order for $ReleasePackageFile..." -f Yellow
            foreach ($fixApp in $fixList)
            {
                #get the indexes for both apps
                $moveIndex = $releaseData.ReleasePackage.Applications.Application.Name.IndexOf($fixApp.MoveApp)
                $beforeIndex = $releaseData.ReleasePackage.Applications.Application.Name.IndexOf($fixApp.BeforeApp)

                #only do the re-order if it actually helps.
                if ($moveIndex -gt $beforeIndex)
                {
                    $move = $releaseData.ReleasePackage.Applications.Application | Where-Object Name -eq $fixApp.MoveApp
                    $before = $releaseData.ReleasePackage.Applications.Application | Where-Object Name -eq $fixApp.BeforeApp
                    Write-Host "Moving application $($fixApp.MoveApp)" -f DarkCyan
                    $releaseData.ReleasePackage.Applications.InsertBefore($move, $before) | Out-Null
                }
            }
            # if platform is in the list ensure it's the first app
            $platform = $releaseData.ReleasePackage.Applications.Application | Where-Object Name -eq "RACQESB.Platform"
            if ($platform)
            {
                $first = $releaseData.ReleasePackage.Applications.Application[0]
                if ($platform.Name -ne $first.Name)
                {
                    Write-Host "Moving application RACQESB.Platform to the top" -f DarkCyan
                    $releaseData.ReleasePackage.Applications.InsertBefore($platform,$first) | Out-Null
                }
            }
            $releaseData.Save($ReleasePackageFile)
            Write-Host "$ReleasePackageFile has been updated. Would recommend running this script again to confirm." -f Yellow
         }
    }
}
return !($orderIssues -or $dupeIssues)