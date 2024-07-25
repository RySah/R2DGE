#!/bin/bash

# Variables
base_directory = "/mnt/c/Users/{USER}/source/repos"
solution_name = "MySolution"
engine_project_name = "R2DGE"
user_project_name = "$solution_name"
engine_project_template_path = "/mnt/c/path/to/predefined/project/template"
# EndOfVariables

# Create base directory if it doesn't exist
if [ ! -d "$base_directory" ]; then
    mkdir -p "$base_directory"
fi

# Change to the base directory
cd "$base_directory"

# Create a new solution
dotnet new sln -n $solution_name

# Create a new predefined project directory and copy files
dotnet new classlib -n $engine_project_name
rm -rf $engine_project_name/*
cp -r $engine_project_template_path/* $engine_project_name/

# Add predefined project to the solution
dotnet sln add "$engine_project_name/$engine_project_name.csproj"

# Restore packages for the predefined project
cd $engine_project_name
dotnet restore
cd ..

# Create a new user project
dotnet new console -n $user_project_name

# Add user project to the solution
dotnet sln add "$user_project_name/$user_project_name.csproj"
