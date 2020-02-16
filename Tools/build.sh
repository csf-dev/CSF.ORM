#!/bin/bash

SCRIPT_PATH="$(dirname $0)"
source "$SCRIPT_PATH/common-vars.txt"
CONFIGURATION="${1:-Debug}"

function core_build() {
    cd "$PROJECT_ROOT"
    dotnet build -c "$CONFIGURATION"
}

function run_tests() {
    dotnet test
}

core_build
run_tests
