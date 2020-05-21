<#
.SYNOPSIS
Performs a clean up of a folder containing nuget packages in the format of <package Name>.a.b.c.d.nupkg. The clean up involves actioning any file older than the keep number.
It treats the packagename and the major and minor number of the version as a unique package. [<package Name>.a.b].c.d.nupkg  is the 
The actions available are either delelete or move to another folder.
 
.DESCRIPTION
This script will keep on the last $KeepNum number of packages for each unique package in the folder $PackageLocation. The user can chose to either Move the files or Delete the files.
This script is currently utilised a lot on the Octopus Deploy server to clean on the local Nuget Cache folder. Currently the Nuget server is file based which takes a long time to re-index the packages
whenever a few file is dropped into the Package folder. Running this will keep the file count down, until we can migrate to a SQL Server based Nuget Server.
 
.PARAMETER PackageFolder
Fully qualified location of package folder to clean up.
 
.PARAMETER Action
Can be either Move or Delete. Determines the action to perform.
 
.PARAMETER KeepNum
Defaults to 4 if not specified. Determines the number of latest packages to keep for each unique package in the folder.
 
.PARAMETER MoveFolder
Only required if the Action is set to 'Move'. Specifies a folder to move older packages

.PARAMETER WhatIf
If specified, the script will output what it would do if run without -WhatIf. Not actual deleting or moving occurs.

 
.EXAMPLE
C:\PS> .\Clean-NugetPackages.ps1 -PackageFolder C:\Packages -Action Move -MoveFolder C:\OldPackages
This will move all packages older than the 4 latest ones for each package from C:\Packages to C:\oldPackages
 
#>

param (
     [Parameter(Mandatory=$true)]
     [string]$PackageFolder, 

     [Parameter(Mandatory=$true)]
     [ValidateSet("Move", "Delete")]
     [string]$Action,
     
     [int]$KeepNum = 4,
     
     [string]$MoveFolder = "",
     
     [switch]$WhatIf)

#  ------------------------------------------------------------------------------------------------------------------------------------------

Function Generate-Version([string]$filename)
{
    $numArray = $filename -split "[^\d]+"

    [string]$buffer = ""
    foreach ($num in $numArray)
    {
        if ($num.Length -gt 0)
        {
            [int]$i = $num
            $buffer += [string]("{0:D4}" -f $i)
        }
    }
    return [int64]($buffer)
}

# This used to just return the package name without the version, but now we need to include the Major and Minor numbers are part of the package to store
# e.g. www.racq.com.2.7.x.x and www.racq.com.2.8.x.x as two separate packages instead of just www.racq.com.x.x.x.x
Function Generate-Package([string]$filename)
{
    #return ($filename -split ".\d+.\d+.\d+.\d+.")[0]

    $rr = [regex]::Match($filename, '(([a-zA-Z]+.)+.\d+.\d+)')
    return $rr.Value
}

#  ------------------------------------------------------------------------------------------------------------------------------------------

# MAIN START

if ($Action -eq "Move" -and ($MoveFolder.Length -eq 0 -or !(Test-Path $MoveFolder -PathType Container)))
{
    Write-Host "The Move Folder $MoveFolder does not exist. Please check it and try again." -f Red
    return
}
Write-Host "Keeping the last $KeepNum packages for each package in folder $PackageFolder" -f Green
if ($Action -eq "Move") { Write-Host "Will move older packages to folder $MoveFolder" -f Green} else {Write-Host "Will be deleting older packages." -f Green}


$files = Get-ChildItem $PackageFolder
Write-Host "$($files.Length) files found to process..." -f DarkCyan

$fileList = @()
foreach ($file in $files)
{
    $fileList += @{"Package"  = Generate-Package $file.Name; 
                   "FullName" = $file.FullName; 
                   "Version"  = Generate-Version $file.Name; 
                   "Action"   = $true}
}

$packages = $fileList | ForEach-Object {$_.Package} | Select-Object -Unique
Write-Host "$($packages.Length) unique packages found" -f DarkCyan

foreach ($pack in $packages)
{
    $fileList | where Package -eq $pack | Sort-Object -Property @{Expression={[int64]$_.Version}} |Select-Object -Last $KeepNum | ForEach-Object {$_.Action = $false}
}

if ($WhatIf)
{
    foreach ($ff in $fileList) 
    {
        Write-Host "$($ff.Version) - [$($ff.Package)] Name: $($ff.FullName) - " -f Cyan -NoNewline; if ($ff.Action) {write-host $Action -f Yellow} else {write-host }
    }
}
else
{
    foreach ($file in $fileList)  
    { 
       if ($file.Action)
       {
           if ($Action -eq "Delete")
           {
               Write-Host "Deleting file: " -f DarkCyan -NoNewline; Write-Host $file.FullName -f Cyan
               Remove-Item $file.FullName -Force
           }
           else
           {
                Write-Host "Moving file: " -f DarkCyan -NoNewline; Write-Host $file.FullName -f Cyan -NoNewline; Write-Host " to folder $MoveFolder" -f DarkCyan
                Move-Item $file.FullName -Destination $MoveFolder
            }
        }
    }

    Write-Host "$(($fileList | Where Action).Count) files have been $Action`d" -f Yellow
    Write-Host "$(($fileList | Where Action -eq $False).Count) files are remaining" -f Yellow
}

Write-Host "All done!" -f Green
