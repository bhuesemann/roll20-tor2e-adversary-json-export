<#
.SYNOPSIS
    Releases a new version: updates RELEASE and CHANGELOG.md, commits, tags and pushes.

.DESCRIPTION
    Pushing the tag triggers the GitHub "Build" workflow, which builds the tool and
    publishes a GitHub release using the notes from the matching CHANGELOG.md section.

    If CHANGELOG.md has no "## [<version>]" section yet, the contents of the
    "## [Unreleased]" section are moved into a new section dated today.

.EXAMPLE
    ./release.ps1 0.2.2
.EXAMPLE
    ./release.ps1 0.2.2 -NoPush      # commit and tag locally only
.EXAMPLE
    ./release.ps1 0.2.2 -WhatIf      # show what would happen
#>
#Requires -Version 7

[CmdletBinding(SupportsShouldProcess, ConfirmImpact = 'High')]
param(
    [Parameter(Mandatory, Position = 0)]
    [ValidatePattern('^\d+\.\d+\.\d+$')]
    [string]$Version,

    [string]$Remote = 'origin',

    [string]$Branch = 'master',

    [switch]$NoPush
)

$ErrorActionPreference = 'Stop'
Set-Location $PSScriptRoot

function Invoke-Git {
    $output = & git @args 2>&1
    if ($LASTEXITCODE -ne 0) { throw "git $($args -join ' ') failed:`n$output" }
    return $output
}

# --- preconditions -----------------------------------------------------------

$currentBranch = Invoke-Git rev-parse --abbrev-ref HEAD
if ($currentBranch -ne $Branch) {
    throw "Releases are made from '$Branch', but you are on '$currentBranch'."
}

if (Invoke-Git status --porcelain) {
    throw 'Working tree is not clean. Commit or stash your changes first.'
}

Write-Host "Fetching $Remote..."
Invoke-Git fetch $Remote --tags | Out-Null
$behind = [int](Invoke-Git rev-list --count "HEAD..$Remote/$Branch")
if ($behind -gt 0) {
    throw "Local '$Branch' is $behind commit(s) behind $Remote/$Branch. Pull first."
}

if (Invoke-Git tag --list $Version) {
    throw "Tag '$Version' already exists."
}

$current = (Get-Content RELEASE -Raw).Trim()
if ([version]$Version -lt [version]$current) {
    throw "Version $Version is lower than the current version $current."
}

# --- CHANGELOG.md ------------------------------------------------------------

$changelog = Get-Content CHANGELOG.md -Raw
$nl = if ($changelog -match "`r`n") { "`r`n" } else { "`n" }
$escaped = [regex]::Escape($Version)

if ($changelog -match "(?m)^## \[$escaped\]") {
    Write-Host "CHANGELOG.md already has a section for $Version."
}
else {
    # everything between "## [Unreleased]" and the next "## [" heading
    $unreleased = [regex]::Match($changelog, '(?ms)^## \[Unreleased\][ \t]*\r?\n(.*?)(?=^## \[)')
    if (-not $unreleased.Success -or -not $unreleased.Groups[1].Value.Trim()) {
        throw "CHANGELOG.md has no section for $Version and nothing under [Unreleased]. Add release notes first."
    }
    $notes = $unreleased.Groups[1].Value.Trim()
    $date = Get-Date -Format 'yyyy-MM-dd'
    $replacement = "## [Unreleased]$nl$nl## [$Version] - $date$nl$notes$nl$nl"
    $changelog = $changelog.Remove($unreleased.Index, $unreleased.Length).Insert($unreleased.Index, $replacement)
    Write-Host "Moved [Unreleased] notes to a new [$Version] - $date section."
}

# --- build check -------------------------------------------------------------

Write-Host 'Building in Release configuration...'
dotnet build --configuration Release --nologo --verbosity quiet
if ($LASTEXITCODE -ne 0) { throw 'Build failed, aborting release.' }

# --- commit, tag, push -------------------------------------------------------

if ($PSCmdlet.ShouldProcess("version $Version", 'Update RELEASE and CHANGELOG.md, commit and tag')) {
    [System.IO.File]::WriteAllText("$PSScriptRoot/CHANGELOG.md", $changelog)
    # no trailing newline: the workflow uses the file contents as the tag name
    [System.IO.File]::WriteAllText("$PSScriptRoot/RELEASE", $Version)

    Invoke-Git add RELEASE CHANGELOG.md | Out-Null
    if (Invoke-Git status --porcelain) {
        Invoke-Git commit --quiet -m "Release $Version" | Out-Null
    }
    Invoke-Git tag --annotate $Version -m "Release $Version" | Out-Null
    Write-Host "Created tag $Version on $(Invoke-Git rev-parse --short HEAD)."
}
else {
    return
}

if ($NoPush) {
    Write-Host "Not pushed. When ready: git push $Remote $Branch $Version"
    return
}

if ($PSCmdlet.ShouldProcess("$Remote ($Branch and tag $Version)", 'Push and trigger the release workflow')) {
    Invoke-Git push --atomic $Remote $Branch $Version | Out-Null
    Write-Host "Pushed. The release workflow is now running for $Version."
}
