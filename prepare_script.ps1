# make sure you have set the execution policy to Unrestricted
# Set-ExecutionPolicy Unrestricted

[CmdletBinding()] Param (
  [Parameter (Mandatory,HelpMessage='You must enter the new component name (without .dll extension!)')] [string] $componentName
)

$BaseDirectory = ".\"

# JSON configuration filename to use
$BaseConfig = "prepare_settings.json"

$Config = ""

# Load and parse the JSON configuration file
try {
    $Config = Get-Content "$BaseDirectory$BaseConfig" -Raw -ErrorAction:SilentlyContinue -WarningAction:SilentlyContinue | ConvertFrom-Json -ErrorAction:SilentlyContinue -WarningAction:SilentlyContinue
} catch {
  Throw "The Base configuration file is missing!!"
}

# Check the configuration
if (!($Config)) {
  Throw "The Base configuration file is missing!"
}

$templateName = "_ComNameToReplace_"

$vsPath  = ($Config.Basic.VisualStudioPath)
$srcPath = $templateName  + ".dll"
$newPath = $componentName + ".dll"
$message = "Processing files of " + $componentName

cls

Write-Progress -CurrentOperation $message ( "Please wait a moment ..." )

if (!(Test-Path $vsPath)) {
    Throw "Path not exists: " + $vsPath
}

# copy whole template to a new location
New-Item -ItemType directory -Path "$newPath" -Force | Out-Null
Copy-Item -Path "$srcPath\*" -Destination "$newPath\" -Recurse -Force

# remove unnecessary packages
Remove-Item -Path "$newPath\packages\*" -Recurse -Force

# temporary name replacement
$filenames = @(Get-ChildItem -Path "$newPath\*" -Recurse -File -Exclude "Component.snk" | % { $_.FullName })

# generate needed GUIDs
$guidStr1 = [guid]::NewGuid().Guid.ToUpper() <# Solution base GUID #>
$guidStr2 = [guid]::NewGuid().Guid.ToUpper() <# Project MAIN GUID #>
$guidStr3 = [guid]::NewGuid().Guid.ToUpper() <# Project TEST GUID #>
$guidStr4 = [guid]::NewGuid().Guid.ToUpper() <# Component GUID #>
$guidStr5 = [guid]::NewGuid().Guid.ToUpper() <# Interface GUID #>
$guidStr6 = [guid]::NewGuid().Guid.ToUpper() <# Typelib GUID #>

$counter = 0
$maxCount = $filenames.Count

foreach ($file in $filenames)
{
     $counter++
     Write-Progress -Id 1 -Activity "Updating" -Status 'Progress' -PercentComplete (100*$counter/$maxCount) -CurrentOperation $message

     Set-ItemProperty $file -name IsReadOnly -value $false

    (Get-Content $file) |
        Foreach-object { $_ -replace $templateName , $componentName } |
        <# Template static GUIDs #>
        Foreach-object { $_ -replace $Config.Internal.SolutionBaseGUID , $guidStr1 } |
        Foreach-object { $_ -replace $Config.Internal.ProjectMainGUID  , $guidStr2 } |
        Foreach-object { $_ -replace $Config.Internal.ProjectTestGUID  , $guidStr3 } |
        Foreach-object { $_ -replace $Config.Internal.ComponentGUID    , $guidStr4 } |
        Foreach-object { $_ -replace $Config.Internal.ComponentGUID    , $guidStr5 } |
        Foreach-object { $_ -replace $Config.Internal.TypelibGUID      , $guidStr6 } |
     Set-Content $file
}

# final work
Get-ChildItem -Path "$newPath\*" -Recurse -File -Exclude "Component.snk" | Rename-Item -NewName { $_.Name.replace($templateName, $componentName) }

cls

# rebuild dependencies
$fileExe = $vsPath + "\MSBuild\Current\Bin\MSBuild.exe"

if (!(Test-Path $fileExe -PathType Leaf)) {
    Throw "File not exists: " + $fileExe
}

& $fileExe -m -t:restore -p:RestorePackagesConfig=true $newPath

# move secret file

$sourceFile = "$newPath\devel_secret.json"
$destinationFile = "$env:userprofile\AppData\Roaming\devel_secret.json"

if (!(Test-Path $destinationFile)) {
  Move-Item -Path $sourceFile -Destination $destinationFile -Confirm:$false
}
else {
  Move-Item -Path $sourceFile -Destination $destinationFile -Confirm:$true
}

'*' * 80
Write-Host "Remember to rename target COM+ application (formerly: COM+ package name) in install.bat and deploy.bat"
Write-Host "All done!"
