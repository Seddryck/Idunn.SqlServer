$root = (split-path -parent $MyInvocation.MyCommand.Definition)
$lib = "$root\.package\lib\45\"
If (Test-Path $lib)
{
	Remove-Item $lib -recurse
}
new-item -Path $lib -ItemType directory
new-item -Path $root\.nupkg -ItemType directory -force
Copy-Item $root\Idunn.SqlServer.Console\bin\Debug\* $lib

Write-Host "Setting .nuspec version tag to $env:GitVersion_NuGetVersion"

$content = (Get-Content $root\Idunn.SqlServer.nuspec -Encoding UTF8) 
$content = $content -replace '\$version\$',$env:GitVersion_NuGetVersion

$content | Out-File $root\.package\Idunn.SqlServer.compiled.nuspec -Encoding UTF8

& NuGet.exe pack $root\.package\Idunn.SqlServer.compiled.nuspec -Version $env:GitVersion_NuGetVersion -OutputDirectory $root\.nupkg
