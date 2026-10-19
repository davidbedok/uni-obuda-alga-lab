#!/usr/bin/env bash

# Usage:   ./delete-project.sh <directory-name>
# Example: ./delete-project.sh lorem-ipsum
set -euo pipefail

if [[ $# -ne 1 ]]; then
    echo "Usage: $0 <directory-name>" >&2
    exit 1
fi

directory_name="$1"

# Accept only a single, safe directory name below the labors folder.
if [[ ! "$directory_name" =~ ^[A-Za-z0-9][A-Za-z0-9_-]*$ ]]; then
    echo "Error: the directory name may contain only letters, numbers, hyphens, and underscores." >&2
    exit 1
fi

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
target_dir="$script_dir/$directory_name"
solution_file="$script_dir/labors.sln"

if [[ ! -f "$solution_file" ]]; then
    echo "Error: the solution file is missing: $solution_file" >&2
    exit 1
fi

if [[ ! -d "$target_dir/src" || ! -d "$target_dir/tests" ]]; then
    echo "Error: expected src and tests directories were not found: $target_dir" >&2
    exit 1
fi

source_projects=("$target_dir/src"/*.csproj)
test_projects=("$target_dir/tests"/*.csproj)

if [[ ${#source_projects[@]} -ne 1 || ${#test_projects[@]} -ne 1 ]]; then
    echo "Error: expected exactly one .csproj file in both src and tests." >&2
    exit 1
fi

dotnet sln "$solution_file" remove "${source_projects[0]}" "${test_projects[0]}"
rm -rf "$target_dir"

echo "Project deleted: $target_dir"
