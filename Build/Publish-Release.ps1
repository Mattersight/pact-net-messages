param (
	[Parameter(Mandatory=$true)]
	[ValidatePattern('\d\.\d\.*')]
	[string]
	$ReleaseVersionNumber,
	
	[string]$ApiKey
)

$PSScriptFilePath = (Get-Item $MyInvocation.MyCommand.Path).FullName

$BuildRoot = Split-Path -Path $PSScriptFilePath -Parent
$SolutionRoot = Split-Path -Path $BuildRoot -Parent
$NuGetExe = Join-Path $BuildRoot -ChildPath '..\.nuget\nuget.exe'

Write-Output $ReleaseVersionNumber

# Build the NuGet package
$ProjectPath = Join-Path -Path $SolutionRoot -ChildPath 'PactNetMessages\PactNetMessages.nuspec'
& $NuGetExe pack $ProjectPath -Prop Configuration=Release -OutputDirectory $BuildRoot -Version $ReleaseVersionNumber
if (-not $?)
{
	throw 'The NuGet process returned an error code.'
}

# Upload the NuGet package
if($ApiKey)
{
	& $NuGetExe setApiKey $ApiKey
	
	$NuPkgPath = Join-Path -Path $BuildRoot -ChildPath "PactNetMessages.$ReleaseVersionNumber.nupkg"
	& $NuGetExe push $NuPkgPath -Source https://www.nuget.org
	if (-not $?)
	{
		throw 'The NuGet process returned an error code.'
	}
}