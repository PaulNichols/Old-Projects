<# 
.SYNOPSIS 
    Contains generic helper functions for the main Octopus Deploy related script in this folder

.DESCRIPTION 
    Trying to adhere to the DRY principle and if there were repeated functions in multiple files they would get commonised here.
#> 

$JiraRegexExpression = '\[([A-Z]{2,7}-[0-9]{1,5})\]'

function IsDeploymentsDifferent($left, $right)
{
    if (($left -eq $null) -or ($right -eq $null))
    {
        return $trueech
    }
    return (($left.Version -ne $right.Version) -or ($left.Status -ne $right.Status))
}

function LaterOrOlderDeployment($left, $right)
{
    if (($left -eq $null) -or ($right -eq $null))
    {
        return $trueech 
    }
    
    if($left.Version -gt $right.Version)
    {
        return 'newerVersion'
    }

    if($left.Version -lt $right.Version)
    {
        return 'olderVersion'
    }
    
    return $false
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Populate-ApplicationsFromFile($relPackageFile, $projects)
{
    $apps = @()
    Write-Host "Determining application list from file $relPackageFile..." -f DarkCyan
    
    [xml]$packageFile = Get-Content $relPackageFile

    foreach ($app in $packageFile.ReleasePackage.Applications.Application)
    {
        $proj = $projects | Where-Object Name -eq $app.Name
        $release = Get-ProjectReleaseVersion $ServerName -ApiKey $ApiKey -ProjectId $proj.Id -Version $app.Version
        $apps += New-Object PsObject -Property @{ProjectId=$proj.Id;ProjectName=$proj.Name;Version=$release.Version;ReleaseId=$release.Id}
    }

    return ,$apps
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Populate-Applications($env, [switch]$findLastDeployedSuccessfully)
{
    $apps = @()
    Write-Host "Determining current applications deployed in $($env.Name)" -NoNewline -f DarkCyan
 
    foreach ($item in $dashboard.Items)
    {
       if ($item.EnvironmentId -eq $env.Id)
       {
          $project = Get-Project -Server $ServerName -ApiKey $ApiKey -ProjectId $item.ProjectId

          # only interested in applications that start with RACQESB.
          if ($project.Name -like "RACQESB.*")
          {
              Write-Host "." -NoNewline -f DarkCyan
          
              $release = Get-Release -Server $ServerName -ApiKey $ApiKey -ReleaseId $item.ReleaseId
              $task = Get-Task -Server $ServerName -ApiKey $ApiKey -TaskId $item.TaskId
              # if the flag is set, ensure it was deployed successfully
              if ($findLastDeployedSuccessfully)
              {
                 # TODO
                 Write-Host "findLastDeployedSuccessfully is True! Not implemented yet though!" -f Yellow
                 if ($task.State -ne "Success")
                 {
                    # TODO  
                 }
              }
              $apps += New-Object PsObject -Property @{ProjectId=$project.Id; ProjectName=$project.Name; Version=$release.Version; ReleaseId=$release.Id; Status=$task.State;DeployedTime=$task.CompletedTime}
          }
       }
    }
    $apps = $apps | Sort-Object -Property DeployedTime

    $apps = MoveTo-TopOfArray $apps "MasterDataService"
    $apps = MoveTo-TopOfArray $apps "RACQESB.Platform"

    Write-Host
    return ,$apps
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function MoveTo-TopOfArray($appArray, $searchString)
{
    $matchingApps = $appArray | Where-Object ProjectName -like "*$searchString*"
    
    if ($matchingApps)
    {
        $tempArray = @()
        foreach ($match in $matchingApps)
        {
            $tempArray += $match
        }
        foreach ($a in $appArray |Where-Object ProjectName -notlike "*$searchString*")
        {
            $tempArray += $a
        }
        return ,$tempArray
    }
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function AreReleaseNoteRequired($from, $to)
{
    if ($from -eq $null)
    {
        return $false
    }
    if ($to -eq $null)
    {
        return $true
    }
    return ($from.Version -ne $to.Version)
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Get-JiraDetails($JiraId) 
{
    # Username: Dashboard     Password: RACQ1234  (use Postman to generate the Basic auth if the account details change)
    return Invoke-RestMethod -Uri "http://jira.racqgroup.local:12001/rest/api/2/issue/$jiraId" -Headers @{"Authorization"="Basic RGFzaGJvYXJkOlJBQ1ExMjM0"}
}

# -------------------------------------------------------------------------------------------------------------------------------------------------

function Create-ReleaseNotes ($fromApps, $toEnv, $toApps, $releaseDescription)
{
    $releaseNotes = "Release Notes: $releaseDescription`n"
    $releaseNotes += "Created by $($env:USERNAME) on $(Get-Date)`n"
    
    # need to build up the array of Jiras actioned, App updates, and apps not touched.
    $appsNew = @()
    $appsUpdated = @()
    $jirasActioned = @()
    $appsNotUpdated = @()

    foreach ($appFrom in $fromApps)
    {
        $appTo = $toApps | Where-Object ProjectName -eq $appFrom.ProjectName

        if (AreReleaseNoteRequired $appFrom $appTo)
        {
            Write-Host "- $($appFrom.ProjectName)" -f DarkCyan
            if ($appTo -eq $null)
            {
                $appsNew += New-Object PsObject -Property @{Name=$appFrom.ProjectName; Version=$appFrom.Version}
                # get the release for this verion
                # add any Jiras
                $release = Get-ProjectReleaseVersion -Server $ServerName -ApiKey $ApiKey -ProjectId $appFrom.ProjectId -Version $appFrom.Version
                $relNotes = $release.ReleaseNotes
                if ($relNotes -ne $null)
                {
                    $jiraIds = [regex]::Matches($relNotes, $JiraRegexExpression) | select value
                    foreach ($jira in $JiraIds)
                    {
                        $jiraId = $jira.Value -replace '[\[\]]',''
                        # only get the details for the Jira if we have not already done it for this applications
                        if (($jirasActioned | Where-Object Id -eq $jiraId) -eq $null) 
                        {
                            $jiraRelNote = $null
                            try
                            {
                                $jiraRecord = Get-JiraDetails $jiraId
                                $jiraTitle = $jiraRecord.fields.Summary
                                $relNoteComment = $jiraRecord.fields.customfield_10870  # grabs the release notes field (custom field) if it exists.
                                if ($relNoteComment -ne $null -and $relNoteComment -ne "")
                                {
                                    $jiraRelNote = "`t$($relNoteComment -replace "`r`n", "`r`n`t")`n" 
                                }
                            }
                            catch [Exception]
                            { 
                                $jiraTitle = "**ERROR** Unable to find Jira record details."
                            }
                            $jirasActioned += New-Object PsObject -Property @{Id=$jiraId; Title=$jiraTitle; RelNote=$jiraRelNote}
                        }
                    }
                }
            }
            else
            {
                $appsUpdated += New-Object PsObject -Property @{Name=$appFrom.ProjectName; VersionFrom=$appTo.Version; VersionTo=$appFrom.Version}
                if ([System.Version]$appFrom.Version -gt [System.Version]$appTo.Version )
                {
                    $appReleases = Get-ProjectReleases -Server $ServerName -ApiKey $ApiKey -ProjectId $appFrom.ProjectId

                    # build up list of Releases to include in Release Notes doco
                    $searchForFirst = $true
                    $includedReleases = @()
                    # need to sort the released by Version

                    $sortedAppReleases = $appReleases.Items | Sort-Object -Property Version -Descending
                    foreach ($rel in $sortedAppReleases)
                    {
                        if ($searchForFirst -and $rel.Version -eq $appFrom.Version)
                        {
                            $searchForFirst = $false
                            $includedReleases += $rel
                        }
                        else
                        {
                            if (!$searchForFirst)
                            {
                                if ($rel.Version -eq $appTo.Version)
                                {
                                    break
                                }
                                $includedReleases += $rel
                            }
                        }
                    }
                    foreach ($incRel in $includedReleases)
                    {
                        $relNotes = $incRel.ReleaseNotes
                        if ($relNotes -ne $null)
                        {
                            $jiraIds = [regex]::Matches($relNotes, $JiraRegexExpression) | select value
                            foreach ($jira in $JiraIds)
                            {
                                $jiraId = $jira.Value -replace '[\[\]]',''
                                # only get the details for the Jira if we have not already done it for this applications
                                if (($jirasActioned | Where-Object Id -eq $jiraId) -eq $null) 
                                {
                                    $jiraRelNote = $null
                                    try
                                    {
                                        $jiraRecord = Get-JiraDetails $jiraId
                                        $jiraTitle = $jiraRecord.fields.Summary
                                        $relNoteComment = $jiraRecord.fields.customfield_10870  # grabs the release notes field (custom field) if it exists.
                                        if ($relNoteComment -ne $null -and $relNoteComment -ne "")
                                        {
                                            $jiraRelNote = "`t$($relNoteComment -replace "`r`n", "`r`n`t")`n" 
                                        }
                                    }
                                    catch [Exception]
                                    { 
                                        $jiraTitle = "**ERROR** Unable to find Jira record details."
                                    }
                                    $jirasActioned += New-Object PsObject -Property @{Id=$jiraId; Title=$jiraTitle; RelNote=$jiraRelNote}
                                }
                            }
                        }
                    }
                }
            }
        }
        else # not updated due to no version difference detected
        {
            $appsNotUpdated += New-Object PsObject -Property @{Name=$appFrom.ProjectName; Version=$appFrom.Version}
        }
    }
    # have all the information to populate the release notes
    $releaseNotes += "`nJIRAs actioned:`n`n"
    $jirasActioned = $jirasActioned | Sort-Object -Property Id
    foreach ($jira in $jirasActioned)
    {
        $jiraFormatted = "[$($jira.Id)]".PadRight(10)
        $releaseNotes += "$jiraFormatted - $($jira.Title)`n"
        if ($jira.RelNote)
        {
            $releaseNotes += "$($jira.RelNote)`n"
        }
    }
    $appsNew = $appsNew | Sort-Object -Property Name 
    $appsUpdated = $appsUpdated | Sort-Object -Property Name 
    $appsNotUpdated = $appsNotUpdated | Sort-Object -Property Name 

    if ($appsNew.Length -gt 0)
    {
        $releaseNotes += "`nApplications new to $($toEnv.Name)`:`n`n"
        foreach ($app in $appsNew) { $releaseNotes += "$($app.Name) - $($app.Version)`n" }
    }
    if ($appsUpdated.Length -gt 0)
    {
        $releaseNotes += "`nApplications updated in $($toEnv.Name)`:`n`n"
        foreach ($app in $appsUpdated)  
        { 
            $releaseNotes += "$($app.Name) - $($app.VersionFrom) -> $($app.VersionTo)" 
            if ([System.Version]$app.VersionFrom -gt [System.Version]$app.VersionTo)
            {
                $releaseNotes += " (DOWNGRADE)`n"
            }
            else
            {
               $releaseNotes += "`n"
            }
        }
    }

    if ($appsNotUpdated.Length -gt 0)
    {
        $releaseNotes += "`nApplications not updated in $($toEnv.Name):`n`n"
        foreach ($app in $appsNotUpdated) { $releaseNotes += "$($app.Name) - $($app.Version)`n" }
    }
    return $releaseNotes
}