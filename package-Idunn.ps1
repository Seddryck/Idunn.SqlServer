$root = (split-path -parent $MyInvocation.MyCommand.Definition)
$lib = "$root\.package\lib\45\"
if (Test-Path $lib)
{
	Remove-Item $lib -recurse
}
new-item -Path $lib -ItemType directory
new-item -Path $root\.nupkg -ItemType directory -force
Copy-Item $root\Idunn.SqlServer\bin\Debug\* $lib

$version = $env:GitVersion_NuGetVersion
if ([string]::IsNullOrEmpty($version))
{
    Write-Warning "No version found in environment variables, using version of the dll"
    $version = (Get-Item $lib\Idunn.SqlServer.dll).VersionInfo.FileVersion
}
Write-Host "Setting .nuspec version tag to $version"

$content = (Get-Content $root\Idunn.SqlServer.nuspec -Encoding UTF8) 
$content = $content -replace '\$version\$',$version

$content | Out-File $root\.package\Idunn.SqlServer.compiled.nuspec -Encoding UTF8

& NuGet.exe pack $root\.package\Idunn.SqlServer.compiled.nuspec -Version $version -OutputDirectory $root\.nupkg

