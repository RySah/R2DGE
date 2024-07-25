# Variables
$baseDirectory = "C:\path\to\projects"
$solutionName = "MySolution"
$engineProjectName = "R2DGE"
$userProjectName = $solutionName
$engineProjectTemplatePath = "C:\path\to\predefined\project\template"
# EndOfVariables

# Create base directory if it doesn't exist
if (-Not (Test-Path -Path $baseDirectory)) {
    New-Item -ItemType Directory -Path $baseDirectory
}

# Change to the base directory
Set-Location -Path $baseDirectory

# Create a new solution
dotnet new sln -n $solutionName

# Create a new predefined project directory and copy files
dotnet new classlib -n $engineProjectName
Remove-Item -Recurse -Force "$engineProjectName\*"
Copy-Item -Recurse -Path "$engineProjectTemplatePath\*" -Destination $engineProjectName

# Add predefined project to the solution
dotnet sln add "$engineProjectName\$engineProjectName.csproj"

# Restore packages for the predefined project
Set-Location $engineProjectName
dotnet restore
Set-Location ..

# Create a new user project
dotnet new console -n $userProjectName

# Add user project to the solution
dotnet sln add "$userProjectName\$userProjectName.csproj"
