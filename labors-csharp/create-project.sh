#!/usr/bin/env bash

# Usage:   ./create-project.sh <directory-name> <ProjectName> <Namespace>
# Example: ./create-project.sh lorem-ipsum LoremIpsum UniObuda.Alga.Labxx
set -euo pipefail

if [[ $# -ne 3 ]]; then
    echo "Usage: $0 <directory-name> <ProjectName> <Namespace>" >&2
    exit 1
fi

directory_name="$1"
project_name="$2"
namespace_name="$3"

if [[ ! "$directory_name" =~ ^[A-Za-z0-9][A-Za-z0-9_-]*$ ]]; then
    echo "Error: the directory name may contain only letters, numbers, hyphens, and underscores." >&2
    exit 1
fi

if [[ ! "$project_name" =~ ^[A-Za-z_][A-Za-z0-9_]*$ ]]; then
    echo "Error: the project name must be a valid C# identifier." >&2
    exit 1
fi

if [[ ! "$namespace_name" =~ ^[A-Za-z_][A-Za-z0-9_]*(\.[A-Za-z_][A-Za-z0-9_]*)*$ ]]; then
    echo "Error: the namespace must contain dot-separated valid C# identifiers." >&2
    exit 1
fi

path_separator="/"
namespace_path="${namespace_name//./$path_separator}"

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
target_dir="$script_dir/$directory_name"
solution_file="$script_dir/labors.sln"
common_project="$script_dir/common/src/Common.csproj"
tasks_file="$script_dir/.vscode/tasks.json"
launch_file="$script_dir/.vscode/launch.json"

update_vscode_configuration() {
    local temporary_file
    local build_label="C#: Build $project_name"
    local run_label="C#: Run $project_name"
    local test_label="C#: Test $project_name"
    local source_project="$directory_name/src/$project_name.csproj"
    local test_project="$directory_name/tests/$project_name.Tests.csproj"
    local program_path="$directory_name/src/bin/Debug/net10.0/$project_name.dll"
    local source_directory="$directory_name/src"

    temporary_file="$(mktemp)"
    jq \
        --arg build_label "$build_label" \
        --arg run_label "$run_label" \
        --arg test_label "$test_label" \
        --arg source_project "$source_project" \
        --arg test_project "$test_project" \
        --arg workspace '${workspaceFolder}' \
        '
        .tasks = ((.tasks // [])
            | map(select(.label != $build_label and .label != $run_label and .label != $test_label))
            + [
                {
                    label: $build_label,
                    type: "process",
                    command: "dotnet",
                    args: ["build", $workspace + "/" + $source_project],
                    problemMatcher: "$msCompile",
                    group: "build"
                },
                {
                    label: $run_label,
                    type: "process",
                    command: "dotnet",
                    args: ["run", "--project", $workspace + "/" + $source_project],
                    problemMatcher: []
                },
                {
                    label: $test_label,
                    type: "process",
                    command: "dotnet",
                    args: ["test", $workspace + "/" + $test_project],
                    problemMatcher: []
                }
            ])
        ' "$tasks_file" > "$temporary_file"
    mv "$temporary_file" "$tasks_file"

    temporary_file="$(mktemp)"
    jq \
        --arg run_label "$run_label" \
        --arg build_label "$build_label" \
        --arg program_path "$program_path" \
        --arg source_directory "$source_directory" \
        --arg workspace '${workspaceFolder}' \
        '
        .configurations = ((.configurations // [])
            | map(select(.name != $run_label))
            + [
                {
                    name: $run_label,
                    type: "coreclr",
                    request: "launch",
                    preLaunchTask: $build_label,
                    program: ($workspace + "/" + $program_path),
                    args: [],
                    cwd: ($workspace + "/" + $source_directory),
                    console: "integratedTerminal",
                    stopAtEntry: false,
                    justMyCode: true
                }
            ])
        ' "$launch_file" > "$temporary_file"
    mv "$temporary_file" "$launch_file"
}

if [[ ! -f "$solution_file" ]]; then
    echo "Error: the solution file is missing: $solution_file" >&2
    exit 1
fi

if [[ ! -f "$common_project" ]]; then
    echo "Error: the shared Common project is missing: $common_project" >&2
    exit 1
fi

if ! command -v jq >/dev/null 2>&1; then
    echo "Error: jq is required to update the VS Code configuration files." >&2
    exit 1
fi

if [[ ! -f "$tasks_file" || ! -f "$launch_file" ]]; then
    echo "Error: VS Code configuration files are missing from $script_dir/.vscode." >&2
    exit 1
fi

if ! jq empty "$tasks_file" || ! jq empty "$launch_file"; then
    echo "Error: the VS Code configuration files contain invalid JSON." >&2
    exit 1
fi

if [[ -e "$target_dir" ]]; then
    echo "Error: the target directory already exists: $target_dir" >&2
    exit 1
fi

cd "$script_dir"
dotnet new console -n "$project_name" -o "$directory_name/src" --framework net10.0 --no-restore
dotnet new nunit -n "$project_name.Tests" -o "$directory_name/tests" --framework net10.0 --no-restore

# Keep the project files generated by the .NET templates. The only local style
# adjustment is disabling implicit usings in both projects.
sed -i.bak '/<ImplicitUsings>enable<\/ImplicitUsings>/d' "$directory_name/src/$project_name.csproj"
sed -i.bak '/<ImplicitUsings>enable<\/ImplicitUsings>/d' "$directory_name/tests/$project_name.Tests.csproj"
rm -f "$directory_name/src/$project_name.csproj.bak" "$directory_name/tests/$project_name.Tests.csproj.bak"

rm -f "$directory_name/src/Program.cs" "$directory_name/tests/UnitTest1.cs"
mkdir -p "$directory_name/src/$namespace_path" "$directory_name/tests/$namespace_path"

# Every executable project can use the shared classes from common. The test
# project references its own source project, which also brings this dependency.
dotnet add "$directory_name/src/$project_name.csproj" reference "$common_project"
dotnet add "$directory_name/tests/$project_name.Tests.csproj" reference "$directory_name/src/$project_name.csproj"

cat > "$directory_name/src/$namespace_path/Program.cs" <<EOF
namespace $project_name.$namespace_name;

using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World");
        Console.WriteLine("1 + 2 = {0}", Calculator.Add(1, 2));
    }
}
EOF

cat > "$directory_name/src/$namespace_path/Calculator.cs" <<EOF
namespace $project_name.$namespace_name;

public class Calculator
{
    public static int Add(int a, int b) => a + b;
}
EOF

cat > "$directory_name/tests/$namespace_path/ProgramTest.cs" <<EOF
namespace $project_name.Tests.$namespace_name;

using $project_name.$namespace_name;

public class ProgramTest
{
    [Test]
    public void Test1()
    {
        Assert.That(Calculator.Add(1, 2), Is.EqualTo(3));
    }
}
EOF

dotnet sln "$solution_file" add "$directory_name/src/$project_name.csproj" "$directory_name/tests/$project_name.Tests.csproj"
update_vscode_configuration
echo "Skeleton created: $target_dir"
