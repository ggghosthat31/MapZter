#!/bin/bash

#release build name
app_name="MapZter"

#prepare allocation directory
function allocate_out_dir () {
    mkdir "../../out/"
    mkdir "../../out/linux"
    mkdir "../../out/linux/$app_name/";
}

#publish app instance
function process_app() {
    local app_path=$1
    local app_out_directory="../../out/linux/$app_name/"
    
    echo "$app_path $app_out_directory"

    dotnet publish $app_path -c Release --framework=net9.0 --runtime=linux-x64 --self-contained -p PublishSingleFile=True -o=$app_out_directory
}

#allocate web API project within output folder
function alloc () {
    allocate_out_dir

    #publish 'app' component
    local app="../../src/$app_name.API/$app_name.API.csproj"
    process_app $app;
}

alloc